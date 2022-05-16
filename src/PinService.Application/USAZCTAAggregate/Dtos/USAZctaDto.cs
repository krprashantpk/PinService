using System.Runtime.Serialization;

namespace PinService.Application.USAZCTAAggregate.Dtos
{
    [DataContract]
    public class USAZctaDto
    {
        [DataMember]
        public int Zcta { get; set; }

        [DataMember]
        public string? StateCode { get; set; }

        [DataMember]
        public string? State { get; set; }

        [DataMember]
        public string? County { get; set; }

    }
}
