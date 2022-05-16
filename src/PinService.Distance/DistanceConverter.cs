namespace PinService.Distance
{
    public class DistanceConverter
    {
        public static readonly double MilesInOneMetre = 0.00062137;
        public static readonly double KiloMetresInOneMetre = 0.001;

        public static double MetresToKiloMetres(double metres)
        {
            return metres * KiloMetresInOneMetre;
        }
        public static double MetresToMiles(double metres)
        {
            return metres * MilesInOneMetre;
        }
        public static double MilesToMetres(double miles)
        {
            return miles / MilesInOneMetre;
        }
        public static double KiloMetreToMetres(double kms)
        {
            return kms / KiloMetresInOneMetre;
        }
    }
}
