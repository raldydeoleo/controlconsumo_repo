using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Lots")]
    public class Lots
    {
        [NotNull, MaxLength(18)]
        public String MaterialCode { get; set; }

        [NotNull, MaxLength(10)]
        public String Code { get; set; }

        [MaxLength(15)]
        public String Reference { get; set; }

        [MaxLength(5)]
        public String Center { get; set; }

        public DateTime Expire { get; set; }

        public DateTime LastReceived { get; set; }

        [Ignore]
        public DateTime Created { get; set; }

        [Ignore]
        public DateTime? Updated { get; set; }
    }
}
