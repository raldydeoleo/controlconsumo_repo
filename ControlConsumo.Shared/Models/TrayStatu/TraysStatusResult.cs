using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.TrayStatu
{
    class TraysStatusResult
    {
        public string mandt { get; set; }
        public string idbandeja { get; set; }
        public int secuencia { get; set; }
        public string status { get; set; }
        public int cpudt { get; set; }
        public String cpudm { get; set; }
    }
}
