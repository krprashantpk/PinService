
namespace PinService.Cache.USAZctaCaches;

public class USAZctaCachekeys
{

    public const string EveryZcta = "PinService_EveryZcta";
    public static string NearByRadius(int zcta, int radius) => $"PinService_Zcta{zcta}_Radius{radius}";

}
