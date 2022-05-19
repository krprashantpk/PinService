using AutoMapper;
using PinService.Application.USAZCTAAggregate.Dtos;
using PinService.Domain.USAZCTAAggregates;

namespace PinService.Application.USAZCTAAggregate
{
    public class USAZctaProfile : Profile
    {
        public USAZctaProfile()
        {
            CreateMap<USAZcta, USAZctaDto>();
        }
    }
}
