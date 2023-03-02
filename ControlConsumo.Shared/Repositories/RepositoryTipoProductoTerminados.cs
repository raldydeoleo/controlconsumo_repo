using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.TipoProductoTerminado;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    internal class RepositoryTipoProductoTerminados : RepositoryBase, IRepository<TipoProductoTerminado>
    {
        public RepositoryTipoProductoTerminados(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTipoProductoTerminados(MyDbConnection connection) : base(connection) { }

        public async Task<TipoProductoTerminado> GetAsyncByKey(object key)
        {
            var Intentado = false;

        VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().GetAsync<TipoProductoTerminado>(key);
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

        public async Task<IEnumerable<TipoProductoTerminado>> GetAsyncAll()
        {
            var Intentado = false;

        VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<TipoProductoTerminado>().ToListAsync();
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

        public Task<bool> InsertAsync(TipoProductoTerminado model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<TipoProductoTerminado> models)
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

        public Task<bool> InsertOrReplaceAsync(TipoProductoTerminado models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<TipoProductoTerminado> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TipoProductoTerminado model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<TipoProductoTerminado> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TipoProductoTerminado model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<TipoProductoTerminado> models)
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

            //var info = await GetConnectionAsync().QueryAsync<TableInfoResult>("pragma table_info(\"ConfiguracionTiempoSalida\")");

            //if (!info.Any(p => p.name == "Key"))
            //{
            //    await GetConnectionAsync().DropTableAsync<ConfiguracionTiempoSalida>();
            //    await GetConnectionAsync().CreateTableAsync<ConfiguracionTiempoSalida>();
            //}
            //else
            //{
            //    var Intentado = false;

            //VolverABorrar:

            //    if (Intentado) await Task.Delay(Task_Delay);

            //    try
            //    {
            //        await GetConnectionAsync().DeleteAllAsync<ConfiguracionTiempoSalida>();
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
            var listaTipoProductosTerminadosNoExistentes = new List<TipoProductoTerminado>();
            var listaTipoProductosTerminadosExistentes = new List<TipoProductoTerminado>();
            try
            {
                var con = GetConnectionAsync();

                var json = await GetJsonAsync(GetSqlServicePath(SqlServiceType.GetTipoProductoTerminados));

                if (json.isOk && !json.Json.IsJsonEmpty())
                {
                    var listaTipoProductosTerminados = new List<TipoProductoTerminado>();

                    foreach (var item in JsonConvert.DeserializeObject<List<TipoProductoTerminadoResult>>(json.Json))
                    {
                        var elemento = new TipoProductoTerminado();
                        elemento.id = item.id;
                        elemento.nombre = item.nombre;
                        elemento.alias = item.alias;
                        elemento.estatus = item.estatus;
                        listaTipoProductosTerminados.Add(elemento);
                    }
                    foreach (var item in listaTipoProductosTerminados)
                    {
                        var tipoProductoTerminado = await GetAsyncByKey(item.id);

                        if (tipoProductoTerminado == null)
                            listaTipoProductosTerminadosNoExistentes.Add(item);
                        else
                            listaTipoProductosTerminadosExistentes.Add(item);
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
                await GetConnectionAsync().InsertAllAsync(listaTipoProductosTerminadosNoExistentes);
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
                await GetConnectionAsync().UpdateAllAsync(listaTipoProductosTerminadosExistentes);
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

        public async Task<bool> CreateIndexAsync()
        {
            await Task.Run(() =>
            {
                GetConnection().CreateIndex("TipoProductoTerminado", new String[] { "ProductCode", "VerID" }, false);
            });

            return true;
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrIgnoreAllAsync(IEnumerable<TipoProductoTerminado> models)
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

        public Task<string> InsertOrUpdateAsyncSql(TipoProductoTerminado[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #region MyRegion


        #endregion
    }
}
