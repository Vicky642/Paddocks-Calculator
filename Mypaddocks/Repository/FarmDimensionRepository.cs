using Mypaddocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mypaddocks.Repository
{
    public class FarmDimensionRepository
    {
        private FarmDimensions _farmDimensions = new FarmDimensions();

        // Store the dimensions globally
        public FarmDimensions GetFarmDimensions()
        {
            return _farmDimensions;
        }

        // Set the dimensions
        public void SetFarmDimensions(int length, int width)
        {
            _farmDimensions.Length = length;
            _farmDimensions.Width = width;
        }

        // Calculate the area (if both dimensions are valid)
        public bool CalculateArea()
        {
            if (_farmDimensions.Length >= 210 && _farmDimensions.Width >= 310)
            {
                return true;
            }
            return false;
        }

        // Validation logic for length and width
        public bool ValidateLength() => _farmDimensions.Length >= 210;
        public bool ValidateWidth() => _farmDimensions.Width >= 310;
    }
}
