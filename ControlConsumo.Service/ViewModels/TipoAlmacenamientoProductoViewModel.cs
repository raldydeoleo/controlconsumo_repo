using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class TipoAlmacenamientoProductoViewModel
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string horaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public string usuarioModificacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public TimeSpan? HoraModificacion { get; set; }
        public bool estatus { get; set; }    
    }
}