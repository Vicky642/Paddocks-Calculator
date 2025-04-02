using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mypaddocks.Models
{
   public class CalculationResult
    {
        public FarmDimensions FarmDimensions { get; set; }
        public PaddockConfiguration PaddockConfiguration { get; set; }
        public Dictionary<string, double[]> PaddockCoordinates { get; set; }
    }
}
