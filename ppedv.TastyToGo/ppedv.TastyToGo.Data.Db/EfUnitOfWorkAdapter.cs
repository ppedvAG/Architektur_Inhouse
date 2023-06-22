using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Data.Db
{
    public class EfUnitOfWorkAdapter : IUnitOfWork
    {
        private TastyToGoContext context;

        public IRepository<Order> OrderRepo => new EfRepositoryAdapter<Order>(context);

        public IRepository<Customer> CustomerRepo => new EfRepositoryAdapter<Customer>(context);

        public IProductRepo ProductRepo => new EfProductRepositoryAdapter(context);

        public EfUnitOfWorkAdapter(string conString)
        {
            context = new TastyToGoContext(conString);
        }

        public void SaveAll()
        {
            context.SaveChanges();
        }
    }
}
