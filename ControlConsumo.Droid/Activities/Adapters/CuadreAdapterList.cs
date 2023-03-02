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
using ControlConsumo.Shared.Models.Stock;
using ControlConsumo.Shared.Models.R;
using ControlConsumo.Droid.Activities.Widgets;
using Android.Support.V7.Widget;
using ControlConsumo.Droid.Activities.Adapters.Entities;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class CuadreAdapterList : RecyclerView.Adapter
    {
        private readonly Boolean IsNotify;
        private readonly Context context;
        public readonly List<MaterialReport> lista;

        public CuadreAdapterList(Context context, IEnumerable<MaterialReport> lista, Boolean IsNotify)
        {
            this.IsNotify = IsNotify;
            this.context = context;
            this.lista = lista.ToList();
        }

        public override int ItemCount
        {
            get { return lista.Count() + 1; }
        }
        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CuadreAdapterHolder viewHolder = holder as CuadreAdapterHolder;
           
            if (position == 0)
            {
                viewHolder.chkActivar.Visibility = ViewStates.Invisible;

                viewHolder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                viewHolder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                viewHolder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.txtViewUnidad.Text = context.GetString(Resource.String.ReportTitleUnidad);
                viewHolder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                viewHolder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.txtViewUnidadView.Text = context.GetString(Resource.String.ReportTitleUnidad);
                viewHolder.txtViewUnidadView.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                viewHolder.txtViewUnidadView.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.txtViewCaja.Text = context.GetString(Resource.String.ReportTitleCaja);
                viewHolder.txtViewCaja.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                viewHolder.txtViewCaja.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.txtViewLoteSap.Text = context.GetString(Resource.String.ReportTitleLote);
                viewHolder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                viewHolder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleFinal);
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
                viewHolder.txtViewMaterial.Text = lista[position - 1]._MaterialName;
                viewHolder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                viewHolder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.txtViewCaja.Text = lista[position - 1].BoxNumber > 0 ? lista[position - 1].BoxNumber.ToString() : String.Empty;
                viewHolder.txtViewCaja.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                viewHolder.txtViewCaja.SetTextColor(Android.Graphics.Color.Black);
                           
                viewHolder.txtViewLoteSap.Text = lista[position - 1].Lot;
                viewHolder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                viewHolder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);
                                               
                viewHolder.txtViewUnidad.Text = lista[position - 1].MaterialUnit;
                viewHolder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                viewHolder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.txtViewUnidadView.Text = lista[position - 1].NeedPercent ? "%" : lista[position - 1].MaterialUnit;
                viewHolder.txtViewUnidadView.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                viewHolder.txtViewUnidadView.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.txtViewCantidad.Text = lista[position - 1].Quantity.ToString();
                viewHolder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                viewHolder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                viewHolder.EditCantidad.AfterTextChanged += EditCantidad_AfterTextChanged;

                viewHolder.chkActivar.Click += chkActivar_Click;

                viewHolder.chkActivar.Tag = holder;
                viewHolder.EditCantidad.Tag = holder;

                if (IsNotify)
                {
                    viewHolder.chkActivar.Visibility = ViewStates.Gone;
                    viewHolder.EditCantidad.Visibility = ViewStates.Gone;
                    viewHolder.txtViewUnidad.Visibility = ViewStates.Gone;
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflar el Recycler View para el elemento:
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.adapter_cuadre_view, parent, false);

            //Crear un ViewHolder para sostener las referencias de la vista dentro del Recycler View:
            CuadreAdapterHolder viewHolder = new CuadreAdapterHolder(itemView);
            return viewHolder;

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
            public CheckBox chkActivar { get; private set; }

            public CuadreAdapterHolder(View itemView) : base(itemView)
            {

                txtViewMaterial = itemView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                txtViewCaja = itemView.FindViewById<TextView>(Resource.Id.txtViewCaja);
                txtViewCantidad = itemView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                txtViewLoteSap = itemView.FindViewById<TextView>(Resource.Id.txtViewLoteSap);
                EditCantidad = itemView.FindViewById<EditText>(Resource.Id.EditCantidad);
                txtViewUnidad = itemView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                txtViewUnidadView = itemView.FindViewById<TextView>(Resource.Id.txtViewUnidadView);
                chkActivar = itemView.FindViewById<CheckBox>(Resource.Id.chkActivar);
            }
        }


        private void chkActivar_Click(object sender, EventArgs e)
        {
            var obj = sender as CheckBox;
            var holder = obj.Tag as CuadreAdapterHolder;

            holder.EditCantidad.Enabled = obj.Checked;
            holder.EditCantidad.RequestFocus();
            holder.EditCantidad.SetBackgroundResource(obj.Checked ? Resource.Drawable.bg_input_white : Resource.Drawable.selector_input_text);

            //if (!obj.Checked) lista[holder.Position].Quantity = 0;
        }

        private void EditCantidad_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            var obj = sender as EditText;
            var holder = obj.Tag as CuadreAdapterHolder;
            var position = holder.AdapterPosition - 1;

            if (holder != null)
            {
                if (lista[position].NeedPercent && obj.Text.ToNumeric() > 1)
                {
                    new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.DialogClosedNoMax), "100%"));
                    obj.Text = String.Empty;
                    return;
                }
                else if (!lista[position].NeedPercent && obj.Text.ToNumeric() > lista[position].Acumulated && !String.IsNullOrEmpty(lista[position].TrayID))
                {
                    //Validación de cantidad remanente de cigarros de bandeja vs. cantidad de cigarros correspondiente al tipo de bandeja.
                    new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.DialogClosedNoMax), lista[position].Acumulated.ToString("N3")));
                    obj.Text = String.Empty;
                    return;
                }

                lista[position].EntryQuantity = obj.Text.ToNumeric();
            }
        }
    }
}