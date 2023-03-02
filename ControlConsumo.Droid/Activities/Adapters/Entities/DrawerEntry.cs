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

namespace ControlConsumo.Droid.Activities.Adapters.Entities
{
    class DrawerEntry
    {
        public readonly Type activity;
        public readonly int Icon;
        public readonly String Description;
        public readonly RolsPermits.Permits Permit;

        public DrawerEntry(int Icon, String Description, Type activity, RolsPermits.Permits Permit)
        {
            this.Permit = Permit;
            this.Icon = Icon;
            this.Description = Description;
            this.activity = activity;            
        }
    }
}