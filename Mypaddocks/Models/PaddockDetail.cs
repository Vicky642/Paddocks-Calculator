using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mypaddocks.Models
{
    public class PaddockDetail
    {
        /// <summary>
        /// Name/identifier of the paddock (e.g., "A", "B", "AA", etc.)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Length of the paddock in meters
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Width of the paddock in meters
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Area of the paddock in square meters
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Array containing the corner coordinates:
        /// [0] = Top Left X
        /// [1] = Top Left Y
        /// [2] = Bottom Right X
        /// [3] = Bottom Right Y
        /// </summary>
        public double[] Coordinates { get; set; }

        /// <summary>
        /// Number of cows in this paddock
        /// </summary>
        public int Cows { get; set; }

        /// <summary>
        /// Layout option used for this paddock (Option A or Option B)
        /// </summary>
        public string LayoutOption { get; set; }

        /// <summary>
        /// X coordinate for visual representation (top-left corner)
        /// </summary>
        public double XPosition { get; set; }

        /// <summary>
        /// Y coordinate for visual representation (top-left corner)
        /// </summary>
        public double YPosition { get; set; }

        /// <summary>
        /// Formatted string of the coordinates for display purposes
        /// </summary>
        public string FormattedCoordinates => 
            $"TL:({Coordinates[0]:N1}, {Coordinates[1]:N1}) BR:({Coordinates[2]:N1}, {Coordinates[3]:N1})";

        /// <summary>
        /// Formatted dimensions for display purposes
        /// </summary>
        public string FormattedDimensions => 
            $"{Length:N2}m × {Width:N2}m";

        /// <summary>
        /// Formatted area for display purposes
        /// </summary>
        public string FormattedArea => 
            $"{Area:N2} m²";
    }
}
