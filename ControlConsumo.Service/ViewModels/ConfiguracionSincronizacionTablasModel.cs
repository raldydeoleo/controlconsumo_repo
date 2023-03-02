using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class ConfiguracionSincronizacionTablasModel
    {
        public int id { get; set; }
        public string nombreTabla { get; set; }
        public Boolean procesarSap { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public string usuarioModificacion { get; set; }
    }
}