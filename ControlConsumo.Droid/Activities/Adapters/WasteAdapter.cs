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
using ControlConsumo.Shared.Models.R;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class WasteAdapter : BaseAdapter
    {
        private readonly Context context;
        public readonly List<MaterialReport> Materiales;
        private readonly LayoutInflater Inflater;

        public WasteAdapter(Context context, IEnumerable<MaterialReport> Materiales)
        {
            this.context = context;
            Inflater = LayoutInflater.From(context);
            this.Materiales = Materiales.ToList();
        }

        public override int Count
        {
            get { return Materiales.Count() + 1; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var holder = new Holder();

            if (convertView == null)
            {
                convertView = Inflater.Inflate(Resource.Layout.adapter_waste, null);
                holder.chkActivar = convertView.FindViewById<CheckBox>(Resource.Id.chkActivar);
                holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                holder.txtViewCantidad = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                holder.txtViewUnidad = convertView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                holder.EditCantidad = convertView.FindViewById<EditText>(Resource.Id.EditCantidad);
                holder.EditCantidad.AfterTextChanged += EditCantidad_AfterTextChanged;
                holder.chkActivar.Click += chkActivar_Click;
                convertView.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as Holder;
            }

            if (position == 0)
            {
                holder.chkActivar.Visibility = ViewStates.Invisible;
                holder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewUnidad.Text = context.GetString(Resource.String.ReportTitleUnidad);
                holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleAcumulated);
                holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                holder.EditCantidad.Enabled = false;
                holder.EditCantidad.Text = context.GetString(Resource.String.ReportTitleCantidad);
                holder.EditCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.EditCantidad.SetTextColor(Android.Graphics.Color.Black);
                holder.EditCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);
            }
            else
            {
                var pos = Materiales.ElementAt(position - 1);

                holder.txtViewMaterial.Text = pos.MaterialName;
                holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewUnidad.Text = pos.MaterialUnit ?? pos.Unit;
                holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCantidad.Text = pos.Acumulated.ToString("N3");
                holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                holder.EditCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.EditCantidad.SetTextColor(Android.Graphics.Color.Black);               

                if (holder.chkActivar.Checked)
                {
                    holder.EditCantidad.Text = pos.Quantity > 0 ? pos.Quantity.ToString() : String.Empty;
                    holder.EditCantidad.SetBackgroundResource(Resource.Drawable.bg_input_white);
                    holder.EditCantidad.Enabled = true;                   
                }
                else
                {
                    holder.EditCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);
                    holder.EditCantidad.Enabled = false;
                    holder.EditCantidad.Text = String.Empty;
                }

                holder.Position = position - 1;
                holder.EditCantidad.Tag = holder;
                holder.chkActivar.Tag = holder;
            }

            return convertView;
        }

        private void chkActivar_Click(object sender, EventArgs e)
        {
            var obj = sender as CheckBox;
            var holder = obj.Tag as Holder;

            holder.EditCantidad.Enabled = obj.Checked;
            holder.EditCantidad.RequestFocus();
            holder.EditCantidad.SetBackgroundResource(obj.Checked ? Resource.Drawable.bg_input_white : Resource.Drawable.selector_input_text);

            if (!obj.Checked) Materiales[holder.Position].Quantity = 0;
        }

        private void EditCantidad_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            var obj = sender as EditText;
            var holder = obj.Tag as Holder;

            if (holder != null)
            {
                Materiales[holder.Position].Quantity = obj.Text.ToNumeric();
            }
        }

        private class Holder : Java.Lang.Object
        {
            public Int32 Position { get; set; }
            public CheckBox chkActivar;
            public TextView txtViewMaterial;
            public TextView txtViewCantidad;
            public TextView txtViewUnidad;
            public EditText EditCantidad;
        }
    }
}