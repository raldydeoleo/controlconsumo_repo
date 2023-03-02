using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.R
{
    class TiquetsRequest
    {
        public String IDBANDEJA { get; set; }
        public Int16 SECSALIDA { get; set; }
        public String IDEQUIPO { get; set; }
        public String IDTIEMPO { get; set; }
        public String BATCHID { get; set; }
        public String FECHA { get; set; }
        public String HORA { get; set; }
    }
}
