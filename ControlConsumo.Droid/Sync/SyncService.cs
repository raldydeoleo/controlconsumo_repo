using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using ControlConsumo.Droid.Managers;
using System.Threading.Tasks;
using System.Threading;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Shared.Models.Z;
using Android.Net;
using ControlConsumo.Shared.Models.Process;
using ControlConsumo.Shared.Models.Config;
using Java.Util.Concurrent.Locks;
using SyncResult = ControlConsumo.Shared.Models.Z.SyncResult;
using SyncRequest = ControlConsumo.Shared.Models.Z.SyncRequest;
using Android.Preferences;
using ControlConsumo.Shared;
using static ControlConsumo.Shared.Tables.Syncro;

namespace ControlConsumo.Droid.Sync
{
    [Service]
    [IntentFilter(new String[] { "controlconsumo.droid.SyncService" })]
    public class SyncService : Service
    {

        private const Int32 ReleaseTime = 1800000;
        private ProcessList Proceso { get; set; }
        public ActualConfig actualConfig { get; set; }
        private RepositoryFactory _repo { get; set; }
        private RepositoryFactory repo /// Evitar que la Conexión este Nula y no lance errores de Nulo.
        {
            get
            {
                if (_repo.Connection == null || _repo.Connection == null || _repo.Connection.Connection == null)
                {
                    _repo = new RepositoryFactory(Util.GetConnection());
                }

                return _repo;
            }
        }
        private int Seconds { get; set; }
        public Boolean IsConnected { get; set; }
        // public Boolean ReleaseDataBaseConnection { get; set; }
        public Boolean awaitConnection { get; set; }
        public DisconectionsType DisconectionType { get; set; }
        private static ILock WriterLocker;
        private ServiceBinder binder;
        private Boolean ServiceBinded;

        public enum DisconectionsType
        {
            Connected,
            WifiOff,
            ServerDown
        }

        private ILock GetLocker
        {
            get
            {
                if (WriterLocker == null)
                {
                    var locker = new ReentrantReadWriteLock();
                    WriterLocker = locker.WriteLock();
                }

                return WriterLocker;
            }
        }

        public Boolean IsThereConnection
        {
            get
            {
                var cm = (ConnectivityManager)GetSystemService(Context.ConnectivityService);
                var netInfo = cm.ActiveNetworkInfo;
                return netInfo != null && netInfo.IsConnectedOrConnecting;
            }
        }

        public delegate void reviewWarnings();
        public event reviewWarnings ReviewWarnings;

        public delegate void returnConsumptions(List<Consumptions> Consumos);
        public event returnConsumptions ONReturnConsumtions;

        public delegate void MasterDataUpdated();
        public event MasterDataUpdated OnMasterDataUpdated;

        private void ShowNotification(String message)
        {
            /*
            var nMgr = (NotificationManager)GetSystemService(NotificationService);
            var notification = new Notification(Resource.Drawable.Icon, message);
            var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MainActivity)), 0);
            notification.SetLatestEventInfo(this, "Control Consumo Notificacion", message, pendingIntent);
            nMgr.Notify(0, notification);*/
        }

        public override void OnCreate()
        {
            base.OnCreate();
            _repo = new RepositoryFactory(Util.GetConnection());
            Util.ReleaseLock(this, Util.Locks.Servicio);
            AsyncHelper.RunSync(() => Util.SaveServiceLog("Servicio creado"));
        }

        public static Boolean IsRunning(Context ctx)
        {
            return Util.CheckLock(ctx, Util.Locks.Servicio);
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            Util.ReleaseLock(this, Util.Locks.Servicio);
            AsyncHelper.RunSync(() => Util.SaveServiceLog("Servicio Conectado"));

            ServiceBinded = true;
            binder = new ServiceBinder(this);

            Seconds = 25000;

            ThreadPool.QueueUserWorkItem(o => StartSync());

            return binder;
        }

        public override bool OnUnbind(Intent intent)
        {
            ServiceBinded = false;
            AsyncHelper.RunSync(() => Util.SaveServiceLog("Servicio Desconectado"));
            Util.ReleaseLock(this, Util.Locks.Servicio);
            return base.OnUnbind(intent);
        }

