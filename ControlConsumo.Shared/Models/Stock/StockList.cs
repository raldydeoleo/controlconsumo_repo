using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Stock
{
    public class StockList : NextConfig
    {     
        public DateTime? End { get; set; }
        public Byte TurnID { get; set; }       
        public Boolean IsNotify { get; set; }
    }
}
