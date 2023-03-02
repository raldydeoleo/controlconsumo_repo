using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.ConfiguracionInicialControlSalidas;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    internal class RepositoryConfiguracionInicialControlSalidas : RepositoryBase, IRepository<ConfiguracionInicialControlSalidas>
    {
        public RepositoryConfiguracionInicialControlSalidas(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryConfiguracionInicialControlSalidas(MyDbConnection connection) : base(connection) { }

        public async Task<ConfiguracionInicialControlSalidas> GetAsyncByKey(object key)
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().GetAsync<ConfiguracionInicialControlSalidas>(key);
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

        public async Task<IEnumerable<ConfiguracionInicialControlSalidas>> GetAsyncAll()
        {
            var Intentado = false;

        VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<ConfiguracionInicialControlSalidas>().ToListAsync();
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

        public Task<bool> InsertAsync(ConfiguracionInicialControlSalidas model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<ConfiguracionInicialControlSalidas> models)
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

        public Task<bool> InsertOrReplaceAsync(ConfiguracionInicialControlSalidas models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<ConfiguracionInicialControlSalidas> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ConfiguracionInicialControlSalidas model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<ConfiguracionInicialControlSalidas> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ConfiguracionInicialControlSalidas model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<ConfiguracionInicialControlSalidas> models)
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

        public async Task<bool> SyncAsync(Boolean procesarSap)
        {
            //#region Validar si existe un columna

            //var info = await GetConnectionAsync().QueryAsync<TableInfoResult>("pragma table_info(\"ConfiguracionSincronizacionTablas\")");

            //if (!info.Any(p => p.name == "Key"))
            //{
            //    await GetConnectionAsync().DropTableAsync<ConfiguracionSincronizacionTablas>();
            //    await GetConnectionAsync().CreateTableAsync<ConfiguracionSincronizacionTablas>();
            //}
            //else
            //{
            //    var Intentado = false;

            //VolverABorrar:

            //    if (Intentado) await Task.Delay(Task_Delay);

            //    try
            //    {
            //        await GetConnectionAsync().DeleteAllAsync<ConfiguracionSincronizacionTablas>();
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

            return await SyncAsyncAll();
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var listaConfiguracionExistentes = new List<ConfiguracionInicialControlSalidas>();
            var listaConfiguracionNoExistentes = new List<ConfiguracionInicialControlSalidas>();
            try
            {
                var con = GetConnectionAsync();
                var parametros = String.Format("?idEquipo={0}", Proceso.IsSubEquipment ? Proceso.SubEquipmentID : Proceso.EquipmentID);
                var json = await GetJsonAsync(GetSqlServicePath(SqlServiceType.GetConfiguracionInicialControlSalidas));
                
                if (json.isOk && !json.Json.IsJsonEmpty())
                {
                    var listaConfiguracionJson = new List<ConfiguracionInicialControlSalidas>();

                    foreach (var item in JsonConvert.DeserializeObject<List<ConfiguracionInicialControlSalidasResult>>(json.Json))
                    {
                        var elemento = new ConfiguracionInicialControlSalidas();
                        elemento.ID = item.Id;
                        elemento.IdEquipo = item.IdEquipo;
                        elemento.IdProducto = item.IdProducto;
                        elemento.CustomFecha = item.FechaProduccion.GetDBDate();
                        elemento.FechaProduccion = item.FechaProduccion;
                        elemento.Turno = item.Turno;
                        elemento.CantidadConsumoPendiente = item.CantidadConsumoPendiente;
                        elemento.Unidad = item.Unidad;
                        elemento.UsuarioRegistro = item.UsuarioRegistro;
                        elemento.FechaRegistro = item.FechaRegistro;
                        elemento.Estatus = item.Estatus;
                        if (item.UsuarioModificacion!=null && item.FechaModificacion != null)
                        {
                            elemento.FechaModificacion = item.FechaModificacion.Value;
                            elemento.UsuarioModificacion = item.UsuarioModificacion;
                        }
                        listaConfiguracionJson.Add(elemento);
                    }
                    foreach (var item in listaConfiguracionJson)
                    {
                        var configuracion = new ConfiguracionInicialControlSalidas();
                        configuracion = await GetAsyncByKey(item.ID);

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
                GetConnection().CreateIndex("ConfiguracionInicialControlSalidas", new String[] { "ProductCode", "VerID" }, false);
            });

            return true;
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrIgnoreAllAsync(IEnumerable<ConfiguracionInicialControlSalidas> models)
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

        public Task<string> InsertOrUpdateAsyncSql(ConfiguracionInicialControlSalidas[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #region MyRegion


        #endregion
    }
}
