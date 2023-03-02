using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.TrayProduct
{
    public class TraysProductsResult
    {
        public String mandt { get; set; }
        public String idbandeja { get; set; }
        public Int32 zsecuencia { get; set; }
        public String status { get; set; }
        public String idtiempo { get; set; }
        public String idprocess { get; set; }
        public String IDEQUIPO { get; set; }
        public Int16 SECSALIDA { get; set; }
        public String BATCHID { get; set; }
        public String FECHA { get; set; }
        public String HORA { get; set; }
        public String UsuarioLlenada { get; set; }
        public String matnr { get; set; }
        public String verid { get; set; }
        public Single menge { get; set; }
        public String meins { get; set; }
        public String cpudt { get; set; }
        public String cputm { get; set; }
        public String FechaVaciada { get; set; }
        public String HoraVaciada { get; set; }
        public String UsuarioVaciada { get; set; }
        public String IdEquipoVaciado { get; set; }
        public string formaVaciada { get; set; }
        public Boolean esUnaDevolucion { get; set; }
    }
}
