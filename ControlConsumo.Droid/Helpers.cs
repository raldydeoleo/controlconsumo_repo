using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Globalization;
using ControlConsumo.Shared.Tables;
using System.Threading.Tasks;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Shared.Repositories;

namespace ControlConsumo.Droid
{
    static class Helpers
    {
        private const String LoteDateFormat = "yyMMdd";
        public static BarCodeResult GetBarCode(this String str)
        {
            var split = str.Split('-');

            var Result = new BarCodeResult();

            Result.FullBarCode = str.ToUpper();

            switch (split.Length)
            {
                case 4:
                case 5:
                    Result.IsCustom = true;
                    Result.BarCode = split[0].Trim().ToUpper();
                    Result.IsLotInternal = ValidaLoteInterno(split[1].Trim());

                    if (Result.IsLotInternal)
                        Result.Lot = split[1].Trim();
                    else
                    {
                        var repoz = new RepositoryZ(Util.GetConnection());
                        Result.Lot = repoz.GetLoteInternoBySupplier(Result.BarCode, split[1].Trim(), Convert.ToInt16(split[3]));
                    }

                    Result.Quantity = Convert.ToSingle(split[2]);
                    Result.Sequence = Convert.ToInt16(split[3]);

                    break;

                case 3:
                    Result.IsCustom = true;
                    Result.BarCode = split[0].Trim().ToUpper();
                    Result.IsLotInternal = true;
                    Result.Lot = split[1].Trim();
                    Result.Quantity = Convert.ToSingle(split[2]);

                    break;

                default:
                    String barCode = String.Empty;
                    String Secuence = String.Empty; ;

                    foreach (var item in str.ToCharArray())
                    {
                        if (!Char.IsNumber(item))
                            barCode += item.ToString();
                        else
                            Secuence += item.ToString();
                    }

                    Result.BarCode = (!String.IsNullOrEmpty(barCode) ? barCode : Secuence.ToString()).ToUpper();
                    try
                    {
                        Result.Sequence = Convert.ToInt16(Secuence);
                    }
                    catch
                    { }

                    break;
            }

            return Result;
        }
        private static Boolean ValidaLoteInterno(String Lote)
        {
            Lote = Lote.Trim();

            if (Lote.Length != 10) return false;

            if (!Lote.StartsWith("00000"))
            {
                try
                {
                    var Parseo = DateTime.ParseExact(Lote.Substring(0, 6), LoteDateFormat, CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    return false;
                }

                if (!IsNumeric((Lote.Substring(6, 4)))) return false;
            }

            return true;
        }

        public static String ToCustomString(this Single value)
        {
            return value.ToString("N3").Replace(".000", "");
        }

        public static Int16 CastToShort(this String str)
        {
            if (String.IsNullOrEmpty(str)) return 0;

            try
            {
                return Convert.ToInt16(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Single CastToSingle(this String str)
        {
            if (String.IsNullOrEmpty(str)) return 0;

            try
            {
                return Convert.ToSingle(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static String ToCapitalize(this String str)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(str);
        }

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

        public static Byte[] ToByte(this String str)
        {
            return Encoding.Default.GetBytes(str);
        }

        public static String ToLocalD(this DateTime fecha)
        {
            return fecha.ToLocalTime().ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);
        }

        public static String ToLocalH(this DateTime fecha)
        {
            return fecha.ToLocalTime().ToString("hh:mm tt", CultureInfo.InvariantCulture);
        }

        public static void SetMinDate(this DatePicker picker, DateTime dt)
        {
            var javaMinDt = new DateTime(1970, 1, 1);
            if (dt.CompareTo(javaMinDt) < 0)
                throw new ArgumentException("Must be >= Java's min DateTime of 1/1970");

            var longVal = dt - javaMinDt;
            picker.MinDate = (long)longVal.TotalMilliseconds;
        }

        public static void SetMaxDate(this DatePicker picker, DateTime dt)
        {
            var javaMinDt = new DateTime(1970, 1, 1);
            if (dt.CompareTo(javaMinDt) < 0)
                throw new ArgumentException("Must be >= Java's min DateTime of 1/1970");

            var longVal = dt - javaMinDt;
            picker.MaxDate = (long)longVal.TotalMilliseconds;
        }

        /// <summary>
        /// Metodo para verificar el tipo de Transaccion
        /// </summary>
        /// <param name="tr">Transaccion</param>
        /// <returns>Tipo</returns>
        public static Transactions.Types Get_Type(this Transactions tr, Context context)
        {
            if (tr.Reason == context.GetString(Resource.String.ReceiptConceptReceive))
                return Transactions.Types.Entrega_Material;
            else if (tr.Reason == context.GetString(Resource.String.ReceiptConsumption))
                return Transactions.Types.Consumo_Material;
            else if (tr.Reason == context.GetString(Resource.String.ReceiptReturn))
                return Transactions.Types.Devolucion_Consumo;
            else if (tr.Reason == context.GetString(Resource.String.ReceiptRetiro))
                return Transactions.Types.Devolucion_Buffer;
            else if (tr.Reason == context.GetString(Resource.String.ReceiptClose))
                return Transactions.Types.Cierre_Turno;
            else if (tr.Reason == context.GetString(Resource.String.ReceiptAjust))
                return Transactions.Types.Ajuste_Inventario;
            else return Transactions.Types.Not_Available;
        }
    }
}