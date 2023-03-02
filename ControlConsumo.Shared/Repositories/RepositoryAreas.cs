using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Area;
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
    internal class RepositoryAreas : RepositoryBase, IRepository<Areas>
    {
        public RepositoryAreas(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryAreas(MyDbConnection connection) : base(connection) { }

        public Task<Areas> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Areas>> GetAsyncAll()
        {
            return await GetConnectionAsync().Table<Areas>().ToListAsync();
        }

        public Task<bool> InsertAsync(Areas model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Areas> models)
        {
            await GetConnectionAsync().InsertAllAsync(models);
            return true;
        }

        public async Task<bool> InsertOrReplaceAsync(Areas models)
        {
            await GetConnectionAsync().InsertOrReplaceAsync(models);
            return true;
        }

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Areas> models)
        {
            await GetConnectionAsync().InsertOrReplaceAllAsync(models);
            return true;
        }

        public Task<bool> DeleteAsync(Areas model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Areas> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Areas model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<Areas> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsync(bool procesarSap)
        {
            if (IsSyncing) return false;

            try
            {
                IsSyncing = true;

                var repoSyncro = new RepositorySyncro(this.Connection);

                var regSyncro = await repoSyncro.GetAsyncByKey(Syncro.Tables.Areas);

                var url = GetService(ServicesType.GET_AREAS, false, regSyncro);

                var json = await GetJsonAsync(url);

                if (json.isOk)
                    await InsertCommon(json.Json);
                else
                    throw json.ex;

                return json.isOk;
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

            var count = await con.Table<Areas>().CountAsync();

            if (count > 0) return false;

            var url = GetService(ServicesType.GET_AREAS, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json);
            else
                throw json.ex;

            return json.isOk;
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
        #region Custom Methods

        private async Task InsertCommon(String Json)
        {
            DateTime? max = null;

            if (!Json.IsJsonEmpty())
            {
                var results = JsonConvert.DeserializeObject<AreasResult[]>(Json);

                var buffer = results.Select(p => new Areas
                {
                    ID = (Byte)p.znoareas,
                    Name = p.areas,
                    status = (Areas.Status)p.status
                }).ToList();

                foreach (var item in buffer)
                {
                    await InsertOrReplaceAsync(item);
                }

                max = results.Max(p => GetDatetime(p.cpudt, p.cputm).Value);
            }

            var sincrorepo = new RepositorySyncro(this.Connection);

            await sincrorepo.InsertOrReplaceAsync(new Syncro()
            {
                Tabla = Syncro.Tables.Areas,
                LastSync = max ?? DateTime.Now,
                Sync = false
            });
        }

        public Task<string> InsertOrUpdateAsyncSql(Areas[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
