using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PinService.Application.USAZCTAAggregate.Dtos;
using PinService.Application.USAZCTAAggregate.Queries;
using PinService.Distance;
using PinService.Domain.Core.Interfaces;
using PinService.Domain.Core.MediatR;
using PinService.Domain.USAZCTAAggregates;

namespace PinService.Application.USAZCTAAggregate.Handlers
{
    public class USAZctaQueryHandler : IRequestHandler<SearchNearByZctasByRadius, IEnumerable<USAZctaDto>>
    {
        private readonly IGenericRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<USAZctaQueryHandler> logger;

        public USAZctaQueryHandler(
            IGenericRepository repository,
            IMapper mapper,
            ILogger<USAZctaQueryHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<IEnumerable<USAZctaDto>> Handle(SearchNearByZctasByRadius request, CancellationToken cancellationToken)
        {
            var wholeZCTAs = await repository.SearchAsync<USAZcta>();

            var zip1 = wholeZCTAs!.FirstOrDefault(x => x.Zcta == int.Parse(request.Zcta)!);

            if (zip1 == null)
            {
                var error = $"Entered USA Zcta {request.Zcta} doesn't exit.";
                logger.LogWarning(error);
                throw new RequestValidationException<SearchNearByZctasByRadius, IEnumerable<USAZctaDto>>(Domain.Core.Seed.ErrorCode.RequestValidationFailed, error);
            }

            var result = wholeZCTAs.Where(
                item =>
                HaversineFormula.Distance(zip1.Latitude, zip1.Longitude, item.Latitude, item.Longitude) <= request.Radius
                );

            return mapper.Map<IEnumerable<USAZcta>, IEnumerable<USAZctaDto>>(result);
        }
    }
}
