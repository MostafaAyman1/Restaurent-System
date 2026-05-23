namespace MVCimproving.Models
{
    public class TakeawayOrder : Order
    {
        public string PickupTime { get; set; } = string.Empty;
        public override string GetOrderType()
        {
            return "Takeaway";
        }
    }
}
