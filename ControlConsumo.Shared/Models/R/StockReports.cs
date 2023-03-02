using ControlConsumo.Shared.Models.ConfigMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.R
{
    public class StockReports : ConfigMaterialList
    {
        public String Lot { get; set; }
        public String Logon { get; set; }
        public DateTime Fecha { get; set; }
        public Single TotalQuantity { get; set; }
        public Single Total { get; set; }
        public Single Quantity { get; set; }
        public Byte TurnID { get; set; }
        public String Reason { get; set; }
        public String LoteSuplidor { get; set; }
        public Int32 BoxNumber { get; set; }
    }
}
