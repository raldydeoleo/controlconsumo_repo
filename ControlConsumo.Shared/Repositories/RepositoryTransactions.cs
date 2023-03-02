using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Transaction;
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
    internal class RepositoryTransactions : RepositoryBase, IRepository<Transactions>
    {
        public RepositoryTransactions(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTransactions(MyDbConnection connection) : base(connection) { }

        public Task<Transactions> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transactions>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Transactions model)
        {
            var repoinv = new RepositoryInventories(this.Connection);
            var repoz = new RepositoryZ(this.Connection);
            var cantidad = await repoz.GetStockAvailableAsync(model.MaterialCode, model.Lot, model.BoxNumber);
            model.Total = (Single)Math.Round(cantidad + model.Quantity, 3);

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

            await repoinv.UpdateInvetory(model.MaterialCode, model.Lot, model.BoxNumber, model.Quantity, model.Unit);
            return true;
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Transactions> models)
        {
            var trans = models.GroupBy(g => new
            {
                g.MaterialCode,
                g.Lot,
                g.BoxNumber
            }).Select(s => s.First());

            var repoz = new RepositoryZ(this.Connection);
            var repoinv = new RepositoryInventories(this.Connection);

            foreach (var item in trans)
            {
                var Cantidad = await repoz.GetStockAvailableAsync(item.MaterialCode, item.Lot, item.BoxNumber);

                item.Total = (Single)Math.Round(item.Quantity + Cantidad, 3);

                if (item.Total < 0) return false;

                await repoinv.UpdateInvetory(item.MaterialCode, item.Lot, item.BoxNumber, item.Quantity, item.Unit);
            }

            var intentado = false;

            VolveraInsertar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().InsertAllAsync(trans);
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

        public Task<bool> InsertOrReplaceAsync(Transactions models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Transactions> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Transactions model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Transactions> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Transactions model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<Transactions> models)
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

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            if (IsSyncing) return false;

            var url = GetService(ServicesType.POST_LOGIC_INVENTORY_DET);
            IEnumerable<Transactions> pendientes = null;

            var intentado = false;

            VolveraIntentar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                pendientes = await GetConnectionAsync().Table<Transactions>().Where(p => p.Sync).ToListAsync();
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

            if (pendientes.Any())
            {
                var repoz = new RepositoryZ(this.Connection);

                var Proceso = await repoz.GetProces();

                var buffer = pendientes.Select((p, n) => new TransactionsRequest()
                {
                    CHARG = p.Lot,
                    CONCEPTO = p.Reason,
                    CPUDT = p.Fecha.GetSapDateL(),
                    CPUTM = p.Fecha.AddSeconds(n).GetSapHoraL(),
                    IDEQUIPO = Proceso.EquipmentID,
                    IDPROCESS = Proceso.Process,
                    IDTURNO = p.TurnID,
                    MATNR = p.MaterialCode,
                    MEINS = p.Unit,
                    MENGE = p.Quantity.Round3(),
                    MENGE2 = p.Total.Round3(),
                    UNAME = p.Logon,
                    WERKS = Proceso.Centro,
                    BOXNO = p.BoxNumber
                }).ToList();

                var json = await PostJsonAsync(url, buffer);

                if (!json.isOk) throw json.ex;

                foreach (var item in pendientes)
                {
                    item.Sync = false;
                }

                await UpdateAllAsync(pendientes);

                return true;
            }

            return false;
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            try
            {
                var con = GetConnectionAsync();

                var count = await con.Table<Transactions>().CountAsync();

                if (count > 0) return false;

                var url = GetService(ServicesType.POST_LOGIC_INVENTORY_DET, true, null, isItForInitialSync);

                var json = await GetJsonAsync(url);

                if (json.isOk && !json.Json.IsJsonEmpty())
                {
                    var inventarios = JsonConvert.DeserializeObject<TransactionsRequest[]>(json.Json);

                    var buffer = inventarios.Select(p => new Transactions
                    {
                        Lot = p.CHARG,
                        MaterialCode = p.MATNR,
                        Quantity = p.MENGE.Round3(),
                        Sync = false,
                        Fecha = GetDatetime(p.CPUDT, p.CPUTM).Value,
                        Logon = p.UNAME,
                        Reason = p.CONCEPTO,
                        TurnID = p.IDTURNO,
                        Unit = p.MEINS,
                        Total = p.MENGE2.Round3(),
                        BoxNumber = p.BOXNO,
                        CustomFecha = GetDatetime(p.CPUDT, p.CPUTM).Value.GetDBDate()
                    }).OrderBy(o => o.Fecha).ToList();

                    var intentado = false;

                    VolveraIntentar:

                    if (intentado) await Task.Delay(Task_Delay);

                    try
                    {
                        await con.InsertAllAsync(buffer);
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
                else if (!json.isOk)
                {
                    throw json.ex;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
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
        
        public Task<string> InsertOrUpdateAsyncSql(Transactions[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
