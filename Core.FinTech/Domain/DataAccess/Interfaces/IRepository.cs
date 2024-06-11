using Core.Common.Domain.Entities;
using System.Linq.Expressions;

namespace Core.FinTech.Domain.DataAccess.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool getDeleted = false, int? page = null, int? count = null,
            params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetNotTracked(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool getDeleted = false, int? page = null, int? count = null,
            params Expression<Func<T, object>>[] includeProperties);
        Task<T?> Find(object id, bool getDeleted = false);
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task Delete(object id, Guid? deletedBy = null);
        void HardDelete(T entity);
        Task<long> Count(Expression<Func<T, bool>>? filter = null, bool getDeleted = false);
    }
}
