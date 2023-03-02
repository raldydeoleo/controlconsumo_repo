using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Syncro")]
    public class Syncro
    {
        public enum Tables
        {
            TraysProducts = 0,
            MaterialZilms = 1,
            Equipments = 2,
            Trays = 3,
            TraysTimes = 4,
            Rols = 5,
            Areas = 6,
            Users = 7,
            Lots = 8,
            Materials = 9,
            Configs = 10,
            Stocks = 11,
            MaterialsProcess = 12,
            ProductsRoutes = 13,
            Consumptions = 14,
            Elaborates = 15,
            ConfigMaterials = 16,
            Errors = 17,
            Inventories = 18,
            RolsPermit = 19,
            Times = 20,
            Tracking = 21,
            TrayRelease = 22,
            Wastes = 23,
            ConfiguracionTiempoSalida=24,
            TipoProductoTerminado = 25,
            TipoAlmacenamientoProducto = 26,
            ProductoTipoAlmacenamiento = 27,
            LabelPrintingLog = 28
        }

        [PrimaryKey, NotNull, MaxLength(30)]
        public Tables Tabla { get; set; }

        [NotNull]
        public DateTime LastSync { get; set; }

        [NotNull, Default(true, false)]
        public Boolean Sync { get; set; }

        [NotNull, Default(true, false)]
        public Boolean IsDaily { get; set; }
    }
}
