using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Inventory
{
    class InventoriesRequest
    {
        public String IDPROCESS { get; set; }
        public String WERKS { get; set; }
        public String IDEQUIPO { get; set; }
        public String MATNR { get; set; }
        public String CHARG { get; set; }
        public Int16 BOXNO { get; set; }
        public Single MENGE { get; set; }
        public String MEINS { get; set; }
    }
}
