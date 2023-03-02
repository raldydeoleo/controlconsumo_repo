using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.User
{
    class UsersResult
    {
        public string mandt { get; set; }
        public string bname { get; set; }
        public int pernr { get; set; }
        public string name1 { get; set; }
        public int znorol { get; set; }
        public string cpudt { get; set; }
        public string cputm { get; set; }
        public string usnam { get; set; }
        public string gltgb { get; set; }
        public int uflag { get; set; }
        public string newcode { get; set; }
        public DateTime? Fecha
        {
            get
            {
                if (Repositories.RepositoryBase.GetDatetime(cpudt, cputm).HasValue)
                    return Repositories.RepositoryBase.GetDatetime(cpudt, cputm);
                else
                    return null;
            }
        }
    }
}
