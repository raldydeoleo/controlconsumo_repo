using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Config
{
    public class NextConfig
    {
        public String EquipmentID { get; set; }
        public String Equipment { get; set; }
        public Int32 ConfigID { get; set; }       
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String ProductShort { get; set; }
        public String ProductType { get; set; }
        public String ProductReference { get; set; }
        public String ProductUnit { get; set; }
        public Configs._Status Status { get; set; }
        public DateTime Begin { get; set; }
        public Boolean IsCold { get; set; }
        public String Identifier { get; set; }
        public String TimeID { get; set; }
        public Times.ProductTypes Producto { get; set; }
        public DateTime? _Begin
        {
            get
            {
                if (Begin.Year == 1900)
                    return null;
                else
                    return Begin;
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

        public String _DisplayCode { get { return String.IsNullOrEmpty(ProductReference) || ProductReference.Length > 6 ? _ProductCode : ProductReference; } }
    }
}
