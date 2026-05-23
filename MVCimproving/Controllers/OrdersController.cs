using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MVCimproving.Data;
using MVCimproving.Models;
using MVCimproving.Models.Factory;
using MVCimproving.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MVCimproving.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly RestaurantDbContext _context;

        public OrdersController(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dine = await _context.DineInOrders.Include(o => o.OrderItems).ToListAsync();
            var takeaway = await _context.TakeawayOrders.Include(o => o.OrderItems).ToListAsync();
            var delivery = await _context.DeliveryOrders.Include(o => o.OrderItems).ToListAsync();

            var all = dine.Cast<Order>().Concat(takeaway).Concat(delivery).OrderByDescending(o => o.OrderDate).ToList();
            return View(all);
        }

        public async Task<IActionResult> Details(string type, int id)
        {
            if (string.IsNullOrEmpty(type)) return NotFound();
            Order? order = await GetOrderByTypeAndId(type, id);
            if (order == null) return NotFound();
            return View(order);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.OrderTypes = new[] { "DineIn", "Takeaway", "Delivery" };
            ViewBag.MenuItems = await _context.MenuItems.ToListAsync();
            return View(new CreateOrderViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            ViewBag.OrderTypes = new[] { "DineIn", "Takeaway", "Delivery" };
            ViewBag.MenuItems = await _context.MenuItems.ToListAsync();

            if (string.IsNullOrEmpty(model.OrderType)) ModelState.AddModelError("OrderType", "Order type is required");
            if (!ModelState.IsValid) return View(model);

            var concrete = OrderFactory.CreateOrder(model.OrderType);
            concrete.OrderDate = model.OrderDate ?? DateTime.Now;

            // map items
            var items = new System.Collections.Generic.List<OrderItem>();
            foreach (var dto in model.Items)
            {
                // ensure menu item exists
                var menu = await _context.MenuItems.FindAsync(dto.MenuItemId);
                if (menu == null) continue;

                var oi = new OrderItem
                {
                    MenuItemId = dto.MenuItemId,
                    Quantity = dto.Quantity,
                    UnitPrice = dto.UnitPrice
                };
                items.Add(oi);
            }

            // map specific fields
            if (concrete is DeliveryOrder d)
            {
                d.DeliveryAddress = model.DeliveryAddress ?? string.Empty;
                d.DeliveryFee = model.DeliveryFee ?? 0m;
            }
            else if (concrete is DineInOrder di)
            {
                di.TableNumber = model.TableNumber ?? 0;
            }
            else if (concrete is TakeawayOrder t)
            {
                t.PickupTime = model.PickupTime ?? string.Empty;
            }

            concrete.OrderItems = items;
            concrete.CalculateTotal();

            _context.Add(concrete);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string type, int id)
        {
            var order = await GetOrderByTypeAndId(type, id);
            if (order == null) return NotFound();
            return View(order);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string type, int id)
        {
            var order = await GetOrderByTypeAndId(type, id);
            if (order != null)
            {
                _context.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Cancel(string type, int id)
        {
            var order = await GetOrderByTypeAndId(type, id);
            if (order == null) return NotFound();

            // Users can only cancel their own pending orders
            if (order.Status != "Pending")
            {
                return BadRequest("Only pending orders can be cancelled");
            }

            return View(order);
        }

        [Authorize]
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelConfirmed(string type, int id)
        {
            var order = await GetOrderByTypeAndId(type, id);
            if (order != null && order.Status == "Pending")
            {
                order.Status = "Cancelled";
                _context.Update(order);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Order cancelled successfully";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<Order?> GetOrderByTypeAndId(string type, int id)
        {
            return type.ToLower() switch
            {
                "dinein" => await _context.DineInOrders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id),
                "takeaway" => await _context.TakeawayOrders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id),
                "delivery" => await _context.DeliveryOrders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id),
                _ => null,
            };
        }
    }
}
