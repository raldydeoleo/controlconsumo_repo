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

namespace ControlConsumo.Droid
{
    public class BarCodeResult
    {
        public String ID
        {
            get
            {
                return String.Concat(BarCode, Sequence.ToString("00000"));
            }
        }
        public String FullBarCode { get; set; }
        public String Ean
        {
            get
            {
                try
                {
                    return Convert.ToInt64(FullBarCode).ToString();
                }
                catch (Exception)
                {
                    return String.Empty;
                }
            }
        }
        public Boolean IsLotInternal { get; set; }
        public Boolean IsCustom { get; set; }
        public String BarCode { get; set; }
        public String Lot { get; set; }
        public Single Quantity { get; set; }
        public Int16 Sequence { get; set; }
        public DateTime? Expired { get; set; }
        public DateTime? Fecha
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(Lot.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}