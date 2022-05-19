using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PinService.Domain.Core.Interfaces;

namespace PinService.Application
{
    public class UnitOfWork<Context> : IUnitOfWork<Context> where Context : DbContext
    {
        private readonly DbContext dbContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DbContext DbContext => dbContext;
        public bool IsTransactionActive => _transaction != null;
        public IDbContextTransaction ContextTransaction
        {
            get
            {
                if (IsTransactionActive) return _transaction;
                throw new InvalidOperationException("No Active Transaction");
            }
        }
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (IsTransactionActive) throw new InvalidOperationException("Active Transaction is in progress");

            return _transaction = await dbContext.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            if (!IsTransactionActive) throw new InvalidOperationException("No Active Transaction.");
            await _transaction!.CommitAsync();
        }
        public async Task RollbackAsync()
        {
            if (!IsTransactionActive) throw new InvalidOperationException("No Active Transaction.");
            await _transaction!.RollbackAsync();
        }
    }
}
