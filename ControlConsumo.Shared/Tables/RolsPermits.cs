using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsumo.Shared.Tables
{
    [Table("RolsPermits")]
    public class RolsPermits
    {
        public enum Permits
        {
            NONE = 0,
            CONFIGURACION = 1,
            PLANIFICACION = 2,
            ASIGNACION = 3,
            OPERACION = 4,
            LIBERACION = 5,
            REPORTES = 6,
            DEVOLUCION = 7,
            CIERRES = 8,
            REPORTAR_VARILLAS = 9,
            AUTORIZAR_VENCIDOS = 10,
            ENTREGA_MATERIALES = 11,
            DEVOLUCION_PRODUCTO = 12,
            BANDEJAS_EN_CONTIGENCIA = 13,
            ACTUALIZAR_BANDEJAS = 14,
            REIMPRIMIR_ETIQUETAS = 15,
            CONSUMIR_BANDEJAS = 16,
            REIMPRIMIR_REIMPRESION_ETIQUETAS = 17,
            AUTORIZAR_SALIDA_PARCIAL = 18,
            GENERAL_REPORTS = 97,
            QUALITY_SCREEN = 98,           
            CHANGE_PASS = 99
        }              

        [NotNull, Indexed]
        public short RolID { get; set; }

        [NotNull]
        public Permits Permit { get; set; }
    }
}
