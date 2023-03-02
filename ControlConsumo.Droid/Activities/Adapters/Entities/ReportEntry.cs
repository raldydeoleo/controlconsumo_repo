using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ControlConsumo.Droid.Activities.Adapters.Entities
{
    class ReportEntry
    {
        public enum EntryTypes
        {
            Entrada,
            Salida,
            Inventario,
            InventarioResumen,
            ReporteTickets,
            ReporteProduction,
            ReportedeBom
        }

        public int Imagen { get; set; }
        public String Title { get; set; }
        public EntryTypes Type { get; set; }
    }
}