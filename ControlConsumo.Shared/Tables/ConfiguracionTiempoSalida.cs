using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("ConfiguracionTiempoSalida")]
    public class ConfiguracionTiempoSalida
    {
        [NotNull, PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [NotNull, MaxLength(10)]
        public String idProceso { get; set; }

        [NotNull, MaxLength(10)]
        public String idTiempo { get; set; }

        public double tiempoMinimo { get; set; }

        [NotNull, MaxLength(3)]
        public string unidadTiempo { get; set; }

        [NotNull]
        public DateTime fechaRegistro { get; set; }

        [NotNull, MaxLength(7)]
        public TimeSpan horaRegistro { get; set; }

        [NotNull, MaxLength(50)]
        public string usuarioRegistro { get; set; }

        [MaxLength(50)]
        public string usuarioModificacion { get; set; }

        public DateTime? fechaModificacion { get; set; }

        public TimeSpan? horaModificacion { get; set; }

        [NotNull, Default(true, false)]
        public bool estatus { get; set; }
    }
}
