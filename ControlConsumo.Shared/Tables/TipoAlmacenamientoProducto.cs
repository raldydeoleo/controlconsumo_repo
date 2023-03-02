using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("TipoAlmacenamientoProducto")]
    public class TipoAlmacenamientoProducto
    {
        [NotNull, PrimaryKey]
        public int id { get; set; }

        [NotNull, MaxLength(50)]
        public String nombre { get; set; }
        
        [NotNull, Default(true, false)]
        public bool estatus { get; set; }
    }
}