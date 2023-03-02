using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Stock
{
    class StockRequest
    {
        public Int16 IDCORTE { get; set; }
        public String IDPROCESS { get; set; }
        public String WERKS { get; set; }
        public String IDEQUIPO { get; set; }
        public String FECHA { get; set; }
        public String HORAMIN { get; set; }
        public String HORAMAX { get; set; }
        public Byte IDTURNO { get; set; }
        public String STTURNO { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String USNAM { get; set; }
        public String NOTIFICADO { get; set; }
        public IEnumerable<Detail> DETALLE { get; set; }

        public class Detail
        {
            public Int16 ID { get; set; }
            public String IDPROCESS { get; set; }
            public String WERKS { get; set; }
            public String IDEQUIPO { get; set; }
            public String IDTIEMPO { get; set; }
            public String FECHA { get; set; }
            public String MATNR { get; set; }
            public String VERID { get; set; }
            public String HORA { get; set; }
            public Byte IDTURNO { get; set; }
            public String MATNR2 { get; set; }
            public String IDEQUIPO2 { get; set; }
            public String CHARG { get; set; }
            public Single MENGE { get; set; }
            public String MEINS { get; set; }
            public Single MENGE2 { get; set; }
            public Int16 BOXNO { get; set; }
            public String CPUDT { get; set; }
            public String CPUTM { get; set; }
            public String USNAM { get; set; }
        }
    }
}
