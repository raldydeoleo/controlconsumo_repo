//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ControlConsumo.Service.Models.ControlConsumo
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoAlmacenamientoProducto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoAlmacenamientoProducto()
        {
            this.ProductoTipoAlmacenamientoes = new HashSet<ProductoTipoAlmacenamiento>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public System.DateTime fechaRegistro { get; set; }
        public System.TimeSpan horaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public string usuarioModificacion { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        public Nullable<System.TimeSpan> HoraModificacion { get; set; }
        public bool estatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductoTipoAlmacenamiento> ProductoTipoAlmacenamientoes { get; set; }
    }
}
