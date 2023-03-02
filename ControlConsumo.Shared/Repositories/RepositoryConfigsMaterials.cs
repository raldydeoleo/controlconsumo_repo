using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.ConfigMaterial;
using ControlConsumo.Shared.Models.System;
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    internal class RepositoryConfigsMaterials : RepositoryBase, IRepository<ConfigMaterials>
    {
        public RepositoryConfigsMaterials(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryConfigsMaterials(MyDbConnection connection) : base(connection) { }

        public Task<ConfigMaterials> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ConfigMaterials>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(ConfigMaterials model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<ConfigMaterials> models)
        {
            var Intentado = false;

            VolverAInsertar:

            if (Intentado) await Task.Delay(Task_Delay);

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
            return true;
        }

        public Task<bool> InsertOrReplaceAsync(ConfigMaterials models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<ConfigMaterials> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ConfigMaterials model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<ConfigMaterials> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ConfigMaterials model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<ConfigMaterials> models)
        {
            var Intentado = false;

            VolverAActualizar:

            if (Intentado) await Task.Delay(Task_Delay);

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
                            Intentado = true;
                            goto VolverAActualizar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverAActualizar;

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
            //#region Validar si existe un columna

            //var info = await GetConnectionAsync().QueryAsync<TableInfoResult>("pragma table_info(\"ConfigMaterials\")");

            //if (!info.Any(p => p.name == "Key"))
            //{
            //    await GetConnectionAsync().DropTableAsync<ConfigMaterials>();
            //    await GetConnectionAsync().CreateTableAsync<ConfigMaterials>();
            //}
            //else
            //{
            //    var Intentado = false;

            //VolverABorrar:

            //    if (Intentado) await Task.Delay(Task_Delay);

            //    try
            //    {
            //        await GetConnectionAsync().DeleteAllAsync<ConfigMaterials>();
            //    }
            //    catch (SQLiteException ex)
            //    {
            //        switch (ex.Result)
            //        {
            //            case SQLite.Net.Interop.Result.Busy:
            //            case SQLite.Net.Interop.Result.Locked:
            //                Intentado = true;
            //                goto VolverABorrar;

            //            default:
            //                throw;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
            //}

            //#endregion

            return await SyncAsyncAll(false);
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.ConfigMaterials, Fecha = DateTime.Now };

            var url = GetService(ServicesType.GET_CONFIG_MATERIALS, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            Synclog.SizeBajada = json.SizePackageDownloading;

            if (json.isOk)
            {
                Synclog.RegistrosBajada = await InsertCommon(json.Json, true);
            }
            else
                throw json.ex;

            SyncMonitor.Detalle.Add(Synclog);

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

        public async Task<bool> CreateIndexAsync()
        {
            await Task.Run(() =>
            {
                GetConnection().CreateIndex("ConfigMaterials", new String[] { "ProductCode", "VerID" }, false);
            });

            return true;
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrIgnoreAllAsync(IEnumerable<ConfigMaterials> models)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().InsertOrIgnoreAllAsync(models);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverALeer;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverALeer;

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


        #region MyRegion

        public async Task<Int32> InsertCommon(String Json, Boolean IsInitialSync)
        {
            if (!Json.IsJsonEmpty())
            {
                var repoz = new RepositoryZ(this.Connection);

                var result = JsonConvert.DeserializeObject<ConfigMaterialResult[]>(Json);

                var buffer = result.Select(s => new ConfigMaterials()
                {
                    ProductCode = s.matnr, // materiales.Single(a => a.Code == p.matnr).ID,
                    VerID = s.verid,
                    MaterialCode = s.idnrk, //materiales.Single(a => a.Code == p.idnrk).ID,
                    Unit = s.meins,
                    Quantity = s.GetMenge
                });

                var Intentado = false;

                VolverABorrar:

                if (Intentado) await Task.Delay(Task_Delay);

                try
                {
                    await GetConnectionAsync().DeleteAllAsync<ConfigMaterials>();
                }
                catch (SQLiteException ex)
                {
                    switch (ex.Result)
                    {
                        case SQLite.Net.Interop.Result.Error:
                            if (ex.Message.Equals(conMessage))
                            {
                                Intentado = true;
                                goto VolverABorrar;
                            }
                            else
                                throw;

                        case SQLite.Net.Interop.Result.Busy:
                        case SQLite.Net.Interop.Result.Locked:
                            Intentado = true;
                            goto VolverABorrar;

                        default:
                            throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                await InsertAsyncAll(buffer);

                //var BufferUpdate = new List<ConfigMaterials>();

                //foreach (var item in result)
                //{
                //    var config = new ConfigMaterials()
                //    {
                //        ProductCode = item.matnr, // materiales.Single(a => a.Code == p.matnr).ID,
                //        VerID = item.verid,
                //        MaterialCode = item.idnrk, //materiales.Single(a => a.Code == p.idnrk).ID,
                //        Unit = item.meins,
                //        Quantity = item.GetMenge
                //    };

                //    if (IsInitialSync)
                //    {
                //        bufferInsert.Add(config);
                //    }
                //    else
                //    {
                //        var Intentado = false;

                //    VolverALeer:

                //        if (Intentado) await Task.Delay(Task_Delay);

                //        ConfigMaterials existe = null;

                //        try
                //        {
                //            existe = await GetConnectionAsync().Table<ConfigMaterials>().Where(c => c.ProductCode == config.ProductCode && c.MaterialCode == config.MaterialCode).FirstOrDefaultAsync();
                //        }
                //        catch (SQLiteException ex)
                //        {
                //            switch (ex.Result)
                //            {
                //                case SQLite.Net.Interop.Result.Busy:
                //                case SQLite.Net.Interop.Result.Locked:
                //                    Intentado = true;
                //                    goto VolverALeer;

                //                default:
                //                    throw;
                //            }
                //        }
                //        catch (Exception)
                //        {
                //            throw;
                //        }

                //        if (existe != null)
                //        {
                //            config.ID = existe.ID;
                //            BufferUpdate.Add(config);
                //        }
                //        else
                //        {
                //            bufferInsert.Add(config);
                //        }
                //    }
                //}

                //await InsertAsyncAll(bufferInsert);
                //await UpdateAllAsync(BufferUpdate);

                var repoSyncro = new RepositorySyncro(this.Connection);
                await repoSyncro.InsertOrReplaceAsync(new Syncro()
                {
                    Tabla = Syncro.Tables.ConfigMaterials,
                    Sync = true,
                    LastSync = result.Max(p => GetDatetime(p.prdat).Value),
                    IsDaily = false
                });

                return result.Count();
            }

            return 0;
        }

        public Task<string> InsertOrUpdateAsyncSql(ConfigMaterials[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
