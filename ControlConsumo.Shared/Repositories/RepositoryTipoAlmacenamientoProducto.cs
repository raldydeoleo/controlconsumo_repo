using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.TipoAlmacenamientoProducto;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    internal class RepositoryTipoAlmacenamientoProductos : RepositoryBase, IRepository<TipoAlmacenamientoProducto>
    {
        public RepositoryTipoAlmacenamientoProductos(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTipoAlmacenamientoProductos(MyDbConnection connection) : base(connection) { }

        public async Task<TipoAlmacenamientoProducto> GetAsyncByKey(object key)
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().GetAsync<TipoAlmacenamientoProducto>(key);
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

        public async Task<IEnumerable<TipoAlmacenamientoProducto>> GetAsyncAll()
        {
            var Intentado = false;

        VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<TipoAlmacenamientoProducto>().ToListAsync();
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

        public Task<bool> InsertAsync(TipoAlmacenamientoProducto model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<TipoAlmacenamientoProducto> models)
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

        public Task<bool> InsertOrReplaceAsync(TipoAlmacenamientoProducto models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<TipoAlmacenamientoProducto> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TipoAlmacenamientoProducto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<TipoAlmacenamientoProducto> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TipoAlmacenamientoProducto model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<TipoAlmacenamientoProducto> models)
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
            var listaTipoAlmacenamientoExistentes = new List<TipoAlmacenamientoProducto>();
            var listaTipoAlmacenamientoNoExistentes = new List<TipoAlmacenamientoProducto>();
            try
            {
                var con = GetConnectionAsync();

                var json = await GetJsonAsync(GetSqlServicePath(SqlServiceType.GetTipoAlmacenamientoProductos));

                if (json.isOk && !json.Json.IsJsonEmpty())
                {
                    var listaProductosPorTipoAlmacenamiento = new List<TipoAlmacenamientoProducto>();

                    foreach (var item in JsonConvert.DeserializeObject<List<TipoAlmacenamientoProductoResult>>(json.Json))
                    {
                        var elemento = new TipoAlmacenamientoProducto();
                        elemento.id = item.id;
                        elemento.nombre = item.nombre;
                        elemento.estatus = item.estatus;
                        listaProductosPorTipoAlmacenamiento.Add(elemento);
                    }
                    foreach (var item in listaProductosPorTipoAlmacenamiento)
                    {
                        var tipoAlmacenamiento = await GetAsyncByKey(item.id);

                        if (tipoAlmacenamiento == null)
                            listaTipoAlmacenamientoNoExistentes.Add(item);
                        else
                            listaTipoAlmacenamientoExistentes.Add(item);
                    }
                }
            }
            catch(Exception e)
            {
                throw;
            }

            var Intentado = false;

        VolverAInsertar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().InsertAllAsync(listaTipoAlmacenamientoNoExistentes);
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
                await GetConnectionAsync().UpdateAllAsync(listaTipoAlmacenamientoExistentes);
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
                GetConnection().CreateIndex("TipoAlmacenamientoProducto", new String[] { "ProductCode", "VerID" }, false);
            });

            return true;
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrIgnoreAllAsync(IEnumerable<TipoAlmacenamientoProducto> models)
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

        public Task<string> InsertOrUpdateAsyncSql(TipoAlmacenamientoProducto[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #region MyRegion


        #endregion
    }
}
