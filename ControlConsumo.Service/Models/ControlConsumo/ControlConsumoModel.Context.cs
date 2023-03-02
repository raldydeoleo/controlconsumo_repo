﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ControlConsumoEntities : DbContext
    {
        public ControlConsumoEntities()
            : base("name=ControlConsumoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bandeja> Bandejas { get; set; }
        public virtual DbSet<ConfiguracionInicialControlSalida> ConfiguracionInicialControlSalidas { get; set; }
        public virtual DbSet<ConfiguracionSincronizacionTabla> ConfiguracionSincronizacionTablas { get; set; }
        public virtual DbSet<ConfiguracionTiempoSalida> ConfiguracionTiempoSalidas { get; set; }
        public virtual DbSet<Consumo> Consumoes { get; set; }
        public virtual DbSet<EstatusBandeja> EstatusBandejas { get; set; }
        public virtual DbSet<HistorialReimpresionEtiqueta> HistorialReimpresionEtiquetas { get; set; }
        public virtual DbSet<MezclasProduccion> MezclasProduccions { get; set; }
        public virtual DbSet<MotivoReimpresionEtiqueta> MotivoReimpresionEtiquetas { get; set; }
        public virtual DbSet<ParametrosSSI> ParametrosSSIS { get; set; }
        public virtual DbSet<ProductoTipoAlmacenamiento> ProductoTipoAlmacenamientoes { get; set; }
        public virtual DbSet<SalidaProducto> SalidaProductoes { get; set; }
        public virtual DbSet<TiempoBandeja> TiempoBandejas { get; set; }
        public virtual DbSet<TipoAlmacenamientoProducto> TipoAlmacenamientoProductoes { get; set; }
        public virtual DbSet<TipoProductoTerminado> TipoProductoTerminadoes { get; set; }
        public virtual DbSet<Traza> Trazas { get; set; }
        public virtual DbSet<TrazaSequence> TrazaSequences { get; set; }
        public virtual DbSet<Turno> Turnos { get; set; }
        public virtual DbSet<SSIS_Configuration> SSIS_Configurations { get; set; }
        public virtual DbSet<View_EstatusBandejas> View_EstatusBandejas { get; set; }
    
        public virtual ObjectResult<Lynxsp_CargarDatosTraza_Result> Lynxsp_CargarDatosTraza(string idProceso, string idEquipo, Nullable<int> secSalida, Nullable<System.DateTime> fechaSalida)
        {
            var idProcesoParameter = idProceso != null ?
                new ObjectParameter("IdProceso", idProceso) :
                new ObjectParameter("IdProceso", typeof(string));
    
            var idEquipoParameter = idEquipo != null ?
                new ObjectParameter("IdEquipo", idEquipo) :
                new ObjectParameter("IdEquipo", typeof(string));
    
            var secSalidaParameter = secSalida.HasValue ?
                new ObjectParameter("SecSalida", secSalida) :
                new ObjectParameter("SecSalida", typeof(int));
    
            var fechaSalidaParameter = fechaSalida.HasValue ?
                new ObjectParameter("FechaSalida", fechaSalida) :
                new ObjectParameter("FechaSalida", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Lynxsp_CargarDatosTraza_Result>("Lynxsp_CargarDatosTraza", idProcesoParameter, idEquipoParameter, secSalidaParameter, fechaSalidaParameter);
        }
    
        public virtual ObjectResult<SP_GetSalidas_Result> SP_GetSalidas(Nullable<System.DateTime> fechaProduccion, Nullable<int> turno)
        {
            var fechaProduccionParameter = fechaProduccion.HasValue ?
                new ObjectParameter("FechaProduccion", fechaProduccion) :
                new ObjectParameter("FechaProduccion", typeof(System.DateTime));
    
            var turnoParameter = turno.HasValue ?
                new ObjectParameter("Turno", turno) :
                new ObjectParameter("Turno", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetSalidas_Result>("SP_GetSalidas", fechaProduccionParameter, turnoParameter);
        }
    }
}