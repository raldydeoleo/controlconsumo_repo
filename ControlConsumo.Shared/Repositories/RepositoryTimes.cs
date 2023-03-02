using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Time;
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
    internal class RepositoryTimes : RepositoryBase, IRepository<Times>
    {
        public RepositoryTimes(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTimes(MyDbConnection connection) : base(connection) { }

        private static List<Times> BufferTimes { get; set; }

        public async Task<Times> GetAsyncByKey(object key)
        {
            var Intentado = false;

            VolverActualizar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var all = await GetAsyncAll();
                return all.FirstOrDefault(p => Convert.ToInt32(p.ID) == Convert.ToInt32(key));
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverActualizar;
                        }
                        else
                            throw;

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
        }

        public async Task<IEnumerable<Times>> GetAsyncAll()
        {
            var Intentado = false;

            VolverActualizar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                if (BufferTimes == null || !BufferTimes.Any())
                {
                    BufferTimes = await GetConnectionAsync().Table<Times>().ToListAsync();
                }

                return BufferTimes;
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverActualizar;
                        }
                        else
                            throw;

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
        }

        public Task<bool> InsertAsync(Times model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Times> models)
        {
            var Intentado = false;

            VolverActualizar:

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
                            goto VolverActualizar;
                        }
                        else
                            throw;

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

            BufferTimes = null;

            return true;
        }

        public Task<bool> InsertOrReplaceAsync(Times models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Times> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Times model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Times> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Times model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<Times> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            return await SyncAsyncAll(false);
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var url = GetService(ServicesType.GET_TIMES, true, null, isItForInitialSync);

            var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Times, Fecha = DateTime.Now };

            var json = await GetJsonAsync(url);

            if (json.isOk && !json.Json.IsJsonEmpty())
            {
                var tiempos = JsonConvert.DeserializeObject<TimeResult[]>(json.Json);

                Synclog.RegistrosBajada = tiempos.Count();
                Synclog.SizeBajada = json.SizePackageDownloading;

                var buffer = tiempos.Where(w => !String.IsNullOrEmpty(w.idtiempo)).Select(p => new Times
                {
                    ID = p.idtiempo.PadLeft(2, '0'),
                    Time = p.tiempo,
                    Group = p.matkl,
                    Min = p.Maxima,
                    Valid_Out = !String.IsNullOrEmpty(p.ValidOut),
                    Producto = Get_Tipo(p.Producto),
                    Copias = p.Copias
                }).ToList();

                if (!buffer.Any()) return false;

                var Intentado = false;

                VolverABorrar:

                if (Intentado) await Task.Delay(Task_Delay);

                try
                {
                    await GetConnectionAsync().DeleteAllAsync<Times>();
                }
                catch (SQLiteException ex)
                {
                    switch (ex.Result)
                    {
                        case SQLite.Net.Interop.Result.Error:
                            if (ex.Message.Equals(conMessage))
                            {
                                Intentado = true;
                                goto VolverABorrar;
                            }
                            else
                                throw;

                        case SQLite.Net.Interop.Result.Busy:
                        case SQLite.Net.Interop.Result.Locked:
                            Intentado = true;
                            goto VolverABorrar;

                        default:
                            throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                await InsertAsyncAll(buffer);

                SyncMonitor.Detalle.Add(Synclog);
            }
            else if (!json.isOk)
            {
                throw json.ex;
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

        public Task<bool> CreateIndexAsync()
        {
            throw new NotImplementedException();
        }


        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        private Times.ProductTypes Get_Tipo(String Producto)
        {
            switch ((Producto ?? String.Empty))
            {
                case "N": return Times.ProductTypes.None;
                case "S": return Times.ProductTypes.Validar_Salida;
                case "T": return Times.ProductTypes.Validar_Tipo_Almacenamiento;
                case "C": return Times.ProductTypes.Validar_Salida_y_Tipo_Almacenamiento;
                default: return Times.ProductTypes.None;
            }
        }

        public Task<string> InsertOrUpdateAsyncSql(Times[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
