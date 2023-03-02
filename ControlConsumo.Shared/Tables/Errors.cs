using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Errors")]
    public class Errors
    {
        public enum Messages
        {
            WRONG_MATERIAL = 1,
            EXPIRED_MATERIAL = 2,
            WRONG_UPCCODE_MATERIALCONSUMPTION = 3,
            WRONG_UPCCODE_INVENTORYENTRY = 4
        }

        [PrimaryKey, AutoIncrement, NotNull]
        public int ID { get; set; }

        [NotNull, MaxLength(4)]
        public String ProcessID { get; set; }

        [NotNull, MaxLength(4)]
        public String Center { get; set; }

        [NotNull]
        public DateTime Produccion { get; set; }

        [NotNull]
        public DateTime Fecha { get; set; }

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

        [NotNull, Default(true, 0)]
        public Byte TurnID { get; set; }

        [NotNull]
        public Messages Message { get; set; }

        [MaxLength(10)]
        public String TrayID { get; set; }

        [MaxLength(10)]
        public String Lot { get; set; }

        [NotNull, Default(true, 0)]
        public Single Quantity { get; set; }

        [MaxLength(10)]
        public String SubEquipmentID { get; set; }

        [MaxLength(3)]
        public String Unit { get; set; }        
    }
}
