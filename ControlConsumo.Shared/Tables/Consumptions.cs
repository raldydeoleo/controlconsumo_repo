using ControlConsumo.Shared.Models.Z;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Consumptions")]
    public class Consumptions
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int ID { get; set; }

        [NotNull, MaxLength(4)]
        public String ProcessID { get; set; }

        [NotNull, MaxLength(4)]
        public String Center { get; set; }

        [NotNull]
        public Int32 CustomFecha { get; set; }

        [NotNull]
        public DateTime Produccion { get; set; }

        [NotNull]
        public DateTime Fecha { get; set; }

        [NotNull, Default(true, 0)]
        public Int16 CustomID { get; set; }

        [NotNull, MaxLength(10)]
        public String EquipmentID { get; set; }

        [NotNull, MaxLength(2)]
        public String TimeID { get; set; }

        [NotNull, MaxLength(18)]
        public String ProductCode { get; set; }

        [NotNull, MaxLength(4)]
        public String VerID { get; set; }

        [NotNull, MaxLength(18)]
        public String MaterialCode { get; set; }

        [NotNull, MaxLength(12)]
        public String Logon { get; set; }

        [NotNull, Default(true, true), Indexed]
        public Boolean Sync { get; set; }

        [NotNull, Default(true, true), Indexed]
        public Boolean SyncSQL { get; set; }

        [NotNull, Default(true, 0)]
        public Byte TurnID { get; set; }

        [MaxLength(10)]
        public String TrayID { get; set; }

        [MaxLength(10)]
        public String Lot { get; set; }

        [NotNull, Default(true, 0)]
        public Int16 BoxNumber { get; set; }

        [NotNull, Default(true, 0)]
        public Single Quantity { get; set; }

        [MaxLength(10)]
        public String SubEquipmentID { get; set; }

        [MaxLength(3)]
        public String Unit { get; set; }

        [MaxLength(10)]
        public String TrayEquipmentID { get; set; }

        public Int16 ElaborateID { get; set; }

        public DateTime? TrayDate { get; set; }

        [MaxLength(18)]
        public String BatchID { get; set; }

        [Ignore]
        public TraysProducts Bandeja { get; set; }

        [Ignore]
        public Boolean IsMemoryCreated { get; set; }

        [Ignore]
        public DateTime _Produccion { get { return IsMemoryCreated ? Produccion.ToUniversalTime() : Produccion; } }

        [Ignore]
        public DateTime _Fecha { get { return IsMemoryCreated ? Fecha.ToUniversalTime() : Fecha; } }

        [Ignore]
        public Boolean IsLotInternal { get; set; }
    }
}
