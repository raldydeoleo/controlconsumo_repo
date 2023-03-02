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
using System.Threading.Tasks;
using ControlConsumo.Droid.Managers;

namespace ControlConsumo.Droid.Activities.Widgets
{
    public class ChangePassword
    {
        private readonly Context context;
        private readonly Dialog dialog;
        private readonly Boolean NeedConfirm;
        private readonly String Logon;
        private readonly TextView txtViewUserName;
        private readonly EditText editOldPass;
        private readonly EditText editNewPass;
        private readonly EditText editConfigPass;
        private readonly EditText editCode;
        private readonly Button btnAcept;
        private readonly Button btnAceptDialog;
        private readonly Button btnCancelDialog;
        private readonly RepositoryFactory repo;
        private readonly Boolean Admin;
        private readonly ViewFlipper flipper;
        private Users usuario;


        public EventHandler evento;

        public ChangePassword(Context context, Boolean NeedConfirm, String Logon, Boolean Admin = false)
        {
            this.context = context;
            this.NeedConfirm = NeedConfirm;
            this.Logon = Logon;
            this.Admin = Admin;

            repo = new RepositoryFactory(Util.GetConnection());

            dialog = new Dialog(context, Android.Resource.Style.ThemeTranslucentNoTitleBar);
            dialog.SetContentView(Resource.Layout.dialog_change_password);
            dialog.SetCancelable(false);

            #region Init Layout

            var LayoutPass = dialog.FindViewById<LinearLayout>(Resource.Id.custom_dialog_first_rl);
            flipper = dialog.FindViewById<ViewFlipper>(Resource.Id.viewflipper);
            txtViewUserName = dialog.FindViewById<TextView>(Resource.Id.txtViewUserName);
            editOldPass = dialog.FindViewById<EditText>(Resource.Id.editOldPass);
            editNewPass = dialog.FindViewById<EditText>(Resource.Id.editNewPass);
            editConfigPass = dialog.FindViewById<EditText>(Resource.Id.editConfigPass);
            editCode = dialog.FindViewById<EditText>(Resource.Id.editCode);
            btnAcept = dialog.FindViewById<Button>(Resource.Id.btnAcept);
            btnAceptDialog = dialog.FindViewById<Button>(Resource.Id.btnAceptDialog);
            btnCancelDialog = dialog.FindViewById<Button>(Resource.Id.btnCancelDialog);

            btnAcept.Click += btnAceptDialog_Click;
            btnAceptDialog.Click += btnAceptDialog_Click;
            btnCancelDialog.Click += btnCancelDialog_Click;

            #endregion

            if (NeedConfirm)
            {
                editOldPass.Visibility = ViewStates.Visible;
                flipper.DisplayedChild = 1;
            }
            else
            {
                flipper.DisplayedChild = 0;
                editOldPass.Visibility = ViewStates.Gone;
            }

            LayoutPass.Background = context.Resources.GetDrawable(Resource.Color.gray_base);

            LayoutPass.Background.SetAlpha(175);

            dialog.Show();

            LoadUser();
        }

        private async Task LoadUser()
        {
            var repouse = repo.GetRepositoryUsers();
            usuario = await repouse.GetAsyncByKey(Logon);
            txtViewUserName.Text = usuario.Name;

            if (Admin)
            {
                editCode.Text = usuario.Code;
                editCode.Enabled = false;
                editOldPass.Visibility = ViewStates.Gone;
                flipper.DisplayedChild = 1;
            }
        }

        private void btnCancelDialog_Click(object sender, EventArgs e)
        {
            if (Admin && evento != null) evento.Invoke(sender, e);
            dialog.Dismiss();
            dialog.Dispose();
        }

        private async void btnAceptDialog_Click(object sender, EventArgs e)
        {
            if (NeedConfirm && !editOldPass.Text.Equals(usuario.Password))
            {
                editOldPass.RequestFocus();
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.FeedBackWrong));
                return;
            }

            if (String.IsNullOrEmpty(editNewPass.Text))
            {
                editNewPass.RequestFocus();
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.dialogpassnoempty));
                return;
            }

            if (String.IsNullOrEmpty(editCode.Text))
            {
                editCode.RequestFocus();
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.dialogpassnocode));
                return;
            }

            if (!editCode.Text.Equals(usuario.Code))
            {
                editCode.RequestFocus();
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.FeedBackWrongCode));
                return;
            }

            if (!editNewPass.Text.Equals(editConfigPass.Text))
            {
                editNewPass.Text = String.Empty;
                editConfigPass.Text = String.Empty;
                editNewPass.RequestFocus();
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.dialogpasswrongconfirm));
                return;
            }

            if (editNewPass.Text.Equals(usuario.Password))
            {
                editNewPass.Text = String.Empty;
                editConfigPass.Text = String.Empty;
                editNewPass.RequestFocus();
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.dialogpassnoold));
                return;
            }

            if (editNewPass.Text.Equals(usuario.Code))
            {
                editNewPass.Text = String.Empty;
                editConfigPass.Text = String.Empty;
                editNewPass.RequestFocus();
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.dialogpassnosamecode));
                return;
            }

            if (editNewPass.Text.Length < 4)
            {
                editNewPass.Text = String.Empty;
                editConfigPass.Text = String.Empty;
                editNewPass.RequestFocus();
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.dialogpassnoleght));
                return;
            }

            var repouse = repo.GetRepositoryUsers();

            usuario.Password = editNewPass.Text;
            usuario.Sync = true;
            usuario.Expire = DateTime.Now.AddDays(45);

            await repouse.UpdateAsync(usuario);

            if (evento != null)
            {
                evento.Invoke(null, null);
            }            

            Toast.MakeText(context, Resource.String.dialogpasscorrect, ToastLength.Long).Show();

            dialog.Dismiss();
            dialog.Dispose();
        }
    }
}