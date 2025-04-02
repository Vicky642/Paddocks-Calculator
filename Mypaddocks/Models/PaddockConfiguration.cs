using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mypaddocks.Models
{
    public class PaddockConfiguration
    {
        public int CowsPerPaddock { get; set; }
        public string LayoutOption { get; set; }
        public double PaddockLength { get; set; }
        public double PaddockWidth { get; set; }
        public double PaddockArea { get; set; }
        public int PaddocksPerRow { get; set; }
        public int PaddocksPerColumn { get; set; }
        public int TotalFittingPaddocks { get; set; }
        public double RemainingArea { get; set; }
        public string[] PaddockNames { get; set; }
        public string FormattedDimensions =>
            $"{PaddockLength:N2}m × {PaddockWidth:N2}m";
        public string FormattedArea =>
            $"{PaddockArea:N2} m²";
        public string FormattedFarmArea => $"{FarmArea:N2} m²";
        public string FormattedRemainingArea =>
            $"{RemainingArea:N2} m²";
        public double SpacePerCow => PaddockArea / CowsPerPaddock;
        public string FormattedSpacePerCow =>
            $"{SpacePerCow:N2} m²/cow";
        public int NumberOfPaddocks { get; set; }
        public double FarmArea { get; set; }
    }
}
