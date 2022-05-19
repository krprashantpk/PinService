using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace PinService.Domain.Core.Interfaces
{
    public interface IUnitOfWork<Context> where Context : DbContext
    {
        DbContext DbContext { get; }
        bool IsTransactionActive { get; }
        IDbContextTransaction ContextTransaction { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
