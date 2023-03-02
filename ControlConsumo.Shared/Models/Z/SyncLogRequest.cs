using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Z
{
    public class SyncLogRequest
    {
        public enum SType
        {
            T,
            S,
            D
        }

        public String MANDT { get; set; }
        public String WERKS { get; set; }
        public String IDPROCESS { get; set; }   
        public String IDEQUIPO { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String SYNC_TYPE { get { return STYPE.ToString(); } }
        public Int32 PASO { get; set; }
        public Int32 PASOMAX { get; set; }
        public String MESSAGE { get; set; }
        public SType STYPE { get; set; }
        public String ERROR { get; set; }
    }
}
