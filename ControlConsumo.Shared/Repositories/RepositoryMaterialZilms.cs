using ControlConsumo.Shared.Interfaces;
using ControlConsumo.Shared.Models.MaterialZilm;
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
    internal class RepositoryMaterialZilms : RepositoryBase, IRepository<MaterialsZilm>
    {
        public RepositoryMaterialZilms(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryMaterialZilms(MyDbConnection connection) : base(connection) { }

        public static List<MaterialsZilm> _Buffer = new List<MaterialsZilm>();

        public async Task<MaterialsZilm> GetAsyncByKey(object key)
        {
            var all = await GetAsyncAll();
            var reg = all.FirstOrDefault(f => f.MaterialCode == key.ToString());
            return reg ?? new MaterialsZilm();
        }

        public async Task<IEnumerable<MaterialsZilm>> GetAsyncAll()
        {
            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                if (_Buffer == null || !_Buffer.Any())
                {
                    _Buffer = await GetConnectionAsync().Table<MaterialsZilm>().ToListAsync();
                }

                return _Buffer;
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

                    case SQLite.Net.Interop.Result.NotFound:
                        throw;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<bool> InsertAsync(MaterialsZilm model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsyncAll(IEnumerable<MaterialsZilm> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertOrReplaceAsync(MaterialsZilm models)
        {
            var Intentado = false;

            VolvelaIntentar:

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

            return true;
        }

        public async Task<bool> InsertOrReplaceAsyncAll(IEnumerable<MaterialsZilm> models)
        {
            var Intentado = false;

            VolvelaIntentar:

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

            return true;
        }

        public Task<bool> DeleteAsync(MaterialsZilm model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllAsync(IEnumerable<MaterialsZilm> models)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(MaterialsZilm model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAllAsync(IEnumerable<MaterialsZilm> models)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SyncAsync(bool procesarSAP)
        {
            return await SyncAsyncAll(false);
        }

        public async Task<bool> SyncAsyncAll(Boolean isItForInitialSync = true)
        {
            var con = GetConnectionAsync();

            var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.MaterialZilms, Fecha = DateTime.Now };

            var url = GetService(ServicesType.GET_MATERIAL_ZILM, true, null, isItForInitialSync);

            var json = await GetJsonAsync(url);

            if (!json.isOk) throw json.ex;

            var materiales = JsonConvert.DeserializeObject<MaterialsZilmResult[]>(json.Json);

            var buffer = materiales.Select(p => new MaterialsZilm
            {
                MaterialCode = p.matnr,
                Days = (short)p.dias,
                DateisRequired = !String.IsNullOrEmpty(p.fecha),
                EtiquetaIs3x1 = !String.IsNullOrEmpty(p.etiqueta3x1),
                NeedBoxNo = !String.IsNullOrEmpty(p.nocaja),
                SplitLots = !String.IsNullOrEmpty(p.splitlote),
                Cantidad = !String.IsNullOrEmpty(p.cantidad),
                AllowNoLot = !String.IsNullOrEmpty(p.ignoreloteofday),
                Percent = !String.IsNullOrEmpty(p.percent),
                IgnoreStock = !String.IsNullOrEmpty(p.ignorestock),
            }).ToList();

            var Intentado = false;

            VolvelaIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                await con.DeleteAllAsync<MaterialsZilm>();
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

            await InsertOrReplaceAsyncAll(buffer);

            Synclog.RegistrosBajada = buffer.Count();
            Synclog.SizeBajada = json.SizePackageDownloading;

            var repoSincro = new RepositorySyncro(this.Connection);

            await repoSincro.InsertOrReplaceAsync(new Syncro()
            {
                Tabla = Syncro.Tables.MaterialZilms,
                LastSync = DateTime.Now,
                Sync = false
            });

            _Buffer = null;

            SyncMonitor.Detalle.Add(Synclog);

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
        
        public Task<string> InsertOrUpdateAsyncSql(MaterialsZilm[] traysProducts, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
