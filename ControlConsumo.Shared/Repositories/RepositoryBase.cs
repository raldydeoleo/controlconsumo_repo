using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using SQLite.Net;
using SQLite.Net.Async;
using System.Linq.Expressions;
using System.Net;
using System.IO;
using ControlConsumo.Shared.Models.Json;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;
using ControlConsumo.Shared.Models.Process;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Shared.Models.Z;
using Xamarin.Android.Net;
using System.Threading;
using System.Net.Http;

namespace ControlConsumo.Shared.Repositories
{
    /// <summary>
    /// Clase para compartir el getConnection y los metodos de consumo de Servicios a todos los modelos.
    /// Aristoteles Estrella Garcia 13.01.15
    /// </summary>
    public abstract class RepositoryBase
    {
        protected static EnvironmentType ConnectionEnvironment { get; set; }

        public RepositoryBase(SQLiteAsyncConnection Connection)
        {
            this.Connection = Connection;
        }

        public RepositoryBase(MyDbConnection Connection)
        {
            this.LockConnection = Connection.LockConnection;
            this.Connection = Connection.Connection;
            ConnectionEnvironment = EnvironmentType.Calidad;
        }

        protected SQLiteConnectionWithLock LockConnection { get; set; }

        protected SQLiteAsyncConnection Connection { get; set; }

        protected static Boolean DataBaseLocked { get; set; }
        protected static Boolean IsSyncing { get; set; }
        protected const byte Return = 1;
        protected static ProcessList Proceso { get; set; }
        protected static Users Usuario { get; set; }
        protected const Int32 Task_Delay = 2000;
        protected const Int32 MAX_ROWS = 5000;
        protected const Int32 MAX_ROWS_SQLSERVER = 2500;


        protected const String conMessage = "Cannot create commands from unopened database";

        protected static SyncLogMonitor SyncMonitor = new SyncLogMonitor();

        /// <summary>
        /// Tipos de Servicios, Get, Post, Sync
        /// </summary>
        protected enum ServicesType
        {
            GET_PROCESS,
            GET_USERS,
            GET_PERMITS,
            GET_ROLES,
            GET_ROLESPERMITS,
            GET_EQUIPMENTS,
            GET_EQUIPMENTTYPES,
            GET_CONFIGS,
            GET_EQUIPOSCONFIG,
            GET_MATERIALS,
            GET_BATCHES,
            GET_AREAS,
            GET_AREAS_DETAILS,
            GET_CONFIG_MATERIALS,
            GET_TIMES,
            GET_TURNS,
            GET_MATERIAL_ZILM,
            GET_BANDEJAS,
            GET_BANDEJAS_MAT,
            GET_BANDEJAS_TIEMPO,
            GET_SYNC_TABLES,
            GET_DMATERIALS,
            GET_MATERIALTIEMPO,
            POST_CONSUMPTIONS,
            POST_ELABORATES,
            POST_TRACKING,
            POST_ERRORS,
            POST_RELEASE,
            POST_STOCKS,
            POST_WASTES,
            POST_SYNCLOG,
            POST_LOGIC_INVENTORY,
            POST_LOGIC_INVENTORY_DET,
            POST_ROUTES,
            REPORT_GET_PARAMETERS,
            REPORT_PRODUCTION,
            REPORT_TICKETS,
            VALID_SECUENCES_INLINE,
            POST_SECUENCE_EXTERN,
            GET_NEXT_SECUENCE,
            POST_EMPAQUES,
            VALID_BANDEJA
        }

        /// <summary>
        /// Tipos servicios SQL Server
        /// </summary>
        protected enum SqlServiceType
        {
            GetParametrosTiempo, //GetConfiguracionTiempoSalidas
            GetTipoAlmacenamientoProductos,
            GetTipoProductoTerminados,
            GetProductoTipoAlmacenamientos,
            GetConfiguracionBandejas,
            GetTiemposBandejas,
            GetEstatusBandeja,
            CrudEstatusBandeja,
            GetEstatusBandejas,
            GetConsumos,
            PostConsumo,
            GetConfiguracionSincronizacionTablas,
            GetSalidas,
            PostSalida,
            PostTracking,
            GetConfiguracionInicialControlSalidas,
            GetSumatoriaConsumoProductos,
            GetCantidadBandejasConsumidas,
            GetHistorialReimpresionEtiquetas,
            PostReimpresionEtiquetas,
            GetMotivosReimpresionEtiquetas
        }

        public enum EnvironmentType
        {
            Calidad,
            Desarrollo,
            Produccion
        }

