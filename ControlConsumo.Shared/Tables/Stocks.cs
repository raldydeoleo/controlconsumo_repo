using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Stocks")]
    public class Stocks
    {
        public enum _Status
        {
            Abierto,
            Cerrado
        }

        [NotNull, PrimaryKey, AutoIncrement]
        public Int32 ID { get; set; }

        [NotNull]
        public Int16 CustomID { get; set; }

        [NotNull]
        public Int32 CustomFecha { get; set; }

        [NotNull]
        public Byte TurnID { get; set; }

        [NotNull]
        public _Status Status { get; set; }

        [NotNull, MaxLength(4)]
        public String ProcessID { get; set; }

        [NotNull, MaxLength(4)]
        public String Center { get; set; }

        [NotNull, MaxLength(10)]
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

        [NotNull]
        public DateTime Begin { get; set; }

        [NotNull]
        public DateTime End { get; set; }

        [NotNull, Default(true, true), Indexed]
        public Boolean Sync { get; set; }

        [NotNull]
        public Boolean IsNotified { get; set; }

        [Ignore]
        public Boolean IsMemoryCreated { get; set; }

        [Ignore]
        public DateTime _Begin { get { return IsMemoryCreated ? Begin : Begin.ToLocalTime(); } }
    }
}
