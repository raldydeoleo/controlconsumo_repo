using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Stock
{
    class StockResult
    {
        public Byte IDCORTE { get; set; }
        public String IDPROCESS { get; set; }
        public String WERKS { get; set; }
        public String IDEQUIPO { get; set; }
        public String FECHA { get; set; }
        public String HORA_MIN { get; set; }
        public String HORA_MAX { get; set; }
        public Byte IDTURNO { get; set; }
        public String STTURNO { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String USNAM { get; set; }
        public String NOTIFICADO { get; set; }
    }
}
