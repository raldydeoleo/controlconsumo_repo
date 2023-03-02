using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("Settings")]
    public class Settings
    {
        public enum Params
        {
            IsSynchronized = 0,
            Werks = 1,
            WerksName = 2,
            Process = 3,
            ProcessName = 4,
            IsEquipment = 5,
            ConfigID = 6,
            ConfigName = 7,
            ConfigIsActive = 8,
            LastDailyUpdate = 9,
            IsLast = 10,
            NeedEan = 11,
            PrinterAddress = 12,
            PrinterCopies = 13,
            NeedGramo = 14,
            IsSubEquipment = 15,
            BatchID = 16,
            FechaUltimoMantenimiento = 17,
            TurnoUltimoMantenimiento = 18,
            AllComponentAreInside = 19,
            EquipmentSynced = 20,
            PrinterName = 21,
            Second_Equipment = 22,
            PackID = 23,
            Planificacion_Automatica = 24,
            IsInputOutputControlActive = 25,
            IsPartialElaborateAuthorized = 26,
            PrinterConnectivity = 27
        }

        [NotNull, PrimaryKey]
        public Params Key { get; set; }

        [NotNull, MaxLength(150)]
        public String Value { get; set; }
    }
}
