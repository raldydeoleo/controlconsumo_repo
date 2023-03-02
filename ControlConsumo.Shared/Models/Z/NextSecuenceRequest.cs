using ControlConsumo.Shared.Models.R;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Z
{
    class NextSecuenceRequest : ValidSecuenceRequest
    {
        public Int32 SECUENCE { get; set; }
    }
}
