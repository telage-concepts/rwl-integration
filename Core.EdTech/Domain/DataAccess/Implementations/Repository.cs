using Core.Common.Domain.Entities;
using Core.EdTech.Domain.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.EdTech.Domain.DataAccess.Implementations
{
  public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbSet;

        internal Repository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task<long> Count(Expression<Func<T, bool>>? filter = null, bool getDeleted = false)
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            query = query.Where(e => !e.IsDeleted && e.IsDeleted == getDeleted);


            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            entity.LastUpdatedBy = entity.DeletedBy;

            Update(entity);
        }

        public async Task Delete(object id, Guid? deletedBy = null)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.DeletedBy = deletedBy;
                Delete(entity);
            }
        }

        public async Task<T?> Find(object id, bool getDeleted = false)
        {
            var record = await dbSet.FindAsync(id);

            if (record == null) return null;
            if (!getDeleted && record.IsDeleted) return null;

            return record;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool getDeleted = false, int? page = null, int? count = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            if (!getDeleted) query = query.Where(e => !e.IsDeleted);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties == null) includeProperties = Array.Empty<Expression<Func<T, object>>>();
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            if (orderBy != null)
            {
                return (page != null && count != null)
                    ? orderBy(query).Skip(((page.Value - 1 < 0) ? 0 : (page.Value - 1)) * count.Value).Take(count.Value)
                    : orderBy(query);
            }
            else
            {
                return (page != null && count != null)
                    ? query.Skip(((page.Value - 1 < 0) ? 0 : (page.Value - 1)) * count.Value).Take(count.Value)
                    : query;
            }
        }

        public IQueryable<T> GetNotTracked(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool getDeleted = false, int? page = null, int? count = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            if (!getDeleted) query = query.Where(e => !e.IsDeleted);


            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties == null) includeProperties = Array.Empty<Expression<Func<T, object>>>();
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }

            if (orderBy != null)
            {
                return (page != null && count != null)
                    ? orderBy(query).Skip(((page.Value - 1 < 0) ? 0 : (page.Value - 1)) * count.Value).Take(count.Value)
                    : orderBy(query);
            }
            else
            {
                return (page != null && count != null)
                    ? query.Skip(((page.Value - 1 < 0) ? 0 : (page.Value - 1)) * count.Value).Take(count.Value)
                    : query;
            }
        }

        public void HardDelete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public async Task Insert(T entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            await dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            entity.LastUpdatedOn = DateTime.UtcNow;

            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
