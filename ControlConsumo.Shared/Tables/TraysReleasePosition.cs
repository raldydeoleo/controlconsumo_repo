using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("TraysReleasePosition")]
    public class TraysReleasePosition
    { 
        [NotNull, Indexed]
        public int TraysReleaseID { get; set; }

        [MaxLength(15), NotNull]
        public String TrayID { get; set; }
    }
}
