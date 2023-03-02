using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Z
{
    public class PostSecuenceRequest
    {
        public String WERKS { get; set; }
        public String MATNR { get; set; }
        public String CHARG { get; set; }
        public String LICHA { get; set; }
        public Int32 EMPAQUENO { get; set; }
        public Int32 REIMPRESIONNO { get; set; }
        public String ESTADO { get; set; }
        public String CREADOR { get; set; }
    }
}
