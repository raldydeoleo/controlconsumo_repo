using ControlConsumo.Shared.Tables;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Models.Config
{
    public class ConfigList
    {       
        public String Material { get; set; }
        public String Short { get; set; }
        public String Code { get; set; }
        public String Version { get; set; }
        public String TimeID { get; set; }
        public String _Code
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Code).ToString();
                }
                catch (Exception)
                {
                    return Code;
                }
            }
        }
    }
}
