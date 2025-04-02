using System.Windows;
using System.Windows.Media;

namespace Mypaddocks.Models
{
    public class PaddockVisual
    {
        /// <summary>
        /// Paddock identifier (e.g., "A", "B", "AA", etc.)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// X coordinate of top-left corner (in meters)
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y coordinate of top-left corner (in meters)
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Width of paddock in meters (for visualization)
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height of paddock in meters (for visualization)
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Color for visual representation
        /// </summary>
        public string Color { get; set; } = "#4CAF50"; // Default green

        /// <summary>
        /// Opacity level for visualization (0-1)
        /// </summary>
        public double Opacity { get; set; } = 0.7;

        /// <summary>
        /// Formatted tooltip text for UI display
        /// </summary>
        public string Tooltip =>
            $"{Name}\n" +
            $"Position: ({X:N1}, {Y:N1})\n" +
            $"Dimensions: {Width:N1}m × {Height:N1}m\n" +
            $"Area: {Width * Height:N1}m²";

        /// <summary>
        /// Corner coordinates for potential drawing operations
        /// [0] = Top-Left, [1] = Top-Right
        /// [2] = Bottom-Right, [3] = Bottom-Left
        /// </summary>
        public Point[] Corners => new Point[]
        {
            new Point(X, Y),
            new Point(X + Width, Y),
            new Point(X + Width, Y + Height),
            new Point(X, Y + Height)
        };

        // Add this property
        public Brush BackgroundColor { get; set; } = Brushes.Green;

         private Brush _backgroundColor;
   

    }
}
