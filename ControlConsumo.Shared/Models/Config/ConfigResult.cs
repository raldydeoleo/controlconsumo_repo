using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Config
{
    class ConfigResult
    {
        public string mandt { get; set; }
        public string idprocess { get; set; }
        public string idequipo { get; set; }
        public string fecha { get; set; }
        public string horaMin { get; set; }
        public string horaEje { get; set; }
        public string werks { get; set; }
        public string idtiempo { get; set; }
        public string matnr { get; set; }
        public string verid { get; set; }
        public string idequipo2 { get; set; }
        public int zstatus { get; set; }
        public string usnam1 { get; set; }
        public string cpudt1 { get; set; }
        public string cputm1 { get; set; }
        public string usnam2 { get; set; }
        public string cpudt2 { get; set; }
        public string cputm2 { get; set; }
        public string cold { get; set; }

        public string _matnr
        {
            get { return ExtensionsMethodsHelper.GetSapCode(matnr); }
        }

        public DateTime? GetLastDate
        {
            get
            {
                try
                {
                    if (cpudt2 != null && Convert.ToInt32(cpudt2.Replace("-", "")) > 0)
                    {
                        if (Repositories.RepositoryBase.GetDatetime(cpudt1, cputm1) < Repositories.RepositoryBase.GetDatetime(cpudt2, cputm2))
                            return Repositories.RepositoryBase.GetDatetime(cpudt2, cputm2);
                        else
                            return Repositories.RepositoryBase.GetDatetime(cpudt1, cputm1);
                    }
                    else
                    {
                        return Repositories.RepositoryBase.GetDatetime(cpudt1, cputm1);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
