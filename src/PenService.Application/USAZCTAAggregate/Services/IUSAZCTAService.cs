using PenService.Domain.USAZCTAAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Application.USAZCTAAggregate.Services
{
    public interface IUSAZctaService
    {
        Task<IEnumerable<USAZcta>> SearchAsync();
        Task<IEnumerable<USAZcta>> SearchAsync(Expression<Func<USAZcta, bool>>? predicate = null);
    }
}
