using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Tracked;
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Shared.Tables;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    internal class RepositoryTracking : RepositoryBase, IRepository<Tracking>
    {
        public RepositoryTracking(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTracking(MyDbConnection connection) : base(connection) { }

        public Task<Tracking> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tracking>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Tracking model)
        {
            var intentado = false;

            VolveraInsertar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().InsertAsync(model);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            intentado = true;
                            goto VolveraInsertar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraInsertar;

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

        public async Task<bool> InsertAsyncAll(IEnumerable<Tracking> models)
        {
            var intentado = false;

            VolveraInsertar:

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
                            goto VolveraInsertar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraInsertar;

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

        public Task<bool> InsertOrReplaceAsync(Tracking models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Tracking> models)
        {
            var intentado = false;

            VolveraInsertar:

            if (intentado) await Task.Delay(Task_Delay);

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
                            intentado = true;
                            goto VolveraInsertar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraInsertar;

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

        public Task<bool> DeleteAsync(Tracking model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Tracking> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Tracking model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<Tracking> models)
        {
            var intentado = false;

            VolveraActualizar:

            if (intentado) await Task.Delay(Task_Delay);

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
                            intentado = true;
                            goto VolveraActualizar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
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

        public async Task<bool> SyncAsync(Boolean procesarSap)
        {
            if (IsSyncing) return false;

            var intentado = false;

            VolveraSincronizar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                if (procesarSap)//Sincronización de datos por SAP
                {
                    var url = GetService(ServicesType.POST_TRACKING);

                    var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Tracking, Fecha = DateTime.Now };

                    var pendientes = await GetConnectionAsync().Table<Tracking>().Where(p => p.Sync).Take(MAX_ROWS).ToListAsync();

                    if (pendientes.Any())
                    {
                        var repoz = new RepositoryZ(this.Connection);

                        var proceso = await repoz.GetProces();

                        var actualconfig = await new RepositoryZ(this.Connection).GetActualConfig(Proceso.EquipmentID);

                        var buffer = pendientes.Select(p => new TrackingRequest
                        {
                            IDEQUIPO = actualconfig.EquipmentID,
                            IDPROCESS = proceso.Process,
                            SECENTRADA = p.ConsumptionID,
                            ZENTRADADATE = p.FechaConsumption.GetSapDateL(),
                            SECSALIDA = p.ElaborateID,
                            ZSALIDADATE = p.FechaElaborate.GetSapDateL(),
                            CPUDT = DateTime.Now.GetSapDate(),
                            CPUTM = DateTime.Now.GetSapHora()
                        }).ToList();

                        var json = await PostJsonAsync(url, buffer);

                        Synclog.RegistrosSubida = buffer.Count();
                        Synclog.SizeSubida = json.SizePackageUploading;

                        if (!json.isOk) throw json.ex;

                        foreach (var item in pendientes)
                        {
                            item.Sync = false;
                        }

                        await UpdateAllAsync(pendientes);

                        SyncMonitor.Detalle.Add(Synclog);

                        //return true;
                    }
                }

                await SyncAsyncSQL();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            intentado = true;
                            goto VolveraSincronizar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraSincronizar;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsSyncing = false;
            }

            return false;
        }

        public async Task<bool> SyncAsyncSQL()
        {
            if (IsSyncing) return false;

            var intentado = false;

            VolveraSincronizar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                var url = GetSqlServicePath(SqlServiceType.PostTracking);

                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Tracking, Fecha = DateTime.Now };

                var pendientes = await GetConnectionAsync().Table<Tracking>().Where(p => p.SyncSQL).Take(MAX_ROWS_SQLSERVER).ToListAsync();

                if (pendientes.Any())
                {
                    var repoz = new RepositoryZ(this.Connection);

                    var proceso = await repoz.GetProces();

                    var actualconfig = await new RepositoryZ(this.Connection).GetActualConfig(Proceso.EquipmentID);

                    var buffer = pendientes.Select(p => new TrackingRequest
                    {
                        IDEQUIPO = actualconfig.EquipmentID,
                        IDPROCESS = proceso.Process,
                        SECENTRADA = p.ConsumptionID,
                        ZENTRADADATE = p.FechaConsumption.GetSapDateL(),
                        SECSALIDA = p.ElaborateID,
                        ZSALIDADATE = p.FechaElaborate.GetSapDateL(),
                        CPUDT = DateTime.Now.GetSapDate(),
                        CPUTM = DateTime.Now.GetSapHora()
                    }).ToList();

                    var json = await PostJsonAsync(url, buffer);

                    Synclog.RegistrosSubida = buffer.Count();
                    Synclog.SizeSubida = json.SizePackageUploading;

                    if (!json.isOk) throw json.ex;

                    foreach (var item in pendientes)
                    {
                        item.SyncSQL = false;
                    }

                    await UpdateAllAsync(pendientes);

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
                            intentado = true;
                            goto VolveraSincronizar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraSincronizar;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsSyncing = false;
            }

            return false;
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            throw new NotImplementedException();
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

        public Task<string> InsertOrUpdateAsyncSql(Tracking[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}