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
using ControlConsumo.Shared.Models.Process;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Shared.Repositories;

namespace ControlConsumo.Droid.Activities.Widgets
{
    public class PassDialog
    {
        private readonly Dialog dialog;
        private readonly EditText editScanCodigo;
        private readonly EditText editPassword;
        private readonly TextView txtViewUser;
        private readonly TextView txtViewMessage;
        private readonly Button btnAceptDialog;
        private readonly Button btnCancelDialog;
        private readonly RepositoryZ repoz;
        private readonly Context context;
        private readonly RolsPermits.Permits Permit;
        private Users User;
        private Boolean AlreadyScan { get; set; }

        public delegate void AcceptPress(ProcessList Process);
        public delegate void CancelPress();

        public event AcceptPress OnAcceptPress;
        public event CancelPress OnCancelPress;

        public enum Motivos
        {
            Fin_Turno,
            Necesita_Autorizacion
        }

        public PassDialog(Context context, Motivos motivo, RolsPermits.Permits Permit = RolsPermits.Permits.NONE)
        {
            this.context = context;
            this.Permit = Permit;
            repoz = new RepositoryFactory(Util.GetConnection()).GetRepositoryZ();
            dialog = new Dialog(context, Android.Resource.Style.ThemeTranslucentNoTitleBar);
            dialog.SetContentView(Resource.Layout.dialog_catch_password);
            dialog.SetCancelable(false);

            var LayoutPass = dialog.FindViewById<LinearLayout>(Resource.Id.custom_dialog_first_rl);
            editScanCodigo = dialog.FindViewById<EditText>(Resource.Id.editScanCodigo);
            txtViewUser = dialog.FindViewById<TextView>(Resource.Id.txtViewUser);
            btnAceptDialog = dialog.FindViewById<Button>(Resource.Id.btnAceptDialog);
            btnCancelDialog = dialog.FindViewById<Button>(Resource.Id.btnCancelDialog);
            txtViewMessage = dialog.FindViewById<TextView>(Resource.Id.txtViewMessage);
            editPassword = dialog.FindViewById<EditText>(Resource.Id.editPassword);
            editPassword.Enabled = false;

            switch (motivo)
            {
                case Motivos.Fin_Turno:
                    txtViewMessage.Text = context.GetString(Resource.String.AlertChangeTurnChangeDialog);

                    break;

                case Motivos.Necesita_Autorizacion:
                    txtViewMessage.Text = context.GetString(Resource.String.AlertNeedPower);

                    break;
            }

            btnAceptDialog.Click += btnAceptDialog_Click;
            btnCancelDialog.Click += btnCancelDialog_Click;
            editScanCodigo.KeyPress += editScanCodigo_KeyPress;

            LayoutPass.Background = context.Resources.GetDrawable(Resource.Color.gray_base);

            LayoutPass.Background.SetAlpha(175);

            dialog.Show();
        }

        private async void editScanCodigo_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editScanCodigo.Text) && !AlreadyScan && e.Event.Action == KeyEventActions.Down)
            {
                AlreadyScan = true;

                User = await repoz.GetLogonByCode(editScanCodigo.Text);

                if (User != null)
                {
                    if (Permit != RolsPermits.Permits.NONE)
                    {
                        if (!User.Permisos.Any(p => p.Permit == Permit))
                        {
                            var noPower = new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.AlertNoPower));
                            editScanCodigo.Text = String.Empty;
                            AlreadyScan = false;
                            noPower.OnAcceptPress += wrongdialog_OnAcceptPress;
                            return;
                        }
                    }

                    txtViewUser.Text = User.Name;
                    editPassword.Enabled = true;
                    editPassword.RequestFocus();
                }
                else
                {
                    var wrongdialog = new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.FeedBackWrongCode));
                    wrongdialog.OnAcceptPress += wrongdialog_OnAcceptPress;
                }

                AlreadyScan = false;
                editScanCodigo.Text = String.Empty;

                e.Handled = true;
            }
        }

        private void wrongdialog_OnAcceptPress(Boolean IsCantidad, float Box, Single Cantidad)
        {
            editScanCodigo.RequestFocus();
        }

        private void btnCancelDialog_Click(object sender, EventArgs e)
        {
            if (OnCancelPress != null)
            {
                OnCancelPress.Invoke();
            }

            dialog.Dismiss();
            dialog.Dispose();
        }

        private async void btnAceptDialog_Click(object sender, EventArgs e)
        {
            var process = await repoz.GetProces();

            if (User == null)
            {
                return;
            }
            else if (!User.Password.Equals(editPassword.Text))
            {
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.FeedBackWrong));
                return;
            }

            process.Logon = User.Logon;
            process.UserName = User.Name;

            if (OnAcceptPress != null)
            {
                OnAcceptPress.Invoke(process);
            }

            dialog.Dismiss();
            dialog.Dispose();
        }
    }
}