using Microsoft.EntityFrameworkCore;
using PenService.Domain.Core.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Infrastructure.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly PinServiceContext context;

        public GenericRepository(PinServiceContext context)
        {
            this.context = context;
        }
        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await context.Set<TEntity>().AddAsync(entity);
        }
        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            context.Entry<TEntity>(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }
        public async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            if(predicate == null) throw new ArgumentNullException(nameof(predicate));
            return await context.Set<TEntity>().FirstAsync(predicate);

        }
        public async Task<IEnumerable<TEntity>> SearchAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> SearchAsync<TEntity>() where TEntity : BaseEntity
        {
            return await context.Set<TEntity>().ToListAsync();
        }
    }
}