        public String GetServerName()
        {
            var service = GetService(ServicesType.GET_AREAS);
            return service.Split(':')[1].Replace("//", "").Trim();
        }

        protected String GetService(ServicesType Service, Boolean isInitial = false, Syncro syncro = null, Boolean isItForInitialSync =false)
        {
            if (Proceso == null) Proceso = new ProcessList();

            var parametros = String.Format("Service={0}&Centro={1}&Process={2}&IdEquipo={3}", Service.ToString(), Proceso.Centro, Proceso.Process, Proceso.EquipmentID);

            if (isInitial)
                parametros = String.Concat(parametros, "&Sync=X");

            if (syncro != null)
            {
                var fecha = syncro.LastSync.ToLocalTime();
                parametros = string.Concat(parametros, "&CPUDT=", fecha.ToString("yyyyMMdd"), "&CPUTM=", fecha.ToString("HHmmss"));
            }
            var url = "";

            switch (ConnectionEnvironment)
            {
                case EnvironmentType.Desarrollo:
                    url = String.Concat("http://ladostgsap06.la.local:8000/sap(bD1lcyZjPTQwMA==)/bc/bsp/sap/ztake_servicio/index.htm?", parametros);
                    break;
                case EnvironmentType.Calidad:
                    url = String.Concat("http://ladostgsap07.la.local:8000/sap(bD1lcyZjPTQwMA==)/bc/bsp/sap/ztake_servicio/index.htm?", parametros);
                    break;
                case EnvironmentType.Produccion:
                    if (isInitial && isItForInitialSync) // doble validación que determina el uso del servidor principal para cargas iniciales. 
                    {
                        url = String.Concat("http://ladostgsap01.la.local:8000/sap(bD1lbiZjPTQwMA==)/bc/bsp/sap/ztake_servicio/index.htm?", parametros);
                    }
                    else
                    {
                        url = String.Concat("http://ladostgsap08.la.local:8000/sap(bD1lbiZjPTQwMA==)/bc/bsp/sap/ztake_servicio/index.htm?", parametros);
                    }
                    break;
            }
            // Area Temporal para Tener Siempre Disponibilidad

            //if (Service == ServicesType.GET_MATERIALS || Service == ServicesType.GET_BATCHES)
            //{
            // PRODUCCION
            //return String.Concat("http://ladostgsap01.la.local:8000/sap(bD1lbiZjPTQwMA==)/bc/bsp/sap/ztake_servicio/index.htm?", parametros);
            //}

            //Area Temporal para Tener Siempre Disponibilidad

            //return String.Concat("http://ladostgsap05.la.local:8000/sap(bD1lbiZjPTQwMA==)/bc/bsp/sap/ztake_servicio/index.htm?", parametros);

            ////#if DEBUG

            // CALIDAD
            //return String.Concat("http://ladostgsap07.la.local:8000/sap(bD1lcyZjPTQwMA==)/bc/bsp/sap/ztake_servicio/index.htm?", parametros);

            ////#else
            //WEB DISPATCHER - PRODUCCION
            //return String.Concat("http://ladostgsap08.la.local:8000/sap(bD1lbiZjPTQwMA==)/bc/bsp/sap/ztake_servicio/index.htm?", parametros);
            ////#endif

            //return String.Concat("http://ladostgsap02.la.local:8000/sap(bD1lcyZjPTQwMA==)/bc/bsp/sap/ztake_servicio/index.htm?", parametros);

            //return String.Concat("http://ladostgsap03.la.local:8000/sap(bD1lbiZjPTQwMA==)/bc/bsp/sap/ztake_servicio/index.htm?", parametros);

            return url;
        }

        /// <summary>
        /// Método para obtener la ruta hacia el servidor sql
        /// </summary>
        /// <param name="Service"></param>
        /// <returns></returns>
        protected String GetSqlServicePath(SqlServiceType Service, String parametros = null)
        {
            var url = "";

            //Desarrollo (Servidor viejo)
            //String path = String.Concat("http://10.199.98.24/ControlConsumo.Service/Configuracion/", Service.ToString());

            //Desarrollo (Servidor nuevo)
            //String path = String.Concat("http://10.199.98.25/ControlConsumo.Service/Configuracion/", Service.ToString());


            //Local debug
            //String path = String.Concat("http://10.199.64.148:3000/Configuracion/", Service.ToString());
            //Producción
            //String path = String.Concat("http://10.199.98.84/ControlConsumo/Configuracion/", Service.ToString());
            //Producción servidor nuevo
            //String path = String.Concat("http://10.199.98.217/ControlConsumo/Configuracion/", Service.ToString());

            switch (ConnectionEnvironment)
            {
                case EnvironmentType.Calidad:
                case EnvironmentType.Desarrollo:
                    url = String.Concat("http://10.199.98.25/ControlConsumo.Service/Configuracion/", Service.ToString());
                    break;
                case EnvironmentType.Produccion:
                    url = String.Concat("http://10.199.98.217/ControlConsumo/Configuracion/", Service.ToString());
                    break;

            }

            if (parametros != null)
            {
                url = String.Concat(url, parametros);
            }
            return url;
        }

