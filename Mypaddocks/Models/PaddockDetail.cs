using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mypaddocks.Models
{
    public class PaddockDetail
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Area { get; set; }
        public double[] Coordinates { get; set; }
        public int Cows { get; set; }
        public string LayoutOption { get; set; }
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public string FormattedCoordinates => 
            $"TL:({Coordinates[0]:N1}, {Coordinates[1]:N1}) BR:({Coordinates[2]:N1}, {Coordinates[3]:N1})";
        public string FormattedDimensions => 
            $"{Length:N2}m × {Width:N2}m";
        public string FormattedArea => 
            $"{Area:N2} m²";
    }
}
