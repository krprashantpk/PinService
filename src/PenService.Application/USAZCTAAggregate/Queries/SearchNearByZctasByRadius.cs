using FluentValidation;
using MediatR;
using PenService.Domain.USAZCTAAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenService.Application.USAZCTAAggregate.Queries
{
    public class SearchNearByZctasByRadius : IRequest<IEnumerable<USAZcta>>
    {
        public SearchNearByZctasByRadius(uint radius, string zcta)
        {
            Radius = radius;
            Zcta = zcta;
        }
        public uint Radius { get; }
        public string Zcta { get; }
    }

    public class SearchNearByZctasByRadiusValidator : AbstractValidator<SearchNearByZctasByRadius>
    {
        public SearchNearByZctasByRadiusValidator()
        {

            RuleFor(x => x.Zcta)
                .NotNull()
                .NotEmpty()
                .Length(5, 5)
                .Must(x => int.TryParse(x, out int _));

        }
    }
}
