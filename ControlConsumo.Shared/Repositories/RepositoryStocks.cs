using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Json;
using ControlConsumo.Shared.Models.Stock;
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
    internal class RepositoryStocks : RepositoryBase, IRepository<Stocks>
    {
        public RepositoryStocks(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryStocks(MyDbConnection connection) : base(connection) { }

        public async Task<Stocks> GetAsyncByKey(object key)
        {
            var intentado = false;

            VolveraInsertar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().GetAsync<Stocks>(key);
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
        }

        public async Task<IEnumerable<Stocks>> GetAsyncAll()
        {
            var intentado = false;

            VolveraInsertar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Stocks>().ToListAsync();
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
        }

        public async Task<bool> InsertAsync(Stocks model)
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

            await CreateSyncro(true);

            return true;
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Stocks> models)
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

            await CreateSyncro(true);

            return true;
        }

        public Task<bool> InsertOrReplaceAsync(Stocks models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Stocks> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Stocks model)
        {
            var intentado = false;

            VolveraInsertar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().DeleteAsync(model);
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

        public Task<bool> DeleteAllAsync(IEnumerable<Stocks> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Stocks model)
        {
            var intentado = false;

            VolveraInsertar:

            if (intentado) await Task.Delay(Task_Delay);

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

            await CreateSyncro(true);

            return true;
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<Stocks> models)
        {
            var intentado = false;

            VolveraInsertar:

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

            await CreateSyncro(true);

            return true;
        }

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            var intentado = false;
            IEnumerable<Stocks> detalle = null;

            VolveraLeer:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                detalle = await GetConnectionAsync().Table<Stocks>().Where(p => p.Sync).Take(MAX_ROWS).ToListAsync();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            intentado = true;
                            goto VolveraLeer;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraLeer;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (detalle.Any())
            {
                var buffer = new List<StockRequest>();

                foreach (var item in detalle)
                {
                    var nuevo = new StockRequest()
                    {
                        IDCORTE = item.CustomID,
                        IDPROCESS = item.ProcessID,
                        WERKS = item.Center,
                        IDEQUIPO = item.Equipment,
                        FECHA = item.Begin.GetSapDateL(),
                        HORAMIN = item.Begin.GetSapHoraL(),
                        HORAMAX = item.End.GetSapHoraL(),
                        IDTURNO = item.TurnID,
                        STTURNO = item.Status == Stocks._Status.Abierto ? "A" : "C",
                        CPUDT = DateTime.Now.GetSapDate(),
                        CPUTM = DateTime.Now.GetSapHora(),
                        USNAM = item.Logon,
                        NOTIFICADO = item.IsNotified ? "X" : String.Empty
                    };

                    var result = await GetConnectionAsync().Table<StocksDetails>()
                    .Where(d => d.StockID == item.ID)
                    .ToListAsync();

                    nuevo.DETALLE = result.Select(d => new StockRequest.Detail
                    {
                        ID = item.CustomID,
                        IDPROCESS = item.ProcessID,
                        WERKS = item.Center,
                        IDEQUIPO = item.Equipment,
                        IDTIEMPO = item.TimeID,
                        FECHA = item.Begin.GetSapDateL(),
                        MATNR = item.ProductCode,
                        VERID = item.VerID,
                        HORA = item.Begin.GetSapHoraL(),
                        IDTURNO = item.TurnID,
                        MATNR2 = d.MaterialCode,
                        IDEQUIPO2 = item.SubEquipment ?? String.Empty,
                        CHARG = d.Lot,
                        MENGE = d.Quantity,
                        MEINS = d.Unit,
                        MENGE2 = d.Quantity2,
                        BOXNO = d.BoxNumber,
                        CPUDT = DateTime.Now.GetSapDate(),
                        CPUTM = DateTime.Now.GetSapHora(),
                        USNAM = item.Logon
                    });

                    buffer.Add(nuevo);
                }

                var url = GetService(ServicesType.POST_STOCKS, false, new Syncro() { LastSync = DateTime.Now });

                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Stocks, Fecha = DateTime.Now };

                var json = await PostJsonAsync(url, buffer);

                Synclog.SizeBajada = json.SizePackageDownloading;
                Synclog.SizeSubida = json.SizePackageUploading;
                Synclog.RegistrosSubida = buffer.Count();

                if (json.isOk && !json.Json.IsJsonEmpty())
                {
                    var recivido = JsonConvert.DeserializeObject<StockResult[]>(json.Json);

                    foreach (var item in recivido)
                    {
                        var first = detalle.First(p => p.CustomFecha.ToString() == item.FECHA && p.TurnID == item.IDTURNO);
                        first.CustomID = item.IDCORTE;
                        first.Sync = false;
                    }

                    Synclog.RegistrosBajada = recivido.Count();

                    await UpdateAllAsync(detalle);
                }
                else if (json.isOk)
                {
                    intentado = false;

                    var query = String.Format("UPDATE Stocks SET Sync = 0 WHERE ID IN ({0});", detalle.Select(p => p.ID).GetInt32Enumerable());

                    VolveraEjecutar:

                    if (intentado) await Task.Delay(Task_Delay);

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
                                    intentado = true;
                                    goto VolveraEjecutar;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                intentado = true;
                                goto VolveraEjecutar;

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
                    throw json.ex;
                }

                SyncMonitor.Detalle.Add(Synclog);

                return true;
            }

            return false;
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var url = GetService(ServicesType.POST_STOCKS, true, new Syncro() { LastSync = DateTime.Now.AddDays(-15) }, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json, true);
            else
                throw json.ex;

            return true;
        }

        public async Task<bool> CreateAsync()
        {
            await GetConnectionAsync().CreateTableAsync<Stocks>();
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

                IEnumerable<Stocks> detalle = null;
                var repoSyncro = new RepositorySyncro(this.Connection);

                var LastUpdate = await repoSyncro.GetAsyncByKey(Syncro.Tables.Stocks);

                var url = GetService(ServicesType.POST_STOCKS, false, LastUpdate);

                JResult json = null;

                if (!LastUpdate.Sync)
                {
                    json = await GetJsonAsync(url);
                }
                else
                {
                    var intentado = false;

                    VolveraLeer:

                    if (intentado) await Task.Delay(Task_Delay);

                    try
                    {
                        detalle = await GetConnectionAsync().Table<Stocks>().Where(p => p.Sync).Take(50).ToListAsync();
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                    goto VolveraLeer;
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                intentado = true;
                                goto VolveraLeer;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    var buffer = new List<StockRequest>();

                    if (detalle.Any())
                    {
                        foreach (var item in detalle)
                        {
                            var nuevo = new StockRequest()
                            {
                                IDCORTE = item.CustomID,
                                IDPROCESS = item.ProcessID,
                                WERKS = item.Center,
                                IDEQUIPO = item.Equipment,
                                FECHA = item.Begin.GetSapDateL(),
                                HORAMIN = item.Begin.GetSapHoraL(),
                                HORAMAX = item.End.GetSapHoraL(),
                                IDTURNO = item.TurnID,
                                STTURNO = item.Status == Stocks._Status.Abierto ? "A" : "C",
                                CPUDT = DateTime.Now.GetSapDate(),
                                CPUTM = DateTime.Now.GetSapHora(),
                                USNAM = item.Logon,
                                NOTIFICADO = item.IsNotified ? "X" : String.Empty
                            };

                            IEnumerable<StocksDetails> result = null;
                            intentado = false;

                            VolveraLeerDetalle:

                            if (intentado) await Task.Delay(Task_Delay);

                            try
                            {
                                result = await GetConnectionAsync().Table<StocksDetails>()
                               .Where(d => d.StockID == item.ID)
                               .ToListAsync();
                            }
                            catch (SQLiteException ex)
                            {
                                switch (ex.Result)
                                {
                                    case SQLite.Net.Interop.Result.Error:
                                        if (ex.Message.Equals(conMessage))
                                            goto VolveraLeerDetalle;
                                        else
                                            throw;

                                    case SQLite.Net.Interop.Result.Busy:
                                    case SQLite.Net.Interop.Result.Locked:
                                        intentado = true;
                                        goto VolveraLeerDetalle;

                                    default:
                                        throw;
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }

                            nuevo.DETALLE = result.Select(d => new StockRequest.Detail
                            {
                                ID = item.CustomID,
                                IDPROCESS = item.ProcessID,
                                WERKS = item.Center,
                                IDEQUIPO = item.Equipment,
                                IDTIEMPO = item.TimeID,
                                FECHA = item.Begin.GetSapDateL(),
                                MATNR = item.ProductCode,
                                VERID = item.VerID,
                                HORA = item.End.GetSapHoraL(),
                                IDTURNO = item.TurnID,
                                MATNR2 = d.MaterialCode,
                                IDEQUIPO2 = item.SubEquipment ?? String.Empty,
                                CHARG = d.Lot,
                                MENGE = d.Quantity,
                                MENGE2 = d.Quantity2,
                                MEINS = d.Unit,
                                BOXNO = d.BoxNumber,
                                CPUDT = DateTime.Now.GetSapDate(),
                                CPUTM = DateTime.Now.GetSapHora(),
                                USNAM = item.Logon
                            });

                            buffer.Add(nuevo);
                        }
                    }

                    json = await PostJsonAsync(url, buffer);
                }

                if (json.isOk)
                    await InsertCommon(json.Json, false);
                else
                    throw json.ex;

                if (detalle != null && detalle.Any())
                {
                    var query = String.Format("UPDATE Stocks SET Sync = 0 WHERE ID IN ({0});", detalle.Select(p => p.ID).GetInt32Enumerable());

                    var intentado = false;

                    VolveraEjecutar:

                    if (intentado) await Task.Delay(Task_Delay);

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
                                    intentado = true;
                                    goto VolveraEjecutar;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                intentado = true;
                                goto VolveraEjecutar;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

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

        #region Common Methods

        public async Task InsertCommon(String json, Boolean IsInitialSync)
        {
            try
            {
                if (!json.IsJsonEmpty())
                {
                    var stocks = JsonConvert.DeserializeObject<StockRequest[]>(json).OrderBy(s=>GetDatetime(s.FECHA, s.HORAMIN));

                    var buffertoInsert = new List<Stocks>();
                    var buffertoUpdate = new List<Stocks>();
                    var bufferDetalle = new List<StocksDetails>();

                    foreach (var item in stocks)
                    {
                        var newstock = new Stocks()
                        {
                            CustomID = item.IDCORTE,
                            CustomFecha = GetDatetime(item.FECHA, item.HORAMIN).Value.GetDBDate(),
                            Begin = GetDatetime(item.FECHA, item.HORAMIN).Value,
                            End = GetDatetime(item.FECHA, item.HORAMAX).Value,
                            Center = item.WERKS,
                            Equipment = item.IDEQUIPO,
                            ProcessID = item.IDPROCESS,
                            Logon = item.USNAM,
                            TurnID = item.IDTURNO,
                            Sync = false,
                            Status = item.STTURNO == "A" ? Stocks._Status.Abierto : Stocks._Status.Cerrado,
                            IsNotified = !String.IsNullOrEmpty(item.NOTIFICADO)
                        };

                        if (!IsInitialSync)
                        {
                            var intentado = false;
                            Stocks first = null;

                            VolveraLeer:

                            if (intentado) await Task.Delay(Task_Delay);

                            try
                            {
                                first = await GetConnectionAsync().Table<Stocks>().Where(p => p.TurnID == newstock.TurnID && p.CustomFecha == newstock.CustomFecha).FirstOrDefaultAsync();
                            }
                            catch (SQLiteException ex)
                            {
                                switch (ex.Result)
                                {
                                    case SQLite.Net.Interop.Result.Error:
                                        if (ex.Message.Equals(conMessage))
                                        {
                                            intentado = true;
                                            goto VolveraLeer;
                                        }
                                        else
                                            throw;

                                    case SQLite.Net.Interop.Result.Busy:
                                    case SQLite.Net.Interop.Result.Locked:
                                        intentado = true;
                                        goto VolveraLeer;

                                    default:
                                        throw;
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }

                            if (first == null)
                                buffertoInsert.Add(newstock);
                            else
                            {
                                first.CustomID = newstock.CustomID;
                                first.IsNotified = newstock.IsNotified;
                                first.Sync = false;
                                buffertoUpdate.Add(first);
                            }
                        }
                        else
                        {
                            var position = item.DETALLE.FirstOrDefault();

                            if (position != null)
                            {
                                newstock.TimeID = position.IDTIEMPO;
                                newstock.SubEquipment = position.IDEQUIPO2;
                                newstock.ProductCode = position.MATNR;
                                newstock.VerID = position.VERID;
                            }

                            buffertoInsert.Add(newstock);
                        }
                    }

                    var tratado = false;

                    VolveraInsertar:

                    if (tratado) await Task.Delay(Task_Delay);

                    try
                    {
                        if (buffertoInsert.Any()) await GetConnectionAsync().InsertAllAsync(buffertoInsert);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                {
                                    tratado = true;
                                    goto VolveraInsertar;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                tratado = true;
                                goto VolveraInsertar;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    tratado = false;

                    VolveraActualizar:

                    if (tratado) await Task.Delay(Task_Delay);

                    try
                    {
                        if (buffertoUpdate.Any()) await GetConnectionAsync().UpdateAllAsync(buffertoUpdate);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                {
                                    tratado = true;
                                    goto VolveraActualizar;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                tratado = true;
                                goto VolveraActualizar;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    if (IsInitialSync)
                    {
                        var newStock = stocks.Where(m => m.DETALLE.Any()).ToList();

                        foreach (var item in newStock)
                        {
                            var Insertado = buffertoInsert.First(p => p.CustomID == item.IDCORTE && p.CustomFecha == Convert.ToInt32(item.FECHA));

                            bufferDetalle.AddRange(item.DETALLE.Select(p => new StocksDetails
                            {
                                StockID = Insertado.ID,
                                MaterialCode = p.MATNR2,
                                Lot = p.CHARG,
                                BoxNumber = p.BOXNO,
                                Quantity = p.MENGE,
                                Quantity2 = p.MENGE2,
                                Unit = p.MEINS
                            }));
                        }

                        if (bufferDetalle.Any()) await new RepositoryStocksDetails(this.Connection).InsertAsyncAll(bufferDetalle);
                    }

                    var fecha = stocks.Max(p => GetDatetime(p.CPUDT, p.CPUTM).Value);

                    var sincro = new Syncro()
                    {
                        Tabla = Syncro.Tables.Stocks,
                        Sync = false,
                        LastSync = fecha
                    };

                    var reposincro = new RepositorySyncro(this.Connection);

                    await reposincro.InsertOrReplaceAsync(sincro);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateSyncro(Boolean value)
        {
            var repoSyncro = new RepositorySyncro(this.Connection);
            await repoSyncro.UpdateTableAsync(Syncro.Tables.Stocks, value);
        }

        public Task<string> InsertOrUpdateAsyncSql(Stocks[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
