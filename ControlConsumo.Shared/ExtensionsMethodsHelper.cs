using ControlConsumo.Shared.Models.Consumption;
using ControlConsumo.Shared.Models.ProductRoute;
using ControlConsumo.Shared.Models.TrayProduct;
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ControlConsumo
{
    /// <summary>
    /// Clase para los definir todos los Helpers de la Aplicacion
    /// </summary>
    public static class ExtensionsMethodsHelper
    {
        #region Sql

        public static String ContainHelper(this String str)
        {
            return string.Concat("%", str, "%");
        }

        public static String BeginHelper(this String str)
        {
            return string.Concat(str, "%");
        }

        public static String EndHelper(this String str)
        {
            return string.Concat("%", str);
        }

        public static String DateTimeHelper(this DateTime date)
        {
            return date.ToFileTimeUtc().ToString();
        }

        #endregion

        public static Boolean IsNumeric(String str)
        {
            try
            {
                Convert.ToSingle(str);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Single ToNumeric(this String str)
        {
            Single Result = 0;

            Single.TryParse(str, out Result);

            return Result;
        }

        public static Boolean IsJsonEmpty(this String str)
        {
            return str == "[]\r\n" || str == "\r\n";
        }

        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        /*
        public static String JsonClean(this String json)
        {
            var split = json.Split(',');

            var Cache = new List<String>();

            foreach (var item in split)
            {
                foreach (var item2 in item.Split(':'))
                {
                    if (Cache.Contains(item2)) continue;

                    var CleanItem = item2.Replace(",", "").Replace("}", "").Replace("]", "").Replace("\n", "").Replace("\r", "");

                    if (!CleanItem.Contains("\"") && IsNumeric(CleanItem) && (CleanItem.Substring(0, 1) == "0" || CleanItem.Length == 8)) // Horas en 000000
                    {
                        json = json.Replace(String.Concat(":", CleanItem, ","), String.Concat(":\"", CleanItem, "\","));
                        json = json.Replace(String.Concat(":", CleanItem, "}"), String.Concat(":\"", CleanItem, "\"}"));
                        Cache.Add(item2);
                    }
                }
            }

            return json;
        }*/

        public static String GetSapCode(String matnr)
        {
            try
            {
                return Convert.ToInt32(matnr).ToString();
            }
            catch (Exception)
            {
                return matnr;
            }
        }

        public static String GetSapDateL(this DateTime date)
        {
            return GetSapDate(date.ToLocalTime());
        }

        public static String GetSapHoraL(this DateTime hora)
        {
            return GetSapHora(hora.ToLocalTime());
        }

        public static String GetSapDate(this DateTime date)
        {
            if (date.Year == 1)
            {
                return "00000000";
            }

            return date.ToString("yyyyMMdd");
        }

        public static Int32 GetDBDateL(this DateTime date)
        {
            return GetDBDate(date.ToLocalTime());
        }

        public static Int32 GetDBDate(this DateTime date)
        {
            return Convert.ToInt32(date.ToString("yyyyMMdd"));
        }

        public static String GetSapHora(this DateTime hora)
        {
            if (hora.Year == 1)
            {
                return "000000";
            }

            return hora.ToString("HHmmss");
        }

        public static String GetStringEnumerable(this IEnumerable<String> ls)
        {
            return String.Join(",", ls.Select(p => String.Concat("'", p, "'")));
        }

        public static String GetInt32Enumerable(this IEnumerable<Int32> ls)
        {
            return String.Join(",", ls.Select(p => p.ToString()));
        }

        public static String GetInt64Enumerable(this IEnumerable<Int64> ls)
        {
            return String.Join(",", ls.Select(p => p.ToString()));
        }

        public static Single Round3(this Single Value)
        {
            return (Single)Math.Round(Value, 3);
        }

        public static T Clone<T>(this T Source)
        {
            if (Source != null)
            {
                var serialized = JsonConvert.SerializeObject(Source);
                return JsonConvert.DeserializeObject<T>(serialized);
            }

            return default(T);
        }

        public static String ToSapCode(this String Code)
        {
            return Code.PadLeft(18, '0');
        }

        public static Consumptions Get(this ConsumptionsRequest p)
        {
            return new Consumptions
            {
                ProcessID = p.IDPROCESS,
                Center = p.WERKS,
                CustomFecha = RepositoryBase.GetDatetime(p.FECHA, p.HORA).Value.GetDBDate(),
                Produccion = RepositoryBase.GetDatetime(p.FECHA, p.HORA).Value,
                Fecha = RepositoryBase.GetDatetime(p.CPUDT, p.CPUTM).Value,
                CustomID = p.SECENTRADA,
                EquipmentID = p.IDEQUIPO,
                TimeID = p.IDTIEMPO,
                ProductCode = p.MATNR,
                VerID = p.VERID,
                MaterialCode = p.MATNR2,
                Logon = p.USNAM,
                Sync = false,
                TurnID = p.IDTURNO,
                Lot = p.CHARG,
                BoxNumber = p.BOXNO,
                Quantity = p.MENGE,
                SubEquipmentID = p.IDEQUIPO2,
                Unit = p.MEINS,
                TrayID = p.IDBANDEJA,
                ElaborateID = p.SECSALIDA,
                TrayEquipmentID = p.IDEQUIPO3,
                TrayDate = RepositoryBase.GetDatetime(p.CPUDT3),
                BatchID = p.BATCHID
            };
        }

        public static TraysProducts Get(this TraysProductsResult p)
        {
            return new TraysProducts
            {
                ID = String.Concat(p.idbandeja.ToUpper(), p.zsecuencia.ToString("00000")),
                ProductCode = p.matnr,
                Status = p.status == "LL" ? TraysProducts._Status.Lleno : TraysProducts._Status.Vacio,
                Quantity = p.menge,
                TimeID = p.idtiempo,
                Sync = false,
                TrayID = p.idbandeja,
                Secuencia = (short)p.zsecuencia,
                Unit = p.meins,
                VerID = p.verid,
                ElaborateID = p.SECSALIDA,
                EquipmentID = p.IDEQUIPO,
                Fecha = RepositoryBase.GetDatetime(p.FECHA, p.HORA),
                UsuarioLlenada = p.UsuarioLlenada,
                ModifyDate = RepositoryBase.GetDatetime(p.cpudt, p.cputm).Value,
                BatchID = p.BATCHID,
                FechaHoraVaciada = p.FechaVaciada != null ? RepositoryBase.GetDatetime(p.FechaVaciada, p.HoraVaciada) : null,
                UsuarioVaciada = p.UsuarioVaciada,
                IdEquipoVaciado = p.IdEquipoVaciado,
                formaVaciada = p.formaVaciada
            };
        }

        public static ProductsRoutes Get(this ProductRouteRequest item)
        {
            var newRoute = new ProductsRoutes();

            newRoute.ProcessID = item.IDPROCESS;
            newRoute.TimeID = item.IDTIEMPO;
            newRoute.CustomID = item.IDREGISTRO;
            newRoute.Year = item.MJAHR;
            newRoute.EquipmentID = item.IDEQUIPO;
            newRoute.Center = item.WERKS;
            newRoute.ProductCode = item.MATNR;
            newRoute.VerID = item.VERID;
            newRoute.Lot = item.CHARG;
            newRoute.LotManufacture = item.LOTEMAN ?? String.Empty;
            newRoute.ElaborateID = item.SECSALIDA;
            newRoute.CustomFecha = Convert.ToInt32(RepositoryBase.GetDatetime(item.FECHA, item.HORA).Value.GetSapDate());
            newRoute.Produccion = RepositoryBase.GetDatetime(item.FECHA, item.HORA).Value;
            newRoute.TurnID = item.IDTURNO;
            newRoute.BatchID = item.BATCHID;
            newRoute.PackID = item.IDEMPAQUE ?? String.Empty;
            newRoute.SecuenciaEmpaque = item.SECEMPAQUE;
            newRoute.Quantity = item.MENGE;
            newRoute.Peso = item.MENGE2;
            newRoute.Unit = item.MEINS;
            newRoute.TrayID = item.IDBANDEJA;
            newRoute.Status = (ProductsRoutes.RoutesStatus)item.STATUSBAN;
            newRoute.Logon = item.USNAM;
            newRoute.Fecha = RepositoryBase.GetDatetime(item.CPUDT3, item.CPUTM3).Value;
            newRoute.TimeID2 = item.IDTIEMPO2;
            newRoute.CustomID2 = item.IDREGISTRO2;
            newRoute.Year2 = item.MJAHR2;
            newRoute.LastUpdate = item.GetLastDate;
            newRoute.IsActive = false;
            newRoute.Sync = false;

            if (RepositoryBase.GetDatetime(item.CPUDT, item.CPUTM).HasValue) newRoute.Begin = RepositoryBase.GetDatetime(item.CPUDT, item.CPUTM).Value;

            if (RepositoryBase.GetDatetime(item.CPUDT2, item.CPUTM2).HasValue) newRoute.End = RepositoryBase.GetDatetime(item.CPUDT2, item.CPUTM2).Value;

            return newRoute;
        }
    }
}