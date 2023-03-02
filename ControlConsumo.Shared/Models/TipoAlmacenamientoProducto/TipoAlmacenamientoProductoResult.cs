using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.TipoAlmacenamientoProducto
{
    class TipoAlmacenamientoProductoResult
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fechaRegistro { get; set; }
        public TimeSpan horaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public string usuarioModificacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public TimeSpan? horaModificacion { get; set; }
        public bool estatus { get; set; }
    }
}
