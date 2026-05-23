namespace MVCimproving.Models
{
    public class DineInOrder : Order
    {
        
        public int TableNumber { get; set; }

        public override string GetOrderType()
        {
            return "Dine-In";
        }
    }
}
   