using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.TrayProduct
{
    public class TrayProductRoute
    {
        public enum _Status
        {
            Comunicacion,
            Incorrecto,
            Correcto
        }

        public _Status Result { get; set; }
        public TraysProducts Bandeja { get; set; }
        public ProductsRoutes Traza { get; set; }
        public Consumptions Entrada { get; set; }
    }
}
