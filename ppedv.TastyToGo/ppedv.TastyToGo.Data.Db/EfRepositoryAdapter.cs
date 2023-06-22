using ppedv.TastyToGo.Model.Contracts;
using ppedv.TastyToGo.Model.DomainModel;

namespace ppedv.TastyToGo.Data.Db
{

    public class EfRepositoryAdapter<T> : IRepository<T> where T : Entity
    {
        protected TastyToGoContext context;

        public EfRepositoryAdapter(TastyToGoContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            context.Add(entity);
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public IQueryable<T> Query()
        {
            return context.Set<T>();
        }

        public T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }
    }
}
