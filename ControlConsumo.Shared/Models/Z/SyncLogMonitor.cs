using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Z
{
    public class SyncLogMonitor
    {
        public SyncLogMonitor()
        {
            Detalle = new List<Detail>();
        }

        public Int64 TotalRegistrosBajada { get; set; }
        public Int64 TotalRegistrosSubida { get; set; }
        public Int64 TotalRegistros { get { return TotalRegistrosBajada + TotalRegistrosSubida; } }
        public Int64 TotalSizeBajada { get; set; }
        public Int64 TotalSizeSubida { get; set; }
        public Int64 TotalSize { get { return TotalSizeBajada + TotalSizeSubida; } }
        public List<Detail> Detalle { get; set; }

        public class Detail
        {
            public Detail()
            {
                Salida = new SalidaDetail();
            }

            public Syncro.Tables Tabla { get; set; }
            public Int64 RegistrosBajada { get; set; }
            public Int64 RegistrosSubida { get; set; }
            public Int64 RegistrosTotal { get { return RegistrosBajada + RegistrosSubida; } }
            public Int64 SizeBajada { get; set; }
            public Int64 SizeSubida { get; set; }
            public Int64 SizeTotal { get { return SizeBajada + SizeSubida; } }
            public DateTime Fecha { get; set; }
            public SalidaDetail Salida { get; set; }
        }

        public class SalidaDetail
        {
            public SalidaDetail()
            {
                Ids = new List<Int32>();
            }
            public Boolean IsOK { get; set; }
            public Exception  ex { get; set; }
            public Int64 SecAlta { get; set; }
            public Int64 SecBaja { get; set; }
            public List<Int32> Ids { get; set; }
            public HttpStatusCode Status { get; set; }
            public Boolean Updated { get; set; }
            public String Html { get; set; }
        }
    }
}
