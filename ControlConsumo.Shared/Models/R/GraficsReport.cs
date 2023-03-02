using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.R
{
    public class GraficsReport
    {
        public GraficsReport()
        {
            Details = new List<DetailsResult>();
            Transactions = new List<TransactionResult>();
            Materiales = new List<MaterialResult>();
        }

        public IEnumerable<DetailsResult> Details { get; set; }
        public IEnumerable<TransactionResult> Transactions { get; set; }
        public IEnumerable<MaterialResult> Materiales { get; set; }

        public class MaterialResult
        {
            public String IdEquipo { get; set; }            
            public String MaterialCode { get; set; }
            public String Lot { get; set; }
            public Single Quantity { get; set; }
            public Int64 BoxNumber { get; set; }
        }

        public class DetailsResult
        {
            public String ProductID { get; set; }
            public String TypeID { get; set; }
            public String SubTypeID { get; set; }
            public String ParametroID { get; set; }
            public Single Value { get; set; }
            public Boolean IsVisible { get; set; }
        }

        public class TransactionResult
        {
            public Single Value { get; set; }
            public Single ValueRange { get; set; }
            public Int64 Tick { get; set; }
            public DateTime Fecha { get { return new DateTime(Tick); } }
            public List<Single> Values { get; set; }
        }
    }
}
