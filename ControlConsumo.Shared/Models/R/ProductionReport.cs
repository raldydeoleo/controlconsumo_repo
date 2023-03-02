using ControlConsumo.Shared.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.R
{
    public class ProductionReport : NextConfig
    {
        public Int32 ElaborateID { get; set; }
        public DateTime Fecha { get; set; }
        public String Unit { get; set; }
        public Int16 Quantity { get; set; }
        public Single Total { get; set; }
    }
}
