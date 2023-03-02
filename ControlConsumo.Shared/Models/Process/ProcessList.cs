using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Process
{
    public class ProcessList
    {
        public ProcessList()
        {
            NeedGramo = false;
            ConfigActive = false;
            IsLast = false;
            NeedEan = false;
            IsSubEquipment = false;
            IsPartialElaborateAuthorized = false;
        }

        public String Logon { get; set; }
        public String UserName { get; set; }
        public String Process { get; set; }
        public String ProcessName { get; set; }
        public String Centro { get; set; }
        public String CentroName { get; set; }
        public String EquipmentID { get; set; }
        public String SubEquipmentID { get; set; }
        public String Equipment { get; set; }
        public Boolean NeedGramo { get; set; }
        public Boolean ConfigActive { get; set; }
        public Boolean IsLast { get; set; }
        public Boolean NeedEan { get; set; }
        public Boolean IsSubEquipment { get; set; }
        public Boolean _IsDoubleEquipment { get { return !String.IsNullOrEmpty(SubEquipmentID); } }
        public Boolean IsInputOutputControlActive { get; set; }
        public Boolean IsPartialElaborateAuthorized { get; set; }
    }
}
