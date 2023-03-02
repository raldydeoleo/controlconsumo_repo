using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Times")]
    public class Times
    {
        public enum ProductTypes
        {
            None,
            Validar_Salida,
            Validar_Tipo_Almacenamiento,
            Validar_Salida_y_Tipo_Almacenamiento
        }

        [NotNull, PrimaryKey, MaxLength(2)]
        public String ID { get; set; }

        [NotNull, MaxLength(20)]
        public String Time { get; set; }

        [NotNull, Default(true)]
        public Byte Min { get; set; }

        [NotNull]
        public Boolean Valid_Out { get; set; }

        [NotNull]
        public ProductTypes Producto { get; set; }

        [NotNull]
        public Byte Copias { get; set; }

        [MaxLength(5)]
        public String Group { get; set; }
    }
}
