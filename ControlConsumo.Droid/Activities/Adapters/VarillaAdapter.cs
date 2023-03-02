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
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Droid.Activities.Widgets;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class VarillaAdapter : BaseAdapter
    {
        private readonly Context context;
        public readonly List<MaterialReport> Materiales;
        private readonly LayoutInflater Inflater;
        private readonly RepositoryZ repoz = new RepositoryZ(Util.GetConnection());
        private readonly Byte TurnID;
        private readonly Int32 ProductionDate;

        public VarillaAdapter(Context context, IEnumerable<MaterialReport> Materiales, Byte TurnID, Int32 ProductionDate)
        {
            this.context = context;
            Inflater = LayoutInflater.From(context);
            this.Materiales = Materiales.ToList();
            this.TurnID = TurnID;
            this.ProductionDate = ProductionDate;
        }

        public override int Count
        {
            get { return Materiales.Count() + 1; }
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
                convertView = Inflater.Inflate(Resource.Layout.adapter_varilla, null);
                holder.chkActivar = convertView.FindViewById<CheckBox>(Resource.Id.chkActivar);
                holder.txtViewProduct = convertView.FindViewById<TextView>(Resource.Id.txtViewProduct);
                holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                holder.txtViewCantidad = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                holder.txtViewUnidad = convertView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                holder.EditCantidad = convertView.FindViewById<EditText>(Resource.Id.EditCantidad);
                holder.imgButtonPrint = convertView.FindViewById<ImageButton>(Resource.Id.imgButtonPrint);
                holder.imgButtonPrint.Click += imgButtonPrint_Click;
                holder.EditCantidad.AfterTextChanged += EditCantidad_AfterTextChanged;
                holder.chkActivar.Click += chkActivar_Click;
                convertView.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as Holder;
            }

            if (position == 0)
            {
                holder.chkActivar.Visibility = ViewStates.Invisible;

                holder.imgButtonPrint.Visibility = ViewStates.Invisible;

                holder.txtViewProduct.Text = context.GetString(Resource.String.ReportTitleProducto);
                holder.txtViewProduct.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewProduct.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewUnidad.Text = context.GetString(Resource.String.ReportTitleUnidad);
                holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleAcumulated);
                holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                holder.EditCantidad.Enabled = false;
                holder.EditCantidad.Text = context.GetString(Resource.String.ReportTitleCantidad);
                holder.EditCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.EditCantidad.SetTextColor(Android.Graphics.Color.Black);
                holder.EditCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);
            }
            else
            {
                var pos = Materiales.ElementAt(position - 1);

                holder.txtViewProduct.Text = pos.ProductShort ?? pos.ProductCode;
                holder.txtViewProduct.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewProduct.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewMaterial.Text = pos.MaterialName;
                holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewUnidad.Text = pos.MaterialUnit ?? pos.Unit;
                holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCantidad.Text = pos.Acumulated.ToString("N3");
                holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                holder.EditCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.EditCantidad.SetTextColor(Android.Graphics.Color.Black);

                if (holder.chkActivar.Checked)
                {
                    holder.EditCantidad.Text = pos.Quantity > 0 ? pos.Quantity.ToString() : String.Empty;
                    holder.EditCantidad.SetBackgroundResource(Resource.Drawable.bg_input_white);
                    holder.EditCantidad.Enabled = true;
                }
                else
                {
                    holder.EditCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);
                    holder.EditCantidad.Enabled = false;
                    holder.EditCantidad.Text = String.Empty;
                }

                if (pos.Acumulated > 0)
                {
                    holder.imgButtonPrint.Enabled = true;
                }
                else
                {
                    holder.imgButtonPrint.Enabled = false;
                }

                holder.Position = position - 1;
                holder.EditCantidad.Tag = holder;
                holder.chkActivar.Tag = holder;
                holder.imgButtonPrint.Tag = holder;
            }

            return convertView;
        }

        private async void imgButtonPrint_Click(object sender, EventArgs e)
        {
            var obj = sender as ImageButton;
            var holder = obj.Tag as Holder;

            if (holder != null)
            {
                var printer = new PrinterManager();
                var material = Materiales.ElementAt(holder.Position);

                Dialog lotdialog = null;
                var lotbuilder = new AlertDialog.Builder(context);
                lotbuilder.SetTitle(Resource.String.OutEntryPrintTitle);
                lotbuilder.SetIcon(Android.Resource.Drawable.IcMenuAgenda);
                var view = Inflater.Inflate(Resource.Layout.dialog_lots, null);
                var lstlots = view.FindViewById<ListView>(Resource.Id.lstlots);
                var detalle = await repoz.GetLastVarillas(ProductionDate, TurnID, material.MaterialCode);
                var adapter = new VarillaAdapterReprint(context, detalle);
                adapter.OnPrint += async (etiqueta) =>
               {
                   var lista = new List<Etiquetas>();
                   etiqueta.Fecha = material.Expire;
                  // etiqueta.Secuencia = material.BoxNumber;
                   lista.Add(etiqueta);
                   lotdialog.Dismiss();
                   lotdialog.Dispose();
                   await printer.ExecutePrint(context, lista);
               };

                lstlots.Adapter = adapter;
                lotbuilder.SetView(view);
                lotbuilder.SetNegativeButton(Resource.String.Cancel, (se, t) =>
                {
                    lotdialog.Dismiss();
                    lotdialog.Dispose();
                });

                lotdialog = lotbuilder.Create();

                lotdialog.Show();
            }
        }

        private void chkActivar_Click(object sender, EventArgs e)
        {
            var obj = sender as CheckBox;
            var holder = obj.Tag as Holder;

            if (obj.Checked)
            {
                var material = Materiales.ElementAt(holder.Position);
                var uihelp = new CustomDialog(context, CustomDialog.Status.Good, context.GetString(Resource.String.AlertScanMessage), CustomDialog.ButtonStyles.OneButtonScanCode);
                uihelp.SetMaterial(material);
                uihelp.OnCancelPress += () =>
                {
                    obj.Checked = false;
                    holder.EditCantidad.Enabled = false;
                    Materiales[holder.Position].Quantity = 0;
                    holder.EditCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);
                };
                uihelp.ONScanResult += (Result) =>
                {
                    holder.BarCode = Result;
                    holder.EditCantidad.Enabled = true;
                    holder.EditCantidad.SetBackgroundResource(Resource.Drawable.bg_input_white);
                };
            }
            else
            {
                holder.EditCantidad.Enabled = false;
                Materiales[holder.Position].Quantity = 0;
                holder.EditCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);
            }
        }

        private void EditCantidad_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            var obj = sender as EditText;
            var holder = obj.Tag as Holder;

            if (holder != null && holder.BarCode != null)
            {
                Materiales[holder.Position].Quantity = obj.Text.ToNumeric();
                Materiales[holder.Position].Lot = holder.BarCode.Lot;
            }
        }

        private class Holder : Java.Lang.Object
        {
            public BarCodeResult BarCode { get; set; }
            public Int32 Position { get; set; }
            public ImageButton imgButtonPrint;
            public CheckBox chkActivar;
            public TextView txtViewProduct;
            public TextView txtViewMaterial;
            public TextView txtViewCantidad;
            public TextView txtViewUnidad;
            public EditText EditCantidad;
        }
    }
}