using System;
using System.Collections.Generic;

namespace MVCimproving.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        public string OrderType { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }

        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        // Delivery / DineIn / Takeaway specific fields
        public string? DeliveryAddress { get; set; } = string.Empty;
        public decimal? DeliveryFee { get; set; }
        public int? TableNumber { get; set; }
        public string? PickupTime { get; set; } = string.Empty;
    }
}