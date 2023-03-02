using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Tray;
using ControlConsumo.Shared.Models.TraysResultSql;
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
    internal class RepositoryTrays : RepositoryBase, IRepository<Trays>
    {
        public RepositoryTrays(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTrays(MyDbConnection connection) : base(connection) { }

        public async Task<Trays> GetAsyncByKey(object key)
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().GetAsync<Trays>(key);
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

        public async Task<IEnumerable<Trays>> GetAsyncAll()
        {
            var intentado = false;

        VolveraIntentar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Trays>().ToListAsync();
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

        public Task<bool> InsertAsync(Trays model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Trays> models)
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

        public Task<bool> InsertOrReplaceAsync(Trays models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Trays> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Trays model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Trays> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Trays model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<Trays> models)
        {
            throw new NotImplementedException();
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

            var listaBandejasExistentes = new List<Trays> ();
            var listaBandejasNoExistentes = new List<Trays>();
            var listaBandejasJson = new List<Trays>();
            try
            {
                var con = GetConnectionAsync();
                var url = GetSqlServicePath(SqlServiceType.GetConfiguracionBandejas);
                var json = await GetJsonAsync(url);

                if (json.isOk && !json.Json.IsJsonEmpty())
                {

                    foreach (var item in JsonConvert.DeserializeObject<List<TraysResultSql>>(json.Json))
                    {
                        listaBandejasJson.Add(new Trays
                        {
                            ID = item.idBandeja,
                            Desde = item.secuenciaInicial,
                            Hasta = item.secuenciaFinal,
                            procesarSAP = item.procesarSap,
                            fechaRegistro = item.fechaRegistro,
                            usuarioRegistro = item.usuarioRegistro,
                            estatusVigencia = item.estatusVigencia
                        });
                    }
                    foreach (var item in listaBandejasJson)
                    {
                        var configuracionBandeja = await GetAsyncByKey(item.ID);

                        if (configuracionBandeja == null)
                            listaBandejasNoExistentes.Add(item);
                        else
                            listaBandejasExistentes.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            var Intentado = false;

            VolverAInsertar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().InsertAllAsync(listaBandejasNoExistentes);
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
                await GetConnectionAsync().UpdateAllAsync(listaBandejasExistentes);
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
            try
            {
                if (listaBandejasJson!=null && listaBandejasJson.Count > 0)
                {
                    var fecha = listaBandejasJson.Max(p => p.fechaRegistro);

                    var repoSyncro = new RepositorySyncro(this.Connection);

                    await repoSyncro.InsertOrReplaceAsync(new Syncro()
                    {
                        Tabla = Syncro.Tables.Trays,
                        LastSync = fecha,
                        Sync = false,
                        IsDaily = false
                    });
                }
            }
            catch (Exception)
            {
                throw;
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
        
        #region Common Methods

        public async Task InsertCommon(String Json)
        {
            if (!Json.IsJsonEmpty())
            {
                var bandejas = JsonConvert.DeserializeObject<TraysResultSql[]>(Json);

                var buffer = bandejas.Select(p => new Trays
                {
                    ID = p.idBandeja,
                    Desde = p.secuenciaInicial,
                    Hasta = p.secuenciaFinal,
                    procesarSAP = p.procesarSap,
                    fechaRegistro = p.fechaRegistro,
                    usuarioRegistro = p.usuarioRegistro,
                    estatusVigencia = p.estatusVigencia
                }).ToList();

                await InsertAsyncAll(buffer);

                var fecha = bandejas.Max(p => p.fechaRegistro);

                var repoSyncro = new RepositorySyncro(this.Connection);

                await repoSyncro.InsertAsync(new Syncro()
                { 
                    IsDaily = false,
                    LastSync = fecha,
                    Sync = false,
                    Tabla = Syncro.Tables.Trays
                });
            }
        }

        public Task<string> InsertOrUpdateAsyncSql(Trays[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
