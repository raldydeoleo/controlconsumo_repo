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
    
    public partial class View_EstatusBandejas
    {
        public string Id_Bandeja { get; set; }
        public int Secuencia { get; set; }
        public string estatus { get; set; }
        public string Id_Proc { get; set; }
        public string Id_Tiempo { get; set; }
        public string Material { get; set; }
        public string Versión_fabricación { get; set; }
        public string Equipo_llenado { get; set; }
        public Nullable<System.DateTime> Fecha_ { get; set; }
        public Nullable<System.TimeSpan> Hora_de_llenada { get; set; }
        public string Usuario_de_llenado { get; set; }
        public int Secuencia_Salida { get; set; }
        public string Batch_ID { get; set; }
        public double cantidad { get; set; }
        public string UMP { get; set; }
        public System.DateTime Fecha_de_registro { get; set; }
        public System.TimeSpan Hora__de_registro { get; set; }
        public string Equipo_de_vaciado { get; set; }
        public Nullable<System.DateTime> Fecha_de_vaciado_ { get; set; }
        public Nullable<System.TimeSpan> Hora_de_vaciado { get; set; }
        public string Usuario { get; set; }
        public string Forma_de_vaciado { get; set; }
    }
}