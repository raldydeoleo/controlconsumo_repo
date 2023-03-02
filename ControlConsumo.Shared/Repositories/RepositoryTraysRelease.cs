using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Tray;
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
    internal class RepositoryTraysRelease : RepositoryBase, IRepository<TraysRelease>
    {
        private static readonly List<TraysRelease> TraysReleaseBufferInsert = new List<TraysRelease>();

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            var Count = TraysReleaseBufferInsert.Count();

            try
            {
                if (TraysReleaseBufferInsert.Any())
                {
                    await connection.InsertAllAsync(TraysReleaseBufferInsert);
                    TraysReleaseBufferInsert.Clear();
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

            return Count;
        }

        public RepositoryTraysRelease(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTraysRelease(MyDbConnection connection) : base(connection) { }

        public Task<TraysRelease> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TraysRelease>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(TraysRelease model)
        {
            var repopos = new RepositoryTraysReleasePosition(this.Connection);
            var reposincro = new RepositorySyncro(this.Connection);

            var buffer = new List<TraysReleasePosition>();

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
                            TraysReleaseBufferInsert.Add(model);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        TraysReleaseBufferInsert.Add(model);
                        break;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            foreach (var item in model.Positions)
            {
                item.TraysReleaseID = model.ID;
                buffer.Add(item);
            }

            await repopos.InsertAsyncAll(buffer);

            return true;
        }

        public Task<bool> InsertAsyncAll(IEnumerable<TraysRelease> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsync(TraysRelease models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<TraysRelease> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TraysRelease model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<TraysRelease> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TraysRelease model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<TraysRelease> models)
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
            var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.TrayRelease, Fecha = DateTime.Now };

            var url = GetService(ServicesType.POST_RELEASE, false, new Syncro() { LastSync = DateTime.Now });

            IEnumerable<TraysRelease> detalle = null;

            var intentado = false;

            VolveraIntentar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                detalle = await GetConnectionAsync().Table<TraysRelease>().Where(p => p.Sync).Take(MAX_ROWS).ToListAsync();
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

            if (detalle.Any())
            {
                var buffer = new List<TrayRequest>();

                foreach (var item in detalle)
                {
                    var newOne = new TrayRequest()
                    {
                        CPUDT = item.Fecha.GetSapDateL(),
                        CPUTM = item.Fecha.GetSapHoraL(),
                        USNAM = item.Logon
                    };

                    IEnumerable<TraysReleasePosition> posiciones = null;

                    intentado = false;

                    VolveraIntentarDetalle:

                    if (intentado) await Task.Delay(Task_Delay);

                    try
                    {
                        posiciones = await GetConnectionAsync().Table<TraysReleasePosition>().Where(p => p.TraysReleaseID == item.ID).ToListAsync();
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                {
                                    intentado = true;
                                    goto VolveraIntentarDetalle;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                intentado = true;
                                goto VolveraIntentarDetalle;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    newOne.POSICIONES = posiciones.Select(p => new TrayRequest.Position { IDBANDEJA = p.TrayID }).ToList();

                    buffer.Add(newOne);

                    item.Sync = false;
                }

                var json = await PostJsonAsync(url, buffer);

                if (!json.isOk) throw json.ex;

                Synclog.RegistrosSubida = buffer.Sum(c => c.POSICIONES.Count());
                Synclog.SizeBajada = json.SizePackageUploading;

                await UpdateAllAsync(detalle);

                SyncMonitor.Detalle.Add(Synclog);

                return true;
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
        
        public Task<string> InsertOrUpdateAsyncSql(TraysRelease[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
