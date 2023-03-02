using ControlConsumo.Service.Model;
using ControlConsumo.Service.Models.ControlConsumo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ControlConsumo.Service.Managers
{
    /// <summary>
    /// Clase encargada de emular las transacciones 
    /// </summary>
    public class TransaccionManager
    {
        static TransaccionManager()
        {
            LastTraza = new List<Traza>();
            using (var ctx = new ControlConsumoEntities())
            {
                Secuencias = ctx.TrazaSequences.ToList();
                Turnos = ctx.Turnos.ToList();
            }
        }

        private static List<TrazaSequence> Secuencias { get; set; }
        private static List<Traza> LastTraza { get; set; }
        private static List<Turno> Turnos { get; set; }

        /// <summary>
        /// Metodo para cargar una secuencia nueva
        /// </summary>
        /// <param name="year">Ano</param>
        /// <param name="Tiempo">Tiempo</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private static Int32 GetNextSequences(int year, String tiempo)
        {
            var retorno = 0;
            var proximo = Secuencias.Where(s => s.AnoEjercicioDocumentoMaterial == year).Where(s => s.IdTiempo == tiempo).SingleOrDefault();
            using (var ctx = new ControlConsumoEntities())
            {
                if (proximo == null)
                {
                    var nuevo = new TrazaSequence
                    {
                        AnoEjercicioDocumentoMaterial = Convert.ToInt16(year),
                        IdTiempo = tiempo,
                        IdRegistro = 1
                    };
                    ctx.TrazaSequences.Add(nuevo);
                    retorno = nuevo.IdRegistro;
                }
                else
                {
                    proximo.IdRegistro++;
                    var update = ctx.TrazaSequences.Single(s => s.AnoEjercicioDocumentoMaterial == proximo.AnoEjercicioDocumentoMaterial && s.IdTiempo == proximo.IdTiempo);
                    update.IdRegistro = proximo.IdRegistro;
                    retorno = proximo.IdRegistro;
                }
                ctx.SaveChanges();
            }

            return retorno;
        }

        public static void SaveMezclas(List<MezclaModel> models)
        {
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    foreach (var item in models)
                    {
                        ///Validamos que no exista 
                        var fechaEntrada = new DateTime();
                        var fechaSalida = new DateTime();
                        fechaEntrada = Util.GetDatetime(item.ZENTRADADATE, null).Value;
                        fechaSalida = Util.GetDatetime(item.ZSALIDADATE, null).Value;
                        var existe = ctx.MezclasProduccions.Any(a =>
                            a.Idequipo == item.IDEQUIPO &&
                            a.Idproceso == item.IDPROCESS &&
                            a.SecuenciaEntrada == item.SECENTRADA &&
                            a.SecuenciaSalida == item.SECSALIDA &&
                            a.FechaEntrada == fechaEntrada &&
                            a.FechaSalida == fechaSalida
                        );

                        if (!existe)
                        {
                            var mezclaProduccion = new MezclasProduccion
                            {
                                Idequipo = item.IDEQUIPO,
                                Idproceso = item.IDPROCESS,
                                SecuenciaEntrada = item.SECENTRADA,
                                SecuenciaSalida = item.SECSALIDA,
                                FechaEntrada = Util.GetDatetime(item.ZENTRADADATE).Value,
                                FechaSalida = Util.GetDatetime(item.ZSALIDADATE).Value,
                                FechaLlenada = Util.GetDatetime(item.CPUDT, item.CPUTM).Value,
                                fechaCarga = DateTime.Now
                            };
                            ctx.MezclasProduccions.Add(mezclaProduccion);
                        }
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SaveConsumos(List<ConsumoModel> models)
        {
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    foreach (var item in models)
                    {
                        ///Validamos si existe el registro
                        ///
                        var fechaProduccion = new DateTime();
                        fechaProduccion = Util.GetDatetime(item.FECHA, item.HORA).Value;
                        var existe = ctx.Consumoes.Any(s =>
                            s.IdProceso == item.IDPROCESS &&
                            s.Centro == item.WERKS &&
                            s.IdEquipo == item.IDEQUIPO &&
                            s.IdTiempo == item.IDTIEMPO &&
                            s.IdProducto == item.MATNR &&
                            s.VersionFabricacion == item.VERID &&
                            s.Secuencia == item.SECENTRADA &&
                            s.FechaProduccion == fechaProduccion &&
                            s.Turno == item.IDTURNO &&
                            s.IdMaterial == item.MATNR2 &&
                            s.Usuario == item.USNAM
                            );

                        if (!existe)
                        {
                            ctx.Consumoes.Add(new Consumo
                            {
                                IdProceso = item.IDPROCESS,
                                Centro = item.WERKS,
                                IdEquipo = item.IDEQUIPO,
                                IdTiempo = item.IDTIEMPO,
                                IdProducto = item.MATNR,
                                VersionFabricacion = item.VERID,
                                Secuencia = item.SECENTRADA,
                                FechaProduccion = fechaProduccion,
                                Turno = item.IDTURNO,
                                IdMaterial = item.MATNR2,
                                Usuario = item.USNAM,
                                IdSubEquipo = item.IDEQUIPO2,
                                Lote = String.IsNullOrEmpty(item.CHARG)? "": item.CHARG,
                                Cantidad = item.MENGE,
                                Unidad = item.MEINS,
                                NumeroCaja = item.BOXNO,
                                FechaRegistro = Util.GetDatetime(item.CPUDT, item.CPUTM).Value,
                                FechaSincronizacion = DateTime.Now,
                                IdBandeja = item.IDBANDEJA,
                                IdEquipoOrigenMaterial = item.IDEQUIPO3,
                                SecuenciaSalida = item.SECSALIDA,
                                FechaSalida = Util.GetDatetime(item.CPUDT3, item.CPUTM3),
                                BatchId = item.BATCHID
                            });
                        }
                    }
                    ctx.SaveChanges();
                }
            }
            catch(Exception)
            {
                throw;
            }

        }

        public static void SaveSalidas(List<SalidaModel> models)
        {
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    foreach (var item in models)
                    {
                        var fechaproduccion = Util.GetDatetime(item.FECHA, item.HORA).Value;
                        var fechaRegistro = Util.GetDatetime(item.CPUDT, item.CPUTM).Value;
                        var batchId = !String.IsNullOrEmpty(item.BATCHID) ? item.BATCHID : String.Empty;
                        ///Valido si la salida no existe 
                        var salidaProducto = ctx.SalidaProductoes.Where(a =>
                            a.IdProceso == item.IDPROCESS &&
                            a.Centro == item.WERKS &&
                            a.IdEquipo == item.IDEQUIPO &&
                            a.IdTiempo == item.IDTIEMPO &&
                            a.IdProducto == item.MATNR &&
                            a.VersionFabricacion == item.VERID &&
                            a.Secuencia == item.SECSALIDA &&
                            a.FechaProduccion == fechaproduccion &&
                            a.Turno == item.IDTURNO &&
                            a.BatchId == batchId &&
                            a.Usuario == item.USNAM
                        ).SingleOrDefault();

                        if (salidaProducto == null)
                        {
                            ctx.SalidaProductoes.Add(new SalidaProducto
                            {
                                IdProceso = item.IDPROCESS,
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
                            });
                        }
                        else//Si existe, validar que sea una devolución para actualizar el registro
                        {
                            var esDevuelto = !String.IsNullOrEmpty(item.RETURNED);
                            if (esDevuelto != salidaProducto.Devuelto)
                            {
                                salidaProducto.Devuelto = esDevuelto;
                                salidaProducto.FechaSincronizacion = DateTime.Now;
                                ctx.SalidaProductoes.AddOrUpdate(salidaProducto);
                            }
                        }

                        
                         /* Guardado de registro en tabla TRAZA y TRAZA SEQUENCES
                         * Se desactivó hasta que se reciba la corrección del algoritmo por el suplidor. 
                         * 
                          
                        ///Valido si el registro no existe para no duplicarlo
                        var existe = ctx.Trazas.Any(s =>
                            s.IdProceso == item.IDPROCESS &&
                            s.IdTiempo == item.IDTIEMPO &&
                            s.AnoEjercicio == fechaproduccion.Year &&
                            s.IdEquipo == item.IDEQUIPO &&
                            s.SecuenciaSalida == item.SECSALIDA &&
                            s.FechaProduccion == fechaproduccion
                        );

                        if (!existe)
                        {
                            ///Buscamos el registro padre al que pertenece la traza
                            var resultPadre = ctx.Lynxsp_CargarDatosTraza(item.IDPROCESS, item.IDEQUIPO, item.SECSALIDA, fechaproduccion).FirstOrDefault();

                            var newTraza = new Traza
                            {
                                IdProceso = item.IDPROCESS,
                                IdTiempo = item.IDTIEMPO,
                                IdRegistro = GetNextSequences(fechaproduccion.Year, item.IDTIEMPO),
                                AnoEjercicio = fechaproduccion.Year,
                                IdEquipo = item.IDEQUIPO,
                                Centro = item.WERKS,
                                IdProducto = item.MATNR,
                                VersionFabricacion = item.VERID,
                                NumeroLote = item.CHARG,
                                LoteManufactura = String.Empty,
                                SecuenciaSalida = item.SECSALIDA,
                                FechaProduccion = fechaproduccion,
                                Turno = item.IDTURNO,
                                BatchId = batchId,
                                IdBandeja = item.IDBANDEJA,
                                IdEmpaque = item.IDEMPAQUE,
                                CantidadPedido = item.MENGE,
                                UnidadMedidaPedido = item.MEINS,
                                Peso = item.MENGE2,
                                Usuario = item.USNAM,
                                FechaSalida = Util.GetDatetime(item.CPUDT, item.CPUTM).Value,
                                FechaContabilizacionDocumento = Util.GetDatetime(item.CPUDT2, item.CPUTM2),
                                FechaRegistro = DateTime.Now,
                                IdRegistroPadre = resultPadre?.IdRegistro,
                                IdTiempoRegistroPadre = resultPadre?.IdTiempo,
                                AnoEjercicioPadre = resultPadre?.AnoEjercicio
                            };

                            if (!String.IsNullOrEmpty(item.COLD))
                            {
                                var traza = LastTraza.Where(a => a.IdEquipo == item.IDEQUIPO).Where(a => a.FechaRegistro.Value.Date == fechaRegistro.Date).SingleOrDefault();

                                if (traza != null) /// Valido la ultima Secuencia que se genero
                                {
                                    traza = ctx.Trazas.Where(w => w.IdEquipo == item.IDEQUIPO)
                                        .OrderByDescending(o => o.FechaRegistro).SingleOrDefault() ??
                                        new Traza() { IdEquipo = item.IDEQUIPO, FechaRegistro = DateTime.Now, SecuenciaEmpaque = 0 };
                               

                                    traza.SecuenciaEmpaque++;

                                    LastTraza.RemoveAll(r => r.IdEquipo == item.IDEQUIPO);
                                    LastTraza.Add(traza); ///cacheo el registro de traza para futuras busquedas

                                    ///Asignamos la secuencia de empaque correspondiente al registro de traza
                                    newTraza.SecuenciaEmpaque = traza.SecuenciaEmpaque;
                                    ///Asignamos el empaque que corresponde al registro
                                    newTraza.IdEmpaque = GetPackID(item.IDEQUIPO, item);
                                }
                            }

                            ctx.Trazas.Add(newTraza);
                        }*/
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para Generar el empaque de la traza
        /// </summary>
        /// <param name="EquipmentID">Numero del equipo</param>
        /// <param name="model">Salida</param>
        /// <returns></returns>
        public static String GetPackID(String EquipmentID, SalidaModel model)
        {
            try
            {
                var idturno = Convert.ToByte(model.IDTURNO);
                var Turno = Turnos.Single(p => p.ID == idturno);
                var Fecha = Util.GetDatetime(model.FECHA, model.HORA).Value;
                var Hour = Turno.Inicio;
                var totalWork = WorkHour(Turno);
                Int32 counter = 0;

                counter++;

                while (true)
                {
                    if (Hour == Fecha.Hour)
                        break;

                    counter++;
                    Hour++;

                    if (Hour > 23) Hour = 0;
                }

                if ((Fecha.Hour == 0 ? 24 : Fecha.Hour) < Turno.Inicio && (Fecha.Hour > 21 || idturno != 3 && counter > totalWork))
                    counter = 1;
                else if (counter > totalWork)
                    counter = totalWork;

                return String.Format("{0}-{1}-{2}-{3}-{4}", Fecha.DayOfYear.ToString("000"), Turno.Empaque, Fecha.ToString("yy").Substring(1, 1), EquipmentID.Trim().Substring(2, 1), counter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Calculamos el total de horas Trabajadas
        /// </summary>
        /// <param name="turno"></param>
        /// <returns></returns>
        public static Int32 WorkHour(Turno turno)
        {
            var Begin = GetBeginDate(turno);
            var End = GetEndDate(turno);

            var TotalWorkHour = End.Subtract(Begin).Hours + 1;

            if (TotalWorkHour < 0)
            {
                TotalWorkHour = End.AddDays(1).Subtract(Begin).Hours + 1;
            }

            return TotalWorkHour;
        }

        /// <summary>
        /// Metodo para extrar la fecha de Inicio
        /// </summary>
        /// <param name="turno"></param>
        /// <returns></returns>
        public static DateTime GetBeginDate(Turno turno)
        {
            var Fechaint = "19000101";
            var hora = String.Format("{0}{1}00", turno.Inicio.ToString("00"), turno.MinutosInicio.ToString("00"));
            return Util.GetDatetime(Fechaint, hora).Value;
        }

        /// <summary>
        /// Metodo para extraer la fecha de Fin
        /// </summary>
        /// <param name="turno"></param>
        /// <returns></returns>
        public static DateTime GetEndDate(Turno turno)
        {
            var Fechaint = "19000101";
            var hora = String.Format("{0}{1}00", turno.Fin.ToString("00"), turno.MinutosFin.ToString("00"));
            return Util.GetDatetime(Fechaint, hora).Value;
        }
    }
}