using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Json;
using ControlConsumo.Shared.Models.User;
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
    internal class RepositoryUsers : RepositoryBase, IRepository<Users>
    {
        public RepositoryUsers(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryUsers(MyDbConnection connection) : base(connection) { }

        public async Task<Users> GetAsyncByKey(object key)
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().GetAsync<Users>(key);
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
                return null;
            }
        }

        public async Task<IEnumerable<Users>> GetAsyncAll()
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Users>().ToListAsync();
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

        public Task<bool> InsertAsync(Users model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Users> models)
        {
            var Intentado = false;

            VolvelaIntentar:

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

            return true;
        }

        public Task<bool> InsertOrReplaceAsync(Users models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Users> models)
        {
            var Intentado = false;

            VolvelaIntentar:

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

            return true;
        }

        public Task<bool> DeleteAsync(Users model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Users> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Users model)
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().UpdateAsync(model);
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

            await CreateSyncro(true);

            return true;
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<Users> models)
        {
            var Intentado = false;

            VolvelaIntentar:

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

            await CreateSyncro(true);

            return true;
        }

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            var repoSyncro = new RepositorySyncro(this.Connection);

            var Syncrouser = await repoSyncro.GetAsyncByKey(Syncro.Tables.Users);

            var url = GetService(ServicesType.GET_USERS, false, Syncrouser);

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json, false);
            else
                throw json.ex;

            return true;
        }
        
        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var id = await con.Table<Users>().CountAsync();

            if (id > 0) return false;

            var url = GetService(ServicesType.GET_USERS, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json, true);
            else
                throw json.ex;

            return true;
        }

        public async Task<bool> CreateAsync()
        {
            await GetConnectionAsync().CreateTableAsync<Users>();
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

                IEnumerable<Users> detalle = null;

                var repoSyncro = new RepositorySyncro(this.Connection);

                var LastUpdate = await repoSyncro.GetAsyncByKey(Syncro.Tables.Users);

                var url = GetService(ServicesType.GET_USERS, false);

                JResult json = null;

                var isDataUpdated = false;

                #region Subida de datos hacia SAP
                var intentado = false;

                VolveraIntentar:

                if (intentado) await Task.Delay(Task_Delay);

                try
                {
                    detalle = await GetConnectionAsync().Table<Users>().Where(p => p.Sync).Take(50).ToListAsync();
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

                if (detalle != null && detalle.Any())
                {
                    var buffer = detalle.Select(p => new UsersResult
                    {
                        bname = p.Logon,
                        name1 = p.Name,
                        newcode = p.Password,
                        znorol = p.RolID,
                        gltgb = p.Expire.Value.GetSapDateL(),
                        uflag = p.IsActive ? 1 : 2,
                        pernr = Convert.ToInt32(p.Code),
                        cpudt = DateTime.Now.GetSapDate(),
                        cputm = DateTime.Now.GetSapHora(),
                        usnam = Proceso.Logon
                    }).ToList();

                    json = await PostJsonAsync(url, buffer);

                    if (json.isOk)
                        await InsertCommon(json.Json, false);
                    else
                        throw json.ex;

                    var query = String.Format("UPDATE Users SET Sync = 0 WHERE Logon IN ({0});", detalle.Select(p => p.Logon).GetStringEnumerable());

                    var IntentadoActualizar = false;

                    VolveraIntentarActualizar:

                    if (IntentadoActualizar) await Task.Delay(Task_Delay);

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
                                {
                                    IntentadoActualizar = true;
                                    goto VolveraIntentar;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                IntentadoActualizar = true;
                                goto VolveraIntentarActualizar;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    isDataUpdated = true;
                }

                #endregion

                #region Descarga de datos desde SAP
                if (!isDataUpdated)
                {
                    json = await GetJsonAsync(url);

                    if (json.isOk)
                        await InsertCommon(json.Json, false);
                    else
                        throw json.ex;
                }
                #endregion

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

        public Task<string> InsertOrUpdateAsyncSql(Users[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #region CommonMethods

        public async Task InsertCommon(String Json, Boolean IsInitialSync)
        {
            if (!Json.IsJsonEmpty())
            {
                var usuarios = JsonConvert.DeserializeObject<UsersResult[]>(Json);

                var buffer = usuarios.Select(p => new Users
                {
                    Logon = p.bname,
                    Name = p.name1,
                    Password = p.newcode,
                    RolID = (short)p.znorol,
                    Expire = GetDatetime(p.gltgb),
                    IsActive = p.uflag == 1,
                    Code = p.pernr.ToString(),
                    Sync = false
                }).ToList();

                if (!IsInitialSync)
                {
                    var BuffertoInsert = new List<Users>();
                    var BuffertoUpdate = new List<Users>();

                    foreach (var item in buffer)
                    {
                        var user = await GetAsyncByKey(item.Logon);

                        if (user == null)
                            BuffertoInsert.Add(item);
                        else
                            BuffertoUpdate.Add(item);
                    }

                    var Intentado = false;

                    VolverAInsertar:

                    if (Intentado) await Task.Delay(Task_Delay);

                    try
                    {
                        await GetConnectionAsync().InsertAllAsync(BuffertoInsert);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
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

                    Intentado = false;

                    VolverActualizar:

                    if (Intentado) await Task.Delay(Task_Delay);

                    try
                    {
                        await GetConnectionAsync().UpdateAllAsync(BuffertoUpdate);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                Intentado = true;
                                goto VolverActualizar;

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
                    var Intentado = false;

                    VolverActualizar:

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
                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                Intentado = true;
                                goto VolverActualizar;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                if (usuarios.Any())
                {
                    var fecha = usuarios.Where(p => p.Fecha.HasValue).Max(p => p.Fecha.Value);

                    var sincrorepo = new RepositorySyncro(this.Connection);

                    await sincrorepo.InsertOrReplaceAsync(new Syncro()
                    {
                        LastSync = fecha,
                        Tabla = Syncro.Tables.Users,
                        Sync = false
                    });
                }
            }
        }

        public async Task CreateSyncro(Boolean value)
        {
            var repoSyncro = new RepositorySyncro(this.Connection);
            await repoSyncro.UpdateTableAsync(Syncro.Tables.Users, value);
        }

        #endregion
    }
}
