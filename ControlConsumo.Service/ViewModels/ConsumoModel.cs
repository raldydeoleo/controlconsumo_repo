using ControlConsumo.Service.Models.ControlConsumo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class ConsumoModel
    {
        public String IDPROCESS { get; set; }
        public String WERKS { get; set; }
        public String IDEQUIPO { get; set; }
        public String IDTIEMPO { get; set; }
        public String MATNR { get; set; }
        public String VERID { get; set; }
        public Int16 SECENTRADA { get; set; }
        public String FECHA { get; set; }
        public String HORA { get; set; }
        public Byte IDTURNO { get; set; }
        public String MATNR2 { get; set; }
        public String IDEQUIPO2 { get; set; }
        public String CHARG { get; set; }
        public Single MENGE { get; set; }
        public String MEINS { get; set; }
        public Int16 BOXNO { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String CPUDT2 { get; set; }
        public String CPUTM2 { get; set; }
        public String USNAM { get; set; }
        public String IDBANDEJA { get; set; }
        public String IDEQUIPO3 { get; set; }
        public Int16 SECSALIDA { get; set; }
        public String CPUDT3 { get; set; }
        public String CPUTM3 { get; set; }
        public String BATCHID { get; set; }
        public static implicit operator ConsumoModel(Consumo consumo)
        {
            var consumoModel = new ConsumoModel
            {
                IDPROCESS = consumo.IdProceso,
                WERKS = consumo.Centro,
                IDEQUIPO = consumo.IdEquipo,
                IDTIEMPO = consumo.IdTiempo,
                MATNR = consumo.IdProducto,
                VERID = consumo.VersionFabricacion,
                SECENTRADA = (short)consumo.Secuencia,
                FECHA = consumo.FechaProduccion.GetSapDate(),
                HORA = consumo.FechaProduccion.GetSapHora(),
                IDTURNO = (byte)consumo.Turno,
                MATNR2 = consumo.IdMaterial,
                USNAM = consumo.Usuario,
                IDEQUIPO2 = consumo.IdSubEquipo,
                CHARG = String.IsNullOrEmpty(consumo.Lote) ? "" : consumo.Lote,
                MENGE = (float)consumo.Cantidad,
                MEINS = consumo.Unidad,
                BOXNO = (short)consumo.NumeroCaja,
                CPUDT = consumo.FechaRegistro.GetSapDate(),
                CPUTM = consumo.FechaRegistro.GetSapHora(),
                CPUDT2 = consumo.FechaSincronizacion.GetSapDate(),
                CPUTM2 = consumo.FechaSincronizacion.GetSapHora(),
                IDBANDEJA = consumo.IdBandeja,
                IDEQUIPO3 = consumo.IdEquipoOrigenMaterial,
                SECSALIDA = (short)consumo.SecuenciaSalida,
                CPUDT3 = consumo.FechaSalida != null ? consumo.FechaSalida.Value.GetSapDate() : null,
                CPUTM3 = consumo.FechaSalida != null ? consumo.FechaSalida.Value.GetSapHora() : null,
                BATCHID = consumo.BatchId
            };
            return consumoModel;
        }
    }
}