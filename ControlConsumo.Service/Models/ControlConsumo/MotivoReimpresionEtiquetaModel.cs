using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class MotivoReimpresionEtiquetaModel
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Estatus { get; set; }

        public static implicit operator MotivoReimpresionEtiquetaModel(MotivoReimpresionEtiqueta motivoReimpresionEtiqueta)
        {
            var reimpresionEtiquetaModel = new MotivoReimpresionEtiquetaModel
            {
                ID = motivoReimpresionEtiqueta.ID,
                Descripcion = motivoReimpresionEtiqueta.Descripcion,
                FechaCreacion = motivoReimpresionEtiqueta.FechaCreacion,
                UsuarioCreacion = motivoReimpresionEtiqueta.UsuarioCreacion,
                FechaModificacion = motivoReimpresionEtiqueta.FechaModificacion,
                UsuarioModificacion = motivoReimpresionEtiqueta.UsuarioModificacion,
                Estatus = motivoReimpresionEtiqueta.Estatus
            };
            return reimpresionEtiquetaModel;
        }
    }
}