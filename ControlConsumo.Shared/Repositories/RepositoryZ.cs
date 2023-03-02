using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Models.ConfigMaterial;
using ControlConsumo.Shared.Models.Consumption;
using ControlConsumo.Shared.Models.Elaborate;
using ControlConsumo.Shared.Models.Equipment;
using ControlConsumo.Shared.Models.Json;
using ControlConsumo.Shared.Models.LabelPrintingLog;
using ControlConsumo.Shared.Models.Lot;
using ControlConsumo.Shared.Models.Material;
using ControlConsumo.Shared.Models.Process;
using ControlConsumo.Shared.Models.R;
using ControlConsumo.Shared.Models.Stock;
using ControlConsumo.Shared.Models.TrayProduct;
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    public class RepositoryZ : RepositoryBase
    {
        private const Int32 ToSync = 150;

        public static Consumptions UltimaBandeja { get; set; }

        private static readonly List<CustomQuery> BufferCustom = new List<CustomQuery>();

        public RepositoryZ(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryZ(MyDbConnection connection) : base(connection) { }

        public static void AddCustomValuetoBuffer(CustomQuery Query)
        {
            BufferCustom.Add(Query);
        }

        public async static Task<Int32> ExecutePendingJobs(SQLiteAsyncConnection connection)
        {
            try
            {
                if (BufferCustom.Any())
                {
                    foreach (var item in BufferCustom)
                    {
                        await connection.ExecuteAsync(item.Query, item.args);
                    }

                    BufferCustom.Clear();
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

            return BufferCustom.Count();
        }

        public void SetProcess(ProcessList proceso)
        {
            Proceso = proceso;
        }

        public void SetUser(Users _Usuario)
        {
            Usuario = _Usuario;
        }

        public async Task<Users> GetLogon(String Logon)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var user = await GetConnectionAsync().Table<Users>().Where(p => p.Logon == Logon || p.Code == Logon).FirstOrDefaultAsync();

                if (user != null)
                    user.Permisos = await GetConnectionAsync().Table<RolsPermits>().Where(p => p.RolID == user.RolID).ToListAsync();

                return user;
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
            catch (Exception ex)
            {
                throw;
            }
        }

        public Users GetLogonSync(String Logon)
        {
            return AsyncHelper.RunSync<Users>(() => GetLogon(Logon));
        }

        public async Task<Users> GetLogonByCode(String Code)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var user = await GetConnectionAsync().Table<Users>().Where(p => p.Code == Code).FirstOrDefaultAsync();
                if (user != null)
                    user.Permisos = await GetConnectionAsync().Table<RolsPermits>().Where(p => p.RolID == user.RolID).ToListAsync();

                return user;
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

        public Users GetUser()
        {
            return Usuario;
        }

        public ProcessList GetProcesSync()
        {
            return AsyncHelper.RunSync<ProcessList>(() => GetProces());
        }

        public async Task<ProcessList> GetProces()
        {
            if (Proceso == null) // Se le agregó este control porque si android destruye la actividad bota esta configuración
            {
                Proceso = new ProcessList();
                Proceso.Process = await GetSettingAsync<String>(Settings.Params.Process, null);
                Proceso.Centro = await GetSettingAsync<String>(Settings.Params.Werks, null);
                Proceso.ConfigActive = await GetSettingAsync<Boolean>(Settings.Params.ConfigIsActive, false);
                Proceso.EquipmentID = await GetSettingAsync<String>(Settings.Params.ConfigID, null);
                Proceso.Equipment = await GetSettingAsync<String>(Settings.Params.ConfigName, null);
                Proceso.IsLast = await GetSettingAsync<Boolean>(Settings.Params.IsLast, false);
                Proceso.NeedEan = await GetSettingAsync<Boolean>(Settings.Params.NeedEan, false);
                Proceso.NeedGramo = await GetSettingAsync<Boolean>(Settings.Params.NeedGramo, false);
                Proceso.IsSubEquipment = await GetSettingAsync<Boolean>(Settings.Params.IsSubEquipment, false);
                Proceso.SubEquipmentID = await GetSettingAsync<String>(Settings.Params.Second_Equipment, null);
                if (Usuario != null)
                {
                    Proceso.UserName = Usuario.Name;
                    Proceso.Logon = Usuario.Code;
                }
                Proceso.IsInputOutputControlActive = await GetSettingAsync<Boolean>(Settings.Params.IsInputOutputControlActive, true);
                Proceso.IsPartialElaborateAuthorized = await GetSettingAsync<Boolean>(Settings.Params.IsPartialElaborateAuthorized, false);
            }
            if (Proceso != null && Proceso.Process == null)
            {
                Proceso.Process = await GetSettingAsync<String>(Settings.Params.Process, null);
            }
            Proceso.EquipmentID = await GetSettingAsync<String>(Settings.Params.ConfigID, null);
            Proceso.IsSubEquipment = await GetSettingAsync<Boolean>(Settings.Params.IsSubEquipment, false);
            Proceso.SubEquipmentID = await GetSettingAsync<String>(Settings.Params.Second_Equipment, null);
            Proceso.IsInputOutputControlActive = await GetSettingAsync<Boolean>(Settings.Params.IsInputOutputControlActive, true);
            Proceso.IsPartialElaborateAuthorized = await GetSettingAsync<Boolean>(Settings.Params.IsPartialElaborateAuthorized, false);
            return Proceso.Clone();
        }

        public async Task<IEnumerable<ProcessList>> GetProcess()
        {
            var url = GetService(ServicesType.GET_PROCESS, true, null, true);
            var json = await GetJsonAsync(url);
            if (!json.isOk) return new List<ProcessList>();
            var Procesos = JsonConvert.DeserializeObject<ProcessResult[]>(json.Json);

            return Procesos.Select(p => new ProcessList
            {
                Process = p.idprocess,
                ProcessName = p.zproceso,
                Centro = p.werks,
                CentroName = p.name1
            }).ToList();
        }

        public async Task<List<EquipmentList>> GetEquipments()
        {
            var con = GetConnectionAsync();

            var StatusAreas = (Byte)Areas.Status.Activa;
            var StatusActived = (Byte)Configs._Status.Actived;
            var StatusEnabled = (Byte)Configs._Status.Enabled;

            var builder = new StringBuilder();
            builder.Append("SELECT ");
            builder.Append("    E.ID AS EquipmentID, ");
            builder.Append("    E.Name AS Equipment, ");
            builder.Append("    E.IsActive AS Status, ");
            builder.Append("    E.TimeID, ");
            builder.Append("    C.Begin, ");
            builder.Append("    C.ID AS ConfigID, ");
            builder.Append("    C.SubEquipmentID, ");
            builder.Append("    SE.Name AS SubEquipment, ");
            builder.Append("    C.VerID AS Version, ");
            builder.Append("    C.CreateDate AS [Create], ");
            builder.Append("    C.Logon , ");
            builder.Append("    CASE WHEN C.Status = ? THEN 1 ELSE 0 END AS IsActive, ");
            builder.Append("    T.Name AS Type, ");
            builder.Append("    A.Name AS Area, ");
            builder.Append("    A.ID AS AreaID, ");
            builder.Append("    M.Name AS Material, ");
            builder.Append("    M.Code, ");
            builder.Append("    M.Short, ");
            builder.Append("    E.EquipmentTypeID ");
            builder.Append("FROM        Equipments E ");
            builder.Append("INNER JOIN  EquipmentTypes T ");
            builder.Append("ON          E.EquipmentTypeID = T.ID ");
            builder.Append("LEFT JOIN   Configs C ");
            builder.Append("ON          C.EquipmentID = E.ID ");
            builder.Append("AND         C.Status IN (?,?) ");
            builder.Append("AND         C.Begin < ? ");
            builder.Append("LEFT JOIN   Materials M ");
            builder.Append("ON          M.Code = C.ProductCode ");
            builder.Append("LEFT JOIN   Equipments SE ");
            builder.Append("ON          C.SubEquipmentID = SE.ID ");
            builder.Append("AND         SE.IsSubEquipment = 1 ");
            builder.Append("LEFT JOIN   AreasEquipments AE ");
            builder.Append("ON          AE.EquipmentID = E.ID ");
            builder.Append("AND         AE.IsDeleted = 0 ");
            builder.Append("LEFT JOIN   Areas A ");
            builder.Append("ON          A.ID = AE.AreaID ");
            builder.Append("AND         A.status = ? ");
            builder.Append("WHERE       E.IsSubEquipment = 0 ");
            builder.Append("ORDER BY    E.ID, C.Begin");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await con.QueryAsync<EquipmentList>(builder.ToString(), StatusEnabled, StatusActived, StatusEnabled, DateTime.Now.AddDays(1).Ticks, StatusAreas);
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

        public async Task<List<Equipments>> GetSubEquipments()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Equipments>().Where(p => p.IsSubEquipment).ToListAsync();
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

        public async Task<List<ConfigList>> GetConfigs(String TimeID = null)
        {
            var builder = new StringBuilder();
            builder.Append("SELECT ");
            builder.Append("    M.Name AS Material, ");
            builder.Append("    M.Short, ");
            builder.Append("    M.Code, ");
            builder.Append("    C.VerID AS Version, ");
            builder.Append("    MP.TimeID ");
            builder.Append("FROM        ConfigMaterials C ");
            builder.Append("INNER JOIN  Materials M ");
            builder.Append("ON          M.Code = C.ProductCode ");
            builder.Append("INNER JOIN  MaterialsProcess MP ");
            builder.Append("ON          MP.ProductCode = C.ProductCode ");

            if (!String.IsNullOrEmpty(TimeID))
            {
                builder.Append(String.Format("AND         MP.TimeID = '{0}' ", TimeID));
            }

            builder.Append("GROUP BY    M.Name, M.Short, M.Code, C.VerID, MP.TimeID ");
            builder.Append("ORDER BY    M.Short, M.Code");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<ConfigList>(builder.ToString());
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

        public async Task<List<ConfigList>> GetConfigsByTray(String ID)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT ");
            builder.Append("    M.Name AS Material, ");
            builder.Append("    M.Short, ");
            builder.Append("    M.Code, ");
            builder.Append("    C.VerID AS Version ");
            builder.Append("FROM        ConfigMaterials C ");
            builder.Append("INNER JOIN  Materials M ");
            builder.Append("ON          M.Code = C.ProductCode ");
            builder.Append("INNER JOIN  MaterialsProcess MP ");
            builder.Append("ON          MP.ProductCode = C.ProductCode ");
            builder.Append("INNER JOIN  TraysTimes TT ");
            builder.Append("ON          TT.TimeID = MP.TimeID ");
            builder.Append("WHERE       TT.TrayID = ? ");
            builder.Append("GROUP BY    M.Name, M.Short, M.Code, C.VerID ");
            builder.Append("ORDER BY    M.Short, M.Code");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<ConfigList>(builder.ToString(), ID);
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
        public async Task<Equipments> GetEquipment(String EquipmentId)
        {
            var Intentado = false;

            VolveraIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Equipments>().Where(s => s.ID == EquipmentId).FirstOrDefaultAsync(); 
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolveraIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
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
        public async Task UpdateEquipment(IEnumerable<EquipmentUpdate> Param)
        {
            var repoa = new RepositoryAreasEquipments(this.Connection);
            List<AreasEquipments> allarea = (List<AreasEquipments>)await repoa.GetAsyncAll();

            var repoSyncro = new RepositorySyncro(this.Connection);

            if (Param.Any(p => p.UpdateStatus))
            {
                var builder = new StringBuilder();

                if (Param.Any(p => p.IsActive))
                    builder.Append(String.Format("UPDATE Equipments Set IsActive=1, Sync=1 WHERE ID IN ({0});", Param.Where(p => p.IsActive).Select(p => p.EquipmentID).GetStringEnumerable()));

                if (Param.Any(p => !p.IsActive))
                    builder.Append(String.Format("UPDATE Equipments Set IsActive=0, Sync=1 WHERE ID IN ({0});", Param.Where(p => !p.IsActive).Select(p => p.EquipmentID).GetStringEnumerable()));

                try
                {
                    await GetConnectionAsync().ExecuteAsync(builder.ToString());
                }
                catch (SQLiteException ex)
                {
                    switch (ex.Result)
                    {
                        case SQLite.Net.Interop.Result.Error:
                            if (ex.Message.Equals(conMessage))
                            {
                                BufferCustom.Add(new CustomQuery()
                                {
                                    Query = builder.ToString(),
                                    args = new object[0]
                                });
                            }
                            else
                                throw;

                            break;

                        case SQLite.Net.Interop.Result.Busy:
                        case SQLite.Net.Interop.Result.Locked:
                            BufferCustom.Add(new CustomQuery()
                            {
                                Query = builder.ToString(),
                                args = new object[0]
                            });
                            break;

                        default:
                            throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                await repoSyncro.UpdateTableAsync(Syncro.Tables.Equipments, true);
            }

            if (Param.Any(p => p.UpdateArea))
            {
                var buffer = new List<AreasEquipments>();

                foreach (var item in Param.Where(p => p.AreaID > 0))
                {
                    var exist = allarea.SingleOrDefault(p => p.EquipmentID == item.EquipmentID);

                    if (exist == null)
                    {
                        buffer.Add(new AreasEquipments() { EquipmentID = item.EquipmentID, AreaID = item.AreaID, Sync = true });
                    }
                    else
                    {
                        exist.AreaID = item.AreaID;
                        exist.Sync = true;
                        buffer.Add(exist);
                    }
                }

                await repoa.InsertAsyncAll(buffer.Where(p => p.ID == 0).ToList());
                await repoa.UpdateAllAsync(buffer.Where(p => p.ID > 0).ToList());

                if (Param.Any(p => p.AreaID == 0))
                {
                    var query = String.Format("UPDATE AreasEquipments Set Sync=1, IsDeleted=1 WHERE EquipmentID IN ({0});", Param.Where(p => p.AreaID == 0).Select(p => p.EquipmentID).GetStringEnumerable());
                    await GetConnectionAsync().ExecuteAsync(query);
                }

                await repoSyncro.UpdateTableAsync(Syncro.Tables.Areas, true);
            }
        }

        public async Task UpdateConfig(List<Int32> configs)
        {
            var ConfigStatus = (Byte)Configs._Status.Inactived;

            var query = String.Format("UPDATE Configs Set Sync=1, ModifyDate={2}, Status={0}, Logon2='{3}' WHERE ID IN ({1});", ConfigStatus, configs.GetInt32Enumerable(), DateTime.Now.ToUniversalTime().Ticks, Proceso.Logon);

            try
            {
                await GetConnectionAsync().ExecuteAsync(query);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            BufferCustom.Add(new CustomQuery()
                            {
                                Query = query,
                                args = new object[0]
                            });
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        BufferCustom.Add(new CustomQuery()
                        {
                            Query = query,
                            args = new object[0]
                        });
                        break;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            var repoSincro = new RepositorySyncro(this.Connection);

            await repoSincro.UpdateTableAsync(Syncro.Tables.Configs, true);
        }

        public List<EquipmentTypes> GetEquipmentTypes()
        {
            return AsyncHelper.RunSync<List<EquipmentTypes>>(() => GetEquipmentTypesAsync());
        }

        public async Task<List<EquipmentTypes>> GetEquipmentTypesAsync()
        {
            var Intentado = false;

            VolveraIntentar:

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
                            goto VolveraIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
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

        public async Task<NextConfig> GetNextConfig(String Equipment)
        {
            var Intentado = false;

            VolveraIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var status = (Byte)Configs._Status.Actived;

                var builder = new StringBuilder();

                builder.Append("SELECT ");
                builder.Append("    C.ID AS ConfigID, ");
                builder.Append("    P.Code AS ProductCode, ");
                builder.Append("    P.Name AS ProductName, ");
                builder.Append("    P.Short AS ProductShort, ");
                builder.Append("    P.ProductType AS ProductType, ");
                builder.Append("    C.Begin, ");
                builder.Append("    C.EquipmentID, ");
                builder.Append("    E.Name AS Equipment, ");
                builder.Append("    C.SubEquipmentID, ");
                builder.Append("    E2.Name AS SubEquipment, ");
                builder.Append("    C.IsCold, ");
                builder.Append("    C.Identifier, ");
                builder.Append("    C.TimeID ");
                builder.Append("FROM         Configs C ");
                builder.Append("INNER JOIN   Materials P ");
                builder.Append("ON           C.ProductCode = P.Code ");
                builder.Append("INNER JOIN   Equipments E ");
                builder.Append("ON           C.EquipmentID = E.ID ");
                builder.Append("LEFT JOIN    Equipments E2 ");
                builder.Append("ON           C.SubEquipmentID = E2.ID ");
                builder.Append("WHERE        ? IN (C.EquipmentID, C.SubEquipmentID) ");
                builder.Append("AND          C.Status = ? ");
                builder.Append("ORDER BY     C.Begin ");

                var result = await GetConnectionAsync().QueryAsync<NextConfig>(builder.ToString(), Equipment, (Byte)status);

                return result.Any() ? result.FirstOrDefault() : null;
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolveraIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
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

        public List<LotsList> GetMaterialLotConfig(List<String> Codes)
        {
            return AsyncHelper.RunSync<List<LotsList>>(() => GetMaterialLotConfigAsync(Codes));
        }

        public async Task<List<LotsList>> GetMaterialLotConfigAsync(List<String> Codes)
        {
            var builder = new StringBuilder();

            if (Codes == null) return new List<LotsList>();

            var lista = Codes.GetStringEnumerable();

            builder.Append("SELECT  ");
            builder.Append("    MaterialCode, ");
            builder.Append("    Code, ");
            builder.Append("    Reference, ");
            builder.Append("    Expire ");
            builder.Append("FROM   Lots ");
            builder.Append(String.Format("WHERE   MaterialCode IN ({0})", lista));

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<LotsList>(builder.ToString());
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

        public List<MaterialList> GetMaterialConfig(String ProductCode, String VerID)
        {
            return AsyncHelper.RunSync<List<MaterialList>>(() => GetMaterialConfigAsync(ProductCode, VerID));
        }

        public async Task<List<MaterialList>> GetMaterialConfigAsync(String ProductCode, String VerID)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT  ");
            builder.Append("    M.Name AS MaterialName, ");
            builder.Append("    M.Code AS MaterialCode, ");
            builder.Append("    M.Reference AS MaterialReference, ");
            builder.Append("    M.Unit AS MaterialUnit, ");
            builder.Append("    IFNULL(MZ.NeedBoxNo, 0) AS NeedBoxNo, ");
            builder.Append("    IFNULL(MZ.Cantidad, 0) AS NeedCantidad, ");
            builder.Append("    IFNULL(MZ.AllowNoLot, 0) AS AllowLotDay, ");
            builder.Append("    IFNULL(MZ.Days, 0) AS ExpireDay, ");
            builder.Append("    IFNULL(MZ.IgnoreStock, 0) AS IgnoreStock, ");
            builder.Append("    U.Ean, ");
            builder.Append("    U.Unit, ");
            builder.Append("    U.IsBase, ");
            builder.Append("    U.[From], ");
            builder.Append("    U.[To] ");
            builder.Append("FROM         ConfigMaterials CM ");
            builder.Append("INNER JOIN   Materials M ");
            builder.Append("ON           M.Code = CM.MaterialCode ");
            builder.Append("INNER JOIN   Units U ");
            builder.Append("ON           U.MaterialCode = M.Code ");
            builder.Append("LEFT JOIN    MaterialsZilm MZ ");
            builder.Append("ON           MZ.MaterialCode = M.Code ");
            builder.Append("WHERE        CM.ProductCode = ? ");
            builder.Append("AND          CM.VerID = ? ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var result = await GetConnectionAsync().QueryAsync<ConfigMaterialList>(builder.ToString(), ProductCode, VerID);

                return result.GroupBy(g => new { g.MaterialName, g.MaterialCode, g.MaterialReference, g.MaterialUnit, g.NeedBoxNo, g.NeedCantidad, g.AllowLotDay, g.ExpireDay, g.IgnoreStock })
                    .Select(p => new MaterialList
                    {
                        MaterialCode = p.Key.MaterialCode.Trim(),
                        MaterialName = p.Key.MaterialName,
                        MaterialReference = p.Key.MaterialReference,
                        MaterialUnit = p.Key.MaterialUnit,
                        NeedBoxNo = p.Key.NeedBoxNo,
                        NeedCantidad = p.Key.NeedCantidad,
                        AllowLotDay = p.Key.AllowLotDay,
                        ExpireDay = p.Key.ExpireDay,
                        IgnoreStock = p.Key.IgnoreStock,
                        units = p.Select(u => new Units
                        {
                            Ean = u.Ean,
                            Unit = u.Unit,
                            IsBase = u.IsBase,
                            From = u.From,
                            To = u.To
                        }).ToList()
                    }).ToList();
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

        public async Task<ActualConfig> GetActualConfig(String Equipment)
        {
            var Intentado = false;

            VolveraIntentar:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();

                var status = (Byte)Configs._Status.Enabled;

                builder.Append("SELECT  ");
                builder.Append("    C.ID AS ConfigID, ");
                builder.Append("    C.Begin, ");
                builder.Append("    C.VerID, ");
                builder.Append("    C.TimeID, ");
                builder.Append("    C.EquipmentID, ");
                builder.Append("    E.Name AS Equipment, ");
                builder.Append("    C.SubEquipmentID, ");
                builder.Append("    E.Name AS Equipment, ");
                builder.Append("    P.Code AS ProductCode, ");
                builder.Append("    P.Name AS ProductName, ");
                builder.Append("    P.ProductType AS ProductType, ");
                builder.Append("    P.Reference AS ProductReference, ");
                builder.Append("    P.Short AS ProductShort, ");
                builder.Append("    C.IsCold, ");
                builder.Append("    C.Identifier, ");
                builder.Append("    T.Producto, ");
                builder.Append("    T.Copias ");
                builder.Append("FROM         Configs C ");
                builder.Append("INNER JOIN   Materials P ");
                builder.Append("ON           C.ProductCode = P.Code ");
                builder.Append("INNER JOIN   Equipments E ");
                builder.Append("ON           C.EquipmentID = E.ID ");
                builder.Append("INNER JOIN   Times T ");
                builder.Append("ON           T.ID = C.TimeID ");
                builder.Append("LEFT JOIN    Equipments E2 ");
                builder.Append("ON           C.SubEquipmentID = E2.ID ");
                builder.Append("WHERE        ? IN (C.EquipmentID, C.SubEquipmentID) ");
                builder.Append("AND          C.Status = ? ");

                var result = await GetConnectionAsync().QueryAsync<ActualConfig>(builder.ToString(), Equipment, status);

                var first = result.FirstOrDefault();

                if (first != null)
                {
                    var Code = first.ProductCode;
                    first.units = await GetConnectionAsync().Table<Units>().Where(p => p.MaterialCode == Code).ToListAsync();
                }

                return first;
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            Intentado = true;
                            goto VolveraIntentar;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        Intentado = true;
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

        public async Task UpdateConfigStatusAsync(Int32 ConfigID, Configs._Status status, String Logon = null, Boolean IsCold = false, String Identifier = "", String ProductType = "", Boolean IsSubEquipment = false)
        {
            var fecha = DateTime.Now.ToUniversalTime();
            var query = String.Empty;

            if (status == Configs._Status.Enabled)
                query = String.Format("UPDATE Configs SET Sync={8}, ProductType='{7}', Identifier='{6}', ExecuteDate={0}, ModifyDate={1}, Status={2}, Logon2='{4}', IsCold={5} WHERE ID={3}",
                    fecha.Ticks, fecha.Ticks, (Int32)status, ConfigID, Logon ?? Proceso.Logon, IsCold ? 1 : 0, Identifier, ProductType, IsSubEquipment ? 0 : 1);
            else
                query = String.Format("UPDATE Configs SET Sync={4}, ModifyDate={0}, Status={1}, Logon2='{3}' WHERE ID={2}", fecha.Ticks, (Int32)status, ConfigID, Logon ?? Proceso.Logon, IsSubEquipment ? 0 : 1);

            var con = GetConnectionAsync();

            try
            {
                await con.ExecuteAsync(query);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            BufferCustom.Add(new CustomQuery()
                            {
                                Query = query,
                                args = new object[0]
                            });
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        BufferCustom.Add(new CustomQuery()
                        {
                            Query = query,
                            args = new object[0]
                        });
                        break;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            var repoSincro = new RepositorySyncro(this.Connection);

            await repoSincro.UpdateTableAsync(Syncro.Tables.Configs, true);
        }

        public void UpdateConfigStatus(Int32 ConfigID, Configs._Status status, String Logon = null, Boolean IsCold = false)
        {
            var fecha = DateTime.Now.ToUniversalTime();
            var query = String.Empty;

            if (status == Configs._Status.Enabled)
                query = String.Format("UPDATE Configs SET Sync=1, ExecuteDate={0}, ModifyDate={1}, Status={2}, Logon2='{4}', IsCold={5} WHERE ID={3}", fecha.Ticks, fecha.Ticks, (Int32)status, ConfigID, Logon ?? Proceso.Logon, IsCold ? 1 : 0);
            else
                query = String.Format("UPDATE Configs SET Sync=1, ModifyDate={0}, Status={1}, Logon2='{3}' WHERE ID={2}", fecha.Ticks, (Int32)status, ConfigID, Logon ?? Proceso.Logon);

            var con = GetConnection();

            try
            {
                con.Execute(query);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            BufferCustom.Add(new CustomQuery()
                            {
                                Query = query,
                                args = new object[0]
                            });
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        BufferCustom.Add(new CustomQuery()
                        {
                            Query = query,
                            args = new object[0]
                        });
                        break;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            var repoSincro = new RepositorySyncro(this.Connection);

            repoSincro.UpdateTable(Syncro.Tables.Configs, true);
        }

        public async Task<List<SyncResult>> ValidateTables(List<SyncRequest> tables)
        {
            List<SyncResult> retorno = new List<SyncResult>();

            var url = GetService(ServicesType.GET_SYNC_TABLES);

            //if (tables.Any(p => p.TABLE == "Lots" || p.TABLE == "Materials"))
            //{
            //    url = url.Replace("ladostgsap02", "ladostgsap01");
            //}

            var json = await PostJsonAsync(url, tables);

            if (json.isOk && !json.Json.IsJsonEmpty())
            {
                retorno = JsonConvert.DeserializeObject<SyncResult[]>(json.Json).ToList();
            }
            else if (!json.isOk)
            {
                throw json.ex;
            }

            #region Force para error de tabla Syncro que no encendio el indicador de cambios

            /// Tabla traza
            var ProductsRoutes = tables.SingleOrDefault(s => s.TABLE.ToLower().Equals("productsroutes"));

            if (ProductsRoutes != null && !ProductsRoutes.Sync)
            {
                var conteo = await GetConnectionAsync().Table<ProductsRoutes>().Where(w => w.Sync).CountAsync();

                if (conteo > 0)
                {
                    ProductsRoutes.Sync = true;
                    tables.RemoveAll(r => r.TABLE == ProductsRoutes.TABLE);
                    tables.Add(ProductsRoutes);
                }
            }

            /// Tabla planificacion
            var Configs = tables.SingleOrDefault(s => s.TABLE.ToLower().Equals("configs"));

            if (Configs != null && !Configs.Sync)
            {
                var conteo = await GetConnectionAsync().Table<Configs>().Where(w => w.Sync).CountAsync();

                if (conteo > 0)
                {
                    Configs.Sync = true;
                    tables.RemoveAll(r => r.TABLE == Configs.TABLE);
                    tables.Add(Configs);
                }
            }

            /// Tabla Bandejas
            var TraysProducts = tables.SingleOrDefault(s => s.TABLE.ToLower().Equals("traysproducts"));

            if (TraysProducts != null && !TraysProducts.Sync)
            {
                var conteo = await GetConnectionAsync().Table<TraysProducts>().Where(w => w.Sync).CountAsync();

                if (conteo > 0)
                {
                    TraysProducts.Sync = true;
                    tables.RemoveAll(r => r.TABLE == TraysProducts.TABLE);
                    tables.Add(TraysProducts);
                }
            }

            /// Tabla Inventario locales de la tabla 
            var Stocks = tables.SingleOrDefault(s => s.TABLE.ToLower().Equals("stocks"));

            if (Stocks != null && !Stocks.Sync)
            {
                var conteo = await GetConnectionAsync().Table<Stocks>().Where(w => w.Sync).CountAsync();

                if (conteo > 0)
                {
                    Stocks.Sync = true;
                    tables.RemoveAll(r => r.TABLE == Stocks.TABLE);
                    tables.Add(Stocks);
                }
            }

            #endregion

            foreach (var item in tables.Where(p => p.Sync))
            {
                if (!retorno.Any(p => p.TABLE == item.TABLE))
                {
                    retorno.Add(new SyncResult()
                    {
                        TABLE = item.TABLE,
                        CPUDT = item.CPUDT,
                        CPUTM = item.CPUTM
                    });
                }
            }

            return retorno;
        }

        public async Task<TraysList> GetBandejaSalida(String ID, Int16 Secuencia, String TimeID = null)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT  ");
            builder.Append("   TT.TrayID, ");
            builder.Append("   CASE WHEN IFNULL(TP.Quantity, 0) > 0 THEN TP.Quantity ELSE TT.Quantity END AS Quantity, ");
            builder.Append("   TT.Unit, ");
            builder.Append("   TP.Secuencia, ");
            builder.Append("   TP.ProductCode, ");
            builder.Append("   TP.VerID, ");
            builder.Append("   TP.TimeID, ");
            builder.Append("   TP.Status, ");
            builder.Append("   TP.ElaborateID, ");
            builder.Append("   TP.EquipmentID, ");
            builder.Append("   TP.Fecha, ");
            builder.Append("   TP.BatchID, ");
            builder.Append("   TP.ModifyDate ");
            builder.Append("FROM         TraysTimes TT ");
            builder.Append("LEFT JOIN    TraysProducts TP ");
            builder.Append("ON           TT.TrayID = TP.TrayID ");
            builder.Append("AND          TP.Secuencia = ? ");
            builder.Append("WHERE        TT.TrayID = ? ");

            if (!String.IsNullOrEmpty(TimeID))
            {
                builder.Append(String.Format("AND        TT.TimeID = '{0}' ", TimeID));
            }

            // builder.Append("ORDER BY     TP.ProductCode DESC"); Ordenación Innecesaria 2016.09.07

            var con = GetConnectionAsync();

            List<TraysList> Result = null;

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                Result = await con.QueryAsync<TraysList>(builder.ToString(), Secuencia, ID.ToUpper());
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

            var single = Result.FirstOrDefault();

            if (single != null && single.Secuencia == 0)
                single.Secuencia = Secuencia;

            return single;
        }

        public async Task<TraysList> GetBandejaConfig(String ProcessId, String TimeID, String Unit = null)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT  ");
            builder.Append("   TT.TrayID, ");
            builder.Append("   TT.Quantity, ");
            builder.Append("   TT.Unit ");
            builder.Append("FROM         TraysTimes TT ");
            builder.Append(String.Format("WHERE      TT.IdProceso = '{0}'", ProcessId));
            builder.Append(String.Format("AND        TT.TimeID = '{0}'", TimeID));

            if (!String.IsNullOrEmpty(Unit))
            {
                builder.Append(String.Format("AND     TrayID = '{0}'", Unit));
            }

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            List<TraysList> Result = null;

            try
            {
                Result = await GetConnectionAsync().QueryAsync<TraysList>(builder.ToString());
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

            return Result.Any() ? Result.First() : null;
        }

        public async Task<TraysList> GetBandejaEntrada(String ID, Int16 Secuencia, String TimeID)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT  ");
            builder.Append("   P.TrayID, ");
            builder.Append("   P.Secuencia, ");
            builder.Append("   P.ProductCode, ");
            builder.Append("   P.VerID, ");
            builder.Append("   P.TimeID, ");
            builder.Append("   P.Quantity, ");
            builder.Append("   P.Unit, ");
            builder.Append("   S.Status ");
            builder.Append("FROM         TraysProducts P ");
            builder.Append("INNER JOIN   TraysStatus S ");
            builder.Append("ON           P.TrayID = S.TrayID ");
            builder.Append("AND          P.Secuencia = S.Secuencia ");
            builder.Append("WHERE        P.TrayID = ? ");
            builder.Append("AND          P.Secuencia = ? ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var result = await GetConnectionAsync().QueryAsync<TraysList>(builder.ToString(), ID, Secuencia);

                return result.SingleOrDefault();
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

        //Consulta Cantidad de Cigarros Consumidos en un Equipo de un Tipo de Producto durante un turno.
        //Date: 2021-10-25, Raldy de Jesus
        public async Task<ConsumptionTotal> GetTotalElaborateQtyTurno(String TrayID , String EquipmentID, DateTime fecha, String ProductCode, Byte TurnID)
        {
           
            var builder = new StringBuilder();
            builder.Append("SELECT  ");
            builder.Append("    Unit,");
            builder.Append("    COUNT(Unit) AS Quantity, ");
            builder.Append("    SUM(Quantity) AS Amounts ");
            builder.Append("FROM    Consumptions ");
            builder.Append("WHERE   CustomFecha = ? ");
            builder.Append("AND     ProductCode = ? ");
            builder.Append("AND     TurnID = ? ");
            builder.Append("AND     TrayID  = ? ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var result = await GetConnectionAsync().QueryAsync<ConsumptionTotal>(builder.ToString(), TrayID,  fecha.GetSapDate(), ProductCode, TurnID);
                var rest = result.FirstOrDefault();
                if (rest == null) rest = new ConsumptionTotal();
                rest.SecuenciaEmpaque = await GetConnectionAsync().ExecuteScalarAsync<Int16>("SELECT MAX(SecuenciaEmpaque) FROM ProductsRoutes WHERE CustomFecha = ? AND EquipmentID = ? AND TurnID = ?", fecha.GetSapDate(), (Proceso.IsSubEquipment ? Proceso.SubEquipmentID : Proceso.EquipmentID), TurnID);
                return rest;
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


        public async Task<ElaborateTotal> GetTotalElaborate(DateTime fecha, String ProductCode, Byte TurnID)
        {
            var builder = new StringBuilder();
            builder.Append("SELECT  ");
            builder.Append("    Unit,");
            builder.Append("    COUNT(Unit) AS Quantity, ");
            builder.Append("    SUM(Quantity) AS Amounts ");
            builder.Append("FROM    Elaborates ");
            builder.Append("WHERE   CustomFecha = ? ");
            builder.Append("AND     ProductCode = ? ");
            builder.Append("AND     TurnID = ? ");
            builder.Append("AND     IsReturn = 0 ");
            builder.Append("GROUP BY   Unit");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var result = await GetConnectionAsync().QueryAsync<ElaborateTotal>(builder.ToString(), fecha.GetSapDate(), ProductCode, TurnID);
                var rest = result.FirstOrDefault();
                if (rest == null) rest = new ElaborateTotal();
                rest.SecuenciaEmpaque = await GetConnectionAsync().ExecuteScalarAsync<Int16>("SELECT MAX(SecuenciaEmpaque) FROM ProductsRoutes WHERE CustomFecha = ? AND EquipmentID = ? AND TurnID = ?", fecha.GetSapDate(), (Proceso.IsSubEquipment ? Proceso.SubEquipmentID : Proceso.EquipmentID), TurnID);
                return rest;
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

        public async Task<IEnumerable<NextConfig>> GetNextConfigs(String Equipment)
        {
            var statusActived = (Byte)Configs._Status.Actived;
            var statusEnabled = (Byte)Configs._Status.Enabled;

            var builder = new StringBuilder();
            builder.Append("SELECT ");
            builder.Append("    C.ID AS ConfigID, ");
            builder.Append("    C.Status, ");
            builder.Append("    P.Code AS ProductCode, ");
            builder.Append("    P.Name AS ProductName, ");
            builder.Append("    P.Short AS ProductShort, ");
            builder.Append("    C.Begin, ");
            builder.Append("    C.TimeID, ");
            builder.Append("    T.Producto ");
            builder.Append("FROM         Configs C ");
            builder.Append("INNER JOIN   Materials P ");
            builder.Append("ON           C.ProductCode = P.Code ");
            builder.Append("INNER JOIN   Times T ");
            builder.Append("ON           T.ID = C.TimeID ");
            builder.Append("WHERE        ? IN (C.EquipmentID, C.SubEquipmentID) ");
            builder.Append("AND          C.Status IN (?,?) ");
            builder.Append("AND          C.Begin <= ? ");
            builder.Append("ORDER BY     C.Status DESC, C.Begin ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<NextConfig>(builder.ToString(), Equipment, statusActived, statusEnabled, DateTime.Now.AddDays(1).Ticks);
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

        public Materials GetMaterialByCodeOrReference(String MaterialCode)
        {
            return AsyncHelper.RunSync<Materials>(() => GetMaterialByCodeOrReferenceAsync(MaterialCode));
        }

        public async Task<Materials> GetMaterialByCodeOrReferenceAsync(String MaterialCode)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var material = MaterialCode.PadLeft(18, '0');
                return await GetConnectionAsync().Table<Materials>().Where(p => p.Code == material || p.Reference == MaterialCode).FirstOrDefaultAsync();
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

        public Materials GetMaterialByCode(String MaterialCode)
        {
            var material = AsyncHelper.RunSync<Materials>(() => GetMaterialByCodeAsync(MaterialCode));
            return material;
        }

        private Materials MaterialCaching = new Materials();

        public async Task<Materials> GetMaterialByCodeAsync(String MaterialCode)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var material = MaterialCode.PadLeft(18, '0');
                if (MaterialCaching == null || (MaterialCaching.Code ?? String.Empty) != material)
                {
                    MaterialCaching = await GetConnectionAsync().Table<Materials>().Where(p => p.Code == material).FirstOrDefaultAsync();
                }

                return MaterialCaching;
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

        public async Task<Units> GetUnitByCode(String Ean)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                return await GetConnectionAsync().Table<Units>().Where(p => p.Ean == Ean).FirstOrDefaultAsync();
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

        public async Task<Boolean> ValidaBoxNumberAsync(String MaterialCode, String Lot, Int16 BoxNumber)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var caja = await GetConnectionAsync().Table<Consumptions>().Where(p => p.MaterialCode == MaterialCode && p.Lot == Lot && p.BoxNumber == BoxNumber).OrderByDescending(o => o.ID).FirstOrDefaultAsync();

                if (caja != null && caja.Quantity < 0) return false;

                return caja != null;
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

        public Boolean ValidaBoxNumber(String MaterialCode, String Lot, Int16 BoxNumber)
        {
            return AsyncHelper.RunSync<Boolean>(() => ValidaBoxNumberAsync(MaterialCode, Lot, BoxNumber));
        }

        public async Task<List<MaterialReport>> GetLastMaterial(String ProductCode, String VerID, DateTime? Produccion = null, Byte? TurnID = null)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();

                builder.Append("SELECT  ");
                builder.Append("    M.Name AS MaterialName, ");
                builder.Append("    M.Code AS MaterialCode, ");
                builder.Append("    M.Reference AS MaterialReference, ");
                builder.Append("    M.Unit, ");
                builder.Append("    C.ID, ");
                builder.Append("    C.CustomID, ");
                builder.Append("    C.Quantity + Last.Quantity AS EntryQuantity, ");
                builder.Append("    C.Unit AS MaterialUnit, ");
                builder.Append("    C.Logon, ");
                builder.Append("    C.BoxNumber, ");
                builder.Append("    C.Produccion, ");
                builder.Append("    C.TurnID, ");
                builder.Append("    C.Lot, ");
                builder.Append("    L.Reference AS LoteSuplidor, ");
                builder.Append("    (SELECT Percent FROM MaterialsZilm Z WHERE Z.MaterialCode = M.Code) AS NeedPercent, ");
                builder.Append("    (SELECT IgnoreStock FROM MaterialsZilm Z WHERE Z.MaterialCode = M.Code) AS IgnoreStock ");
                builder.Append("FROM         Consumptions C ");
                builder.Append("INNER JOIN   Materials M ");
                builder.Append("ON           M.Code = C.MaterialCode ");
                builder.Append("INNER JOIN   Lots L ");
                builder.Append("ON           L.MaterialCode = C.MaterialCode ");
                builder.Append("AND          L.Code = C.Lot ");
                builder.Append("INNER JOIN ");
                builder.Append("(     ");
                builder.Append("    SELECT   ");
                builder.Append("        SC.MaterialCode, ");
                builder.Append("        MAX(SC.Fecha) AS Fecha, ");
                builder.Append("        IFNULL(Returned.Quantity, 0) AS Quantity ");
                builder.Append("    FROM        Consumptions SC ");
                builder.Append("    LEFT JOIN    ");
                builder.Append("    (   ");
                builder.Append("        SELECT    ");
                builder.Append("            Produccion, ");
                builder.Append("            MaterialCode, ");
                builder.Append("            Lot, ");
                builder.Append("            SUM(Quantity) AS Quantity ");
                builder.Append("        FROM  Consumptions ");
                builder.Append("        WHERE Quantity < 0 ");
                builder.Append("        GROUP BY  Produccion, MaterialCode, Lot ");
                builder.Append("    ) Returned ");
                builder.Append("    ON          Returned.MaterialCode = SC.MaterialCode ");
                builder.Append("    AND         Returned.Produccion = SC.Produccion ");
                builder.Append("    AND         Returned.Lot = SC.Lot ");
                builder.Append("    WHERE       SC.Quantity > 0 ");
                builder.Append("    AND         (SC.Quantity + IFNULL(Returned.Quantity, 0)) > 0 ");
                builder.Append("    AND         ProductCode = ? ");
                builder.Append("    AND         VerID = ? ");

                if (Produccion.HasValue && TurnID.HasValue)
                {
                    var pfecha = Produccion.Value.GetDBDate();
                    builder.Append(String.Format("    AND     CustomFecha = {0}", pfecha));
                    builder.Append(String.Format("    AND     TurnID = {0}", TurnID.Value));
                }

                builder.Append("    GROUP BY    SC.MaterialCode ");
                builder.Append(") Last ");
                builder.Append("ON          Last.MaterialCode = C.MaterialCode ");
                builder.Append("AND         Last.Fecha = C.Fecha ");
                builder.Append("WHERE       C.Quantity > 0 ");

                return await GetConnectionAsync().QueryAsync<MaterialReport>(builder.ToString(), ProductCode, VerID);
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

        public async Task<List<MaterialReport>> GetLastTraysMaterial(String ProductCode, String VerID, DateTime? Produccion = null, Byte? TurnID = null)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();

                builder.Append("SELECT  ");
                builder.Append("    C.TrayId AS TrayID, ");
                builder.Append("    M.Name AS MaterialName, ");
                builder.Append("    M.Code AS MaterialCode, ");
                builder.Append("    M.Reference AS MaterialReference, ");
                builder.Append("    M.Unit, ");
                builder.Append("    C.ID, ");
                builder.Append("    C.CustomID, ");
                builder.Append("    C.Quantity + Last.Quantity AS EntryQuantity, ");
                builder.Append("    C.Unit AS MaterialUnit, ");
                builder.Append("    C.Logon, ");
                builder.Append("    C.BoxNumber, ");
                builder.Append("    C.Produccion, ");
                builder.Append("    C.TurnID, ");
                builder.Append("    C.Lot, ");
                builder.Append("    C.BatchId AS BatchID, ");
                builder.Append("    C.TrayEquipmentID AS TrayEquipmentID, ");
                builder.Append("    C.ElaborateID AS ElaborateID, ");
                builder.Append("    C.TrayDate AS TrayDate, ");
                builder.Append("    (SELECT Percent FROM MaterialsZilm Z WHERE Z.MaterialCode = M.Code) AS NeedPercent, ");
                builder.Append("    (SELECT IgnoreStock FROM MaterialsZilm Z WHERE Z.MaterialCode = M.Code) AS IgnoreStock ");
                builder.Append("FROM         Consumptions C ");
                builder.Append("INNER JOIN   Materials M ");
                builder.Append("ON           M.Code = C.MaterialCode ");
                builder.Append("INNER JOIN ");
                builder.Append("(     ");
                builder.Append("    SELECT   ");
                builder.Append("        SC.MaterialCode, ");
                builder.Append("        MAX(SC.Fecha) AS Fecha, ");
                builder.Append("        IFNULL(Returned.Quantity, 0) AS Quantity ");
                builder.Append("    FROM        Consumptions SC ");
                builder.Append("    LEFT JOIN    ");
                builder.Append("    (   ");
                builder.Append("        SELECT    ");
                builder.Append("            Produccion, ");
                builder.Append("            MaterialCode, ");
                builder.Append("            TrayId, ");
                builder.Append("            SUM(Quantity) AS Quantity ");
                builder.Append("        FROM  Consumptions ");
                builder.Append("        WHERE Quantity < 0 ");
                builder.Append("        GROUP BY  Produccion, MaterialCode, Lot ");
                builder.Append("    ) Returned ");
                builder.Append("    ON          Returned.MaterialCode = SC.MaterialCode ");
                builder.Append("    AND         Returned.Produccion = SC.Produccion ");
                builder.Append("    AND         Returned.TrayId = SC.TrayId");
                builder.Append("    WHERE       SC.Quantity > 0 ");
                builder.Append("    AND         (SC.Quantity + IFNULL(Returned.Quantity, 0)) > 0 ");
                builder.Append("    AND         ProductCode = ? ");
                builder.Append("    AND         VerID = ? ");

                if (Produccion.HasValue)
                {
                    var pfecha = Produccion.Value.GetDBDate();
                    builder.Append(String.Format("    AND     CustomFecha = {0}", pfecha));
                    
                    if (TurnID.HasValue)
                    {
                        builder.Append(String.Format("    AND     TurnID = {0}", TurnID.Value));
                    }
                }

                builder.Append("    GROUP BY    SC.MaterialCode ");
                builder.Append(") Last ");
                builder.Append("ON          Last.MaterialCode = C.MaterialCode ");
                builder.Append("AND         Last.Fecha = C.Fecha ");
                builder.Append("WHERE       C.Quantity > 0  AND C.TRAYID IS NOT NULL");

                return await GetConnectionAsync().QueryAsync<MaterialReport>(builder.ToString(), ProductCode, VerID);
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
        
        public async Task<List<MaterialReport>> GetLastReturn()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();

                builder.Append("SELECT  ");
                builder.Append("    M.Name AS MaterialName, ");
                builder.Append("    M.Code AS MaterialCode, ");
                builder.Append("    M.Reference AS MaterialReference, ");
                builder.Append("    M.Unit AS MaterialUnit, ");
                builder.Append("    C.Quantity * -1 AS EntryQuantity, ");
                builder.Append("    C.Fecha, ");
                builder.Append("    C.Lot, ");
                builder.Append("    C.BoxNumber, ");
                builder.Append("    L.Reference AS LoteSuplidor,");
                builder.Append("    (SELECT Percent FROM MaterialsZilm Z WHERE Z.MaterialCode = M.Code) AS NeedPercent ");
                builder.Append("FROM         Consumptions C ");
                builder.Append("INNER JOIN   Materials M ");
                builder.Append("ON           M.Code = C.MaterialCode ");
                builder.Append("AND          C.Quantity < 0 ");
                builder.Append("INNER JOIN   Lots L ");
                builder.Append("ON           L.MaterialCode = C.MaterialCode ");
                builder.Append("AND          L.Code = C.Lot ");
                builder.Append("ORDER BY     C.Fecha DESC ");
                builder.Append("LIMIT 5");

                return await GetConnectionAsync().QueryAsync<MaterialReport>(builder.ToString());
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

        public async Task<List<MaterialReport>> GetInitialMaterial()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();

                builder.Append("SELECT  DISTINCT ");
                builder.Append("    M.Name AS MaterialName, ");
                builder.Append("    M.Code AS MaterialCode, ");
                builder.Append("    M.Reference AS MaterialReference, ");
                builder.Append("    R.Lot, ");
                builder.Append("    R.Unit AS MaterialUnit, ");
                builder.Append("    R.BoxNumber, ");
                builder.Append("    R.Quantity ");
                builder.Append("FROM         StocksDetails R ");
                builder.Append("INNER JOIN   Materials M ");
                builder.Append("ON           M.Code = R.MaterialCode ");
                builder.Append("WHERE        R.StockID = (SELECT MAX(ID) FROM Stocks)");

                return await GetConnectionAsync().QueryAsync<MaterialReport>(builder.ToString());
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

        public async Task<List<MaterialReport>> GetMaterialStockAsync(DateTime Produccion, Byte TurnID, Stocks stock)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();

                var fecha = Produccion.GetDBDate();

                builder.Append("SELECT  ");
                builder.Append("    M.Name AS MaterialName, ");
                builder.Append("    M.Code AS MaterialCode, ");
                builder.Append("    M.Reference AS MaterialReference, ");
                builder.Append("    R.Lot, ");
                builder.Append("    R.Unit AS MaterialUnit, ");
                builder.Append("    R.BoxNumber, ");
                builder.Append("    R.Quantity, ");
                builder.Append("    R.Quantity2 AS Acumulated, ");
                builder.Append("    (SELECT Percent FROM MaterialsZilm Z WHERE Z.MaterialCode = R.MaterialCode) AS NeedPercent, ");
                builder.Append("    (SELECT IgnoreStock FROM MaterialsZilm Z WHERE Z.MaterialCode = R.MaterialCode) AS IgnoreStock, ");
                builder.Append("    (SELECT TrayID FROM Consumptions C WHERE C.MaterialCode = R.MaterialCode) AS TrayID ");
                builder.Append("FROM         StocksDetails R ");
                builder.Append("INNER JOIN   Materials M ");
                builder.Append("ON           M.Code = R.MaterialCode ");
                builder.Append("INNER JOIN   Stocks S ");
                builder.Append("ON           S.ID = R.StockID ");
                builder.Append("WHERE        S.CustomFecha = ? ");
                builder.Append("AND          S.TurnID = ?");

                return await GetConnectionAsync().QueryAsync<MaterialReport>(builder.ToString(), fecha, TurnID);
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

        public async Task<Stocks> GetActualStock()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Stocks>().Where(p => p.Status == Stocks._Status.Abierto).FirstOrDefaultAsync();
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

        public Boolean ExistStock(DateTime ProduccionDate, Byte TurnID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var fecha = ProduccionDate.GetDBDate();

                var stock = GetConnection().Table<Stocks>().Where(p => p.CustomFecha == fecha && p.TurnID == TurnID).FirstOrDefault();

                return stock != null;
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

        public Stocks ExistOpenStock()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var stock = GetConnection().Table<Stocks>().Where(p => p.Status == Stocks._Status.Abierto).FirstOrDefault();

                return stock;
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

        public Stocks ExistClosedStock(DateTime ProduccionDate, Byte TurnID)
        {
            return AsyncHelper.RunSync<Stocks>(() => ExistClosedStockAsync(ProduccionDate, TurnID));
        }

        public async Task<Stocks> ExistClosedStockAsync(DateTime ProduccionDate, Byte TurnID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var fecha = ProduccionDate.GetDBDate();

                return await GetConnectionAsync().Table<Stocks>().Where(p => p.CustomFecha == fecha && p.TurnID == TurnID).FirstOrDefaultAsync();
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

        public Boolean StockIsReported(Int32 ProduccionDate, Byte TurnID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var stock = GetConnection().Table<Stocks>().Where(p => p.CustomFecha == ProduccionDate && p.TurnID == TurnID).FirstOrDefault();
                return stock.IsNotified;
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

        public async Task<List<MaterialReport>> GetWastesMaterial(DateTime ProduccionDate, Byte TurnID, String ProductCode, String VerID, Int32 StockID)
        {
            var builder = new StringBuilder();

            var fecha = ProduccionDate.GetDBDate();

            builder.Append("SELECT  ");
            builder.Append("    WM.Name AS  MaterialName, ");
            builder.Append("    WM.MaterialCode, ");
            builder.Append("    '' AS MaterialReference, ");
            builder.Append("    WM.Unit AS MaterialUnit, ");
            builder.Append("    SUM( W.Quantity ) AS Acumulated ");
            builder.Append("FROM         WastesMaterials WM ");
            builder.Append("LEFT JOIN    Wastes W ");
            builder.Append("ON           W.MaterialCode = WM.MaterialCode ");
            builder.Append("AND          W.StockID = ? ");
            builder.Append("AND          W.CustomFecha = ? ");
            builder.Append("GROUP BY     WM.Name, WM.MaterialCode, WM.Unit ");
            builder.Append("UNION ALL ");
            builder.Append("SELECT  ");
            builder.Append("    M.Name AS MaterialName, ");
            builder.Append("    M.Code AS MaterialCode, ");
            builder.Append("    M.Reference AS MaterialReference, ");
            builder.Append("    M.Unit AS MaterialUnit, ");
            builder.Append("    SUM( W.Quantity ) AS Acumulated ");
            builder.Append("FROM         ConfigMaterials CM ");
            builder.Append("INNER JOIN   Materials M ");
            builder.Append("ON           M.Code = CM.MaterialCode ");
            builder.Append("LEFT JOIN    Wastes W ");
            builder.Append("ON           W.MaterialCode = M.Code ");
            builder.Append("AND          W.StockID = ? ");
            builder.Append("AND          W.CustomFecha = ? ");
            builder.Append("WHERE        CM.ProductCode = ?");
            builder.Append("AND          CM.VerID = ? ");
            builder.Append("GROUP BY     M.Name, M.Code, M.Reference, M.Unit ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<MaterialReport>(builder.ToString(), StockID, fecha, StockID, fecha, ProductCode, VerID);
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

        public async Task<List<MaterialReport>> GetBomMaterial(String ProductCode, String VerID)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT  ");
            builder.Append("    M.Name AS MaterialName, ");
            builder.Append("    M.Code AS MaterialCode, ");
            builder.Append("    M.Reference AS MaterialReference, ");
            builder.Append("    M.Unit AS MaterialUnit ");
            builder.Append("FROM         ConfigMaterials CM ");
            builder.Append("INNER JOIN   Materials M ");
            builder.Append("ON           M.Code = CM.MaterialCode ");
            builder.Append("WHERE        CM.ProductCode = ?");
            builder.Append("AND          CM.VerID = ? ");
            builder.Append("GROUP BY     M.Name, M.Code, M.Reference, M.Unit ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<MaterialReport>(builder.ToString(), ProductCode, VerID);
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

        public async Task<List<MaterialReport>> GetVarillas(Int32 ProduccionDate, Byte TurnID)
        {
            var builder = new StringBuilder();

            var status = (Byte)Configs._Status.Enabled;
            var status2 = (Byte)Configs._Status.Completed;
            var fechaDesde = DateTime.ParseExact(ProduccionDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture).Date;

            builder.Append("SELECT  ");
            builder.Append("    MP.Code AS ProductCode, ");
            builder.Append("    MP.Name AS ProductName, ");
            builder.Append("    MP.Short AS ProductShort, ");
            builder.Append("    M.Name AS MaterialName, ");
            builder.Append("    M.Code AS MaterialCode, ");
            builder.Append("    M.Reference AS MaterialReference, ");
            builder.Append("    M.Unit AS MaterialUnit, ");
            builder.Append("    CM.VerID, ");
            builder.Append("    W.LoteSuplidor, ");
            builder.Append("    W.Expire, ");
            builder.Append("    W.BoxNumber, ");
            builder.Append("    W.ID, ");
            builder.Append("    SUM( W.Quantity ) AS Acumulated ");
            builder.Append("FROM         ConfigMaterials CM ");
            builder.Append("INNER JOIN   Materials M ");
            builder.Append("ON           M.Code = CM.MaterialCode ");
            builder.Append("INNER JOIN   Materials MP ");
            builder.Append("ON           MP.Code = CM.ProductCode ");
            builder.Append("INNER JOIN   MaterialsProcess MPF ");
            builder.Append("ON           MP.Code = MPF.ProductCode ");
            builder.Append("LEFT JOIN    Wastes W ");
            builder.Append("ON           W.MaterialCode = M.Code ");
            builder.Append("AND          W.CustomFecha = ? ");
            builder.Append("AND          W.TurnID = ? ");
            builder.Append("WHERE        CM.Quantity < 0 ");
            builder.Append("AND          EXISTS ");
            builder.Append("( ");
            builder.Append("    SELECT  * ");
            builder.Append("    FROM    Configs ");
            builder.Append("    WHERE   Configs.ProductCode = MPF.ProductCode ");
            builder.Append("    AND     Configs.TimeID = MPF.TimeID ");
            builder.Append("    AND     ( Status = ? OR ( Status = ? AND Begin >= ? ) ) ");
            builder.Append(") ");
            builder.Append("GROUP BY     M.Name, M.Code, M.Reference, M.Unit, CM.VerID, MP.Code, MP.Name, MP.Short, W.LoteSuplidor, W.Expire");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<MaterialReport>(builder.ToString(), ProduccionDate, TurnID, status, status2, fechaDesde.Ticks);
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

        public async Task<IEnumerable<StockList>> GetStockList()
        {
            var builder = new StringBuilder();

            var fecha = DateTime.Now.AddDays(-7);

            builder.Append("SELECT ");
            builder.Append("    P.Code AS ProductCode, ");
            builder.Append("    P.Name AS ProductName, ");
            builder.Append("    P.Short AS ProductShort, ");
            builder.Append("    S.TurnID, ");
            builder.Append("    S.Begin, ");
            builder.Append("    S.End, ");
            builder.Append("    S.Status, ");
            builder.Append("    S.IsNotified AS IsNotify ");
            builder.Append("FROM         Materials P ");
            builder.Append("INNER JOIN   Stocks S ");
            builder.Append("ON           S.ProductCode = P.Code ");
            builder.Append("WHERE        Begin > ? ");
            builder.Append("ORDER BY     S.ID DESC ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<StockList>(builder.ToString(), fecha.Ticks);
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

        public async Task<List<ElaborateTotal>> GetLotList(DateTime fecha, String ProductCode)
        {
            var builder = new StringBuilder();
            builder.Append("SELECT      DISTINCT C.Lot, C.MaterialCode, L.Reference, L.Expire ");
            builder.Append("FROM        Consumptions C ");
            //builder.Append("INNER JOIN  Materials M ");
            //builder.Append("ON          M.Code = C.MaterialCode ");
            builder.Append("LEFT JOIN   Lots L ");
            builder.Append("ON          C.MaterialCode = L.MaterialCode ");
            builder.Append("AND         C.Lot = L.Code ");
            builder.Append("WHERE       C.CustomFecha <= ? ");
            builder.Append("AND         C.ProductCode = ? ");
            builder.Append("ORDER BY    C.ID DESC ");
            builder.Append("LIMIT       5;");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<ElaborateTotal>(builder.ToString(), fecha.GetDBDate(), ProductCode);
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

        public Lots GetLoteForMaterial(String MaterialCode, String Code)
        {
            return AsyncHelper.RunSync<Lots>(() => GetLoteForMaterialAsync(MaterialCode, Code));
        }

        public async Task<Lots> GetLoteForMaterialAsync(String MaterialCode, String Code)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var lote = await GetConnectionAsync().Table<Lots>().Where(p => p.MaterialCode == MaterialCode && p.Code == Code).FirstOrDefaultAsync();
                return lote;
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

        public async Task<List<MaterialReport>> GetLastVarillas(Int32 ProduccionDate, Byte TurnID, String MaterialCode)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT  ");
            builder.Append("    M.Name AS MaterialName, ");
            builder.Append("    M.Code AS MaterialCode, ");
            builder.Append("    M.Reference AS MaterialReference, ");
            builder.Append("    M.Unit AS MaterialUnit, ");
            builder.Append("    W.Quantity AS EntryQuantity, ");
            builder.Append("    W.Fecha AS Produccion, ");
            builder.Append("    W.Lot, ");
            builder.Append("    W.LoteSuplidor, ");
            builder.Append("    W.Expire, ");
            builder.Append("    W.BoxNumber ");
            builder.Append("FROM         Wastes W ");
            builder.Append("INNER JOIN   Materials M ");
            builder.Append("ON           M.Code = W.MaterialCode ");
            builder.Append("WHERE        W.CustomFecha = ? ");
            builder.Append("AND          W.TurnID = ? ");
            builder.Append("AND          W.MaterialCode = ? ");
            builder.Append("ORDER BY     W.Fecha DESC ");
            builder.Append("LIMIT 5 ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<MaterialReport>(builder.ToString(), ProduccionDate, TurnID, MaterialCode);
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

        public async Task PostSyncLog(IEnumerable<SyncLogRequest> Logs)
        {
            var url = GetService(ServicesType.POST_SYNCLOG, false);
            await PostJsonAsync(url, Logs);
        }

        public String GetLastLotConsumed(String MaterialCode)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var con = GetConnection();

                var consumo = con.Table<Consumptions>().Where(p => p.MaterialCode == MaterialCode).OrderByDescending(p => p.Fecha).FirstOrDefault();

                return consumo != null ? consumo.Lot : String.Empty;
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

        public async Task<Tracking> GetTrackingWithCount(DateTime ProduccionDate, Int32 ConsumptionID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var tracking = await GetTracking(ProduccionDate, ConsumptionID);
                if (tracking == null) tracking = new Tables.Tracking() { WasNull = true };
                tracking.Count = await GetConnectionAsync().Table<Tracking>().CountAsync();
                return tracking;
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

        public async Task<Tracking> GetTracking(DateTime ProduccionDate, Int32 ConsumptionID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var retorno = await GetConnectionAsync().Table<Tracking>().Where(p => p.ConsumptionID == ConsumptionID).ToListAsync();

                return retorno.FirstOrDefault(p => p.FechaElaborate.Date == ProduccionDate.Date);
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

        public Int16 GetNextSequences(DateTime ProduccionDate)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var fecha = ProduccionDate.GetDBDate();
                var first = GetConnection().Table<Consumptions>().Where(p => p.CustomFecha == fecha).OrderByDescending(p => p.CustomID).FirstOrDefault();
                short mas = 1;
                return first == null ? mas : (short)(first.CustomID + mas);
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

        public async Task<Int16> GetNextSequenceAsync(DateTime ProduccionDate, ProcessList Proceso)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var fecha = ProduccionDate.GetDBDate();
                var first = await GetConnectionAsync().Table<Consumptions>().Where(p => p.CustomFecha == fecha).OrderByDescending(p => p.CustomID).FirstOrDefaultAsync();

                Int16 init = 1;

                var result = first == null ? init : (Int16)(first.CustomID + 1);

                if (Proceso.IsSubEquipment && result < 1000) result += 1000;

                return result;
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

        public Int16 GetNextOut(DateTime ProduccionDate)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var fecha = ProduccionDate.GetDBDate();
                var first = GetConnection().Table<Elaborates>().Where(p => p.CustomFecha == fecha).OrderByDescending(p => p.CustomID).FirstOrDefault();
                short mas = 1;
                return first == null ? mas : (short)(first.CustomID + mas);
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

        public async Task<Int16> GetNextOutAsync(DateTime ProduccionDate)
        {
            var Intentado = false;
            var contador = 0;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);
            contador++;

            if (contador == 5) throw new Exception("Elaborate Sequence value cannot be zero.");

            try
            {
                var fecha = ProduccionDate.GetDBDate();
                var first = await GetConnectionAsync().Table<Elaborates>().Where(p => p.CustomFecha == fecha).OrderByDescending(p => p.CustomID).FirstOrDefaultAsync();
                short mas = 1;
                short nextSequence = 0;
                nextSequence = first == null ? mas : (short)(first.CustomID + mas);
                if (nextSequence == 0)
                {
                    Intentado = true;
                    goto VolverALeer;
                }
                return nextSequence;
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

        public async Task<Single> GetStockAvailableByTransactionsAsync(String MaterialCode, String Lot, Int32 BoxNumber)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var stock = await GetConnectionAsync().Table<Transactions>()
                    .Where(p => p.MaterialCode == MaterialCode && p.Lot == Lot && p.BoxNumber == BoxNumber)
                    .OrderByDescending(s=>s.ID).FirstOrDefaultAsync();

                if (stock == null) return 0;

                return stock.Total;
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

        public async Task<Single> GetStockAvailableAsync(String MaterialCode, String Lot, Int32 BoxNumber)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var stock = await GetConnectionAsync().Table<Inventories>().Where(p => p.MaterialCode == MaterialCode && p.Lot == Lot && p.BoxNumber == BoxNumber).FirstOrDefaultAsync();

                if (stock == null) return 0;

                return stock.Quantity;
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


        //PARA CONTROLAR CANTIDAD DE MATERIAL CONSUMIDO DE UNA CAJA O CODIGO DE BARRAS
        //RALDY DE JESUS - 29-12-2022
        public async Task<Int32> GetQuantityMaterialConsumedAsync(String MaterialCode, String Lote, Int16 EmpaqueNo)
        {

            var Intentado = false;

        VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {

                var builder = new StringBuilder();
                builder.Append("SELECT ");
                builder.Append("    SUM(Quantity) AS Total ");
                builder.Append("FROM    CONSUMPTIONS ");
                builder.Append("WHERE  MaterialCode=?  AND Lot=? ");
                builder.Append("AND BoxNumber=?");

                var cantidadConsumida = await GetConnectionAsync().ExecuteScalarAsync<Int32>(builder.ToString(), MaterialCode, Lote, EmpaqueNo);
                return cantidadConsumida;
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


        public Elaborates GetLastSalida()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                return GetConnection().Table<Elaborates>().Where(p => p.Quantity > 0).OrderByDescending(p => p.ID).FirstOrDefault();
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

        //AGREGADO POR RALDY 14-02-2023
        public async Task<Consumptions> GetLastConsumoAsync()
        {
            var Intentado = false;

        VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                return await GetConnectionAsync().Table<Consumptions>().Where(p => p.Quantity > 0).OrderByDescending(p => p.ID).FirstOrDefaultAsync();
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


        //AGREGADO POR RALDY 21-02-2023
        public async Task<Consumptions> GetLastConsumoBandejaAsync()
        {
            var Intentado = false;

        VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                return await GetConnectionAsync().Table<Consumptions>().Where(p => p.Quantity != 0).OrderByDescending(p => p.Fecha).FirstOrDefaultAsync();
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


        public async Task<Elaborates> GetLastSalidaAsync()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                return await GetConnectionAsync().Table<Elaborates>().Where(p => p.Quantity > 0).OrderByDescending(p => p.ID).FirstOrDefaultAsync();
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

        public async Task<ProductsRoutes> LoadPendingTraza(TraysList Bandeja)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var bandejas = await GetConnectionAsync().Table<ProductsRoutes>().Where(p => p.TrayID == Bandeja._TrayID && p.Status == ProductsRoutes.RoutesStatus.EnTransito).ToListAsync();
                var bandeja = bandejas.FirstOrDefault(p => p.CustomFecha == Bandeja._Fecha && p.ElaborateID == Bandeja.ElaborateID && p.EquipmentID == Bandeja.EquipmentID);
                return bandeja;
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

        public async Task<ProductsRoutes> LoadRoute(String EquipmentID, Int16 SecSalida, DateTime Fecha)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<ProductsRoutes>().Where(p => p.EquipmentID == EquipmentID && p.ElaborateID == SecSalida && p.Produccion == Fecha).FirstOrDefaultAsync();
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

        public async Task<ProductsRoutes> LoadActive()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<ProductsRoutes>().Where(p => p.IsActive).FirstOrDefaultAsync();
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

        public async Task<ProductsRoutes> LoadRoutebySync(String EquipmentID, Int16 SecSalida, String TrayID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<ProductsRoutes>().Where(p => p.EquipmentID == EquipmentID && p.ElaborateID == SecSalida && p.TrayID == TrayID).OrderByDescending(p => p.Produccion).FirstOrDefaultAsync();
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

        public async Task<ProductsRoutes> LoadLastRoute(String EquipmentID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<ProductsRoutes>().Where(p => p.EquipmentID == EquipmentID).OrderByDescending(p => p.Produccion).FirstOrDefaultAsync();
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

        public async Task<ProductsRoutes> LoadRouteByRealKey(String TimeID, String Year, Int32 CustomID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<ProductsRoutes>().Where(p => p.TimeID == TimeID && p.Year == Year && p.CustomID == CustomID).FirstOrDefaultAsync();
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

        public Int16 GetNextSequence(DateTime ProduccionDate, Byte TurnID, String EquipmentID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var fecha = ProduccionDate.GetDBDate();
                var first = GetConnection().Table<ProductsRoutes>().Where(p => p.CustomFecha == fecha && p.TurnID == TurnID && p.EquipmentID == EquipmentID).OrderByDescending(p => p.SecuenciaEmpaque).FirstOrDefault();
                short mas = 1;
                return first == null ? mas : (short)(first.SecuenciaEmpaque + mas);
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

        public async Task<bool> DoesElaborateExists(Elaborates elaborate)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var fecha = elaborate.Produccion.GetDBDate();
                var exists = await GetConnectionAsync().Table<Elaborates>()
                    .Where(p => p.CustomFecha == fecha
                    && p.TurnID == elaborate.TurnID && p.EquipmentID == elaborate.EquipmentID
                    && p.CustomID == elaborate.CustomID).FirstOrDefaultAsync();

                return exists != null ? true: false;
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

        public async Task<Int16> GetNextSequenceAsync(DateTime ProduccionDate, Byte TurnID, String EquipmentID)
        {
            var Intentado = false;
            var contador = 0;
            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);
            contador++;

            if (contador == 5)
            {
                throw new Exception("Pack Sequence value cannot be zero");
            }
            try
            {
                var fecha = ProduccionDate.GetDBDate();
                var lastElaborate = await GetConnectionAsync().Table<Elaborates>().Where(p => p.CustomFecha == fecha && p.TurnID == TurnID && p.EquipmentID == EquipmentID).OrderByDescending(p => p.PackSequence).FirstOrDefaultAsync();
                short mas = 1;
                short nextPackSequence = 0;

                if (lastElaborate == null)//Si no hay registro de salida de ese turno
                {
                    nextPackSequence = mas;
                }
                else
                {
                    if (lastElaborate.PackSequence == 0)//Si se hizo una carga inicial desde SAP, los registros de salida no tienen guardado la secuencia de etiqueta. 
                    {
                        //Se obtiene la secuencia de etiqueta desde la tabla de TRAZA.
                        var first = await GetConnectionAsync().Table<ProductsRoutes>().Where(p => p.CustomFecha == fecha && p.TurnID == TurnID && p.EquipmentID == EquipmentID).OrderByDescending(p => p.SecuenciaEmpaque).FirstOrDefaultAsync();
                        nextPackSequence =  first == null ? mas : (short)(first.SecuenciaEmpaque + mas);
                    }
                    else
                    {
                        //Se obtiene la secuencia de etiqueta desde el último registro de la tabla de Salidas.
                        nextPackSequence = (short) (lastElaborate.PackSequence + 1);
                    }
                }
                if (nextPackSequence == 0)
                {
                    Intentado = true;
                    goto VolverALeer;
                }
                return nextPackSequence;
                //return first == null ? mas : (short)(first.PackSequence + mas);
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

        public async Task<List<ElaborateList>> GetElaborates()
        {
            var builder = new StringBuilder();

            builder.Append("SELECT  ");
            builder.Append("    P.Code AS ProductCode, ");
            builder.Append("    P.Name AS ProductName, ");
            builder.Append("    P.Reference AS ProductReference, ");
            builder.Append("    P.Short AS ProductShort, ");
            builder.Append("    E.TurnID, ");
            builder.Append("    E.Logon, ");
            builder.Append("    E.CustomID AS ElaborateID, ");
            builder.Append("    E.Produccion, ");
            builder.Append("    E.Fecha, ");
            builder.Append("    E.Quantity, ");
            builder.Append("    E.Unit, ");
            builder.Append("    E.TrayID, ");
            builder.Append("    E.ID, ");
            builder.Append("    E.BatchID ");
            builder.Append("FROM         Elaborates E ");
            builder.Append("INNER JOIN   Materials P ");
            builder.Append("ON           E.ProductCode = P.Code ");
            builder.Append("WHERE        E.IsReturn = 0 ");
            builder.Append("ORDER BY     E.Fecha DESC ");
            builder.Append("LIMIT 100 ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<ElaborateList>(builder.ToString());
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

        public async Task SetListAsReturn(List<Int32> lstIds)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var query = String.Format("UPDATE Elaborates SET IsReturn = 1, Sync = 1, SyncSQL=1 WHERE ID IN ({0});", lstIds.GetInt32Enumerable());

                await GetConnectionAsync().ExecuteAsync(query);
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

        public async Task<IEnumerable<Materials>> GetMaterials(IEnumerable<String> Codigos)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var query = String.Format("SELECT * FROM Materials WHERE Code IN ({0})", Codigos.GetStringEnumerable());
                return await GetConnectionAsync().QueryAsync<Materials>(query);
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

        public async Task<IEnumerable<DateTurnList>> GetDateandTurns()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var fecha = DateTime.Now.AddDays(-7).GetDBDate();

                var result = await GetConnectionAsync().Table<Stocks>().Where(p => p.Status == Stocks._Status.Cerrado && p.CustomFecha >= fecha).OrderByDescending(p => p.Begin).ToListAsync();

                return result.Select(p => new DateTurnList
                {
                    Produccion = p.Begin.ToLocalTime().Date,
                    TurnID = p.TurnID,
                    IsNotified = p.IsNotified
                }).ToList();
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

        public async Task<StocksDetails> GetStockDetail(Int32 StockId, String MaterialCode)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<StocksDetails>().Where(p => p.StockID == StockId && p.MaterialCode == MaterialCode).FirstOrDefaultAsync();
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

        public async Task<Configs> GetConfigByProduction(DateTime ProduccionDate)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Configs>().Where(p => p.Begin < ProduccionDate).OrderByDescending(p => p.Begin).FirstOrDefaultAsync();
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

        public async Task<List<Inventories>> GetAvailableStock()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<Inventories>().Where(p => p.Quantity > 0).ToListAsync();
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

        public async Task ReleaseTraza(IEnumerable<TraysList> Bandejas)
        {
            var repoRoutes = new RepositoryProductsRoutes(this.Connection);
            var buffer = new List<ProductsRoutes>();
            var status = (Byte)ProductsRoutes.RoutesStatus.EnTransito;

            var intentado = false;

            VolveraLeer:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                var query = String.Format("SELECT * FROM ProductsRoutes WHERE TrayID IN ({0}) AND Status = {1}", Bandejas.Select(p => p.BarCode).GetStringEnumerable(), status);
                var routes = await GetConnectionAsync().QueryAsync<ProductsRoutes>(query);

                foreach (var route in routes)
                {
                    route.Sync = true;
                    route.Status = ProductsRoutes.RoutesStatus.Cancelada;
                    route.Begin = DateTime.Now;

                    buffer.Add(route);
                }
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            intentado = true;
                            goto VolveraLeer;
                        }
                        else
                            throw;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        intentado = true;
                        goto VolveraLeer;

                    default:
                        throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            await repoRoutes.UpdateAllAsync(buffer);
        }

        public async Task<List<Consumptions>> SyncConsumptions(DateTime fechaProduccion, int turno)
        {
            //Descarga de consumos de equipo principal desde la base de datos SQL Server.
            var leido = false;
            var contador = 0;

            VolveraLeer:

            if (leido) await Task.Delay(Task_Delay);

            contador++;

            if (contador == 5) return new List<Tables.Consumptions>();

            try
            {
                var repoConsumptions = new RepositoryConsumptions(this.Connection);
                var repoSyncro = new RepositorySyncro(this.Connection);

                var LastUpdate = await repoSyncro.GetAsyncByKey(Syncro.Tables.Consumptions);
                var parametros = String.Format("?idEquipo={0}&fechaFinal={1}&turno={2}", Proceso.SubEquipmentID
                    , fechaProduccion.ToString("dd'/'MM'/'yyyy"), turno);
                var url = GetSqlServicePath(SqlServiceType.GetConsumos, parametros);
                var Synclog = new SyncLogMonitor.Detail() { Tabla = Syncro.Tables.Consumptions, Fecha = LastUpdate.LastSync };

                var json = await GetJsonAsync(url);

                if (!json.isOk) throw json.ex; /// Lanzo la excepción para ver el problema

                if (json.Json.IsJsonEmpty()) return new List<Tables.Consumptions>(); /// Si no llego nada devuelvo que todo esta vacio

                var Consumos = JsonConvert.DeserializeObject<ConsumptionsRequest[]>(json.Json);

                Synclog.RegistrosBajada = Consumos.Count();
                Synclog.SizeBajada = json.SizePackageDownloading;

                var buffer = Consumos.Select(s => s.Get()).ToList();
                
                //var buffer = Consumos.Select(p => new Consumptions
                //{
                //    ProcessID = p.IDPROCESS,
                //    Center = p.WERKS,
                //    CustomFecha = Convert.ToInt32(GetDatetime(p.FECHA, p.HORA).Value.GetSapDate()),
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
                //}).ToList();

                var consumosLocales = await repoConsumptions.GetAsyncAll();

                var bufferInsert = new List<Consumptions>();

                bufferInsert = (from consumoNuevo in buffer
                                where consumosLocales.Any(s => s.ProcessID == consumoNuevo.ProcessID
                                && s.Center == consumoNuevo.Center
                                && s.EquipmentID == consumoNuevo.EquipmentID
                                && s.TimeID == consumoNuevo.TimeID
                                && s.VerID == consumoNuevo.VerID
                                && s.CustomID == consumoNuevo.CustomID
                                && s.CustomFecha == consumoNuevo.CustomFecha 
                                && s.TurnID == consumoNuevo.TurnID
                                && s.Logon == consumoNuevo.Logon) == false
                                select new Consumptions
                                {
                                    ProcessID = consumoNuevo.ProcessID,
                                    Center = consumoNuevo.Center,
                                    CustomFecha = consumoNuevo.CustomFecha,
                                    Produccion = consumoNuevo.Produccion,
                                    Fecha = consumoNuevo.Fecha,
                                    CustomID = consumoNuevo.CustomID,
                                    EquipmentID = consumoNuevo.EquipmentID,
                                    TimeID = consumoNuevo.TimeID,
                                    ProductCode = consumoNuevo.ProductCode,
                                    VerID = consumoNuevo.VerID,
                                    MaterialCode = consumoNuevo.MaterialCode,
                                    Logon = consumoNuevo.Logon,
                                    Sync = consumoNuevo.Sync,
                                    TurnID = consumoNuevo.TurnID,
                                    Lot = consumoNuevo.Lot,
                                    BoxNumber = consumoNuevo.BoxNumber,
                                    Quantity = consumoNuevo.Quantity,
                                    SubEquipmentID = consumoNuevo.SubEquipmentID,
                                    Unit = consumoNuevo.Unit,
                                    TrayID = consumoNuevo.TrayID,
                                    ElaborateID = consumoNuevo.ElaborateID,
                                    TrayEquipmentID = consumoNuevo.TrayEquipmentID,
                                    TrayDate = consumoNuevo.TrayDate,
                                    BatchID = consumoNuevo.BatchID
                                }).ToList();
                await repoConsumptions.InsertAsyncAll(bufferInsert);

                var MaxValue = Consumos.Max(p => GetDatetime(p.CPUDT2, p.CPUTM2));

                if (MaxValue.HasValue)
                {
                    await repoSyncro.InsertOrReplaceAsync(new Syncro()
                    {
                        Tabla = Syncro.Tables.Consumptions,
                        IsDaily = false,
                        Sync = false,
                        LastSync = MaxValue.Value
                    });
                }

                var BufferToReturn = new List<Consumptions>();

                //Materiales
                var Prebuffer = buffer.Where(s=>String.IsNullOrEmpty(s.TrayID)).GroupBy(p => p.MaterialCode)
                                      .Select(p => new { MaterialCode = p.Key, Produccion = p.Max(m => m.Produccion) }).ToList();

                foreach (var item in Prebuffer)
                {
                    BufferToReturn.Add(buffer.Single(p => p.MaterialCode == item.MaterialCode && p.Produccion == item.Produccion));
                }

                //Agregar la última bandeja consumida
                var lastConsumedTray = buffer.Where(s => !String.IsNullOrEmpty(s.TrayID)).OrderByDescending(s => s.CustomID).FirstOrDefault();

                if (lastConsumedTray != null)
                {
                    BufferToReturn.Add(lastConsumedTray);
                }

                SyncMonitor.Detalle.Add(Synclog);

                return BufferToReturn;
            }
            catch (JsonException)
            {
                leido = true;
                goto VolveraLeer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public String GetLastBatchID()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var builder = new StringBuilder();

                var status = (Byte)Configs._Status.Actived;

                builder.Append("SELECT  E.* ");
                builder.Append("FROM        Elaborates E ");
                builder.Append("INNER JOIN  Configs C ");
                builder.Append("ON          E.ProductCode = C.ProductCode ");
                builder.Append("AND         E.Fecha > C.ExecuteDate ");
                builder.Append("WHERE       C.Status = ? ");
                builder.Append("AND         ? IN (C.EquipmentID, C.SubEquipmentID)");
                builder.Append("ORDER BY    E.Fecha DESC ");
                builder.Append("LIMIT 1;");

                var salida = GetConnection().Query<Elaborates>(builder.ToString(), status, Proceso.EquipmentID).FirstOrDefault();
                return salida != null ? salida.BatchID : String.Empty;
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
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

        public async Task<Consumptions> GetLastBandejaEntrada(Boolean UseCaching = true)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                if (UltimaBandeja == null || !UseCaching)
                {
                    var bandejalst = await GetConnectionAsync().QueryAsync<Consumptions>("SELECT * FROM Consumptions WHERE IFNULL(TrayID,'') != '' AND QUANTITY >=0 ORDER BY ID DESC LIMIT 1");
                    UltimaBandeja = bandejalst.FirstOrDefault();
                }

                return UltimaBandeja;
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

        public async Task CleanTraza()
        {
            var builder = new StringBuilder();
            var repoTraza = new RepositoryProductsRoutes(this.Connection);

            var Completado = (Byte)ProductsRoutes.RoutesStatus.Terminada;
            var Pendiente = (Byte)ProductsRoutes.RoutesStatus.EnEquipo;

            builder.Append("UPDATE      ProductsRoutes ");
            builder.Append("SET         Status = ?, ");
            builder.Append("            Sync = 1 ");
            builder.Append("WHERE       EXISTS ");
            builder.Append("( ");
            builder.Append("    SELECT      * ");
            builder.Append("    FROM        Consumptions ");
            builder.Append("    WHERE       ProductsRoutes.BatchID = Consumptions.BatchID ");
            builder.Append("    AND         ProductsRoutes.ElaborateID = Consumptions.ElaborateID ");
            builder.Append("    AND         ProductsRoutes.TrayID = Consumptions.TrayID ");
            builder.Append("    AND         ProductsRoutes.Status = ? ");
            builder.Append("    AND         ProductsRoutes.IsActive = 0 ");
            builder.Append(") ");

            try
            {
                var rows = await GetConnectionAsync().ExecuteAsync(builder.ToString(), Completado, Pendiente);

                if (rows > 0) await repoTraza.CreateSyncro(true);
            }
            catch (SQLiteException ex)
            {
                switch (ex.Result)
                {
                    case SQLite.Net.Interop.Result.Error:
                        if (ex.Message.Equals(conMessage))
                        {
                            AddCustomValuetoBuffer(new CustomQuery()
                            {
                                Query = builder.ToString(),
                                args = new object[] { Completado, Pendiente }
                            });
                        }
                        else
                            throw;

                        break;

                    case SQLite.Net.Interop.Result.Busy:
                    case SQLite.Net.Interop.Result.Locked:
                        AddCustomValuetoBuffer(new CustomQuery()
                        {
                            Query = builder.ToString(),
                            args = new object[] { Completado, Pendiente }
                        });
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

        public async Task<ExecutePendingJobsResult> ExecutePendingJobs()
        {
            var result = new ExecutePendingJobsResult()
            {
                ZCount = await ExecutePendingJobs(GetConnectionAsync()),
                TraysProductsCount = await RepositoryTraysProducts.ExecutePendingJobs(GetConnectionAsync()),
                ProductsRoutesCount = await RepositoryProductsRoutes.ExecutePendingJobs(GetConnectionAsync()),
                SyncroCount = await RepositorySyncro.ExecutePendingJobs(GetConnectionAsync()),
                MaterialsCount = await RepositoryMaterials.ExecutePendingJobs(GetConnectionAsync()),
                UnitsCount = await RepositoryUnits.ExecutePendingJobs(GetConnectionAsync()),
                RelaseCount = await RepositoryTraysRelease.ExecutePendingJobs(GetConnectionAsync()),
                RelasePositionCount = await RepositoryTraysReleasePosition.ExecutePendingJobs(GetConnectionAsync()),
                ConfigCount = await RepositoryConfigs.ExecutePendingJobs(GetConnectionAsync()),
                ConsumptionsCount = await RepositoryConsumptions.ExecutePendingJobs(GetConnectionAsync()),
                ElaboratesCount = await RepositoryElaborates.ExecutePendingJobs(GetConnectionAsync())
            };

            return result;
        }

        public async Task<String> CleanAllValues()
        {
            var con = GetConnectionAsync();
            var fecha = DateTime.Now.AddDays(-32).Date;

            var query = "DELETE FROM ProductsRoutes WHERE Produccion < ?";
            await con.ExecuteAsync(query, fecha);

            query = "DELETE FROM Consumptions WHERE Produccion < ?";
            await con.ExecuteAsync(query, fecha);

            query = "DELETE FROM Elaborates WHERE Produccion < ?";
            await con.ExecuteAsync(query, fecha);

            query = "DELETE FROM Tracking WHERE FechaElaborate < ?";
            await con.ExecuteAsync(query, fecha);

            query = "DELETE FROM Configs WHERE Begin < ?";
            await con.ExecuteAsync(query, fecha);

            query = "DELETE FROM StocksDetails WHERE StockID IN (SELECT ID FROM Stocks WHERE Begin < ?)";
            await con.ExecuteAsync(query, fecha);

            query = "DELETE FROM Stocks WHERE Begin < ?";
            await con.ExecuteAsync(query, fecha);

            query = "DELETE FROM TraysReleasePosition WHERE TraysReleaseID IN (SELECT ID FROM TraysRelease WHERE Fecha < ?)";
            await con.ExecuteAsync(query, fecha);

            query = "DELETE FROM TraysRelease WHERE Fecha < ?";
            await con.ExecuteAsync(query, fecha);

            query = "DELETE FROM Wastes WHERE Fecha < ?";
            await con.ExecuteAsync(query, fecha);

            await con.ExecuteAsync("vacuum;"); // Dar Mantenimento a la BD.

            return String.Empty;
        }

        public async Task<TraysTimes> GetTimeByTray(String TrayID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().Table<TraysTimes>().Where(p => p.TrayID == TrayID).FirstOrDefaultAsync();
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

        public List<Turns> GetAllTurns()
        {
            return AsyncHelper.RunSync<List<Turns>>(() => GetAllTurnsAsync());
        }

        public async Task<List<Turns>> GetAllTurnsAsync()
        {
            var repoT = new RepositoryTurns(this.Connection);

            var turnos = await repoT.GetAsyncAll();

            return turnos.ToList();
        }

        public async Task<List<ZBomLackMaterial>> Get_Lack_Material(String ProductCode, String VerID, Times Time)
        {
            var builder = new StringBuilder();
            builder.Append("SELECT ");
            builder.Append("    M.[Code], ");
            builder.Append("    M.[Reference], ");
            builder.Append("    M.[Name], ");
            builder.Append("    M.[Short], ");
            builder.Append("    M.[Group], ");
            builder.Append("    CONSUMO.[Produccion], ");
            builder.Append("    CONSUMO.[Lot], ");
            builder.Append("    CONSUMO.[TurnID], ");
            builder.Append("    CONSUMO.[BoxNumber], ");
            builder.Append("    CONSUMO.[SupLot], ");
            builder.Append("    CONSUMO.[BatchID], ");
            builder.Append("    0 AS [IsObligatory] ");
            builder.Append("FROM        Materials M ");
            builder.Append("INNER JOIN  ConfigMaterials CM ");
            builder.Append("ON          M.Code = CM.MaterialCode ");
            builder.Append("LEFT JOIN ");
            builder.Append("( ");
            builder.Append("    SELECT ");
            builder.Append("         C.MaterialCode, ");
            builder.Append("         TurnID, ");
            builder.Append("         Lot, ");
            builder.Append("         BoxNumber, ");
            builder.Append("         Produccion, ");
            builder.Append("         [Reference] AS SupLot, ");
            builder.Append("         BatchID ");
            builder.Append("    FROM          CustomSecuences CS ");
            builder.Append("    INNER JOIN    Consumptions C ");
            builder.Append("    ON            CS.MaterialCode = C.MaterialCode ");
            builder.Append("    AND           CS.CustomFecha = C.CustomFecha ");
            builder.Append("    AND           CS.ConsumptionID = C.CustomID ");
            builder.Append("    LEFT JOIN     Lots L ");
            builder.Append("    ON            L.MaterialCode = C.MaterialCode ");
            builder.Append("    AND           L.Code =  C.Lot ");
            builder.Append(")  CONSUMO ");
            builder.Append("ON          CONSUMO.MaterialCode = CM.MaterialCode ");
            builder.Append("WHERE       CM.ProductCode = ? ");
            builder.Append("AND         CM.VerID = ? ");
            builder.Append("AND         CM.Quantity > 0 ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var matlist = await GetConnectionAsync().QueryAsync<ZBomLackMaterial>(builder.ToString(), ProductCode, VerID);

                /// Se tuvo que hacer asi el union all se estaba volviendo loco

                if (Time.Producto == Times.ProductTypes.Validar_Salida || Time.Producto == Times.ProductTypes.Validar_Salida_y_Tipo_Almacenamiento)
                {
                    builder.Clear();
                    builder.Append("SELECT ");
                    builder.Append("    M.[Code], ");
                    builder.Append("    M.[Reference], ");
                    builder.Append("    M.[Name], ");
                    builder.Append("    M.[Short], ");
                    builder.Append("    M.[Group], ");
                    builder.Append("    C.[Produccion], ");
                    builder.Append("    C.[Lot], ");
                    builder.Append("    C.[TurnID], ");
                    builder.Append("    C.[BoxNumber], ");
                    builder.Append("    '' AS [SupLot], ");
                    builder.Append("    C.[BatchID], ");
                    builder.Append("    1 AS [IsObligatory] ");
                    builder.Append("FROM        Materials M ");
                    builder.Append("LEFT JOIN   CustomSecuences CS ");
                    builder.Append("ON          M.Code = CS.MaterialCode ");
                    builder.Append("LEFT JOIN   Consumptions C ");
                    builder.Append("ON          CS.MaterialCode = C.MaterialCode ");
                    builder.Append("AND         CS.CustomFecha = C.CustomFecha ");
                    builder.Append("AND         CS.ConsumptionID = C.CustomID ");
                    builder.Append("WHERE       M.Code = ? ");

                    var list = await GetConnectionAsync().QueryAsync<ZBomLackMaterial>(builder.ToString(), ProductCode);
                    matlist.AddRange(list);
                }

                return matlist.OrderByDescending(d => d.Produccion).ToList();
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

        public async Task<double> GetTotalConsumedProducts(string EquipmentId, string ProductCode
            , DateTime ProductionDate, int TurnID)
        {
            var TotalConsumedProducts = 0.0;
            try
            {
                var url = GetSqlServicePath(SqlServiceType.GetSumatoriaConsumoProductos);
                JResult json = null;
                //idEquipo, string idProducto, string fechaProduccion, int turno, string Unidad
                var parameters = String.Format("?idEquipo={0}&idProducto={1}&fechaProduccion={2}" +
                    "&turno={3}"
                    , EquipmentId, ProductCode, ProductionDate.ToString("dd'/'MM'/'yyyy"), TurnID);

                json = await GetJsonAsync(url + parameters);
                if (json.isOk)
                {
                    TotalConsumedProducts = json.Json.ToNumeric();
                }
                else
                {
                    throw json.ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TotalConsumedProducts;

        }

        public async Task<double> GetOnlineCountElaboratesConsumptions(string EquipmentId, DateTime ProductionDate, int TurnID)
        {
            var counterElaboratesConsumptions = 0.0;
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counterElaboratesConsumptions;

        }   

        
         

        public async Task<double> GetOnlineCountTraysConsumptions(string EquipmentId, DateTime ProductionDate, int TurnID)
        {
            var counterTraysConsumptions = 0.0;
            try
            {
                var url = GetSqlServicePath(SqlServiceType.GetCantidadBandejasConsumidas);
                JResult json = null;
                //idEquipo, string idProducto, string fechaProduccion, int turno, string Unidad
                var parameters = String.Format("?idEquipo={0}&fechaProduccion={1}" +
                    "&turno={2}"
                    , EquipmentId, ProductionDate.ToString("dd'/'MM'/'yyyy"), TurnID);

                json = await GetJsonAsync(url + parameters);
                if (json.isOk)
                {
                    counterTraysConsumptions = json.Json.ToNumeric();
                }
                else
                {
                    throw json.ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return counterTraysConsumptions;

        }

        public async Task<Int32> GetCantidadReimpresionesEtiquetaAsync(DateTime fechaProduccion, int turno, string empaque, int secuenciaEtiqueta)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();
                builder.Append("SELECT ");
                builder.Append("    SUM(Quantity) AS Total ");
                builder.Append("FROM    LABELPRINTINGLOGS ");
                builder.Append("WHERE  CUSTOMFECHA=?  AND TurnID = ? ");
                builder.Append("AND PACKID=? AND PackSequence=?");

                var cantidadReimpresionesEtiqueta = await GetConnectionAsync().ExecuteScalarAsync<Int32>(builder.ToString(),
                    fechaProduccion.GetDBDate(), turno, empaque, secuenciaEtiqueta);

                return cantidadReimpresionesEtiqueta;
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


        //CODIGO AGREGADO POR RALDY PARA MOSTRAR EN PANTALLA DE SALIDA, LA CANTIDAD DE CIGARROS CONSUMIDOS
        //Ultima actualizacion 23-febrero-2023
        public async Task<double> GetCantidadCigarrosConsumidosAsync(string idEquipo, DateTime fechaProduccion, int turno, string idProducto = "")
         {
            
            
            var Intentado = false;
            var cantidadCigarrosConsumidos = 0.0;
            var totalCantidadCigarrosConsumidos = 0.0;
            var inventarioInicialTurno = 0.0;
            Int64 turnoAnterior = 0;
        VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            { 

                var builder = new StringBuilder();
                builder.Append("SELECT  ");
                builder.Append("    SUM(Quantity) AS Total ");
                builder.Append("FROM    CONSUMPTIONS ");
                builder.Append("WHERE   IFNULL(TrayID,'') != ''  AND QUANTITY != 0 AND EquipmentID = ? ");
                builder.Append("AND CUSTOMFECHA=? AND TURNID=?");
               

                if (!String.IsNullOrEmpty(idProducto))
                {
                    builder.Append(String.Format(" AND PRODUCTCODE='{0}'", idProducto));
                }

                string query = "SELECT SUM(Quantity) AS Total " +
                            "FROM CONSUMPTIONS " +
                            "WHERE   IFNULL(TrayID,'') != ''  AND QUANTITY != 0 AND EquipmentID=? " +
                            "AND CUSTOMFECHA=? " +
                            "AND TURNID=? " +
                            "GROUP BY ID " +
                            "ORDER BY CUSTOMFECHA DESC " +
                            "LIMIT 1 OFFSET 0";

                if (turno != 1)
                {
                    turnoAnterior = turno - 1;
                    //inventarioInicialTurno = await GetConnectionAsync().ExecuteScalarAsync<double>(builder.ToString(), idEquipo, fechaProduccion.GetDBDate(), turnoAnterior);                   
                    inventarioInicialTurno = await GetConnectionAsync().ExecuteScalarAsync<double>(query, idEquipo, fechaProduccion.GetDBDate(), turnoAnterior);                    
                    cantidadCigarrosConsumidos = await GetConnectionAsync().ExecuteScalarAsync<double>(builder.ToString(), idEquipo, fechaProduccion.GetDBDate(), turno);
                    totalCantidadCigarrosConsumidos = inventarioInicialTurno + cantidadCigarrosConsumidos;
                }
                else
                {
                    turnoAnterior = turno + 2;
                    //inventarioInicialTurno = await GetConnectionAsync().ExecuteScalarAsync<double>(builder.ToString(), idEquipo, fechaProduccion.GetDBDate(), turnoAnterior);
                   /* string query = "SELECT SUM(Quantity) AS Total " +
                            "FROM CONSUMPTIONS " +
                            "WHERE   IFNULL(TrayID,'') != ''  AND QUANTITY != 0 AND EquipmentID=? " +
                            "AND CUSTOMFECHA=? " +
                            "AND TURNID=? " +
                            "GROUP BY ID " +
                            "ORDER BY CUSTOMFECHA DESC " +
                            "LIMIT 1 OFFSET 0";*/
                    inventarioInicialTurno = await GetConnectionAsync().ExecuteScalarAsync<double>(query, idEquipo, fechaProduccion.GetDBDate(), turnoAnterior);
                    cantidadCigarrosConsumidos = await GetConnectionAsync().ExecuteScalarAsync<double>(builder.ToString(), idEquipo, fechaProduccion.GetDBDate(), turno);
                    totalCantidadCigarrosConsumidos = inventarioInicialTurno + cantidadCigarrosConsumidos;
                }
                return totalCantidadCigarrosConsumidos;
                
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

        public async Task<double> GetCigarrosUltimaBandeja(string idEquipo, DateTime fechaProduccion, Int64 turno, string idProducto = "")
        {
            var Intentado = false;
            var cigarsLastTray = 0.0;
           
            VolverALeer:
            if (Intentado) await Task.Delay(Task_Delay);
            try
            {
               var builder = new StringBuilder();
               builder.Append("SELECT TOP 1 ");
               builder.Append("    SUM(Quantity) AS Total ");
               builder.Append("FROM    CONSUMPTIONS ");
               builder.Append("WHERE   IFNULL(TrayID,'') != ''  AND QUANTITY != 0 AND EquipmentID = ? ");
               builder.Append("AND CUSTOMFECHA=? AND TURNID=? ");
               builder.Append("GROUP BY CUSTOMFECHA=? ");
               builder.Append("ORDER BY CUSTOMFECHA=? DESC");

                cigarsLastTray = await (GetConnectionAsync().ExecuteScalarAsync<double>(builder.ToString(), idEquipo, fechaProduccion.GetDBDate(), turno));
                return cigarsLastTray;
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

        public async Task<Int32> GetCantidadBandejasConsumidasAsync(string idEquipo, DateTime fechaProduccion, int turno, string idProducto="")
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();
                builder.Append("SELECT ");
                builder.Append("    COUNT(Quantity) AS Total ");
                builder.Append("FROM    CONSUMPTIONS ");
                builder.Append("WHERE   IFNULL(TrayID,'') != ''  AND QUANTITY >=0 AND EquipmentID = ? ");
                builder.Append("AND CUSTOMFECHA=? AND TURNID=?");

                if (!String.IsNullOrEmpty(idProducto))
                {
                    builder.Append(String.Format(" AND PRODUCTCODE='{0}'", idProducto));
                }

                var cantidadBandejasConsumidas = await GetConnectionAsync().ExecuteScalarAsync<Int32>(builder.ToString(),
                    idEquipo,fechaProduccion.GetDBDate(),turno);

                return cantidadBandejasConsumidas;
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

        public async Task<Single> GetCantidadSalidas(string idEquipo, string idSubEquipo, string idProducto, 
            DateTime fechaProduccion, int turno)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();
                builder.Append("SELECT ");
                builder.Append("    SUM(Quantity) AS Total ");
                builder.Append("FROM    ELABORATES ");
                builder.Append("WHERE   ISRETURN=0   AND   EQUIPMENTID =? AND SUBEQUIPMENTID =?");
                builder.Append("AND  PRODUCTCODE=? AND CUSTOMFECHA=? AND TURNID=?");

                var cantidadTotalSalidas = await GetConnectionAsync().ExecuteScalarAsync<Single>(builder.ToString(),
                    idEquipo, idSubEquipo, idProducto, fechaProduccion.GetDBDate(), turno);

                return cantidadTotalSalidas;
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

        public async Task<Double> GetConsumptionsSumByMaterialCode(String materialCode, String EquipmentId, DateTime ProductionDate, int Turno)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();
                builder.Append("SELECT ");
                builder.Append("    SUM(Quantity) AS Total ");
                builder.Append("FROM    CONSUMPTIONS ");
                builder.Append("WHERE   IFNULL(TrayID,'') != '' AND EquipmentID = ? ");
                builder.Append("AND  MATERIALCODE=? AND CUSTOMFECHA=? AND TurnID=?");


                var productsConsumedQuantity = 0.0;

                productsConsumedQuantity = await GetConnectionAsync().ExecuteScalarAsync<Single>(builder.ToString(),
                       EquipmentId, materialCode, ProductionDate.GetDBDate(), Turno);

                return productsConsumedQuantity;
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

        public async Task<Boolean> Get_InputOutputControl_ByCigarsConsumed(ActualConfig actualConfig, float cantidadProximaSalida, DateTime fechaProduccion, int Turno, String Unidad)
        {
            var repoz = new RepositoryZ(this.Connection);
            var repotimes = new RepositoryTimes(this.Connection);

            var time = await repotimes.GetAsyncByKey(actualConfig.TimeID);

            if (time.Producto == Times.ProductTypes.None || time.Producto == Times.ProductTypes.Validar_Tipo_Almacenamiento)
            {
                return true;
            }

            if (!Proceso.IsInputOutputControlActive && Proceso.IsSubEquipment) //Contingencia de desactivación de control de salidas para sub equipos
            {
                return true;
            }

            var Intentado = false;

        VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();
                builder.Append("SELECT ");
                builder.Append("    SUM(Quantity) AS Total ");
                builder.Append("FROM    CONSUMPTIONS ");
                builder.Append("WHERE   IFNULL(TrayID,'') != '' AND EquipmentID = ? ");
                builder.Append("AND  PRODUCTCODE=? AND CUSTOMFECHA=? AND TurnID=?");

                var materialesBom = new List<MaterialReport>();
                var materialRolado = new MaterialReport();

                var cantidadProductoConsumidos = 0.0;
                var cantidadProductosAnterior = 0.0;
                var cantidadProductoActual = 0.0;

                if (Proceso.IsSubEquipment)
                {
                    try
                    {
                        //Consulta en línea de cantidad de bandejas consumidas.
                        /*cantidadProductoConsumidos = await repoz.GetTotalConsumedProducts
                         (actualConfig.EquipmentID, actualConfig.ProductCode, fechaProduccion, Turno);*/

                        await repoz.SyncConsumptions(fechaProduccion, Turno);
                        cantidadProductoConsumidos = await GetConnectionAsync().ExecuteScalarAsync<Single>(builder.ToString()
                            , actualConfig.EquipmentID, actualConfig.ProductCode, fechaProduccion.GetDBDate(), Turno);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    materialesBom = await repoz.GetBomMaterial(actualConfig.ProductCode, actualConfig.VerID);
                    materialRolado = materialesBom.Where(s => s.MaterialName.StartsWith("R.", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                    /*cantidadProductoConsumidos = await GetConnectionAsync().ExecuteScalarAsync<Single>(builder.ToString(), 
                        actualConfig.EquipmentID, actualConfig.ProductCode, fechaProduccion.GetDBDate(), Turno);*/

                    if (materialRolado != null)
                    {
                        cantidadProductoConsumidos = await GetConsumptionsSumByMaterialCode(materialRolado.MaterialCode, actualConfig.EquipmentID, fechaProduccion, Turno);

                        cantidadProductosAnterior = await GetElaboratesSumByMaterialCode(materialRolado.MaterialCode, actualConfig, fechaProduccion, Turno);
                    }
                }

                builder = new StringBuilder();
                builder.Append("SELECT CANTIDADCONSUMOPENDIENTE ");
                builder.Append("FROM    CONFIGURACIONINICIALCONTROLSALIDAS ");
                builder.Append("WHERE   ESTATUS=1 AND IDEQUIPO =? AND IDPRODUCTO =?");
                builder.Append("AND  CUSTOMFECHA=? AND TURNO=?");

                var cantidadConsumoPendiente = await GetConnectionAsync()
                    .ExecuteScalarAsync<Single>(builder.ToString()
                    , actualConfig.EquipmentID, actualConfig.ProductCode
                    , fechaProduccion.GetDBDate(), Turno);
                
                var cantidadRemanenteInventarioFinalTurno = 0.0;

                var listTurns = await GetDateandTurns();
                var ultimoTurnoCerrado = listTurns.OrderByDescending(s => s.Produccion).FirstOrDefault();
                var stock = new Stocks();
                var materials = new List<MaterialReport>();

                if (ultimoTurnoCerrado != null)
                {
                    stock = await repoz.ExistClosedStockAsync(ultimoTurnoCerrado.Produccion, ultimoTurnoCerrado.TurnID);
                    materials = await repoz.GetMaterialStockAsync(ultimoTurnoCerrado.Produccion,
                        ultimoTurnoCerrado.TurnID, stock);

                    if (materials.Count > 0)
                    {
                        var inventarioFinalTurnoAnterior = new MaterialReport();
                        if (Proceso.IsSubEquipment)
                        {
                            inventarioFinalTurnoAnterior = materials.FirstOrDefault(s => s.MaterialCode.Equals(actualConfig.ProductCode, StringComparison.CurrentCultureIgnoreCase));
                        }
                        else
                        {
                            if (materialRolado != null)
                            {
                                inventarioFinalTurnoAnterior = materials.FirstOrDefault(s => s.MaterialCode.Equals(materialRolado.MaterialCode, StringComparison.CurrentCultureIgnoreCase));
                            }
                        }
                        if (inventarioFinalTurnoAnterior != null)
                        {
                            cantidadRemanenteInventarioFinalTurno = inventarioFinalTurnoAnterior.Quantity;
                        }
                    }
                }

                builder = new StringBuilder();
                builder.Append("SELECT ");
                builder.Append("    SUM(Quantity) AS Total ");
                builder.Append("FROM    ELABORATES ");
                builder.Append("WHERE   ISRETURN=0   AND   EQUIPMENTID =?");
                if (Proceso.IsSubEquipment)
                {
                    builder.Append(String.Format(" AND SUBEQUIPMENTID='{0}'", actualConfig.SubEquipmentID));
                }
                builder.Append("AND  PRODUCTCODE=? AND CUSTOMFECHA=? AND TURNID=?");

                cantidadProductoActual = await GetConnectionAsync().ExecuteScalarAsync<Single>(builder.ToString(), actualConfig.EquipmentID,
                    actualConfig.ProductCode, fechaProduccion.GetDBDate(), Turno);                              

                double totalConsumo = Math.Round(cantidadProductoConsumidos +  cantidadConsumoPendiente + cantidadRemanenteInventarioFinalTurno, 3);
                double totalSalida = Math.Round(cantidadProductosAnterior + cantidadProductoActual + cantidadProximaSalida, 3);

                var diferencia = totalConsumo - totalSalida;

                if (diferencia >= 0.0)
                {
                    return true;
                }
                else
                {
                    return false;
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
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<double> GetElaboratesSumByMaterialCode(string materialCode, ActualConfig actualConfig, DateTime productionDate, int turn)
        {
            var Intentado = false;

        VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var customFecha = productionDate.GetDBDate();
                var cantidadTotalProductos = 0.0;

                var listaProductosAnterioresTurno = (await GetConnectionAsync().Table<Elaborates>()
                    .Where(s => s.EquipmentID == actualConfig.EquipmentID
                    && s.CustomFecha == customFecha
                    && s.TurnID == turn && s.ProductCode != actualConfig.ProductCode).ToListAsync()).Select(s => s.ProductCode).Distinct();

                //Buscar productos anteriormente ejecutados en el turno que utilizan el mismo rolado
                foreach (var codigoProducto in listaProductosAnterioresTurno)
                {
                    var materialesBomProductoAnterior = await GetBomMaterial(codigoProducto, actualConfig.VerID);
                    if (materialesBomProductoAnterior != null)
                    {
                        //Buscar el material rolado del producto anterior
                        var materialRoladoProductoAnterior = materialesBomProductoAnterior.Where(s => s.MaterialName.StartsWith("R.", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                        //validar que el producto actual y el anterior utilicen el mismo rolado
                        if (materialRoladoProductoAnterior != null && materialCode.Equals(materialRoladoProductoAnterior.MaterialCode))
                        {
                            var builder = new StringBuilder();
                            builder.Append("SELECT ");
                            builder.Append("    SUM(Quantity) AS Total ");
                            builder.Append("FROM    ELABORATES ");
                            builder.Append("WHERE   ISRETURN=0   AND   EQUIPMENTID =?");
                            if (Proceso.IsSubEquipment)
                            {
                                builder.Append(String.Format(" AND SUBEQUIPMENTID='{0}'", actualConfig.SubEquipmentID));
                            }
                            builder.Append("AND  PRODUCTCODE=? AND CUSTOMFECHA=? AND TURNID=?");

                            var cantidadCigarros = await GetConnectionAsync().ExecuteScalarAsync<Single>(builder.ToString(), actualConfig.EquipmentID,
                                codigoProducto.ToString(), productionDate.GetDBDate(), turn);

                            //Sumar las salidas de productos de cada producto anterior al total de salidas 
                            cantidadTotalProductos += cantidadCigarros;
                        }
                    }
                }
                return cantidadTotalProductos;
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

        public async Task<ProductoTipoAlmacenamiento> GetAlmFillerByIdentifier(ActualConfig actualConfig)
        {
            var builder = new StringBuilder();
            builder.Append("SELECT ");
            builder.Append(" *FROM PRODUCTOTIPOALMACENAMIENTO ");
            builder.Append("WHERE identificador = ?");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var ProductoTipoAlmacenamiento =  await GetConnectionAsync().QueryAsync<ProductoTipoAlmacenamiento>
                    (builder.ToString(), actualConfig.Identifier);
                return ProductoTipoAlmacenamiento.FirstOrDefault();
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

        public List<ProductoTipoAlmacenamiento> GetAlmFillers()
        {
            return AsyncHelper.RunSync(() => GetAlmFillersAsync());
        }

        public async Task<List<ProductoTipoAlmacenamiento>> GetAlmFillersAsync()
        {
            var repoAlmFillers = new RepositoryProductoTipoAlmacenamientos(this.Connection);

            var AlmFillers = await repoAlmFillers.GetAsyncAll();

            return AlmFillers.ToList();
        }

        public async Task<Boolean> Get_Input_Output(ActualConfig actualconfig, Boolean UseCaching)
        {
            var repoz = new RepositoryZ(this.Connection);
            var repotimes = new RepositoryTimes(this.Connection);

            var time = await repotimes.GetAsyncByKey(actualconfig.TimeID);

            if (time.Producto == Times.ProductTypes.None || time.Producto == Times.ProductTypes.Validar_Tipo_Almacenamiento) return true;

            var bandejaEntrada = await repoz.GetLastBandejaEntrada(UseCaching);

            if (bandejaEntrada == null) return false;

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var builder = new StringBuilder();
                builder.Append("SELECT ");
                builder.Append("    SUM(Quantity) AS Total ");
                builder.Append("FROM    Elaborates ");
                builder.Append("WHERE   Fecha > ? ");

                var ret = await GetConnectionAsync().ExecuteScalarAsync<Single>(builder.ToString(), bandejaEntrada._Fecha.Ticks);

                var cat = ret - (bandejaEntrada.Quantity); /// Se le duplica

                return cat < 0;
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

        public T GetSetting<T>(Settings.Params Setting, T DefaultValue)
        {
            return AsyncHelper.RunSync<T>(() => GetSettingAsync(Setting, DefaultValue));
        }

        public async Task<T> GetSettingAsync<T>(Settings.Params Setting, T DefaultValue)
        {
            var repoSettings = new RepositorySettings(this.Connection);

            var sett = await repoSettings.GetAsyncByKey(Setting);

            if (sett == null)
            {
                return DefaultValue;
            }
            if (typeof(T) == typeof(Boolean)) return (T)(Object)Convert.ToBoolean(sett.Value);
            if (typeof(T) == typeof(Byte)) return (T)(Object)Convert.ToByte(sett.Value);
            if (typeof(T) == typeof(Int16)) return (T)(Object)Convert.ToInt16(sett.Value);
            if (typeof(T) == typeof(Int32)) return (T)(Object)Convert.ToInt32(sett.Value);
            if (typeof(T) == typeof(Int64)) return (T)(Object)Convert.ToInt64(sett.Value);

            return (T)(Object)sett.Value;
        }

        public async Task<Boolean> ExisteEnlos2UltimosTurnos(String MaterialCode, Int32 ConsumptionID, Int32 CustomFecha)
        {
            var builder = new StringBuilder();
            builder.Append("SELECT  1 ");
            builder.Append("FROM    Consumptions C ");
            builder.Append("WHERE   MaterialCode = ? ");
            builder.Append("AND     CustomID = ? ");
            builder.Append("AND     CustomFecha = ? ");
            builder.Append("AND     EXISTS ");
            builder.Append("( ");
            builder.Append("    SELECT  1 ");
            builder.Append("    FROM    Stocks S ");
            builder.Append("    WHERE   S.CustomFecha = C.CustomFecha ");
            builder.Append("    AND     S.TurnID = C.TurnID ");
            builder.Append("    AND     S.ID IN (SELECT ID FROM Stocks ORDER BY Begin DESC LIMIT 2) ");
            builder.Append(") ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().ExecuteScalarAsync<Boolean>(builder.ToString(), MaterialCode, ConsumptionID, CustomFecha);
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

        private List<Categories> CategoriesBuffer = new List<Categories>(); // Caching de Categorias

        public async Task<List<Categories>> GetCategorias(String ProductCode)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                if (CategoriesBuffer == null || !CategoriesBuffer.Any(w => w.MaterialCode == ProductCode))
                {
                    CategoriesBuffer = await GetConnectionAsync().Table<Categories>().Where(w => w.MaterialCode == ProductCode).ToListAsync();
                }

                return CategoriesBuffer;
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

        public String GetLoteInternoBySupplier(String ProductCode, String Lote, Int16 EmpaqueNo)
        {
            return AsyncHelper.RunSync<String>(() => GetLoteInternoBySupplierAsync(ProductCode, Lote, EmpaqueNo));
        }

        public async Task<String> GetLoteInternoBySupplierAsync(String ProductCode, String Lote, Int16 EmpaqueNo)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var material = await GetMaterialByCodeOrReferenceAsync(ProductCode);

                if (material != null)
                {
                    ProductCode = material.Code;

                    var builder = new StringBuilder();
                    builder.Append("SELECT ");
                    builder.Append("    L.Code, ");
                    builder.Append("    I.BoxNumber ");
                    builder.Append("FROM        Lots L ");
                    builder.Append("LEFT JOIN   Inventories I ");
                    builder.Append("ON          L.MaterialCode = I.MaterialCode ");
                    builder.Append("AND         L.Code = I.Lot ");
                    builder.Append("AND         I.BoxNumber = ? ");
                    builder.Append("WHERE       L.MaterialCode = ? ");
                    builder.Append("AND         L.Reference = ? ");

                    var lote = await GetConnectionAsync().QueryAsync<LotSecuence>(builder.ToString(), EmpaqueNo, ProductCode, Lote);

                    if (lote.Any(a => a.BoxNumber.HasValue))
                        return lote.Single(w => w.BoxNumber == EmpaqueNo).Code;
                    else
                    {
                        var intentado = 0;

                        VolveralRequest:

                        if (intentado == 5) return Lote;

                        if (intentado > 0) await Task.Delay(Task_Delay);

                        intentado++;

                        try
                        {
                            var proceso = GetProces().Result;

                            var url = GetService(ServicesType.POST_SECUENCE_EXTERN);

                            var request = new ValidSecuenceRequest()
                            {
                                WERKS = proceso.Centro,
                                MATNR = ProductCode,
                                LICHA = Lote,
                                EMPAQUENO = EmpaqueNo
                            };

                            var json = await PostJsonAsync(url, request);

                            if (!json.isOk || json.Json.IsJsonEmpty()) return null;

                            var result = JsonConvert.DeserializeObject<ValidSecuenceRequest>(json.Json);

                            return String.IsNullOrEmpty(result.CHARG) ? null : result.CHARG;
                        }
                        catch (JsonException)
                        {
                            goto VolveralRequest;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                else
                    return Lote;
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

        public async Task<Transactions> Get_Last_Transaction(String MaterialCode, String Lote, Int16 EmpaqueNo, Int32 CustomFecha, Byte TurnID)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var transaction = await GetConnectionAsync().Table<Transactions>().Where(w => w.MaterialCode == MaterialCode && w.Lot == Lote && w.BoxNumber == EmpaqueNo && w.CustomFecha == CustomFecha && w.TurnID == TurnID).OrderByDescending(d => d.ID).FirstOrDefaultAsync();
                return transaction;
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

        public async Task<Int32> Get_Next_Sequence(String MaterialCode, String Lot, String LoteSuplidor, Int32 Default)
        {
            var intentado = 0;

            VolveralRequest:

            if (intentado == 5) return Default;

            if (intentado > 0) await Task.Delay(Task_Delay);

            intentado++;

            try
            {
                var proceso = await GetProces();

                var url = GetService(ServicesType.GET_NEXT_SECUENCE);

                var request = new NextSecuenceRequest()
                {
                    WERKS = proceso.Centro,
                    MATNR = MaterialCode,
                    CHARG = Lot,
                    LICHA = LoteSuplidor
                };

                var json = await PostJsonAsync(url, request);

                if (json.isOk)
                {
                    var result = JsonConvert.DeserializeObject<NextSecuenceRequest>(json.Json);
                    return result.SECUENCE;
                }
                else
                    return Default;
            }
            catch (JsonException)
            {
                goto VolveralRequest;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Boolean> Post_Secuences(List<PostSecuenceRequest> lista)
        {
            var intentado = 0;

            VolveralRequest:

            if (intentado == 5) return false;

            if (intentado > 0) await Task.Delay(Task_Delay);

            intentado++;

            try
            {
                var url = GetService(ServicesType.POST_EMPAQUES);

                var json = await PostJsonAsync(url, lista);

                if (json.isOk)
                {
                    var result = JsonConvert.DeserializeObject<ZsilmReturn>(json.Json);
                    return result.TIPO.Equals("S");
                }
                else
                    return false;
            }
            catch (JsonException)
            {
                goto VolveralRequest;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InitMonitor()
        {
            if (SyncMonitor == null) SyncMonitor = new SyncLogMonitor();

            SyncMonitor.Detalle.Clear();
        }

        public SyncLogMonitor GetSyncMonitor()
        {
            SyncMonitor.TotalRegistrosBajada += SyncMonitor.Detalle.Sum(s => s.RegistrosBajada);
            SyncMonitor.TotalRegistrosSubida += SyncMonitor.Detalle.Sum(s => s.RegistrosSubida);
            SyncMonitor.TotalSizeBajada += SyncMonitor.Detalle.Sum(s => s.SizeBajada);
            SyncMonitor.TotalSizeSubida += SyncMonitor.Detalle.Sum(s => s.SizeSubida);

            return SyncMonitor;
        }

        public async Task<TrayProductRoute> ValidaBandejaEnLinea(TraysList bandeja)
        {
            var intentado = 0;

            VolveralRequest:

            if (intentado == 3) return new TrayProductRoute() { Result = TrayProductRoute._Status.Comunicacion };

            if (intentado > 0) await Task.Delay(Task_Delay);

            intentado++;

            try
            {
                var url = GetService(ServicesType.VALID_BANDEJA);

                var anonimo = new TrayProductRequest
                {
                    idbandeja = bandeja.TrayID,
                    zsecuencia = bandeja.Secuencia,
                    cpudt = bandeja.ModifyDate.GetSapDateL(),
                    cputm = bandeja.ModifyDate.GetSapHoraL()
                };

                var result = await PostJsonAsync(url, anonimo);

                if (result.isOk)
                {
                    var json = JsonConvert.DeserializeObject<TrayProductRouteResult>(result.Json);

                    if (json.Type.Equals("N")) return new TrayProductRoute()
                    {
                        Result = TrayProductRoute._Status.Incorrecto
                    };

                    var p = json.Bandeja;

                    return new TrayProductRoute
                    {
                        Result = TrayProductRoute._Status.Correcto,
                        Bandeja = json.Bandeja.Get(),
                        Traza = json.traza.Get(),
                        Entrada = json.Entrada.Get()
                    };
                }
                else
                {
                    goto VolveralRequest;
                }
            }
            catch (JsonException)
            {
                goto VolveralRequest;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean ExisteEnBom(String ProductCode, String MaterialCode)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var material = AsyncHelper.RunSync<ConfigMaterials>(() => GetConnectionAsync().Table<ConfigMaterials>().Where(w => w.ProductCode == ProductCode && w.MaterialCode == MaterialCode).FirstOrDefaultAsync());

                return material != null;
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

        public Boolean ExisteLote(String MaterialCode, String Lot)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var lote = AsyncHelper.RunSync<Lots>(() => GetConnectionAsync().Table<Lots>().Where(w => w.MaterialCode == MaterialCode && w.Code == Lot).FirstOrDefaultAsync());

                return lote != null;
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

        public List<ConfiguracionTiempoSalida> CargarConfiguracionTiemposSalidas()
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var repoTiempo = new RepositoryConfiguracionTiempoSalidas(this.Connection);

                return AsyncHelper.RunSync<IEnumerable<ConfiguracionTiempoSalida>>(() => repoTiempo.GetAsyncAll()).ToList();
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

        //AGREGADO POR RALDY PARA FUNCIONALIDA TIEMPO CONSUMO EN PMB 15-02-2023
        public List<ConfiguracionTiempoConsumo> CargarConfiguracionTiemposConsumos()
        {
            var Intentado = false;

        VolverALeer:

            if (Intentado) Task.Delay(Task_Delay).Wait();

            try
            {
                var repoTiempo = new RepositoryConfiguracionTiempoConsumos(this.Connection); //RepositoryConfiguracionTiempoSalidas(this.Connection);

                return AsyncHelper.RunSync<IEnumerable<ConfiguracionTiempoConsumo>>(() => repoTiempo.GetAsyncAll()).ToList();
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



        /// <summary>
        /// Método para obtener una bandeja específica desde SQL server
        /// </summary>
        /// <param name="idBandeja"></param>
        /// <param name="secuenciaBandeja"></param>
        /// <param name="timeID"></param>
        /// <returns></returns>

        public async Task<TraysList> GetEstatusBandeja(string idBandeja, short secuenciaBandeja, string timeID = null)
        {
            var traysList = new TraysList();
            try
            {
                var parametros = "?idBandeja=" + idBandeja + "&secuenciaBandeja=" + secuenciaBandeja.ToString() + "&idTiempo=" + timeID;
                var url = GetSqlServicePath(SqlServiceType.GetEstatusBandeja, parametros);
                var json = await GetJsonAsync(url);
                if (!json.Json.IsJsonEmpty())
                {
                    if (json.isOk)
                    {
                        var estatusBandeja = new TraysProductsResult();
                        estatusBandeja = JsonConvert.DeserializeObject<TraysProductsResult>(json.Json);
                        if (estatusBandeja != null)
                        {
                            traysList.BarCode = estatusBandeja.idbandeja;
                            traysList.Secuencia = (short)estatusBandeja.zsecuencia;
                            traysList.BatchID = estatusBandeja.BATCHID;
                            traysList.EquipmentID = estatusBandeja.IDEQUIPO;
                            traysList.ElaborateID = estatusBandeja.SECSALIDA;
                            traysList.ProductCode = estatusBandeja.matnr;
                            traysList.Status = estatusBandeja.status == "VA" ? TraysProducts._Status.Vacio : TraysProducts._Status.Lleno;
                            traysList.Quantity = estatusBandeja.menge;
                            traysList.Fecha = RepositoryBase.GetDatetime(estatusBandeja.FECHA, estatusBandeja.HORA);
                            traysList.TimeID = estatusBandeja.idtiempo;
                            traysList.ModifyDate = RepositoryBase.GetDatetime(estatusBandeja.cpudt, estatusBandeja.cputm).Value;
                            traysList.TrayID = estatusBandeja.idbandeja;
                            traysList.Unit = estatusBandeja.meins;
                            traysList.VerID = estatusBandeja.verid;
                        }
                        if (estatusBandeja == null)
                        {
                            traysList = null;
                        }
                    }
                    else
                    {
                        throw json.ex;
                    }
                }

            }
            catch (Exception e)
            {
                throw;
            }
            return traysList;
        }

        public float GetHopperCigarsQuantity(String process, string timeID)
        {
            var cigarsQuantity = 0.0;

            try
            {
                if (process.Equals("2301"))
                {
                    //Proceso 2301, tipos de maquinas PMB de area FILTER TIP 
                    if (timeID.Equals("23") || timeID.Equals("25") || timeID.Equals("26"))
                    {
                        //Cantidad de cigarritos en unidad TH
                        cigarsQuantity = 2.2;
                        //cigarsQuantity = 0.720;// + 3.6 = 4.320 //ESTA ES LA MODIFICACION PROPUESTA POR RALDY 20/02/2023
                    }
                }
                return (float) cigarsQuantity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double GetMaxQuantityForPartialElaborates(String process, string timeID)
        {
            var traysCigarsMaxQuantity = 0.0;

            try
            {
                if (process.Equals("2301"))
                {
                    //Proceso 2301, tipos de maquinas PMB  
                    switch (timeID)
                    {
                        case "02":
                        case "23": // FILTER TIP
                            traysCigarsMaxQuantity = 1.875;
                            break;
                        case "03":
                            traysCigarsMaxQuantity = 1.975; //traysCigarsMaxQuantity = 3.599;//+ 0.720 (hopper) = 4.320 //ESTA ERA MODIFICACION PROPUESTA: 
                            break;
                        case "26": // FILTER TIP
                            traysCigarsMaxQuantity = 2.370;
                            break;
                    }
                }
                return traysCigarsMaxQuantity;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}



