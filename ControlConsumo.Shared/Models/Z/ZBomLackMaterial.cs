using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Z
{
    public class ZBomLackMaterial
    {
        public String _Code
        {
            get
            {
                return ExtensionsMethodsHelper.GetSapCode(Code);
            }
        }
        public String Code { get; set; }
        public String Name { get; set; }
        public String Reference { get; set; }
        public String Short { get; set; }
        public String Group { get; set; }
        public String _DisplayCode { get { return String.IsNullOrEmpty(Reference) ? _Code : Reference; } }
        public String _Short { get { return String.IsNullOrEmpty(Short) ? _Code : Short; } }
        public Byte? TurnID { get; set; }
        public DateTime? Produccion { get; set; }
        public String Lot { get; set; }
        public String SupLot { get; set; }
        public Int16? BoxNumber { get; set; }
        public String BatchID { get; set; }
        public Boolean IsObligatory { get; set; }
    }
}
