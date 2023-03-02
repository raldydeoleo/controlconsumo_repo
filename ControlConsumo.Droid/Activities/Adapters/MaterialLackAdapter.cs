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
    class MaterialLackAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly LayoutInflater Inflater;
        private readonly IEnumerable<ZBomLackMaterial> list;

        public MaterialLackAdapter(Context context, List<ZBomLackMaterial> list)
        {
            this.context = context;
            this.Inflater = LayoutInflater.From(context);
            this.list = list;
        }

        public override int Count
        {
            get { return list.Any() ? list.Count() + 1 : 0; }
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
                convertView = Inflater.Inflate(Resource.Layout.adapter_report_lack_consumo, null);
                holder = new Holder()
                {
                    txtViewTurn = convertView.FindViewById<TextView>(Resource.Id.txtViewTurn),
                    txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial),
                    txtViewLoteSap = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSap),
                    txtViewLote = convertView.FindViewById<TextView>(Resource.Id.txtViewLote),
                    txtViewFecha = convertView.FindViewById<TextView>(Resource.Id.txtViewFecha),
                    txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora),
                    txtViewCode = convertView.FindViewById<TextView>(Resource.Id.txtViewCode),
                    txtViewCaja = convertView.FindViewById<TextView>(Resource.Id.txtViewCaja),
                };

                convertView.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as Holder;
            }

            if (position == 0)
            {
                holder.txtViewCaja.SetText(Resource.String.ReportTitleCaja);
                holder.txtViewCaja.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewCaja.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewTurn.SetText(Resource.String.ReportTitleTurn);
                holder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewTurn.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewMaterial.SetText(Resource.String.ReportTitleMaterial);
                holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewLoteSap.SetText(Resource.String.ReportTitleLoteSupl);
                holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewLote.SetText(Resource.String.ReportTitleLote);
                holder.txtViewLote.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewLote.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewFecha.SetText(Resource.String.ReportTitleFecha);
                holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewHora.SetText(Resource.String.ReportTitleHora);
                holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCode.SetText(Resource.String.EntryMaterialCodeHit);
                holder.txtViewCode.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewCode.SetTextColor(Android.Graphics.Color.Black);
            }
            else
            {
                var pos = list.ElementAt(position - 1);

                holder.txtViewCode.Text = pos._Short;
                holder.txtViewCode.SetTextColor(Android.Graphics.Color.Black);
                holder.txtViewCode.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.txtViewMaterial.Text = pos.Name;
                holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);
                holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.txtViewCaja.Text = pos.BoxNumber.GetValueOrDefault() > 0 ? pos.BoxNumber.Value.ToString() : String.Empty;
                holder.txtViewCaja.SetTextColor(Android.Graphics.Color.Black);
                holder.txtViewCaja.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.txtViewTurn.Text = pos.TurnID.HasValue ? pos.TurnID.Value.ToString() : String.Empty;
                holder.txtViewTurn.SetTextColor(Android.Graphics.Color.Black);
                holder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.txtViewLote.Text = String.IsNullOrEmpty(pos.Lot) ? Util.MaskBatchID(pos.BatchID) : pos.Lot;
                holder.txtViewLote.SetTextColor(Android.Graphics.Color.Black);
                holder.txtViewLote.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.txtViewLoteSap.Text = pos.SupLot;
                holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);
                holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);
                holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);
                holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                if (pos.Produccion.HasValue)
                {
                    holder.txtViewFecha.Text = pos.Produccion.Value.ToLocalTime().ToString("dd/MM/yy");
                    holder.txtViewHora.Text = pos.Produccion.Value.ToLocalTime().ToString("HH:mm");
                }
                else
                {
                    holder.txtViewFecha.Text = String.Empty;
                    holder.txtViewHora.Text = String.Empty;
                }
            }

            return convertView;
        }

        private class Holder : Java.Lang.Object
        {
            public TextView txtViewCode { get; set; }
            public TextView txtViewMaterial { get; set; }
            public TextView txtViewCaja { get; set; }
            public TextView txtViewLote { get; set; }
            public TextView txtViewLoteSap { get; set; }
            public TextView txtViewTurn { get; set; }
            public TextView txtViewFecha { get; set; }
            public TextView txtViewHora { get; set; }
        }
    }
}