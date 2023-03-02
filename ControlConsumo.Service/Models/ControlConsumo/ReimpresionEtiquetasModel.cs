using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class ReimpresionEtiquetasModel
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
        public static implicit operator ReimpresionEtiquetasModel(HistorialReimpresionEtiqueta historialReimpresionEtiqueta)
        {
            var reimpresionEtiquetaModel = new ReimpresionEtiquetasModel
            {
                IdEquipo = historialReimpresionEtiqueta.IdEquipo,
                SecuenciaEtiqueta = historialReimpresionEtiqueta.SecuenciaEtiqueta,
                IdMotivoReimpresionEtiqueta = historialReimpresionEtiqueta.IdMotivoReimpresion,
                Cantidad = historialReimpresionEtiqueta.Cantidad,
                Empaque = historialReimpresionEtiqueta.Empaque,
                AlmFiller = historialReimpresionEtiqueta.AlmFiller,
                FechaProduccion = historialReimpresionEtiqueta.FechaProduccion,
                Turno = historialReimpresionEtiqueta.Turno,
                UsuarioReimpresion = historialReimpresionEtiqueta.UsuarioReimpresion,
                FechaReimpresion = historialReimpresionEtiqueta.FechaReimpresion,
                Estatus = historialReimpresionEtiqueta.Estatus,
            };
            return reimpresionEtiquetaModel;
        }
    }
}