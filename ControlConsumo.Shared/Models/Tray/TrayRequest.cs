using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Tray
{
    class TrayRequest
    {
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String USNAM { get; set; }
        public IEnumerable<Position> POSICIONES { get; set; }

        public class Position
        {
            public String IDBANDEJA { get; set; }     
        }
    }
}
