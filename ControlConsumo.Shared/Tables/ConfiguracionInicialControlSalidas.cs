using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("ConfiguracionInicialControlSalidas")]
    public class ConfiguracionInicialControlSalidas
    {
        [NotNull, PrimaryKey]
        public int ID { get; set; }
        
        [NotNull, MaxLength(10)]
        public string IdEquipo { get; set; }

        [NotNull, MaxLength(20)]
        public string IdProducto { get; set; }

        [NotNull]
        public int CustomFecha { get; set; }

        [NotNull]
        public DateTime FechaProduccion { get; set; }

        [NotNull]
        public int Turno { get; set; }

        [NotNull]
        public double CantidadConsumoPendiente { get; set; }

        [NotNull, MaxLength(10)]
        public string Unidad { get; set; }

        public DateTime FechaLectura { get; set; }

        [NotNull]
        public DateTime FechaRegistro { get; set; }

        [NotNull, MaxLength(25)]
        public string UsuarioRegistro { get; set; }

        public DateTime FechaModificacion { get; set; }

        [MaxLength(25)]
        public string UsuarioModificacion { get; set; }

        [NotNull, Default(true, true)]
        public bool Estatus { get; set; }
    }
}
