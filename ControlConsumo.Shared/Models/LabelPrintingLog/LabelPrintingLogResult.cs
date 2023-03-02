using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.LabelPrintingLog
{
    class LabelPrintingLogResult
    {
        public string IdEquipo { get; set; }
        public int SecuenciaEtiqueta { get; set; }
        public int IdMotivoReimpresionEtiqueta { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaProduccion { get; set; }
        public int Turno { get; set; }
        public string Empaque { get; set; }
        public string AlmFiller { get; set; }
        public string UsuarioReimpresion { get; set; }
        public DateTime FechaReimpresion { get; set; }
        public bool Estatus { get; set; }
    }
}
