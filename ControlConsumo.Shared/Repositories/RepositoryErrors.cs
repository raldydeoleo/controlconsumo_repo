using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Error;
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
    internal class RepositoryErrors : RepositoryBase, IRepository<Errors>
    {
        public RepositoryErrors(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryErrors(MyDbConnection connection) : base(connection) { }

        public Task<Errors> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Errors>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(Errors model)
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

        public Task<bool> InsertAsyncAll(IEnumerable<Errors> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsync(Errors models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Errors> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Errors model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Errors> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Errors model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<Errors> models)
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
            var Intentado = false;

            VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Errors, Fecha = DateTime.Now };

            IEnumerable<Errors> pending = null;

            try
            {
                pending = await GetConnectionAsync().Table<Errors>().Where(p => p.Sync).Take(MAX_ROWS).ToListAsync();
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

            if (pending.Any())
            {
                var url = GetService(ServicesType.POST_ERRORS, false);

                var buffer = pending.Select(p => new ErrorsRequest
                {
                    IDPROCESS = p.ProcessID,
                    WERKS = p.Center,
                    IDEQUIPO = p.EquipmentID,
                    IDTIEMPO = p.TimeID,
                    MATNR = p.ProductCode,
                    VERID = p.VerID,
                    FECHA = p.Produccion.GetSapDateL(),
                    HORA = p.Produccion.GetSapHoraL(),
                    IDTURNO = p.TurnID,
                    MATNR2 = p.MaterialCode,
                    IDEQUIPO2 = p.SubEquipmentID ?? String.Empty,
                    IDBANDEJA = p.TrayID ?? String.Empty,
                    CHARG = p.Lot,
                    MENGE = p.Quantity,
                    MEINS = p.Unit,
                    USNAM = p.Logon,
                    ERRORID = (short)p.Message,
                    CPUDT = DateTime.Now.GetSapDate(),
                    CPUTM = DateTime.Now.GetSapHora()
                }).ToList();

                var json = await PostJsonAsync(url, buffer);

                if (!json.isOk) throw json.ex;

                Synclog.RegistrosSubida = buffer.Count();
                Synclog.SizeSubida = json.SizePackageUploading;

                foreach (var item in pending)
                {
                    item.Sync = false;
                }

                await UpdateAllAsync(pending);

                SyncMonitor.Detalle.Add(Synclog);
            }

            return true;
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

        public Task<string> InsertOrUpdateAsyncSql(Errors[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
