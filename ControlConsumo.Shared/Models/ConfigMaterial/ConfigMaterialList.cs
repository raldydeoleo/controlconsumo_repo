using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.ConfigMaterial
{
    public class ConfigMaterialList
    {
        public Int16 ConfigID { get; set; }
        public DateTime Begin { get; set; }        
        public String ProductName { get; set; }
        public String ProductCode { get; set; }
        public String ProductReference { get; set; }
        public String ProductShort { get; set; }      
        public String MaterialName { get; set; }
        public String MaterialCode { get; set; }
        public String MaterialReference { get; set; }
        public String MaterialUnit { get; set; }
        public String Ean { get; set; }
        public String Unit { get; set; }
        public Boolean IsBase { get; set; }
        public Boolean NeedBoxNo { get; set; }
        public Boolean NeedCantidad { get; set; }
        public Boolean AllowLotDay { get; set; }
        public Boolean IgnoreStock { get; set; }
        public Byte ExpireDay { get; set; }
        public Single From { get; set; }
        public Single To { get; set; }

        public String _MaterialName
        {
            get
            {
                try
                {
                    return String.Format("{0} - {1}", _MaterialCode.Trim(), MaterialName.Trim());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public String _MaterialCode
        {
            get
            {
                return ExtensionsMethodsHelper.GetSapCode(MaterialCode);
            }
        }

        public String _ProductName
        {
            get
            {
                try
                {
                    if (!String.IsNullOrEmpty(ProductShort))
                        return String.Format("{0} - {1} - {2}", ProductShort.Trim(), _ProductCode.Trim(), ProductName.Trim());
                    else
                        return String.Format("{0} - {1}", _ProductCode.Trim(), ProductName.Trim());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public String _ProductCode
        {
            get
            {
                return ExtensionsMethodsHelper.GetSapCode(ProductCode);
            }
        }
    }
}
