using Core.Common.Domain.Entities;

namespace Core.FinTech.Domain.DataAccess.Interfaces
{
  public interface IUnitOfWork<T> : IDisposable where T : EntityBase, new()
    {
        IRepository<T> Repository { get; }
        Task SaveAsync();
        void Save();
    }
}
