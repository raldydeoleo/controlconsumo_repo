using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.TrayTime
{
    class TrayTimeResultSql
    {
        public int id { get; set; }
        public string idProceso { get; set; }
        public string idTiempo { get; set; }
        public string idBandeja { get; set; }
        public double cantidad { get; set; }
        public string unidad { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
