using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace ControlConsumo.Droid.Managers
{
    class KeyboardManager
    {
        public InputMethodManager inputMethodManager { get; set; }

        public KeyboardManager(Context context)
        {
            this.inputMethodManager = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
        }

        public void HideSoftKeyboard(Activity activity)
        {
            var currentFocus = activity.CurrentFocus;
            if (currentFocus != null)
            {
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }

        public void ShowSoftKeyboard(Context context, View view)
        {
            inputMethodManager = (InputMethodManager) context.GetSystemService(Context.InputMethodService);
            inputMethodManager.ShowSoftInput(view, ShowFlags.Forced);
        }
    }
}