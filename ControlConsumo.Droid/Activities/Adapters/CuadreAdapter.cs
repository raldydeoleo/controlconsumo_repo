using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using Android.Widget;
using ControlConsumo.Shared.Models.R;
using ControlConsumo.Droid.Activities.Widgets;
using Android.Views.InputMethods;
using ControlConsumo.Droid.Managers;
using Android.Support.V7.Widget;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class CuadreAdapter : RecyclerView.Adapter
    {
        private readonly Context context;
        public readonly List<MaterialReport> Materiales;
        private Boolean ReadOnly;
        private Boolean isSubEquipment;

        public CuadreAdapter(Context context, IEnumerable<MaterialReport> Materiales, Boolean ReadOnly, Boolean isSubEquipment)
        {
            this.context = context;
            this.ReadOnly = ReadOnly;
            this.Materiales = Materiales.ToList();
            this.isSubEquipment = isSubEquipment;
        }
        
        public override int ItemCount
        {
            get { return Materiales.Count() + 1; }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CuadreAdapterHolder viewHolder = holder as CuadreAdapterHolder;

            if (position == 0)
            {
                if (isSubEquipment)
                {
                    viewHolder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleProducto);
                }
                else
                {
                    viewHolder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                }
                viewHolder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                viewHolder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.txtViewUnidad.Text = context.GetString(Resource.String.ReportTitleUnidad);
                viewHolder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                viewHolder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                if (!isSubEquipment)
                {
                    viewHolder.txtViewCaja.Text = context.GetString(Resource.String.ReportTitleCaja);
                    viewHolder.txtViewCaja.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    viewHolder.txtViewCaja.SetTextColor(Android.Graphics.Color.Black);

                    viewHolder.txtViewLoteSap.Text = context.GetString(Resource.String.ReportTitleLote);
                    viewHolder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    viewHolder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);
                }

                if (isSubEquipment)
                {
                    viewHolder.txtViewCaja.Visibility = ViewStates.Gone;
                    viewHolder.txtViewLoteSap.Visibility = ViewStates.Gone;
                    viewHolder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleUltimaCantidad);
                }

                if (!isSubEquipment)
                {
                    viewHolder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleConsumed);
                }

                viewHolder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                viewHolder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.EditCantidad.Enabled = false;
                viewHolder.EditCantidad.Text = context.GetString(Resource.String.ReportTitleCantidad);
                viewHolder.EditCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                viewHolder.EditCantidad.SetTextColor(Android.Graphics.Color.Black);
                viewHolder.EditCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);
            }
            else
            {
                viewHolder.txtViewMaterial.Text = Materiales[position -1].MaterialName;
                viewHolder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                viewHolder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.txtViewUnidad.Text = Materiales[position - 1].NeedPercent && !ReadOnly ? "%" : Materiales[position - 1].MaterialUnit;
                viewHolder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                viewHolder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                if (!isSubEquipment)
                {
                    viewHolder.txtViewCaja.Text = Materiales[position - 1].BoxNumber > 0 ? Materiales[position - 1].BoxNumber.ToString() : String.Empty;
                    viewHolder.txtViewCaja.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    viewHolder.txtViewCaja.SetTextColor(Android.Graphics.Color.Black);

                    viewHolder.txtViewLoteSap.Text = Materiales[position - 1].Lot;
                    viewHolder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    viewHolder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);
                }

                if (isSubEquipment)
                {
                    viewHolder.txtViewCaja.Visibility = ViewStates.Gone;
                    viewHolder.txtViewLoteSap.Visibility = ViewStates.Gone;
                }

                viewHolder.txtViewCantidad.Text = Materiales[position - 1].EntryQuantity.ToString("N3");
                viewHolder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                viewHolder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.EditCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                viewHolder.EditCantidad.SetTextColor(Android.Graphics.Color.Black);

                if (ReadOnly)
                {
                    viewHolder.EditCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);
                    viewHolder.EditCantidad.Enabled = false;
                    viewHolder.EditCantidad.Text = Materiales[position - 1].Quantity.ToString("N3");
                }
                else
                {
                    viewHolder.EditCantidad.Enabled = true;
                    viewHolder.EditCantidad.Focusable = true;
                    viewHolder.EditCantidad.SetBackgroundResource(Resource.Drawable.bg_input_white);
                    viewHolder.EditCantidad.Text = Materiales[position - 1].Quantity > 0 ? Materiales[position - 1].Quantity.ToString() : String.Empty;
                    viewHolder.EditCantidad.SetSelection(viewHolder.EditCantidad.Text.Length);
                    viewHolder.EditCantidad.TextChanged += EditCantidad_TextChanged;
                }

                viewHolder.EditCantidad.Tag = holder;
            }

            if (ReadOnly)
            {
                viewHolder.txtViewCantidad.Visibility = ViewStates.Gone;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflar el Recycler View para el elemento:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.adapter_cuadre, parent, false);

            //Crear un ViewHolder para sostener las referencias de la vista dentro del Recycler View:
            CuadreAdapterHolder viewHolder = new CuadreAdapterHolder(itemView);
            return viewHolder;
        }
        
         private void EditCantidad_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
         {
            var obj = sender as EditText;
            var holder = obj.Tag as CuadreAdapterHolder;
            var position = holder.AdapterPosition - 1;

             if (holder != null && !ReadOnly)
             {
                if (Materiales[position].NeedPercent && obj.Text.ToNumeric() > 1)
                {
                    new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.DialogClosedNoMax), "100%"));
                    obj.Text = String.Empty;
                    return;
                }
                else if (!Materiales[position].NeedPercent && obj.Text.ToNumeric() > Materiales[position].EntryQuantity)
                {
                    new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.DialogClosedNoMax), Materiales[position].EntryQuantity.ToString("N3")));
                    obj.Text = String.Empty;
                    return;
                }

                Materiales[position].Quantity = obj.Text.ToNumeric();
             }
         }
         

        private class CuadreAdapterHolder : RecyclerView.ViewHolder
        {
            public TextView txtViewMaterial { get; private set; }
            public TextView txtViewCaja { get; private set; }
            public TextView txtViewLoteSap { get; private set; }
            public TextView txtViewCantidad { get; private set; }
            public EditText EditCantidad { get; private set; }
            public TextView txtViewUnidad { get; private set; }
            public TextView txtViewUnidadView { get; private set; }

            public CuadreAdapterHolder(View itemView) : base(itemView)
            {

                txtViewMaterial = itemView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                txtViewCaja = itemView.FindViewById<TextView>(Resource.Id.txtViewCaja);
                txtViewCantidad = itemView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                txtViewLoteSap = itemView.FindViewById<TextView>(Resource.Id.txtViewLoteSap);
                EditCantidad = itemView.FindViewById<EditText>(Resource.Id.EditCantidad);
                txtViewUnidad = itemView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                txtViewUnidadView = itemView.FindViewById<TextView>(Resource.Id.txtViewUnidadView);
            }
        }
    }
}