        public override void OnDestroy()
        {
            Util.ReleaseLock(this, Util.Locks.Servicio);
            AsyncHelper.RunSync(() => Util.SaveServiceLog("Servicio Destruido"));
            base.OnDestroy();
        }

        public override void OnRebind(Intent intent)
        {
            if (binder == null || !IsRunning(this))
            {
                Util.ReleaseLock(this, Util.Locks.Servicio);
                AsyncHelper.RunSync(() => Util.SaveServiceLog("Servicio conectado nuevamente"));

                binder = new ServiceBinder(this);

                Seconds = 25000;

                ThreadPool.QueueUserWorkItem(o => StartSync());
            }

            base.OnRebind(intent);
        }

        public String GetText()
        {
            return "Some text";
        }

        private async void StartSync()
        {
            var repoz = repo.GetRepositoryZ();

            await Task.Delay(1000);

            ThreadPool.QueueUserWorkItem(t => { ExecuteDailyUpdate(); });/// Para evitar que coincidan con el proceso de sincronizacion standard
        
            await Util.SaveServiceLog("Inicio Rutina Sincronización");

            IniciarBucle:

            do
            {
                var hasError = false;

                try
                {
                    Proceso = await repo.GetRepositoryZ().GetProces();

                    repoz.InitMonitor();

                    //if (awaitConnection) ///Si esta descargado los datos de maquina lo detengo para evitar multiples request
                    //{
                    //    await Task.Delay(1000);
                    //    goto IniciarBucle;
                    //}

                    try
                    {
                        await ExecutePendingJobs(); ;
                    }
                    catch (Exception e)
                    {
                        await Util.SaveException(e, "Sincronización de trabajos pendientes.");
                    }

                    try
                    {
                        ThreadPool.QueueUserWorkItem(t => { ReviewWarnings?.Invoke(); });
                    }
                    catch (Exception)
                    { }

                    if (actualConfig == null || String.IsNullOrEmpty(actualConfig.SubEquipmentID)) /// Si son dos priorizo los procesos de ambos
                    {
                        await Task.Delay(Seconds);

                        if (IsThereConnection)
                        {
                            IsConnected = true;
                            if (!String.IsNullOrEmpty(Proceso.EquipmentID))
                                PostData();

                            ExecuteUpdate();
                        }
                        else
                        {
                            DisconectionType = DisconectionsType.WifiOff;
                            IsConnected = false;
                        }
                    }
                    else
                    {
                        await Task.Delay(Seconds / 2);

                        if (IsThereConnection)
                        {
                            IsConnected = true;

                            #region Prioridad a la planificacion de ambas maquinas

                            SyncTwoEquipment(Proceso);

                            #endregion

                            //await Task.Delay(Seconds / 2);

                            PostData();
                            ExecuteUpdate();
                        }
                        else
                        {
                            DisconectionType = DisconectionsType.WifiOff;
                            IsConnected = false;
                        }
                    }

                    try
                    {
                        ThreadPool.QueueUserWorkItem(t => { ReviewWarnings?.Invoke(); });
                    }
                    catch (Exception)
                    { }
                }
                catch (Exception ex)
                {
                    hasError = true;
                    await Util.SaveException(ex);
                }
                finally
                {
                    try
                    {
                        GC.Collect();
                    }
                    catch (Exception)
                    { }
                }

                if (hasError)
                    goto IniciarBucle;

            } while (ServiceBinded);
        }

