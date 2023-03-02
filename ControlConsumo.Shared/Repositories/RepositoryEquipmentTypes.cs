using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.EquipmentType;
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
    internal class RepositoryEquipmentTypes : RepositoryBase, IRepository<EquipmentTypes>
    {
        public RepositoryEquipmentTypes(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryEquipmentTypes(MyDbConnection connection) : base(connection) { }

        public Task<EquipmentTypes> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EquipmentTypes>> GetAsyncAll()
        {
            var Intentado = false;

            VolverAInsertar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<EquipmentTypes>().ToListAsync();
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

        public Task<bool> InsertAsync(EquipmentTypes model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<EquipmentTypes> models)
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

        public Task<bool> InsertOrReplaceAsync(EquipmentTypes models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<EquipmentTypes> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(EquipmentTypes model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<EquipmentTypes> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(EquipmentTypes model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<EquipmentTypes> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsync(bool procesarSAP)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var count = await con.Table<EquipmentTypes>().CountAsync();

            if (count > 0) return false;

            var url = GetService(ServicesType.GET_EQUIPMENTTYPES, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk && !json.Json.IsJsonEmpty())
            {
                var tipos = JsonConvert.DeserializeObject<EquipmentTypesResult[]>(json.Json);

                var buffer = tipos.Select(p => new EquipmentTypes
                {
                    ID = (Byte)p.znotipoeq,
                    Name = p.tipoEquipo,
                    NeedWeight = !String.IsNullOrEmpty(p.requieregramos),
                    IsFinal = !String.IsNullOrEmpty(p.equipoFinal),
                    NeedEan = !String.IsNullOrEmpty(p.requiereEan)
                }).ToList();

                await InsertAsyncAll(buffer);
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

        public Task<string> InsertOrUpdateAsyncSql(EquipmentTypes[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
