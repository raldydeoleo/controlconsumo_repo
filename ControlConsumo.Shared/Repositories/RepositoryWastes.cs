using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Waste;
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
    internal class RepositoryWastes : RepositoryBase, IRepository<Wastes>
    {
        public RepositoryWastes(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryWastes(MyDbConnection connection) : base(connection) { }

        public Task<Wastes> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Wastes>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Wastes model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Wastes> models)
        {
            var intentado = false;

            VolveraIntentar:

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

        public Task<bool> InsertOrReplaceAsync(Wastes models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Wastes> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Wastes model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Wastes> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Wastes model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<Wastes> models)
        {
            var intentado = false;

            VolveraIntentar:

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

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            var url = GetService(ServicesType.POST_WASTES);
            IEnumerable<Wastes> pendientes = null;
            var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Wastes, Fecha = DateTime.Now };

            var intentado = false;

            VolveraIntentar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                pendientes = await GetConnectionAsync().Table<Wastes>().Where(p => p.Sync).Take(MAX_ROWS).ToListAsync();
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
                var buffer = pendientes.Select(p => new WastesRequests
                {
                    BOXNO = p.BoxNumber,
                    CHARG = p.Lot,
                    CPUDT = DateTime.Now.GetSapDate(),
                    CPUTM = DateTime.Now.GetSapHora(),
                    FECHA = p.Fecha.GetSapDateL(),
                    HORA = p.Fecha.GetSapHoraL(),
                    ID = (Byte)p.StockID,
                    IDEQUIPO = p.Equipment,
                    IDEQUIPO2 = p.SubEquipment,
                    IDPROCESS = p.ProcessID,
                    IDTIEMPO = p.TimeID,
                    IDTURNO = p.TurnID,
                    MATNR = p.ProductCode,
                    MATNR2 = p.MaterialCode,
                    MEINS = p.Unit,
                    MENGE = p.Quantity,
                    USNAM = p.Logon,
                    VERID = p.VerID,
                    WERKS = p.Center,
                    VFDAT = p.Expire.GetSapDateL(),
                    CHARG1 = p.LoteSuplidor
                }).ToList();

                var json = await PostJsonAsync(url, buffer);

                if (!json.isOk) throw json.ex;

                Synclog.RegistrosSubida = pendientes.Count();
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
            var url = GetService(ServicesType.POST_WASTES, true, new Syncro() { LastSync = DateTime.Now.AddDays(-30) }, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk && !json.Json.IsJsonEmpty())
            {
                var desperdicios = JsonConvert.DeserializeObject<WastesRequests[]>(json.Json);

                var buffer = desperdicios.Select(p => new Wastes
                {
                    BoxNumber = p.BOXNO,
                    Center = p.WERKS,
                    CustomFecha = Convert.ToInt32(GetDatetime(p.FECHA, p.HORA).Value.GetSapDate()),
                    Equipment = p.IDEQUIPO,
                    Fecha = GetDatetime(p.FECHA, p.HORA).Value,
                    Logon = p.USNAM,
                    Lot = p.CHARG,
                    MaterialCode = p.MATNR2,
                    ProcessID = p.IDPROCESS,
                    ProductCode = p.MATNR,
                    Quantity = p.MENGE,
                    StockID = p.ID,
                    SubEquipment = p.IDEQUIPO2,
                    Sync = false,
                    TimeID = p.IDTIEMPO,
                    TurnID = p.IDTURNO,
                    Unit = p.MEINS,
                    VerID = p.VERID
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

        public Task<string> InsertOrUpdateAsyncSql(Wastes[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
