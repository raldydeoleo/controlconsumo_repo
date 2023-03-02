using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Equipment
{
    public class EquipmentList
    {
        public Int32 ConfigID { get; set; }
        public String EquipmentID { get; set; }
        public String Equipment { get; set; }
        public Boolean Status { get; set; }
        public Boolean IsActive { get; set; }
        public String Type { get; set; }
        public String TimeID { get; set; }
        public Int32 AreaID { get; set; }
        public Int32 EquipmentTypeID { get; set; }
        public String Area { get; set; }
        public String SubEquipmentID { get; set; }
        public String SubEquipment { get; set; }
        public String Material { get; set; }
        public String Code { get; set; }      
        public String Short { get; set; }
        public String Version { get; set; }
        public String Logon { get; set; }
        public DateTime? Create { get; set; }
        public DateTime? Begin { get; set; }
        public Boolean IsEdit { get; set; }       
    }
}
