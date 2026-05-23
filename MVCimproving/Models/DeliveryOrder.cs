namespace MVCimproving.Models
{
    public class DeliveryOrder : Order
    {
        public string DeliveryAddress { get; set; } = string.Empty;
        public decimal DeliveryFee { get; set; }

        public override decimal CalculateTotal()
        {
            return base.CalculateTotal() + DeliveryFee;
        }
        public override string GetOrderType()
        {
            return "Delivery";
        }

    }
}
