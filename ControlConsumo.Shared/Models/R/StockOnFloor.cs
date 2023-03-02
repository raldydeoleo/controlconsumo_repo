using ControlConsumo.Shared.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.R
{
    public class StockOnFloor : NextConfig
    {
        public StockOnFloor()
        {
            BoxesNo = new List<Int16>();
        }

        public String Lot { get; set; }
        public Int32 Logico { get; set; }
        public Int32 Fisico { get; set; }
        public Int32 Total { get { return Fisico - Logico; } }
        public Single Amount { get; set; }
        public Single AmountEscaneado { get; set; }
        public Single AjusteAmount { get { return Math.Abs(AmountEscaneado - Amount); } }
        public Single AmountAjustado { get { return AjusteAmount / Total; } }
        public List<Int16> BoxesNo { get; set; }
    }
}
