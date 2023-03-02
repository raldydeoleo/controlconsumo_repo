using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.ProductoTipoAlmacenamiento
{
    class ProductoTipoAlmacenamientoResult
    {
        public int id { get; set; }
        public int idTipoProductoTerminado { get; set; }
        public int idTipoAlmacenamiento { get; set; }
        public string identificador { get; set; }
        public DateTime fechaRegistro { get; set; }
        public TimeSpan horaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public string usuarioModificacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public TimeSpan? HoraModificacion { get; set; }
        public bool? estatus { get; set; }
    }
}
