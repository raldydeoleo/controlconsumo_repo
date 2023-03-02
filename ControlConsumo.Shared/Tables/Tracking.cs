using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Tracking")]
    public class Tracking
    {
        [NotNull, PrimaryKey, AutoIncrement]
        public Int32 ID { get; set; }

        [NotNull]
        public DateTime FechaConsumption { get; set; }

        [NotNull, Indexed]
        public Int16 ConsumptionID { get; set; }

        [NotNull]
        public DateTime FechaElaborate { get; set; }

        [NotNull]
        public Int16 ElaborateID { get; set; }

        [NotNull]
        public Boolean Sync { get; set; }

        [NotNull]
        public Boolean SyncSQL { get; set; }

        [Ignore]
        public Boolean WasNull { get; set; }

        [Ignore]
        public Int32 Count { get; set; }
    }
}
