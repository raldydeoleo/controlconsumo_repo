//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ControlConsumo.Service.Models.SodiQubeDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_QualityTestsLast6Months
    {
        public Nullable<int> Año { get; set; }
        public Nullable<int> Mes { get; set; }
        public Nullable<int> Semana { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public System.DateTime Fecha_y_hora { get; set; }
        public Nullable<byte> Turno { get; set; }
        public short No__Sodiqube { get; set; }
        public string Código_de_Filler { get; set; }
        public string Filler { get; set; }
        public string Máquina { get; set; }
        public Nullable<float> Diámetro__mm_ { get; set; }
        public short CycleId { get; set; }
        public string Notificación_diámetro { get; set; }
        public Nullable<float> Peso__gr_ { get; set; }
        public string Notificación_de_peso { get; set; }
        public Nullable<float> Tiro__mm_h2O_ { get; set; }
        public string Notificación_de_tiro { get; set; }
    }
}
