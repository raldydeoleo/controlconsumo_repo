using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.TrayTime;
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
    internal class RepositoryTraysTimes : RepositoryBase, IRepository<TraysTimes>
    {
        public RepositoryTraysTimes(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTraysTimes(MyDbConnection connection) : base(connection) { }

        public async Task<TraysTimes> GetAsyncByKey(object key)
        {
            var Intentado = false;

        VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().GetAsync<TraysTimes>(key);
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

        public Task<IEnumerable<TraysTimes>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(TraysTimes model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<TraysTimes> models)
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

        public Task<bool> InsertOrReplaceAsync(TraysTimes models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<TraysTimes> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TraysTimes model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<TraysTimes> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TraysTimes model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<TraysTimes> models)
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
            var listaTiempoBandejaExistentes = new List<TraysTimes>();
            var listaTiempoBandejaNoExistentes = new List<TraysTimes>();
            var listaTiempoBandejaJson = new List<TraysTimes>();
            try
            {
                var con = GetConnectionAsync();
                var url = GetSqlServicePath(SqlServiceType.GetTiemposBandejas);
                var json = await GetJsonAsync(url);
                if (json.isOk && !json.Json.IsJsonEmpty())
                {
                    foreach (var item in JsonConvert.DeserializeObject<List<TrayTimeResultSql>>(json.Json))
                    {

                        listaTiempoBandejaJson.Add(new TraysTimes
                        {
                            ID = item.id,
                            IdProceso = item.idProceso,
                            TrayID = item.idBandeja,
                            TimeID = item.idTiempo,
                            Quantity = (Single)item.cantidad,
                            Unit = item.unidad
                        });
                    }
                    foreach (var item in listaTiempoBandejaJson)
                    {
                        var tiempoBandeja = await GetAsyncByKey(item.ID);
                        if (tiempoBandeja == null)
                            listaTiempoBandejaNoExistentes.Add(item);
                        else
                            listaTiempoBandejaExistentes.Add(item);
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
                await GetConnectionAsync().InsertAllAsync(listaTiempoBandejaNoExistentes);
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
                await GetConnectionAsync().UpdateAllAsync(listaTiempoBandejaExistentes);
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

        public async Task<Int32> InsertCommon(String Json)
        {
            //if (!Json.IsJsonEmpty())
            //{
            //    var bandejasTiempo = JsonConvert.DeserializeObject<TrayTimeResultSql[]>(Json);

            //    var buffer = bandejasTiempo.Select(p => new TraysTimes
            //    {
            //        TrayID = p.idBandeja,
            //        TimeID = p.idTiempo,
            //        Unit = p.unidad,
            //        Quantity = (float)p.cantidad
            //    });

            //    await InsertAsyncAll(buffer);

            //    var fecha = bandejasTiempo.Max(p => p.fechaRegistro);

            //    var repoSyncro = new RepositorySyncro(this.Connection);

            //    await repoSyncro.InsertOrReplaceAsync(new Syncro()
            //    {
            //        IsDaily = false,
            //        LastSync = fecha,
            //        Sync = false,
            //        Tabla = Syncro.Tables.TraysTimes
            //    });

            //    return bandejasTiempo.Count();
            //}

            return 0;
        }

        public Task<string> InsertOrUpdateAsyncSql(TraysTimes[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
