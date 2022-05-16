using FluentValidation;
using MediatR;
using PinService.Application.USAZCTAAggregate.Dtos;

namespace PinService.Application.USAZCTAAggregate.Queries
{
    public class SearchNearByZctasByRadius : IRequest<IEnumerable<USAZctaDto>>
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
