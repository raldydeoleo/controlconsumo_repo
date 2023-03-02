using Android.Content;
using Android.Preferences;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Models.Json;
using ControlConsumo.Shared.Models.Material;
using ControlConsumo.Shared.Models.Process;
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Shared.Tables;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Droid.Managers
{
    /// <summary>
    /// Clase para implementar los servicios de dependencia de los PCL.
    /// Aristoteles Estrella Garcia 13.01.15
    /// </summary>
    public class Util
    {
        private static MyDbConnection Connection;
        private const String DataBaseName = "ControlConsumo.db";
        private const String ApkFileName = "ControlConsumo.apk";
        private const String FolderApk = "Download";
        private const String FolderApp = "ControlConsumo";
        private const String FolderAppExepcions = "ErrorsLog";
        private const String FolderDataBaseBackup = "BackupDB";

        /// <summary>
        /// Metodo para Retornar la ruta de la Base de Datos.
        /// </summary>
        /// <returns>Ruta de la DB</returns>
        private static String GetDataBasePath()
        {
            //#if DEBUG
            string docsPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            docsPath = Path.Combine(docsPath, FolderApp);
            var directorio = new DirectoryInfo(docsPath);
            if (!directorio.Exists) directorio.Create();
            docsPath = Path.Combine(docsPath, DataBaseName);
            ////#else
            ////            string docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ////            docsPath = Path.Combine(docsPath, DataBaseName);
            ////#endif
            return docsPath;
        }

        private static String GetExepcionsFolderLog()
        {
            var info = new DirectoryInfo(Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, FolderApp, FolderAppExepcions));

            if (!info.Exists) info.Create();

            return info.FullName;
        }

        private static String GetBackupFolder()
        {
            var info = new DirectoryInfo(Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, FolderApp, FolderDataBaseBackup));

            if (!info.Exists) info.Create();

            return info.FullName;
        }

        public static String GetDownloadFolder()
        {
            var info = new DirectoryInfo(Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, FolderApp, FolderApk));

            if (!info.Exists) info.Create();

            return info.FullName;
        }

        /// <summary>
        /// Metodo para guardar los Errores de la app
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async static Task SaveException(Exception e, String Step = null, Boolean Ignored = true)
        {
            var builder = new StringBuilder();

            if (e is WebException
                || e is SocketException
                || e is Java.Net.SocketException
                || e is Java.Net.SocketTimeoutException
                || e is Org.Apache.Http.Conn.ConnectTimeoutException
                || e is Newtonsoft.Json.JsonReaderException
                || e is Java.Net.NoRouteToHostException
                || e is Newtonsoft.Json.JsonSerializationException
                || e is IOException)
            {
                builder.AppendLine("----------------------------------------------------------------");
                builder.AppendLine(DateTime.Now.ToString("ddddd MMM dd MM, yyyyy  hh:mm tt"));
                builder.AppendLine(String.Format("Paso de la Sicronización: \"{0}\"", Step));
                builder.AppendLine(Ignored ? "Este error fue ignorado" : "Este error no fue Ignorado(Pide Confirmación Bandejas)");
                builder.AppendLine(e.Message);
                builder.AppendLine();

                var file = Path.Combine(GetExepcionsFolderLog(), String.Format("CommunicationsLog_{0}.txt", DateTime.Now.ToString("yyyyMMdd")));

                using (var writter = new StreamWriter(file, true))
                {
                    await writter.WriteAsync(builder.ToString());
                }
            }
            else
            {
                builder.AppendLine(String.Concat("Fecha       :   ", DateTime.Now.ToString("ddddd MMM dd MM, yyyyy  hh:mm:ss tt")));
                builder.AppendLine(String.Concat("Exception   :   ", e.InnerException != null ? e.InnerException.Message : e.Message));

                if (!String.IsNullOrEmpty(e.Source))
                    builder.AppendLine(String.Concat("Source      :   ", e.Source));

                if (!String.IsNullOrEmpty(e.StackTrace))
                    builder.AppendLine(String.Concat("StackTrace      :   ", e.StackTrace));

                var file = Path.Combine(GetExepcionsFolderLog(), String.Concat(DateTime.Now.ToString("yyyyyMMdd_hhmmss_"), ".txt"));

                using (var writter = new StreamWriter(file))
                {
                    await writter.WriteAsync(builder.ToString());
                }
            }
        }

        public async static Task SavePendingJobsResult(ExecutePendingJobsResult Result)
        {
            var builder = new StringBuilder();

            builder.AppendLine("La aplicacion capturo un error de bloqueo de base de datos.");
            builder.AppendLine("\"Este archivo no es de error, son registros de logs.\"");
            builder.AppendLine(String.Concat("Fecha       :   ", DateTime.Now.ToString("ddddd MMM dd MM, yyyyy  hh:mm tt")));
            builder.AppendLine(String.Format("Registros de \"Consultas Variadas\" Capturados : {0}", Result.ZCount));
            builder.AppendLine(String.Format("Registros de \"Traza\" Capturados : {0}", Result.ProductsRoutesCount));
            builder.AppendLine(String.Format("Registros de \"Bandejas\" Capturados : {0}", Result.TraysProductsCount));
            builder.AppendLine(String.Format("Registros de \"Control Cambios\" Capturados : {0}", Result.SyncroCount));
            builder.AppendLine(String.Format("Registros de \"Materiales\" Capturados : {0}", Result.MaterialsCount));
            builder.AppendLine(String.Format("Registros de \"Unidades\" Capturados : {0}", Result.UnitsCount));
            builder.AppendLine(String.Format("Registros de \"Liberacion\" Capturados : {0}", Result.RelaseCount));
            builder.AppendLine(String.Format("Registros de \"Planificaciones\" Capturados : {0}", Result.ConfigCount));
            builder.AppendLine(String.Format("Registros de \"Liberacion Bandejas\" Capturados : {0}", Result.RelasePositionCount));
            builder.AppendLine(String.Format("Registros de \"Consumo\" Capturados : {0}", Result.ConsumptionsCount));
            builder.AppendLine(String.Format("Registros de \"Bandejas\" Capturados : {0}", Result.ElaboratesCount));
            builder.AppendLine(String.Format("Registros de \"BOM\" Capturados : {0}", Result.ConfigMaterialsCount));
            builder.AppendLine(String.Format("Registros de \"Users\" Capturados : {0}", Result.UsersCount));

            var file = Path.Combine(GetExepcionsFolderLog(), String.Concat("PendingJobs_", DateTime.Now.ToString("yyyyyMMdd_hhmmss_"), ".txt"));

            using (var writter = new StreamWriter(file))
            {
                await writter.WriteAsync(builder.ToString());
            }
        }

        public async static Task SaveSyncLogMonitor(SyncLogMonitor monitor)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Log de estatus de consumo de recursos.");
            builder.AppendLine(String.Concat("Fecha       :   ", DateTime.Now.ToString("ddddd MMM dd MM, yyyyy  hh:mm tt")));
            builder.AppendLine(String.Format("Total Registros   Bajada: {0}, Subida: {1}, Total: {2}", monitor.TotalRegistrosBajada, monitor.TotalRegistrosSubida, monitor.TotalRegistros));
            builder.AppendLine(String.Format("Total Tamano    Bajada: {0}, Subida: {1}, Total: {2}", SizeSuffix(monitor.TotalSizeBajada), SizeSuffix(monitor.TotalSizeSubida), SizeSuffix(monitor.TotalSize)));

            foreach (var item in monitor.Detalle)
            {
                var nombre = String.Empty;

                switch (item.Tabla)
                {
                    case Syncro.Tables.Configs:
                        nombre = "Planificaciones";
                        break;
                    case Syncro.Tables.Trays:
                        nombre = "Configuración de Bandejas";
                        break;
                    case Syncro.Tables.TraysProducts:
                        nombre = "Bandejas";
                        break;
                    case Syncro.Tables.TraysTimes:
                        nombre = "Tiempos-Bandejas";
                        break;
                    case Syncro.Tables.ProductsRoutes:
                        nombre = "Traza";
                        break;
                    case Syncro.Tables.ConfigMaterials:
                        nombre = "BOM";
                        break;
                    case Syncro.Tables.Consumptions:
                        nombre = "Consumo";
                        break;
                    case Syncro.Tables.Elaborates:
                        nombre = "Salida";
                        break;
                    case Syncro.Tables.Equipments:
                        nombre = "Equipos";
                        break;
                    case Syncro.Tables.Errors:
                        nombre = "Errores";
                        break;
                    case Syncro.Tables.Inventories:
                        nombre = "Inventarios";
                        break;
                    case Syncro.Tables.Lots:
                        nombre = "Lotes";
                        break;
                    case Syncro.Tables.Materials:
                        nombre = "Materiales";
                        break;
                    case Syncro.Tables.MaterialsProcess:
                        nombre = "Material X Proceso";
                        break;
                    case Syncro.Tables.MaterialZilms:
                        nombre = "Material Zilm";
                        break;
                    case Syncro.Tables.Rols:
                        nombre = "Roles";
                        break;
                    case Syncro.Tables.RolsPermit:
                        nombre = "Permisos";
                        break;
                    case Syncro.Tables.Stocks:
                        nombre = "Turnos";
                        break;
                    case Syncro.Tables.Times:
                        nombre = "Tiempos";
                        break;
                    case Syncro.Tables.Tracking:
                        nombre = "Mezcla";
                        break;
                    case Syncro.Tables.TrayRelease:
                        nombre = "Liberacion de Bandejas";
                        break;
                    case Syncro.Tables.Users:
                        nombre = "Usuarios";
                        break;
                    case Syncro.Tables.Wastes:
                        nombre = "Desperdicios";
                        break;
                    case Syncro.Tables.ConfiguracionTiempoSalida:
                        nombre = "Configuracion de Tiempo de Salida";
                        break;
                    case Syncro.Tables.TipoAlmacenamientoProducto:
                        nombre = "Tipos de almacenamiento de producto";
                        break;
                    case Syncro.Tables.TipoProductoTerminado:
                        nombre = "Tipos de producto terminado";
                        break;
                    case Syncro.Tables.ProductoTipoAlmacenamiento:
                        nombre = "Configuración de producto por tipo de almacenamiento";
                        break;

                    default:
                        nombre = item.Tabla.ToString();
                        break;
                }

                builder.AppendLine();
                builder.AppendLine(String.Format("Fecha de cambio : {0}", item.Fecha.ToString("dd/MM/yyyy hh:mm tt")));
                builder.AppendLine(String.Format("Tabla {0}", nombre));
                builder.AppendLine(String.Format("      Registros     Bajada: {0}, Subida: {1}, Total: {2}", item.RegistrosBajada.ToString().PadRight(15), item.RegistrosSubida.ToString().PadRight(15), item.RegistrosTotal.ToString().PadRight(15)));
                builder.AppendLine(String.Format("      Tamano       Bajada: {0}, Subida: {1}, Total: {2}", SizeSuffix(item.SizeBajada).PadRight(15), SizeSuffix(item.SizeSubida).PadRight(15), SizeSuffix(item.SizeTotal).PadRight(15)));
                builder.AppendLine(String.Format("Tabla {0}", nombre));
            }

            builder.AppendLine();

            var file = String.Format(Path.Combine(GetExepcionsFolderLog(), "SyncMonitorLog_{0}.txt"), DateTime.Now.ToString("yyyyMMdd"));

            using (var writter = new StreamWriter(file, true))
            {
                await writter.WriteAsync(builder.ToString());
            }
        }

        public async static Task SaveSalidaLog(SyncLogMonitor.Detail Salida)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Log de estatus de consumo de recursos para la tabla de salida.");
            builder.AppendLine(String.Concat("Fecha       :   ", DateTime.Now.ToString("ddddd MMM dd MM, yyyyy  hh:mm tt")));
            builder.AppendLine(String.Format("Total Registros Bajada: {0}, Subida: {1}, Total: {2}", Salida.RegistrosBajada, Salida.RegistrosSubida, Salida.RegistrosTotal));
            builder.AppendLine(String.Format("Total Tamano Bajada: {0}, Subida: {1}, Total: {2}", SizeSuffix(Salida.SizeBajada), SizeSuffix(Salida.SizeSubida), SizeSuffix(Salida.SizeTotal)));
            builder.AppendLine(String.Format("Tamano Bytes Salida: {0}", Salida.SizeBajada));
            builder.AppendLine(String.Format("Secuencia Salida Minima: {0}", Salida.Salida.SecBaja));
            builder.AppendLine(String.Format("Secuencia Salida Maxima: {0}", Salida.Salida.SecAlta));
            builder.AppendLine(String.Format("Ids Internos: ({0})", Salida.Salida.Ids.GetInt32Enumerable()));
            builder.AppendLine(String.Format("Llamada a Servidor OK: {0}", Salida.Salida.IsOK ? "Si" : "No"));
            builder.AppendLine(String.Format("Registros Actualizados: {0}", Salida.Salida.Updated ? "Si" : "No"));
            builder.AppendLine(String.Format("Respuesta Servidor: {0}", ((Int32)Salida.Salida.Status).ToString()));

            if (!String.IsNullOrEmpty(Salida.Salida.Html))
            {
                var filehtml = String.Format(Path.Combine(GetExepcionsFolderLog(), "ResponseSalida_{0}.html"), DateTime.Now.ToString("yyyyMMdd_HHMMss"));

                using (var writter = new StreamWriter(filehtml, true))
                {
                    await writter.WriteAsync(Salida.Salida.Html);
                }
            }

            if (Salida.Salida.ex != null)
            {
                var error = Salida.Salida.ex.InnerException ?? Salida.Salida.ex;
                builder.AppendLine(String.Format("Exepcion de error: {0}", error.Message));
            }

            builder.AppendLine();

            var file = String.Format(Path.Combine(GetExepcionsFolderLog(), "SyncMonitorLogSalida_{0}.txt"), DateTime.Now.ToString("yyyyMMdd"));

            using (var writter = new StreamWriter(file, true))
            {
                await writter.WriteAsync(builder.ToString());
            }
        }

        /// <summary>
        /// Metodo para obtener la Coneccion y pasarlo al PCL.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        /// <returns>Conexion</returns>
        public static MyDbConnection GetConnection()
        {
            var docsPath = GetDataBasePath();
            var attr = new FileInfo(docsPath);

            if (Connection == null)
            {
                var cwLock = new SQLiteConnectionWithLock(new DataBase(), new SQLiteConnectionString(docsPath, true));
                Connection = new MyDbConnection(cwLock);
            }

            Connection.Exist = attr.Length != 0;

            return Connection;
        }

        /// <summary>
        /// Para reciclar la conecction despues de una carga inicial
        /// </summary>
        public static void RecycleConnection()
        {
            try
            {
                if (Connection != null)
                {
                    Connection.Dispose();
                    Connection = null;
                    GC.Collect();
                    GetConnection();
                }
            }
            catch (SQLiteException)
            { }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para liberar la conexion de la base de datos.
        /// </summary>
        public static void CloseConnection()
        {
            try
            {
                if (Connection != null)
                {
                    Connection.Dispose();
                    Connection = null;
                    GC.Collect();
                }
            }
            catch (SQLiteException)
            { }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para Ejecutar las Setencias en el Servidor Web.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        /// <param name="url">Direccion</param>
        /// <returns>Resultado</returns>
        public JResult GetJson(String url)
        {
            try
            {
                var request = HttpWebRequest.Create(url);
                request.Method = "GET";
                using (var response = request.GetResponse() as HttpWebResponse)
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return new JResult()
                    {
                        Json = reader.ReadToEnd(),
                        isOk = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new JResult()
                {
                    Json = ex.Message,
                    isOk = false
                };
            }
        }

        /// <summary>
        /// Metodo para Ejecutar un Post en el Servidor Web.
        /// Aristoteles Estrella Garcia 13.01.15
        /// </summary>
        /// <param name="url">Direccion</param>
        /// <param name="body">Cuerpo del Mensaje</param>
        /// <returns>Resultado</returns>
        public JResult PostJson(String url, String body)
        {
            try
            {
                var request = HttpWebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "POST";
                request.ContentLength = body.Length;
                var bytes = Encoding.ASCII.GetBytes(body);
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, body.Length);
                }
                using (var response = request.GetResponse() as HttpWebResponse)
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return new JResult()
                    {
                        Json = reader.ReadToEnd(),
                        isOk = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new JResult()
                {
                    Json = ex.Message,
                    isOk = false
                };
            }
        }

        /// <summary>
        /// Metodo para hacer un get Asyncrono
        /// </summary>
        /// <param name="url">Url de la Direccion</param>
        /// <returns>Retorno</returns>
        public async Task<JResult> GetJsonAsync(String url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();
            respStream.Flush();

            using (var sr = new StreamReader(respStream))
            {
                //Need to return this response 
                string strContent = sr.ReadToEnd();
                return new JResult()
                {
                    isOk = true,
                    Json = strContent
                };
            }
        }

        public async Task<Int64> GetJsonMaterialAsync(String url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();
            respStream.Flush();

            using (var sr = new StreamReader(respStream))
            {
                //Need to return this response 
                Int64 strContent = Convert.ToInt64(sr.ReadToEnd());
                return strContent;
               /* return new JResult()
                {
                    isOk = true,
                    Json = strContent
                };*/
            }
        }


        /// <summary>
        /// Metodo para hacer los post Asyncronos
        /// </summary>
        /// <param name="url">Url de la Direccion</param>
        /// <param name="body">Cuerpo del post</param>
        /// <returns>Retorno</returns>
        public async Task<JResult> PostJsonAsync(String url, String body)
        {
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";

            var stream = await request.GetRequestStreamAsync();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(body);
                writer.Flush();
                writer.Dispose();
            }

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();

            using (var sr = new StreamReader(respStream))
            {
                return new JResult()
                {
                    isOk = true,
                    Json = sr.ReadToEnd()
                };
            }
        }

        public static async Task AddError(ProcessList Proceso, ActualConfig actualConfig, MaterialList material, BarCodeResult barcode, CachingManager Caching)
        {
            var error = new Errors()
            {
                ProcessID = Proceso.Process,
                Center = Proceso.Centro,
                Logon = Proceso.Logon,
                Produccion = Caching.GetProductionDate(),
                Fecha = DateTime.Now,
                Lot = barcode.Lot ?? String.Empty,
                EquipmentID = Proceso.EquipmentID,
                SubEquipmentID = actualConfig.SubEquipmentID,
                Sync = true,
                MaterialCode = material.MaterialCode,
                ProductCode = actualConfig.ProductCode,
                VerID = actualConfig.VerID,
                Quantity = barcode.Quantity,
                Unit = material.MaterialUnit,
                TurnID = Caching.TurnoID,
                TimeID = actualConfig.TimeID,
                Message = Errors.Messages.EXPIRED_MATERIAL
            };

            var repoerror = new RepositoryFactory(GetConnection()).GetRepositoryErrors();
            await repoerror.InsertAsync(error);
        }

        public static async Task AddError(ProcessList Proceso, ActualConfig actualConfig, Materials material, BarCodeResult barcode, CachingManager Caching, Boolean IncorrectUpcCode=false, Errors.Messages errorMessage = Errors.Messages.WRONG_UPCCODE_MATERIALCONSUMPTION)
        {
            var error = new Errors()
            {
                ProcessID = Proceso.Process,
                Center = Proceso.Centro,
                Logon = Proceso.Logon,
                Produccion = Caching.GetProductionDate(),
                Fecha = DateTime.Now,
                Lot = barcode.Lot ?? String.Empty,
                EquipmentID = Proceso.EquipmentID,
                SubEquipmentID = actualConfig.SubEquipmentID,
                Sync = true,
                MaterialCode = material._Code,
                ProductCode = actualConfig.ProductCode,
                VerID = actualConfig.VerID,
                Quantity = barcode.Quantity,
                Unit = material.Unit,
                TurnID = Caching.TurnoID,
                TimeID = actualConfig.TimeID,
                Message = IncorrectUpcCode ? errorMessage : Errors.Messages.WRONG_MATERIAL
            };

            var repoerror = new RepositoryFactory(GetConnection()).GetRepositoryErrors();
            await repoerror.InsertAsync(error);
        }

        public static void DownloadFile(String Url, String _Path)
        {
            var request = HttpWebRequest.Create(Url);
            if (File.Exists(_Path)) File.Delete(_Path);
            using (var response = request.GetResponse() as HttpWebResponse)
            using (var MyResponseStream = response.GetResponseStream())
            using (var MyFileStream = new FileStream(_Path, FileMode.Create, FileAccess.Write))
            {
                byte[] MyBuffer = new byte[4096];
                int BytesRead;

                while (0 < (BytesRead = MyResponseStream.Read(MyBuffer, 0, MyBuffer.Length)))
                {
                    MyFileStream.Write(MyBuffer, 0, BytesRead);
                }
            }
        }

        public static void CatchException(Context context, Exception ex)
        {
            var errorDialog = new ErrorDialog(context, ex);
        }

        public static Single CtoUnidad(Units UFrom, Single Quantity, Units UTo, IEnumerable<Units> units)
        {
            var cantidadfrom = CtoUnidadBase(UFrom, 1, units);
            var cantidadto = CtoUnidadBase(UTo, 1, units);
            return (cantidadfrom / cantidadto) * Quantity;
        }

        public static Single CtoUnidadBase(Units unidad, Single Quantity, IEnumerable<Units> units)
        {
            var unidadbase = units.Single(p => p.IsBase);

            Single Cantidad = 0;

            if (unidad.Unit == unidadbase.Unit) return Quantity;

            Cantidad = Quantity * unidad.Factor;

            return Cantidad;
        }

        public static String MaskBatchID(String BatchID)
        {
            try
            {
                if (String.IsNullOrEmpty(BatchID)) return BatchID;

                return String.Format("{0}-{1}-{2}-{3}-{4}", BatchID.Substring(0, 2), BatchID.Substring(2, 2), BatchID.Substring(6, 2), BatchID.Substring(8, 1), BatchID.Substring(10, 2));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async void CreateDataBaseBackup()
        {
            try
            {
                if (!Connection.Exist) return;

                var dbpath = Connection.LockConnection.DatabasePath;
                var dbpathbackup = Path.Combine(GetBackupFolder(), String.Format("{0}_{1}.bak", DataBaseName.Split('.')[0], DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));
                CloseConnection();
                File.Copy(dbpath, dbpathbackup);

                var fecha_semana = DateTime.Now.AddDays(-7).Date;

                var BackupsViejos = new DirectoryInfo(GetBackupFolder()).GetFiles().Where(p => p.CreationTime < fecha_semana).ToList();

                foreach (var item in BackupsViejos)
                {
                    File.Delete(item.FullName);
                }

                var LogsViejos = new DirectoryInfo(GetExepcionsFolderLog()).GetFiles().Where(p => p.CreationTime < fecha_semana).ToList();

                foreach (var item in LogsViejos)
                {
                    File.Delete(item.FullName);
                }
            }
            catch (Exception ex)
            {
                await SaveException(ex);
            }
            finally
            {
                RecycleConnection();
            }
        }

        /// <summary>
        /// Funcion para convertir de Dp to Pixel
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Int32 DpToPx(Context context, Single dp)
        {
            var d = context.Resources.DisplayMetrics.Density;
            return (int)(dp * d); // margin in pixels
        }

        private static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        private static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }

        public enum Locks
        {
            Servicio,
            Reimprimir,
            Turnos,
            CallBackSync,
            ChangeProduct,
            Dialog,
            ActividadRecepcion
        }

        public static Boolean TryLock(Context context, Locks ValueLock)
        {
            var val = String.Concat("Util.", ValueLock.ToString());

            var prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            var value = prefs.GetBoolean(val, false);

            if (!value)
            {
                var editor = prefs.Edit();
                editor.PutBoolean(val, true);
                editor.Commit();
            }

            return !value;
        }

        public static Boolean CheckLock(Context context, Locks ValueLock)
        {
            var val = String.Concat("Util.", ValueLock.ToString());

            var prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            var value = prefs.GetBoolean(val, false);

            return value;
        }

        public static void ReleaseLock(Context context, params Locks[] ValuesLock)
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            var editor = prefs.Edit();

            foreach (var item in ValuesLock)
            {
                editor.PutBoolean(String.Concat("Util.", item.ToString()), false);
            }

            editor.Commit();
        }

        /// <summary>
        /// Metodo para guardar los Errores de la app
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async static Task SaveServiceLog(String action)
        {
            try
            {
                var builder = new StringBuilder();

                builder.AppendLine("----------------------------------------------------------------");
                builder.AppendLine(DateTime.Now.ToString("ddddd MMM dd MM, yyyyy  hh:mm tt"));
                builder.AppendLine(action);
                builder.AppendLine();

                var file = Path.Combine(GetExepcionsFolderLog(), String.Format("ServiceLog_{0}.txt", DateTime.Now.ToString("yyyyMMdd")));

                using (var writter = new StreamWriter(file, true))
                {
                    await writter.WriteAsync(builder.ToString());
                    writter.Close();
                }
            }
            catch (Exception)
            { }
        }
    }
}