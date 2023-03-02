using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("MaterialsProcess")]
    public class MaterialsProcess
    {
        [NotNull, MaxLength(2)]
        public String TimeID { get; set; }

        [NotNull, MaxLength(18)]
        public String ProductCode { get; set; }      
    }
}
