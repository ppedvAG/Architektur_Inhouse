namespace ppedv.TastyToGo.Model
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public DateTime Modified { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
    }

    public class Customer : Entity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }

    public class Product : Entity
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int KCal { get; set; }
        public bool Vegan { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    }

    public class Order : Entity
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string OrderNumber { get; set; } = string.Empty;
        public required virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }

    public class OrderItem : Entity
    {
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public required virtual Order Order { get; set; }
        public required virtual Product Product { get; set; }
    }
}