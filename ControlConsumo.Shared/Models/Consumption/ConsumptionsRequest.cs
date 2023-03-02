using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Consumption
{
    public class ConsumptionsRequest
    {
        public String IDPROCESS { get; set; }
        public String WERKS { get; set; }
        public String IDEQUIPO { get; set; }
        public String IDTIEMPO { get; set; }
        public String MATNR { get; set; }
        public String VERID { get; set; }
        public Int16 SECENTRADA { get; set; }
        public String FECHA { get; set; }
        public String HORA { get; set; }
        public Byte IDTURNO { get; set; }
        public String MATNR2 { get; set; }
        public String IDEQUIPO2 { get; set; }
        public String CHARG { get; set; }
        public Single MENGE { get; set; }
        public String MEINS { get; set; }
        public Int16 BOXNO { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String CPUDT2 { get; set; }
        public String CPUTM2 { get; set; }
        public String USNAM { get; set; }
        public String IDBANDEJA { get; set; }
        public String IDEQUIPO3 { get; set; }
        public Int16 SECSALIDA { get; set; }
        public String CPUDT3 { get; set; }
        public String CPUTM3 { get; set; }
        public String BATCHID { get; set; }
    }
}
