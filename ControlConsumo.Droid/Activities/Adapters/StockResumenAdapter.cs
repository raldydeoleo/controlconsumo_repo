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
using ControlConsumo.Droid.Managers;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class StockResumenAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly IEnumerable<StockResumeList> list;
        private readonly LayoutInflater Inflater;

        public StockResumenAdapter(Context context, IEnumerable<StockResumeList> list, Byte TurnID, DateTime Fecha)
        {
            var CustomFecha = Convert.ToInt32(Fecha.GetSapDate());

            this.list = list.Where(p => p.Total > 0 || (p.Total == 0 && p.TurnID == TurnID && p.CustomFecha == CustomFecha)).OrderBy(p => p._ProductCode).ThenBy(p => p.Lot).ToList();
            this.context = context;
            this.Inflater = LayoutInflater.From(context);
        }

        public override int Count
        {
            get { return list.Count() + 2; }
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
            try
            {
                switch (position)
                {
                    case 0:

                        convertView = Inflater.Inflate(Resource.Layout.question_dialog, null);
                        var txtViewTitle = convertView.FindViewById<TextView>(Resource.Id.txtQuestion);

                        txtViewTitle.Text = context.GetString(Resource.String.ReportTitleInventory);
                        txtViewTitle.SetBackgroundResource(Resource.Drawable.bg_input_disabled);
                        txtViewTitle.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        txtViewTitle.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                        break;

                    case 1:

                        GetHolder(ref convertView);

                        var holder = convertView.Tag as Holder;

                        holder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                        holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewLoteSap.Text = context.GetString(Resource.String.ReportTitleLote);
                        holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewLoteSup.Text = context.GetString(Resource.String.ReportTitleLoteSupl);
                        holder.txtViewLoteSup.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewLoteSup.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewEntregado.Text = context.GetString(Resource.String.ReportTitleEntregado);
                        holder.txtViewEntregado.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewEntregado.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewConsumido.Text = context.GetString(Resource.String.ReportTitleConsumido);
                        holder.txtViewConsumido.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewConsumido.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewFinal.Text = context.GetString(Resource.String.ReportTitleActual);
                        holder.txtViewFinal.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewFinal.SetTextColor(Android.Graphics.Color.Black);

                        break;

                    default:

                        GetHolder(ref convertView);

                        holder = convertView.Tag as Holder;

                        var pos = list.ElementAt(position - 2);

                        holder.txtViewMaterial.Text = pos._ProductName;
                        holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewLoteSap.Text = pos.Lot;
                        holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewLoteSup.Text = pos.Reference;
                        holder.txtViewLoteSup.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewLoteSup.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewEntregado.Text = pos.Entregado.ToString("N3");
                        holder.txtViewEntregado.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewEntregado.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewConsumido.Text = pos.Consumido.ToString("N3");
                        holder.txtViewConsumido.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewConsumido.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewFinal.Text = pos.Total.ToString("N3");
                        holder.txtViewFinal.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                        if (pos.Total > 0)
                            holder.txtViewFinal.SetTextColor(Android.Graphics.Color.DarkGreen);
                        else
                            holder.txtViewFinal.SetTextColor(Android.Graphics.Color.Red); 

                        break;
                }
            }
            catch (Exception ex)
            {
                Util.CatchException(context, ex);
            }
            return convertView;
        }

        private class Holder : Java.Lang.Object
        {
            public TextView txtViewMaterial { get; set; }
            public TextView txtViewLoteSap { get; set; }
            public TextView txtViewLoteSup { get; set; }
            public TextView txtViewEntregado { get; set; }
            public TextView txtViewConsumido { get; set; }
            public TextView txtViewFinal { get; set; }
        }

        private void GetHolder(ref View convertView)
        {
            if (convertView == null || convertView.Tag == null)
            {
                var holder = new Holder();
                convertView = Inflater.Inflate(Resource.Layout.adapter_stock_resume, null);
                holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                holder.txtViewLoteSap = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSap);
                holder.txtViewLoteSup = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSup);
                holder.txtViewEntregado = convertView.FindViewById<TextView>(Resource.Id.txtViewEntregado);
                holder.txtViewConsumido = convertView.FindViewById<TextView>(Resource.Id.txtViewConsumido);
                holder.txtViewFinal = convertView.FindViewById<TextView>(Resource.Id.txtViewFinal);
                convertView.Tag = holder;
            }
        }
    }
}