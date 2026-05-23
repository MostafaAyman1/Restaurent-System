using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCimproving.Models
{
    public abstract class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public virtual decimal CalculateTotal()
        {
            TotalAmount = OrderItems.Sum(item => item.GetTotal());
            return TotalAmount;
        }

        public abstract string GetOrderType();
    }
}
