using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("TraysTimes")]
    public class TraysTimes
    {
        [NotNull, PrimaryKey]
        public Int32 ID { get; set; }

        [NotNull, MaxLength(10)]
        public String IdProceso { get; set; }

        [NotNull, MaxLength(10), Indexed]
        public String TrayID { get; set; }

        [NotNull, MaxLength(2)]
        public String TimeID { get; set; }

        [NotNull]
        public Single Quantity { get; set; }

        [NotNull, MaxLength(3)]
        public String Unit { get; set; }
    }
}
