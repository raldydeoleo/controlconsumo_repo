using ControlConsumo.Shared.Models.Material;
using ControlConsumo.Shared.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Config
{
    public class ActualConfig : NextConfig
    {
        public String TrayID { get; set; } //AGREGADA PROPIEDAD BANDEJA
        public String Unit { get; set; }
        public String VerID { get; set; }    
        public String SubEquipment { get; set; }
        public String SubEquipmentID { get; set; }
        public Byte Copias { get; set; }
        public String _ProductName2
        {
            get
            {
                try
                {
                    if (!String.IsNullOrEmpty(ProductReference) && ProductReference.Length == 6)
                        return String.Format("{0}   {1}   {2}", ProductReference.Trim(), _ProductCode.Trim(), ProductName.Trim());
                    else
                        return String.Format("{0}   {1}", _ProductCode.Trim(), ProductName.Trim());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public IEnumerable<Units> units { get; set; }
    }
}
