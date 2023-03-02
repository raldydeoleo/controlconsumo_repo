using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Elaborate
{
    public class ElaboratesReport
    {
        public DateTime Produccion { get; set; }
        public Byte TurnID { get; set; }
        public String EquipmentID { get; set; }
        public String ProductCode { get; set; }
        public String VerID { get; set; }
        public String BatchID { get; set; }
        public String TrayID { get; set; }
        public Single Quantity { get; set; }
        public String Unit { get; set; }
        public Single Peso { get; set; }
        public ProductsRoutes.RoutesStatus Status { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? Fecha2 { get; set; }
        public String EquipmentID2 { get; set; }
        public String Empaque { get; set; }
        public Int16 SecuenciaEmpaque { get; set; }
        public String TrayID2 { get; set; }
    }
}
