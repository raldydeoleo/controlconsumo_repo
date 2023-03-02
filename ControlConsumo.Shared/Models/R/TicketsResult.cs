using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.R
{
    class TicketsResult
    {
        public Header HEAD { get; set; }
        public IEnumerable<Detail> DETAILS { get; set; }

        public class Header
        {
            public String IDEQUIPO { get; set; }
            public String MATNR { get; set; }
            public String VERID { get; set; }
            public String CHARG { get; set; }
            public Int16 SECSALIDA { get; set; }
            public String FECHA { get; set; }
            public String HORA { get; set; }
            public Byte IDTURNO { get; set; }
            public String IDBANDEJA { get; set; }
            public String BATCHID { get; set; }
            public String STATUS { get; set; }
            public String MENGE { get; set; }
            public String MEINS { get; set; }
            public String MENGE2 { get; set; }
            public String LOTEMAN { get; set; }
            public String CPUDT { get; set; }
            public String CPUTM { get; set; }
        }

        public class Detail
        {
            public String IDEQUIPO { get; set; }
            public String IDTIEMPO { get; set; }
            public String MATNR2 { get; set; }
            public String CHARG { get; set; }
            public String MEINS { get; set; }
            public String BOXNO { get; set; }
            public String FECHA { get; set; }
            public String HORA { get; set; }
            public String CPUDT { get; set; }
            public String CPUTM { get; set; }
            public String IDBANDEJA { get; set; }
            public String IDEQUIPO3 { get; set; }
            public Int16 SECSALIDA { get; set; }
            public String CPUDT3 { get; set; }
            public String BATCHID { get; set; }
            public Byte IDTURNO { get; set; }
            public String NORMT { get; set; }
            public String BISMT { get; set; }
            public String MAKTG { get; set; }
            public String LICHA { get; set; }
        }
    }
}
