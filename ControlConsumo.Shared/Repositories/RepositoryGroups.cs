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
    internal class RepositoryGroups : RepositoryBase, IRepository<Groups>
    {
        public RepositoryGroups(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryGroups(MyDbConnection connection) : base(connection) { }

        public Task<Groups> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Groups>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Groups model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsyncAll(IEnumerable<Groups> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsync(Groups models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Groups> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Groups model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Groups> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Groups model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<Groups> models)
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
            await GetConnectionAsync().CreateTableAsync<Groups>();
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

        public Task<string> InsertOrUpdateAsyncSql(Groups[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
