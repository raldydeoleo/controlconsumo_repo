using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.MaterialZilm
{
    class MaterialsZilmResult
    {
        public string mandt { get; set; }
        public string matnr { get; set; }
        public int dias { get; set; }
        public string fecha { get; set; }
        public string etiqueta3x1 { get; set; }
        public string splitlote { get; set; }
        public string nocaja { get; set; }
        public string cantidad { get; set; }
        public string ignoreloteofday { get; set; }
        public string percent { get; set; }
        public string ignorestock { get; set; }
    }
}
