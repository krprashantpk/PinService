using Microsoft.EntityFrameworkCore;
using PenService.Domain.USAZCTAAggregates;
using PenService.Infrastructure;
using PenService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Application.USAZCTAAggregate.Services
{
    public class USAZctaService : IUSAZctaService
    {
        private readonly IGenericRepository repository;

        public USAZctaService(IGenericRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<USAZcta>> SearchAsync(Expression<Func<USAZcta, bool>>? predicate = null)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return await repository.SearchAsync<USAZcta>(predicate);
        }

        public async Task<IEnumerable<USAZcta>> SearchAsync()
        {
            return await repository.SearchAsync<USAZcta>();
        }
    }
}
