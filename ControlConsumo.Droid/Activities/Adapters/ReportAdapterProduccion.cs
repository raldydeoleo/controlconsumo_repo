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
using ControlConsumo.Shared.Models.Process;
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Droid.Managers;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class ReportAdapterProduccion : BaseAdapter
    {
        private readonly IEnumerable<ProductionReport> Produccion;
        private readonly LayoutInflater Inflater;
        private readonly Context context;
        private readonly ProcessList Proceso;
        private readonly Byte TurnID;
        private readonly DateTime produccion;

        public ReportAdapterProduccion(Context context, IEnumerable<ProductionReport> Produccion, ProcessList Proceso, Byte TurnID, DateTime produccion)
        {
            this.Proceso = Proceso;
            this.context = context;
            Inflater = LayoutInflater.From(context);
            this.Produccion = Produccion;
            this.TurnID = TurnID;
            this.produccion = produccion;
        }

        public override int Count
        {
            get { return Produccion.Count() + 3; }
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
            switch (position)
            {
                case 0:

                    convertView = Inflater.Inflate(Resource.Layout.question_dialog, null);
                    var txtViewTitle = convertView.FindViewById<TextView>(Resource.Id.txtQuestion);

                    txtViewTitle.Text = String.Format("{0} {1}", context.GetString(Resource.String.ReportTitleProduccion), produccion.ToString("MMMMM yyyy"));
                    txtViewTitle.SetBackgroundResource(Resource.Drawable.bg_input_disabled);
                    txtViewTitle.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    txtViewTitle.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    break;

                case 1:

                    convertView = Inflater.Inflate(Resource.Layout.adapter_report_produccion_promedio, null);

                    var txtViewBandejas = convertView.FindViewById<TextView>(Resource.Id.txtViewBandejas);
                    var txtViewUnidad = convertView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                    var txtViewTotal = convertView.FindViewById<TextView>(Resource.Id.txtViewTotal);
                    var txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);

                    txtViewTitle = convertView.FindViewById<TextView>(Resource.Id.txtViewTitle);

                    txtViewTitle.Text = context.GetString(Resource.String.ReportTitleAverageElaborates);
                    txtViewTitle.SetTextColor(Android.Graphics.Color.DarkBlue);

                    var grupo = Produccion.GroupBy(g => g.Fecha.Date)
                        .Select(s => new
                        {
                            s.Key.Date,
                            Quantity = s.Sum(d => d.Quantity),
                            Total = s.Sum(d => d.Total)
                        }).ToList();

                    txtViewBandejas.Text = Produccion.Any() ? Math.Floor(grupo.Average(p => p.Quantity)).ToString() : "0.000";
                    txtViewBandejas.SetTextColor(Android.Graphics.Color.DarkBlue);

                    txtViewTotal.Text = Produccion.Any() ? grupo.Average(p => p.Total).ToString("N3") : "0.000";
                    txtViewTotal.SetTextColor(Android.Graphics.Color.DarkBlue);

                    txtViewUnidad.Text = Produccion.Any() ? Produccion.First().Unit : String.Empty;
                    txtViewUnidad.SetTextColor(Android.Graphics.Color.DarkBlue);
                    
                    txtViewMaterial.Text = Produccion.Any() ? Produccion.First().ProductShort : String.Empty;
                    txtViewMaterial.SetTextColor(Android.Graphics.Color.DarkBlue);

                    break;

                case 2:

                    GetHolder(ref convertView);

                    var holder = convertView.Tag as Holder;

                    holder.txtViewFecha.Text = context.GetString(Resource.String.ReportTitleFecha);
                    holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);

                    if (!Proceso.IsLast)
                        holder.txtViewBandejas.Text = context.GetString(Resource.String.ReportTitleBandejas);
                    else
                        holder.txtViewBandejas.Text = context.GetString(Resource.String.ReportTitleBandejas2);

                    holder.txtViewBandejas.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewBandejas.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                    holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewTotal.Text = context.GetString(Resource.String.ReportTitleTotalProduccion);
                    holder.txtViewTotal.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewTotal.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewUnidad.Text = context.GetString(Resource.String.ReportTitleUnidad);
                    holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                    break;

                default:

                    GetHolder(ref convertView);

                    holder = convertView.Tag as Holder;

                    var detalle = Produccion.ElementAt(position - 3);

                    holder.position = position;
                    holder.fecha = detalle.Fecha;

                    holder.txtViewFecha.Text = detalle.Fecha.ToString("MMM dd, yyyy");
                    holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewBandejas.Text = detalle.Quantity.ToString();
                    holder.txtViewBandejas.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewBandejas.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewMaterial.Text = detalle.ProductShort ?? detalle.ProductCode;
                    holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewTotal.Text = detalle.Total.ToString("N3");
                    holder.txtViewTotal.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewTotal.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewUnidad.Text = detalle.Unit;
                    holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    break;
            }

            return convertView;
        }

        private class Holder : Java.Lang.Object
        {
            public DateTime fecha { get; set; }
            public Int32 position { get; set; }
            public TextView txtViewFecha;
            public TextView txtViewMaterial;
            public TextView txtViewBandejas;
            public TextView txtViewTotal;
            public TextView txtViewUnidad;
        }

        private void GetHolder(ref View convertView)
        {
            if (convertView == null || convertView.Tag == null)
            {
                var holder = new Holder();
                convertView = Inflater.Inflate(Resource.Layout.adapter_report_produccion, null);
                holder.txtViewFecha = convertView.FindViewById<TextView>(Resource.Id.txtViewFecha);
                holder.txtViewBandejas = convertView.FindViewById<TextView>(Resource.Id.txtViewBandejas);
                holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                holder.txtViewTotal = convertView.FindViewById<TextView>(Resource.Id.txtViewTotal);
                holder.txtViewUnidad = convertView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                convertView.Tag = holder;
            }
        }
    }
}