using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Areas")]
    public class Areas
    {
        public enum Status
        {
            Inactiva = 0,
            Activa = 1,          
        }

        [NotNull, PrimaryKey]
        public Byte ID { get; set; }

        [NotNull, Default(true, 0)]
        public Status status { get; set; }

        [NotNull, MaxLength(30)]
        public String Name { get; set; }
    }
}
