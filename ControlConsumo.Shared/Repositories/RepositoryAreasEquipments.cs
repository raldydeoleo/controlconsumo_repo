using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.AreaEquipment;
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
    internal class RepositoryAreasEquipments : RepositoryBase, IRepository<AreasEquipments>
    {
        public RepositoryAreasEquipments(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryAreasEquipments(MyDbConnection connection) : base(connection) { }

        public Task<AreasEquipments> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AreasEquipments>> GetAsyncAll()
        {
            return await GetConnectionAsync().Table<AreasEquipments>().ToListAsync();
        }

        public Task<bool> InsertAsync(AreasEquipments model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<AreasEquipments> models)
        {
            await GetConnectionAsync().InsertAllAsync(models);
            return true;
        }

        public Task<bool> InsertOrReplaceAsync(AreasEquipments models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<AreasEquipments> models)
        {
            await GetConnectionAsync().InsertOrReplaceAllAsync(models);
            return true;
        }

        public Task<bool> DeleteAsync(AreasEquipments model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<AreasEquipments> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AreasEquipments model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<AreasEquipments> models)
        {
            await GetConnectionAsync().UpdateAllAsync(models);
            return true;
        }

        public Task<bool> SyncAsync(bool procesarSAP)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var count = await con.Table<AreasEquipments>().CountAsync();

            if (count > 0) return false;

            var url = GetService(ServicesType.GET_AREAS_DETAILS, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk && !json.Json.IsJsonEmpty())
            {
                var results = JsonConvert.DeserializeObject<AreaEquipmentResult[]>(json.Json);

                var buffer = results.Select(p => new AreasEquipments()
                {
                    AreaID = (Byte)p.znoareas,
                    EquipmentID = p.idequipo
                }).ToList();

                await InsertAsyncAll(buffer);
            }
            else if (!json.isOk)
            {
                throw json.ex;
            }

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
        
        public Task<string> InsertOrUpdateAsyncSql(AreasEquipments[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
