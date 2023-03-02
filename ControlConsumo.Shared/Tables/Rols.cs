using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Rols")]
    public class Rols
    {
        [NotNull, PrimaryKey]
        public Int16 ID { get; set; }

        [NotNull, MaxLength(50)]
        public String Rol { get; set; }
    }
}
