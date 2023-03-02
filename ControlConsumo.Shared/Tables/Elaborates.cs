using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Elaborates")]
    public class Elaborates
    {
        public enum Sources
        {
            DataBase,
            Memory,
            LastTray
        }

        [NotNull, PrimaryKey, AutoIncrement]
        public Int32 ID { get; set; }

        [NotNull, MaxLength(4)]
        public String ProcessID { get; set; }

        [NotNull, MaxLength(4)]
        public String Center { get; set; }

        [NotNull, Indexed]
        public DateTime Produccion { get; set; }

        [NotNull]
        public DateTime Fecha { get; set; }

        [NotNull]
        public Int32 CustomFecha { get; set; }

        [NotNull, Default(true, 0)]
        public Int32 CustomID { get; set; }

        [NotNull, MaxLength(10)]
        public String EquipmentID { get; set; }

        [NotNull, MaxLength(2)]
        public String TimeID { get; set; }

        [NotNull, MaxLength(18)]
        public String ProductCode { get; set; }

        [NotNull, MaxLength(4)]
        public String VerID { get; set; }

        [NotNull, Default(true, 0)]
        public Byte TurnID { get; set; }

        [NotNull, MaxLength(12)]
        public String Logon { get; set; }

        [NotNull, MaxLength(18)]
        public String BatchID { get; set; }

        [NotNull]
        public Single Peso { get; set; }

        [NotNull, Default(true, true), Indexed]
        public Boolean Sync { get; set; }

        [NotNull, Default(true, true), Indexed]
        public Boolean SyncSQL { get; set; }

        [MaxLength(15), NotNull]
        public String TrayID { get; set; }

        [NotNull, Default(true, 0)]
        public Single Quantity { get; set; }

        [MaxLength(10)]
        public String SubEquipmentID { get; set; }

        [MaxLength(3)]
        public String Unit { get; set; }

        [MaxLength(10)]
        public String Lot { get; set; }

        //Atributo que almacena el lote interno
        [MaxLength(15)]
        public String Reference { get; set; }

        public DateTime? ExpireDate { get; set; }

        [MaxLength(15)]
        public String PackID { get; set; }

        [NotNull, Default(true, false)]
        public Boolean IsReturn { get; set; }

        [NotNull, Default(true, false)]
        public Boolean IsCold { get; set; }

        //Atributo que guarda la secuencia de empaque
        [Default(true, 0)]
        public Int32 PackSequence { get; set; }

        //Atributo que indica el código de producción de acuerdo al tipo de producto y tipo de almacenamiento escogido.
        [MaxLength(10)]
        public String Identifier { get; set; }

        [Ignore]
        public Sources Source { get; set; }

        [Ignore]
        public DateTime _Produccion
        {
            get
            {
                switch (Source)
                {
                    case Sources.DataBase: return Produccion.ToLocalTime();
                    case Sources.Memory: return Produccion;
                    default: return Produccion.ToUniversalTime();
                }
            }
        }

        [Ignore]
        public DateTime _Fecha
        {
            get
            {
                switch (Source)
                {
                    case Sources.DataBase: return Fecha.ToLocalTime();
                    case Sources.Memory: return Fecha;
                    default: return Fecha.ToUniversalTime();
                }
            }
        }
    }
}
