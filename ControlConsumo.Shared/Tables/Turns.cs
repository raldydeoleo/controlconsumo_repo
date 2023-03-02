using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Turns")]
    public class Turns
    {
        [NotNull, PrimaryKey]
        public Byte ID { get; set; }

        [NotNull, MaxLength(30)]
        public String Name { get; set; }

        [NotNull, MaxLength(1)]
        public String Etiqueta { get; set; }

        [NotNull, MaxLength(1)]
        public String Empaque { get; set; }

        [NotNull]
        public DateTime Begin { get; set; }

        [NotNull]
        public DateTime End { get; set; }

        [Ignore]
        public Int16 BeginH
        {
            get
            {
                return Convert.ToInt16(Begin.ToLocalTime().ToString("HH"));
            }
        }

        [Ignore]
        public Int16 EndH
        {
            get
            {
                return Convert.ToInt16(End.ToLocalTime().ToString("HH"));
            }
        }

        [Ignore]
        public Int32 WorkHour
        {
            get
            {
                var TotalWorkHour = End.Subtract(Begin).Hours + 1;

                if (TotalWorkHour < 0)
                {
                    TotalWorkHour = End.AddDays(1).Subtract(Begin).Hours + 1;
                }

                return TotalWorkHour;
            }
        }
    }
}
