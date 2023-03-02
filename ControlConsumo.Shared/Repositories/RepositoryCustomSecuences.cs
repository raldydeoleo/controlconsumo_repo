using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.CustomSecuence;
using ControlConsumo.Shared.Models.System;
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
    internal class RepositoryCustomSecuences : RepositoryBase, IRepository<CustomSecuences>
    {
        public RepositoryCustomSecuences(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryCustomSecuences(MyDbConnection connection) : base(connection) { }

        public Task<CustomSecuences> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomSecuences>> GetAsyncAll()
        {
            var Intentado = false;

            VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                #region Validar si se pierda la Columna "CustomFecha" de "CustomSecuences" despues del mantenimiento Aristoteles

                var info = await GetConnectionAsync().QueryAsync<TableInfoResult>("pragma table_info(\"CustomSecuences\")");

                if (!info.Any(p => p.name == "CustomFecha"))
                {
                    var Registros = await GetConnectionAsync().QueryAsync<CustomSecuences>("SELECT * FROM CustomSecuences");
                    await GetConnectionAsync().DropTableAsync<CustomSecuences>();
                    await GetConnectionAsync().CreateTableAsync<CustomSecuences>();
                    if (Registros.Any()) await GetConnectionAsync().InsertAllAsync(Registros.Select(s => new CustomSecuences
                    {
                        CustomFecha = Convert.ToInt32(s.Fecha.GetSapDate()),
                        ConsumptionID = s.ConsumptionID,
                        MaterialCode = s.MaterialCode,
                        ElaborateID = s.ElaborateID,
                        Fecha = s.Fecha,
                        Fecha2 = s.Fecha2
                    }).ToList());
                }

                #endregion

                return await GetConnectionAsync().Table<CustomSecuences>().ToListAsync();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverAIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverAIntentar;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> InsertAsync(CustomSecuences model)
        {
            var Intentado = false;

            VolverAInsertar:

            if (Intentado) await Task.Delay(Task_Delay);

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

        public async Task<bool> InsertAsyncAll(IEnumerable<CustomSecuences> models)
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

        public async Task<bool> InsertOrReplaceAsync(CustomSecuences models)
        {
            var Intentado = false;

            VolverAInsertar:

            if (Intentado) await Task.Delay(Task_Delay);

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

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<CustomSecuences> models)
        {
            var Intentado = false;

            VolverAInsertar:

            if (Intentado) await Task.Delay(Task_Delay);

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

        public async Task<bool> DeleteAsync(CustomSecuences model)
        {
            var Intentado = false;

            VolverABorrar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().DeleteAsync(model);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverABorrar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverABorrar;

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

        public async Task<bool> DeleteAllAsync(IEnumerable<CustomSecuences> models)
        {
            var Intentado = false;

            VolverABorrar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().DeleteAllAsync<CustomSecuences>();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolverABorrar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolverABorrar;

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

        public Task<bool> UpdateAsync(CustomSecuences model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<CustomSecuences> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsync(bool procesarSAP)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var url = GetService(ServicesType.POST_TRACKING, true, new Syncro() { LastSync = DateTime.Now }, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk && !json.Json.IsJsonEmpty())
            {
                var CustomSecuence = JsonConvert.DeserializeObject<CustomSecuenceResult[]>(json.Json);

                var buffer = CustomSecuence.Where(p => !String.IsNullOrEmpty(p.matnr))
                    .Select(p => new CustomSecuences
                    {
                        CustomFecha = Convert.ToInt32(GetDatetime(p.zentradadate).Value.GetSapDate()),
                        MaterialCode = p.matnr,
                        Fecha = GetDatetime(p.zentradadate).Value,
                        ConsumptionID = p.secentrada,
                        Fecha2 = GetDatetime(p.zsalidadate).Value,
                        ElaborateID = p.secsalida
                    }).ToList();

                var repoz = new RepositoryZ(this.Connection);

                var process = await repoz.GetProces();
                var actualconfig = await repoz.GetActualConfig(process.EquipmentID);
                if (actualconfig != null)
                {
                    var materials = repoz.GetMaterialConfig(actualconfig.ProductCode, actualconfig.VerID);
                    buffer.RemoveAll(p => !materials.Select(s => s.MaterialCode).Contains(p.MaterialCode));
                }

                var bufferFinal = new List<CustomSecuences>();

                foreach (var item in buffer)
                {
                    var result = await repoz.ExisteEnlos2UltimosTurnos(item.MaterialCode, item.ConsumptionID, item.CustomFecha);

                    if (result) bufferFinal.Add(item);
                }

                var duplicado = bufferFinal.GroupBy(p => p.MaterialCode).Select(c => new
                {
                    c.Key,
                    Conteo = c.Count(),
                    NumeroAlto = c.Max(m => m.ConsumptionID)
                });

                if (duplicado.Any(a => a.Conteo > 1))
                {
                    foreach (var item in duplicado)
                    {
                        bufferFinal.RemoveAll(r => r.MaterialCode == item.Key && r.ConsumptionID != item.NumeroAlto);
                    }
                }

                await InsertAsyncAll(bufferFinal);

                return true;
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

        public Task<string> InsertOrUpdateAsyncSql(CustomSecuences[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
