using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Z
{
    public class DateTurnList
    {
        public DateTime Produccion { get; set; }
        public Byte TurnID { get; set; }
        public Boolean IsNotified { get; set; }
    }
}
