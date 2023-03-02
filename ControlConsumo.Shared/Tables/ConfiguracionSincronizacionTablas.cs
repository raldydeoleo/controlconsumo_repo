using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("ConfiguracionSincronizacionTablas")]
    public class ConfiguracionSincronizacionTablas
    {
        [NotNull, PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [NotNull, MaxLength(75)]
        public string nombreTabla { get; set; }

        [NotNull, Default(true, false)]
        public bool procesarSap { get; set; }

        [NotNull]
        public DateTime fechaRegistro { get; set; }

        [NotNull, MaxLength(50) ]
        public string usuarioRegistro { get; set; }

        public DateTime? fechaModificacion { get; set; }
        public string usuarioModificacion { get; set; }
    }
}
