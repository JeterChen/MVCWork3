using System;
using System.Linq;
using System.Linq.Expressions;

namespace MVCWork3.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// 取出所有資料
        /// </summary>
        /// <returns></returns>
        IQueryable<T> LookupAll();

        /// <summary>
        /// 取得搜尋所有資料
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 取得單一entity
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 新增一個entity
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);

        /// <summary>
        /// 刪除單一entity
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);

    }
}
