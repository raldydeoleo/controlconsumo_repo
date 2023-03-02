using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Inventory;
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
    internal class RepositoryInventories : RepositoryBase, IRepository<Inventories>
    {
        public RepositoryInventories(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryInventories(MyDbConnection connection) : base(connection) { }

        public Task<Inventories> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inventories>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Inventories model)
        {
            var Intentado = false;

        VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

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

            return true;
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Inventories> models)
        {
            var Intentado = false;

        VolverAIntentar:

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

            return true;
        }

        public async Task<bool> InsertOrReplaceAsync(Inventories models)
        {
            var Intentado = false;

        VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().InsertOrReplaceAsync(models);
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

            return true;
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Inventories> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Inventories model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Inventories> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Inventories model)
        {
            var Intentado = false;

        VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

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

            return true;
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<Inventories> models)
        {
            var Intentado = false;

        VolverAIntentar:

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

            return true;
        }

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            var url = GetService(ServicesType.POST_LOGIC_INVENTORY);

            var Intentado = false;
            IEnumerable<Inventories> pendientes = null;
            var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Inventories, Fecha = DateTime.Now };

        VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                pendientes = await GetConnectionAsync().Table<Inventories>().Where(p => p.Sync).ToListAsync();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                            goto VolverAIntentar;
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

            if (pendientes.Any())
            {
                var repoz = new RepositoryZ(this.Connection);

                var Proceso = await repoz.GetProces();

                var buffer = pendientes.Select(p => new InventoriesRequest()
                {
                    CHARG = p.Lot,
                    IDEQUIPO = Proceso.EquipmentID,
                    IDPROCESS = Proceso.Process,
                    WERKS = Proceso.Centro,
                    MATNR = p.MaterialCode,
                    BOXNO = p.BoxNumber,
                    MENGE = p.Quantity.Round3(),
                    MEINS = p.Unit
                }).ToList();

                var json = await PostJsonAsync(url, buffer);

                if (!json.isOk) throw json.ex;

                Synclog.RegistrosSubida = buffer.Count();
                Synclog.SizeSubida = json.SizePackageUploading;

                foreach (var item in pendientes)
                {
                    item.Sync = false;
                }

                await UpdateAllAsync(pendientes);

                SyncMonitor.Detalle.Add(Synclog);

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

                var count = await con.Table<Inventories>().CountAsync();

                if (count > 0) return false;

                var url = GetService(ServicesType.POST_LOGIC_INVENTORY, true, null, isItForInitialSync);

                var json = await GetJsonAsync(url);

                if (json.isOk && !json.Json.IsJsonEmpty())
                {
                    var inventarios = JsonConvert.DeserializeObject<InventoriesRequest[]>(json.Json);

                    var buffer = inventarios.Select(p => new Inventories
                    {
                        Lot = p.CHARG,
                        MaterialCode = p.MATNR,
                        BoxNumber = p.BOXNO,
                        Quantity = p.MENGE.Round3(),
                        Sync = false
                    }).ToList();

                    await InsertAsyncAll(buffer);

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

        public async Task UpdateInvetory(String MaterialCode, String Lot, Int16 BoxNumber, Single Quantity, String Unit)
        {
            var repoInventory = new RepositoryInventories(this.Connection);

            Inventories stock = null;

            var Intentado = false;

        VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                stock = await GetConnectionAsync().Table<Inventories>().Where(p => p.MaterialCode == MaterialCode && p.Lot == Lot && p.BoxNumber == BoxNumber).FirstOrDefaultAsync();
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

            if (stock != null)
            {
                stock.Sync = true;
                stock.Quantity = (Single)Math.Round(stock.Quantity + Quantity, 3);
                await repoInventory.UpdateAsync(stock);
            }
            else
            {
                stock = new Inventories()
                {
                    Sync = true,
                    Lot = Lot,
                    MaterialCode = MaterialCode,
                    Quantity = Quantity,
                    BoxNumber = BoxNumber,
                    Unit = Unit
                };
                await repoInventory.InsertAsync(stock);
            }
        }

        public Task<string> InsertOrUpdateAsyncSql(Inventories[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
