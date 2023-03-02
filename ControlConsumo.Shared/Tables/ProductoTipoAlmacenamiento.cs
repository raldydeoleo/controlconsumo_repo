using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("ProductoTipoAlmacenamiento")]
    public class ProductoTipoAlmacenamiento
    {
        [NotNull, PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [NotNull]
        public int idTipoProductoTerminado { get; set; }

        [NotNull]
        public int idTipoAlmacenamientoProducto { get; set; }

        [NotNull, MaxLength(10)]
        public String identificador { get; set; }

        [NotNull, Default(true, false)]
        public bool estatus { get; set; }
    }
}
