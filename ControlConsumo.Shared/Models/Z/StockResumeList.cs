using ControlConsumo.Shared.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Z
{
    public class StockResumeList : NextConfig
    {
        public Byte TurnID { get; set; }
        public Int32 CustomFecha { get; set; }
        public String Lot { get; set; }
        public Single Consumido { get; set; }
        public Single Entregado { get; set; }
        public String Reference { get; set; }
        public Single Total { get { return Entregado - Consumido; } }
    }
}
