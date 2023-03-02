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
using ControlConsumo.Droid.Managers;
using ControlConsumo.Shared.Repositories;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class ReportAdapterProduccionSap : BaseAdapter
    {
        private readonly Context context;
        private readonly LayoutInflater Inflater;
        private readonly IEnumerable<ElaboratesReport> lista;
        private readonly Byte TimeID;
        private readonly RepositoryZ repoz = new RepositoryFactory(Util.GetConnection()).GetRepositoryZ();

        public ReportAdapterProduccionSap(Context context, IEnumerable<ElaboratesReport> lista, Byte TimeID)
        {
            this.context = context;
            this.Inflater = LayoutInflater.From(context);
            this.lista = lista;
            this.TimeID = TimeID;
        }

        public override int Count
        {
            get { return lista.Count(); }
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
                convertView = Inflater.Inflate(Resource.Layout.adapter_report_produccion_sap, null);
                holder.txtViewProduccion = convertView.FindViewById<TextView>(Resource.Id.txtViewProduccion);
                holder.imgLittleCar = convertView.FindViewById<ImageView>(Resource.Id.imgLittleCar);
                holder.imgLittleBox = convertView.FindViewById<ImageView>(Resource.Id.imgLittleBox);
                holder.txtViewTurn = convertView.FindViewById<TextView>(Resource.Id.txtViewTurn);
                holder.txtViewBandejaConsumo = convertView.FindViewById<TextView>(Resource.Id.txtViewBandejaConsumo);
                holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                holder.txtViewBatchID = convertView.FindViewById<TextView>(Resource.Id.txtViewBatchID);
                holder.txtViewBandejas = convertView.FindViewById<TextView>(Resource.Id.txtViewBandejas);
                holder.txtViewPeso = convertView.FindViewById<TextView>(Resource.Id.txtViewPeso);
                holder.txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora);
                holder.txtViewEquipo = convertView.FindViewById<TextView>(Resource.Id.txtViewEquipo);
                holder.txtViewFecha1 = convertView.FindViewById<TextView>(Resource.Id.txtViewFecha1);
                holder.txtViewHora1 = convertView.FindViewById<TextView>(Resource.Id.txtViewHora1);
                convertView.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as Holder;
            }

            var pos = lista.ElementAt(position);

            var material = repoz.GetMaterialByCode(pos.ProductCode);

            holder.imgLittleCar.Visibility = ViewStates.Gone;
            holder.imgLittleBox.Visibility = ViewStates.Gone;

            holder.txtViewProduccion.Text = pos.Produccion.ToString("dd/MM/yy");
            holder.txtViewProduccion.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewProduccion.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewProduccion.SetBackgroundResource(Resource.Drawable.bg_input_gray);

            holder.txtViewTurn.Text = pos.TurnID.ToString();
            holder.txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewTurn.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewTurn.SetBackgroundResource(Resource.Drawable.bg_input_gray);

            holder.txtViewBandejaConsumo.Text = pos.TrayID2;
            holder.txtViewBandejaConsumo.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewBandejaConsumo.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewBandejaConsumo.SetBackgroundResource(Resource.Drawable.bg_input_gray);

            holder.txtViewMaterial.Text = material._Short;
            holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewMaterial.SetBackgroundResource(Resource.Drawable.bg_input_gray);

            holder.txtViewBatchID.Text = Util.MaskBatchID(pos.BatchID);
            holder.txtViewBatchID.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewBatchID.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewBatchID.SetBackgroundResource(Resource.Drawable.bg_input_gray);

            holder.txtViewBandejas.Text = String.IsNullOrEmpty(pos.TrayID) ? pos.Empaque : pos.TrayID;
            holder.txtViewBandejas.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewBandejas.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewBandejas.SetBackgroundResource(Resource.Drawable.bg_input_gray);

            holder.txtViewPeso.Text = pos.Peso > 0 ? pos.Peso.ToString("N3") : pos.SecuenciaEmpaque.ToString();
            holder.txtViewPeso.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewPeso.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewPeso.SetBackgroundResource(Resource.Drawable.bg_input_gray);

            holder.txtViewHora.Text = pos.Produccion.ToString("hh:mm tt");
            holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewHora.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewHora.SetBackgroundResource(Resource.Drawable.bg_input_gray);

            holder.txtViewFecha1.Visibility = ViewStates.Visible;
            holder.txtViewFecha1.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewFecha1.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewFecha1.SetBackgroundResource(Resource.Drawable.bg_input_gray);
            holder.txtViewFecha1.Text = String.Empty;

            holder.txtViewHora1.Visibility = ViewStates.Visible;
            holder.txtViewHora1.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewHora1.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewHora1.SetBackgroundResource(Resource.Drawable.bg_input_gray);
            holder.txtViewHora1.Text = String.Empty;

            holder.txtViewEquipo.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewEquipo.SetTextColor(Android.Graphics.Color.Black);
            holder.txtViewEquipo.Text = String.Empty;

            switch (pos.Status)
            {
                case Shared.Tables.ProductsRoutes.RoutesStatus.EnTransito:
                    holder.txtViewEquipo.SetBackgroundResource(Resource.Drawable.bg_input_gray_yellow);
                    break;

                case Shared.Tables.ProductsRoutes.RoutesStatus.EnEquipo:
                    holder.txtViewEquipo.SetBackgroundResource(Resource.Drawable.bg_input_gray_blue);
                    break;

                case Shared.Tables.ProductsRoutes.RoutesStatus.Procesado:
                case Shared.Tables.ProductsRoutes.RoutesStatus.Cancelada:
                    holder.txtViewEquipo.SetBackgroundResource(Resource.Drawable.bg_input_gray_green);
                    break;
            }

            if (!String.IsNullOrEmpty(pos.EquipmentID2) && pos.Status != Shared.Tables.ProductsRoutes.RoutesStatus.EnTransito)
            {
                holder.imgLittleCar.Visibility = ViewStates.Gone;
                holder.txtViewEquipo.Visibility = ViewStates.Visible;
                holder.txtViewEquipo.Text = pos.EquipmentID2;
                holder.txtViewFecha1.Text = pos.Fecha2.HasValue ? pos.Fecha2.Value.ToString("dd/MM/yy") : String.Empty;
                holder.txtViewHora1.Text = pos.Fecha2.HasValue ? pos.Fecha2.Value.ToString("hh:mm tt") : String.Empty;

                if (TimeID > 1)
                {
                    holder.txtViewPeso.Visibility = ViewStates.Gone;
                    holder.txtViewBandejaConsumo.Visibility = ViewStates.Visible;
                }
                else
                {
                    holder.txtViewBandejaConsumo.Visibility = ViewStates.Gone;
                }
            }
            else
            {
                holder.txtViewEquipo.Visibility = ViewStates.Gone;
                holder.txtViewFecha1.Visibility = ViewStates.Invisible;
                holder.txtViewHora1.Visibility = ViewStates.Invisible;
                holder.txtViewPeso.Visibility = ViewStates.Visible;

                if (TimeID > 1)
                {
                    holder.txtViewPeso.Visibility = ViewStates.Gone;
                    holder.txtViewBandejaConsumo.Visibility = ViewStates.Visible;
                }
                else
                {
                    holder.txtViewBandejaConsumo.Visibility = ViewStates.Gone;
                }

                if (pos.Status == Shared.Tables.ProductsRoutes.RoutesStatus.Cancelada)
                {
                    holder.txtViewFecha1.Text = pos.Fecha2.HasValue ? pos.Fecha2.Value.ToString("dd/MM/yy") : String.Empty;
                    holder.txtViewFecha1.Visibility = ViewStates.Visible;

                    holder.txtViewHora1.Text = pos.Fecha2.HasValue ? pos.Fecha2.Value.ToString("hh:mm tt") : String.Empty;
                    holder.txtViewHora1.Visibility = ViewStates.Visible;

                    holder.txtViewEquipo.Visibility = ViewStates.Visible;
                }
                else if (String.IsNullOrEmpty(pos.TrayID))
                {
                    holder.txtViewPeso.Visibility = ViewStates.Visible;
                    holder.imgLittleCar.Visibility = ViewStates.Visible;
                    holder.txtViewFecha1.Visibility = ViewStates.Gone;
                    holder.txtViewHora1.Visibility = ViewStates.Gone;
                }
                else
                {
                    holder.imgLittleBox.Visibility = ViewStates.Visible;
                }
            }

            return convertView;
        }

        private class Holder : Java.Lang.Object
        {
            public ImageView imgLittleCar { get; set; }
            public ImageView imgLittleBox { get; set; }
            public TextView txtViewProduccion { get; set; }
            public TextView txtViewTurn { get; set; }
            public TextView txtViewMaterial { get; set; }
            public TextView txtViewBatchID { get; set; }
            public TextView txtViewBandejas { get; set; }
            public TextView txtViewPeso { get; set; }
            public TextView txtViewHora { get; set; }
            public TextView txtViewEquipo { get; set; }
            public TextView txtViewFecha1 { get; set; }
            public TextView txtViewHora1 { get; set; }
            public TextView txtViewBandejaConsumo { get; set; }
        }
    }
}