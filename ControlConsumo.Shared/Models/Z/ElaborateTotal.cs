using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Z
{
    public class ElaborateTotal
    {
        public Int32 SecuenciaEmpaque { get; set; }
        public Int32 Quantity { get; set; }
        public Single Amounts { get; set; }
        public String Unit { get; set; }
        public String Lot { get; set; }
        public String MaterialCode { get; set; }
        public String Reference { get; set; }
        public DateTime? Expire { get; set; }
    }
}
