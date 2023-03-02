using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Rol;
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
    internal class RepositoryRols : RepositoryBase, IRepository<Rols>
    {
        public RepositoryRols(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryRols(MyDbConnection connection) : base(connection) { }

        public Task<Rols> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Rols>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Rols model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Rols> models)
        {
            var Intentado = false;

        VolveraActualizar:

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
                            goto VolveraActualizar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolveraActualizar;

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

        public Task<bool> InsertOrReplaceAsync(Rols models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Rols> models)
        {
            var Intentado = false;

        VolveraActualizar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().InsertOrReplaceAllAsync(models);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolveraActualizar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolveraActualizar;

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

        public Task<bool> DeleteAsync(Rols model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Rols> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Rols model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<Rols> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            if (IsSyncing) return false;

            try
            {
                IsSyncing = true;

                var repoSyncro = new RepositorySyncro(this.Connection);

                var Syncrorol = await repoSyncro.GetAsyncByKey(Syncro.Tables.Rols);

                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Rols, Fecha = Syncrorol.LastSync };
                
                var url = GetService(ServicesType.GET_ROLES, false);

                var json = await GetJsonAsync(url);

                Synclog.SizeBajada = json.SizePackageDownloading;

                if (json.isOk)
                    Synclog.RegistrosBajada = await InsertCommon(json.Json);
                else
                    throw json.ex;

                var repodet = new RepositoryRolsPermits(this.Connection);
                await repodet.SyncAsync(true);

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

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var id = await con.Table<Rols>().CountAsync();

            if (id > 0) return false;

            var url = GetService(ServicesType.GET_ROLES, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json);
            else
                throw json.ex;

            return true;
        }

        public async Task<bool> CreateAsync()
        {
            await GetConnectionAsync().CreateTableAsync<Rols>();
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

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        #region Common Methods

        public async Task<Int32> InsertCommon(String Json)
        {
            if (!Json.IsJsonEmpty())
            {
                var roles = JsonConvert.DeserializeObject<RolsResult[]>(Json);

                var buffer = roles.Select(p => new Rols
                {
                    ID = (short)p.znorol,
                    Rol = p.zrol
                });

                await InsertOrReplaceAsyncAll(buffer);

                var fecha = roles.Select(p => GetDatetime(p.cpudt, p.cputm).HasValue ? GetDatetime(p.cpudt, p.cputm).Value : new DateTime(1900, 1, 1)).Max();

                var sincrorepo = new RepositorySyncro(this.Connection);

                var sincro = new Syncro()
                {
                    Tabla = Syncro.Tables.Rols,
                    LastSync = fecha,
                    Sync = false
                };

                await sincrorepo.InsertOrReplaceAsync(sincro);

                /*var repopermit = new RepositoryRolsPermits(this.Connection);
                await repopermit.SyncAsync(true);*/
                return buffer.Count();
            }

            return 0;
        }

        public Task<string> InsertOrUpdateAsyncSql(Rols[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
