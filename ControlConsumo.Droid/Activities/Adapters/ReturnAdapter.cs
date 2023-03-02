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
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Shared.Repositories;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class ReturnAdapter : BaseAdapter
    {
        private readonly Context context;
        public readonly List<MaterialReport> Materiales;
        private readonly LayoutInflater Inflater;
        private readonly RepositoryZ repoz = new RepositoryZ(Util.GetConnection());
        private readonly Boolean ShowPrintButton;

        public ReturnAdapter(Context context, IEnumerable<MaterialReport> Materiales, Boolean ShowPrintButton = false)
        {
            this.context = context;
            Inflater = LayoutInflater.From(context);
            this.Materiales = Materiales.ToList();
            this.ShowPrintButton = ShowPrintButton;
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
                convertView = Inflater.Inflate(Resource.Layout.adapter_return, null);
                holder.chkActivar = convertView.FindViewById<CheckBox>(Resource.Id.chkActivar);
                holder.imgButtonPrint = convertView.FindViewById<ImageButton>(Resource.Id.imgButtonPrint);
                holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                holder.txtViewCaja = convertView.FindViewById<TextView>(Resource.Id.txtViewCaja);
                holder.txtViewLoteSap = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSap);
                holder.txtViewLoteSup = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSup);
                holder.txtViewCantidad = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                holder.txtViewUnidad = convertView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                holder.EditCantidad = convertView.FindViewById<EditText>(Resource.Id.EditCantidad);
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

                if (!ShowPrintButton)
                {
                    holder.imgButtonPrint.Visibility = ViewStates.Gone;
                }
                else
                {
                    holder.EditCantidad.Visibility = ViewStates.Gone;
                    holder.chkActivar.Visibility = ViewStates.Gone;
                    holder.imgButtonPrint.Visibility = ViewStates.Invisible;
                }

                holder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewUnidad.Text = context.GetString(Resource.String.ReportTitleUnidad);
                holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCaja.Text = context.GetString(Resource.String.ReportTitleCaja);
                holder.txtViewCaja.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewCaja.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewLoteSap.Text = context.GetString(Resource.String.ReportTitleLote);
                holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewLoteSup.Text = context.GetString(Resource.String.ReportTitleLoteSupl);
                holder.txtViewLoteSup.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                holder.txtViewLoteSup.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleUltimaCantidad);
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

                holder.txtViewMaterial.Text = pos.MaterialName;
                holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewUnidad.Text = pos.NeedPercent ? "%" : pos.MaterialUnit ?? pos.Unit;
                holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCaja.Text = pos.BoxNumber > 0 ? pos.BoxNumber.ToString() : String.Empty;
                holder.txtViewCaja.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewCaja.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewLoteSap.Text = pos.Lot;
                holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewLoteSup.Text = pos.LoteSuplidor;
                holder.txtViewLoteSup.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewLoteSup.SetTextColor(Android.Graphics.Color.Black);

                holder.txtViewCantidad.Text = pos.EntryQuantity.ToString("N3");
                holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);

                holder.EditCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                holder.EditCantidad.SetTextColor(Android.Graphics.Color.Black);

                if (holder.chkActivar.Checked)
                {
                    holder.EditCantidad.SetBackgroundResource(Resource.Drawable.bg_input_white);
                    holder.EditCantidad.Enabled = true;
                    holder.EditCantidad.Text = pos.Quantity > 0 ? pos.Quantity.ToString() : String.Empty;
                }
                else
                {
                    holder.EditCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);
                    holder.EditCantidad.Enabled = false;
                    holder.EditCantidad.Text = String.Empty;
                }

                holder.Position = position - 1;
                holder.EditCantidad.Tag = holder;
                holder.chkActivar.Tag = holder;
                holder.imgButtonPrint.Tag = holder;

                if (!ShowPrintButton)
                {
                    holder.imgButtonPrint.Visibility = ViewStates.Gone;
                }
                else
                {
                    holder.EditCantidad.Visibility = ViewStates.Gone;
                    holder.chkActivar.Visibility = ViewStates.Gone;
                }
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

                var repozmat = new RepositoryFactory(Util.GetConnection()).GetRepositoryMaterialZilm();

                var position = Materiales.ElementAt(holder.Position);

                var etiqueta = new Etiquetas()
                {
                    Cantidad = 1,
                    Codigo = position.MaterialReference ?? String.Empty,
                    Descripcion = position.MaterialName,
                    Medida = (decimal)position.EntryQuantity,
                    Unidad = position.MaterialUnit,
                    LoteInterno = position.Lot,
                    Material = position._MaterialCode,
                    Secuencia = position.BoxNumber
                };

                var lote = repoz.GetLoteForMaterial(position.MaterialCode, position.Lot);

                if (lote != null)
                {
                    etiqueta.LoteSuplidor = lote.Reference;

                    if (lote.Expire.Year > 2000)
                    {
                        etiqueta.Fecha = lote.Expire;
                    }
                }
                else
                {
                    var zmaterial = await repozmat.GetAsyncByKey(position.MaterialCode);

                    if (zmaterial.AllowNoLot)
                    {
                        var barcode = String.Format("{0}-{1}-1", position._MaterialCode, position.Lot).GetBarCode();

                        if (barcode.Fecha.HasValue)
                        {
                            etiqueta.Fecha = barcode.Fecha.Value.AddDays(zmaterial.Days).Date;
                        }
                    }
                }

                var lista = new List<Etiquetas>();
                lista.Add(etiqueta);
                await printer.ExecutePrint(context, lista);

                if (OnPrint != null)
                {
                    OnPrint.Invoke();
                }
            }
        }

        private void chkActivar_Click(object sender, EventArgs e)
        {
            var obj = sender as CheckBox;
            var holder = obj.Tag as Holder;

            holder.EditCantidad.Enabled = obj.Checked;
            holder.EditCantidad.RequestFocus();
            holder.EditCantidad.SetBackgroundResource(obj.Checked ? Resource.Drawable.bg_input_white : Resource.Drawable.selector_input_text);

            if (!obj.Checked) Materiales[holder.Position].Quantity = 0;
        }

        private void EditCantidad_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            var obj = sender as EditText;
            var holder = obj.Tag as Holder;

            if (holder != null)
            {
                if (Materiales[holder.Position].NeedPercent && obj.Text.ToNumeric() > 1)
                {
                    new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.DialogClosedNoMax), "100%"));
                    obj.Text = String.Empty;
                    return;
                }
                else if (!Materiales[holder.Position].NeedPercent && obj.Text.ToNumeric() > Materiales[holder.Position].EntryQuantity)
                {
                    var msg = context.GetString(Resource.String.ReportTitleWrongQuantity);
                    new CustomDialog(context, CustomDialog.Status.Error, msg);
                    Materiales[holder.Position].Quantity = 0;
                    obj.Text = String.Empty;
                    return;
                }

                Materiales[holder.Position].Quantity = obj.Text.ToNumeric();
            }
        }

        public delegate void Print();
        public event Print OnPrint;

        private class Holder : Java.Lang.Object
        {
            public Int32 Position { get; set; }
            public ImageButton imgButtonPrint;
            public CheckBox chkActivar;
            public TextView txtViewMaterial;
            public TextView txtViewCaja;
            public TextView txtViewLoteSap;
            public TextView txtViewLoteSup;
            public TextView txtViewUnidad;
            public TextView txtViewCantidad;
            public EditText EditCantidad;
        }
    }
}