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
using ControlConsumo.Shared.Models.Elaborate;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class ReturnProductionAdapter : BaseAdapter
    {
        public readonly List<ElaborateList> Elaborates;
        private readonly LayoutInflater Inflater;
        private readonly Context context;

        public ReturnProductionAdapter(Context context, IEnumerable<ElaborateList> Elaborates)
        {
            this.context = context;
            this.Elaborates = Elaborates.OrderByDescending(o => o.Fecha).ThenByDescending(t => t.TurnID).ToList();
            this.Inflater = LayoutInflater.From(context);
        }

        public override int Count
        {
            get { return Elaborates.Count + 1; }
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
                convertView = Inflater.Inflate(Resource.Layout.adapter_produccion, null);
                holder.chkSelect = convertView.FindViewById<CheckBox>(Resource.Id.chkSelect);
                holder.txtViewProduct = convertView.FindViewById<TextView>(Resource.Id.txtViewProduct);
                holder.txtViewTurn = convertView.FindViewById<TextView>(Resource.Id.txtViewTurn);
                holder.txtViewFecha = convertView.FindViewById<TextView>(Resource.Id.txtViewFecha);
                holder.txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora);
                holder.txtViewBandeja = convertView.FindViewById<TextView>(Resource.Id.txtViewBandeja);
                holder.txtViewCantidad = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                holder.txtViewUnidad = convertView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                holder.txtViewLogon = convertView.FindViewById<TextView>(Resource.Id.txtViewLogon);
                holder.chkSelect.Click += chkSelect_Click;

                convertView.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as Holder;
            }

            if (position == 0)
            {
                holder.chkSelect.Visibility = ViewStates.Invisible;

                holder.txtViewProduct.Text = context.GetString(Resource.String.ReportTitleMaterial);
                holder.txtViewProduct.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewProduct.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewTurn.Text = context.GetString(Resource.String.ReportTitleTurn);
                holder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewTurn.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewFecha.Text = context.GetString(Resource.String.ReportTitleFecha);
                holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewHora.Text = context.GetString(Resource.String.ReportTitleHora);
                holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewBandeja.Text = context.GetString(Resource.String.ReportTitleBandejas);
                holder.txtViewBandeja.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewBandeja.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleCantidad);
                holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewUnidad.Text = context.GetString(Resource.String.ReportTitleUnidad);
                holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewLogon.Text = context.GetString(Resource.String.ReportTitleUsuario);
                holder.txtViewLogon.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewLogon.SetTextColor(Android.Graphics.Color.Black);
            }
            else
            {
                var pos = Elaborates.ElementAt(position - 1);

                holder.Position = position - 1;
                holder.chkSelect.Tag = null;

                holder.chkSelect.Visibility = ViewStates.Visible;
                holder.chkSelect.Checked = pos.IsActive;

                holder.txtViewProduct.Text = pos._DisplayCode;
                holder.txtViewProduct.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewProduct.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewTurn.Text = pos.TurnID.ToString();
                holder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewTurn.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewFecha.Text = pos.Produccion.ToLocalTime().ToString("dd MMMM yyyy");
                holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewHora.Text = pos.Produccion.ToLocalTime().ToString("HH:mm:ss");
                holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewBandeja.Text = pos.TrayID;
                holder.txtViewBandeja.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewBandeja.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCantidad.Text = pos.Quantity.ToString("N3");
                holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewUnidad.Text = pos.Unit;
                holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewLogon.Text = pos.Logon;
                holder.txtViewLogon.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewLogon.SetTextColor(Android.Graphics.Color.Black);

                holder.chkSelect.Tag = holder;
            }

            return convertView;
        }

        private void chkSelect_Click(object sender, EventArgs e)
        {
            var obj = sender as CheckBox;

            var holder = obj.Tag as Holder;

            if (holder != null)
            {
                Elaborates[holder.Position].IsActive = obj.Checked;
            }
        }

        private class Holder : Java.Lang.Object
        {
            public Int32 Position { get; set; }
            public CheckBox chkSelect { get; set; }
            public TextView txtViewTurn { get; set; }
            public TextView txtViewProduct { get; set; }
            public TextView txtViewFecha { get; set; }
            public TextView txtViewHora { get; set; }
            public TextView txtViewBandeja { get; set; }
            public TextView txtViewCantidad { get; set; }
            public TextView txtViewUnidad { get; set; }
            public TextView txtViewLogon { get; set; }
        }
    }
}