using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("TraysProducts")]
    public class TraysProducts
    {
        public enum _Status
        {
            Vacio,
            Lleno
        }

        [NotNull, PrimaryKey]
        public String ID { get; set; }

        [NotNull, MaxLength(10)]
        public String TrayID { get; set; }

        [NotNull]
        public Int16 Secuencia { get; set; }

        [NotNull]
        public _Status Status { get; set; }

        [NotNull, MaxLength(18)]
        public String ProductCode { get; set; }

        [NotNull, MaxLength(4)]
        public String VerID { get; set; }

        [MaxLength(2), NotNull]
        public String TimeID { get; set; }

        [MaxLength(10)]
        public String EquipmentID { get; set; }

        public DateTime? Fecha { get; set; }

        [MaxLength(18)]
        public string UsuarioLlenada { get; set; }

        public Int16 ElaborateID { get; set; }

        [MaxLength(18)]
        public String BatchID { get; set; }

        [NotNull]
        public Single Quantity { get; set; }

        [NotNull, MaxLength(3)]
        public String Unit { get; set; }

        [NotNull, Default(true, false)]
        public Boolean Sync { get; set; }

        public DateTime ModifyDate { get; set; }

        public DateTime? FechaHoraVaciada { get; set; }

        [MaxLength(18)]
        public string UsuarioVaciada { get; set; }

        [MaxLength(10)]
        public string IdEquipoVaciado { get; set; }

        [MaxLength(10)]
        public string formaVaciada { get; set; }
    }
}
