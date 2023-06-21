namespace ppedv.TastyToGo.Model.DomainModel
{
    public class OrderItem : Entity
    {
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public required virtual Order Order { get; set; }
        public required virtual Product Product { get; set; }
    }
}