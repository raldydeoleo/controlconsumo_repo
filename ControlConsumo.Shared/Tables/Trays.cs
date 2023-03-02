using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Trays")]
    public class Trays
    {
        [PrimaryKey, NotNull, MaxLength(10)]
        public String ID { get; set; }

        [NotNull]
        public Int32 Desde { get; set; }

        [NotNull]
        public Int32 Hasta { get; set; }

        [NotNull, Default(value: false)]
        public Boolean procesarSAP { get; set; }

        [NotNull]
        public DateTime fechaRegistro { get; set; }

        [NotNull, MaxLength(50)]
        public string usuarioRegistro { get; set; }

        [NotNull, Default(value:true)]
        public Boolean estatusVigencia { get; set; }
    }
}
