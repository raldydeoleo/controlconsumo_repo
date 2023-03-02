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
using ControlConsumo.Shared.Models.Material;

namespace ControlConsumo.Droid.Activities.Adapters.Entities
{
    class ReceiptEntry : MaterialList
    {
        public String Lot { get; set; }
        public String LoteSuplidor { get; set; }
        public Single Total { get { return Details.Sum(p => p.Quantity); } }
        public Single Quantity { get { return Details.Count(); } }
        public List<Detail> Details { get; set; }

        public class Detail
        {
            public Single Quantity { get; set; }
            public Int16 BoxNumber { get; set; }
        }
    }
}