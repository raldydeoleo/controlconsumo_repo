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
using ControlConsumo.Droid.Activities.Adapters.Entities;
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Droid.Activities.Widgets;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class TicketsAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly LayoutInflater Inflater;
        private readonly List<TicketEntry> Entries;
        private readonly RepositoryR repor = new RepositoryR(Util.GetConnection());

        public TicketsAdapter(Context context, TicketReport Ticket)
        {
            this.context = context;
            this.Inflater = LayoutInflater.From(context);
            this.Entries = GetList(Ticket);
        }

        public override int Count
        {
            get { return Entries.Count(); }
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
            var pos = Entries.ElementAt(position);

            switch (pos.Field)
            {
                case TicketEntry.Fields.Divider:

                    convertView = Inflater.Inflate(Resource.Layout.question_dialog, null);
                    convertView.FindViewById<TextView>(Resource.Id.txtQuestion).Visibility = ViewStates.Invisible;

                    break;

                case TicketEntry.Fields.HeaderTitle:

                    var headHolder = GetHeadHolder(ref convertView);

                    var nextpos = Entries.ElementAt(position + 1);

                    headHolder.txtViewBandeja.Text = nextpos.TrayID;
                    headHolder.txtViewBandeja.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewBandeja.SetTextColor(Android.Graphics.Color.White);
                    headHolder.txtViewBandeja.SetBackgroundColor(Android.Graphics.Color.DarkBlue);

                    headHolder.txtViewProduct.Text = String.Format("{0} - {1}", nextpos.ProductCode, nextpos.ProductName);
                    headHolder.txtViewProduct.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewProduct.SetTextColor(Android.Graphics.Color.White);
                    headHolder.txtViewProduct.SetBackgroundColor(Android.Graphics.Color.DarkBlue);

                    headHolder.txtViewShort.Text = nextpos.ProductShort;
                    headHolder.txtViewShort.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewShort.SetTextColor(Android.Graphics.Color.White);
                    headHolder.txtViewShort.SetBackgroundColor(Android.Graphics.Color.DarkBlue);

                    headHolder.txtViewBatchID.Text = context.GetString(Resource.String.ReportTitleBatchID);
                    headHolder.txtViewBatchID.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    headHolder.txtViewBatchID.SetTextColor(Android.Graphics.Color.Black);
                    headHolder.txtViewBatchID.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    headHolder.txtViewEquipment.Text = context.GetString(Resource.String.ReportTitleEquipment);
                    headHolder.txtViewEquipment.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    headHolder.txtViewEquipment.SetTextColor(Android.Graphics.Color.Black);
                    headHolder.txtViewEquipment.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    headHolder.txtViewPeso.Text = context.GetString(Resource.String.ReportTitlePeso);
                    headHolder.txtViewPeso.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    headHolder.txtViewPeso.SetTextColor(Android.Graphics.Color.Black);
                    headHolder.txtViewPeso.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    headHolder.txtViewTurn.Text = context.GetString(Resource.String.ReportTitleTurn);
                    headHolder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    headHolder.txtViewTurn.SetTextColor(Android.Graphics.Color.Black);
                    headHolder.txtViewTurn.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    headHolder.txtViewFecha.Text = context.GetString(Resource.String.ReportTitleFechaSalida);
                    headHolder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    headHolder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);
                    headHolder.txtViewFecha.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    headHolder.txtViewHora.Text = context.GetString(Resource.String.ReportTitleHoraSalida);
                    headHolder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    headHolder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);
                    headHolder.txtViewHora.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    headHolder.txtViewTraza.Text = context.GetString(Resource.String.ReportTitleTraza);
                    headHolder.txtViewTraza.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    headHolder.txtViewTraza.SetTextColor(Android.Graphics.Color.Black);
                    headHolder.txtViewTraza.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    break;

                case TicketEntry.Fields.Header:

                    headHolder = GetHeadHolder(ref convertView);

                    headHolder.txtViewBandeja.Visibility = ViewStates.Gone;
                    headHolder.txtViewBandeja.Text = pos.TrayID;
                    headHolder.txtViewBandeja.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewBandeja.SetTextColor(Android.Graphics.Color.Black);

                    headHolder.txtViewProduct.Visibility = ViewStates.Gone;
                    headHolder.txtViewProduct.Text = pos.ProductCode;
                    headHolder.txtViewProduct.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewProduct.SetTextColor(Android.Graphics.Color.Black);

                    headHolder.txtViewShort.Visibility = ViewStates.Gone;
                    headHolder.txtViewShort.Text = pos.ProductShort;
                    headHolder.txtViewShort.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewShort.SetTextColor(Android.Graphics.Color.Black);

                    headHolder.txtViewBatchID.Text = Util.MaskBatchID(pos.BatchID);
                    headHolder.txtViewBatchID.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewBatchID.SetTextColor(Android.Graphics.Color.Black);

                    headHolder.txtViewEquipment.Text = pos.EquipmentID;
                    headHolder.txtViewEquipment.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewEquipment.SetTextColor(Android.Graphics.Color.Black);

                    headHolder.txtViewPeso.Text = pos.Quantity2 > 0 ? pos.Quantity2.ToString("N3") : String.Empty;
                    headHolder.txtViewPeso.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewPeso.SetTextColor(Android.Graphics.Color.Black);

                    headHolder.txtViewTurn.Text = pos.TurnID.ToString();
                    headHolder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewTurn.SetTextColor(Android.Graphics.Color.Black);

                    headHolder.txtViewFecha.Text = pos.Produccion.Value.ToString("dd MMM yyyy");
                    headHolder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);

                    headHolder.txtViewHora.Text = pos.Produccion.Value.ToString("hh:mm tt");
                    headHolder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);

                    headHolder.txtViewTraza.Text = pos.Traza;
                    headHolder.txtViewTraza.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    headHolder.txtViewTraza.SetTextColor(Android.Graphics.Color.Black);

                    break;

                case TicketEntry.Fields.DetailTitle:

                    GetHolder(ref convertView);

                    var holder = convertView.Tag as Holder;

                    holder.txtViewHeaderDetail.Visibility = ViewStates.Visible;
                    holder.txtViewHeaderDetail.SetBackgroundColor(Android.Graphics.Color.DarkBlue);

                    holder.txtViewCajaNo.Text = context.GetString(Resource.String.ReportTitleCaja);
                    holder.txtViewCajaNo.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewCajaNo.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewCajaNo.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    holder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                    holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewMaterial.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    holder.txtViewDescripcion.Text = context.GetString(Resource.String.ReportTitleDescripcion);
                    holder.txtViewDescripcion.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewDescripcion.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewDescripcion.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    holder.txtViewLoteSupl.Text = context.GetString(Resource.String.ReportTitleLoteSupl);
                    holder.txtViewLoteSupl.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewLoteSupl.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewLoteSupl.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    holder.txtViewLote.Text = context.GetString(Resource.String.ReportTitleLote);
                    holder.txtViewLote.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewLote.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewLote.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    holder.txtViewTurn.Text = context.GetString(Resource.String.ReportTitleTurn);
                    holder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewTurn.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewTurn.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    holder.txtViewFecha.Text = context.GetString(Resource.String.ReportTitleFecha);
                    holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewFecha.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    holder.txtViewHora.Text = context.GetString(Resource.String.ReportTitleHora);
                    holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewHora.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    holder.imgShowOther.Visibility = ViewStates.Invisible;
                    holder.imgHideOther.Visibility = ViewStates.Gone;

                    break;

                case TicketEntry.Fields.Detail:

                    GetHolder(ref convertView);

                    holder = convertView.Tag as Holder;

                    holder.txtViewHeaderDetail.Visibility = ViewStates.Gone;

                    holder.txtViewCajaNo.Text = pos.BoxNo > 0 ? pos.BoxNo.ToString() : String.Empty;
                    holder.txtViewCajaNo.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewCajaNo.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewCajaNo.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewMaterial.Text = pos._DisplayCode;
                    holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewMaterial.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewDescripcion.Text = pos.MaterialName.ToString();
                    holder.txtViewDescripcion.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewDescripcion.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewDescripcion.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewLoteSupl.Text = pos.LotReference;
                    holder.txtViewLoteSupl.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewLoteSupl.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewLoteSupl.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewLote.Text = pos.Lot;
                    holder.txtViewLote.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewLote.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewLote.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewTurn.Text = pos.TurnID.ToString();
                    holder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewTurn.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewTurn.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewFecha.Text = pos.Produccion.Value.ToString("dd/MM/yy");
                    holder.txtViewFecha.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewFecha.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewFecha.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewHora.Text = pos.Produccion.Value.ToString("hh:mm tt");
                    holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewHora.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    if (!pos._IsSemiElaborate)
                    {
                        holder.imgShowOther.Visibility = ViewStates.Gone;
                        holder.imgHideOther.Visibility = ViewStates.Invisible;
                    }
                    else if (pos._IsSemiElaborate && !pos.IsNotShow)
                    {
                        holder.imgShowOther.Visibility = ViewStates.Visible;
                        holder.imgHideOther.Visibility = ViewStates.Gone;
                    }
                    else
                    {
                        holder.imgShowOther.Visibility = ViewStates.Gone;
                        holder.imgHideOther.Visibility = ViewStates.Visible;
                    }

                    if (pos.Fecha2.HasValue)
                    {
                        holder.TrayID = pos.TrayID;
                        holder.Equipo = pos.EquipmentID3;
                        holder.Fecha = pos.Fecha2.Value;
                        holder.SecSalida = pos.SecSalida;
                        holder.Position = position;
                    }

                    holder.imgShowOther.Tag = holder;
                    holder.imgHideOther.Tag = holder;

                    break;
            }

            return convertView;
        }

        private List<TicketEntry> GetList(TicketReport ticket, Boolean AddDivider = false)
        {
            var lista = new List<TicketEntry>();

            var level = Entries != null ? Entries.Max(p => p.Level) + 1 : 0;

            if (AddDivider) lista.Add(new TicketEntry() { Field = TicketEntry.Fields.Divider, Level = level });

            lista.Add(new TicketEntry() { Field = TicketEntry.Fields.HeaderTitle, Level = level });

            lista.Add(new TicketEntry()
            {
                Level = level,
                Field = TicketEntry.Fields.Header,
                EquipmentID = ticket.EquipmentID,
                ProductCode = ticket._DisplayCode,
                ProductName = ticket.ProductName,
                ProductShort = ticket.ProductShort,
                VerID = ticket.VerID,
                Lot = ticket.Lot,
                SecSalida = ticket.SecSalida,
                Produccion = ticket.Produccion,
                TurnID = ticket.TurnID,
                TrayID = ticket.TrayID,
                BatchID = ticket.BatchID,
                Status = ticket.Status,
                Quantity = ticket.Quantity,
                Quantity2 = ticket.Quantity2,
                MaterialUnit = ticket.Unit,
                Traza = ticket.Traza
            });

            if (ticket.Details.Any())
                lista.Add(new TicketEntry() { Field = TicketEntry.Fields.DetailTitle, Level = level });

            lista.AddRange(ticket.Details.OrderBy(p => p._DisplayCode).GroupBy(g => new
            {
                g.MaterialCode,
                g.MaterialReference,
                g.MaterialName,
                g.MaterialShort,
                g.MaterialUnit,
                g.EquipmentID,
                g.TimeID,
                g.Lot,
                g.LotReference
            })
            .Select(p => new TicketEntry()
            {
                Level = level,
                Field = TicketEntry.Fields.Detail,
                MaterialCode = p.Key.MaterialCode,
                MaterialReference = p.Key.MaterialReference,
                MaterialName = p.Key.MaterialName,
                MaterialShort = p.Key.MaterialShort,
                MaterialUnit = p.Key.MaterialUnit,
                EquipmentID = p.Key.EquipmentID,
                TimeID = p.Key.TimeID,
                Lot = p.Key.Lot,
                LotReference = p.Key.LotReference,
                BoxNo = p.Where(f => f.Fecha < ticket.Fecha).OrderByDescending(o => o.Fecha).First().BoxNo,
                Produccion = p.Where(f => f.Fecha < ticket.Fecha).OrderByDescending(o => o.Fecha).First().Produccion,
                Fecha = p.Where(f => f.Fecha < ticket.Fecha).OrderByDescending(o => o.Fecha).First().Fecha,
                TrayID = p.Where(f => f.Fecha < ticket.Fecha).OrderByDescending(o => o.Fecha).First().TrayID,
                EquipmentID3 = p.Where(f => f.Fecha < ticket.Fecha).OrderByDescending(o => o.Fecha).First().EquipmentID3,
                SecSalida = p.Where(f => f.Fecha < ticket.Fecha).OrderByDescending(o => o.Fecha).First().SecSalida,
                Fecha2 = p.Where(f => f.Fecha < ticket.Fecha).OrderByDescending(o => o.Fecha).First().Fecha2,
                BatchID = p.Where(f => f.Fecha < ticket.Fecha).OrderByDescending(o => o.Fecha).First().BatchID,
                TurnID = p.Where(f => f.Fecha < ticket.Fecha).OrderByDescending(o => o.Fecha).First().TurnID
            }));

            return lista;
        }

        private HeadHolder GetHeadHolder(ref View convertView)
        {
            convertView = Inflater.Inflate(Resource.Layout.adapter_report_ticket_header, null);
            return new HeadHolder()
            {
                txtViewBandeja = convertView.FindViewById<TextView>(Resource.Id.txtViewBandeja),
                txtViewProduct = convertView.FindViewById<TextView>(Resource.Id.txtViewProduct),
                txtViewShort = convertView.FindViewById<TextView>(Resource.Id.txtViewShort),
                txtViewBatchID = convertView.FindViewById<TextView>(Resource.Id.txtViewBatchID),
                txtViewEquipment = convertView.FindViewById<TextView>(Resource.Id.txtViewEquipment),
                txtViewPeso = convertView.FindViewById<TextView>(Resource.Id.txtViewPeso),
                txtViewTurn = convertView.FindViewById<TextView>(Resource.Id.txtViewTurn),
                txtViewFecha = convertView.FindViewById<TextView>(Resource.Id.txtViewFecha),
                txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora),
                txtViewTraza = convertView.FindViewById<TextView>(Resource.Id.txtViewTraza)
            };
        }

        private void GetHolder(ref View convertView)
        {
            if (convertView == null || convertView.Tag == null)
            {
                var holder = new Holder();
                convertView = Inflater.Inflate(Resource.Layout.adapter_report_ticket_detail, null);
                holder.txtViewHeaderDetail = convertView.FindViewById<TextView>(Resource.Id.txtViewHeaderDetail);
                holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                holder.txtViewDescripcion = convertView.FindViewById<TextView>(Resource.Id.txtViewDescripcion);
                holder.txtViewLoteSupl = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSupl);
                holder.txtViewLote = convertView.FindViewById<TextView>(Resource.Id.txtViewLote);
                holder.txtViewCajaNo = convertView.FindViewById<TextView>(Resource.Id.txtViewCajaNo);
                holder.txtViewTurn = convertView.FindViewById<TextView>(Resource.Id.txtViewTurn);
                holder.txtViewFecha = convertView.FindViewById<TextView>(Resource.Id.txtViewFecha);
                holder.txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora);
                holder.imgShowOther = convertView.FindViewById<ImageButton>(Resource.Id.imgShowOther);
                holder.imgHideOther = convertView.FindViewById<ImageButton>(Resource.Id.imgHideOther);
                holder.imgShowOther.Click += imgShowOther_Click;
                holder.imgHideOther.Click += imgHideOther_Click;
                convertView.Tag = holder;
            }
        }

        private void imgHideOther_Click(object sender, EventArgs e)
        {
            var obj = sender as ImageButton;
            var holder = obj.Tag as Holder;

            if (holder != null)
            {
                var level = Entries[holder.Position].Level;
                Entries[holder.Position].IsNotShow = false;
                Entries.RemoveAll(r => r.Level > level);
                NotifyDataSetChanged();
            }
        }

        private async void imgShowOther_Click(object sender, EventArgs e)
        {
            var obj = sender as ImageButton;
            var holder = obj.Tag as Holder;

            if (holder != null)
            {
                var tickets = await repor.GetTicketReport(holder.Equipo, holder.Fecha, holder.SecSalida, holder.TrayID);

                if (tickets == null)
                {
                    new CustomDialog(context, CustomDialog.Status.Warning, context.GetString(Resource.String.ReportTitleNoProductTraza));
                    return;
                }
                Entries.AddRange(GetList(tickets, true));
                Entries[holder.Position].IsNotShow = true;
                NotifyDataSetChanged();
            }
        }

        private class Holder : Java.Lang.Object
        {
            public Int32 Position { get; set; }
            public String Equipo { get; set; }
            public DateTime Fecha { get; set; }
            public Int16 SecSalida { get; set; }
            public String TrayID { get; set; }
            public TextView txtViewHeaderDetail { get; set; }
            public TextView txtViewMaterial { get; set; }
            public TextView txtViewDescripcion { get; set; }
            public TextView txtViewLoteSupl { get; set; }
            public TextView txtViewLote { get; set; }
            public TextView txtViewCajaNo { get; set; }
            public TextView txtViewTurn { get; set; }
            public TextView txtViewFecha { get; set; }
            public TextView txtViewHora { get; set; }
            public ImageButton imgShowOther { get; set; }
            public ImageButton imgHideOther { get; set; }
        }

        private class HeadHolder
        {
            public TextView txtViewBandeja { get; set; }
            public TextView txtViewProduct { get; set; }
            public TextView txtViewShort { get; set; }
            public TextView txtViewBatchID { get; set; }
            public TextView txtViewEquipment { get; set; }
            public TextView txtViewPeso { get; set; }
            public TextView txtViewTurn { get; set; }
            public TextView txtViewFecha { get; set; }
            public TextView txtViewHora { get; set; }
            public TextView txtViewTraza { get; set; }
        }
    }
}