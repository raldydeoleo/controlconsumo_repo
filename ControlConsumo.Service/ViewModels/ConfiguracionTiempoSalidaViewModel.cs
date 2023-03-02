using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class ConfiguracionTiempoSalidaViewModel
    {
        public int id { get; set; }
        public String idProceso { get; set; }
        public String idTiempo { get; set; }
        public double tiempoMinimo { get; set; }
        public string unidadTiempo { get; set; }
        public DateTime fechaRegistro { get; set; }
        public String horaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public string usuarioModificacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public String horaModificacion { get; set; }
        public Boolean estatus { get; set; }
    }
}