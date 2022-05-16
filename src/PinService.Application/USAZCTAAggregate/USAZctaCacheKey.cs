namespace PinService.Application.USAZCTAAggregate
{
    public class USAZctaCacheKey
    {
        public static readonly string USAZCTA = "USAZCTA";
        public static string ZctaKey(int Zcta, uint Radius) => $"USAZCTA_{Zcta}_{Radius}";

    }
}
