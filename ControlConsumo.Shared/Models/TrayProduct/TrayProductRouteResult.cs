using ControlConsumo.Shared.Models.Consumption;
using ControlConsumo.Shared.Models.ProductRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.TrayProduct
{
    class TrayProductRouteResult
    {
        public String Type { get; set; }
        public TraysProductsResult Bandeja { get; set; }
        public ProductRouteRequest traza { get; set; }
        public ConsumptionsRequest Entrada { get; set; }
    }
}
