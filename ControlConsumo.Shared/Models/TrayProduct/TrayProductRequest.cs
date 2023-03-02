using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.TrayProduct
{
    class TrayProductRequest
    {
        public String idbandeja { get; set; }
        public Int32 zsecuencia { get; set; }
        public String cpudt { get; set; }
        public String cputm { get; set; }
    }
}