        private void PostData()
        {
            var repoz = repo.GetRepositoryZ();
            var repoConfiguracionSincronizacionTablas = repo.GetRepositoryConfiguracionSincronizacionTablas();
            var repoEntrada = repo.GetRepositoryConsumptions();
            var repoSalida = repo.GetRepositoryElaborates();
            var repotracking = repo.GetRepositoryTracking();
            var reporelease = repo.GetRepositoryTraysRelease();
            var repoerror = repo.GetRepositoryErrors();
            var repoInv = repo.GetRepositoryInventories();
            var repotra = repo.GetRepositoryTransactions();
            var repoLabelPrintingLogs = repo.GetRepositoryLabelPrintingLogs();

            try
            {
                var configuracionSincronizacionConsumos =
                    repoConfiguracionSincronizacionTablas
                    .GetAsyncByKey(Tables.Consumptions.ToString()).Result;

                if (configuracionSincronizacionConsumos != null)
                {
                    AsyncHelper.RunSync(() => repoEntrada.SyncAsync(configuracionSincronizacionConsumos.procesarSap));
                }
            }
            catch (Exception ex)
            {
                ManageExceptions("Cargando Entradas", ex);
            }

            try
            {
                var configuracionSincronizacionSalidas =
                    repoConfiguracionSincronizacionTablas
                    .GetAsyncByKey(Tables.Elaborates.ToString()).Result;

                if (configuracionSincronizacionSalidas != null)
                {
                    AsyncHelper.RunSync(() => repoSalida.SyncAsync(configuracionSincronizacionSalidas.procesarSap));
                    var monitor = repoz.GetSyncMonitor();
                    var salida = monitor.Detalle.FirstOrDefault(s => s.Tabla == Tables.Elaborates);

                    if (salida != null)
                        AsyncHelper.RunSync(() => Util.SaveSalidaLog(salida));
                }
            }
            catch (Exception ex)
            {
                ManageExceptions("Cargando Salidas", ex);
            }

            try
            {
                var configuracionSincronizacionTracking =
                    repoConfiguracionSincronizacionTablas
                    .GetAsyncByKey(Tables.Tracking.ToString()).Result;

                if (configuracionSincronizacionTracking != null)
                {
                    AsyncHelper.RunSync(() => repotracking.SyncAsync(configuracionSincronizacionTracking.procesarSap));
                }
            }
            catch (Exception ex)
            {
                ManageExceptions("Cargando Mezclas", ex);
            }

            try
            {
                AsyncHelper.RunSync(() => repoInv.SyncAsync(true));
            }
            catch (Exception ex)
            {
                ManageExceptions("Cargando Inventario", ex);
            }

            try
            {
                AsyncHelper.RunSync(() => repotra.SyncAsync(true));
            }
            catch (Exception ex)
            {
                ManageExceptions("Cargando Movimientos de Inventario", ex);
            }

            try
            {
                AsyncHelper.RunSync(() => reporelease.SyncAsync(true));
            }
            catch (Exception ex)
            {
                ManageExceptions("Cargando Historico de Liberacion Bandejas", ex);
            }

            try
            {
                AsyncHelper.RunSync(() => repoerror.SyncAsync(true));
            }
            catch (Exception ex)
            {
                ManageExceptions("Cargando Registros de Errores", ex);
            }

            try
            {
                AsyncHelper.RunSync(() => repoLabelPrintingLogs.SyncAsync(false));
            }
            catch (Exception ex)
            {
                ManageExceptions("Cargando Registros de historial de reimpresión de etiquetas", ex);
            }
        }

        private async Task<List<SyncResult>> ValidTables(params Syncro.Tables[] Tables)
        {
            var repoSyncro = repo.GetRepositorySyncro();
            var repoz = repo.GetRepositoryZ();

            var allSyncro = await repoSyncro.GetAsyncAll();

            var tables = allSyncro
               .Where(p => Tables.Contains(p.Tabla))
               .Select(p => new SyncRequest
               {
                   Sync = p.Sync,
                   TABLE = p.Tabla.ToString(),
                   CPUDT = p.LastSync.GetSapDateL(),
                   CPUTM = p.LastSync.GetSapHoraL()
               }).ToList();

            if (!tables.Any()) return new List<ControlConsumo.Shared.Models.Z.SyncResult>();

            return await repoz.ValidateTables(tables);
        }

