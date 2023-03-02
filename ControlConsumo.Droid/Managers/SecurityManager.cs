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
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Shared.Models.Process;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Droid.Activities.Adapters.Entities;

namespace ControlConsumo.Droid.Managers
{
    /// <summary>
    /// Clase para manejar la Seguridad de la Aplicacion
    /// </summary>
    class SecurityManager
    {
        private readonly Context context;
        private readonly RepositoryZ Repoz;
        public static ProcessList CurrentProcess { get; private set; }
        private Users User;
        private DrawerEntry Entry;

        public delegate void ResponseHandler(Boolean IsAutorize, DrawerEntry Entry);
        public event ResponseHandler Response;

        public SecurityManager(Context context)
        {
            this.context = context;
            Repoz = new RepositoryZ(Util.GetConnection());
            LoadUser();
        }

        private async void LoadUser()
        {
            var process = await Repoz.GetProces();
            User = await Repoz.GetLogon(process.Logon);
        }

        public void HaveAccess(RolsPermits.Permits permit)
        {
            HaveAccess(new DrawerEntry(0, String.Empty, null, permit));
        }

        public async void HaveAccess(DrawerEntry Entry)
        {
            this.Entry = Entry;

            if (User == null)
            {
                var process = await Repoz.GetProces();
                User = await Repoz.GetLogon(process.Logon);
            }

            if (User.Permisos.Any(p => p.Permit == Entry.Permit))
            {
                CurrentProcess = await Repoz.GetProces();
                if (Response != null) Response.Invoke(true, Entry);
                return;
            }

            var SecurityDialog = new PassDialog(context, PassDialog.Motivos.Necesita_Autorizacion, Entry.Permit);
            SecurityDialog.OnCancelPress += SecurityDialog_OnCancelPress;
            SecurityDialog.OnAcceptPress += SecurityDialog_OnAcceptPress;
        }

        private void SecurityDialog_OnAcceptPress(ProcessList Process)
        {
            CurrentProcess = Process;
            Response.Invoke(true, Entry);
        }

        private void SecurityDialog_OnCancelPress()
        {
            Response.Invoke(false, Entry);
        }

        public static void SetProcess(ProcessList Process)
        {
            CurrentProcess = Process;
        }
    }
}