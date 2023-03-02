using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("StocksDetails")]
    public class StocksDetails
    {
        [NotNull, PrimaryKey, AutoIncrement]
        public Int32 ID { get; set; }

        [NotNull, Indexed]
        public Int32 StockID { get; set; }

        [NotNull, MaxLength(18)]
        public String MaterialCode { get; set; }
        
        [MaxLength(10)]
        public String Lot { get; set; }

        [NotNull, Default(true, 0)]
        public Int16 BoxNumber { get; set; }

        [NotNull, Default(true, 0)]
        public Single Quantity { get; set; }

        [NotNull, Default(true, 0)]
        public Single Quantity2 { get; set; }

        [MaxLength(3)]
        public String Unit { get; set; }
    }
}
