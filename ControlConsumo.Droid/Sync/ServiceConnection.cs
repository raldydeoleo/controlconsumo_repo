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
using ControlConsumo.Droid.Activities;

namespace ControlConsumo.Droid.Sync
{
    class ServiceConnection : Java.Lang.Object, IServiceConnection
    {
        private MenuActivity activity;

        public ServiceConnection(MenuActivity activity)
        {
            this.activity = activity;
        }

        public ServiceBinder binder { get; private set; }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var binder = service as ServiceBinder;

            if (binder != null)
            {
                activity.binder = binder;
                this.binder = binder;
                activity.BindEventos();           
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            activity.UnBindEventos();
        }

    }
}