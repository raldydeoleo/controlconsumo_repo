using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Z
{
    public class TraysList
    {
        public String TrayID { get; set; }
        public Int16 Secuencia { get; set; }
        public TraysProducts._Status Status { get; set; }
        public String ProductCode { get; set; }
        public String VerID { get; set; }
        public Single Quantity { get; set; }
        public String Unit { get; set; }
        public String TimeID { get; set; }
        public String BarCode { get; set; }
        public String EquipmentID { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime ModifyDate { get; set; }
        public Int16 ElaborateID { get; set; }
        public String BatchID { get; set; }
        public String _ProductCode { get { return ExtensionsMethodsHelper.GetSapCode(ProductCode); } }
        public String _TrayID { get { return String.Format("{0}{1}", TrayID, Secuencia.ToString("00000")); } }
        public Int32 _Fecha { get { return Convert.ToInt32(Fecha.Value.GetSapDateL()); } }
    }
}
