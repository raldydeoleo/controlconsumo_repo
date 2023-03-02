using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.ConfiguracionSincronizacionTablas
{
    public class ConfiguracionSincronizacionTablasResult
    {
        public int id { get; set; }

        public string nombreTabla { get; set; }

        public bool procesarSap { get; set; }

        public DateTime fechaRegistro { get; set; }

        public string usuarioRegistro { get; set; }

        public DateTime? fechaModificacion { get; set; }
        public string usuarioModificacion { get; set; }
    }
}

