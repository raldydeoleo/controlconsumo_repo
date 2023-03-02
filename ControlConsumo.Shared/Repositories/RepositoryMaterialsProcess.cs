using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.MaterialProcess;
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
    internal class RepositoryMaterialsProcess : RepositoryBase, IRepository<MaterialsProcess>
    {
        public RepositoryMaterialsProcess(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryMaterialsProcess(MyDbConnection connection) : base(connection) { }

        public Task<MaterialsProcess> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MaterialsProcess>> GetAsyncAll()
        {
            return await GetConnectionAsync().Table<MaterialsProcess>().ToListAsync();
        }

        public Task<bool> InsertAsync(MaterialsProcess model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsyncAll(IEnumerable<MaterialsProcess> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsync(MaterialsProcess models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<MaterialsProcess> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(MaterialsProcess model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<MaterialsProcess> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(MaterialsProcess model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<MaterialsProcess> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            var repoSyncro = new RepositorySyncro(this.Connection);

            var LastUpdate = await repoSyncro.GetAsyncByKey(Syncro.Tables.MaterialsProcess);

            var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Lots, Fecha = LastUpdate.LastSync };

            var url = GetService(ServicesType.GET_MATERIALTIEMPO, true);

            var json = await GetJsonAsync(url);

            Synclog.SizeBajada = json.SizePackageDownloading;

            if (json.isOk)
            {
                Synclog.RegistrosBajada = await InsertCommon(json.Json);
            }
            else
                throw json.ex;

            SyncMonitor.Detalle.Add(Synclog);

            return true;
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var count = await con.Table<MaterialsProcess>().CountAsync();

            if (count > 0) return false;

            var url = GetService(ServicesType.GET_MATERIALTIEMPO, true,null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json);
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

        #region Common Methods

        public async Task<Int32> InsertCommon(String json)
        {
            try
            {
                DateTime? fecha = null;

                if (!json.IsJsonEmpty())
                {
                    var repoz = new RepositoryZ(this.Connection);
                    var materiales = JsonConvert.DeserializeObject<MaterialsProcessResult[]>(json);

                    var Intentado = false;

                VolvelaIntentar:

                    if (Intentado) await Task.Delay(Task_Delay);

                    try
                    {
                        await GetConnectionAsync().DeleteAllAsync<MaterialsProcess>();
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

                    var buffer = materiales.Select(s => new MaterialsProcess()
                    {
                        TimeID = s.IDTIEMPO,
                        ProductCode = s.MATNR
                    }).ToList();

                    //var AllConfigs = await GetAsyncAll();

                    //var BufferttoInsert = new List<MaterialsProcess>();
                    //var BuffertoUpdate = new List<MaterialsProcess>();

                    //foreach (var item in materiales)
                    //{
                    //    var material = new MaterialsProcess()
                    //    {
                    //        TimeID = item.IDTIEMPO,
                    //        ProductCode = item.MATNR
                    //    };

                    //    if (AllConfigs.Any(p => p.ProductCode == material.ProductCode && p.TimeID == material.TimeID))
                    //    {
                    //        BuffertoUpdate.Add(material);
                    //    }
                    //    else
                    //    {
                    //        BufferttoInsert.Add(material);
                    //    }
                    //}

                    Intentado = false;

                VolverAInsertar:

                    if (Intentado) await Task.Delay(Task_Delay);

                    try
                    {
                        await GetConnectionAsync().InsertAllAsync(buffer);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                {
                                    Intentado = true;
                                    goto VolverAInsertar;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                Intentado = true;
                                goto VolverAInsertar;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    //Intentado = false;

                    //VolveraActualizar:

                    //if (Intentado) await Task.Delay(Task_Delay);

                    //try
                    //{
                    //    await GetConnectionAsync().UpdateAllAsync(BuffertoUpdate);
                    //}
                    //catch (SQLiteException ex)
                    //{
                    //    switch (ex.Result)
                    //    {
                    //        case SQLite.Net.Interop.Result.Busy:
                    //        case SQLite.Net.Interop.Result.Locked:
                    //            Intentado = true;
                    //            goto VolveraActualizar;

                    //        default:
                    //            throw;
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //    throw;
                    //}

                    fecha = materiales.Max(p => GetDatetime(p.CPUDT, p.CPUTM).Value);

                    var reposincro = new RepositorySyncro(this.Connection);

                    var sincro = new Syncro()
                    {
                        Tabla = Syncro.Tables.MaterialsProcess,
                        Sync = false,
                        LastSync = fecha ?? DateTime.Now
                    };

                    await reposincro.InsertOrReplaceAsync(sincro);

                    return materiales.Count();
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
            await repoSyncro.UpdateTableAsync(Syncro.Tables.MaterialsProcess, value);
        }

        public Task<string> InsertOrUpdateAsyncSql(MaterialsProcess[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
