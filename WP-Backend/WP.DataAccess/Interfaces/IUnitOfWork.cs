using Microsoft.EntityFrameworkCore.Storage;

namespace WP.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IDbContextTransaction Transaction { get; }

        Task<IDbContextTransaction> CreateTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();

        Task SaveChangesAsync();
    }
}
