using PenService.Distance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PenService.UnitTesting.Distance
{
    public class HaversineFormulaTest
    {
        [Fact]
        public void ValidateHaversineFormula()
        {

            double lat1 = 18.18055534;
            double long1 = -66.74996185;
            double lat2 = 18.3619442;
            double long2 = -67.17559814;
            var actual = HaversineFormula.Distance(lat1, long1, lat2, long2);
            Assert.Equal(49.26, actual);
        }

    }
}
