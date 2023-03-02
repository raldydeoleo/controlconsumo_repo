using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Material
{
    class MaterialsResult
    {
        public string matnr { get; set; }
        public string bismt { get; set; }
        public string maktx { get; set; }
        public string normt { get; set; }
        public string meins { get; set; }
        public string laeda { get; set; }
        public string ersda { get; set; }
        public string matkl { get; set; }
        public string formt { get; set; }
        public List<Unit> units { get; set; }
        public List<Category> clasificaciones { get; set; }
        public string _bismt { get { return String.IsNullOrEmpty(bismt) ? null : bismt; } }
        public string _date
        {
            get
            {
                return Convert.ToInt32(laeda) == 0 ? ersda : laeda;
            }
        }

        public class Unit
        {
            public string meinh { get; set; }
            public int umren { get; set; }
            public int umrez { get; set; }
            public string ean11 { get; set; }
            public string _ean11 { get { return String.IsNullOrEmpty(ean11) ? null : ean11; } }
        }

        public class Category
        {
            public Int32 ATINN { get; set; }
            public string ATWRT { get; set; }
            public string ATNAM { get; set; }
        }
    }
}
