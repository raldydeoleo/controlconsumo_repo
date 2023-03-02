using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    /// <summary>
    /// Tabla para controlar las entradas de Consumo en el equipo para Cachearlo.
    /// </summary>
    [Table("CustomSecuences")]
    public class CustomSecuences
    {
        [NotNull, MaxLength(18), PrimaryKey]
        public String MaterialCode { get; set; }

        [NotNull, Indexed]
        public Int32 CustomFecha { get; set; }

        [NotNull]
        public DateTime Fecha { get; set; }

        [NotNull]
        public Int16 ConsumptionID { get; set; }

        [NotNull]
        public DateTime Fecha2 { get; set; }

        [NotNull]
        public Int16 ElaborateID { get; set; }

        [NotNull, Default(true, false)]
        public Boolean HasChanged { get; set; }

        [Ignore] // Para Saber si el registro viene de la BD y ejecutar el ToLocalTime
        public Boolean IsMemoryCreated { get; set; }

        [Ignore] ///Para Solvertar el problema de las 4 Horas con LocalTime.
        public DateTime _Fecha { get { return IsMemoryCreated ? Fecha : Fecha.ToLocalTime(); } }

        [Ignore] ///Para Solvertar el problema de las 4 Horas con LocalTime.
        public DateTime _Fecha2 { get { return IsMemoryCreated ? Fecha2 : Fecha2.ToLocalTime(); } }         
    }
}
