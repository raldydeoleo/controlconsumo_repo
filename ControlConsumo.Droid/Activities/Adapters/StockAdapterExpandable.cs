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
    class StockAdapterExpandable : BaseExpandableListAdapter
    {
        private readonly Context context;
        private readonly LayoutInflater Inflater;
        private readonly IEnumerable<Stocks> stocks;

        public StockAdapterExpandable(Context context, IEnumerable<StockReports> Transactions)
        {
            this.Inflater = LayoutInflater.From(context);
            this.context = context;
            this.stocks = Transactions.GroupBy(p => new
            {
                p.MaterialCode,
                p.MaterialName,
                p.MaterialReference,
                p.MaterialUnit,
                p._MaterialCode,
                p._MaterialName,
                p.Lot,
                p.LoteSuplidor,
                p.TotalQuantity
            }).Select(g => new Stocks
            {
                _MaterialCode = g.Key._MaterialCode,
                _MaterialName = g.Key._MaterialName,
                Name = g.Key.MaterialName,
                Code = g.Key.MaterialCode,
                Reference = g.Key.MaterialReference,
                Unit = g.Key.MaterialUnit,
                Quantity = g.Key.TotalQuantity,
                Fecha = g.Max(m => m.Fecha).ToLocalTime(),
                Lot = g.Key.Lot,
                LotSuplidor = g.Key.LoteSuplidor,
                Details = g.Select(p => new Stocks.Detail
                {
                    Fecha = p.Fecha.ToLocalTime(),
                    Logon = p.Logon,
                    Quantity = p.Quantity,
                    Reason = p.Reason,
                    Total = p.Total,
                    TurnID = p.TurnID,
                    BoxNumber = p.BoxNumber
                }).ToList()
            }).OrderBy(p => p._MaterialCode).ThenBy(p => p.Lot).ToList();
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return 0;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            try
            {
                var holder = new ChildHolder();

                if (convertView == null)
                {
                    convertView = Inflater.Inflate(Resource.Layout.adapter_stock_detail, null);
                    holder.txtViewFecha = convertView.FindViewById<TextView>(Resource.Id.txtViewFecha);
                    holder.txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora);
                    holder.txtViewCantidad = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                    holder.txtViewBoxNumber = convertView.FindViewById<TextView>(Resource.Id.txtViewBoxNumber);
                    holder.txtViewUser = convertView.FindViewById<TextView>(Resource.Id.txtViewUser);
                    holder.txtViewReason = convertView.FindViewById<TextView>(Resource.Id.txtViewReason);
                    holder.txtViewTurn = convertView.FindViewById<TextView>(Resource.Id.txtViewTurn);
                    convertView.Tag = holder;
                }
                else
                {
                    holder = convertView.Tag as ChildHolder;
                }

                if (childPosition == 0)
                {
                    holder.txtViewFecha.Text = context.GetString(Resource.String.ReportTitleFecha);
                    holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewHora.Text = context.GetString(Resource.String.ReportTitleHora);
                    holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewTurn.Text = context.GetString(Resource.String.ReportTitleTurn);
                    holder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewTurn.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleCantidad);
                    holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewBoxNumber.Text = context.GetString(Resource.String.ReportTitleCaja);
                    holder.txtViewBoxNumber.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewBoxNumber.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewUser.Text = context.GetString(Resource.String.ReportTitleUsuario);
                    holder.txtViewUser.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewUser.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewReason.Text = context.GetString(Resource.String.ReportTitleReason);
                    holder.txtViewReason.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewReason.SetTextColor(Android.Graphics.Color.Black);
                }
                else
                {
                    var pos = stocks.ElementAt(groupPosition - 2).Details.ElementAt(childPosition - 1);

                    holder.txtViewFecha.Text = pos.Fecha.ToString("MMM dd, yyyy");
                    holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewHora.Text = pos.Fecha.ToString("hh:mm tt");
                    holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewTurn.Text = pos.TurnID.ToString();
                    holder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewCantidad.Text = pos.Quantity.ToString("N3");
                    holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewBoxNumber.Text = pos.BoxNumber == 0 ? String.Empty : pos.BoxNumber.ToString();
                    holder.txtViewBoxNumber.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewUser.Text = pos.Logon;
                    holder.txtViewUser.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    holder.txtViewReason.Text = pos.Reason;
                    holder.txtViewReason.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                    if (pos.Quantity > 0)
                        holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.DarkGreen);
                    else
                        holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Red);
                }
            }
            catch (Exception ex)
            {
                Util.CatchException(context, ex);
            }

            return convertView;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return groupPosition < 2 ? 0 : stocks.ElementAt(groupPosition - 2).Details.Count() + 1;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return null;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            try
            {
                switch (groupPosition)
                {
                    case 0:

                        convertView = Inflater.Inflate(Resource.Layout.question_dialog, null);
                        var txtViewTitle = convertView.FindViewById<TextView>(Resource.Id.txtQuestion);

                        txtViewTitle.Text = String.Format("{0} - {1}", context.GetString(Resource.String.ReportTitleStockNow), DateTime.Now.ToString("dd MMMM yyyy"));
                        txtViewTitle.SetBackgroundResource(Resource.Drawable.bg_input_disabled);
                        txtViewTitle.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        txtViewTitle.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                        break;

                    case 1:

                        GetHolder(ref convertView);

                        var holder = convertView.Tag as Holder;

                        holder.txtViewFecha.Text = context.GetString(Resource.String.ReportTitleFecha);
                        holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewHora.Text = context.GetString(Resource.String.ReportTitleHora);
                        holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                        holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewLoteSap.Text = context.GetString(Resource.String.ReportTitleLote);
                        holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewLoteSup.Text = context.GetString(Resource.String.ReportTitleLoteSupl);
                        holder.txtViewLoteSup.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewLoteSup.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewUnidad.Text = context.GetString(Resource.String.ReportTitleUnidad);
                        holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleActual);
                        holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                        break;

                    default:

                        GetHolder(ref convertView);

                        holder = convertView.Tag as Holder;

                        var pos = stocks.ElementAt(groupPosition - 2);

                        holder.txtViewFecha.Text = pos.Fecha.ToString("MMM dd, yyyy");
                        holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewHora.Text = pos.Fecha.ToString("hh:mm tt");
                        holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewMaterial.Text = pos._MaterialName;
                        holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewLoteSap.Text = pos.Lot;
                        holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewLoteSup.Text = pos.LotSuplidor;
                        holder.txtViewLoteSup.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewLoteSup.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewUnidad.Text = pos.Unit;
                        holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                        holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                        holder.txtViewCantidad.Text = pos.Quantity.ToString("N3");
                        holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                        if (pos.Quantity > 0)
                            holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.DarkGreen);
                        else
                            holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Red);

                        break;
                }

            }
            catch (Exception ex)
            {
                Util.CatchException(context, ex);
            }

            return convertView;
        }

        public override int GroupCount
        {
            get
            {
                if (stocks.Count() == 0)
                    return 0;
                else
                    return stocks.Count() + 2;
            }
        }

        public override bool HasStableIds
        {
            get { return false; }
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return false;
        }

        private void GetHolder(ref View convertView)
        {
            if (convertView == null || convertView.Tag == null)
            {
                var holder = new Holder();
                convertView = Inflater.Inflate(Resource.Layout.adapter_stock_head, null);
                holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                holder.txtViewCantidad = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                holder.txtViewUnidad = convertView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                holder.txtViewLoteSap = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSap);
                holder.txtViewLoteSup = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSup);
                holder.txtViewFecha = convertView.FindViewById<TextView>(Resource.Id.txtViewFecha);
                holder.txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora);
                convertView.Tag = holder;
            }
        }

        private class Holder : Java.Lang.Object
        {
            public TextView txtViewMaterial { get; set; }
            public TextView txtViewCantidad { get; set; }
            public TextView txtViewUnidad { get; set; }
            public TextView txtViewLoteSap { get; set; }
            public TextView txtViewLoteSup { get; set; }
            public TextView txtViewFecha { get; set; }
            public TextView txtViewHora { get; set; }
        }

        private class ChildHolder : Java.Lang.Object
        {
            public TextView txtViewFecha { get; set; }
            public TextView txtViewHora { get; set; }
            public TextView txtViewTurn { get; set; }
            public TextView txtViewCantidad { get; set; }
            public TextView txtViewBoxNumber { get; set; }
            public TextView txtViewUser { get; set; }
            public TextView txtViewReason { get; set; }
        }

        private class Stocks : Java.Lang.Object
        {
            public Int32 ID { get; set; }
            public String Name { get; set; }
            public String Code { get; set; }
            public String Reference { get; set; }
            public String Unit { get; set; }
            public Single Quantity { get; set; }
            public String _MaterialName { get; set; }
            public String _MaterialCode { get; set; }
            public String Lot { get; set; }
            public String LotSuplidor { get; set; }
            public DateTime Fecha { get; set; }
            public List<Detail> Details { get; set; }

            public class Detail
            {
                public DateTime Fecha { get; set; }
                public String Reason { get; set; }
                public Byte TurnID { get; set; }
                public String Logon { get; set; }
                public Single Quantity { get; set; }
                public Single Total { get; set; }
                public Int32 BoxNumber { get; set; }
            }
        }
    }
}