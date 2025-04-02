using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mypaddocks.Repository
{
    public class PaddockConfigurationRepository
    {
        public int CalculateNumberOfPaddocks(int area, int cowsPerPaddock)
        {
            const int grazingAreaPerCow = 3; // Each cow requires 3 square meters of grazing area
            int paddockAreaRequired = grazingAreaPerCow * cowsPerPaddock;
            return area / paddockAreaRequired;
        }
    }
}
