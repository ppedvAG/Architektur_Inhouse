namespace ppedv.TastyToGo.Model.DomainModel
{
    public class Product : Entity
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int KCal { get; set; }
        public bool Vegan { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    }
}