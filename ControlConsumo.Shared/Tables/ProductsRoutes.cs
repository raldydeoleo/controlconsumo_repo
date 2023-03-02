using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("ProductsRoutes")]
    public class ProductsRoutes
    {
        public enum RoutesStatus
        {
            EnTransito = 1,
            EnEquipo = 2,
            Procesado = 3,
            Devuelto = 4,
            Terminada = 5,
            Cancelada = 6
        }

        [NotNull, PrimaryKey, AutoIncrement]
        public Int64 ID { get; set; }

        [NotNull, MaxLength(4)]
        public String ProcessID { get; set; }

        [NotNull, MaxLength(2)]
        public String TimeID { get; set; }

        [NotNull, MaxLength(4)]
        public String Year { get; set; }

        [NotNull, Default(true, 0)]
        public Int64 CustomID { get; set; }

        [NotNull, MaxLength(10)]
        public String EquipmentID { get; set; }

        [NotNull, MaxLength(4)]
        public String Center { get; set; }

        [NotNull, MaxLength(18)]
        public String ProductCode { get; set; }

        [NotNull, MaxLength(4)]
        public String VerID { get; set; }

        [MaxLength(10)]
        public String Lot { get; set; }

        [MaxLength(100)]
        public String LotManufacture { get; set; }

        [NotNull]
        public Int32 CustomFecha { get; set; }

        [NotNull]
        public DateTime Produccion { get; set; }

        [NotNull, Default(true, 0)]
        public Byte TurnID { get; set; }

        [MaxLength(15), NotNull]
        public String TrayID { get; set; }

        [MaxLength(18)]
        public String BatchID { get; set; }

        [MaxLength(15)]
        public String PackID { get; set; }

        [NotNull]
        public Int16 SecuenciaEmpaque { get; set; }

        [NotNull, Default(true, 0)]
        public Single Quantity { get; set; }

        [NotNull, Default(true, 0)]
        public Single Peso { get; set; }

        [MaxLength(3)]
        public String Unit { get; set; }

        [NotNull, MaxLength(15)]
        public String Logon { get; set; }

        [NotNull, Default(true, true), Indexed]
        public Boolean Sync { get; set; }

        [NotNull]
        public RoutesStatus Status { get; set; }

        [NotNull]
        public Int16 ElaborateID { get; set; }

        [NotNull]
        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public DateTime Fecha { get; set; }

        [MaxLength(2)]
        public String TimeID2 { get; set; }

        [MaxLength(4)]
        public String Year2 { get; set; }

        [Default(true, 0)]
        public Int64 CustomID2 { get; set; }

        [NotNull, Default(true, false)]
        public Boolean IsActive { get; set; }

        [Ignore]
        public DateTime LastUpdate { get; set; }

        [Ignore]
        public String UniqueKey { get { return String.Concat(ProcessID, TimeID, Year, CustomID); } }
    }
}
