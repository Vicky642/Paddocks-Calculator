using Mypaddocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mypaddocks.Interfaces
{
    public interface ICalculationRepository
    {
        CalculationResult CalculatePaddockLayout(FarmDimensions dimensions, PaddockConfiguration config);
        IEnumerable<PaddockDetail> GetPaddockDetails(CalculationResult result);
        IEnumerable<PaddockVisual> GetPaddockVisuals(CalculationResult result);
        void ExportToCsv(CalculationResult result, string filePath);
    }
}
