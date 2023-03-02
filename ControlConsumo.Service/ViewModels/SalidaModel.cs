using ControlConsumo.Service.Models.ControlConsumo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Model
{
    public class SalidaModel
    {
        public String MANDT { get; set; }
        public String IDPROCESS { get; set; }
        public String IDEQUIPO { get; set; }
        public String WERKS { get; set; }
        public String IDTIEMPO { get; set; }
        public String IDBANDEJA { get; set; }
        public Int16 SECSALIDA { get; set; }
        public String MATNR { get; set; }
        public String VERID { get; set; }
        public String FECHA { get; set; }
        public String HORA { get; set; }
        public Byte IDTURNO { get; set; }
        public String USNAM { get; set; }
        public Single MENGE { get; set; }
        public Single MENGE2 { get; set; }
        public String MEINS { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String CPUDT2 { get; set; }
        public String CPUTM2 { get; set; }
        public String RETURNED { get; set; }
        public String CHARG { get; set; }
        public String BATCHID { get; set; }
        public String IDEQUIPO2 { get; set; }
        public String VFDAT { get; set; }
        public String IDEMPAQUE { get; set; }
        //Campo de SAP que guarda el identificador. Ej: si es X, S, P, H
        public String COLD { get; set; }
        public Int16 SECEMPAQUE { get; set; }

        /*
         *    IdProceso = item.IDPROCESS,
                                Centro = item.WERKS,
                                IdEquipo = item.IDEQUIPO,
                                IdTiempo = item.IDTIEMPO,
                                IdProducto = item.MATNR,
                                VersionFabricacion = item.VERID,
                                Secuencia = item.SECSALIDA,
                                FechaProduccion = fechaproduccion,
                                Turno = item.IDTURNO,
                                BatchId = batchId,
                                Usuario = item.USNAM,
                                IdBandeja = item.IDBANDEJA,
                                Cantidad = item.MENGE,
                                Unidad = item.MEINS,
                                PesoProducto = item.MENGE2,
                                FechaSincronizacion = DateTime.Now,
                                //FechaContabilizacion = Util.GetDatetime(item.CPUDT, item.CPUTM).Value,
                                FechaRegistro = fechaRegistro,
                                Devuelto = !String.IsNullOrEmpty(item.RETURNED),
                                SubEquipo = item.IDEQUIPO2,
                                Lote = item.CHARG,
                                FechaCaducidad = Util.GetDatetime(item.VFDAT, null),
                                Empaque = item.IDEMPAQUE,
                                AlmFiller = item.COLD,
                                SecuenciaEtiqueta = item.SECEMPAQUE
         * */
        public static implicit operator SalidaModel(SalidaProducto salida)
        {
            var salidaModel = new SalidaModel
            {
                IDPROCESS = salida.IdProceso,
                WERKS = salida.Centro,
                IDEQUIPO = salida.IdEquipo,
                IDTIEMPO = salida.IdTiempo,
                MATNR = salida.IdProducto,
                VERID = salida.VersionFabricacion,
                SECSALIDA = (short) salida.Secuencia,
                FECHA = salida.FechaProduccion.GetSapDate(),
                HORA = salida.FechaProduccion.GetSapHora(),
                IDTURNO = (byte) salida.Turno,
                BATCHID = salida.BatchId,
                USNAM = salida.Usuario,
                IDBANDEJA = !string.IsNullOrEmpty(salida.IdBandeja)? salida.IdBandeja : string.Empty,
                MENGE = (float) salida.Cantidad,
                MEINS = salida.Unidad,
                MENGE2 = (float) salida.PesoProducto,
                CPUDT = salida.FechaRegistro.GetSapDate(),
                CPUTM = salida.FechaRegistro.GetSapHora(),
                RETURNED = (salida.Devuelto != null ? (salida.Devuelto.Value == true ? "X" : null) : null),
                IDEQUIPO2 = salida.SubEquipo,
                CHARG = salida.Lote,
                VFDAT = salida.FechaCaducidad != null ? salida.FechaCaducidad.Value.GetSapDate() : null,
                IDEMPAQUE = salida.Empaque,
                COLD = salida.AlmFiller,
                SECEMPAQUE = (short) salida.SecuenciaEtiqueta
            };
            return salidaModel;
        }
    }
}