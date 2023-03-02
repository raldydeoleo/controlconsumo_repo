using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Lot;
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    internal class RepositoryLots : RepositoryBase, IRepository<Lots>
    {
        public RepositoryLots(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryLots(MyDbConnection connection) : base(connection) { }

        public Task<Lots> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Lots>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Lots model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Lots> models)
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

        public Task<bool> InsertOrReplaceAsync(Lots models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Lots> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Lots model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Lots> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Lots model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<Lots> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsync(bool procesarSap)
        {
            try
            {
                IsSyncing = true;

                var repoSyncro = new RepositorySyncro(this.Connection);

                var LastLot = await repoSyncro.GetAsyncByKey(Syncro.Tables.Lots);
                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Lots, Fecha = LastLot.LastSync };

                var url = GetService(ServicesType.GET_BATCHES, false, LastLot);

                var json = await GetJsonAsync(url);

                Synclog.SizeBajada = json.SizePackageDownloading;

                if (json.isOk)
                    Synclog.RegistrosBajada = await InsertCommon(json.Json, false);
                else
                    throw json.ex;

                SyncMonitor.Detalle.Add(Synclog);

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

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var id = await con.Table<Lots>().CountAsync();

            if (id > 0) return false;

            var url = GetService(ServicesType.GET_BATCHES, true, null, isItForInitialSync);

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

        public Task<bool> CreateIndexAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }

        #region Common Methods

        public async Task<Int32> InsertCommon(String Json, Boolean IsInitialSync)
        {
            DateTime? fechamax = null;

            if (!Json.IsJsonEmpty())
            {
                var Lots = JsonConvert.DeserializeObject<LotsResult[]>(Json);
                var repoMaterial = new RepositoryMaterials(this.Connection);
                var repoz = new RepositoryZ(this.Connection);
                //var AllMaterial = await repoMaterial.GetAsyncAll();

                var bufferNewLots = new List<Lots>();
                var bufferExistingLots = new List<Lots>();

                foreach (var lot in Lots)
                {
                    var lote = new Lots()
                    {
                        Code = lot.charg,
                        Reference = lot.licha,
                        Created = GetDatetime(lot.ersda).Value,// DateTime.ParseExact(lot.ersda.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture),
                        MaterialCode = lot.matnr // AllMaterial.Single(p => p.Code == lot.matnr).ID
                    };

                    var Value = GetDatetime(lot.lwedt);

                    if (Value.HasValue)
                    {
                        lote.LastReceived = Value.Value; //DateTime.ParseExact(lot.lwedt.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    }

                    Value = GetDatetime(lot.vfdat);

                    if (Value.HasValue)
                    {
                        lote.Expire = Value.Value; //DateTime.ParseExact(lot.vfdat.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    }

                    Value = GetDatetime(lot.laeda);

                    if (Value.HasValue)
                    {
                        lote.Updated = Value.Value; //DateTime.ParseExact(lot.laeda.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    }

                    if (!IsInitialSync)
                    {
                        var Intentado = false;

                    VolverAValidar:

                        if (Intentado) await Task.Delay(Task_Delay);

                        try
                        {
                            var exist = await GetConnectionAsync().Table<Lots>().Where(p => p.MaterialCode == lote.MaterialCode && p.Code == lote.Code).FirstOrDefaultAsync();

                            if (exist == null)
                            {
                                bufferNewLots.Add(lote);
                            }
                            else
                            {
                                bufferExistingLots.Add(lote);
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
                                        goto VolverAValidar;
                                    }
                                    else
                                        throw;

                                case SQLite.Net.Interop.Result.Busy:
                                case SQLite.Net.Interop.Result.Locked:
                                    Intentado = true;
                                    goto VolverAValidar;

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
                        bufferNewLots.Add(lote);
                    }
                }

                if (bufferNewLots.Any())
                {
                    await InsertAsyncAll(bufferNewLots);

                    fechamax = bufferNewLots.Max(p => p.Updated ?? p.Created);

                    var reposincro = new RepositorySyncro(this.Connection);

                    var sincro = new Syncro()
                    {
                        LastSync = DateTime.Now.Date > fechamax ? DateTime.Now.Date : fechamax.Value,
                        Sync = false,
                        Tabla = Syncro.Tables.Lots
                    };

                    await reposincro.InsertOrReplaceAsync(sincro);
                }

                if (bufferExistingLots.Any())
                {
                    await UpdateAllAsync(bufferExistingLots);

                    fechamax = bufferExistingLots.Max(p => p.Updated ?? p.Created);

                    var reposincro = new RepositorySyncro(this.Connection);

                    var sincro = new Syncro()
                    {
                        LastSync = DateTime.Now.Date > fechamax ? DateTime.Now.Date : fechamax.Value,
                        Sync = false,
                        Tabla = Syncro.Tables.Lots
                    };

                    await reposincro.InsertOrReplaceAsync(sincro);
                }


                return bufferNewLots.Count() + bufferExistingLots.Count();
            }

            return 0;
        }

        public Task<string> InsertOrUpdateAsyncSql(Lots[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
