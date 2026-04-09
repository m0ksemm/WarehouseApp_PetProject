using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WP.DataAccess.Interfaces;

namespace WP.DataAccess
{
    public class BaseUnitOfWork<TContext> : IUnitOfWork, IDisposable, IAsyncDisposable
        where TContext : DbContext
    {
        private readonly TContext _context;

        public BaseUnitOfWork(TContext context) 
        {
            _context = context;
        }

        public IDbContextTransaction Transaction { get; private set; }

        public async Task CommitTransactionAsync()
        {
            if (Transaction == null) 
            {
                throw new InvalidOperationException("There is no active transaction");
            }

            try
            {
                await SaveChangesAsync();
                await Transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
            }
            finally
            {
                if (Transaction != null) 
                {
                    await Transaction.DisposeAsync();
                    Transaction = null;
                }
            }
        }

        public async Task<IDbContextTransaction> CreateTransactionAsync()
        {
            if (Transaction != null) 
            {
                throw new InvalidOperationException("A transaction is already in progress. Please commit or rollback the current transaction before starting a new one.");
            }

            Transaction = await _context.Database.BeginTransactionAsync();
            return Transaction;
        }

        public void Dispose()
        {
            _context?.Dispose();
            Transaction?.Dispose();
        }

        public virtual async ValueTask DisposeAsync()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }

            if (Transaction != null)
            {
                await Transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (Transaction == null)
            {
                throw new InvalidOperationException("There is no active transaction.");
            }

            try 
            {
                await Transaction.RollbackAsync();
            }
            catch
            {
                await Transaction.DisposeAsync();
                Transaction = null;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
