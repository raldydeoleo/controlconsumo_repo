using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("EquipmentTypes")]
    public class EquipmentTypes
    {
        [NotNull, PrimaryKey]
        public Byte ID { get; set; }

        [NotNull, MaxLength(30)]
        public String Name { get; set; }

        [NotNull, Default(true, false)]
        public Boolean NeedWeight { get; set; }   

        [NotNull, Default(true, false)]
        public Boolean IsFinal { get; set; }

        [NotNull, Default(true, false)]
        public Boolean NeedEan { get; set; }
    }
}
