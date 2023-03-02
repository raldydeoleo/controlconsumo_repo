using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ControlConsumo.Droid.Managers
{
    class Etiquetas
    {
        public String Material { get; set; }
        public String Codigo { get; set; }
        public String Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Medida { get; set; }
        public String Unidad { get; set; }
        public String LoteInterno { get; set; }
        public String LoteSuplidor { get; set; }
        public Decimal Procesado { get { return Cantidad * Medida; } }
        public Int32 Secuencia { get; set; }

        public String _Medida
        {
            get
            {
                return Medida.ToString("N3").Replace(".000", "");
            }
        }

        public String _LoteInterno
        {
            get
            {
                if (LoteInterno.Length == 10 && Helpers.IsNumeric(LoteInterno))
                    return string.Concat(LoteInterno.Substring(0, 6), "-", LoteInterno.Substring(6, 4));
                else
                    return LoteInterno;
            }
        }

        public String _Descripcion
        {
            get
            {
                if (!String.IsNullOrEmpty(Codigo) && Descripcion.Contains(Codigo))
                    return Descripcion.Replace(Codigo, "").Trim();
                else
                    return Descripcion;
            }
        }

        public String _Material
        {
            get
            {
                if (!String.IsNullOrEmpty(Codigo))
                    return String.Concat(Codigo, " / ", Material);
                else
                    return Material;
            }
        }

        public String CodigoBarra
        {
            get
            {
                // return String.Format("{0}-{1}-{2}-{3}", Codigo, LoteSuplidor ?? LoteInterno, Medida, Fecha.HasValue ? Fecha.Value.ToString("ddMMyyyy") : "000000");
                if (Secuencia == 0)
                    return String.Format("{0}-{1}-{2}", String.IsNullOrEmpty(Codigo) ? Material : Codigo, LoteInterno, _Medida);
                else
                    return String.Format("{0}-{1}-{2}-{3}", String.IsNullOrEmpty(Codigo) ? Material : Codigo, LoteInterno, _Medida, Secuencia.ToString("00000"));
            }
        }
    }
}