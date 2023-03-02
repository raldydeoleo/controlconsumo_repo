using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Groups")]
    public class Groups
    {
        [NotNull, PrimaryKey, AutoIncrement]
        public Int32 ID { get; set; }

        [NotNull, Indexed]
        public Int32 ElaborateID { get; set; }

        [NotNull, MaxLength(4)]
        public String ProcessID { get; set; }

        [NotNull, MaxLength(10)]
        public String Equipment { get; set; }              

        [NotNull]
        public DateTime Fecha { get; set; }

        [NotNull, Default(true, 0)]
        public Int32 GroupID { get; set; }

        [NotNull, Default(true, 0)]
        public Int32 CustomID { get; set; }
    }
}
