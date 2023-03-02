using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.System
{
    class TableInfoResult
    {
        public Int32 cid { get; set; }
        public String name { get; set; }
        public String type { get; set; }
        public String notnull { get; set; }
        public String pk { get; set; }
    }
}
