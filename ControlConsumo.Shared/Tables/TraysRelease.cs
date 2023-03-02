using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("TraysRelease")]
    public class TraysRelease
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int ID { get; set; }

        [NotNull]
        public DateTime Fecha { get; set; }

        [NotNull, MaxLength(12)]
        public String Logon { get; set; }

        [NotNull, Default(true, false)]
        public Boolean Sync { get; set; }

        [Ignore]
        public IEnumerable<TraysReleasePosition> Positions { get; set; }
    }
}
