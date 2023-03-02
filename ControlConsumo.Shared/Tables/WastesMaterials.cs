using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("WastesMaterials")]
    public class WastesMaterials
    {   
        [NotNull, MaxLength(18), Indexed]
        public String MaterialCode { get; set; }

        [NotNull, MaxLength(40)]
        public String Name { get; set; }

        [NotNull, MaxLength(3)]
        public String Unit { get; set; }
    }
}
