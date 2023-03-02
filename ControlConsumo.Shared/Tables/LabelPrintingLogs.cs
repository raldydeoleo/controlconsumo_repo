using ControlConsumo.Shared.Models.Z;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("LabelPrintingLogs")]
    public class LabelPrintingLogs
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public int ID { get; set; }

        [NotNull, MaxLength(10)]
        public String EquipmentID { get; set; }

        [NotNull]
        public Int32 PackSequence { get; set; }

        [NotNull]
        public int LabelPrintingReasonID { get; set; }

        [NotNull]
        public int Quantity { get; set; }

        [MaxLength(10)]
        public String Identifier { get; set; }

        [MaxLength(15)]
        public String PackID { get; set; }

        [NotNull]
        public Int32 CustomFecha { get; set; }

        [NotNull, Indexed]
        public DateTime ProductionDate { get; set; }

        [NotNull, Default(true, 0)]
        public Byte TurnID { get; set; }

        [NotNull, Indexed]
        public DateTime LabelReprintedDate { get; set; }

        [NotNull, MaxLength(12)]
        public String User { get; set; }

        [NotNull, Default(true, true), Indexed]
        public Boolean SyncSQL { get; set; }
    }
}
