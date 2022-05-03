using PenService.Domain.Core.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Infrastructure.Repositories
{
    public interface IGenericRepository
    {
        Task AddAsync<TEntity>(TEntity entity) where TEntity: BaseEntity;
        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> SearchAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> SearchAsync<TEntity>() where TEntity : BaseEntity;

    }
}
