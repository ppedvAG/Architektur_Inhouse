using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Data.Db
{
    public class EfRepositoryAdapter : IRepository
    {
        private TastyToGoContext context;

        public EfRepositoryAdapter(string conString)
        {
            context = new TastyToGoContext(conString);
        }

        public void Add<T>(T entity) where T : Entity
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            context.Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return context.Set<T>().ToList();
        }

        public T? GetById<T>(int id) where T : Entity
        {
            return context.Set<T>().Find(id);
        }

        public void SaveAll()
        {
            context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            context.Update(entity);
        }
    }
}
