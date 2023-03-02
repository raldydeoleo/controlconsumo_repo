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
    internal class RepositorySyncro : RepositoryBase, IRepository<Syncro>
    {
        private Boolean WasExecute = false;
        private static readonly List<Syncro> SyncroBufferInsert = new List<Syncro>();
        private static readonly List<Syncro> SyncroBufferUpdate = new List<Syncro>();
        private static readonly List<Syncro> SyncroBufferInsertOrUpdate = new List<Syncro>();

        public RepositorySyncro(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositorySyncro(MyDbConnection connection) : base(connection) { }

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            var count = SyncroBufferInsert.Count() + SyncroBufferUpdate.Count() + SyncroBufferInsertOrUpdate.Count();

            try
            {
                if (SyncroBufferInsert.Any())
                {
                    await connection.InsertAllAsync(SyncroBufferInsert);
                    SyncroBufferInsert.Clear();
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

            try
            {
                if (SyncroBufferUpdate.Any())
                {
                    await connection.UpdateAllAsync(SyncroBufferUpdate);
                    SyncroBufferUpdate.Clear();
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

            try
            {
                if (SyncroBufferInsertOrUpdate.Any())
                {
                    await connection.InsertOrReplaceAllAsync(SyncroBufferInsertOrUpdate);
                    SyncroBufferInsertOrUpdate.Clear();
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

            return count;
        }

        public async Task<Syncro> GetAsyncByKey(object key)
        {
            Volver:

            if (WasExecute) await Task.Delay(2000);

            try
            {
                return await GetConnectionAsync().GetAsync<Syncro>(key);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        WasExecute = true;
                        goto Volver;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Syncro>> GetAsyncAll()
        {
            Volver:

            if (WasExecute) await Task.Delay(2000);

            try
            {
                return await GetConnectionAsync().Table<Syncro>().ToListAsync();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            WasExecute = true;
                            goto Volver;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        WasExecute = true;
                        goto Volver;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> InsertAsync(Syncro model)
        {
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
                            SyncroBufferInsert.Add(model);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        SyncroBufferInsert.Add(model);
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

        public Task<bool> InsertAsyncAll(IEnumerable<Syncro> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrReplaceAsync(Syncro models)
        {
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
                            SyncroBufferInsertOrUpdate.Add(models);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        SyncroBufferInsertOrUpdate.Add(models);
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

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Syncro> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Syncro model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Syncro> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Syncro model)
        {
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
                            SyncroBufferUpdate.Add(model);
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        SyncroBufferUpdate.Add(model);
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

        public Task<bool> UpdateAllAsync(IEnumerable<Syncro> models)
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

        public async void UpdateTable(Syncro.Tables table, Boolean value)
        {
            var syncro = await GetAsyncByKey(table);

            if (syncro == null)
            {
                syncro = new Syncro()
                {
                    Tabla = table,
                    LastSync = DateTime.Now,
                    Sync = value,
                    IsDaily = false
                };

                try
                {
                    await GetConnectionAsync().InsertOrReplaceAsync(syncro);
                }
                catch (SQLiteException ex)
                {
                    switch (ex.Result)
                    {
                        case SQLite.Net.Interop.Result.Error:
                            if (ex.Message.Equals(conMessage))
                            {
                                SyncroBufferInsert.Add(syncro);
                            }
                            else
                                throw;

                            break;

                        case SQLite.Net.Interop.Result.Busy:
                        case SQLite.Net.Interop.Result.Locked:
                            SyncroBufferInsert.Add(syncro);

                            break;

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
                try
                {
                    syncro.Sync = value;
                    await GetConnectionAsync().UpdateAsync(syncro);
                }
                catch (SQLiteException ex)
                {
                    switch (ex.Result)
                    {
                        case SQLite.Net.Interop.Result.Error:
                            if (ex.Message.Equals(conMessage))
                            {
                                SyncroBufferUpdate.Add(syncro);
                            }
                            else
                                throw;

                            break;

                        case SQLite.Net.Interop.Result.Busy:
                        case SQLite.Net.Interop.Result.Locked:
                            SyncroBufferUpdate.Add(syncro);
                            break;

                        default:
                            throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task UpdateTableAsync(Syncro.Tables table, Boolean value)
        {
            var syncro = await GetAsyncByKey(table);

            if (syncro == null)
            {
                syncro = new Syncro()
                {
                    Tabla = table,
                    LastSync = DateTime.Now.AddHours(-3),
                    Sync = value,
                    IsDaily = false
                };
            }
            else
            {
                syncro.Sync = value;
            }

            await InsertOrReplaceAsync(syncro);
        }
        
        public Task<string> InsertOrUpdateAsyncSql(Syncro[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
