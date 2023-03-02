using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Permit
{
    class PermitsResult
    {
        public string mandt { get; set; }
        public int znopermiso { get; set; }
        public string permiso { get; set; }
        public string cpudt { get; set; }
        public string cputm { get; set; }
        public string usnam { get; set; }
    }
}
