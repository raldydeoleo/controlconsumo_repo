using ControlConsumo.Shared.Models.Consumption;
using ControlConsumo.Shared.Models.Elaborate;
using ControlConsumo.Shared.Models.R;
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Repositories
{
    public class RepositoryR : RepositoryBase
    {
        public RepositoryR(SQLiteAsyncConnection connection) : base(connection) { }

        public RepositoryR(MyDbConnection connection) : base(connection) { }

        private String GetReportUrl()
        {
            var url = "";

            switch (ConnectionEnvironment)
            {
                case EnvironmentType.Calidad:
                case EnvironmentType.Desarrollo:
                    url = "http://10.199.98.25/ControlConsumo/api/Spc/{0}?Material={1}&TurnID={2}&Equipo={3}";
                    break;
                case EnvironmentType.Produccion:
                    //url = "http://10.199.98.84/ControlConsumo/api/Spc/{0}?Material={1}&TurnID={2}&Equipo={3}"; //Antiguo servidor de aplicaciones de producción
                    url = "http://10.199.98.217/ControlConsumo/api/Spc/{0}?Material={1}&TurnID={2}&Equipo={3}"; //servidor actual de aplicaciones de producción
                    break;

            }

            return url;
        }

        private String GetMaterialReportUrl()
        {
            var url = "";

            switch (ConnectionEnvironment)
            {
                case EnvironmentType.Calidad:
                case EnvironmentType.Desarrollo:
                    url = "http://10.199.98.25/Materials/api/Materials/{0}?Material={1}&Lot={2}&BoxNumber={3}";
                    break;
                case EnvironmentType.Produccion:
                    //url = "http://10.199.98.84/Materials/api/Materials/{0}?Material={1}&Lot={2}&BoxNumber={3}"; //Antiguo servidor de aplicaciones de producción
                    url = "http://10.199.98.217/Materials/api/Materials/{0}?Material={1}&Lot={2}&BoxNumber={3}"; //servidor actual de aplicaciones de producción
                    break;

            }

            return url;
        }

        public async Task<List<ProductionReport>> GetProduccion(Byte TurnID, DateTime Fecha)
        {
            var builder = new StringBuilder();
            builder.Append("SELECT  ");
            builder.Append("    P.Code AS ProductCode, ");
            builder.Append("    P.Name AS ProductName, ");
            builder.Append("    P.Short AS ProductShort, ");
            builder.Append("    E.Unit, ");
            builder.Append("    E.TurnID, ");
            builder.Append("    E.Produccion AS Fecha, ");
            builder.Append("    COUNT(E.Quantity) AS Quantity, ");
            builder.Append("    SUM(E.Quantity) AS Total ");
            builder.Append("FROM         Elaborates E ");
            builder.Append("INNER JOIN   Materials P ");
            builder.Append("ON           E.ProductCode = P.Code ");
            builder.Append("WHERE        E.Produccion > ? ");
            builder.Append("AND          E.TurnID = ? ");
            builder.Append("AND          E.IsReturn = 0 ");
            builder.Append("GROUP BY     P.Code, P.Name, P.Short, E.Unit, E.TurnID, E.Fecha ");
            builder.Append("ORDER BY     E.Fecha DESC");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var fecha2 = Fecha.AddDays(-31);

                var result = await GetConnectionAsync().QueryAsync<ProductionReport>(builder.ToString(), fecha2.Ticks, TurnID);

                return result.Where(p => p.Fecha.ToLocalTime().Month == Fecha.Month).GroupBy(p => new { p.ProductCode, p.ProductName, p.ProductShort, p.Unit, p.Fecha.ToLocalTime().Date })
                    .Select(p => new ProductionReport
                    {
                        ProductCode = p.Key.ProductCode,
                        ProductName = p.Key.ProductName,
                        ProductShort = p.Key.ProductShort,
                        Unit = p.Key.Unit,
                        Fecha = p.Key.Date,
                        Quantity = (short)p.Sum(s => s.Quantity),
                        Total = p.Sum(s => s.Total)
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

        public async Task<List<ProductionReport>> GetProduccionDetail(Byte TurnID, DateTime Fecha)
        {
            var CustomFecha = Fecha.GetDBDate();

            var builder = new StringBuilder();
            builder.Append("SELECT  ");
            builder.Append("    P.Code AS ProductCode, ");
            builder.Append("    P.Name AS ProductName, ");
            builder.Append("    P.Short AS ProductShort, ");
            builder.Append("    P.Reference AS ProductReference, ");
            builder.Append("    E.ID AS ElaborateID, ");
            builder.Append("    E.Unit, ");
            builder.Append("    E.TurnID, ");
            builder.Append("    E.Produccion AS Fecha, ");
            builder.Append("    E.Quantity AS Quantity ");
            builder.Append("FROM         Elaborates E ");
            builder.Append("INNER JOIN   Materials P ");
            builder.Append("ON           E.ProductCode = P.Code ");
            builder.Append("WHERE        E.CustomFecha = ? ");
            builder.Append("AND          E.TurnID = ? ");
            builder.Append("ORDER BY     E.Fecha DESC");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<ProductionReport>(builder.ToString(), CustomFecha, TurnID);
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

        public async Task<List<EmpaqueImpresionResult>> GetProduccionDetailEmpaque(String EquipmentID, Byte TurnID, DateTime Fecha, String ProductCode)
        {
            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                var retorno = new List<EmpaqueImpresionResult>();
                var CustomFecha = Fecha.GetDBDate();

                var trazas = await GetConnectionAsync().Table<ProductsRoutes>().Where(w => w.EquipmentID == EquipmentID && w.ProductCode == ProductCode && w.TurnID == TurnID && w.CustomFecha == CustomFecha).OrderByDescending(o => o.ID).ToListAsync();
                var salidas = await GetConnectionAsync().Table<Elaborates>().Where(w => w.CustomFecha == CustomFecha && w.ProductCode == ProductCode && w.TurnID == TurnID).OrderByDescending(o => o.ID).ToListAsync();

                foreach (var item in salidas)
                {
                    var traza = trazas.FirstOrDefault(f => f.ElaborateID == item.CustomID);

                    if (traza != null)
                    {
                        retorno.Add(new EmpaqueImpresionResult
                        {
                            Salida = item,
                            Traza = traza
                        });
                    }
                }

                return retorno;
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

        public async Task<List<MaterialReport>> GetConsumptions(Byte TurnID, DateTime Fecha)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT  ");
            builder.Append("    P.Name AS ProductName, ");
            builder.Append("    P.Code AS ProductCode, ");
            builder.Append("    P.Reference AS ProductReference, ");
            builder.Append("    P.Short AS ProductShort, ");
            builder.Append("    P.Unit AS ProductUnit, ");
            builder.Append("    M.Name AS MaterialName, ");
            builder.Append("    M.Code AS MaterialCode, ");
            builder.Append("    M.Reference AS MaterialReference, ");
            builder.Append("    C.Unit AS MaterialUnit, ");
            builder.Append("    C.Logon, ");
            builder.Append("    C.BoxNumber, ");
            builder.Append("    C.Quantity, ");
            builder.Append("    C.Produccion AS Produccion, ");
            builder.Append("    C.Lot, ");
            builder.Append("    C.BatchID, ");
            builder.Append("    C.TrayID ");
            builder.Append("FROM         Consumptions C ");
            builder.Append("INNER JOIN   Materials P ");
            builder.Append("ON           P.Code = C.ProductCode ");
            builder.Append("INNER JOIN   Materials M ");
            builder.Append("ON           M.Code = C.MaterialCode ");
            builder.Append("WHERE        C.CustomFecha = ? ");
            builder.Append("AND          C.TurnID = ? ");
            builder.Append("ORDER BY     C.ID DESC ");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<MaterialReport>(builder.ToString(), Fecha.GetDBDate(), TurnID);
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

        public async Task<List<PesoChart>> GetChartGramo(Byte TurnID, DateTime Fecha)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT      Peso ");
            builder.Append("FROM        Elaborates ");
            builder.Append("WHERE       CustomFecha = ? ");
            builder.Append("AND         TurnID = ? ");
            builder.Append("ORDER BY    Produccion DESC");

            return await GetConnectionAsync().QueryAsync<PesoChart>(builder.ToString(), Fecha.GetDBDate(), TurnID);
        }

        //CREADO POR RALDY PARA OBTENER CONSUMO DE MATERIALES EN CUALQUIER MAQUINA DESDE SQL. 
        //Fecha de inicio del desarrollo de esta funcionalidad: 19-01-2023
        public async Task<Int64> GetMaterialReport(String Material, String Lot, Int64 BoxNumber)
        {
            var Contado = 0;
            var intentado = false;

        VolveraLeer:

            if (Contado == 5) return 0; // new MaterialReport();

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                Contado++;
                var url = String.Format(GetMaterialReportUrl(), "GetMaterialConsumido", Material, Lot, BoxNumber);
                var json = await GetJsonAsync(url);
                if (!json.isOk || json.Json.IsJsonEmpty()) return 0;// new MaterialReport();
                dynamic blogPosts = JArray.Parse(json.Json); //LINEA AGREGADA
                Int64 cantidadMaterial = 0;//= blogPost.Quantity;
                Int64 acumulado =  0;
                for (int i = 0; i < blogPosts.Count; i++)
                {
                    dynamic blogPost = blogPosts[i];
                    acumulado = blogPost.Quantity;
                    cantidadMaterial = cantidadMaterial + acumulado;
                }
                //dynamic blogPost = blogPosts[0];//LINEA AGREGADA               
                    
                return cantidadMaterial;
                //return JsonConvert.DeserializeObject<MaterialReport>(json.Json);
                //dynamic jsonObj = JsonConvert.DeserializeObject(json.Json);                
            }
            catch (JsonException)
            {
                goto VolveraLeer;                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GraficsReport> GetPesoReport(String Equipo, String Material, Byte TurnID)
        {
            var Contado = 0;
            var intentado = false;

            VolveraLeer:

            if (Contado == 5) return new GraficsReport();

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                Contado++;
                var url = String.Format(GetReportUrl(), "GetPeso", Material, TurnID, Equipo);
                var json = await GetJsonAsync(url);
                if (!json.isOk || json.Json.IsJsonEmpty()) return new GraficsReport();

                return JsonConvert.DeserializeObject<GraficsReport>(json.Json);
            }
            catch (JsonException)
            {
                goto VolveraLeer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GraficsReport> GetDiametroReport(String Equipo, String Material, Byte TurnID)
        {
            var Contado = 0;
            var intentado = false;

            VolveraLeer:

            if (Contado == 5) return new GraficsReport();

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                Contado++;
                var url = String.Format(GetReportUrl(), "GetDiametro", Material, TurnID, Equipo);
                var json = await GetJsonAsync(url);
                if (!json.isOk || json.Json.IsJsonEmpty()) return new GraficsReport();

                return JsonConvert.DeserializeObject<GraficsReport>(json.Json);
            }
            catch (JsonException)
            {
                goto VolveraLeer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GraficsReport> GetTiroReport(String Equipo, String Material, Byte TurnID)
        {
            var Contado = 0;
            var intentado = false;

            VolveraLeer:

            if (Contado == 5) return new GraficsReport();

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                Contado++;
                var url = String.Format(GetReportUrl(), "GetTiro", Material, TurnID, Equipo);
                var json = await GetJsonAsync(url);
                if (!json.isOk || json.Json.IsJsonEmpty()) return new GraficsReport();

                return JsonConvert.DeserializeObject<GraficsReport>(json.Json);
            }
            catch (JsonException)
            {
                goto VolveraLeer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<StockReports>> GetReportStock(Byte TurnID, DateTime Fecha)
        {
            var builder = new StringBuilder();

            var customFecha = Fecha.GetDBDate();
            var fechaMaxima = Fecha.AddDays(-2).GetDBDate();

            builder.Append("SELECT  ");
            builder.Append("    M.Name AS MaterialName, ");
            builder.Append("    M.Code AS MaterialCode, ");
            builder.Append("    M.Reference AS MaterialReference, ");
            builder.Append("    M.Unit AS MaterialUnit, ");
            builder.Append("    I.Quantity AS TotalQuantity, ");
            builder.Append("    I.Lot, ");
            builder.Append("    T.Quantity, ");
            builder.Append("    T.Total, ");
            builder.Append("    T.Logon, ");
            builder.Append("    T.Fecha, ");
            builder.Append("    T.TurnID, ");
            builder.Append("    T.Reason, ");
            builder.Append("    L.Reference AS LoteSuplidor, ");
            builder.Append("    T.BoxNumber ");
            builder.Append("FROM ");
            builder.Append("( ");
            builder.Append("    SELECT  ");
            builder.Append("        MaterialCode, ");
            builder.Append("        Lot, ");
            builder.Append("        SUM(Quantity) AS Quantity ");
            builder.Append("    FROM      Inventories ");
            builder.Append("    GROUP BY  MaterialCode, Lot ");
            builder.Append(") I ");
            builder.Append("INNER JOIN   Materials M ");
            builder.Append("ON           I.MaterialCode = M.Code ");
            builder.Append("INNER JOIN   Transactions T ");
            builder.Append("ON           I.MaterialCode = T.MaterialCode ");
            builder.Append("AND          I.Lot = T.Lot ");
            builder.Append("INNER JOIN   Lots L ");
            builder.Append("ON           L.MaterialCode = I.MaterialCode ");
            builder.Append("AND          L.Code = I.Lot ");
            builder.Append("WHERE        T.CustomFecha > ? ");
            builder.Append("AND          I.Quantity > 0 ");
            builder.Append("OR          ( I.Quantity == 0 ");
            builder.Append("AND          EXISTS ");
            builder.Append("(    ");
            builder.Append("        SELECT      *   ");
            builder.Append("        FROM        Transactions TS ");
            builder.Append("        WHERE       TS.Lot = I.Lot ");
            builder.Append("        AND         TS.MaterialCode = I.MaterialCode ");
            builder.Append("        AND         TS.TurnID = ? ");
            builder.Append("        AND         TS.CustomFecha = ? ");
            builder.Append("))    ");
            builder.Append("ORDER BY     T.Fecha DESC");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<StockReports>(builder.ToString(), fechaMaxima, TurnID, customFecha);
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

        public async Task<List<StockResumeList>> GetStockResumeList(DateTime Fecha, Byte TurnID, String Receive, String Consumption, String Return, String Retiro, String Ajust)
        {
            var builder = new StringBuilder();

            var fprodActual = Fecha.GetDBDate();

            var parametros = new List<Object>();

            builder.Append("SELECT ");
            builder.Append("    P.Code AS ProductCode, ");
            builder.Append("    P.Name AS ProductName, ");
            builder.Append("    P.Short AS ProductShort, ");
            builder.Append("    P.Reference AS ProductReference, ");
            builder.Append("    T.Lot, ");
            builder.Append("    T.Entregado AS Entregado, ");
            builder.Append("    T.Consumido AS Consumido, ");
            builder.Append("    T.CustomFecha AS CustomFecha, ");
            builder.Append("    T.TurnID AS TurnID, ");
            builder.Append("    L.Reference ");
            builder.Append("FROM         Materials P ");
            builder.Append("INNER JOIN   Lots L ");
            builder.Append("ON           L.MaterialCode = P.Code ");
            builder.Append("AND          L.Code = T.Lot ");
            builder.Append("INNER JOIN ");
            builder.Append("( ");
            builder.Append("   SELECT ");
            builder.Append("        SUM( ");
            builder.Append("            CASE ");
            builder.Append("                WHEN Reason = ? THEN 1 "); parametros.Add(Receive);
            builder.Append("                WHEN Reason = ? THEN -1 "); parametros.Add(Retiro);
            builder.Append("            END) AS Entregado, ");
            builder.Append("        SUM( ");
            builder.Append("            CASE ");
            builder.Append("                WHEN Reason = ? THEN 1 "); parametros.Add(Consumption);
            builder.Append("                WHEN Reason = ? THEN -1 "); parametros.Add(Return);
            builder.Append("                WHEN Reason = ? AND Quantity < 0 THEN 1 "); parametros.Add(Ajust);
            builder.Append("                WHEN Reason = ? AND Quantity > 0 THEN -1 "); parametros.Add(Ajust);
            builder.Append("            END) AS Consumido, ");
            builder.Append("        Lot, ");
            builder.Append("        MaterialCode, ");
            builder.Append(String.Format("        {0} AS CustomFecha, ", fprodActual));
            builder.Append(String.Format("        {0} AS TurnID ", TurnID));
            builder.Append("   FROM     Transactions T ");
            builder.Append("   WHERE    CustomFecha = ? "); parametros.Add(fprodActual);
            builder.Append("   AND      TurnID = ? "); parametros.Add(TurnID);
            builder.Append("   GROUP BY Lot, MaterialCode ");
            builder.Append(") T ");
            builder.Append("ON           T.MaterialCode = P.Code ");
            //builder.Append("   UNION ALL ");
            //builder.Append("   SELECT ");
            //builder.Append("     SUM( ");
            //builder.Append("        CASE ");
            //builder.Append("             WHEN Reason = ? THEN 1 "); parametros.Add(Receive);
            //builder.Append("             WHEN Reason = ? THEN -1 "); parametros.Add(Retiro);
            //builder.Append("             WHEN Reason = ? THEN -1 "); parametros.Add(Consumption);
            //builder.Append("             WHEN Reason = ? THEN 1 "); parametros.Add(Return);
            //builder.Append("             WHEN Reason = ? AND Quantity < 0 THEN -1 "); parametros.Add(Ajust);
            //builder.Append("             WHEN Reason = ? AND Quantity > 0 THEN 1 "); parametros.Add(Ajust);
            //builder.Append("        END) AS Entregado, ");
            //builder.Append("        0 AS Consumido, ");
            //builder.Append("        Lot, ");
            //builder.Append("        MaterialCode, ");
            //builder.Append("        0 AS CustomFecha, ");
            //builder.Append("        0 AS TurnID ");
            //builder.Append("   FROM     Transactions ");
            //builder.Append("   WHERE    CustomFecha != ? "); parametros.Add(fprodActual);
            //builder.Append("   OR       TurnID != ? "); parametros.Add(TurnID);
            //builder.Append("   GROUP BY Lot, MaterialCode ");
            //builder.Append(") T ");
            //builder.Append("ON           T.MaterialCode = P.Code ");
            //builder.Append("GROUP BY     P.ID, P.Code, P.Name, P.Reference, P.Short, T.Lot");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<StockResumeList>(builder.ToString(), parametros.ToArray());
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

        public async Task<List<StockOnFloor>> GetStockListForStock(String Receive, String Consumption, String Return, String Retiro, String Ajust)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT ");
            builder.Append("    P.Code AS ProductCode, ");
            builder.Append("    P.Name AS ProductName, ");
            builder.Append("    P.Short AS ProductShort, ");
            builder.Append("    P.Reference AS ProductReference, ");
            builder.Append("    P.Unit AS ProductUnit, ");
            builder.Append("    T.Lot,");
            builder.Append("    SUM(CASE ");
            builder.Append("         WHEN Reason = ? THEN 1 ");
            builder.Append("         WHEN Reason = ? THEN -1 ");
            builder.Append("         WHEN Reason = ? THEN -1 ");
            builder.Append("         WHEN Reason = ? THEN 1 ");
            builder.Append("         WHEN Quantity > 0 AND Reason = ? THEN 1 ");
            builder.Append("         WHEN Quantity < 0 AND Reason = ? THEN -1 ");
            builder.Append("    END) AS Logico, ");
            builder.Append("    (SELECT  Quantity FROM Inventories I WHERE I.MaterialCode = T.MaterialCode AND I.Lot = T.Lot) AS Amount ");
            builder.Append("FROM         Transactions T ");
            builder.Append("INNER JOIN   Materials P ");
            builder.Append("ON           T.MaterialCode = P.Code ");
            builder.Append("GROUP BY     P.Code, P.Name,  P.Short, P.Reference, T.Lot");

            var Intentado = false;

            VolverALeer:

            if (Intentado) await Task.Delay(Task_Delay);

            try
            {
                return await GetConnectionAsync().QueryAsync<StockOnFloor>(builder.ToString(), Receive, Retiro, Consumption, Return, Ajust, Ajust);
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

        public async Task<TicketReport> GetTicketReport(String TrayID)
        {
            return await GetTicketReport(new TiquetsRequest()
            {
                IDBANDEJA = TrayID
            });
        }

        public async Task<TicketReport> GetTicketReport(String Equipo, DateTime Fecha, Int16 SecSalida, String TrayID)
        {
            return await GetTicketReport(new TiquetsRequest()
            {
                IDEQUIPO = Equipo,
                FECHA = Fecha.GetSapDate(),
                SECSALIDA = SecSalida,
                IDBANDEJA = TrayID
            });
        }

        private async Task<TicketReport> GetTicketReport(TiquetsRequest request)
        {
            var intentado = false;

            VolveraLeer:

            if (intentado) await Task.Delay(Task_Delay);

            try
            {
                var url = GetService(ServicesType.REPORT_TICKETS);

                var json = await PostJsonAsync(url, request);

                if (!json.isOk || json.Json.IsJsonEmpty()) return null;

                var result = JsonConvert.DeserializeObject<TicketsResult>(json.Json);

                if (String.IsNullOrEmpty(result.HEAD.MATNR)) return null;

                var material = await new RepositoryZ(this.Connection).GetMaterialByCodeOrReferenceAsync(result.HEAD.MATNR);

                return new TicketReport()
                {
                    EquipmentID = result.HEAD.IDEQUIPO,
                    ProductCode = material.Code,
                    ProductName = material.Name,
                    ProductReference = material.Reference,
                    ProductShort = material.Short,
                    VerID = result.HEAD.VERID,
                    Lot = result.HEAD.CHARG,
                    SecSalida = result.HEAD.SECSALIDA,
                    Produccion = GetDatetime(result.HEAD.FECHA, result.HEAD.HORA),
                    TurnID = result.HEAD.IDTURNO,
                    TrayID = request.IDBANDEJA ?? result.HEAD.IDBANDEJA,
                    BatchID = result.HEAD.BATCHID,
                    Status = result.HEAD.STATUS,
                    Quantity = Convert.ToSingle(result.HEAD.MENGE),
                    Unit = result.HEAD.MEINS,
                    Quantity2 = Convert.ToSingle(result.HEAD.MENGE2),
                    Traza = result.HEAD.LOTEMAN,
                    Fecha = GetDatetime(result.HEAD.CPUDT, result.HEAD.CPUTM),
                    Details = result.DETAILS.Select(p => new TicketReport.Detail
                    {
                        MaterialCode = p.MATNR2,
                        MaterialName = p.MAKTG,
                        MaterialShort = p.NORMT,
                        MaterialReference = p.BISMT,
                        EquipmentID = p.IDEQUIPO,
                        TimeID = p.IDTIEMPO,
                        Lot = p.CHARG,
                        MaterialUnit = p.MEINS,
                        BoxNo = Convert.ToInt16(p.BOXNO),
                        Produccion = GetDatetime(p.FECHA, p.HORA),
                        Fecha = GetDatetime(p.CPUDT, p.CPUTM),
                        TrayID = p.IDBANDEJA,
                        EquipmentID3 = p.IDEQUIPO3,
                        SecSalida = p.SECSALIDA,
                        Fecha2 = GetDatetime(p.CPUDT3),
                        BatchID = p.BATCHID,
                        TurnID = p.IDTURNO,
                        LotReference = p.LICHA
                    }).ToList()
                };
            }
            catch (JsonException)
            {
                goto VolveraLeer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ElaboratesReport>> GetElaborateReport(ElaborateReportRequest request)
        {
            return await GetElaborateServer(request);
        }

        public async Task<List<ElaborateParamResult>> GetParametros(ElaborateReportRequest request)
        {
            var url = GetService(ServicesType.REPORT_GET_PARAMETERS);

            var result = await PostJsonAsync(url, request);

            if (!result.isOk || result.Json.IsJsonEmpty()) return null;

            return JsonConvert.DeserializeObject<ElaborateParamResult[]>(result.Json).ToList();
        }

        private async Task<List<ElaboratesReport>> GetElaborateLocal(ElaborateReportRequest request)
        {
            var builder = new StringBuilder();

            builder.Append("SELECT  ");
            builder.Append("    E.Produccion, ");
            builder.Append("    E.TurnID, ");
            builder.Append("    E.EquipmentID, ");
            builder.Append("    E.ProductCode, ");
            builder.Append("    E.VerID, ");
            builder.Append("    E.BatchID, ");
            builder.Append("    E.TrayID, ");
            builder.Append("    E.Quantity, ");
            builder.Append("    E.Unit, ");
            builder.Append("    E.Peso, ");
            builder.Append("    E.Fecha, ");
            builder.Append("    E.Status, ");
            builder.Append("    RM.Begin AS Fecha2, ");
            builder.Append("    RM.EquipmentID AS EquipmentID2 ");
            builder.Append("FROM        Elaborates E ");
            builder.Append("INNER JOIN  ProductsRoutes R ");
            builder.Append("ON          E.CustomFecha = R.CustomFecha ");
            builder.Append("AND         E.CustomID = R.CustomID ");
            builder.Append("AND         E.EquipmentID = R.EquipmentID ");
            builder.Append("LEFT JOIN   ProductsRoutes RM ");
            builder.Append("ON          E.TimeID2 = RM.TimeID ");
            builder.Append("AND         E.Year2 = RM.Year ");
            builder.Append("AND         E.CustomID2 = RM.CustomID ");
            builder.Append("WHERE       E.EquipmentID = ? ");

            if (String.IsNullOrEmpty(request.HORAFROM))
            {
                if (request.HORAFROM.Equals(request.HORATO))
                    builder.Append(String.Format("AND     E.Fecha BETWEEN {0} AND {1} ", GetDatetime(request.FECHAFROM, request.HORAFROM).Value.Ticks, GetDatetime(request.FECHATO, request.HORATO).Value.Ticks));
                else
                    builder.Append(String.Format("AND     E.Fecha = {0} ", GetDatetime(request.FECHAFROM, request.HORAFROM).Value.Ticks));
            }
            else
            {
                if (request.FECHAFROM.Equals(request.FECHATO))
                    builder.Append(String.Format("AND     E.CustomFecha BETWEEN {0} AND {1} ", request.FECHAFROM, request.FECHATO));
                else
                    builder.Append(String.Format("AND     E.CustomFecha = {0} ", request.FECHAFROM));
            }

            if (!String.IsNullOrEmpty(request.BATCHID))
                builder.Append(String.Format("AND         E.BatchID = '{0}' ", request.BATCHID));

            if (request.IDTURNS.Any())
            {
                builder.Append(String.Format("AND        TurnID IN ({0}) ", request.IDTURNS.Select(p => (Int32)p).GetInt32Enumerable()));
            }

            return await GetConnectionAsync().QueryAsync<ElaboratesReport>(builder.ToString(), request.IDEQUIPO);
        }

        private async Task<List<ElaboratesReport>> GetElaborateServer(ElaborateReportRequest request)
        {
            var url = GetService(ServicesType.REPORT_PRODUCTION);

            var result = await PostJsonAsync(url, request);

            if (!result.isOk || result.Json.IsJsonEmpty()) return null;

            var buffer = JsonConvert.DeserializeObject<ElaborateReportResult[]>(result.Json);

            if (buffer.Any())
            {
                return buffer
                    .Select(p => new ElaboratesReport
                    {
                        EquipmentID = p.IDEQUIPO,
                        BatchID = p.BATCHID,
                        EquipmentID2 = p.IDEQUIPO2,
                        TurnID = p.IDTURNO,
                        Unit = p.MEINS,
                        VerID = p.VERID,
                        TrayID = p.IDBANDEJA,
                        Peso = p.MENGE2,
                        Quantity = p.MENGE,
                        ProductCode = p.MATNR,
                        Produccion = GetDatetime(p.FECHA, p.HORA).Value,
                        Fecha = GetDatetime(p.CPUDT, p.CPUTM).Value,
                        Fecha2 = GetDatetime(p.CPUDT2, p.CPUTM2),
                        Status = (ProductsRoutes.RoutesStatus)p.STATUSBAN,
                        Empaque = p.IDEMPAQUE,
                        SecuenciaEmpaque = p.SECEMPAQUE,
                        TrayID2 = p.IDBANDEJA2
                    }).OrderBy(p => p.Status).ThenBy(p => p.Produccion).ToList();
            }

            return null;
        }

        public async Task<Consumptions> ValidaConsumoenLinea(Consumptions consumo)
        {
            var repoz = new RepositoryZ(this.Connection);

            //var exist = await repoz.ValidaBoxNumberAsync(consumo.MaterialCode, consumo.Lot, consumo.BoxNumber);

            //if (exist) return consumo;

            var intentado = 0;

            VolveraLeer:

            if (intentado == 5) return null;

            if (intentado > 0) await Task.Delay(Task_Delay);

            intentado++;

            try
            {
                var url = GetService(ServicesType.VALID_SECUENCES_INLINE);

                var request = new ValidSecuenceRequest()
                {
                    MATNR = consumo.MaterialCode,
                    CHARG = consumo.Lot,
                    BOXNO = consumo.BoxNumber
                };

                var json = await PostJsonAsync(url, request);

                if (!json.isOk || json.Json.IsJsonEmpty()) return null;

                var result = JsonConvert.DeserializeObject<ConsumptionsRequest>(json.Json);

                if (result.FECHA.Equals("00000000")) return null;

                return new Consumptions()
                {
                    ProcessID = result.IDPROCESS,
                    Center = result.WERKS,
                    CustomFecha = GetDatetime(result.FECHA, result.HORA).Value.GetDBDate(),
                    Produccion = GetDatetime(result.FECHA, result.HORA).Value,
                    Fecha = GetDatetime(result.CPUDT, result.CPUTM).Value,
                    CustomID = result.SECENTRADA,
                    EquipmentID = result.IDEQUIPO,
                    TimeID = result.IDTIEMPO,
                    ProductCode = result.MATNR,
                    VerID = result.VERID,
                    MaterialCode = result.MATNR2,
                    Logon = result.USNAM,
                    Sync = false,
                    TurnID = result.IDTURNO,
                    Lot = result.CHARG,
                    BoxNumber = result.BOXNO,
                    Quantity = result.MENGE,
                    SubEquipmentID = result.IDEQUIPO2,
                    Unit = result.MEINS,
                    TrayID = result.IDBANDEJA,
                    ElaborateID = result.SECSALIDA,
                    TrayEquipmentID = result.IDEQUIPO3,
                    TrayDate = GetDatetime(result.CPUDT3),
                    BatchID = result.BATCHID
                };
            }
            catch (JsonException ex)
            {
                goto VolveraLeer;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
