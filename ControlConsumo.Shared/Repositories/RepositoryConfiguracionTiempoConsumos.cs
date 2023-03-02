using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.ConfiguracionTiempoConsumo;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    internal class RepositoryConfiguracionTiempoConsumos : RepositoryBase, IRepository<ConfiguracionTiempoConsumo>
    {
        public RepositoryConfiguracionTiempoConsumos(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryConfiguracionTiempoConsumos(MyDbConnection connection) : base(connection) { }

        public async Task<ConfiguracionTiempoConsumo> GetAsyncByKey(object key)
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().GetAsync<ConfiguracionTiempoConsumo>(key);
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

        public async Task<IEnumerable<ConfiguracionTiempoConsumo>> GetAsyncAll()
        {
            var Intentado = false;

        VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<ConfiguracionTiempoConsumo>().ToListAsync();
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

        public Task<bool> InsertAsync(ConfiguracionTiempoConsumo model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<ConfiguracionTiempoConsumo> models)
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

        public Task<bool> InsertOrReplaceAsync(ConfiguracionTiempoConsumo models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<ConfiguracionTiempoConsumo> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ConfiguracionTiempoConsumo model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<ConfiguracionTiempoConsumo> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ConfiguracionTiempoConsumo model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<ConfiguracionTiempoConsumo> models)
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
            var listaConfiguracionExistentes = new List<ConfiguracionTiempoConsumo>();
            var listaConfiguracionNoExistentes = new List<ConfiguracionTiempoConsumo>();
            try
            {
                var con = GetConnectionAsync();

                var json = await GetJsonAsync(GetSqlServicePath(SqlServiceType.GetParametrosTiempo));

                if (json.isOk && !json.Json.IsJsonEmpty())
                {
                    var listaConfiguracionJson = new List<ConfiguracionTiempoConsumo>();

                    foreach (var item in JsonConvert.DeserializeObject<List<ConfiguracionTiempoConsumoResult>>(json.Json))
                    {
                        var elemento = new ConfiguracionTiempoConsumo();
                        elemento.id = item.id;
                        elemento.idProceso = item.idProceso;
                        elemento.idTiempo = item.idTiempo;
                        elemento.tiempoMinimo = item.tiempoMinimo;
                        elemento.unidadTiempo = item.unidadTiempo;
                        elemento.usuarioRegistro = item.usuarioRegistro;
                        elemento.fechaRegistro = item.fechaRegistro;
                        elemento.horaRegistro = new TimeSpan();
                        elemento.horaRegistro = TimeSpan.Parse(item.horaRegistro);
                        if (item.fechaModificacion != null && item.horaModificacion != null)
                        {
                            elemento.fechaModificacion = item.fechaModificacion;
                            elemento.horaModificacion = new TimeSpan();
                            elemento.horaModificacion = TimeSpan.Parse(item.horaModificacion);
                        }
                        elemento.estatus = item.estatus;
                        listaConfiguracionJson.Add(elemento);
                    }
                    foreach (var item in listaConfiguracionJson)
                    {
                        var configuracion = await GetAsyncByKey(item.id);

                        if (configuracion == null)
                            listaConfiguracionNoExistentes.Add(item);
                        else
                            listaConfiguracionExistentes.Add(item);
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
                await GetConnectionAsync().InsertAllAsync(listaConfiguracionNoExistentes);
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
                await GetConnectionAsync().UpdateAllAsync(listaConfiguracionExistentes);
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
                GetConnection().CreateIndex("ConfiguracionTiempoConsumo", new String[] { "ProductCode", "VerID" }, false);
            });

            return true;
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrIgnoreAllAsync(IEnumerable<ConfiguracionTiempoConsumo> models)
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

        public Task<string> InsertOrUpdateAsyncSql(ConfiguracionTiempoConsumo[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #region MyRegion


        #endregion
    }
}
