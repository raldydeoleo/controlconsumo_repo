using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Transaction
{
    class TransactionsRequest
    {
        public String MANDT { get; set; }
        public String IDPROCESS { get; set; }
        public String WERKS { get; set; }
        public String IDEQUIPO { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String MATNR { get; set; }
        public Byte IDTURNO { get; set; }
        public String CHARG { get; set; }
        public Single MENGE { get; set; }
        public Single MENGE2 { get; set; }
        public String MEINS { get; set; }
        public String UNAME { get; set; }
        public String CONCEPTO { get; set; }
        public Int16 BOXNO { get; set; }
    }
}
