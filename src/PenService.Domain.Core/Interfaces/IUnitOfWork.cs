using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Domain.Core.Interfaces
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
