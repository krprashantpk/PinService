using PinService.Domain.Core.Seed;

namespace PinService.Domain.USAZCTAAggregates
{
    public class USAZcta : BaseEntity
    {
        public USAZcta(int zcta, double latitude, double longitude, string stateCode, string state, string county)
        {
            Zcta = zcta;
            Latitude = latitude;
            Longitude = longitude;
            StateCode = stateCode;
            State = state;
            County = county;
        }
        public int Zcta { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public string StateCode { get; }
        public string State { get; }
        public string County { get; }
    }
}
