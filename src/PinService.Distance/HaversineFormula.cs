using PinService.Domain.Core.Seed;

namespace PinService.Distance
{
    /// <summary>
    /// Haversine formula to calculate the great-circle distance between two points – that is, the shortest distance over the earth’s surface.
    /// </summary>
    public class HaversineFormula
    {
        public static readonly double R = 6371e3; // metres

        /// <summary>
        /// </summary>
        /// <param name="lat1">Latitue of the First Point</param>
        /// <param name="lon1">Longitude of the First Point</param>
        /// <param name="lat2">Latitue of the Second Point</param>
        /// <param name="lon2">Longitude of the Second Point</param>
        /// <returns>Distance between two longitude and latitude in Metres</returns>
        public static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            return CalculateDistance(lat1, lon1, lat2, lon2);
        }
        public static double Distance(double lat1, double lon1, double lat2, double lon2, Unit unit)
        {
            return CalculateDistance(lat1, lon1, lat2, lon2, unit);
        }
        public static double Distance(double lat1, double lon1, double lat2, double lon2, int decimalDegits)
        {
            return CalculateDistance(lat1, lon1, lat2, lon2, decimalDigit: decimalDegits);
        }
        public static double Distance(double lat1, double lon1, double lat2, double lon2, Unit unit, int decimalDegits)
        {
            return CalculateDistance(lat1, lon1, lat2, lon2, unit, decimalDigit: decimalDegits);
        }
        public static double Distance(Point point1, Point point2)
        {
            if (point1 == null || point2 == null) throw new ArgumentNullException("Point");
            return CalculateDistance(point1.Latitude, point1.Longitude, point2.Latitude, point2.Longitude);
        }
        public static double Distance(Point point1, Point point2, Unit unit)
        {
            if (point1 == null || point2 == null) throw new ArgumentNullException("Point");
            return CalculateDistance(point1.Latitude, point1.Longitude, point2.Latitude, point2.Longitude, unit);
        }
        public static double Distance(Point point1, Point point2, int decimalDigit)
        {
            if (point1 == null || point2 == null) throw new ArgumentNullException("Point");
            return CalculateDistance(point1.Latitude, point1.Longitude, point2.Latitude, point2.Longitude, decimalDigit: decimalDigit);
        }
        public static double Distance(Point point1, Point point2, Unit unit, int decimalDigit)
        {
            if (point1 == null || point2 == null) throw new ArgumentNullException("Point");

            return CalculateDistance(point1.Latitude, point1.Longitude, point2.Latitude, point2.Longitude, unit, decimalDigit: decimalDigit);
        }

        #region Private Method To Calculate the Distance
        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2, Unit unit = Unit.KiloMetre, int decimalDigit = 2)
        {
            double φ1 = lat1 * Math.PI / 180; // φ, λ in radians
            double φ2 = lat2 * Math.PI / 180;
            double Δφ = (lat2 - lat1) * Math.PI / 180;
            double Δλ = (lon2 - lon1) * Math.PI / 180;

            double a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                 Math.Cos(φ1) * Math.Cos(φ2) *
                 Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distanceInMetres = R * c;
            double result = 0;
            switch (unit)
            {
                case Unit.KiloMetre:
                    result = Math.Round(DistanceConverter.MetresToKiloMetres(distanceInMetres), decimalDigit);
                    break;
                case Unit.Mile:
                    result = Math.Round(DistanceConverter.MetresToMiles(distanceInMetres), decimalDigit);
                    break;
                case Unit.Metre:
                    result = Math.Round(distanceInMetres, decimalDigit);
                    break;
            }
            return result;
        }
        #endregion 
    }
}
