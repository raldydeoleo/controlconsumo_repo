using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Wastes")]
    public class Wastes
    {
        [NotNull, PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int StockID { get; set; }

        [NotNull]
        public Int32 CustomFecha { get; set; }

        [NotNull]
        public DateTime Fecha { get; set; }

        [NotNull]
        public Byte TurnID { get; set; }      

        [NotNull, MaxLength(4)]
        public String ProcessID { get; set; }

        [NotNull, MaxLength(4)]
        public String Center { get; set; }

        [MaxLength(10)]
        public String Equipment { get; set; }

        [MaxLength(10)]
        public String SubEquipment { get; set; }

        [MaxLength(2)]
        public String TimeID { get; set; }

        [MaxLength(18)]
        public String ProductCode { get; set; }

        [MaxLength(4)]
        public String VerID { get; set; }

        [NotNull, MaxLength(12)]
        public String Logon { get; set; }

        [NotNull, Default(true, true), Indexed]
        public Boolean Sync { get; set; }

        [NotNull, MaxLength(18)]
        public String MaterialCode { get; set; }

        [MaxLength(10)]
        public String Lot { get; set; }

        [MaxLength(10)]
        public String LoteSuplidor { get; set; }

        [NotNull]
        public DateTime Expire { get; set; }

        [NotNull, Default(true, 0)]
        public Int16 BoxNumber { get; set; }

        [NotNull, Default(true, 0)]
        public Single Quantity { get; set; }

        [MaxLength(3)]
        public String Unit { get; set; }

        [Ignore]
        public String MaterialCode2 { get; set; }
    }
}
