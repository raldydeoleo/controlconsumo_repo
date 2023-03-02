using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.RolPemit;
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
    internal class RepositoryRolsPermits : RepositoryBase, IRepository<RolsPermits>
    {
        public RepositoryRolsPermits(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryRolsPermits(MyDbConnection connection) : base(connection) { }

        public Task<RolsPermits> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RolsPermits>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(RolsPermits model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<RolsPermits> models)
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

        public Task<bool> InsertOrReplaceAsync(RolsPermits models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<RolsPermits> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(RolsPermits model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<RolsPermits> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(RolsPermits model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<RolsPermits> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            var Intentado = false;

            VolveraActualizar:

            if (Intentado) await Task.Delay(Task_Delay);

            var url = GetService(ServicesType.GET_ROLESPERMITS, true);

            var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.RolsPermit, Fecha = DateTime.Now };

            var json = await GetJsonAsync(url);

            if (json.isOk && !json.Json.IsJsonEmpty())
            {
                var permisos = JsonConvert.DeserializeObject<RolsPermitsResult[]>(json.Json);

                try
                {
                    await GetConnectionAsync().DeleteAllAsync<RolsPermits>();
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

                var buffer = permisos.Select(p => new RolsPermits
                {
                    RolID = (short)p.znorol,
                    Permit = (RolsPermits.Permits)p.znopermiso
                });

                Synclog.SizeBajada = json.SizePackageDownloading;
                Synclog.RegistrosBajada = buffer.Count();

                await InsertAsyncAll(buffer);

                SyncMonitor.Detalle.Add(Synclog);
            }
            else if (!json.isOk)
            {
                throw json.ex;
            }

            return true;
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var count = await con.Table<RolsPermits>().CountAsync();

            if (count > 0) return false;

            var url = GetService(ServicesType.GET_ROLESPERMITS, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk && !json.Json.IsJsonEmpty())
            {
                var permisos = JsonConvert.DeserializeObject<RolsPermitsResult[]>(json.Json);

                var buffer = permisos.Select(p => new RolsPermits
                {
                    RolID = (short)p.znorol,
                    Permit = (RolsPermits.Permits)p.znopermiso
                });

                await InsertAsyncAll(buffer);
            }
            else if (!json.isOk)
            {
                throw json.ex;
            }

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


        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }
        
        public Task<string> InsertOrUpdateAsyncSql(RolsPermits[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
