using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Turn;
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
    internal class RepositoryTurns : RepositoryBase, IRepository<Turns>
    {
        public RepositoryTurns(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryTurns(MyDbConnection connection) : base(connection) { }

        public Task<Turns> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Turns>> GetAsyncAll()
        {
            var intentado = false;

        VolveraIntentar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Turns>().ToListAsync();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            intentado = true;
                            goto VolveraIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraIntentar;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> InsertAsync(Turns model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsyncAll(IEnumerable<Turns> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsync(Turns models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Turns> models)
        {
            var intentado = false;

        VolveraIntentar:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().InsertOrReplaceAllAsync(models);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            intentado = true;
                            goto VolveraIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraIntentar;

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

        public Task<bool> DeleteAsync(Turns model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Turns> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Turns model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<Turns> models)
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

            var count = await con.Table<Turns>().CountAsync();

            if (count > 0) return false;

            var url = GetService(ServicesType.GET_TURNS, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk && !json.Json.IsJsonEmpty())
            {
                var turnos = JsonConvert.DeserializeObject<TurnsResult[]>(json.Json);

                var Fechaint = "19000101";

                var buffer = turnos.Select(p => new Turns
                {
                    ID = (Byte)p.idturno,
                    Name = p.descripcion.Trim(),
                    Etiqueta = p.etiqueta.Trim(),
                    Begin = GetDatetime(Fechaint, p._HoraInicio).Value,
                    End = GetDatetime(Fechaint, p._HoraFin).Value,
                    Empaque = p.empaque
                }).ToList();

                await InsertOrReplaceAsyncAll(buffer);

                return true;
            }
            else if (!json.isOk)
            {
                throw json.ex;
            }

            return false;
        }

        public async Task<bool> CreateAsync()
        {
            await GetConnectionAsync().CreateTableAsync<Turns>();
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

        public Task<string> InsertOrUpdateAsyncSql(Turns[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
