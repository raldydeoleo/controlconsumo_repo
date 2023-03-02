using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Json;
using ControlConsumo.Shared.Models.ProductRoute;
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
    internal class RepositoryProductsRoutes : RepositoryBase, IRepository<ProductsRoutes>
    {
        private static readonly List<ProductsRoutes> RoutesBufferInsert = new List<ProductsRoutes>();
        private static readonly List<ProductsRoutes> RoutesBufferUpdate = new List<ProductsRoutes>();

        public RepositoryProductsRoutes(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryProductsRoutes(MyDbConnection connection) : base(connection) { }

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            var count = RoutesBufferInsert.Count() + RoutesBufferUpdate.Count();

            try
            {
                if (RoutesBufferInsert.Any())
                {
                    await connection.InsertAllAsync(RoutesBufferInsert);
                    RoutesBufferInsert.Clear();
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
                if (RoutesBufferUpdate.Any())
                {
                    await connection.UpdateAllAsync(RoutesBufferUpdate);
                    RoutesBufferUpdate.Clear();
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

        public Task<ProductsRoutes> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductsRoutes>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(ProductsRoutes model)
        {
            try
            {
                await GetConnectionAsync().InsertAsync(model);
                await CreateSyncro(true);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                            RoutesBufferInsert.Add(model);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        RoutesBufferInsert.Add(model);
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

        public async Task<bool> InsertAsyncAll(IEnumerable<ProductsRoutes> models)
        {
            try
            {
                await GetConnectionAsync().InsertAllAsync(models);
                await CreateSyncro(true);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                            RoutesBufferInsert.AddRange(models);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        RoutesBufferInsert.AddRange(models);
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

        public Task<bool> InsertOrReplaceAsync(ProductsRoutes models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<ProductsRoutes> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ProductsRoutes model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<ProductsRoutes> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(ProductsRoutes model)
        {
            try
            {
                await GetConnectionAsync().UpdateAsync(model);
                await CreateSyncro(true);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                            RoutesBufferUpdate.Add(model);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        RoutesBufferUpdate.Add(model);
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

        public async Task<bool> UpdateAllAsync(IEnumerable<ProductsRoutes> models)
        {
            try
            {
                await GetConnectionAsync().UpdateAllAsync(models);
                await CreateSyncro(true);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                            RoutesBufferUpdate.AddRange(models);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        RoutesBufferUpdate.AddRange(models);
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

        public Task<bool> SyncAsync(bool procesarSAP)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsyncTwoWay()
        {
            try
            {
                List<ProductsRoutes> Routes = null;
                List<ProductsRoutes> AllRoutes = new List<ProductsRoutes>();

                ///Forzo a que sincronice desde la tabla en si

                var registros = await GetConnectionAsync().QueryAsync<ProductsRoutes>("SELECT MAX(Sync) AS Sync, MAX(Fecha) AS Fecha FROM ProductsRoutes;");

                var registro = registros.FirstOrDefault();

                var LastUpdate = new Syncro
                {
                    IsDaily = false,
                    LastSync = registro.Fecha,
                    Sync = registro.Sync,
                    Tabla = Syncro.Tables.ProductsRoutes
                };

                var url = GetService(ServicesType.POST_ROUTES, false, LastUpdate);

                JResult json = null;

                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.ProductsRoutes, Fecha = LastUpdate.LastSync };

                if (!LastUpdate.Sync)
                {
                    json = await GetJsonAsync(url);

                    if (json.isOk)
                        await InsertCommon(json.Json, false);
                    else
                        throw json.ex;
                }
                else
                {
                    var Intentado = false;

                    VolverAIntentar:

                    if (Intentado) await Task.Delay(Task_Delay);

                    try
                    {
                        Routes = await GetConnectionAsync().Table<ProductsRoutes>().Where(p => p.Sync).OrderBy(o => o.ID).ToListAsync();

                        while (Routes.Any())
                        {
                            ///Forzo a que sincronice desde la tabla en si

                            registros = await GetConnectionAsync().QueryAsync<ProductsRoutes>("SELECT MAX(Sync) AS Sync, MAX(Fecha) AS Fecha FROM ProductsRoutes;");

                            registro = registros.FirstOrDefault();

                            LastUpdate = new Syncro
                            {
                                IsDaily = false,
                                LastSync = registro.Fecha,
                                Sync = registro.Sync,
                                Tabla = Syncro.Tables.ProductsRoutes
                            };

                            url = GetService(ServicesType.POST_ROUTES, false, LastUpdate);

                            var atomar = Routes.Take(100).ToList();

                            var buffer = atomar.Select(p => new ProductRouteRequest
                            {
                                IDPROCESS = p.ProcessID,
                                IDTIEMPO = p.TimeID,
                                IDREGISTRO = p.CustomID,
                                MJAHR = p.Year,
                                IDEQUIPO = p.EquipmentID,
                                WERKS = p.Center,
                                MATNR = p.ProductCode,
                                VERID = p.VerID,
                                CHARG = p.Lot,
                                IDBANDEJA = p.TrayID,
                                LOTEMAN = p.LotManufacture,
                                SECSALIDA = p.ElaborateID,
                                FECHA = p.Produccion.GetSapDateL(),
                                HORA = p.Produccion.GetSapHoraL(),
                                IDTURNO = p.TurnID,
                                BATCHID = p.BatchID,
                                SECEMPAQUE = p.SecuenciaEmpaque,
                                IDEMPAQUE = p.PackID,
                                MENGE = p.Quantity,
                                MENGE2 = p.Peso,
                                MEINS = p.Unit,
                                STATUSBAN = (Byte)p.Status,
                                USNAM = p.Logon,
                                CPUDT = p.Begin.GetSapDateL(),
                                CPUTM = p.Begin.GetSapHoraL(),
                                CPUDT2 = p.End.GetSapDateL(),
                                CPUTM2 = p.End.GetSapHoraL(),
                                CPUDT3 = p.Fecha.GetSapDateL(),
                                CPUTM3 = p.Fecha.GetSapHoraL(),
                                IDTIEMPO2 = p.TimeID2,
                                IDREGISTRO2 = p.CustomID2,
                                MJAHR2 = p.Year2
                            }).ToList();

                            Synclog.RegistrosSubida = buffer.Count();

                            json = await PostJsonAsync(url, buffer);

                            AllRoutes.AddRange(atomar);
                            Routes.RemoveAll(r => atomar.Select(s => s.ID).Contains(r.ID));

                            Synclog.SizeSubida = json.SizePackageUploading;
                            Synclog.SizeBajada = json.SizePackageDownloading;

                            if (json.isOk)
                                Synclog.RegistrosBajada = await InsertCommon(json.Json, false);
                            else
                                throw json.ex;

                            if (atomar != null && atomar.Any())
                            {
                                var query = String.Format("UPDATE ProductsRoutes SET Sync = 0 WHERE ID IN ({0}) AND CustomID > 0;", atomar.Select(p => p.ID).GetInt64Enumerable());

                                await GetConnectionAsync().ExecuteAsync(query);
                            }
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
                }

                SyncMonitor.Detalle.Add(Synclog);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var count = await GetConnectionAsync().Table<ProductsRoutes>().CountAsync();

            if (count > 0)
            {
                return false;
            }

            var url = GetService(ServicesType.POST_ROUTES, true, new Syncro() { LastSync = DateTime.Now.AddDays(-30) }, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json, true);
            else
                throw json.ex;

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
                GetConnection().CreateIndex("ProductsRoutes", new string[] { "Equipment", "Produccion", "TrayID", "ElaborateID" }, false);
            });

            return true;
        }

        #region Common Methods

        public async Task<Int32> InsertCommon(String json, Boolean IsInitialSync)
        {
            var repoSincro = new RepositorySyncro(this.Connection);

            var BufferttoInsert = new List<ProductsRoutes>();
            var BuffertoUpdate = new List<ProductsRoutes>();

            if (!json.IsJsonEmpty())
            {
                var routes = JsonConvert.DeserializeObject<ProductRouteRequest[]>(json);

                foreach (var item in routes)
                {
                    var newRoute = item.Get();

                    //newRoute.ProcessID = item.IDPROCESS;
                    //newRoute.TimeID = item.IDTIEMPO;
                    //newRoute.CustomID = item.IDREGISTRO;
                    //newRoute.Year = item.MJAHR;
                    //newRoute.EquipmentID = item.IDEQUIPO;
                    //newRoute.Center = item.WERKS;
                    //newRoute.ProductCode = item.MATNR;
                    //newRoute.VerID = item.VERID;
                    //newRoute.Lot = item.CHARG;
                    //newRoute.LotManufacture = item.LOTEMAN ?? String.Empty;
                    //newRoute.ElaborateID = item.SECSALIDA;
                    //newRoute.CustomFecha = Convert.ToInt32(GetDatetime(item.FECHA, item.HORA).Value.GetSapDate());
                    //newRoute.Produccion = GetDatetime(item.FECHA, item.HORA).Value;
                    //newRoute.TurnID = item.IDTURNO;
                    //newRoute.BatchID = item.BATCHID;
                    //newRoute.PackID = item.IDEMPAQUE ?? String.Empty;
                    //newRoute.SecuenciaEmpaque = item.SECEMPAQUE;
                    //newRoute.Quantity = item.MENGE;
                    //newRoute.Peso = item.MENGE2;
                    //newRoute.Unit = item.MEINS;
                    //newRoute.TrayID = item.IDBANDEJA;
                    //newRoute.Status = (ProductsRoutes.RoutesStatus)item.STATUSBAN;
                    //newRoute.Logon = item.USNAM;
                    //newRoute.Fecha = GetDatetime(item.CPUDT3, item.CPUTM3).Value;
                    //newRoute.TimeID2 = item.IDTIEMPO2;
                    //newRoute.CustomID2 = item.IDREGISTRO2;
                    //newRoute.Year2 = item.MJAHR2;
                    //newRoute.LastUpdate = item.GetLastDate;
                    //newRoute.IsActive = false;
                    //newRoute.Sync = false;

                    //if (GetDatetime(item.CPUDT, item.CPUTM).HasValue) newRoute.Begin = GetDatetime(item.CPUDT, item.CPUTM).Value;

                    //if (GetDatetime(item.CPUDT2, item.CPUTM2).HasValue) newRoute.End = GetDatetime(item.CPUDT2, item.CPUTM2).Value;

                    if (!IsInitialSync)
                    {
                        var Repetir = false;

                        VolveraBuscar:

                        if (Repetir) await Task.Delay(Task_Delay);

                        try
                        {
                            var OldRoute = await GetConnectionAsync().Table<ProductsRoutes>().Where(p => p.ElaborateID == newRoute.ElaborateID && p.Produccion == newRoute.Produccion && p.EquipmentID == newRoute.EquipmentID).FirstOrDefaultAsync();

                            if (OldRoute == null)
                            {
                                BufferttoInsert.Add(newRoute);
                            }
                            else
                            {
                                newRoute.ID = OldRoute.ID;
                                newRoute.IsActive = OldRoute.IsActive;
                                BuffertoUpdate.Add(newRoute);
                            }
                        }
                        catch (SQLiteException ex)
                        {
                            switch (ex.Result)
                            {
                                case SQLite.Net.Interop.Result.Error:
                                    if (ex.Message.Equals(conMessage))
                                    {
                                        Repetir = true;
                                        goto VolveraBuscar;
                                    }
                                    else
                                        throw;

                                case SQLite.Net.Interop.Result.Busy:
                                case SQLite.Net.Interop.Result.Locked:
                                    Repetir = true;
                                    goto VolveraBuscar;

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
                        BufferttoInsert.Add(newRoute);
                    }
                }

                try
                {
                    await GetConnectionAsync().InsertAllAsync(BufferttoInsert);
                }
                catch (SQLiteException ex)
                {
                    switch (ex.Result)
                    {
                        case SQLite.Net.Interop.Result.Error:
                            if (ex.Message.Equals(conMessage))
                                RoutesBufferInsert.AddRange(BufferttoInsert);
                            else
                                throw;

                            break;

                        case SQLite.Net.Interop.Result.Busy:
                        case SQLite.Net.Interop.Result.Locked:
                            RoutesBufferInsert.AddRange(BufferttoInsert);
                            break;

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
                    await GetConnectionAsync().UpdateAllAsync(BuffertoUpdate);
                }
                catch (SQLiteException ex)
                {
                    switch (ex.Result)
                    {
                        case SQLite.Net.Interop.Result.Error:
                            if (ex.Message.Equals(conMessage))
                                RoutesBufferUpdate.AddRange(BuffertoUpdate);
                            else
                                throw;

                            break;

                        case SQLite.Net.Interop.Result.Busy:
                        case SQLite.Net.Interop.Result.Locked:
                            RoutesBufferUpdate.AddRange(BuffertoUpdate);
                            break;

                        default:
                            throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                var fecha = routes.Max(p => p.GetLastDate);

                await repoSincro.InsertOrReplaceAsync(new Syncro()
                {
                    IsDaily = false,
                    LastSync = fecha,
                    Sync = false,
                    Tabla = Syncro.Tables.ProductsRoutes
                });

                return routes.Count();
            }

            return 0;
        }

        public async Task CreateSyncro(Boolean value)
        {
            var repoSyncro = new RepositorySyncro(this.Connection);
            await repoSyncro.UpdateTableAsync(Syncro.Tables.ProductsRoutes, value);
        }

        public Task<string> InsertOrUpdateAsyncSql(ProductsRoutes[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
