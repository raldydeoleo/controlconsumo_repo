using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Transactions")]
    public class Transactions
    {
        /// <summary>
        /// Enumeracion de los tipos de Transacciones
        /// </summary>
        public enum Types
        {
            Not_Available,
            Entrega_Material,
            Consumo_Material,
            Devolucion_Consumo,
            Devolucion_Buffer,
            Cierre_Turno,
            Ajuste_Inventario
        }

        [NotNull, PrimaryKey, AutoIncrement]
        public Int32 ID { get; set; }

        [NotNull]
        public Int32 CustomFecha { get; set; }

        [NotNull]
        public DateTime Fecha { get; set; }

        [NotNull, Default(true, 0)]
        public Byte TurnID { get; set; }

        [NotNull, MaxLength(18)]
        public String MaterialCode { get; set; }

        [MaxLength(10), NotNull]
        public String Lot { get; set; }

        [NotNull, Default(true, 0)]
        public Single Quantity { get; set; }

        [NotNull, Default(true, 0)]
        public Single Total { get; set; }

        [NotNull, Default(true, 0)]
        public Int16 BoxNumber { get; set; }

        [NotNull, Default(true, true), Indexed]
        public Boolean Sync { get; set; }

        [MaxLength(3)]
        public String Unit { get; set; }

        [NotNull, MaxLength(15)]
        public String Logon { get; set; }

        [NotNull, MaxLength(25)]
        public String Reason { get; set; }
    }
}
