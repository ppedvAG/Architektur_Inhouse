namespace ppedv.TastyToGo.Model
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string OrderNumber { get; set; } = string.Empty;
        public required virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}