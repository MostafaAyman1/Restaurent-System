namespace MVCimproving.Models.Factory
{
    public class OrderFactory
    {
        public static Order CreateOrder(string orderType)
        {
            return orderType.ToLower() switch
            {
                "dinein" => new DineInOrder(),
                "takeaway" => new TakeawayOrder(),
                "delivery" => new DeliveryOrder(),
                _ => throw new ArgumentException("Invalid order type"),
            };
        }
    }
}
