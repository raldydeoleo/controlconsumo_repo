using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Config;
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
    internal class RepositoryConfigs : RepositoryBase, IRepository<Configs>
    {
        private static readonly List<Configs> ConfigsBufferInsert = new List<Configs>();
        private static readonly List<Configs> ConfigsBufferUpdate = new List<Configs>();
        private static readonly List<Configs> ConfigsBufferReplace = new List<Configs>();

        public RepositoryConfigs(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryConfigs(MyDbConnection connection) : base(connection) { }

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            var count = ConfigsBufferInsert.Count() + ConfigsBufferUpdate.Count() + ConfigsBufferReplace.Count();

            try
            {
                if (ConfigsBufferInsert.Any())
                {
                    await connection.InsertAllAsync(ConfigsBufferInsert);
                    ConfigsBufferInsert.Clear();
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
                if (ConfigsBufferUpdate.Any())
                {
                    await connection.UpdateAllAsync(ConfigsBufferUpdate);
                    ConfigsBufferUpdate.Clear();
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
                if (ConfigsBufferReplace.Any())
                {
                    await connection.InsertOrReplaceAllAsync(ConfigsBufferReplace);
                    ConfigsBufferReplace.Clear();
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

        public async Task<Configs> GetAsyncByKey(object key)
        {
            return await GetConnectionAsync().GetAsync<Configs>(key);
        }

        public async Task<IEnumerable<Configs>> GetAsyncAll()
        {
            return await GetConnectionAsync().Table<Configs>().ToListAsync();
        }

        public Task<bool> InsertAsync(Configs model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Configs> models)
        {
            try
            {
                await GetConnectionAsync().InsertAllAsync(models);
                await CreateSyncro(true);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            ConfigsBufferInsert.AddRange(models);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ConfigsBufferInsert.AddRange(models);

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

        public async Task<bool> InsertOrReplaceAsync(Configs models)
        {
            try
            {
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
                            ConfigsBufferReplace.Add(models);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ConfigsBufferReplace.Add(models);

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

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Configs> models)
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
                            ConfigsBufferReplace.AddRange(models);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ConfigsBufferReplace.AddRange(models);

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

        public Task<bool> DeleteAsync(Configs model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Configs> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Configs model)
        {
            try
            {
                await GetConnectionAsync().UpdateAsync(model);
                await CreateSyncro(true);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            ConfigsBufferUpdate.Add(model);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ConfigsBufferUpdate.Add(model);

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

        public async Task<bool> UpdateAllAsync(IEnumerable<Configs> models)
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
                        {
                            ConfigsBufferUpdate.AddRange(models);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ConfigsBufferUpdate.AddRange(models);

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

        public Task<bool> SyncAsync(bool procesarSAP)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var count = await con.Table<Configs>().CountAsync();

            if (count > 0) return false;

            var url = GetService(ServicesType.GET_EQUIPOSCONFIG, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json, true);
            else
                throw json.ex;

            return json.isOk;
        }

        public Task<bool> CreateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DropAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateIndexAsync()
        {
            await Task.Run(() =>
            {
                GetConnection().CreateIndex("Configs", new String[] { "EquipmentID", "Begin" }, false);
                GetConnection().CreateIndex("Configs", new String[] { "Status", "Begin" }, false);
            });

            return true;
        }

        public async Task<bool> SyncAsyncTwoWay()
        {
            if (IsSyncing) return false;

            try
            {
                IsSyncing = true;


                IEnumerable<Configs> configs = null;
                var repoSyncro = new RepositorySyncro(this.Connection);
                var repoz = new RepositoryZ(this.Connection);

                var LastUpdate = await repoSyncro.GetAsyncByKey(Syncro.Tables.Configs);

                var url = GetService(ServicesType.GET_EQUIPOSCONFIG, false, LastUpdate);

                JResult json = null;
                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Configs, Fecha = LastUpdate.LastSync };

                if (!LastUpdate.Sync || Proceso.IsSubEquipment)
                {
                    json = await GetJsonAsync(url);
                }
                else
                {
                    var Intentado = false;

                    VolverAIntentar:

                    if (Intentado) await Task.Delay(Task_Delay);

                    try
                    {
                        configs = await GetConnectionAsync().Table<Configs>().Where(p => p.Sync).Take(MAX_ROWS).ToListAsync();
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

                    var repoMateriales = new RepositoryMaterials(this.Connection);

                    var Proceso = await repoz.GetProces();

                    var buffer = configs.Select(p => new ConfigResult
                    {
                        idprocess = Proceso.Process,
                        idequipo = p.EquipmentID,
                        fecha = p.Begin.GetSapDateL(),
                        horaMin = p.Begin.GetSapHoraL(),
                        horaEje = p.ExecuteDate.GetSapHoraL(),
                        werks = Proceso.Centro,
                        idtiempo = p.TimeID,
                        matnr = p.ProductCode,
                        verid = p.VerID,
                        idequipo2 = p.SubEquipmentID,
                        zstatus = (Int32)p.Status,
                        usnam1 = p.Logon,
                        usnam2 = p.Logon2,
                        cpudt1 = p.CreateDate.GetSapDateL(),
                        cputm1 = p.CreateDate.GetSapHoraL(),
                        cpudt2 = p.ModifyDate.GetSapDateL(),
                        cputm2 = p.ModifyDate.GetSapHoraL(),
                        cold = p.Identifier
                    }).ToList();

                    Synclog.RegistrosSubida = buffer.Count();

                    json = await PostJsonAsync(url, buffer);
                }

                Synclog.SizeSubida = json.SizePackageUploading;
                Synclog.SizeBajada = json.SizePackageDownloading;

                if (json.isOk)
                    Synclog.RegistrosBajada = await InsertCommon(json.Json, false);
                else
                    throw json.ex;

                if (configs != null && configs.Any())
                {
                    var query = String.Format("UPDATE Configs SET Sync = 0 WHERE ID IN ({0})", configs.Select(p => p.ID).GetInt32Enumerable());

                    try
                    {
                        await GetConnectionAsync().ExecuteAsync(query);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                    RepositoryZ.AddCustomValuetoBuffer(new CustomQuery() { Query = query });
                                else
                                    throw;
                                break;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                RepositoryZ.AddCustomValuetoBuffer(new CustomQuery() { Query = query });
                                break;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                SyncMonitor.Detalle.Add(Synclog);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsSyncing = false;
            }

            return true;
        }

        #region Common Methods

        public async Task<Int32> InsertCommon(String json, Boolean IsInitialSync)
        {
            try
            {
                if (!json.IsJsonEmpty())
                {
                    DateTime? fecha = null;

                    var repoz = new RepositoryZ(this.Connection);
                    var planification = await repoz.GetSettingAsync<Boolean>(Settings.Params.Planificacion_Automatica, false);
                    var proceso = await repoz.GetProces();
                    var Configuraciones = JsonConvert.DeserializeObject<ConfigResult[]>(json);

                    var BufferttoInsert = new List<Configs>();
                    var BuffertoUpdate = new List<Configs>();

                    foreach (var item in Configuraciones)
                    {
                        var newConfig = new Configs();

                        newConfig.VerID = item.verid;
                        newConfig.ProductCode = item.matnr.ToSapCode();
                        newConfig.Status = (Configs._Status)item.zstatus;
                        newConfig.EquipmentID = item.idequipo;
                        newConfig.SubEquipmentID = item.idequipo2;
                        newConfig.TimeID = item.idtiempo;
                        newConfig.Begin = GetDatetime(item.fecha, item.horaMin).Value;
                        newConfig.Logon = item.usnam1;
                        newConfig.Logon2 = item.usnam2;
                        newConfig.CreateDate = GetDatetime(item.cpudt1, item.cputm1).Value;
                        newConfig.Sync = false;
                        newConfig.IsCold = !String.IsNullOrEmpty(item.cold);
                        newConfig.Identifier = item.cold;
                        if (GetDatetime(item.cpudt2, item.cputm2).HasValue)
                        {
                            newConfig.ModifyDate = GetDatetime(item.cpudt2, item.cputm2).Value;
                        }

                        if (proceso.IsSubEquipment && !planification && newConfig.EquipmentID.Equals(proceso.SubEquipmentID))
                        {
                            newConfig.Status = Configs._Status.Inactived;
                        }

                        if (!IsInitialSync)
                        {
                            var Repetir = false;

                            VolveraBuscar:

                            if (Repetir) await Task.Delay(Task_Delay);

                            try
                            {
                                var config = await GetConnectionAsync().Table<Configs>().Where(p => p.EquipmentID == item.idequipo && p.Begin == newConfig.Begin).FirstOrDefaultAsync();

                                if (config == null)
                                {
                                    BufferttoInsert.Add(newConfig);
                                }
                                else
                                {
                                    if (config.Status != newConfig.Status || config.ProductCode != newConfig.ProductCode || config.VerID != newConfig.VerID || config.IsCold != newConfig.IsCold)
                                    {
                                        newConfig.ID = config.ID;
                                        newConfig.ExecuteDate = config.ExecuteDate;
                                        BuffertoUpdate.Add(newConfig);
                                    }
                                }
                            }
                            catch (SQLiteException ex)
                            {
                                switch (ex.Result)
                                {
                                    case SQLite.Net.Interop.Result.Error:
                                        if (ex.Message.Equals(conMessage))
                                        {
                                            Repetir = true;
                                            goto VolveraBuscar;
                                        }
                                        else
                                            throw;

                                    case SQLite.Net.Interop.Result.Busy:
                                    case SQLite.Net.Interop.Result.Locked:
                                        Repetir = true;
                                        goto VolveraBuscar;

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
                            BufferttoInsert.Add(newConfig);
                        }
                    }

                    try
                    {
                        await GetConnectionAsync().InsertAllAsync(BufferttoInsert);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                {
                                    ConfigsBufferInsert.AddRange(BufferttoInsert);
                                }
                                else
                                    throw;

                                break;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                ConfigsBufferInsert.AddRange(BufferttoInsert);

                                break;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    try
                    {
                        await GetConnectionAsync().UpdateAllAsync(BuffertoUpdate);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                {
                                    ConfigsBufferUpdate.AddRange(BuffertoUpdate);
                                }
                                else
                                    throw;

                                break;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                ConfigsBufferUpdate.AddRange(BuffertoUpdate);

                                break;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    fecha = Configuraciones.Max(p => p.GetLastDate.Value);

                    if (fecha.HasValue)
                    {
                        var reposincro = new RepositorySyncro(this.Connection);

                        var sincro = new Syncro()
                        {
                            Tabla = Syncro.Tables.Configs,
                            Sync = false,
                            LastSync = fecha.Value
                        };

                        await reposincro.InsertOrReplaceAsync(sincro);
                    }

                    return Configuraciones.Count();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return 0;
        }

        public async Task CreateSyncro(Boolean value)
        {
            var repoSyncro = new RepositorySyncro(this.Connection);
            await repoSyncro.UpdateTableAsync(Syncro.Tables.Configs, value);
        }

        public Task<string> InsertOrUpdateAsyncSql(Configs[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
