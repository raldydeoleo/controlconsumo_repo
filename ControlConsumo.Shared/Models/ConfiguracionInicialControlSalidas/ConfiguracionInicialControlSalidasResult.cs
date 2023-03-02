using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.ConfiguracionInicialControlSalidas
{
    public class ConfiguracionInicialControlSalidasResult
    {
        public int Id { get; set; }
        public string IdEquipo { get; set; }
        public string IdProducto { get; set; }
        public DateTime FechaProduccion { get; set; }
        public int Turno { get; set; }
        public double CantidadConsumoPendiente { get; set; }
        public string Unidad { get; set; }
        public DateTime? FechaLectura { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Estatus { get; set; }
    }
}
