using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Elaborate;
using ControlConsumo.Shared.Models.Json;
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
    internal class RepositoryElaborates : RepositoryBase, IRepository<Elaborates>
    {
        private static readonly List<Elaborates> ElaboratesBufferInsert = new List<Elaborates>();
        private static readonly List<Elaborates> ElaboratesBufferUpdate = new List<Elaborates>();

        public RepositoryElaborates(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryElaborates(MyDbConnection connection) : base(connection) { }

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            var count = ElaboratesBufferInsert.Count() + ElaboratesBufferUpdate.Count();

            try
            {
                if (ElaboratesBufferInsert.Any())
                {
                    await connection.InsertAllAsync(ElaboratesBufferInsert);
                    ElaboratesBufferInsert.Clear();
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
                if (ElaboratesBufferUpdate.Any())
                {
                    await connection.UpdateAllAsync(ElaboratesBufferUpdate);
                    ElaboratesBufferUpdate.Clear();
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

            return count;
        }

        public async Task<Elaborates> GetAsyncByKey(object key)
        {
            return await GetConnectionAsync().GetAsync<Elaborates>(key);
        }

        public async Task<IEnumerable<Elaborates>> GetAsyncAll()
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Elaborates>().ToListAsync();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolvelaIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolvelaIntentar;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> InsertAsync(Elaborates model)
        {
            try
            {
                model.CustomFecha = model.Produccion.GetDBDate();
                await GetConnectionAsync().InsertAsync(model);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                            ElaboratesBufferInsert.Add(model);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ElaboratesBufferInsert.Add(model);
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

        public async Task<bool> InsertAsyncAll(IEnumerable<Elaborates> models)
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
                            ElaboratesBufferInsert.AddRange(models);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ElaboratesBufferInsert.AddRange(models);
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

        public Task<bool> InsertOrReplaceAsync(Elaborates models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Elaborates> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Elaborates model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Elaborates> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Elaborates model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<Elaborates> models)
        {
            try
            {
                await GetConnectionAsync().UpdateAllAsync(models);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                            ElaboratesBufferUpdate.AddRange(models);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ElaboratesBufferUpdate.AddRange(models);
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

        public async Task<bool> SyncAsync(Boolean procesarSap)
        {
            var Intentado = false;

            VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                if (procesarSap)//Sincronización de datos por SAP
                {
                    var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Elaborates, Fecha = DateTime.Now };

                    var url = GetService(ServicesType.POST_ELABORATES);

                    var pendientes = await GetConnectionAsync().Table<Elaborates>().Where(p => p.Sync).Take(MAX_ROWS).ToListAsync();

                    if (pendientes.Any())
                    {
                        var buffer = pendientes.Select(p => new ElaboratesRequest
                        {
                            IDPROCESS = p.ProcessID,
                            IDEQUIPO = p.EquipmentID,
                            IDEQUIPO2 = p.SubEquipmentID,
                            IDTIEMPO = p.TimeID,
                            WERKS = p.Center,
                            IDBANDEJA = p.TrayID,
                            SECSALIDA = (Int16)p.CustomID,
                            MATNR = ExtensionsMethodsHelper.GetSapCode(p.ProductCode),
                            VERID = p.VerID,
                            FECHA = p.Produccion.GetSapDateL(),
                            HORA = p.Produccion.GetSapHoraL(),
                            IDTURNO = p.TurnID,
                            USNAM = p.Logon,
                            MENGE = p.Quantity,
                            MEINS = p.Unit,
                            MENGE2 = p.Peso.Round3(),
                            CPUDT = p.Fecha.GetSapDateL(),
                            CPUTM = p.Fecha.GetSapHoraL(),
                            CPUDT2 = DateTime.Now.GetSapDate(),
                            CPUTM2 = DateTime.Now.GetSapHora(),
                            BATCHID = p.BatchID,
                            CHARG = p.Lot,
                            VFDAT = p.ExpireDate.HasValue ? p.ExpireDate.Value.GetSapDateL() : "00000000",
                            IDEMPAQUE = p.PackID,
                            RETURNED = p.IsReturn ? "X" : String.Empty,
                            COLD = p.Identifier // Debido a un cambio en el proceso, se está enviando el código de identificador como valor.
                        }).OrderByDescending(o => o.FECHA).ToList();

                        var json = await PostJsonAsync(url, buffer);

                        if (!json.isOk) throw json.ex;

                        Synclog.RegistrosSubida = pendientes.Count();
                        Synclog.SizeSubida = json.SizePackageUploading;

                        foreach (var item in pendientes)
                        {
                            item.Sync = false;
                        }

                        var Intentado2 = false;

                        VolvarActualizar:

                        if (Intentado2) await Task.Delay(Task_Delay);

                        try
                        {
                            await GetConnectionAsync().UpdateAllAsync(pendientes);
                        }
                        catch (SQLiteException ex)
                        {
                            switch (ex.Result)
                            {
                                case SQLite.Net.Interop.Result.Error:
                                    if (ex.Message.Equals(conMessage))
                                    {
                                        Intentado2 = true;
                                        goto VolvarActualizar;
                                    }
                                    else
                                        throw;

                                case SQLite.Net.Interop.Result.Busy:
                                case SQLite.Net.Interop.Result.Locked:
                                    Intentado2 = true;
                                    goto VolvarActualizar;

                                default:
                                    throw;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        SyncMonitor.Detalle.Add(Synclog);

                        //return true;
                    }
                }
                await SyncAsyncSQL();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverAIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverAIntentar;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }

        public async Task<bool> SyncAsyncSQL()
        {
            var Intentado = false;

            VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Elaborates, Fecha = DateTime.Now };

                var url = GetSqlServicePath(SqlServiceType.PostSalida);

                var pendientes = await GetConnectionAsync().Table<Elaborates>().Where(p => p.SyncSQL).Take(MAX_ROWS_SQLSERVER).ToListAsync();

                if (pendientes.Any())
                {
                    var buffer = pendientes.Select(p => new ElaboratesRequest
                    {
                        IDPROCESS = p.ProcessID,
                        IDEQUIPO = p.EquipmentID,
                        IDEQUIPO2 = p.SubEquipmentID,
                        IDTIEMPO = p.TimeID,
                        WERKS = p.Center,
                        IDBANDEJA = p.TrayID,
                        SECSALIDA = (Int16)p.CustomID,
                        SECEMPAQUE = (Int16)p.PackSequence,
                        MATNR = ExtensionsMethodsHelper.GetSapCode(p.ProductCode),
                        VERID = p.VerID,
                        FECHA = p.Produccion.GetSapDateL(),
                        HORA = p.Produccion.GetSapHoraL(),
                        IDTURNO = p.TurnID,
                        USNAM = p.Logon,
                        MENGE = p.Quantity,
                        MEINS = p.Unit,
                        MENGE2 = p.Peso.Round3(),
                        CPUDT = p.Fecha.GetSapDateL(),
                        CPUTM = p.Fecha.GetSapHoraL(),
                        CPUDT2 = DateTime.Now.GetSapDate(),
                        CPUTM2 = DateTime.Now.GetSapHora(),
                        BATCHID = p.BatchID,
                        CHARG = p.Lot,
                        VFDAT = p.ExpireDate.HasValue ? p.ExpireDate.Value.GetSapDateL() : "00000000",
                        IDEMPAQUE = p.PackID,
                        RETURNED = p.IsReturn ? "X" : String.Empty,
                        COLD = p.Identifier // Debido a un cambio en el proceso, se está enviando el código de identificador como valor.
                    }).OrderByDescending(o => o.FECHA).ToList();

                    var json = await PostJsonAsync(url, buffer);

                    if (!json.isOk) throw json.ex;

                    Synclog.RegistrosSubida = pendientes.Count();
                    Synclog.SizeSubida = json.SizePackageUploading;

                    foreach (var item in pendientes)
                    {
                        item.SyncSQL = false;
                    }

                    var Intentado2 = false;

                    VolvarActualizar:

                    if (Intentado2) await Task.Delay(Task_Delay);

                    try
                    {
                        await GetConnectionAsync().UpdateAllAsync(pendientes);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                {
                                    Intentado2 = true;
                                    goto VolvarActualizar;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                Intentado2 = true;
                                goto VolvarActualizar;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    SyncMonitor.Detalle.Add(Synclog);

                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverAIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverAIntentar;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }


        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var url = "";
            var repoConfiguracionSincronizacionTablas = new RepositoryConfiguracionSincronizacionTablas(this.Connection);
            var configuracionSincronizacionTabla = await repoConfiguracionSincronizacionTablas.GetAsyncByKey(Syncro.Tables.Elaborates.ToString());
            var processBySAP = true;

            if (configuracionSincronizacionTabla != null)
            {
                processBySAP = configuracionSincronizacionTabla.procesarSap;
            }

            switch (processBySAP)
            {
                case true:
                    url = GetService(ServicesType.POST_ELABORATES, true, new Syncro() { LastSync = DateTime.Now.AddDays(-31) }, isItForInitialSync);
                    break;
                case false:
                    var parametros = String.Format("?fechaFinal={0}&idEquipo={1}", DateTime.Now.AddDays(-31).ToString("dd'/'MM'/'yyyy"),
                        Proceso.IsSubEquipment ? Proceso.SubEquipmentID : Proceso.EquipmentID);
                    url = GetSqlServicePath(SqlServiceType.GetSalidas, parametros);
                    break;
            }

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json, true);
            else
                throw json.ex;

            return false;
        }

        public async Task<bool> CreateAsync()
        {
            await GetConnectionAsync().CreateTableAsync<Elaborates>();
            return true;
        }

        public Task<bool> DropAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateIndexAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsyncTwoWay()
        {
            if (IsSyncing) return false;

            try
            {
                IsSyncing = true;

                var repoSyncro = new RepositorySyncro(this.Connection);

                var SyncroElaborate = await repoSyncro.GetAsyncByKey(Syncro.Tables.Elaborates);

                var url = GetService(ServicesType.POST_ELABORATES, false, SyncroElaborate);

                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Elaborates, Fecha = SyncroElaborate.LastSync };

                var json = await GetJsonAsync(url);

                if (json.isOk)
                {
                    Synclog.RegistrosBajada = await InsertCommon(json.Json, false);
                    Synclog.SizeBajada = json.SizePackageDownloading;
                }
                else
                    throw json.ex;

                SyncMonitor.Detalle.Add(Synclog);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsSyncing = false;
            }
        }

        public async Task<Int32> InsertCommon(String Json, Boolean IsInitial)
        {
            if (Json.IsJsonEmpty()) return 0;

            var Elaborados = JsonConvert.DeserializeObject<ElaboratesRequest[]>(Json);

            var buffer = Elaborados.Select(p => new Elaborates
            {
                ProcessID = p.IDPROCESS,
                Center = p.WERKS,
                Produccion = GetDatetime(p.FECHA, p.HORA).Value,
                Fecha = GetDatetime(p.CPUDT, p.CPUTM).Value,
                CustomFecha = GetDatetime(p.FECHA, p.HORA).Value.GetDBDate(),
                CustomID = p.SECSALIDA,
                EquipmentID = p.IDEQUIPO,
                TurnID = p.IDTURNO,
                TimeID = p.IDTIEMPO,
                ProductCode = p.MATNR.ToSapCode(),
                VerID = p.VERID,
                Logon = p.USNAM,
                BatchID = p.BATCHID,
                Peso = p.MENGE2,
                Sync = false,
                TrayID = p.IDBANDEJA,
                Quantity = p.MENGE,
                SubEquipmentID = p.IDEQUIPO2,
                Unit = p.MEINS,
                Lot = p.CHARG,
                ExpireDate = GetDatetime(p.VFDAT),
                PackID = p.IDEMPAQUE,
                PackSequence = p.SECEMPAQUE,
                IsReturn = !String.IsNullOrEmpty(p.RETURNED),
                IsCold = !String.IsNullOrEmpty(p.COLD),
                Identifier = p.COLD
            }).OrderBy(o => o.Fecha).ToList();

            if (buffer.Any())
            {
                var bufferInsert = new List<Elaborates>();

                foreach (var item in buffer)
                {
                    var elaborate = await GetConnectionAsync().Table<Elaborates>().Where(w => w.CustomFecha == item.CustomFecha && w.CustomID == item.CustomID).FirstOrDefaultAsync();

                    if (elaborate == null)
                    {
                        bufferInsert.Add(item);
                    }
                }

                await InsertAsyncAll(bufferInsert);

                var repoSyncro = new RepositorySyncro(this.Connection);

                var maxValue = Elaborados.Max(p => GetDatetime(p.CPUDT2, p.CPUTM2));

                if (maxValue.HasValue)
                {
                    await repoSyncro.InsertOrReplaceAsync(new Syncro()
                    {
                        Tabla = Syncro.Tables.Elaborates,
                        IsDaily = false,
                        Sync = false,
                        LastSync = maxValue.Value
                    });
                }

                if (IsInitial)
                {
                    var BatchID = buffer.OrderByDescending(d => d.Fecha).First().BatchID;

                    var reposetting = new RepositorySettings(this.Connection);
                    await reposetting.InsertOrReplaceAsync(new Settings
                    {
                        Key = Settings.Params.BatchID,
                        Value = BatchID
                    });
                }
            }

            return buffer.Count();
        }

        public Task<string> InsertOrUpdateAsyncSql(Elaborates[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}