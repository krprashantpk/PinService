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
        private readonly ICacheService cache;
        private readonly ILogger<USAZctaQueryHandler> logger;

        public USAZctaQueryHandler(
            IGenericRepository repository,
            IMapper mapper,
            ICacheService cache,
            ILogger<USAZctaQueryHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.cache = cache;
            this.logger = logger;
        }
        public async Task<IEnumerable<USAZctaDto>> Handle(SearchNearByZctasByRadius request, CancellationToken cancellationToken)
        {
            List<USAZcta> result = new List<USAZcta>();

            var resultCacheKey = USAZctaCacheKey.ZctaKey(int.Parse(request.Zcta)!, request.Radius);

            if (cache.TryGetValue<List<USAZcta>>(resultCacheKey, out result))
            {
                return mapper.Map<IEnumerable<USAZcta>, IEnumerable<USAZctaDto>>(result);
            }

            var wholeZCTAs = await cache.CreateOrGetValueAsync<IEnumerable<USAZcta>>(USAZctaCacheKey.USAZCTA, async () =>
            {
                return await repository.SearchAsync<USAZcta>();

            });

            var zip1 = wholeZCTAs!.FirstOrDefault(x => x.Zcta == int.Parse(request.Zcta)!);

            if (zip1 == null)
            {
                var error = $"Entered USA Zcta {request.Zcta} doesn't exit.";
                logger.LogWarning(error);
                throw new RequestValidationException<SearchNearByZctasByRadius, IEnumerable<USAZctaDto>>(Domain.Core.Seed.ErrorCode.RequestValidationFailed, error);
            }

            result = await cache.SetValueAsync(resultCacheKey, async () =>
            {
                if (result == null) result = new List<USAZcta>();
                foreach (var item in wholeZCTAs!)
                {
                    var distance = HaversineFormula.Distance(zip1.Latitude, zip1.Longitude, item.Latitude, item.Longitude);
                    if (distance <= request.Radius) result.Add(item);
                }
                return await Task.FromResult(result);

            });

            return mapper.Map<IEnumerable<USAZcta>, IEnumerable<USAZctaDto>>(result);
        }
    }
}
