using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Elaborate
{
    class ElaboratesRequest
    {
        public String MANDT { get; set; }
        public String IDPROCESS { get; set; }
        public String IDEQUIPO { get; set; }
        public String WERKS { get; set; }
        public String IDTIEMPO { get; set; }
        public String IDBANDEJA { get; set; }
        public Int16 SECSALIDA { get; set; }
        public String MATNR { get; set; }
        public String VERID { get; set; }
        public String FECHA { get; set; }
        public String HORA { get; set; }
        public Byte IDTURNO { get; set; }
        public String USNAM { get; set; }
        public Single MENGE { get; set; }
        public Single MENGE2 { get; set; }
        public String MEINS { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String CPUDT2 { get; set; }
        public String CPUTM2 { get; set; }
        public String RETURNED { get; set; }
        public String CHARG { get; set; }
        public String BATCHID { get; set; }
        public String IDEQUIPO2 { get; set; }
        public String VFDAT { get; set; }
        public String IDEMPAQUE { get; set; }
        //Campo de SAP que guarda el identificador. Ej: si es X, S, P, H
        public String COLD { get; set; }
        public Int16 SECEMPAQUE { get; set; }
    }
}
