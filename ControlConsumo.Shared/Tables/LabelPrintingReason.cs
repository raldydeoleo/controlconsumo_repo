using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("LabelPrintingReason")]
    public class LabelPrintingReason
    {
        [PrimaryKey, NotNull]
        public Byte ID { get; set; }

        [NotNull, MaxLength(255)]
        public String Description { get; set; }

        [NotNull, Default(true, true)]
        public Boolean Active { get; set; }
    }
}
