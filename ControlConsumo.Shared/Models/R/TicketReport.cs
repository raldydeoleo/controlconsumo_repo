using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Models.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.R
{
    public class TicketReport : NextConfig
    {
        public String Status { get; set; }
        public String VerID { get; set; }
        public String Lot { get; set; }
        public Int16 SecSalida { get; set; }
        public DateTime? Produccion { get; set; }
        public DateTime? Fecha { get; set; }  
        public Byte TurnID { get; set; }
        public String TrayID { get; set; }
        public String BatchID { get; set; }       
        public Single Quantity { get; set; }
        public String Unit { get; set; }
        public Single Quantity2 { get; set; }
        public String Traza { get; set; }

        public IEnumerable<Detail> Details { get; set; }

        public class Detail : MaterialList
        {
            public String EquipmentID { get; set; }
            public String TimeID { get; set; }
            public String Lot { get; set; }
            public Int16 BoxNo { get; set; }
            public DateTime? Produccion { get; set; }
            public DateTime? Fecha { get; set; }
            public String TrayID { get; set; }
            public String EquipmentID3 { get; set; }
            public Int16 SecSalida { get; set; }
            public DateTime? Fecha2 { get; set; }           
            public String BatchID { get; set; }
            public Byte TurnID { get; set; }
            public String LotReference { get; set; }

            public Boolean _IsSemiElaborate { get { return String.IsNullOrEmpty(Lot); } }
        }
    }
}
