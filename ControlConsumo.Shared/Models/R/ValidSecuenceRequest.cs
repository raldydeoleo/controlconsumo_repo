using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.R
{
    class ValidSecuenceRequest
    {
        public String WERKS { get; set; }
        public String MATNR { get; set; }
        public String CHARG { get; set; }
        public String LICHA { get; set; }
        public Int16 BOXNO { get; set; }
        public Int16 EMPAQUENO { get; set; }
    }
}
