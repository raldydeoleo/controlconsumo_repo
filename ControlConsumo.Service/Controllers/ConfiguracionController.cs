using ControlConsumo.Service.Managers;
using ControlConsumo.Service.Model;
using ControlConsumo.Service.Models.ControlConsumo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;

namespace ControlConsumo.Service.Controllers
{
    public class ConfiguracionController : Controller
    {
        [WebMethod]
        [System.Web.Mvc.HttpGet]
        public JsonResult GetParametrosTiempo()
        {
            var lista = new List<ConfiguracionTiempoSalidaViewModel>();
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    foreach (var item in ctx.ConfiguracionTiempoSalidas)
                    {
                        var configuracion = new ConfiguracionTiempoSalidaViewModel();
                        configuracion.id = item.id;
                        configuracion.idProceso = item.idProceso;
                        configuracion.idTiempo = item.idTiempo;
                        configuracion.tiempoMinimo = item.tiempoMinimo;
                        configuracion.unidadTiempo = item.unidadTiempo;
                        configuracion.fechaRegistro = item.fechaRegistro;
                        configuracion.horaRegistro = item.horaRegistro.ToString();
                        configuracion.usuarioRegistro = item.usuarioRegistro;
                        configuracion.estatus = item.estatus;
                        lista.Add(configuracion);
                    }
                }
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        /// <summary>
        /// Método que retorna la configuración de sincronización de las tablas de la aplicación móvil. 
        /// </summary>
        /// <returns>List<ConfiguracionSincronizacionTablasModel></returns>
        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetConfiguracionSincronizacionTablas()
        {
            var lista = new List<ConfiguracionSincronizacionTablasModel>();
            try
            {
                using(var ctx = new ControlConsumoEntities())
                {
                    foreach(var item in ctx.ConfiguracionSincronizacionTablas)
                    {
                        var configuracion = new ConfiguracionSincronizacionTablasModel();
                        configuracion.id = item.id;
                        configuracion.nombreTabla = item.nombreTabla;
                        configuracion.procesarSap = item.procesarSap;
                        configuracion.fechaRegistro = item.fechaRegistro;
                        configuracion.usuarioRegistro = item.usuarioRegistro;
                        if (item.usuarioModificacion!=null && item.fechaModificacion != null)
                        {
                            configuracion.usuarioModificacion = item.usuarioModificacion;
                            configuracion.fechaModificacion = item.fechaModificacion;
                        }
                        lista.Add(configuracion);
                    }
                }
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetTipoAlmacenamientoProductos()
        {
            var lista = new List<TipoAlmacenamientoProductoViewModel>();
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    foreach (var item in ctx.TipoAlmacenamientoProductoes)
                    {
                        var tipoAlmacenamientoProducto = new TipoAlmacenamientoProductoViewModel();
                        tipoAlmacenamientoProducto.id = item.id;
                        tipoAlmacenamientoProducto.nombre = item.nombre;
                        tipoAlmacenamientoProducto.fechaRegistro = item.fechaRegistro;
                        if (item.horaRegistro != null)
                        {
                            tipoAlmacenamientoProducto.horaRegistro = item.horaRegistro.ToString();
                        }
                        tipoAlmacenamientoProducto.usuarioRegistro = item.usuarioRegistro;
                        tipoAlmacenamientoProducto.estatus = item.estatus;
                        lista.Add(tipoAlmacenamientoProducto);
                    }
                }
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetTipoProductoTerminados()
        {
            var lista = new List<TipoProductoTerminadoViewModel>();
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    foreach (var item in ctx.TipoProductoTerminadoes)
                    {
                        var tipoProductoTerminado = new TipoProductoTerminadoViewModel();
                        tipoProductoTerminado.id = item.id;
                        tipoProductoTerminado.nombre = item.nombre;
                        tipoProductoTerminado.alias = item.alias;
                        tipoProductoTerminado.fechaRegistro = item.fechaRegistro;
                        tipoProductoTerminado.horaRegistro = item.horaRegistro.ToString();
                        tipoProductoTerminado.usuarioRegistro = item.usuarioRegistro;
                        tipoProductoTerminado.estatus = item.estatus;
                        lista.Add(tipoProductoTerminado);
                    }
                }
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetProductoTipoAlmacenamientos()
        {
            var lista = new List<ProductoTipoAlmacenamientoViewModel>();
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    foreach (var item in ctx.ProductoTipoAlmacenamientoes)
                    {
                        var tipoAlmacenamientoProducto = new ProductoTipoAlmacenamientoViewModel();
                        tipoAlmacenamientoProducto.id = item.id;
                        tipoAlmacenamientoProducto.idTipoProductoTerminado = item.idTipoProductoTerminado;
                        tipoAlmacenamientoProducto.idTipoAlmacenamiento = item.idTipoAlmacenamiento;
                        tipoAlmacenamientoProducto.identificador = item.identificador;
                        tipoAlmacenamientoProducto.fechaRegistro = item.fechaRegistro;
                        if (item.horaRegistro != null)
                        {
                            tipoAlmacenamientoProducto.horaRegistro = item.horaRegistro.ToString();
                        }
                        tipoAlmacenamientoProducto.usuarioRegistro = item.usuarioRegistro;
                        tipoAlmacenamientoProducto.estatus = item.estatus;
                        lista.Add(tipoAlmacenamientoProducto);
                    }
                }
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// Método que valida el estatus de las bandejas
        /// </summary>
        /// <param name="estatusBandejaVM">Listado de Estatus de Bandejas</param>
        /// <returns></returns>

        [System.Web.Mvc.HttpPost]
        [WebMethod]
        public JsonResult CrudEstatusBandeja(List<EstatusBandejaViewModel> estatusBandejaVM)
        {
            try
            {
                using (var context = new ControlConsumoEntities())
                {
                    List<EstatusBandeja> estatusBandejas = estatusBandejaVM.Select<EstatusBandejaViewModel, EstatusBandeja>(x => x).ToList();
                    context.EstatusBandejas.AddOrUpdate(estatusBandejas.ToArray());
                    context.SaveChanges();
                    var mensaje = new EstatusBandejaResponseModel { Mensaje = "La Bandeja ha sido Actualizada", Validada = true };
                    return Json(mensaje);
                }
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = ex};
            }
        }

        /// <summary>
        /// Metodo que retorna los registros de la tabla EstatusBandeja
        /// </summary>
        /// <returns>Lista en formato json con los registros de EstatusBandeja</returns>

        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetEstatusBandejas()
        {
            var lista = new List<EstatusBandejaViewModel>();
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    foreach (var item in ctx.EstatusBandejas)
                    {
                        lista.Add(item);
                    }
                }
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        /// <summary>
        /// Metodo que retorna registro especifico de la tabla EstatusBandeja
        /// </summary>
        /// <returns>Registro Estatus Bandeja</returns>

        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetEstatusBandeja(string idBandeja, int secuenciaBandeja, string idTiempo = null)
        {
            var estatusBandeja = new EstatusBandejaViewModel();
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    var existe = false;
                    TiempoBandeja tiempoBandeja = new TiempoBandeja();
                    if (!String.IsNullOrEmpty(idTiempo))
                    {
                        tiempoBandeja = ctx.TiempoBandejas.Where(t => t.idBandeja == idBandeja && t.idTiempo == idTiempo).SingleOrDefault();
                        if (tiempoBandeja == null)
                        {
                            return Json(null, JsonRequestBehavior.AllowGet);
                        }
                    }

                    if (ctx.EstatusBandejas
                    .Where(s => s.idBandeja.Equals(idBandeja, StringComparison.InvariantCultureIgnoreCase))
                    .Where(s => s.secuenciaBandeja == secuenciaBandeja).SingleOrDefault() != null)
                    {
                        existe = true;
                        estatusBandeja = ctx.EstatusBandejas
                        .Where(s => s.idBandeja.Equals(idBandeja, StringComparison.InvariantCultureIgnoreCase))
                        .Where(s => s.secuenciaBandeja == secuenciaBandeja).SingleOrDefault();
                        if (tiempoBandeja.cantidad != 0)
                        {
                            estatusBandeja.meins = tiempoBandeja.unidad;
                            estatusBandeja.menge = tiempoBandeja.cantidad;
                        }

                    }
                    if (!existe)
                    {
                        var estatusBandejaNew = new EstatusBandeja();
                        estatusBandejaNew.idBandeja = idBandeja;
                        estatusBandejaNew.secuenciaBandeja = secuenciaBandeja;
                        estatusBandejaNew.fechaRegistro = DateTime.Now;
                        estatusBandejaNew.horaRegistro = DateTime.Now.TimeOfDay;
                        estatusBandejaNew.estatus = "VA";
                        estatusBandejaNew.idTiempo = idTiempo;
                        ctx.EstatusBandejas.Add(estatusBandejaNew);
                        ctx.SaveChanges();
                        estatusBandejaNew.UMP = tiempoBandeja.unidad;
                        estatusBandejaNew.cantidad = tiempoBandeja.cantidad;
                        estatusBandeja = estatusBandejaNew;
                    }
                }
                return Json(estatusBandeja, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                estatusBandeja = null;
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// Metodo que retorna los registros de la tabla Bandeja
        /// </summary>
        /// <returns>Lista en formato json con los registros de la tabla Bandeja</returns>
        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetConfiguracionBandejas()
        {
            var configuracionesBandejas = new List<BandejaViewModel>();
            try
            {
                using (var context = new ControlConsumoEntities())
                {
                    foreach (var item in context.Bandejas)
                    {
                        configuracionesBandejas.Add(item);
                    }
                }
                return Json(configuracionesBandejas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        /// <summary>
        /// Metodo que retorna los registros de la tabla TiempoBandeja
        /// </summary>
        /// <returns>Lista en formato json con los registros de la tabla TiempoBandeja</returns>
        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetTiemposBandejas()
        {
            var tiemposBandejas = new List<TiempoBandejaViewModel>();
            try
            {
                using (var context = new ControlConsumoEntities())
                {
                    foreach (var item in context.TiempoBandejas)
                    {
                        tiemposBandejas.Add(item);
                    }
                }
                return Json(tiemposBandejas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetConsumos(String fechaFinal, String idEquipo, int turno = 0)
        {
            var fecha = DateTime.ParseExact(fechaFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
            var listaConsumos = new List<ConsumoModel>();
            idEquipo = idEquipo.Trim();
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    if (turno == 0)
                    {
                        foreach (var item in ctx.Consumoes.Where(s => s.IdEquipo.Equals(idEquipo, StringComparison.InvariantCultureIgnoreCase))
                            .Where(s => s.FechaProduccion >= fecha))
                        {
                            listaConsumos.Add(item);
                        }
                    }
                    else if (turno != 0)
                    {
                        foreach (var item in ctx.Consumoes.Where(s => s.IdEquipo.Equals(idEquipo, StringComparison.InvariantCultureIgnoreCase))
                            .Where(s => s.FechaProduccion >= fecha)
                            .Where(s => s.Turno == turno))
                        {
                            listaConsumos.Add(item);
                        }
                    }
                }
                return new JsonResult() { Data = listaConsumos, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = int.MaxValue };
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetSalidas(String fechaFinal, String idEquipo, int turno = 0)
        {
            var fecha = DateTime.ParseExact(fechaFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
            var listaSalidas = new List<SalidaModel>();
            idEquipo = idEquipo.Trim();
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    if (turno == 0)
                    {
                        foreach (var item in ctx.SalidaProductoes.Where(s => s.FechaProduccion >= fecha)
                            .Where(s => s.IdEquipo.Equals(idEquipo, StringComparison.InvariantCultureIgnoreCase)))

                        {
                            listaSalidas.Add(item);
                        }
                    }
                    else if (turno != 0)
                    {
                        foreach (var item in ctx.SalidaProductoes.Where(s => s.IdEquipo.Equals(idEquipo, StringComparison.InvariantCultureIgnoreCase))
                            .Where(s => s.FechaProduccion >= fecha)
                            .Where(s => s.Turno == turno))
                        {
                            listaSalidas.Add(item);
                        }
                    }
                }

                return new JsonResult() { Data = listaSalidas, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = int.MaxValue };
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }
        }


        [System.Web.Mvc.HttpGet]
        [WebMethod]
        public JsonResult GetConfiguracionInicialControlSalidas(String idEquipo="")
        {
            var listaConfiguracionInicialControlSalidas = new List<ConfiguracionInicialControlSalidasModel>();
            try
            {
                idEquipo = idEquipo.Trim();
                using (var ctx = new ControlConsumoEntities())
                {
                    if (String.IsNullOrEmpty(idEquipo))
                    {
                        foreach (var item in ctx.ConfiguracionInicialControlSalidas)
                        {
                            listaConfiguracionInicialControlSalidas.Add(item);
                        }

                    }
                    else 
                    {
                        foreach (var item in ctx.ConfiguracionInicialControlSalidas
                                   .Where(s => s.IdEquipo.Equals(idEquipo, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            listaConfiguracionInicialControlSalidas.Add(item);
                        }
                    }
                }
                return Json(listaConfiguracionInicialControlSalidas, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e, JsonRequestBehavior.AllowGet);
            }
        }

        [System.Web.Http.HttpGet]
        [WebMethod]
        public JsonResult GetCantidadBandejasConsumidas(String idEquipo, string fechaProduccion, int turno, string idProducto="")
        {
            var cantidadBandejasConsumidas = 0;
            var fechaMaxima = DateTime.ParseExact(fechaProduccion, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;

            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    var listaConsumos = new List<Consumo>();
                    if (String.IsNullOrEmpty(idProducto))
                    {
                        listaConsumos = (from consumo in ctx.Consumoes
                                         where consumo.IdEquipo.Equals(idEquipo, StringComparison.InvariantCultureIgnoreCase)
                                         && consumo.FechaProduccion >= fechaMaxima && consumo.Turno == turno
                                         && consumo.IdBandeja != null
                                         select consumo).ToList();
                    }
                    else
                    {
                        listaConsumos = (from consumo in ctx.Consumoes
                                         where consumo.IdEquipo.Equals(idEquipo, StringComparison.InvariantCultureIgnoreCase)
                                         && consumo.FechaProduccion >= fechaMaxima && consumo.Turno == turno
                                         && consumo.IdBandeja != null
                                         && consumo.IdProducto.Equals(idProducto, StringComparison.InvariantCultureIgnoreCase)
                                         select consumo).ToList();
                    }

                    cantidadBandejasConsumidas = listaConsumos.Count();
                }
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return Json(cantidadBandejasConsumidas, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Http.HttpGet]
        [WebMethod]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult GetSumatoriaConsumoProductos(string idEquipo, string idProducto, string fechaProduccion, int turno)
        {
            var SumatoriaConsumos = 0.0;
            var fechaMaxima = DateTime.ParseExact(fechaProduccion, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;

            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    var listaConsumos =
                        (from consumo in ctx.Consumoes
                         where consumo.IdEquipo.Equals(idEquipo, StringComparison.InvariantCultureIgnoreCase)
                         && consumo.IdProducto.Equals(idProducto, StringComparison.InvariantCultureIgnoreCase)
                         && consumo.IdBandeja != null
                         && consumo.FechaProduccion >= fechaMaxima && consumo.Turno == turno
                         && consumo.Unidad.Equals("TH", StringComparison.InvariantCultureIgnoreCase)
                         select consumo).ToList();

                    SumatoriaConsumos = listaConsumos.Sum(s => s.Cantidad);
                }
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e , JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }
            return Json(SumatoriaConsumos, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Http.HttpPost]
        public JsonResult PostConfiguracionInicialControlSalidas(List<ConfiguracionInicialControlSalidasModel> configuracionInicialControlSalidas)
        {
            try
            {
                using(var ctx = new ControlConsumoEntities())
                {
                    foreach(var item in configuracionInicialControlSalidas)
                    {
                        var existe = ctx.ConfiguracionInicialControlSalidas.Any(s => s.IdProducto ==item.IdProducto
                        && s.IdEquipo==item.IdEquipo);
                        
                        if (!existe)
                        {
                            ctx.ConfiguracionInicialControlSalidas.Add(
                                new ConfiguracionInicialControlSalida
                                {
                                    IdProducto = item.IdProducto,
                                    IdEquipo = item.IdEquipo,
                                    FechaProduccion = item.FechaProduccion,
                                    Turno = item.Turno,
                                    CantidadConsumoPendiente = item.CantidadConsumoPendiente,
                                    Unidad = item.Unidad,
                                    FechaRegistro = DateTime.Now,
                                    FechaLectura = item.FechaLectura,
                                    UsuarioRegistro = item.UsuarioRegistro
                                }
                            );
                        }
                    }
                    ctx.SaveChanges();
                }
                return new JsonResult() { Data = new { Type = 'S' } };
            }
            catch(Exception e)
            {
                return new JsonResult() { Data = e };
            }
        }
        /// <summary>
        /// Metodo para recibir las entradas de material de Control Consumo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public JsonResult PostConsumo(List<ConsumoModel> request)
        {
            try
            {
                List<ConsumoModel> listaConsumos = request.GroupBy (s => new
                {
                    s.IDPROCESS,
                    s.WERKS,
                    s.IDEQUIPO,
                    s.IDTIEMPO,
                    s.MATNR,
                    s.VERID,
                    s.SECENTRADA,
                    s.FECHA,
                    s.HORA,
                    s.IDTURNO,
                    s.MATNR2,
                    s.USNAM
                })                   
                .Select(s => s.First())
                .Distinct()
                .ToList();
                TransaccionManager.SaveConsumos(listaConsumos);
                return new JsonResult() { Data = new { Type = 'S' } };
            }
            catch(Exception e)
            {
                return new JsonResult() { Data = e };
            }
        }

        /// <summary>
        /// Metodo para recibir las salidas de Control Consumo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public JsonResult PostSalida(List<SalidaModel> request)
        {
            try
            {
                List<SalidaModel> listSalidas = request.GroupBy(s => new
                {
                    s.IDPROCESS,
                    s.WERKS,
                    s.IDEQUIPO,
                    s.IDTIEMPO,
                    s.MATNR,
                    s.VERID,
                    s.SECSALIDA,
                    s.FECHA,
                    s.HORA,
                    s.IDTURNO,
                    s.BATCHID
                })
                .Select(s => s.First())
                .Distinct()
                .ToList();
                TransaccionManager.SaveSalidas(listSalidas);
                return new JsonResult() { Data = new { Type = 'S' } };
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e};
            }
        }

        /// <summary>
        /// Metodo para cargar las mezclas de Control Consumo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public JsonResult PostTracking(List<MezclaModel> request)
        {
            try
            {
                List<MezclaModel> listTracking = request.GroupBy(s => new
                {
                    s.IDPROCESS,
                    s.IDEQUIPO,
                    s.SECENTRADA,
                    s.ZENTRADADATE,
                    s.SECSALIDA,
                    s.ZSALIDADATE
                })
                .Select(s=>s.First())
                .Distinct()
                .ToList();
                TransaccionManager.SaveMezclas(listTracking);
                return new JsonResult() { Data = new { Type = 'S' } };
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e };
            }
        }

        /// <summary>
        /// Metodo para recibir las reimpresion de etiquetas de Control Consumo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public JsonResult PostReimpresionEtiquetas(List<ReimpresionEtiquetasModel> request)
        {
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    foreach (var item in request)
                    {
                        var nuevaImpresionEtiquetas = new HistorialReimpresionEtiqueta();
                        nuevaImpresionEtiquetas.IdEquipo = item.IdEquipo;
                        nuevaImpresionEtiquetas.SecuenciaEtiqueta = item.SecuenciaEtiqueta;
                        nuevaImpresionEtiquetas.IdMotivoReimpresion = item.IdMotivoReimpresionEtiqueta;
                        nuevaImpresionEtiquetas.Cantidad = item.Cantidad;
                        nuevaImpresionEtiquetas.Empaque = item.Empaque;
                        nuevaImpresionEtiquetas.AlmFiller = String.Empty;
                        if (!String.IsNullOrEmpty(item.AlmFiller)){
                            nuevaImpresionEtiquetas.AlmFiller = item.AlmFiller;
                        }
                        nuevaImpresionEtiquetas.FechaProduccion = item.FechaProduccion;
                        nuevaImpresionEtiquetas.Turno = item.Turno;
                        nuevaImpresionEtiquetas.FechaReimpresion = item.FechaReimpresion;
                        nuevaImpresionEtiquetas.UsuarioReimpresion = item.UsuarioReimpresion;
                        nuevaImpresionEtiquetas.Estatus = true;
                        ctx.HistorialReimpresionEtiquetas.Add(nuevaImpresionEtiquetas);
                        ctx.SaveChanges();
                    }
                }
                return new JsonResult() { Data = new { Type = 'S' } };
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e };
            }
        }

        /// <summary>
        /// Metodo para recibir las reimpresion de etiquetas de Control Consumo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public JsonResult GetHistorialReimpresionEtiquetas()
        {

            var listaReimpresionesEtiquetas = new List<ReimpresionEtiquetasModel>();
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    var fechaMinima = DateTime.Now.AddDays(-10);
                    foreach (var item in ctx.HistorialReimpresionEtiquetas.Where(s => s.FechaProduccion >= fechaMinima))
                    {
                        listaReimpresionesEtiquetas.Add(item);
                    }
                    return Json(listaReimpresionesEtiquetas, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e };
            }
        }

        /// <summary>
        /// Metodo para enviar los motivos de etiquetas de Control Consumo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public JsonResult GetMotivosReimpresionEtiquetas()
        {

            var listaMotivosReimpresionEtiquetas = new List<MotivoReimpresionEtiquetaModel>();
            try
            {
                using (var ctx = new ControlConsumoEntities())
                {
                    foreach(var item in ctx.MotivoReimpresionEtiquetas)
                    {
                        listaMotivosReimpresionEtiquetas.Add(item);
                    }
                    return Json(listaMotivosReimpresionEtiquetas, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return new JsonResult() { Data = e };
            }
        }

    }
}
