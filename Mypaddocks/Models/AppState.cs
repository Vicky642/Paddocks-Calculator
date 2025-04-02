using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mypaddocks.Models
{
    public class AppState
    {
        // Store the calculated farm area
        public static int FarmArea { get; set; }
        public static int FarmLength { get; set; }
        public static int FarmWidth { get; set; }
        public static int CowsPerPaddock { get; set; }

    }
}
