using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class ProductoTipoAlmacenamientoViewModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idTipoProductoTerminado { get; set; }        
        public int idTipoAlmacenamiento { get; set; }
        public string identificador { get; set; }
        public System.DateTime fechaRegistro { get; set; }
        public string horaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public string usuarioModificacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public TimeSpan? HoraModificacion { get; set; }
        public Nullable<bool> estatus { get; set; }

    }
}