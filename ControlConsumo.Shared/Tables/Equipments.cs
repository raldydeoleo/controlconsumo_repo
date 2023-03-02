using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Equipments")]
    public class Equipments
    {
        [PrimaryKey, NotNull, MaxLength(10)]
        public String ID { get; set; }

        [NotNull, MaxLength(30)]
        public String Name { get; set; }

        [NotNull]
        public Byte EquipmentTypeID { get; set; }

        [MaxLength(2), NotNull]
        public String TimeID { get; set; }

        [NotNull]
        public Boolean IsSubEquipment { get; set; }

        [NotNull, MaxLength(20)]
        public String Serial { get; set; }

        [NotNull]
        public Boolean IsActive { get; set; }

        [NotNull, Default(true, false)]
        public Boolean Sync { get; set; }

        public String _Display { get { return String.Format("{0} - {1}", ID, Name); } }
    }
}
