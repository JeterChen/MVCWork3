using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCWork3.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork { get; set; }
        private DbSet<T> _objectSet;
        private DbSet<T> ObjectSet => _objectSet ?? (_objectSet = UnitOfWork.Context.Set<T>());

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Create(T entity)
        {
            ObjectSet.Add(entity);
        }

        public T GetSingle(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.SingleOrDefault(filter);
        }

        public IQueryable<T> LookupAll()
        {
            return ObjectSet;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.Where(filter);
        }

        public void Remove(T entity)
        {
            ObjectSet.Remove(entity);
        }
    }
}