        /// <summary>
        /// Metodo para Realizar los Post a los Servidores Web.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        /// <param name="url">Direccion</param>
        /// <param name="body">Cuerpo</param>        
        /// <returns>Resultado</returns>
        protected async Task<JResult> PostJsonAsync(String url, Object body)
        {
            var request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            var postdata = JsonConvert.SerializeObject(body);
            byte[] data = Encoding.UTF8.GetBytes(postdata);

            try
            {
                using (var requestStream = await Task<Stream>.Factory.FromAsync(request.BeginGetRequestStream, request.EndGetRequestStream, request))
                {
                    requestStream.ReadTimeout = 240000;
                    requestStream.WriteTimeout = 240000;
                    await requestStream.WriteAsync(data, 0, data.Length);

                    using (var responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request))
                    using (var responseStream = responseObject.GetResponseStream())
                    using (var sr = new StreamReader(responseStream))
                    {
                        var result = new JResult()
                        {
                            Json = await sr.ReadToEndAsync()
                        };

                        result.Status = ((HttpWebResponse)responseObject).StatusCode;
                        result.isOk = result.Status == HttpStatusCode.OK;
                        result.SizePackageUploading = data.Count();
                        result.SizePackageDownloading = Encoding.UTF8.GetBytes(result.Json).Count();

                        return result;
                    }

                }
            }
            catch (Exception ex)
            {
                return new JResult()
                {
                    ex = ex,
                    Json = String.Empty,
                    isOk = false
                };
            }
            finally
            {

            }
        }

        /// <summary>
        /// Metodo para Ejecutar los Get a los Servidores Web.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        /// <param name="url">Direccion</param>        
        /// <returns>Resultado</returns>
        protected async Task<JResult> GetJsonAsync(String url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            try
            {
                using (var responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request))
                using (var responseStream = responseObject.GetResponseStream())
                using (var sr = new StreamReader(responseStream))
                {
                    var result = new JResult()
                    {
                        Json = await sr.ReadToEndAsync(),
                        isOk = ((HttpWebResponse)responseObject).StatusCode == HttpStatusCode.OK
                    };

                    result.SizePackageUploading = 0;
                    result.SizePackageDownloading = Encoding.UTF8.GetBytes(result.Json).Count();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return new JResult()
                {
                    ex = ex,
                    Json = String.Empty,
                    isOk = false
                };
            }
        }

        public static DateTime? GetDatetime(String date, String time = null)
        {
            try
            {
                if (Convert.ToInt32(date) == 0)
                {
                    return null;
                }
                else if (time != null && Convert.ToInt32(time) > 0)
                {
                    String fecha = String.Concat(date, " ", time);
                    return DateTime.ParseExact(fecha, "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);
                }
                else
                {
                    return DateTime.ParseExact(date.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para Convertir los parametros y Adjuntarlos a la Url.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        /// <param name="url">Direccion</param>
        /// <param name="parametros">Parametros</param>
        /// <returns>Direccion junto con los parametros</returns>
        private static String ParamtoUrl(String url, IEnumerable<Parameters> parametros)
        {
            url += "?";

            url = parametros.Aggregate(url, (current, param) => current + String.Concat(param.Key, "=", param.Value, "&"));

            url = url.Substring(0, url.Length - 1);

            return url;
        }

        #region Metodos Protegidos

        /// <summary>
        /// Metodo para devolver la Coneccion a la base de datos.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        /// <returns>Conexion de la BD</returns>
        protected SQLiteConnectionWithLock GetConnection()
        {
            return LockConnection;
        }

        /// <summary>
        /// Metodo para Retornar la Conexion Asincrona.
        /// </summary>
        /// <returns></returns>
        protected SQLiteAsyncConnection GetConnectionAsync()
        {
            return Connection;
        }

        #endregion
    }
}
