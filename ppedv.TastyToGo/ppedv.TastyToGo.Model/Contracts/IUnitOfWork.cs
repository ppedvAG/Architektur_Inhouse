using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Model.Contracts
{
    public interface IUnitOfWork
    {
        public IRepository<Order> OrderRepo { get; }
        public IRepository<Customer> CustomerRepo { get; }
        public IProductRepo ProductRepo { get; }

        void SaveAll();
    }
}
