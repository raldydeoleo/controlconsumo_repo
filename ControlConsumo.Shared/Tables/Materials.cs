using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Materials")]
    public class Materials
    {
        [NotNull, MaxLength(18), PrimaryKey]
        public String Code { get; set; }

        [Ignore]
        public String _Code
        {
            get
            {
                return ExtensionsMethodsHelper.GetSapCode(Code);
            }
        }

        [MaxLength(18), Indexed]
        public String Reference { get; set; }

        [NotNull, MaxLength(40)]
        public String Name { get; set; }

        [MaxLength(18)]
        public String Short { get; set; }

        [MaxLength(3)]
        public String Unit { get; set; }

        [MaxLength(5)]
        public String Group { get; set; }

        //Atributo que indica el tipo de producto. Ej: Prime, Grand Father, Modify
        [MaxLength(5)]
        public String ProductType { get; set; }

        [Ignore]
        public String _ProductName
        {
            get
            {
                if (!String.IsNullOrEmpty(Short))
                    return String.Format("{0} - {1} - {2}", Short, Name, _Code);
                else
                    return String.Format("{0} - {1}", Name, _Code);
            }
        }

        [Ignore]
        public String _DisplayCode { get { return String.IsNullOrEmpty(Reference) ? _Code : Reference; } }

        [Ignore]
        public String _Short { get { return String.IsNullOrEmpty(Short) ? _Code : Short; } }

        [Ignore]
        public List<Units> units { get; set; }

        [Ignore]
        public List<Categories> categories { get; set; }
    }
}
