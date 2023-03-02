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
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Droid.Activities.Adapters;

namespace ControlConsumo.Droid.Activities.Widgets
{
    class BomDialog
    {
        private readonly Context context;

        public BomDialog(Context context)
        {
            this.context = context;
        }

        public event Shot OnShot;
        public delegate void Shot();

        public void ShowDialog(List<ZBomLackMaterial> lista, Boolean IsComplete)
        {
            Dialog dialog = null;

            dialog = new Dialog(context, Android.Resource.Style.ThemeTranslucentNoTitleBar);
            dialog.SetContentView(Resource.Layout.dialog_lack_material_list);
            dialog.SetCancelable(false);

            var btnCancel = dialog.FindViewById<Button>(Resource.Id.btnCancel);
            var btnAccept = dialog.FindViewById<Button>(Resource.Id.btnAccept);
            var btnCancel2 = dialog.FindViewById<Button>(Resource.Id.btnCancel2);
            var lstGeneral = dialog.FindViewById<ListView>(Resource.Id.lstGeneral);
            var viewflipper = dialog.FindViewById<ViewFlipper>(Resource.Id.viewflipper);
            lstGeneral.Adapter = new MaterialLackAdapter(context, lista);
            var LayoutPass = dialog.FindViewById<LinearLayout>(Resource.Id.custom_dialog_first_rl);
            LayoutPass.Background = context.Resources.GetDrawable(Resource.Color.gray_base);
            LayoutPass.Background.SetAlpha(175);

            btnAccept.Click += (o, a) =>
            {
                dialog.Dismiss();
                dialog.Dispose();
                OnShot.Invoke();
            };

            btnCancel.Click += (o, a) =>
            {
                dialog.Dismiss();
                dialog.Dispose();                
            };

            btnCancel2.Click += (o, a) =>
            {
                dialog.Dismiss();
                dialog.Dispose();                
            };

            if (!IsComplete)
            {
                viewflipper.DisplayedChild = 0;
            }
            else
            {
                viewflipper.DisplayedChild = 1;
            }

            dialog.Show();
        }
    }
}