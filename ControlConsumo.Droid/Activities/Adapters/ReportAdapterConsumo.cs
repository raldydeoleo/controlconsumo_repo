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
using ControlConsumo.Droid.Managers;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class ReportAdapterConsumo : BaseAdapter
    {
        private readonly Context context;
        private readonly IEnumerable<MaterialReport> Consumos;
        private readonly LayoutInflater Inflater;
        private readonly DateTime FechaProduccion;

        public ReportAdapterConsumo(Context context, IEnumerable<MaterialReport> Consumos, DateTime FechaProduccion)
        {
            this.FechaProduccion = FechaProduccion;
            this.context = context;
            Inflater = LayoutInflater.From(context);
            this.Consumos = Consumos;
        }

        public override int Count
        {
            get { return Consumos.Count() + 2; }
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
            var view = convertView;

            switch (position)
            {
                case 0:

                    view = Inflater.Inflate(Resource.Layout.question_dialog, null);
                    var txtViewTitle = view.FindViewById<TextView>(Resource.Id.txtQuestion);

                    txtViewTitle.Text = String.Format(context.GetString(Resource.String.ReportTitleConsumo), FechaProduccion.ToString("dd MMMM yyyy."));
                    txtViewTitle.SetBackgroundResource(Resource.Drawable.bg_input_disabled);
                    txtViewTitle.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    txtViewTitle.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    break;

                case 1:

                    GetCustomView(ref view);

                    var holder = view.Tag as Holder;

                    holder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                    holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleCantidad);
                    holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewUnidad.Text = context.GetString(Resource.String.ReportTitleUnidad);
                    holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewCaja.Text = context.GetString(Resource.String.ReportTitleCaja);
                    holder.txtViewCaja.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewCaja.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewLoteSap.Text = context.GetString(Resource.String.ReportTitleLoteBacht);
                    holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewHora.Text = context.GetString(Resource.String.ReportTitleHora);
                    holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewProducto.Text = context.GetString(Resource.String.ReportTitleProducto);
                    holder.txtViewProducto.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewProducto.SetTextColor(Android.Graphics.Color.Black);

                    break;

                default:

                    GetCustomView(ref view);

                    holder = view.Tag as Holder;

                    var detalle = Consumos.ElementAt(position - 2);

                    holder.txtViewMaterial.Text = detalle.MaterialName;
                    holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewCantidad.Text = detalle.Quantity.ToString("N3");
                    holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewUnidad.Text = detalle.MaterialUnit;
                    holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewCaja.Text = !String.IsNullOrEmpty(detalle.TrayID) ? detalle.TrayID : (detalle.BoxNumber > 0 ? detalle.BoxNumber.ToString() : String.Empty);
                    holder.txtViewCaja.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewCaja.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewLoteSap.Text = String.IsNullOrEmpty(detalle.Lot) ? Util.MaskBatchID(detalle.BatchID) : detalle.Lot;
                    holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewHora.Text = detalle.Produccion.ToLocalH();
                    holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewProducto.Text = detalle.ProductShort;
                    holder.txtViewProducto.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewProducto.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    break;
            }

            return view;
        }

        public class Holder : Java.Lang.Object
        {
            public TextView txtViewMaterial;
            public TextView txtViewCantidad;
            public TextView txtViewUnidad;
            public TextView txtViewCaja;
            public TextView txtViewLoteSap;
            public TextView txtViewHora;
            public TextView txtViewProducto;
        }

        private void GetCustomView(ref View convertView)
        {
            if (convertView == null || convertView.Tag == null)
            {
                var holder = new Holder();
                convertView = Inflater.Inflate(Resource.Layout.adapter_report_consumo, null);
                holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                holder.txtViewCantidad = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                holder.txtViewUnidad = convertView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                holder.txtViewCaja = convertView.FindViewById<TextView>(Resource.Id.txtViewCaja);
                holder.txtViewLoteSap = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSap);
                holder.txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora);
                holder.txtViewProducto = convertView.FindViewById<TextView>(Resource.Id.txtViewProducto);
                convertView.Tag = holder;
            }
        }
    }
}