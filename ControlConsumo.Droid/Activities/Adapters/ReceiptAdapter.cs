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
using ControlConsumo.Droid.Activities.Adapters.Entities;
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Tables;
using Android.Graphics;
using Android.Views.InputMethods;
using System.Threading.Tasks;
using Android.Media;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class ReceiptAdapter : BaseAdapter
    {
        public readonly List<ReceiptEntry> List;
        private readonly LayoutInflater Inflater;
        private readonly Context context;
        private readonly RepositoryZ repoz = new RepositoryZ(Util.GetConnection());
        private readonly RepositoryFactory repo = new RepositoryFactory(Util.GetConnection());
        private readonly ActualConfig actualConfig;
        private readonly CachingManager caching;
        private readonly Boolean IsDevolution;
        private ReceiptEntry Entry;
        private Boolean isKeyBoardLocked;
        public delegate void Finish();
        public event Finish Onfinish;

        public ReceiptAdapter(Context context, Boolean IsDevolution, ActualConfig actualConfig, CachingManager caching, Boolean isKeyBoardLocked = true)
        {
            this.IsDevolution = IsDevolution;
            this.context = context;
            this.Inflater = LayoutInflater.From(context);
            this.caching = caching;
            this.actualConfig = actualConfig;
            List = new List<ReceiptEntry>();
            this.isKeyBoardLocked = isKeyBoardLocked;
        }

        public override int Count
        {
            get
            {
                if (List.Count == 0) return 0;

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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            try
            {
                var holder = new Holder();

                if (convertView == null)
                {
                    convertView = Inflater.Inflate(Resource.Layout.adapter_receipt, null);
                    holder.txtViewMaterial = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterial);
                    holder.txtViewCaja = convertView.FindViewById<TextView>(Resource.Id.txtViewCaja);
                    holder.txtViewLoteSap = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSap);
                    holder.txtViewLoteSup = convertView.FindViewById<TextView>(Resource.Id.txtViewLoteSup);
                    holder.txtViewTotal = convertView.FindViewById<TextView>(Resource.Id.txtViewTotal);
                    holder.txtViewCantidad = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                    holder.txtViewUnidad = convertView.FindViewById<TextView>(Resource.Id.txtViewUnidad);
                    convertView.Tag = holder;
                    var imm = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromWindow(convertView.WindowToken, HideSoftInputFlags.None);

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
                    holder.txtViewMaterial.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewLoteSap.Text = context.GetString(Resource.String.ReportTitleLote);
                    holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewLoteSap.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewLoteSup.Text = context.GetString(Resource.String.ReportTitleLoteSupl);
                    holder.txtViewLoteSup.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewLoteSup.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewLoteSup.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewTotal.Text = context.GetString(Resource.String.ReportTitleTotal);
                    holder.txtViewTotal.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewTotal.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewTotal.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewCantidad.Text = context.GetString(Resource.String.ReportTitleCounter);
                    holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewUnidad.Text = context.GetString(Resource.String.ReportTitleUnidad);
                    holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                    holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewUnidad.SetBackgroundResource(Resource.Drawable.selector_input_text);

                }
                else
                {
                    var pos = List.ElementAt(position - 1);

                    holder.txtViewMaterial.Text = pos._MaterialName;
                    holder.txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewMaterial.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewLoteSap.Text = pos.Lot;
                    holder.txtViewLoteSap.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewLoteSap.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewLoteSap.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewLoteSup.Text = pos.LoteSuplidor;
                    holder.txtViewLoteSup.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewLoteSup.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewLoteSup.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewTotal.Text = pos.Total.ToString("N3");
                    holder.txtViewTotal.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewTotal.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewTotal.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewCantidad.Text = pos.Quantity.ToString();
                    holder.txtViewCantidad.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewCantidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewCantidad.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    holder.txtViewUnidad.Text = pos.MaterialUnit;
                    holder.txtViewUnidad.SetTextColor(Android.Graphics.Color.Black);
                    holder.txtViewUnidad.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
                    holder.txtViewUnidad.SetBackgroundResource(Resource.Drawable.selector_input_text);

                    if (Entry._MaterialCode == pos._MaterialCode && Entry.Lot == pos.Lot)
                    {
                        holder.txtViewMaterial.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                        holder.txtViewLoteSap.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                        holder.txtViewLoteSup.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                        holder.txtViewTotal.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                        holder.txtViewCantidad.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                        holder.txtViewUnidad.SetBackgroundColor(Android.Graphics.Color.LightSkyBlue);
                    }
                }
            }
            catch (Exception ex)
            {
                Util.CatchException(context, ex);
            }

            if (position == Count - 1 && Onfinish != null)
            {
                Onfinish.Invoke();
            }

            return convertView;
        }

        public async void Add(String Resultado)
        {
            var barcode = Resultado.GetBarCode();

            var material = await repoz.GetMaterialByCodeOrReferenceAsync(barcode.BarCode);

            if (material == null)
            {
                new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.MessageWrongEntryNoExist));
                return;
            }

            var lote = repoz.GetLoteForMaterial(material.Code, barcode.Lot);

            var repoZsilm = repo.GetRepositoryMaterialZilm();

            var matconfig = await repoZsilm.GetAsyncByKey(material.Code);

            if (matconfig.IgnoreStock)
            {
                new CustomDialog(context, CustomDialog.Status.Warning, context.GetString(Resource.String.ReceiptNoStock));
                return;
            }

            if (lote == null)
            {
                if (!matconfig.AllowNoLot)
                {
                    if (barcode.Sequence == 0)
                        new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.MessageWrongEntryNoBatchExist));
                    else
                        new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.MessageWrongEntryNoBatchExistSecuence));

                    return;
                }
                else
                {
                    if (barcode.Fecha.HasValue)
                    {
                        lote = new Lots()
                        {
                            Reference = barcode.Lot,
                            Code = barcode.Lot,
                            Expire = barcode.Fecha.Value.AddDays(matconfig.Days).ToUniversalTime()
                        };
                    }
                    else
                    {
                        new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.MessageWrongEntryNoBatchExist));
                        return;
                    }
                }
            }

            if (!matconfig.Cantidad)
            {
                var materialInfo = caching.Materials.SingleOrDefault(p => p._MaterialCode == barcode.BarCode || p.MaterialReference == barcode.BarCode || p.units.Any(u => u.Ean == barcode.BarCode));
                var eanCode = 0.0;

                if (!IsDevolution)
                {
                    if (materialInfo == null)
                    {
                        //Si el material no está dentro del listado de materiales de la BOM
                        AddReal(barcode, material);
                    }
                    else if (!materialInfo.units.Any(s => !String.IsNullOrEmpty(s.Ean)))
                    {
                        AddReal(barcode, material);
                    }
                    else if (materialInfo.units.Any(s => !String.IsNullOrEmpty(s.Ean)) && !IsDevolution)
                    {
                        eanCode = Convert.ToDouble(materialInfo.units.First(s => !String.IsNullOrEmpty(s.Ean)).Ean);

                        Dialog ldialog = null;
                        var builder = new AlertDialog.Builder(context);
                        builder.SetCancelable(false);

                        #region Init

                        var view = LayoutInflater.From(context).Inflate(Resource.Layout.dialog_catch_upcCode, null);

                        var editUpcCode = view.FindViewById<EditText>(Resource.Id.EditUpcCode);
                     
                        
                        if (isKeyBoardLocked)
                        {
                            editUpcCode.Clickable = false;
                            editUpcCode.LongClickable = false;
                            editUpcCode.Focusable = true;
                            editUpcCode.FocusableInTouchMode = false;

                            editUpcCode.Touch += (arg, e) =>
                            {
                                var imm = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
                                imm.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
                            };
                        }

                        view.FindViewById<TextView>(Resource.Id.txtViewTitle).Text = "Lea el código UPC: ";
                        var btnAceptar = view.FindViewById<Button>(Resource.Id.btnAceptar);
                        var btnCancelar = view.FindViewById<Button>(Resource.Id.btnCancelar);

                        editUpcCode.KeyPress += async (arg, e) =>
                        {
                            e.Handled = false;
                            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editUpcCode.Text) && e.Event.Action == KeyEventActions.Down)
                            {
                                if (await ProcessUpcCodeAsync(barcode, eanCode, editUpcCode, view))
                                {
                                    ldialog.Dismiss();
                                    ldialog.Dispose();
                                }
                                e.Handled = true;
                            }
                        };

                        btnAceptar.Click += async (sender, args) =>
                        {
                            try
                            {
                                if (await ProcessUpcCodeAsync(barcode, eanCode, editUpcCode, view))
                                {
                                    ldialog.Dismiss();
                                    ldialog.Dispose();
                                }
                            }
                            catch(Exception e)
                            {
                                Util.CatchException(context, e);
                            }
                        };


                        btnCancelar.Click += (sender, args) =>
                        {
                            try
                            {
                                ldialog.Dismiss();
                                ldialog.Dispose();
                            }
                            catch (Exception ex)
                            {
                                Util.CatchException(context, ex);
                            }
                        };

                        #endregion

                        builder.SetView(view);
                        ldialog = builder.Create();
                        ldialog.Show();
                    }
                }
                else
                {
                    AddReal(barcode, material);
                }

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
        private async Task<Boolean> ProcessUpcCodeAsync(BarCodeResult barcode, Double eanCode, EditText editUpcCode, View view)
        {
            try
            {
                var isCorrect = true;
                var material = await repoz.GetMaterialByCodeOrReferenceAsync(barcode.BarCode);

                if (editUpcCode.Text.Length > 0)
                {
                    if (eanCode != Convert.ToDouble(editUpcCode.Text))
                    {

                        isCorrect = false;
                        view.SetBackgroundColor(Color.Red);
                        view.FindViewById<TextView>(Resource.Id.txtViewTitle).Text = "UPC incorrecto. \n \n Lea el código UPC: ";
                
                        var proceso = await repoz.GetProces();
                        var Caching = new CachingManager(context);

                        var ean = await repoz.GetUnitByCode(editUpcCode.Text);
                        var MaterialWrong = new Materials();

                        var barCodeUPC = editUpcCode.Text.GetBarCode();
                        barCodeUPC.Lot = barcode.Lot;

                        if (ean != null)
                        {
                            var repoMat = repo.GetRepositoryMaterials();
                            MaterialWrong = await repoMat.GetAsyncByKey(ean.MaterialCode);

                            if (MaterialWrong != null)
                            {
                                await Util.AddError(proceso, await repoz.GetActualConfig(proceso.EquipmentID), MaterialWrong, barCodeUPC, Caching, true, Errors.Messages.WRONG_UPCCODE_INVENTORYENTRY);
                            }
                        }

                        editUpcCode.Text = String.Empty;
                        editUpcCode.RequestFocus();

                        return isCorrect;
                    }

                    view.SetBackgroundColor(Color.Green);
                    AddReal(barcode, material);
                }
                return isCorrect;
            }
            catch (Exception ex)
            {
                Util.CatchException(context, ex);
                return false;
            }
        }
        private async void AddReal(BarCodeResult barcode, Materials material)
        {
            if (IsDevolution)
            {
                var realQuantity = List.Where(p => (p._MaterialCode == barcode.BarCode || p.MaterialReference == barcode.BarCode) && p.Lot == barcode.Lot && p.Details.Any(d => d.BoxNumber == barcode.Sequence)).Sum(p => p.Total) + barcode.Quantity;

                var disponible = await repoz.GetStockAvailableAsync(material.Code, barcode.Lot, barcode.Sequence);

                if (disponible < realQuantity)
                {
                    new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.EntryMaterialNoAvailable), material._DisplayCode, barcode.Lot, disponible.ToString("N3")));
                    return;
                }
            }
            else
            {
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
                        new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.MessageWrongEntry), material._DisplayCode, actualConfig.ProductShort ?? actualConfig._ProductCode));
                    }
                    else
                    {
                        new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.MessageWrongEntryNoExist));
                    }

                    return;
                }
            }

            var posicion = List.Select((p, n) => new { p, n }).FirstOrDefault(p => p.p.MaterialCode == material.Code && p.p.Lot == barcode.Lot);

            Entry = new ReceiptEntry()
            {
                MaterialCode = material.Code,
                Lot = barcode.Lot
            };

            if (barcode.Sequence > 0)
            {
                var lastTransaction = await repoz.Get_Last_Transaction(material.Code, barcode.Lot, barcode.Sequence, caching.Stock.CustomFecha, caching.Stock.TurnID);

                if (lastTransaction != null)
                {
                    if (IsDevolution)
                    {
                        if (lastTransaction.Get_Type(context) == Transactions.Types.Devolucion_Buffer)
                        {
                            new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.ReceiptAreadyReturned), barcode.BarCode, barcode.Lot, barcode.Sequence));
                            return;
                        }
                    }
                    else
                    {
                        var mytype = lastTransaction.Get_Type(context);

                        if (mytype == Transactions.Types.Entrega_Material || mytype == Transactions.Types.Consumo_Material || mytype == Transactions.Types.Devolucion_Consumo)
                        {
                            new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.ReceiptAreadyReceipt), barcode.BarCode, barcode.Lot, barcode.Sequence));
                            return;
                        }
                    }
                }
            }

            if (posicion == null)
            {
                List.Add(new ReceiptEntry()
                {
                    MaterialName = material.Name,
                    MaterialCode = material.Code,
                    MaterialReference = material.Reference,
                    MaterialUnit = material.Unit,
                    Lot = barcode.Lot,
                    LoteSuplidor = repoz.GetLoteForMaterial(material.Code, barcode.Lot).Reference,
                    Details = new List<ReceiptEntry.Detail>() { new ReceiptEntry.Detail() { Quantity = barcode.Quantity, BoxNumber = barcode.Sequence } }
                });
            }
            else
            {
                if (barcode.Sequence > 0)
                {
                    if (List[posicion.n].Details.Any(a => a.BoxNumber == barcode.Sequence))
                    {
                        new CustomDialog(context, CustomDialog.Status.Error, String.Format(context.GetString(Resource.String.ReceiptAreadyScanned), barcode.BarCode, barcode.Lot, barcode.Sequence));
                        return;
                    }
                }

                List[posicion.n].Details.Add(new ReceiptEntry.Detail() { Quantity = barcode.Quantity, BoxNumber = barcode.Sequence });
            }

            NotifyDataSetChanged();
        }

        public void Clear()
        {
            List.Clear();
        }

        private class Holder : Java.Lang.Object
        {
            public TextView txtViewMaterial { get; set; }
            public TextView txtViewCaja { get; set; }
            public TextView txtViewLoteSap { get; set; }
            public TextView txtViewLoteSup { get; set; }
            public TextView txtViewTotal { get; set; }
            public TextView txtViewCantidad { get; set; }
            public TextView txtViewUnidad { get; set; }
        }
    }
}