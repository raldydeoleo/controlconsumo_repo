using ControlConsumo.Shared.Models.ConfigMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.R
{
    public class MaterialReport : ConfigMaterialList
    {
        public Int32 ID { get; set; }
        public Int16 CustomID { get; set; }
        public Byte TurnID { get; set; }
        public DateTime Produccion { get; set; }
        public DateTime Expire { get; set; }
        public String Lot { get; set; }
        public String LoteSuplidor { get; set; }
        public String VerID { get; set; }
        public Single Quantity { get; set; }
        public Single EntryQuantity { get; set; }
        public Single Acumulated { get; set; }
        public String Logon { get; set; }
        public Int16 BoxNumber { get; set; }
        public String BatchID { get; set; }
        public Boolean NeedPercent { get; set; }
        public String TrayID { get; set; }
        public String MaterialCode2 { get; set; }
        public String TrayEquipmentID { get; set; }
        public Int16 ElaborateID { get; set; }
        public DateTime? TrayDate { get; set; }
    }
}
