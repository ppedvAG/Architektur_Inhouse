namespace ppedv.TastyToGo.Model.DomainModel
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public DateTime Modified { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
    }
}