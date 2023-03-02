using ControlConsumo.Shared.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Elaborate
{
    public class ElaborateList : NextConfig
    {
        public Boolean IsActive { get; set; }
        public Int32 ID { get; set; }
        public String TrayID { get; set; }
        public String Logon { get; set; }
        public DateTime Produccion { get; set; }
        public DateTime Fecha { get; set; }
        public Single Quantity { get; set; }
        public Byte TurnID { get; set; }
        public String Unit { get; set; }
        public Int16 ElaborateID { get; set; }
        public String BatchID { get; set; }        
    }
}
