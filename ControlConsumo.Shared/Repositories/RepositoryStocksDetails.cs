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
    internal class RepositoryStocksDetails : RepositoryBase, IRepository<StocksDetails>
    {
        public RepositoryStocksDetails(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryStocksDetails(MyDbConnection connection) : base(connection) { }

        public Task<StocksDetails> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StocksDetails>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(StocksDetails model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<StocksDetails> models)
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

            return true;
        }

        public Task<bool> InsertOrReplaceAsync(StocksDetails models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<StocksDetails> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(StocksDetails model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<StocksDetails> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(StocksDetails model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<StocksDetails> models)
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

        public Task<bool> SyncAsync(bool procesarSAP)
        {
            throw new NotImplementedException();
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

        public Task<string> InsertOrUpdateAsyncSql(StocksDetails[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
