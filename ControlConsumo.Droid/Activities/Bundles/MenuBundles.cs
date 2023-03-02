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
using ControlConsumo.Shared.Tables;
using ControlConsumo.Shared.Models.Material;
using ControlConsumo.Shared.Models.Lot;
using ControlConsumo.Shared.Models.Config;

namespace ControlConsumo.Droid.Activities.Bundles
{
    public class MenuBundles
    {
        public Boolean isEventReady { get; set; }
        public String ProductCode { get; set; }
        public String VerID { get; set; }
        public Int16 MaxMinutes { get; set; }
        public Byte Copies { get; set; }
        public String MacAddress { get; set; }
        public Stocks Stock { get; set; }
        public IEnumerable<Turns> turnos { get; set; }
        public IEnumerable<Trays> bandejas { get; set; }
        public IEnumerable<MaterialList> Materials { get; set; }
        public IEnumerable<LotsList> Batches { get; set; }
        public IEnumerable<NextConfig> Configs { get; set; }
    }
}