using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Lot
{
    public class LotsList
    {
        public String MaterialCode { get; set; }
        public String Code { get; set; }
        public String Reference { get; set; }
        public DateTime Expire { get; set; }

        public DateTime? _Expire
        {
            get
            {
                if (Expire.Year > 1)
                    return Expire;
                else
                    return null;
            }
        }
    }
}
