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
    
    public partial class V_ReporteMuestras
    {
        public Nullable<int> Año { get; set; }
        public Nullable<int> Mes { get; set; }
        public Nullable<int> Semana { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime Fecha_y_hora { get; set; }
        public Nullable<byte> Turno { get; set; }
        public string Máquina { get; set; }
        public Nullable<int> Código_de_Filler { get; set; }
        public string Filler { get; set; }
        public int ID { get; set; }
        public Nullable<double> Valor { get; set; }
        public string Variable { get; set; }
        public Nullable<double> Límite_Especificación_Inferior { get; set; }
        public Nullable<double> Límite_Especificación_Superior { get; set; }
        public Nullable<double> Media { get; set; }
        public Nullable<double> Límite_Control_Inferior { get; set; }
        public Nullable<double> Límite_Control_Superior { get; set; }
        public short CycleId { get; set; }
        public short No__Sodiqube { get; set; }
        public Nullable<bool> C_Fue_notificado_ { get; set; }
        public string Tipo_de_notificación { get; set; }
    }
}
