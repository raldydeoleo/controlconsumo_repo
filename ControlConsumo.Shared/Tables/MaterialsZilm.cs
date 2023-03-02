using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("MaterialsZilm")]
    public class MaterialsZilm
    {
        [NotNull, PrimaryKey, MaxLength(18)]
        public String MaterialCode { get; set; }

        [NotNull, Default(true, 0)]
        public Int16 Days { get; set; }

        [NotNull, Default(true, false)]
        public Boolean DateisRequired { get; set; }

        [NotNull, Default(true, false)]
        public Boolean EtiquetaIs3x1 { get; set; }

        [NotNull, Default(true, false)]
        public Boolean SplitLots { get; set; }

        [NotNull, Default(true, false)]
        public Boolean NeedBoxNo { get; set; }

        [NotNull, Default(true, false)]
        public Boolean Cantidad { get; set; }

        [NotNull, Default(true, false)]
        public Boolean AllowNoLot { get; set; }

        [NotNull, Default(true, false)]
        public Boolean Percent { get; set; }

        [NotNull, Default(true, false)]
        public Boolean IgnoreStock { get; set; }
    }
}
