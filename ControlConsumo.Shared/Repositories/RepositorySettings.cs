using ControlConsumo.Shared.Interfaces;
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
    internal class RepositorySettings : RepositoryBase, IRepository<Settings>
    {
        private Boolean WasExecute = false;

        public static List<Settings> _Buffer = new List<Settings>();

        public RepositorySettings(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositorySettings(MyDbConnection connection) : base(connection) { }

        public async Task<Settings> GetAsyncByKey(object key)
        {
            try
            {
                var all = await GetAsyncAll();

                return all.First(p => p.Key == (Settings.Params)key);
            }
            catch (Exception)
            {
                return new Settings();
            }
        }

        public async Task<IEnumerable<Settings>> GetAsyncAll()
        {
            var Intentado = false;

            VolverAInsertar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                if (_Buffer == null || !_Buffer.Any()) _Buffer = await GetConnectionAsync().Table<Settings>().ToListAsync();

                return _Buffer;
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
        }

        public async Task<bool> InsertAsync(Settings model)
        {

            VolvelaIntentar:

            if (WasExecute) await Task.Delay(2000);

            try
            {
                await GetConnectionAsync().InsertAsync(model);
                _Buffer.Clear();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            WasExecute = true;
                            goto VolvelaIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        WasExecute = true;
                        goto VolvelaIntentar;

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

        public async Task<bool> InsertAsyncAll(IEnumerable<Settings> models)
        {

            VolvelaIntentar:

            if (WasExecute) await Task.Delay(2000);

            try
            {
                await GetConnectionAsync().InsertAllAsync(models);
                _Buffer.Clear();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            WasExecute = true;
                            goto VolvelaIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        WasExecute = true;
                        goto VolvelaIntentar;

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

        public async Task<bool> InsertOrReplaceAsync(Settings models)
        {

            VolvelaIntentar:

            if (WasExecute) await Task.Delay(2000);

            try
            {
                await GetConnectionAsync().InsertOrReplaceAsync(models);
                _Buffer.Clear();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            WasExecute = true;
                            goto VolvelaIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        WasExecute = true;
                        goto VolvelaIntentar;

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

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Settings> models)
        {

            VolvelaIntentar:

            if (WasExecute) await Task.Delay(2000);

            try
            {
                await GetConnectionAsync().InsertOrReplaceAllAsync(models);
                _Buffer.Clear();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            WasExecute = true;
                            goto VolvelaIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        WasExecute = true;
                        goto VolvelaIntentar;

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

        public Task<bool> DeleteAsync(Settings model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Settings> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Settings model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<Settings> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsync(bool procesarSAP)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAsync()
        {
            await GetConnectionAsync().CreateTableAsync<Settings>();
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

        public Task<bool> SyncAsyncTwoWay()
        {
            throw new NotImplementedException();
        }
        
        public Task<string> InsertOrUpdateAsyncSql(Settings[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
