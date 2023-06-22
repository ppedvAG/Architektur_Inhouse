using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Model.Contracts
{
    public interface IProductRepo : IRepository<Product>
    {
        IEnumerable<Product> GetAllByStoredProc();
    }
}
