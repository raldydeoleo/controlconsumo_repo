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
    
    public partial class QualityTest
    {
        public short SodimatId { get; set; }
        public System.DateTime DateTime { get; set; }
        public short CycleId { get; set; }
        public short RodId { get; set; }
        public short IsLastRod { get; set; }
        public string TstFile { get; set; }
        public string ProductCode { get; set; }
        public string MachineCode { get; set; }
        public Nullable<int> BrandCode { get; set; }
        public string BatchCode { get; set; }
        public Nullable<short> ReservedFlag { get; set; }
        public Nullable<int> ReservedCode { get; set; }
        public Nullable<int> ReservedCounter { get; set; }
        public Nullable<float> Weight { get; set; }
        public Nullable<float> WeightCor { get; set; }
        public Nullable<float> Moisture1 { get; set; }
        public Nullable<float> PressureDrop { get; set; }
        public Nullable<float> PressureDrop_FE { get; set; }
        public Nullable<float> FilterVent { get; set; }
        public Nullable<float> PaperVent { get; set; }
        public Nullable<float> Moisture2 { get; set; }
        public Nullable<float> Dia_Cir { get; set; }
        public Nullable<float> Ovality { get; set; }
        public Nullable<float> Hardness { get; set; }
        public Nullable<float> HardnessCor { get; set; }
        public Nullable<float> HardnessDiff { get; set; }
        public Nullable<float> HardnessDiffCor { get; set; }
        public Nullable<float> Length { get; set; }
        public Nullable<float> Dia_Cir2 { get; set; }
        public Nullable<float> Ovality2 { get; set; }
        public Nullable<float> PressureDropSealed { get; set; }
        public Nullable<float> FilterVentSealed { get; set; }
        public Nullable<float> Dia_Cir3 { get; set; }
        public Nullable<float> Ovality3 { get; set; }
        public Nullable<float> GlobalPorosity { get; set; }
        public Nullable<float> LocalPorosity { get; set; }
        public Nullable<float> AreaPorosity { get; set; }
        public Nullable<bool> WeightOutOfBounds { get; set; }
        public Nullable<bool> DiameterOutOfBounds { get; set; }
        public Nullable<bool> PressureDropOutOfBounds { get; set; }
        public string WeightCommentOutOfBounds { get; set; }
        public string DiameterCommentOutOfBounds { get; set; }
        public string PressureDropCommentOutOfBounds { get; set; }
        public Nullable<System.DateTime> NotificationDatePressureDrop { get; set; }
        public string NotificationEmail { get; set; }
        public Nullable<bool> isPressureDropProcessed { get; set; }
    }
}
