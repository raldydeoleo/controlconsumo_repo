using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.LabelPrintingLog;
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    internal class RepositoryLabelPrintingLogs : RepositoryBase, IRepository<LabelPrintingLogs>
    {
        private static readonly List<LabelPrintingLogs> LabelPrintingLogsBufferInsert = new List<LabelPrintingLogs>();
        private static readonly List<LabelPrintingLogs> LabelPrintingLogsBufferUpdate = new List<LabelPrintingLogs>();

        public RepositoryLabelPrintingLogs(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryLabelPrintingLogs(MyDbConnection connection) : base(connection) { }

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            var count = LabelPrintingLogsBufferInsert.Count() + LabelPrintingLogsBufferUpdate.Count();

            try
            {
                if (LabelPrintingLogsBufferInsert.Any())
                {
                    await connection.InsertAllAsync(LabelPrintingLogsBufferInsert);
                    LabelPrintingLogsBufferInsert.Clear();
                }
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        return -1;
                }
            }
            catch (Exception)
            { }

            try
            {
                if (LabelPrintingLogsBufferUpdate.Any())
                {
                    await connection.UpdateAllAsync(LabelPrintingLogsBufferUpdate);
                    LabelPrintingLogsBufferUpdate.Clear();
                }
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        return -1;
                }
            }
            catch (Exception)
            { }

            return count;
        }

        public async Task<LabelPrintingLogs> GetAsyncByKey(object key)
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().GetAsync<LabelPrintingLogs>(key);
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

        public async Task<IEnumerable<LabelPrintingLogs>> GetAsyncAll()
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<LabelPrintingLogs>().ToListAsync();
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

        public async Task<bool> InsertAsync(LabelPrintingLogs model)
        {
            try
            {
                model.CustomFecha = model.ProductionDate.GetDBDate();
                await GetConnectionAsync().InsertAsync(model);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                            LabelPrintingLogsBufferInsert.Add(model);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        LabelPrintingLogsBufferInsert.Add(model);
                        break;

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

        public async Task<bool> InsertAsyncAll(IEnumerable<LabelPrintingLogs> models)
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

        public Task<bool> InsertOrReplaceAsync(LabelPrintingLogs models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<LabelPrintingLogs> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(LabelPrintingLogs model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<LabelPrintingLogs> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(LabelPrintingLogs model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<LabelPrintingLogs> models)
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

        public async Task<bool> SyncAsync(bool ProcesarSap)
        {
            var Intentado = false;

            VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.LabelPrintingLog, Fecha = DateTime.Now };

                var url = GetSqlServicePath(SqlServiceType.PostReimpresionEtiquetas);

                var pendientes = await GetConnectionAsync().Table<LabelPrintingLogs>().Where(p => p.SyncSQL).Take(MAX_ROWS).ToListAsync();

                if (pendientes.Any())
                {
                    var buffer = pendientes.Select(p => new LabelPrintingLogResult
                    {
                        IdEquipo = p.EquipmentID,
                        SecuenciaEtiqueta = p.PackSequence,
                        IdMotivoReimpresionEtiqueta = p.LabelPrintingReasonID,
                        Cantidad = p.Quantity,
                        AlmFiller = p.Identifier,
                        Empaque = p.PackID,
                        FechaProduccion = p.ProductionDate,
                        Turno = p.TurnID,
                        UsuarioReimpresion = p.User,
                        FechaReimpresion = p.LabelReprintedDate
                    }).OrderByDescending(o => o.FechaProduccion).ToList();

                    var json = await PostJsonAsync(url, buffer);

                    if (!json.isOk) throw json.ex;

                    Synclog.RegistrosSubida = pendientes.Count();
                    Synclog.SizeSubida = json.SizePackageUploading;

                    foreach (var item in pendientes)
                    {
                        item.SyncSQL = false;
                    }

                    var Intentado2 = false;

                    VolvarActualizar:

                    if (Intentado2) await Task.Delay(Task_Delay);

                    try
                    {
                        await GetConnectionAsync().UpdateAllAsync(pendientes);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                {
                                    Intentado2 = true;
                                    goto VolvarActualizar;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                Intentado2 = true;
                                goto VolvarActualizar;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    SyncMonitor.Detalle.Add(Synclog);

                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverAIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverAIntentar;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var listaHistorialReimpresionesExistentes = new List<LabelPrintingLogs>();
            var listaHistorialReimpresionesNoExistentes = new List<LabelPrintingLogs>();
            try
            {
                var con = GetConnectionAsync();

                var json = await GetJsonAsync(GetSqlServicePath(SqlServiceType.GetHistorialReimpresionEtiquetas));

                if (json.isOk && !json.Json.IsJsonEmpty())
                {
                    var listaHistorialReimpresiones = new List<LabelPrintingLogs>();

                    foreach (var item in JsonConvert.DeserializeObject<List<LabelPrintingLogResult>>(json.Json))
                    {
                        var elemento = new LabelPrintingLogs();
                        elemento.CustomFecha = item.FechaProduccion.GetDBDate();
                        listaHistorialReimpresionesExistentes.Add(elemento);
                    }
                    foreach (var item in listaHistorialReimpresiones)
                    {
                        var reimpresionEtiqueta = await GetAsyncByKey(item.ID);
                        if (reimpresionEtiqueta == null)
                            listaHistorialReimpresionesNoExistentes.Add(item);
                        else
                            listaHistorialReimpresiones.Add(item);
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
                await GetConnectionAsync().InsertAllAsync(listaHistorialReimpresionesNoExistentes);
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
                await GetConnectionAsync().UpdateAllAsync(listaHistorialReimpresionesExistentes);
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
                GetConnection().CreateIndex("LabelPrintingLog", new String[] { "ProductCode", "VerID" }, false);
            });

            return true;
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrIgnoreAllAsync(IEnumerable<LabelPrintingLogs> models)
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

        public Task<string> InsertOrUpdateAsyncSql(LabelPrintingLogs[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #region MyRegion


        #endregion
    }
}
