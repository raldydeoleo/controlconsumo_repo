using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Equipment;
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
    internal class RepositoryEquipments : RepositoryBase, IRepository<Equipments>
    {
        public RepositoryEquipments(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryEquipments(MyDbConnection connection) : base(connection) { }

        public async Task<Equipments> GetAsyncByKey(object key)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var llave = key.ToString();
                return await GetConnectionAsync().Table<Equipments>().Where(W => W.ID == llave).FirstOrDefaultAsync();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverALeer;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverALeer;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Equipments>> GetAsyncAll()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Equipments>().ToListAsync();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverALeer;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverALeer;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> InsertAsync(Equipments model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Equipments> models)
        {
            var Intentado = false;

            VolverALeer:

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
                            goto VolverALeer;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverALeer;

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

        public Task<bool> InsertOrReplaceAsync(Equipments models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Equipments> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Equipments model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Equipments> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Equipments model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<Equipments> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsync(bool procesarSAP)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {            
            var listNonExistingEquipments = new List<Equipments>();
            var listExistingEquipments = new List<Equipments>();

            try
            {
                var con = GetConnectionAsync();

                var url = GetService(ServicesType.GET_EQUIPMENTS, true, null, isItForInitialSync);

                var json = await GetJsonAsync(url);

                if (json.isOk && !json.Json.IsJsonEmpty())
                {
                    var equipments = JsonConvert.DeserializeObject<EquipmentsResult[]>(json.Json);

                    var buffer = equipments.Select(p => new Equipments
                    {
                        Name = p.equipo,
                        EquipmentTypeID = (Byte)p.znotipoeq,
                        ID = p.idequipo,
                        IsSubEquipment = p.subequipo == "S",
                        IsActive = p.status == "A",
                        Serial = p.serial ?? String.Empty,
                        TimeID = p.idtiempo
                    }).ToList();

                    foreach (var item in buffer)
                    {
                        var equipment = await GetAsyncByKey(item.ID);
                        if (equipment == null)
                            listNonExistingEquipments.Add(item);
                        else
                            listExistingEquipments.Add(item);
                    }
                }
                else if (!json.isOk)
                {
                    throw json.ex;
                }
            }
            catch (Exception)
            {
                throw;
            }

            var Intentado = false;

            VolverAInsertar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().InsertAllAsync(listNonExistingEquipments);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
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

            Intentado = false;

            VolverActualizar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().UpdateAllAsync(listExistingEquipments);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverActualizar;

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
        
        public Task<string> InsertOrUpdateAsyncSql(Equipments[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
