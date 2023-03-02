using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("ConfigMaterials")]
    public class ConfigMaterials
    {
        [NotNull, Indexed, MaxLength(18)]
        public String ProductCode { get; set; }

        [NotNull, MaxLength(4)]
        public String VerID { get; set; }

        [NotNull, MaxLength(18)]
        public String MaterialCode { get; set; }

        [NotNull]
        public Single Quantity { get; set; }

        [NotNull, MaxLength(3)]
        public String Unit { get; set; }       
    }
}
