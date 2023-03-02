using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("AreasEquipments")]
    public class AreasEquipments
    {
        [NotNull, PrimaryKey, AutoIncrement]
        public Int16 ID { get; set; }

        [NotNull]
        public Byte AreaID { get; set; }

        [NotNull, MaxLength(10)]
        public String EquipmentID { get; set; }

        [NotNull, Default(true, false)]
        public Boolean Sync { get; set; }

        [NotNull, Default(true, false)]
        public Boolean IsDeleted { get; set; }
    }
}
