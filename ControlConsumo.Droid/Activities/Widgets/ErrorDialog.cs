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
using ControlConsumo.Droid.Managers;
using Android.Text.Method;

namespace ControlConsumo.Droid.Activities.Widgets
{
    public class ErrorDialog
    {
        private readonly Context context;
        private readonly Dialog dialog;
        private readonly TextView txtViewMessage;
        private readonly Button btnAceptDialog;

        public delegate void OkButtonPress();
        public event OkButtonPress OnOkButtonPress;

        public ErrorDialog(Context context, Exception ex)
        {
            this.context = context;
            dialog = new Dialog(context, Android.Resource.Style.ThemeTranslucentNoTitleBar);
            dialog.SetContentView(Resource.Layout.error_dialog_messages);
            dialog.SetCancelable(false);

            var layout = dialog.FindViewById<LinearLayout>(Resource.Id.errorLayout);
            txtViewMessage = dialog.FindViewById<TextView>(Resource.Id.txtViewMessage);
            btnAceptDialog = dialog.FindViewById<Button>(Resource.Id.btnAceptDialog);
            btnAceptDialog.Click += btnAceptDialog_Click;
            txtViewMessage.Text = String.Format("{0}\n{1}", ex.Message, ex.StackTrace);
            txtViewMessage.MovementMethod = new ScrollingMovementMethod();

            layout.Background = context.Resources.GetDrawable(Resource.Color.gray_base);
            layout.Background.SetAlpha(175);           

            dialog.Show();
        }

        private void btnAceptDialog_Click(object sender, EventArgs e)
        {
            if (OnOkButtonPress != null)
            {
                OnOkButtonPress.Invoke();
            }

            dialog.Dismiss();
            dialog.Dispose();
        }
    }
}