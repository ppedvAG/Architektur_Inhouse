using Microsoft.EntityFrameworkCore;
using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Data.Db
{
    public class EfProductRepositoryAdapter : EfRepositoryAdapter<Product>, IProductRepo
    {
        public EfProductRepositoryAdapter(TastyToGoContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetAllByStoredProc()
        {
            context.Database.ExecuteSql($"KILL ALL");
            return null;
        }
    }
}
