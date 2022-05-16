using PinService.Domain.Core.Seed;
using System.Linq.Expressions;

namespace PinService.Domain.Core.Interfaces
{
    public interface IGenericRepository
    {
        Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> SearchAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> SearchAsync<TEntity>() where TEntity : BaseEntity;

    }
}