        public async void ExecuteDailyUpdate()
        {
            var listalog = new List<SyncLogRequest>();
            var repoz = repo.GetRepositoryZ();
            var process = repoz.GetProcesSync();
            var pasoDado = 1;
            var totalPasos = 0;

            try
            {
                if (IsThereConnection)
                {
                    var result = AsyncHelper.RunSync(() => ValidTables(Syncro.Tables.Lots, Syncro.Tables.Materials));
                    result.Add(new SyncResult
                    {
                        TABLE = "ConfigMaterials",
                        CPUDT = DateTime.Now.ToString("yyyyMMdd"),
                        CPUTM = DateTime.Now.ToString("HHmmss")
                    });

                    totalPasos = result.Count + 3;

                    var inicio = new SyncLogRequest
                    {
                        IDEQUIPO = process.EquipmentID,
                        IDPROCESS = process.Process,
                        WERKS = process.Centro,
                        CPUDT = DateTime.Now.GetSapDate(),
                        STYPE = SyncLogRequest.SType.D,
                        PASO = pasoDado,
                        PASOMAX = totalPasos,
                        MESSAGE = "Inicio de Sincronizacion de Lotes y Materiales"
                    };

                    listalog.Add(inicio);

                    foreach (var item in result.OrderByDescending(p => p.TABLE))
                    {
                        pasoDado++;
                        switch (item.TABLE)
                        {
                            case "Lots":

                                var log = new SyncLogRequest
                                {
                                    IDEQUIPO = process.EquipmentID,
                                    IDPROCESS = process.Process,
                                    WERKS = process.Centro,
                                    CPUDT = DateTime.Now.GetSapDate(),
                                    STYPE = SyncLogRequest.SType.D,
                                    PASO = pasoDado,
                                    PASOMAX = totalPasos,
                                    MESSAGE = "Actualizacion de Lotes Completada"
                                };

                                try
                                {
                                    var repolote = repo.GetRepositoryLots();
                                    AsyncHelper.RunSync(() => repolote.SyncAsync(true));
                                    listalog.Add(log);
                                }
                                catch (Exception ex)
                                {
                                    await Util.SaveException(ex, "Descargando Lotes", true);
                                    log.MESSAGE = "Error Actualizando Lotes";
                                    log.ERROR = "X";
                                    listalog.Add(log);
                                }

                                break;

                            case "Materials":

                                log = new SyncLogRequest
                                {
                                    IDEQUIPO = process.EquipmentID,
                                    IDPROCESS = process.Process,
                                    WERKS = process.Centro,
                                    CPUDT = DateTime.Now.GetSapDate(),
                                    STYPE = SyncLogRequest.SType.D,
                                    PASO = pasoDado,
                                    PASOMAX = totalPasos,
                                    MESSAGE = "Actualizacion de Materiales Completada"
                                };

                                try
                                {
                                    var repomaterial = repo.GetRepositoryMaterials();
                                    AsyncHelper.RunSync(() => repomaterial.SyncAsync(true));
                                    listalog.Add(log);
                                }
                                catch (Exception ex)
                                {
                                    await Util.SaveException(ex, "Descargando Materiales", true);
                                    log.MESSAGE = "Error Actualizando Materiales";
                                    log.ERROR = "X";
                                    listalog.Add(log);
                                }

                                break;

                            case "ConfigMaterials":

                                log = new SyncLogRequest
                                {
                                    IDEQUIPO = process.EquipmentID,
                                    IDPROCESS = process.Process,
                                    WERKS = process.Centro,
                                    CPUDT = DateTime.Now.GetSapDate(),
                                    STYPE = SyncLogRequest.SType.D,
                                    PASO = pasoDado,
                                    PASOMAX = totalPasos,
                                    MESSAGE = "Actualizacion de BOM Completada"
                                };

                                try
                                {
                                    var repoConfMaterial = repo.GetRepositoryConfigMaterials();
                                    AsyncHelper.RunSync(() => repoConfMaterial.SyncAsync(true));
                                    listalog.Add(log);
                                }
                                catch (Exception ex)
                                {
                                    await Util.SaveException(ex, "Descargando BOM", true);
                                    log.MESSAGE = "Error Actualizando BOM";
                                    log.ERROR = "X";
                                    listalog.Add(log);
                                }

                                break;
                        }
                    }

                    #region Tablas no Muy Transaccionales

                    Task.Delay(10000).Wait();

                    var repoEquipos = repo.GetRepositoryEquipments();
                    var repoConfiguracionSincronizacion = repo.GetRepositoryConfiguracionSincronizacionTablas();
                    var repoConfiguracionInicialControlSalidas = repo.GetRepositoryConfiguracionInicialControlSalidas();
                    var repotimes = repo.GetRepositoryTimes();
                    var repomaterialprocess = repo.GetRepositoryMaterialsProcess();
                    var repomaterialz = repo.GetRepositoryMaterialZilm();
                    var repoConfiguracionTiempoSalida = repo.GetRepositoryConfiguracionTiempoSalidas();
                    var repoTipoAlmacenamientoProducto = repo.GetRepositoryTipoAlmacenamientoProductos();
                    var repoTipoProductoTerminado = repo.GetRepositoryTipoProductoTerminados();
                    var repoProductoTipoAlmacenamiento = repo.GetRepositoryProductoTipoAlmacenamientos();
                    var repoTrays = repo.GetRepositoryTrays();
                    var repoTraysTimes = repo.GetRepositoryTraysTimes();
                    var repoLabelPrintingReasons = repo.GetRepositoryLabelPrintingReasons();
                    var repoLabelPrintingLogs = repo.GetRepositoryLabelPrintingLogs();

                    try
                    {
                        AsyncHelper.RunSync(() => repoEquipos.SyncAsyncAll(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando equipos", ex);
                    }

                    try
                    {
                        AsyncHelper.RunSync(() => repoConfiguracionSincronizacion.SyncAsync(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando configuración de sincronización de tablas", ex);
                    }

                    try
                    {
                        AsyncHelper.RunSync(() => repomaterialprocess.SyncAsync(true));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Relacion de Bandejas con los Tiempos ", ex);
                    }

                    try
                    {
                        AsyncHelper.RunSync(() => repotimes.SyncAsync(true));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de Tiempos", ex);
                    }

                    try
                    {
                        AsyncHelper.RunSync(() => repomaterialz.SyncAsync(true));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de Configuraciones de tabla Z", ex);
                    }

                    try
                    {
                        AsyncHelper.RunSync(() => repoConfiguracionTiempoSalida.SyncAsync(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de Configuración Tiempo de Salida", ex);
                    }

                    try
                    {
                        AsyncHelper.RunSync(() => repoTipoAlmacenamientoProducto.SyncAsync(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de tipos de almacenamiento de productos", ex);
                    }
                    try
                    {
                        AsyncHelper.RunSync(() => repoTipoProductoTerminado.SyncAsync(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de tipos de producto terminado", ex);
                    }
                    try
                    {
                        AsyncHelper.RunSync(() => repoProductoTipoAlmacenamiento.SyncAsync(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de productos por tipo de almacenamiento", ex);
                    }
                    try
                    {
                        AsyncHelper.RunSync(() => repoTrays.SyncAsync(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de configuración de bandejas", ex);
                    }
                    try
                    {
                        AsyncHelper.RunSync(() => repoTraysTimes.SyncAsync(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de configuración entre Tiempos y Bandejas", ex);
                    }

                    try
                    {
                        AsyncHelper.RunSync(() => repoConfiguracionInicialControlSalidas.SyncAsync(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de configuración inicial de control de salidas", ex);
                    }
                    try
                    {
                        AsyncHelper.RunSync(() => repoLabelPrintingReasons.SyncAsyncAll(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de motivos de reimpresión de etiquetas", ex);
                    }

                    try
                    {
                        AsyncHelper.RunSync(() => repoLabelPrintingLogs.SyncAsyncAll(false));
                    }
                    catch (Exception ex)
                    {
                        ManageExceptions("Descargando Registros de historial de reimpresión de etiquetas", ex);
                    }

                    #endregion

                    ///Envio a refrescar el caching.
                    if (OnMasterDataUpdated != null) OnMasterDataUpdated.Invoke();
                }
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex);
            }
            finally
            {
                pasoDado++;

                var fin = new SyncLogRequest
                {
                    IDEQUIPO = process.EquipmentID,
                    IDPROCESS = process.Process,
                    WERKS = process.Centro,
                    CPUDT = DateTime.Now.GetSapDate(),
                    STYPE = SyncLogRequest.SType.D,
                    PASO = totalPasos,
                    PASOMAX = totalPasos,
                    MESSAGE = listalog.Any(p => !String.IsNullOrEmpty(p.ERROR)) ? "Sincronizacion Termino con Error" : "Fin de Sincronizacion de Lotes y Materiales"
                };

                listalog.Add(fin);
            }

            for (int i = 0; i < listalog.Count(); i++)
            {
                listalog[i].CPUTM = DateTime.Now.AddSeconds(i).GetSapHora();
            }

            if (IsThereConnection)
            {
                AsyncHelper.RunSync(() => repoz.PostSyncLog(listalog));
            }
        }

        private void ExecuteUpdate()
        {
            try
            {
                var reposetting = repo.GetRepositorySettings();
                var repoSyncro = repo.GetRepositorySyncro();
                var repoWaste = repo.GetRepositoryWastes();
                var repoz = repo.GetRepositoryZ();

                List<SyncResult> result = new List<SyncResult>();
                        
                try
                {
                    var repoRols = repo.GetRepositoryRols();
                    AsyncHelper.RunSync(() => repoRols.SyncAsync(true));
                }
                catch (Exception ex)
                {
                    ManageExceptions("Sincronizando Roles", ex);
                }

                try
                {
                    var repoRolsPermit = repo.GetRepositoryRolsPermits();
                    AsyncHelper.RunSync(() => repoRolsPermit.SyncAsync(true));
                }
                catch (Exception ex)
                {
                    ManageExceptions("Sincronizando Permisos de Roles", ex);
                }

                try
                {
                    var repoUsers = repo.GetRepositoryUsers();
                    AsyncHelper.RunSync(() => repoUsers.SyncAsyncTwoWay());
                }
                catch (Exception ex)
                {
                    ManageExceptions("Sincronizando Usuarios", ex);
                }

                try
                {
                    var repoRoutes = repo.GetRepositoryProductsRoutes();
                    AsyncHelper.RunSync(() => repoRoutes.SyncAsyncTwoWay());
                }
                catch (Exception ex)
                {
                    ManageExceptions("Sincronizando de traza ", ex);
                }

                try
                {
                    result = AsyncHelper.RunSync(() => ValidTables(Syncro.Tables.ProductsRoutes, Syncro.Tables.Areas, Syncro.Tables.Configs, Syncro.Tables.Rols, Syncro.Tables.Users, Syncro.Tables.TraysProducts, Syncro.Tables.Stocks));
                }
                catch (Exception ex)
                {
                    ManageExceptions("Validando Traza, Areas, Planificaciones, Roles, Usuarios, Bandejas y Cuadres.", ex);
                }

                try /// Se saco del bloque anterior ahora debe cargar sin equipo asignado
                {
                    AsyncHelper.RunSync(() => repoWaste.SyncAsync(true));
                }
                catch (Exception ex)
                {
                    ManageExceptions("Cargando Desperdicios", ex);
                }

                foreach (var item in result)
                {
                    switch (item.TABLE)
                    {                        
                        case "Areas":

                            try
                            {
                                var repoAreas = repo.GetRepositoryAreas();
                                AsyncHelper.RunSync(() => repoAreas.SyncAsync(true));
                            }
                            catch (Exception ex)
                            {
                                ManageExceptions("Sincronizando de las Areas", ex);
                            }

                            break;

                        case "Configs":

                            try
                            {
                                var repoConfig = repo.GetRepositoryConfigs();
                                AsyncHelper.RunSync(() => repoConfig.SyncAsyncTwoWay());
                            }
                            catch (Exception ex)
                            {
                                ManageExceptions("Sincronizando Planificaciones", ex);
                            }

                            break;

                        case "Trays":

                            try
                            {
                                var repoTrays = repo.GetRepositoryTrays();
                                AsyncHelper.RunSync(() => repoTrays.SyncAsyncAll(false));
                            }
                            catch (Exception ex)
                            {
                                ManageExceptions("Sincronizando Configuración de Bandejas", ex);
                            }

                            break;

                        case "TraysProducts":

                            try
                            {
                                var repoTrayProduct = repo.GetRepositoryTraysProducts();
                                AsyncHelper.RunSync(() => repoTrayProduct.SyncAsyncTwoWay());
                            }
                            catch (Exception ex)
                            {
                                ManageExceptions("Sincronizando Bandejas", ex);
                            }

                            break;

                        case "TraysTimes":

                            try
                            {
                                var repoTraysTimes = repo.GetRepositoryTraysTimes();
                                AsyncHelper.RunSync(() => repoTraysTimes.SyncAsyncAll(false));
                            }
                            catch (Exception ex)
                            {
                                ManageExceptions("Sincronizando Configuración de Tiempos-Bandejas", ex);
                            }

                            break;

                        case "Stocks":

                            try
                            {
                                var repoStock = repo.GetRepositoryStocks();
                                AsyncHelper.RunSync(() => repoStock.SyncAsyncTwoWay());
                            }
                            catch (Exception ex)
                            {
                                ManageExceptions("Sincronizando Cuadres", ex);
                            }

                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DownloadConsumptions()
        {
            var repoz = repo.GetRepositoryZ();
            var Caching = new CachingManager(this);

            List<SyncResult> tables = new List<Shared.Models.Z.SyncResult>();

            try
            {
                tables = AsyncHelper.RunSync<List<SyncResult>>(() => ValidTables(Syncro.Tables.Consumptions));
            }
            catch (Exception ex)
            {
                ManageExceptions("Buscando Cambios de Entrada", ex);
            }

            if (!tables.Any()) return;

            try
            {
                var consumos = AsyncHelper.RunSync<List<Consumptions>>(() =>
                repoz.SyncConsumptions(Caching.GetProductionDate(), Caching.TurnoID));

                if (ONReturnConsumtions != null)
                {
                    ONReturnConsumtions.Invoke(consumos);
                }
            }
            catch (Exception ex)
            {
                ManageExceptions("Descargando Registros de Entradas", ex);
            }
        }

        private void DownloadElaborates()
        {
            var repoElabo = repo.GetRepositoryElaborates();
            var repoz = repo.GetRepositoryZ();

            List<SyncResult> tables = new List<Shared.Models.Z.SyncResult>();

            try
            {
                tables = AsyncHelper.RunSync<List<SyncResult>>(() => ValidTables(Syncro.Tables.Elaborates));
            }
            catch (Exception ex)
            {
                ManageExceptions("Validando Cambios en Salidas", ex);
            }

            if (!tables.Any()) return;

            try
            {
                AsyncHelper.RunSync(() => repoElabo.SyncAsyncTwoWay());
            }
            catch (Exception ex)
            {
                ManageExceptions("Descargando registros de Salidas", ex);
            }
        }

        private void DownloadTraysStatus()
        {
            try
            {
                var repoTrays = repo.GetRepositoryTraysProducts();
                var repoz = repo.GetRepositoryZ();

                var tables = AsyncHelper.RunSync<List<SyncResult>>(() => ValidTables(Syncro.Tables.TraysProducts));

                if (!tables.Any()) return;

                AsyncHelper.RunSync(() => repoTrays.SyncAsyncTwoWay());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task ExecutePendingJobs()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                var result = await repoz.ExecutePendingJobs();

                if (result.HasWorked)
                {
                    await Util.SavePendingJobsResult(result);
                }
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex);
            }
        }

        private void SyncPlanification()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                var repoConfig = repo.GetRepositoryConfigs();

                var tables = AsyncHelper.RunSync<List<SyncResult>>(() => ValidTables(Syncro.Tables.Configs));

                if (!tables.Any()) return;

                AsyncHelper.RunSync(() => repoConfig.SyncAsyncTwoWay());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task RegisterSyncMonitor(SyncLogMonitor monitor)
        {
            await Util.SaveSyncLogMonitor(monitor);
        }

        /// <summary>
        /// Sincronizacion para los equipos en duo
        /// </summary>
        /// <param name="Proceso"></param>
        /// <returns></returns>
        private void SyncTwoEquipment(ProcessList Proceso)
        {
            var repoConfig = repo.GetRepositoryConfigs();
            var repoTrays = repo.GetRepositoryTraysProducts();
            var repoElaborate = repo.GetRepositoryElaborates();
            var repoz = repo.GetRepositoryZ();
            var Caching = new CachingManager(this);
            var tables = new List<SyncResult>();

            if (Proceso.IsSubEquipment)
            {
                try
                {
                    tables = AsyncHelper.RunSync<List<SyncResult>>(() => ValidTables(Syncro.Tables.Configs, Syncro.Tables.TraysProducts, Syncro.Tables.Consumptions));
                }
                catch (Exception ex)
                {
                    ManageExceptions("Validando Configuraciones, Bandejas y Consumo.", ex);
                }
                try
                {
                    var consumos = AsyncHelper.RunSync<List<Consumptions>>(() =>
                    repoz.SyncConsumptions(Caching.GetProductionDate(), Caching.TurnoID));

                    if (ONReturnConsumtions != null)
                    {
                        ONReturnConsumtions.Invoke(consumos);
                    }
                }
                catch (Exception ex)
                {
                    ManageExceptions("Descargando Registros de Consumo", ex);
                }
            }
            else
            {
                try
                {
                    tables = AsyncHelper.RunSync<List<SyncResult>>(() => ValidTables(Syncro.Tables.Configs, Syncro.Tables.TraysProducts, Syncro.Tables.Elaborates));
                }
                catch (Exception ex)
                {
                    ManageExceptions("Validando Configuraciones, Bandejas y Salidas.", ex);
                }
            }

            foreach (var item in tables)
            {
                switch (item.TABLE)
                {
                    case "Configs":
                        try
                        {
                            AsyncHelper.RunSync(() => repoConfig.SyncAsyncTwoWay());
                        }
                        catch (Exception ex)
                        {
                            ManageExceptions("Sincronizando Registros de Planificaciones.", ex);
                        }

                        break;

                    case "TraysProducts":
                        try
                        {
                            AsyncHelper.RunSync(() => repoTrays.SyncAsyncTwoWay());
                        }
                        catch (Exception ex)
                        {
                            ManageExceptions("Sincronizando Registros de Bandejas.", ex);
                        }

                        break;

                    /*case "Consumptions":
                        try
                        {
                            var consumos = AsyncHelper.RunSync<List<Consumptions>>(() => repoz.SyncConsumptions());

                            if (ONReturnConsumtions != null)
                            {
                                ONReturnConsumtions.Invoke(consumos);
                            }
                        }
                        catch (Exception ex)
                        {
                            ManageExceptions("Descargando Registros de Consumo", ex);
                        }

                        break;*/

                    case "Elaborates":
                        try
                        {
                            AsyncHelper.RunSync(() => repoElaborate.SyncAsyncTwoWay());
                        }
                        catch (Exception ex)
                        {
                            ManageExceptions("Descargando Registros de Salidas", ex);
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Metodo para manejar los errores
        /// </summary>
        /// <param name="SyncStep">Paso de la Sincronizacion</param>
        /// <param name="ex">Error</param>
        private async void ManageExceptions(String SyncStep, Exception ex)
        {
            if (ex is Java.Net.UnknownHostException || ex is Java.Net.NoRouteToHostException)
            {
                DisconectionType = DisconectionsType.ServerDown;
                IsConnected = false;
                await Util.SaveException(ex, SyncStep, false);
            }
            else
            {
                DisconectionType = DisconectionsType.Connected;
                IsConnected = true;
                await Util.SaveException(ex, SyncStep, true);
            }
        }

        public override void OnLowMemory()
        {
            GC.Collect();
            base.OnLowMemory();
        }
    }
}