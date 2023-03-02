using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Json;
using ControlConsumo.Shared.Models.TrayProduct;
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    internal class RepositoryTraysProducts : RepositoryBase, IRepository<TraysProducts>
    {
        private static readonly List<TraysProducts> TraysProductsBufferInsert = new List<TraysProducts>();
        private static readonly List<TraysProducts> TraysProductsBufferUpdate = new List<TraysProducts>();
        private static readonly List<TraysProducts> TraysProductsBufferInsertOrUpdate = new List<TraysProducts>();

        public RepositoryTraysProducts(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTraysProducts(MyDbConnection connection) : base(connection) { }

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            var Count = TraysProductsBufferInsert.Count() + TraysProductsBufferUpdate.Count() + TraysProductsBufferInsertOrUpdate.Count();

            try
            {
                if (TraysProductsBufferInsert.Any())
                {
                    await connection.InsertAllAsync(TraysProductsBufferInsert);
                    TraysProductsBufferInsert.Clear();
                }
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        return -1;
                }
            }
            catch (Exception)
            { }

            try
            {
                if (TraysProductsBufferUpdate.Any())
                {
                    await connection.UpdateAllAsync(TraysProductsBufferUpdate);
                    TraysProductsBufferUpdate.Clear();
                }
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        return -1;
                }
            }
            catch (Exception)
            { }

            try
            {
                if (TraysProductsBufferInsertOrUpdate.Any())
                {
                    await connection.InsertOrReplaceAllAsync(TraysProductsBufferInsertOrUpdate);
                    TraysProductsBufferInsertOrUpdate.Clear();
                }
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        return -1;
                }
            }
            catch (Exception)
            { }

            return Count;
        }

        public Task<TraysProducts> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TraysProducts>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(TraysProducts model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<TraysProducts> models)
        {
            try
            {
                await GetConnectionAsync().InsertAllAsync(models);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            TraysProductsBufferInsert.AddRange(models);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        TraysProductsBufferInsert.AddRange(models);
                        break;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public async Task<bool> InsertOrReplaceAsync(TraysProducts models)
        {
            try
            {
                models.ModifyDate = DateTime.Now;
                await GetConnectionAsync().InsertOrReplaceAsync(models);
                await CreateSyncro(true);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            TraysProductsBufferInsertOrUpdate.Add(models);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        TraysProductsBufferInsertOrUpdate.Add(models);
                        break;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<TraysProducts> models)
        {
            try
            {
                await GetConnectionAsync().InsertOrReplaceAllAsync(models);
                await CreateSyncro(true);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            TraysProductsBufferInsertOrUpdate.AddRange(models);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        TraysProductsBufferInsertOrUpdate.AddRange(models);
                        break;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public Task<bool> DeleteAsync(TraysProducts model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<TraysProducts> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TraysProducts model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<TraysProducts> models)
        {
            try
            {
                await GetConnectionAsync().UpdateAllAsync(models);
                await CreateSyncro(true);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            TraysProductsBufferUpdate.AddRange(models);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        TraysProductsBufferUpdate.AddRange(models);
                        break;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            var repoz = new RepositoryZ(this.Connection);
            JResult json = null;

            var Pendientes = await GetConnectionAsync().Table<TraysProducts>().Where(p => p.Sync).ToListAsync();

            if (Pendientes.Any())
            {
                var urlUpdload = GetService(ServicesType.GET_BANDEJAS_MAT, false);

                var Proceso = await repoz.GetProces();

                var buffer = Pendientes.Select(p => new TraysProductsResult
                {
                    zsecuencia = p.Secuencia,
                    status = p.Status == TraysProducts._Status.Lleno ? "LL" : "VA",
                    idbandeja = p.TrayID,
                    idprocess = Proceso.Process,
                    idtiempo = p.TimeID,
                    IDEQUIPO = p.EquipmentID,
                    FECHA = p.Fecha.HasValue ? p.Fecha.Value.GetSapDateL() : "00000000",
                    HORA = p.Fecha.HasValue ? p.Fecha.Value.GetSapHoraL() : "000000",
                    UsuarioLlenada = p.UsuarioLlenada,
                    SECSALIDA = p.ElaborateID,
                    BATCHID = p.BatchID,
                    matnr = p.ProductCode,
                    meins = p.Unit,
                    menge = p.Quantity,
                    verid = p.VerID,
                    cpudt = p.ModifyDate.GetSapDateL(),
                    cputm = p.ModifyDate.GetSapHoraL(),
                    FechaVaciada = p.FechaHoraVaciada.HasValue ? p.FechaHoraVaciada.Value.GetSapDateL() : "00000000",
                    HoraVaciada = p.FechaHoraVaciada.HasValue ? p.FechaHoraVaciada.Value.ToString("HHmmss", System.Globalization.CultureInfo.InvariantCulture) : "000000",
                    UsuarioVaciada = p.UsuarioVaciada,
                    IdEquipoVaciado = p.IdEquipoVaciado
                });

                var retorno = await PostJsonAsync(urlUpdload, buffer);

                if (!retorno.isOk) throw retorno.ex;

                var query = "UPDATE TraysProducts SET Sync = 0 WHERE ID IN ({0});";

                await GetConnectionAsync().ExecuteAsync(String.Format(query, Pendientes.Select(p => p.ID).GetStringEnumerable()));
            }

            var url = GetService(ServicesType.GET_BANDEJAS_MAT, true);
            //var url = GetSqlServicePath("GetEstatusBandejas");

            json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json, false);
            else
                throw json.ex;

            return true;
        }

        public async Task<bool> SyncAsyncTwoWay()
        {
            var intentado = false;

            VolveraLeer:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                var sincro = new RepositorySyncro(this.Connection);

                var Pendientes = await GetConnectionAsync().Table<TraysProducts>().Where(p => p.Sync).ToListAsync();

                var LastRegistro = await sincro.GetAsyncByKey(Syncro.Tables.TraysProducts);

                var url = GetService(ServicesType.GET_BANDEJAS_MAT, false, LastRegistro);

                JResult json = null;

                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.TraysProducts, Fecha = LastRegistro.LastSync };

                if (Pendientes.Any())
                {
                    var repoz = new RepositoryZ(this.Connection);

                    var Proceso = await repoz.GetProces();

                    var buffer = Pendientes.Select(p => new TraysProductsResult
                    {
                        zsecuencia = p.Secuencia,
                        status = p.Status == TraysProducts._Status.Lleno ? "LL" : "VA",
                        idbandeja = p.TrayID,
                        idprocess = Proceso.Process,
                        idtiempo = p.TimeID,
                        IDEQUIPO = p.EquipmentID,
                        FECHA = p.Fecha.HasValue ? p.Fecha.Value.GetSapDateL() : "00000000",
                        HORA = p.Fecha.HasValue ? p.Fecha.Value.GetSapHoraL() : "000000",
                        UsuarioLlenada = p.UsuarioLlenada,
                        SECSALIDA = p.ElaborateID,
                        BATCHID = p.BatchID,
                        matnr = p.ProductCode,
                        meins = p.Unit,
                        menge = p.Quantity,
                        verid = p.VerID,
                        cpudt = p.ModifyDate.GetSapDateL(),
                        cputm = p.ModifyDate.GetSapHoraL(),
                        FechaVaciada = p.FechaHoraVaciada.HasValue ? p.FechaHoraVaciada.Value.GetSapDateL() : "00000000",
                        HoraVaciada = p.FechaHoraVaciada.HasValue ? p.FechaHoraVaciada.Value.ToString("HHmmss", System.Globalization.CultureInfo.InvariantCulture) : "000000",
                        UsuarioVaciada = p.UsuarioVaciada,
                        IdEquipoVaciado = p.IdEquipoVaciado
                    });

                    json = await PostJsonAsync(url, buffer);

                    Synclog.RegistrosSubida = buffer.Count();

                    intentado = false;

                    VolveraActualizar:

                    if (intentado) await Task.Delay(Task_Delay);

                    try
                    {
                        var query = "UPDATE TraysProducts SET Sync = 0 WHERE ID IN ({0});";

                        await GetConnectionAsync().ExecuteAsync(String.Format(query, Pendientes.Select(p => p.ID).GetStringEnumerable()));
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                {
                                    intentado = true;
                                    goto VolveraActualizar;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                intentado = true;
                                goto VolveraActualizar;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    //url = GetSqlServicePath("GetEstatusBandejas");
                    json = await GetJsonAsync(url);
                }

                Synclog.SizeBajada = json.SizePackageDownloading;
                Synclog.SizeSubida = json.SizePackageUploading;

                if (json.isOk)
                    Synclog.RegistrosBajada = await InsertCommon(json.Json, true);
                else
                    throw json.ex;

                SyncMonitor.Detalle.Add(Synclog);

                return true;
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            intentado = true;
                            goto VolveraLeer;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraLeer;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var count = await con.Table<TraysProducts>().CountAsync();

            if (count > 0) return false;

             var url = GetService(ServicesType.GET_BANDEJAS_MAT, true, null, isItForInitialSync);
            //var url = GetSqlServicePath("GetEstatusBandejas");

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json, true);
            else
                throw json.ex;

            return true;
        }

        public Task<bool> CreateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DropAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateIndexAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método para subir datos en línea al servidor SQL
        /// </summary>
        /// <param name="traysProducts"></param>
        /// <returns></returns>
        public async Task<String> InsertOrUpdateAsyncSql(TraysProducts[] traysProducts, Boolean esUnaDevolucion = false)
        {
            String message = "";
            try
            {
                var repoz = new RepositoryZ(this.Connection);
                var Proceso = await repoz.GetProces();
                var url = GetSqlServicePath(SqlServiceType.CrudEstatusBandeja);
                JResult json = null;
                var buffer = traysProducts.Select(p => new TraysProductsResult
                {
                    zsecuencia = p.Secuencia,
                    status = p.Status == TraysProducts._Status.Lleno ? "LL" : "VA",
                    idbandeja = p.TrayID,
                    idprocess = Proceso.Process,
                    idtiempo = p.TimeID,
                    IDEQUIPO = p.EquipmentID,
                    FECHA = p.Fecha.HasValue ? p.Fecha.Value.GetSapDate() : "00000000",
                    HORA = p.Fecha.HasValue ? p.Fecha.Value.ToString("HHmmss", System.Globalization.CultureInfo.InvariantCulture) : "000000",
                    UsuarioLlenada = p.UsuarioLlenada,
                    SECSALIDA = p.ElaborateID,
                    BATCHID = p.BatchID,
                    matnr = p.ProductCode,
                    meins = p.Unit,
                    menge = p.Quantity,
                    verid = p.VerID,
                    cpudt = p.ModifyDate.GetSapDate(),
                    cputm = p.ModifyDate.GetSapHora(),
                    FechaVaciada = p.FechaHoraVaciada.HasValue ? p.FechaHoraVaciada.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : "00000000",
                    HoraVaciada = p.FechaHoraVaciada.HasValue ? p.FechaHoraVaciada.Value.ToString("HHmmss", System.Globalization.CultureInfo.InvariantCulture) : "000000",
                    UsuarioVaciada = p.UsuarioVaciada,
                    IdEquipoVaciado = p.IdEquipoVaciado,
                    formaVaciada = p.formaVaciada,
                    esUnaDevolucion = esUnaDevolucion
                });
                json = await PostJsonAsync(url, buffer);
                if (json.isOk)
                {
                    var jsonResponse = JsonConvert.DeserializeObject<TraysProductSqlServerResponse>(json.Json);
                    if (jsonResponse.Validada)
                    {
                        message = jsonResponse.Mensaje;
                    }
                    else
                    {
                        throw new OperationCanceledException(jsonResponse.Mensaje);
                    }
                }
                else
                {
                    throw json.ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return message;
        }

        #region Common Methods

        public async Task<Int32> InsertCommon(String Json, Boolean IgnorarLocalChange)
        {
            if (!Json.IsJsonEmpty())
            {
                var bandejasTiempoPro = JsonConvert.DeserializeObject<TraysProductsResult[]>(Json);

                var buffer = bandejasTiempoPro.Select(p => p.Get()).ToList();
                //var buffer = bandejasTiempoPro.Select(p => new TraysProducts
                //{
                //    ID = String.Concat(p.idbandeja.ToUpper(), p.zsecuencia.ToString("00000")),
                //    ProductCode = p.matnr,
                //    Status = p.status == "LL" ? TraysProducts._Status.Lleno : TraysProducts._Status.Vacio,
                //    Quantity = p.menge,
                //    TimeID = p.idtiempo,
                //    Sync = false,
                //    TrayID = p.idbandeja,
                //    Secuencia = (short)p.zsecuencia,
                //    Unit = p.meins,
                //    VerID = p.verid,
                //    ElaborateID = p.SECSALIDA,
                //    EquipmentID = p.IDEQUIPO,
                //    Fecha = GetDatetime(p.FECHA, p.HORA),
                //    ModifyDate = GetDatetime(p.cpudt, p.cputm).Value,
                //    BatchID = p.BATCHID
                //});

                var cambiosLocales = await GetConnectionAsync().Table<TraysProducts>().Where(w => w.Sync).ToListAsync();

                var bufferFinal = buffer.Where(w => !cambiosLocales.Select(s => s.ID).Contains(w.ID));

                try
                {
                    //if (IgnorarLocalChange)
                    //    await InsertOrReplaceAsyncAll(buffer);
                    //else
                    //{
                    //    var bufferUpdate = new List<TraysProducts>();
                    //    foreach (var item in buffer)
                    //    {
                    //        var count = await GetConnectionAsync().Table<TraysProducts>().Where(w => w.ID == item.ID && w.Sync).CountAsync();
                    //        if (count == 0) bufferUpdate.Add(item);
                    //    }
                    //    await UpdateAllAsync(bufferUpdate);
                    //}
                    await InsertOrReplaceAsyncAll(bufferFinal);
                }
                catch (SQLiteException ex)
                {
                    switch (ex.Result)
                    {
                        case SQLite.Net.Interop.Result.Error:
                            if (ex.Message.Equals(conMessage))
                            {
                                TraysProductsBufferInsertOrUpdate.AddRange(buffer);
                            }
                            else
                                throw;

                            break;

                        case SQLite.Net.Interop.Result.Busy:
                        case SQLite.Net.Interop.Result.Locked:
                            TraysProductsBufferInsertOrUpdate.AddRange(buffer);
                            break;

                        default:
                            throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                var fecha = bandejasTiempoPro.Max(p => GetDatetime(p.cpudt, p.cputm));

                if (fecha.HasValue)
                {
                    var repoSyncro = new RepositorySyncro(this.Connection);

                    await repoSyncro.InsertOrReplaceAsync(new Syncro()
                    {
                        IsDaily = false,
                        LastSync = fecha.Value,
                        Sync = false,
                        Tabla = Syncro.Tables.TraysProducts
                    });
                }

                return bandejasTiempoPro.Count();
            }

            return 0;
        }

        public async Task CreateSyncro(Boolean value)
        {
            var repoSyncro = new RepositorySyncro(this.Connection);
            await repoSyncro.UpdateTableAsync(Syncro.Tables.TraysProducts, value);
        }

        #endregion
    }
}
