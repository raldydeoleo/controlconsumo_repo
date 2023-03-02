using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Consumption;
using ControlConsumo.Shared.Models.Json;
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Shared.Tables;
using static ControlConsumo.Shared.Tables.Syncro;
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
    internal class RepositoryConsumptions : RepositoryBase, IRepository<Consumptions>
    {
        private static readonly List<Consumptions> ConsumptionsBufferInsert = new List<Consumptions>();
        private static readonly List<Consumptions> ConsumptionsBufferUpdate = new List<Consumptions>();

        public RepositoryConsumptions(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryConsumptions(MyDbConnection connection) : base(connection) { }

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            var count = ConsumptionsBufferInsert.Count() + ConsumptionsBufferUpdate.Count();

            try
            {
                if (ConsumptionsBufferInsert.Any())
                {
                    await connection.InsertAllAsync(ConsumptionsBufferInsert);
                    ConsumptionsBufferInsert.Clear();
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
                if (ConsumptionsBufferUpdate.Any())
                {
                    await connection.UpdateAllAsync(ConsumptionsBufferUpdate);
                    ConsumptionsBufferUpdate.Clear();
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

        public Task<Consumptions> GetAsyncByKey(object key)
        {
            throw new NotImplementedException();
        }
                
        public async Task<IEnumerable<Consumptions>> GetAsyncAll()
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Consumptions>().ToListAsync();
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolvelaIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
                        goto VolvelaIntentar;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> InsertAsync(Consumptions model)
        {
            try
            {
                model.CustomFecha = model.Produccion.GetDBDate();
                await GetConnectionAsync().InsertAsync(model);
                if (!String.IsNullOrEmpty(model.TrayID))
                {
                    model.IsMemoryCreated = true;
                    RepositoryZ.UltimaBandeja = model;
                }
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                            ConsumptionsBufferInsert.Add(model);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ConsumptionsBufferInsert.Add(model);
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

        public async Task<bool> InsertAsyncAll(IEnumerable<Consumptions> models)
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
                            ConsumptionsBufferInsert.AddRange(models);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ConsumptionsBufferInsert.AddRange(models);
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

        public Task<bool> InsertOrReplaceAsync(Consumptions models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Consumptions> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Consumptions model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Consumptions> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Consumptions model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<Consumptions> models)
        {
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
                            ConsumptionsBufferUpdate.AddRange(models);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        ConsumptionsBufferUpdate.AddRange(models);
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

        public async Task<bool> SyncAsync(Boolean procesarSap)
        {
            var Intentado = false;

            VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                if (procesarSap)//Sincronización de datos por SAP
                {
                    var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Consumptions, Fecha = DateTime.Now };

                    var url = GetService(ServicesType.POST_CONSUMPTIONS);

                    var pendientes = await GetConnectionAsync().Table<Consumptions>().Where(p => p.Sync).Take(MAX_ROWS).ToListAsync();

                    if (pendientes.Any())
                    {
                        var buffer = pendientes.Select(p => new ConsumptionsRequest
                        {
                            IDPROCESS = p.ProcessID,
                            WERKS = p.Center,
                            IDEQUIPO = p.EquipmentID,
                            IDTIEMPO = p.TimeID,
                            MATNR = p.ProductCode,
                            VERID = p.VerID,
                            SECENTRADA = p.CustomID,
                            FECHA = p.Produccion.GetSapDateL(),
                            HORA = p.Produccion.GetSapHoraL(),
                            IDTURNO = p.TurnID,
                            MATNR2 = p.MaterialCode,
                            IDEQUIPO2 = p.SubEquipmentID ?? String.Empty,
                            CHARG = p.Lot,
                            MENGE = p.Quantity.Round3(),
                            MEINS = p.Unit,
                            BOXNO = p.BoxNumber,
                            USNAM = p.Logon,
                            CPUDT = p.Fecha.GetSapDateL(),
                            CPUTM = p.Fecha.GetSapHoraL(),
                            CPUDT2 = DateTime.Now.GetSapDate(),
                            CPUTM2 = DateTime.Now.GetSapHora(),
                            IDEQUIPO3 = p.TrayEquipmentID,
                            SECSALIDA = p.ElaborateID,
                            IDBANDEJA = p.TrayID,
                            CPUDT3 = p.TrayDate.HasValue ? p.TrayDate.Value.GetSapDateL() : "00000000",
                            CPUTM3 = p.TrayDate.HasValue ? p.TrayDate.Value.GetSapHoraL() : "000000",
                            BATCHID = p.BatchID
                        }).ToArray();

                        var json = await PostJsonAsync(url, buffer);

                        if (!json.isOk) throw json.ex;

                        Synclog.RegistrosSubida = pendientes.Count();
                        Synclog.SizeSubida = json.SizePackageUploading;

                        foreach (var item in pendientes)
                        {
                            item.Sync = false;
                        }

                        var Intentado2 = false;

                        VolvarActualizar:

                        if (Intentado2) await Task.Delay(Task_Delay);

                        try
                        {
                            await GetConnectionAsync().UpdateAllAsync(pendientes);
                        }
                        catch (SQLiteException ex)
                        {
                            switch (ex.Result)
                            {
                                case SQLite.Net.Interop.Result.Error:
                                    if (ex.Message.Equals(conMessage))
                                    {
                                        Intentado2 = true;
                                        goto VolvarActualizar;
                                    }
                                    else
                                        throw;

                                case SQLite.Net.Interop.Result.Busy:
                                case SQLite.Net.Interop.Result.Locked:
                                    Intentado2 = true;
                                    goto VolvarActualizar;

                                default:
                                    throw;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        SyncMonitor.Detalle.Add(Synclog);

                        //return true;
                    }
                }
                return await SyncAsyncSQL();
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

            return false;
        }

        public async Task<bool> SyncAsyncSQL()
        {
            var Intentado = false;

            VolverAIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Consumptions, Fecha = DateTime.Now };

                var url = GetSqlServicePath(SqlServiceType.PostConsumo);

                var pendientes = await GetConnectionAsync().Table<Consumptions>().Where(p => p.SyncSQL).Take(MAX_ROWS_SQLSERVER).ToListAsync();

                if (pendientes.Any())
                {
                    var buffer = pendientes.Select(p => new ConsumptionsRequest
                    {
                        IDPROCESS = p.ProcessID,
                        WERKS = p.Center,
                        IDEQUIPO = p.EquipmentID,
                        IDTIEMPO = p.TimeID,
                        MATNR = p.ProductCode,
                        VERID = p.VerID,
                        SECENTRADA = p.CustomID,
                        FECHA = p.Produccion.GetSapDateL(),
                        HORA = p.Produccion.GetSapHoraL(),
                        IDTURNO = p.TurnID,
                        MATNR2 = p.MaterialCode,
                        IDEQUIPO2 = p.SubEquipmentID ?? String.Empty,
                        CHARG = p.Lot,
                        MENGE = p.Quantity.Round3(),
                        MEINS = p.Unit,
                        BOXNO = p.BoxNumber,
                        USNAM = p.Logon,
                        CPUDT = p.Fecha.GetSapDateL(),
                        CPUTM = p.Fecha.GetSapHoraL(),
                        CPUDT2 = DateTime.Now.GetSapDate(),
                        CPUTM2 = DateTime.Now.GetSapHora(),
                        IDEQUIPO3 = p.TrayEquipmentID,
                        SECSALIDA = p.ElaborateID,
                        IDBANDEJA = p.TrayID,
                        CPUDT3 = p.TrayDate.HasValue ? p.TrayDate.Value.GetSapDateL() : "00000000",
                        CPUTM3 = p.TrayDate.HasValue ? p.TrayDate.Value.GetSapHoraL() : "000000",
                        BATCHID = p.BatchID
                    }).ToArray();

                    var json = await PostJsonAsync(url, buffer);

                    if (!json.isOk) throw json.ex;

                    Synclog.RegistrosSubida = pendientes.Count();
                    Synclog.SizeSubida = json.SizePackageUploading;

                    foreach (var item in pendientes)
                    {
                        item.SyncSQL = false;
                    }

                    var Intentado2 = false;

                    VolvarActualizar:

                    if (Intentado2) await Task.Delay(Task_Delay);

                    try
                    {
                        await GetConnectionAsync().UpdateAllAsync(pendientes);
                    }
                    catch (SQLiteException ex)
                    {
                        switch (ex.Result)
                        {
                            case SQLite.Net.Interop.Result.Error:
                                if (ex.Message.Equals(conMessage))
                                {
                                    Intentado2 = true;
                                    goto VolvarActualizar;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                Intentado2 = true;
                                goto VolvarActualizar;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    SyncMonitor.Detalle.Add(Synclog);

                    return true;
                }
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

            return false;
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var repoSyncro = new RepositorySyncro(this.Connection);
            var repoConfiguracionSincronizacionTablas = new RepositoryConfiguracionSincronizacionTablas(this.Connection);
            var configuracionSincronizacionTabla = await repoConfiguracionSincronizacionTablas.GetAsyncByKey(Syncro.Tables.Consumptions.ToString());
            var processBySAP = true;

            if (configuracionSincronizacionTabla != null)
            {
                processBySAP = configuracionSincronizacionTabla.procesarSap;
            }
             
            var url = "";

            switch (processBySAP)
            {
                case true:
                    url = GetService(ServicesType.POST_CONSUMPTIONS, true, new Syncro() { LastSync = DateTime.Now.AddDays(-10) }, isItForInitialSync);
                    break;
                case false:
                    var parametros = String.Format("?fechaFinal={0}&idEquipo={1}", DateTime.Now.AddDays(-10).ToString("dd'/'MM'/'yyyy"),
                        Proceso.IsSubEquipment ? Proceso.SubEquipmentID : Proceso.EquipmentID);
                    url = GetSqlServicePath(SqlServiceType.GetConsumos, parametros);
                    break;
            }

            var json = await GetJsonAsync(url);

            if (json.isOk && !json.Json.IsJsonEmpty())
            {
                var Consumos = JsonConvert.DeserializeObject<ConsumptionsRequest[]>(json.Json);

                var buffer = Consumos.Select(p => p.Get()).ToList();

                //var buffer = Consumos.Select(p => new Consumptions
                //{
                //    ProcessID = p.IDPROCESS,
                //    Center = p.WERKS,
                //    CustomFecha = GetDatetime(p.FECHA, p.HORA).Value.GetDBDate(),
                //    Produccion = GetDatetime(p.FECHA, p.HORA).Value,
                //    Fecha = GetDatetime(p.CPUDT, p.CPUTM).Value,
                //    CustomID = p.SECENTRADA,
                //    EquipmentID = p.IDEQUIPO,
                //    TimeID = p.IDTIEMPO,
                //    ProductCode = p.MATNR,
                //    VerID = p.VERID,
                //    MaterialCode = p.MATNR2,
                //    Logon = p.USNAM,
                //    Sync = false,
                //    TurnID = p.IDTURNO,
                //    Lot = p.CHARG,
                //    BoxNumber = p.BOXNO,
                //    Quantity = p.MENGE,
                //    SubEquipmentID = p.IDEQUIPO2,
                //    Unit = p.MEINS,
                //    TrayID = p.IDBANDEJA,
                //    ElaborateID = p.SECSALIDA,
                //    TrayEquipmentID = p.IDEQUIPO3,
                //    TrayDate = GetDatetime(p.CPUDT3),
                //    BatchID = p.BATCHID
                //}).OrderBy(o => o.Fecha).ToList();

                await InsertAsyncAll(buffer);

                await CreateSecuences(buffer);

                await repoSyncro.InsertOrReplaceAsync(new Syncro()
                {
                    Tabla = Syncro.Tables.Consumptions,
                    IsDaily = false,
                    Sync = false,
                    LastSync = !Consumos.Any() ? DateTime.Now : Consumos.Max(p => GetDatetime(p.CPUDT2, p.CPUTM2).Value)
                });

                return true;
            }
            else if (json.isOk && json.Json.IsJsonEmpty())
            {
                await repoSyncro.InsertOrReplaceAsync(new Syncro()
                {
                    Tabla = Syncro.Tables.Consumptions,
                    IsDaily = false,
                    Sync = false,
                    LastSync = DateTime.Now
                });
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

        /// <summary>
        /// Metodo para crear la mezcla
        /// </summary>
        /// <param name="buffer">Lista de Secuencias</param>
        private async Task CreateSecuences(List<Consumptions> consumos)
        {
            var repoz = new RepositoryZ(this.Connection);
            var repoSec = new RepositoryCustomSecuences(this.Connection);

            var buffer = new List<CustomSecuences>();
            var procesados = new List<String>();

            var lastsalida = await repoz.GetLastSalidaAsync(); /// Validamos las ultimas Salidas

            foreach (var item in consumos.OrderByDescending(o => o.Fecha))
            {
                if (!procesados.Any(a => a == item.MaterialCode))
                {
                    procesados.Add(item.MaterialCode);

                    if (item.Quantity < 0) continue;

                    buffer.Add(new CustomSecuences
                    {
                        MaterialCode = item.MaterialCode,
                        CustomFecha = item.CustomFecha,
                        ConsumptionID = item.CustomID,
                        Fecha = item.Produccion,
                        Fecha2 = (lastsalida != null && lastsalida.CustomFecha == item.CustomFecha) ? lastsalida.Produccion : item.Produccion,
                        ElaborateID = (lastsalida != null && lastsalida.CustomFecha == item.CustomFecha) ? (Int16)lastsalida.CustomID : (Int16)1
                    });
                }
            }

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

            await repoSec.InsertAsyncAll(bufferFinal);
        }
        
        public Task<string> InsertOrUpdateAsyncSql(Consumptions[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
