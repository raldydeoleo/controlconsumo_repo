using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Elaborate
{
    public class ElaborateReportRequest
    {
        public ElaborateReportRequest()
        {
            IDTURNS = new List<byte>();
        }

        public String IDEQUIPO { get; set; }
        public String MATNR { get; set; }
        public String BATCHID { get; set; }
        public String FECHAFROM { get; set; }
        public String FECHATO { get; set; }
        public String HORAFROM { get; set; }
        public String HORATO { get; set; }
        public List<Byte> IDTURNS { get; set; }
    }
}
