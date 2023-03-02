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
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Shared.Models.Config;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class StockOnFloorAdapter : BaseAdapter
    {
        private readonly Context context;
        public readonly List<StockOnFloor> List;
        private readonly LayoutInflater Inflater;
        private readonly RepositoryZ repoz = new RepositoryZ(Util.GetConnection());
        private readonly RepositoryFactory repo = new RepositoryFactory(Util.GetConnection());
        private readonly ActualConfig actualConfig;
        private readonly CachingManager caching;
        private StockOnFloor Escaneado { get; set; }

        public StockOnFloorAdapter(Context context, IEnumerable<StockOnFloor> materials, ActualConfig actualConfig, CachingManager caching)
        {
            this.context = context;
            this.Inflater = LayoutInflater.From(context);
            this.actualConfig = actualConfig;
            this.caching = caching;
            this.List = materials.Where(p => p.Amount > 0).ToList();
        }

        public override int Count
        {
            get
            {
                if (List.Count() == 0)
                    return 0;
                else
                    return List.Count() + 1;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        private class Holder : Java.Lang.Object
        {
            public LinearLayout LinearLayoutPosition { get; set; }
            public TextView txtViewMaterial { get; set; }
            public TextView txtViewLoteSap { get; set; }
            public TextView txtViewCantidadLogico { get; set; }
            public TextView txtViewCantidadFisico { get; set; }
            public TextView txtViewTotal { get; set; }
            public ImageView imgStatusOK { get; set; }
            public ImageView imgStatusCancel { get; set; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            try
            {
                var holder = new Holder();

                if (convertView == null)
                {
                    convertView = Inflater.Inflate(Resource.Layout.adapter_receipt_stock, null);
                    holder.LinearLayoutPosition = convertView.FindViewById<LinearLayout>(Resource.Id.LinearLayoutPosition);
                    holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                    holder.txtViewLoteSap = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSap);
                    holder.txtViewCantidadFisico = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidadFisico);
                    holder.txtViewCantidadLogico = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidadLogico);
                    holder.txtViewTotal = convertView.FindViewById<TextView>(Resource.Id.txtViewTotal);
                    holder.imgStatusOK = convertView.FindViewById<ImageView>(Resource.Id.imgStatusOK);
                    holder.imgStatusCancel = convertView.FindViewById<ImageView>(Resource.Id.imgStatusCancel);

                    convertView.Tag = holder;
                }
                else
                {
                    holder = convertView.Tag as Holder;
                }

                if (position == 0)
                {
                    holder.txtViewMaterial.Text = context.GetString(Resource.String.ReportTitleMaterial);
                    holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewLoteSap.Text = context.GetString(Resource.String.ReportTitleLote);
                    holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewCantidadLogico.Text = context.GetString(Resource.String.ReportTitleTeorico);
                    holder.txtViewCantidadLogico.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewCantidadLogico.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewCantidadFisico.Text = context.GetString(Resource.String.ReportTitleFisico);
                    holder.txtViewCantidadFisico.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewCantidadFisico.SetTextColor(Android.Graphics.Color.Black);

                    holder.txtViewTotal.Text = context.GetString(Resource.String.ReportTitleDifference);
                    holder.txtViewTotal.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewTotal.SetTextColor(Android.Graphics.Color.Black);

                    holder.imgStatusOK.Visibility = ViewStates.Invisible;
                    holder.imgStatusCancel.Visibility = ViewStates.Gone;
                }
                else
                {
                    var pos = List.ElementAt(position - 1);

                    holder.txtViewMaterial.Text = pos._ProductName;
                    holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewMaterial.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewLoteSap.Text = pos.Lot;
                    holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewLoteSap.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewCantidadLogico.Text = pos.Logico.ToString();
                    holder.txtViewCantidadLogico.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewCantidadLogico.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewCantidadLogico.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewCantidadFisico.Text = pos.Fisico.ToString();
                    holder.txtViewCantidadFisico.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewCantidadFisico.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewCantidadFisico.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewTotal.Text = pos.Total.ToString();
                    holder.txtViewTotal.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewTotal.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewTotal.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    if (pos.Total != 0)
                    {
                        holder.imgStatusOK.Visibility = ViewStates.Gone;
                        holder.imgStatusCancel.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        holder.imgStatusOK.Visibility = ViewStates.Visible;
                        holder.imgStatusCancel.Visibility = ViewStates.Gone;
                    }

                    if (Escaneado != null && Escaneado.ProductCode == pos.ProductCode && Escaneado.Lot == pos.Lot)
                    {
                        holder.txtViewMaterial.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                        holder.txtViewLoteSap.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                        holder.txtViewCantidadLogico.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                        holder.txtViewCantidadFisico.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                        holder.txtViewTotal.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                    }
                }
            }
            catch (Exception ex)
            {
                Util.CatchException(context, ex);
            }

            return convertView;
        }

        public async void Add(String Result)
        {
            var barcode = Result.GetBarCode();

            var material = await repoz.GetMaterialByCodeOrReferenceAsync(barcode.BarCode);

            if (material == null)
            {
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.MessageWrongEntryNoExist));
                return;
            }

            var Materials = caching.Materials;
            var lmaterial = Materials.SingleOrDefault(p => p._MaterialCode == barcode.BarCode || p.MaterialReference == barcode.BarCode || p.units.Any(u => u.Ean == barcode.BarCode));

            if (lmaterial == null)
            {
                var ean = await repoz.GetUnitByCode(barcode.BarCode);

                var MaterialWrong = new Materials();

                if (ean != null)
                {
                    var repoMat = repo.GetRepositoryMaterials();
                    MaterialWrong = await repoMat.GetAsyncByKey(ean.MaterialCode);
                }
                else
                {
                    MaterialWrong = await repoz.GetMaterialByCodeOrReferenceAsync(barcode.BarCode);
                }

                if (MaterialWrong != null)
                {
                    new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.MessageWrongEntry), MaterialWrong._DisplayCode, actualConfig.ProductShort ?? actualConfig._ProductCode));
                }
                else
                {
                    new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.MessageWrongEntryNoExist));
                }

                return;
            }

            var lote = repoz.GetLoteForMaterial(material.Code, barcode.Lot);

            var repoZsilm = repo.GetRepositoryMaterialZilm();
            var matconfig = await repoZsilm.GetAsyncByKey(material.Code);

            if (lote == null)
            {
                if (!matconfig.AllowNoLot)
                {
                    new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.MessageWrongEntryNoBatchExist));
                    return;
                }
            }

            if (!matconfig.Cantidad)
            {
                AddReal(barcode, material);
            }
            else
            {
                Dialog ldialog = null;
                var builder = new AlertDialog.Builder(context);
                builder.SetCancelable(true);

                #region Init

                var view = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_catch_quantity, null);
                var editQuantity = view.FindViewById<EditText>(Resource.Id.editQuantity);
                view.FindViewById<TextView>(Resource.Id.txtViewTitle).Text = String.Format(context.GetString(Resource.String.EntryBoxQuantity), material.Unit);
                var btnAceptar = view.FindViewById<Button>(Resource.Id.btnAceptar);

                #endregion

                btnAceptar.Click += (sender, args) =>
                {
                    try
                    {
                        if (editQuantity.Text.CastToSingle() == 0 || editQuantity.Text.CastToSingle() > 1000)
                        {
                            editQuantity.Text = String.Empty;
                            editQuantity.RequestFocus();
                            return;
                        }

                        ldialog.Dismiss();
                        ldialog.Dispose();

                        barcode.Quantity = editQuantity.Text.CastToSingle();

                        AddReal(barcode, material);
                    }
                    catch (Exception ex)
                    {
                        Util.CatchException(context, ex);
                    }
                };

                builder.SetView(view);

                ldialog = builder.Create();
                ldialog.Show();
            }
        }

        private async void AddReal(BarCodeResult barcode, Materials material)
        {
            Escaneado = List.FirstOrDefault(p => (p._ProductCode == barcode.BarCode || p.ProductReference == barcode.BarCode) && p.Lot == barcode.Lot);

            //if (Escaneado == null)
            //{
            //    Escaneado = new StockOnFloor()
            //        {
            //            ProductID = material.ID,
            //            ProductCode = material.Code,
            //            ProductName = material.Name,
            //            ProductReference = material.Reference,
            //            ProductShort = material.Short,
            //            ProductUnit = material.Unit,
            //            AmountEscaneado = barcode.Quantity,
            //            Lot = barcode.Lot,
            //            Fisico = 1,
            //            Logico = 0
            //        };

            //    List.Add(Escaneado);
            //}
            //else
            //{

            if (Escaneado == null)
            {
                new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.EntryMaterialNoStock), barcode.BarCode, barcode.Lot));
                return;
            }

            if (barcode.Sequence > 0)
            {
                if (Escaneado.BoxesNo.Contains(barcode.Sequence))
                {
                    new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.ReceiptAreadyScanned), barcode.BarCode, barcode.Lot, barcode.Sequence));
                    return;
                }

                Escaneado.BoxesNo.Add(barcode.Sequence);
            }

            var realQuantity = Escaneado.AmountEscaneado + barcode.Quantity;

            var disponible = await repoz.GetStockAvailableAsync(material.Code, barcode.Lot, barcode.Sequence);

            if (realQuantity > disponible)
            {
                new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.EntryMaterialNoAvailable), barcode.BarCode, barcode.Lot, disponible.ToString("N3")));
                return;
            }

            Escaneado.Fisico++;
            Escaneado.AmountEscaneado += barcode.Quantity;


            //if (Escaneado.Total == 0)
            //{
            //    new CustomDialog(context, CustomDialog.Status.Good, String.Format(context.GetString(Resource.String.ReceiptPositionComplete), material._Code, barcode.Lot));
            //}
            //}

            NotifyDataSetChanged();
        }
    }
}