using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Turn
{
    class TurnsResult
    {
        public string mandt { get; set; }
        public int idturno { get; set; }
        public string descripcion { get; set; }
        public string etiqueta { get; set; }
        public string empaque { get; set; }
        public int inicio { get; set; }
        public int fin { get; set; }
        public int minutosinicio { get; set; }
        public int minutosfin { get; set; }
        public String _HoraInicio
        {
            get
            {
                return String.Format("{0}{1}00", inicio.ToString("00"), minutosinicio.ToString("00"));
            }
        }
        public String _HoraFin
        {
            get
            {
                return String.Format("{0}{1}00", fin.ToString("00"), minutosfin.ToString("00"));
            }
        }
    }
}
