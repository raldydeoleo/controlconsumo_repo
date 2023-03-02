using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.ConfigMaterial
{
    class ConfigMaterialResult
    {
        public string _Key { get {return String.Format("{0}-{1}", matnr, idnrk); } }
        public string mandt { get; set; }
        public string matnr { get; set; }
        public string werks { get; set; }
        public string verid { get; set; }
        public string prdat { get; set; }
        public int bdatu { get; set; }
        //public string stlnr { get; set; }
        //public string bmein { get; set; }
        //public float bmeng { get; set; }
        //public int stlkn { get; set; }
        //public int stpoz { get; set; }
        //public int vgknt { get; set; }
        //public int vgpzl { get; set; }
        public string idnrk { get; set; }
        public string postp { get; set; }
        public string posnr { get; set; }
        public string meins { get; set; }
        public string menge { get; set; }

        public Single GetMenge
        {
            get
            {
                try
                {
                    if (menge.Contains("-"))
                    {
                        var value = menge.Replace("-", "");
                        return Convert.ToSingle(value) * -1;
                    }
                    else
                    {
                        return Convert.ToSingle(menge.Replace(",",""));
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}
