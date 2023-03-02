using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Units")]
    public class Units
    {
        [NotNull, PrimaryKey, MaxLength(50)]
        public String Key { get; set; }

        [NotNull, Indexed, MaxLength(18)]
        public String MaterialCode { get; set; }

        [NotNull, MaxLength(3)]
        public String Unit { get; set; }

        [NotNull, Default(true, false)]
        public Boolean IsBase { get; set; }

        [NotNull, Default(true, 0)]
        public Single From { get; set; }

        [NotNull, Default(true, 0)]
        public Single To { get; set; }

        [MaxLength(18)]
        public String Ean { get; set; }

        [Ignore]
        public Single Factor { get { return To / From; } }
    }
}
