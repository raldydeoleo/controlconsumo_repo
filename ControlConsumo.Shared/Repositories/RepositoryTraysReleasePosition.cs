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
    internal class RepositoryTraysReleasePosition : RepositoryBase, IRepository<TraysReleasePosition>
    {
        private static readonly List<TraysReleasePosition> TraysReleasePositionBufferInsert = new List<TraysReleasePosition>();

        public RepositoryTraysReleasePosition(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTraysReleasePosition(MyDbConnection connection) : base(connection) { }

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            var Count = TraysReleasePositionBufferInsert.Count();

            try
            {
                if (TraysReleasePositionBufferInsert.Any())
                {
                    await connection.InsertAllAsync(TraysReleasePositionBufferInsert);
                    TraysReleasePositionBufferInsert.Clear();
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

        public Task<TraysReleasePosition> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TraysReleasePosition>> GetAsyncAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(TraysReleasePosition model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<TraysReleasePosition> models)
        {
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
                            TraysReleasePositionBufferInsert.AddRange(models);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        TraysReleasePositionBufferInsert.AddRange(models);
                        break;

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

        public Task<bool> InsertOrReplaceAsync(TraysReleasePosition models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<TraysReleasePosition> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TraysReleasePosition model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<TraysReleasePosition> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TraysReleasePosition model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<TraysReleasePosition> models)
        {
            throw new NotImplementedException();
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
        
        public Task<string> InsertOrUpdateAsyncSql(TraysReleasePosition[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
