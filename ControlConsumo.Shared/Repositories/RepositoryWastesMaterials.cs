using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.WasteMaterial;
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
    internal class RepositoryWastesMaterials : RepositoryBase, IRepository<WastesMaterials>
    {
        public RepositoryWastesMaterials(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryWastesMaterials(MyDbConnection connection) : base(connection) { }

        public Task<WastesMaterials> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WastesMaterials>> GetAsyncAll()
        {
            var intentado = false;

            VolveraIntentar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<WastesMaterials>().ToListAsync();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            intentado = true;
                            goto VolveraIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraIntentar;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> InsertAsync(WastesMaterials model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<WastesMaterials> models)
        {
            var intentado = false;

            VolveraIntentar:

            if (intentado) await Task.Delay(Task_Delay);

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
                            intentado = true;
                            goto VolveraIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraIntentar;

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

        public Task<bool> InsertOrReplaceAsync(WastesMaterials models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<WastesMaterials> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(WastesMaterials model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<WastesMaterials> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(WastesMaterials model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<WastesMaterials> models)
        {
            var intentado = false;

            VolveraIntentar:

            if (intentado) await Task.Delay(Task_Delay);

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
                            intentado = true;
                            goto VolveraIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraIntentar;

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
            return await SyncAsyncAll(false);
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var url = GetService(ServicesType.GET_DMATERIALS, false, null, isItForInitialSync);

            var repoequipo = new RepositoryEquipments(this.Connection);
            var equipo = await repoequipo.GetAsyncByKey(Proceso.EquipmentID);

            var json = await PostJsonAsync(url, equipo.TimeID);

            if (!json.isOk) throw json.ex;

            return await ExecuteUpdate(json.Json);
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

        public Task<String> InsertOrUpdateAsyncSql(WastesMaterials[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #region Common Methods

        public async Task<Boolean> ExecuteUpdate(String Json)
        {
            var allMaterial = await GetAsyncAll();

            if (!Json.IsJsonEmpty())
            {
                var Buffer = JsonConvert.DeserializeObject<WastesMaterilsResult[]>(Json);

                var BuffertoUpdate = Buffer.Select(p => new WastesMaterials()
                {
                    MaterialCode = p.MATNR,
                    Name = p.MAKTX,
                    Unit = p.MEINS
                });

                var intentado = false;

                VolveraIntentar:

                if (intentado) await Task.Delay(Task_Delay);

                try
                {
                    await GetConnectionAsync().DeleteAllAsync<WastesMaterials>();
                }
                catch (SQLiteException ex)
                {
                    switch (ex.Result)
                    {
                        case SQLite.Net.Interop.Result.Error:
                            if (ex.Message.Equals(conMessage))
                            {
                                intentado = true;
                                goto VolveraIntentar;
                            }
                            else
                                throw;

                        case SQLite.Net.Interop.Result.Busy:
                        case SQLite.Net.Interop.Result.Locked:
                            intentado = true;
                            goto VolveraIntentar;

                        default:
                            throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                await InsertAsyncAll(BuffertoUpdate);

                return true;
            }

            return false;
        }
        
        #endregion
    }
}
