using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ControlConsumo.Service.Managers
{
    public class Util
    {
        public static DateTime? GetDatetime(String date, String time = null)
        {
            try
            {
                if (Convert.ToInt32(date) == 0)
                {
                    return null;
                }
                else if (time != null && Convert.ToInt32(time) > 0)
                {
                    String fecha = String.Concat(date, " ", time);
                    return DateTime.ParseExact(fecha, "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);
                }
                else
                {
                    return DateTime.ParseExact(date.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}