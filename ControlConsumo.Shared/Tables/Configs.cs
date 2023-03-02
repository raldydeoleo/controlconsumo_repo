using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Configs")]
    public class Configs
    {
        public enum _Status
        {
            Inactived = 0,
            Actived = 1,
            Enabled = 2,
            Completed = 3
        }

        [PrimaryKey, NotNull, AutoIncrement]
        public Int32 ID { get; set; }

        [NotNull, MaxLength(10), Indexed]
        public String EquipmentID { get; set; }

        [NotNull, Default(true, true)]
        public _Status Status { get; set; }

        [NotNull, MaxLength(18)]
        public String ProductCode { get; set; }

        [MaxLength(2)]
        public String TimeID { get; set; }

        [NotNull, MaxLength(4)]
        public String VerID { get; set; }

        [NotNull]
        public DateTime Begin { get; set; }

        [MaxLength(10)]
        public String SubEquipmentID { get; set; }

        [NotNull, Default(true, false), Indexed]
        public Boolean Sync { get; set; }

        [NotNull, MaxLength(12)]
        public String Logon { get; set; }

        [MaxLength(12)]
        public String Logon2 { get; set; }

        [NotNull]
        public DateTime CreateDate { get; set; }

        [NotNull]
        public DateTime ModifyDate { get; set; }

        [NotNull]
        public DateTime ExecuteDate { get; set; }

        [NotNull, Default(true, false)]
        public Boolean IsCold { get; set; }

        //Atributo que indica el tipo de producto (Filler). Ej: Prime, Modify, Grand Father
        [MaxLength(12)]
        public String ProductType { get; set; }

        //Atributo que indica el código de producción de acuerdo al tipo de producto y tipo de almacenamiento escogido.
        [MaxLength(12)]
        public String Identifier { get; set; }
    }
}
