using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.Material;
using ControlConsumo.Shared.Models.System;
using ControlConsumo.Shared.Models.Z;
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
    internal class RepositoryMaterials : RepositoryBase, IRepository<Materials>
    {
        private static readonly List<Materials> MaterialsBufferInsert = new List<Materials>();
        private static readonly List<Materials> MaterialsBufferUpdate = new List<Materials>();

        public RepositoryMaterials(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryMaterials(MyDbConnection connection) : base(connection) { }

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            var count = MaterialsBufferInsert.Count() + MaterialsBufferUpdate.Count();

            try
            {
                if (MaterialsBufferInsert.Any())
                {
                    await connection.InsertAllAsync(MaterialsBufferInsert);
                    MaterialsBufferInsert.Clear();
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
                if (MaterialsBufferUpdate.Any())
                {
                    await connection.UpdateAllAsync(MaterialsBufferUpdate);
                    MaterialsBufferUpdate.Clear();
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

        public async Task<Materials> GetAsyncByKey(object key)
        {
            var Intentado = false;

        VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().GetAsync<Materials>(key);
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
                return null;
            }
        }

        public async Task<IEnumerable<Materials>> GetAsyncAll()
        {
            return await GetConnectionAsync().Table<Materials>().ToListAsync();
        }

        public Task<bool> InsertAsync(Materials model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsyncAll(IEnumerable<Materials> models)
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
                            MaterialsBufferInsert.AddRange(models);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        MaterialsBufferInsert.AddRange(models);
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

        public Task<bool> InsertOrReplaceAsync(Materials models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<Materials> models)
        {
            var Intentado = false;

            VolverALeer:

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

        public Task<bool> DeleteAsync(Materials model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<Materials> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Materials model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAllAsync(IEnumerable<Materials> models)
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
                            MaterialsBufferUpdate.AddRange(models);
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        MaterialsBufferUpdate.AddRange(models);
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

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            try
            {
                IsSyncing = true;

                var repoSincro = new RepositorySyncro(this.Connection);

                var Material = await repoSincro.GetAsyncByKey(Syncro.Tables.Materials);

                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Lots, Fecha = Material.LastSync };

                var url = GetService(ServicesType.GET_MATERIALS, false, Material);

                var json = await GetJsonAsync(url);

                Synclog.SizeBajada = json.SizePackageDownloading;

                if (json.isOk)
                    Synclog.RegistrosBajada = await InsertCommon(json.Json, false);
                else
                    throw json.ex;

                SyncMonitor.Detalle.Add(Synclog);

                return true;
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

            var id = await con.Table<Materials>().CountAsync();

            if (id > 0) return false;

            var url = GetService(ServicesType.GET_MATERIALS, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (json.isOk)
                await InsertCommon(json.Json, true);
            else
                throw json.ex;

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

        public Materials GetByCode(String Code)
        {
            return GetConnection().Table<Materials>().Where(p => p.Code == Code).FirstOrDefault();
        }

        public async Task<bool> InsertOrIgnoreAllAsync(IEnumerable<Materials> models)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await GetConnectionAsync().InsertOrIgnoreAllAsync(models);
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

        #region Common Methods

        public async Task<Int32> InsertCommon(String Json, Boolean IsInitialSync)
        {
            if (!Json.IsJsonEmpty())
            {
                //#region Validar si existe la columna nueva en Unis

                //var info = await GetConnectionAsync().QueryAsync<TableInfoResult>("pragma table_info(\"Units\")");

                //if (!info.Any(p => p.name == "Key"))
                //{
                //    await GetConnectionAsync().DropTableAsync<Units>();
                //    await GetConnectionAsync().CreateTableAsync<Units>();
                //}

                //#endregion

                var Intentado = false;

                VolverALeer:

                if (Intentado) await Task.Delay(Task_Delay);

                try
                {
                    DateTime? fecha = null;

                    var Materials = JsonConvert.DeserializeObject<MaterialsResult[]>(Json);

                    var buffer = new List<Materials>();

                    foreach (var mat in Materials)
                    {
                        var material = new Materials()
                        {
                            Code = mat.matnr,
                            Reference = mat._bismt,
                            Name = mat.maktx,
                            Short = mat.normt,
                            Unit = mat.meins,
                            Group = mat.matkl,
                            ProductType = mat.formt,
                            units = mat.units.Select(p => new Units
                            {
                                Key = String.Format("{0}-{1}", mat.matnr, p.meinh),
                                MaterialCode = mat.matnr,
                                Ean = p._ean11,
                                From = p.umren,
                                To = p.umrez,
                                Unit = p.meinh,
                                IsBase = p.meinh.Equals(mat.meins)
                            }).ToList(),
                            categories = mat.clasificaciones == null ? new List<Categories>() : mat.clasificaciones.Select(s => new Categories
                            {
                                Key = String.Format("{0}-{1}", mat.matnr, s.ATNAM),
                                MaterialCode = mat.matnr,
                                Category = Get_CategoryType(s.ATNAM),
                                Value = s.ATWRT.Trim()
                            }).ToList()
                        };

                        buffer.Add(material);
                    }

                    var repounit = new RepositoryUnits(this.Connection);

                    await InsertOrReplaceAsyncAll(buffer);

                    await repounit.InsertOrReplaceAsyncAll(buffer.SelectMany(m => m.units));

                    if (Materials.Any())
                    {
                        fecha = Materials.Max(p => GetDatetime(p._date).Value);

                        var reposincro = new RepositorySyncro(this.Connection);

                        var sincro = new Syncro()
                        {
                            Tabla = Syncro.Tables.Materials,
                            LastSync = DateTime.Now.Date > fecha ? DateTime.Now.Date : fecha.Value,
                            Sync = false
                        };

                        await reposincro.InsertOrReplaceAsync(sincro);
                    }

                    Intentado = false;

                    VolverAIntentarClasificaciones:

                    if (Intentado) await Task.Delay(2000);

                    try
                    {
                        if (!IsInitialSync)
                        {
                            var buffertoUpdate = new List<Categories>();
                            var buffertoInsert = new List<Categories>();

                            foreach (var item in buffer.SelectMany(m => m.categories))
                            {
                                var count = await GetConnectionAsync().Table<Categories>().Where(w => w.Key == item.Key).CountAsync();

                                if (count > 0)
                                    buffertoUpdate.Add(item);
                                else
                                    buffertoInsert.Add(item);
                            }

                            await GetConnectionAsync().UpdateAllAsync(buffertoUpdate);
                            await GetConnectionAsync().InsertAllAsync(buffertoInsert);
                        }
                        else
                        {
                            await GetConnectionAsync().InsertAllAsync(buffer.SelectMany(m => m.categories));
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
                                    goto VolverAIntentarClasificaciones;
                                }
                                else
                                    throw;

                            case SQLite.Net.Interop.Result.Busy:
                            case SQLite.Net.Interop.Result.Locked:
                                Intentado = true;
                                goto VolverAIntentarClasificaciones;

                            default:
                                throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
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
                catch (JsonSerializationException ex)
                { }
                catch (Exception)
                {
                    throw;
                }
            }

            return 0;
        }

        private Categories.TypesCategories Get_CategoryType(String Name)
        {
            switch ((Name ?? String.Empty).ToUpper())
            {
                case "IPDM_IPACK": return Categories.TypesCategories.IPDM_IPACK;
                case "IPDM_PCKSC": return Categories.TypesCategories.IPDM_PCKSC;
                case "IPDM_PCKCT": return Categories.TypesCategories.IPDM_PCKCT;
                case "IPDM_TALMA": return Categories.TypesCategories.IPDM_TALMA;
                default: return Categories.TypesCategories.IPDM_NONE;
            }
        }

        public Task<string> InsertOrUpdateAsyncSql(Materials[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
