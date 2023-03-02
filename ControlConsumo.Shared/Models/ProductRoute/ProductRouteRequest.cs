using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.ProductRoute
{
    public class ProductRouteRequest
    {
        public String MANDT { get; set; }
        public String IDPROCESS { get; set; }
        public String IDTIEMPO { get; set; }
        public Int64 IDREGISTRO { get; set; }
        public String MJAHR { get; set; }
        public String IDEQUIPO { get; set; }
        public String WERKS { get; set; }
        public String MATNR { get; set; }
        public String VERID { get; set; }
        public String CHARG { get; set; }
        public String LOTEMAN { get; set; }
        public Int16 SECSALIDA { get; set; }
        public String FECHA { get; set; }
        public String HORA { get; set; }
        public Byte IDTURNO { get; set; }
        public String BATCHID { get; set; }
        public String IDBANDEJA { get; set; }
        public Int16 SECEMPAQUE { get; set; }
        public String IDEMPAQUE { get; set; }
        public Single MENGE { get; set; }
        public String MEINS { get; set; }
        public Single MENGE2 { get; set; }
        public Byte STATUSBAN { get; set; }
        public String MBLNR { get; set; }
        public String BUDAT { get; set; }
        public String USNAM { get; set; }
        public String CPUDT { get; set; }
        public String CPUTM { get; set; }
        public String CPUDT2 { get; set; }
        public String CPUTM2 { get; set; }
        public String CPUDT3 { get; set; }
        public String CPUTM3 { get; set; }
        public String IDTIEMPO2 { get; set; }
        public Int64 IDREGISTRO2 { get; set; }
        public String MJAHR2 { get; set; }
        public DateTime GetLastDate
        {
            get
            {
                try
                {
                    return Repositories.RepositoryBase.GetDatetime(CPUDT3, CPUTM3).Value;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
