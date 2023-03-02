using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlConsumo.Service.Tables
{
    public class ReportResult
    {
        public IEnumerable<DetailsResult> Details { get; set; }
        public IEnumerable<TransactionResult> Transactions { get; set; }
        public IEnumerable<ConsumoDeMateriales> Consumos { get; set; }

        public class ConsumoDeMateriales
        {
            public String MaterialCode { get; set; }
            public String Lot { get; set; }
            public String IdEquipo { get; set; }
            public Int64 BoxNumber { get; set; }
            public Double Quantity { get; set; }
        }

        public class DetailsResult
        {
            public String ProductID { get; set; }
            public String TypeID { get; set; }
            public String SubTypeID { get; set; }
            public String ParametroID { get; set; }
            public Decimal Value { get; set; }
            public Boolean IsVisible { get; set; }
        }

        public class TransactionResult
        {
            public Double Value { get; set; }
            public Double ValueRange { get; set; }
            public Int64 Tick { get; set; }
            public List<Single> Values { get; set; }           
        }
    }
}
