using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Equipment
{
    public class EquipmentUpdate
    {
        public String EquipmentID { get; set; }
        public Boolean IsActive { get; set; }
        public Byte AreaID { get; set; }        
        public Boolean UpdateStatus { get; set; }
        public Boolean UpdateArea { get; set; }
    }
}
