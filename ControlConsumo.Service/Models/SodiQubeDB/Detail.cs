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
    
    public partial class Detail
    {
        public string ProductID { get; set; }
        public string TypeID { get; set; }
        public string SubTypeID { get; set; }
        public string ParametroID { get; set; }
        public bool Display { get; set; }
        public decimal Value { get; set; }
    
        public virtual Type Type { get; set; }
    }
}
