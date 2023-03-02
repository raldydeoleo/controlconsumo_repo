using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.TraysResultSql
{
    class TraysResultSql
    {
        public string idBandeja { get; set; }
        public int secuenciaInicial { get; set; }
        public int secuenciaFinal { get; set; }
        public bool procesarSap { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuarioRegistro { get; set; }
        public bool estatusVigencia { get; set; }

    }
}
