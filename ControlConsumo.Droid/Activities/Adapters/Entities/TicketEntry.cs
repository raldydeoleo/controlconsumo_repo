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
using ControlConsumo.Shared.Models.R;

namespace ControlConsumo.Droid.Activities.Adapters.Entities
{
    class TicketEntry : MaterialList
    {
        public enum Fields
        {
            HeaderTitle,
            Header,
            DetailTitle,
            Detail,
            Divider
        }

        public Fields Field { get; set; }
        public String ProductCode { get; set; }
        public String ProductName { get; set; }
        public String ProductShort { get; set; }
        public String VerID { get; set; }
        public String Lot { get; set; }
        public Int16 SecSalida { get; set; }
        public DateTime? Produccion { get; set; }
        public Byte TurnID { get; set; }
        public String TrayID { get; set; }
        public String BatchID { get; set; }
        public String Status { get; set; }
        public Single Quantity { get; set; }
        public String Unit { get; set; }
        public Single Quantity2 { get; set; }
        public String Traza { get; set; }
        public String EquipmentID { get; set; }
        public String TimeID { get; set; }
        public Int16 BoxNo { get; set; }
        public DateTime? Fecha { get; set; }
        public String EquipmentID3 { get; set; }
        public DateTime? Fecha2 { get; set; }
        public String LotReference { get; set; }
        public Int32 Level { get; set; }     
        public Boolean _IsSemiElaborate { get { return String.IsNullOrEmpty(Lot); } }
        public Boolean IsNotShow { get; set; }
    }
}