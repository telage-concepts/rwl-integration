using Core.Common.Domain.Entities;
using Core.FinTech.Domain.Contexts;
using Core.FinTech.Domain.DataAccess.Interfaces;

namespace Core.FinTech.Domain.DataAccess.Implementations
{
  public class UnitOfWork<T> : IUnitOfWork<T> where T : EntityBase, new()
    {
        private bool disposed = false;
        private readonly AppDbContext context;
        private readonly IRepository<T> repository;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            repository = new Repository<T>(context);
        }

        public IRepository<T> Repository => repository;

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
