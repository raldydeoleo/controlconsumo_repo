using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Users")]
    public class Users
    {
        [PrimaryKey, NotNull, MaxLength(12)]
        public String Logon { get; set; }

        [NotNull, MaxLength(40)]
        public String Password { get; set; }

        [NotNull, Default(true, true)]
        public Boolean IsActive { get; set; }

        [NotNull, MaxLength(10)]
        public String Code { get; set; }

        [NotNull, MaxLength(30)]
        public String Name { get; set; }

        [NotNull]
        public Int16 RolID { get; set; }

        public DateTime? Expire { get; set; }

        [NotNull, Default(true, false)]
        public Boolean Sync { get; set; }

        [Ignore]
        public IEnumerable<RolsPermits> Permisos { get; set; }
    }
}
