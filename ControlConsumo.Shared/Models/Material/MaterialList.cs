using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Material
{
    public class MaterialList
    {       
        public String MaterialName { get; set; }
        public String MaterialCode { get; set; }
        public String MaterialReference { get; set; }
        public String MaterialShort { get; set; }
        public String MaterialUnit { get; set; }
        public Boolean NeedBoxNo { get; set; }
        public Boolean NeedCantidad { get; set; }
        public Boolean AllowLotDay { get; set; }
        public Boolean IgnoreStock { get; set; }
        public Byte ExpireDay { get; set; }        
        public String _MaterialName
        {
            get
            {
                return !String.IsNullOrEmpty(MaterialReference) ? MaterialName.Replace(MaterialReference, "") : MaterialName;
            }
        }

        public List<Units> units { get; set; }

        public String _MaterialCode
        {
            get
            {
                return ExtensionsMethodsHelper.GetSapCode(MaterialCode);
            }
        }

        public String _DisplayCode { get { return String.IsNullOrEmpty(MaterialReference) ? _MaterialCode : MaterialReference; } }
    }
}
