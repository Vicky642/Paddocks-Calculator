
using Mypaddocks.Interfaces;
using Mypaddocks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mypaddocks.Repository
{
    public class CalculationRepository : ICalculationRepository
    {
        public CalculationResult CalculatePaddockLayout(FarmDimensions dimensions, PaddockConfiguration config)
        {
            dimensions = new FarmDimensions
            {
                Length = AppState.FarmLength,
                Width = AppState.FarmWidth,
                //Area = AppState.FarmArea,
            };

            config = new PaddockConfiguration
            {
                CowsPerPaddock = AppState.CowsPerPaddock,
                FarmArea = AppState.FarmArea
                
            };

            // Implement the core calculation logic here
            var result = new CalculationResult
            {
                FarmDimensions = dimensions,
                PaddockConfiguration = config
            };

            // Perform the calculations
            (bool isValid, double length, double width, string layoutOption) = FindOptimalPaddockDimensions(
                config.CowsPerPaddock,
                dimensions.Length,
                dimensions.Width);

            if (isValid)
            {
                result.PaddockConfiguration.PaddockLength = length;
                result.PaddockConfiguration.PaddockWidth = width;
                result.PaddockConfiguration.LayoutOption = layoutOption;
                result.PaddockConfiguration.PaddockArea = length * width;
                result.PaddockConfiguration.FarmArea = config.FarmArea;

                // Calculate how many paddocks fit
                result.PaddockConfiguration.PaddocksPerRow = (int)(dimensions.Width / width);
                result.PaddockConfiguration.PaddocksPerColumn = (int)(dimensions.Length / length);
                result.PaddockConfiguration.TotalFittingPaddocks =
                    result.PaddockConfiguration.PaddocksPerRow * result.PaddockConfiguration.PaddocksPerColumn;

                // Calculate remaining space
                result.PaddockConfiguration.RemainingArea = config.FarmArea -
                    (result.PaddockConfiguration.TotalFittingPaddocks * result.PaddockConfiguration.PaddockArea);

                // Generate paddock names and coordinates
                result.PaddockConfiguration.PaddockNames = GeneratePaddockNames(result.PaddockConfiguration.TotalFittingPaddocks);
                result.PaddockCoordinates = CalculatePaddockCoordinates(result);

                //Debug.WriteLine($"--- DEBUG INFO ---");
                //Debug.WriteLine($"Farm Dimensions: {dimensions.Length}m (Length/X) × {dimensions.Width}m (Width/Y)");
                //Debug.WriteLine($"Paddock Dimensions: {length}m (Length/X) × {width}m (Width/Y)");
                //Debug.WriteLine($"Paddocks per row (X-axis): {(int)(dimensions.Width / width)}");
                //Debug.WriteLine($"Paddocks per column (Y-axis): {(int)(dimensions.Length / length)}");
                //Debug.WriteLine($"Total paddocks: {result.PaddockConfiguration.TotalFittingPaddocks}");
            }

            return result;
        }

        public IEnumerable<PaddockDetail> GetPaddockDetails(CalculationResult result)
        {
            var details = new List<PaddockDetail>();

            for (int i = 0; i < result.PaddockConfiguration.TotalFittingPaddocks; i++)
            {
                int row = i / result.PaddockConfiguration.PaddocksPerRow;
                int column = i % result.PaddockConfiguration.PaddocksPerRow;

                double x1 = column * result.PaddockConfiguration.PaddockLength;
                double y1 = row * result.PaddockConfiguration.PaddockWidth;
                double x2 = x1 + result.PaddockConfiguration.PaddockLength;
                double y2 = y1 + result.PaddockConfiguration.PaddockWidth;

                details.Add(new PaddockDetail
                {
                    Name = result.PaddockConfiguration.PaddockNames[i],
                    Length = result.PaddockConfiguration.PaddockLength,
                    Width = result.PaddockConfiguration.PaddockWidth,
                    Area = result.PaddockConfiguration.PaddockArea,
                    Coordinates = new[] { x1, y1, x2, y2 }
                });
            }

            return details;
        }

        public IEnumerable<PaddockVisual> GetPaddockVisuals(CalculationResult result)
        {
            var colors = new[] { Brushes.Green, Brushes.Blue, Brushes.Purple, Brushes.Teal };

            // Paddock dimensions
            double paddockWidth = result.PaddockConfiguration.PaddockLength; // Each paddock's width
            double paddockHeight = result.PaddockConfiguration.PaddockWidth; // Height remains the same for all

            for (int i = 0; i < result.PaddockConfiguration.TotalFittingPaddocks; i++)
            {
                yield return new PaddockVisual
                {
                    Name = result.PaddockConfiguration.PaddockNames[i],
                    X = i * paddockWidth,  // Spread paddocks horizontally
                    Y = 0,  // Keep everything in a single row
                    Width = paddockWidth,
                    Height = paddockHeight,
                    BackgroundColor = colors[i % colors.Length]
                };
            }
        }

        public void ExportToCsv(CalculationResult result, string filePath)
        {
            // Implementation for exporting data to CSV
            var csvContent = new StringBuilder();

            // Add headers
            csvContent.AppendLine("Paddock,Length,Width,Area,TopLeftX,TopLeftY,BottomRightX,BottomRightY");

            // Add data
            foreach (var detail in GetPaddockDetails(result))
            {
                csvContent.AppendLine(
                    $"{detail.Name}," +
                    $"{detail.Length}," +
                    $"{detail.Width}," +
                    $"{detail.Area}," +
                    $"{detail.Coordinates[0]}," +
                    $"{detail.Coordinates[1]}," +
                    $"{detail.Coordinates[2]}," +
                    $"{detail.Coordinates[3]}");
            }

            File.WriteAllText(filePath, csvContent.ToString());
        }

        private (bool isValid, double length, double width, string layoutOption) FindOptimalPaddockDimensions(
            int cowsPerPaddock, int farmLength, int farmWidth)
        {
            double cowLengthA = 1.5, cowWidthA = 2.0;
            double cowLengthB = 2.0, cowWidthB = 1.5;

            double minArea = double.MaxValue;
            double optimalLength = 0, optimalWidth = 0;
            string bestOption = "";
            bool foundValid = false;

            for (int rows = 1; rows <= Math.Sqrt(cowsPerPaddock); rows++)
            {
                if (cowsPerPaddock % rows != 0)
                    continue;

                int cols = cowsPerPaddock / rows;

                // Option A paddock dimensions
                double lengthA = rows * cowLengthA;
                double widthA = cols * cowWidthA;

                if (lengthA <= farmLength && widthA <= farmWidth)
                {
                    double areaA = lengthA * widthA;
                    if (areaA < minArea)
                    {
                        minArea = areaA;
                        optimalLength = lengthA;
                        optimalWidth = widthA;
                        bestOption = "Option A";
                        foundValid = true;
                    }
                }

                // Option B paddock dimensions
                double lengthB = rows * cowLengthB;
                double widthB = cols * cowWidthB;

                if (lengthB <= farmLength && widthB <= farmWidth)
                {
                    double areaB = lengthB * widthB;
                    if (areaB < minArea)
                    {
                        minArea = areaB;
                        optimalLength = lengthB;
                        optimalWidth = widthB;
                        bestOption = "Option B";
                        foundValid = true;
                    }
                }
            }

            return (foundValid, optimalLength, optimalWidth, bestOption);
        }

        private string[] GeneratePaddockNames(int count)
        {
            string[] alphabets = new string[count];
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                alphabets[i] = ConvertToAlphabet(index);
                index++;
            }
            return alphabets;
        }

        static string ConvertToAlphabet(int index)
        {
            string alphabet = "";
            while (index >= 0)
            {
                alphabet = (char)(index % 26 + 'A') + alphabet;
                index = index / 26 - 1;
            }
            return alphabet;
        }

        private Dictionary<string, double[]> CalculatePaddockCoordinates(CalculationResult result)
        {
            var coordinates = new Dictionary<string, double[]>();
            double pl = result.PaddockConfiguration.PaddockLength;
            double pw = result.PaddockConfiguration.PaddockWidth;
            int perRow = result.PaddockConfiguration.PaddocksPerRow;

            for (int i = 0; i < result.PaddockConfiguration.TotalFittingPaddocks; i++)
            {
                int row = i / perRow;
                int col = i % perRow;

                double x = col * pl;
                double y = row * pw;

                // Stores all four corners in order:
                // [0,1] = Top-Left, [2,3] = Top-Right
                // [4,5] = Bottom-Right, [6,7] = Bottom-Left
                coordinates[result.PaddockConfiguration.PaddockNames[i]] = new double[]
                {
            x, y,            // Top-Left
            x + pl, y,       // Top-Right
            x + pl, y + pw,  // Bottom-Right
            x, y + pw        // Bottom-Left
                };
            }

            return coordinates;
        }
    }
}
