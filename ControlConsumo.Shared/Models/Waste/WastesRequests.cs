using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Waste
{
    class WastesRequests
    {
        public Byte ID { get; set; }
        public String IDPROCESS { get; set; }
        public String WERKS { get; set; }
        public String IDEQUIPO { get; set; }
        public String IDTIEMPO { get; set; }
        public String FECHA { get; set; }
        public String HORA { get; set; }
        public String MATNR { get; set; }
        public String VERID { get; set; }
        public Byte IDTURNO { get; set; }
        public String MATNR2 { get; set; }
        public String IDEQUIPO2 { get; set; }
        public String CHARG1 { get; set; }
        public String CHARG { get; set; }
        public String VFDAT { get; set; }
        public Single MENGE { get; set; }
        public String MEINS { get; set; }
        public Int16 BOXNO { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String USNAM { get; set; }
    }
}
