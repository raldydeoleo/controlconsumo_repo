using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Categories")]
    public class Categories
    {
        public enum TypesCategories
        {
            IPDM_NONE = 0,
            IPDM_IPACK = 1,
            IPDM_PCKSC = 2,
            IPDM_PCKCT = 3,
            IPDM_TALMA = 4
        }

        [NotNull, PrimaryKey]
        public String Key { get; set; }

        [NotNull, Indexed, MaxLength(18)]
        public String MaterialCode { get; set; }

        [NotNull]
        public TypesCategories Category { get; set; }

        [NotNull, MaxLength(30)]
        public String Value { get; set; }

        [Ignore]
        public Single _Value { get { return Value.ToNumeric(); } }
    }
}
