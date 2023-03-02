using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Elaborate
{
    class ElaborateReportResult
    {
        public String BATCHID { get; set; }
        public String FECHA { get; set; }
        public String HORA { get; set; }
        public String IDEQUIPO { get; set; }
        public String MATNR { get; set; }
        public String VERID { get; set; }
        public String IDBANDEJA { get; set; }
        public Byte IDTURNO { get; set; }
        public Single MENGE { get; set; }
        public String MEINS { get; set; }
        public Single MENGE2 { get; set; }
        public Byte STATUSBAN { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String IDEQUIPO2 { get; set; }
        public String IDBANDEJA2 { get; set; }
        public String CPUDT2 { get; set; }
        public String CPUTM2 { get; set; }
        public String IDEMPAQUE { get; set; }
        public Int16 SECEMPAQUE { get; set; }
    }
}
