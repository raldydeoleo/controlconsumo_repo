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
using ControlConsumo.Droid.Activities.Adapters;
using ControlConsumo.Droid.Managers;
using Newtonsoft.Json;
using System.Globalization;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Models.Process;
using Android.Content.PM;
using System.Threading;
using ControlConsumo.Shared.Models.R;
using System.Net;

namespace ControlConsumo.Droid.Activities
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop)]
    public class ReturnActivity : BaseActivity
    {
        private const String _PROCESO = "ReturnActivity._PROCESO";
        private const String _TURNID = "ReturnActivity._TURNID";
        private DateTime ProductionDate;
        private Byte TurnoID;
        private ReturnAdapter Adapter;
        private ListView listDevolucion;
        private TextView txtViewReturnTitle;
        private ActualConfig actualconfig;
        private readonly CustomSequencesManager Secuences = new CustomSequencesManager();
        private CachingManager caching;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            

            SetContentView(Resource.Layout.return_activity);
            SetTitle(Resource.String.ApplicationLabel);

            if (listDevolucion == null)
            {
                caching = new CachingManager(this);
                txtViewReturnTitle = FindViewById<TextView>(Resource.Id.txtViewReturnTitle);
                TurnoID = Convert.ToByte(Intent.GetStringExtra(CustExtras.TurnID.ToString()));
                ProductionDate = DateTime.ParseExact(Intent.GetStringExtra(CustExtras.ProductionDate.ToString()), "yyyyMMdd", CultureInfo.InvariantCulture);
                listDevolucion = FindViewById<ListView>(Resource.Id.listDevolucion);
                txtViewReturnTitle.Text = String.Format(GetString(Resource.String.ReturnTitle), TurnoID, ProductionDate.ToString("dd MMMM yyyy"));
                ThreadPool.QueueUserWorkItem(o => Binding());
            }
        }

        protected override async void OnSaveInstanceState(Bundle outState)
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                outState.PutString(_PROCESO, JsonConvert.SerializeObject(repoz.GetProces()));
                outState.PutShort(_TURNID, TurnoID);
                base.OnSaveInstanceState(outState);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected override async void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            try
            {
                base.OnRestoreInstanceState(savedInstanceState);
                var repoz = repo.GetRepositoryZ();

                repoz.SetProcess(JsonConvert.DeserializeObject<ProcessList>(savedInstanceState.GetString(_PROCESO)));
                TurnoID = (Byte)savedInstanceState.GetShort(_TURNID);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void Binding()
        {
            var Repoz = repo.GetRepositoryZ();
            if (actualconfig == null)
            {
                var proceso = await Repoz.GetProces();
                actualconfig = await Repoz.GetActualConfig(proceso.EquipmentID);
            }
            var Materiales = await Repoz.GetLastMaterial(actualconfig.ProductCode, actualconfig.VerID);
            var MaterialesBandejas = await Repoz.GetLastTraysMaterial(actualconfig.ProductCode, actualconfig.VerID, caching.GetProductionDate());
            if (MaterialesBandejas.Count > 0)
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
                Materiales = Materiales.Concat(MaterialesBandejas).ToList();
            }
            Adapter = new ReturnAdapter(this, Materiales);
            RunOnUiThread(() =>
            {
                listDevolucion.Adapter = Adapter;
            });
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            try
            {
                MenuInflater.Inflate(Resource.Menu.ReturnMenu, menu);
            }
            catch (Exception ex)
            {
                CatchException(ex);
            }
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            try
            {
                switch (item.ItemId)
                {
                    case Resource.Id.menu_Cancel:

                        Finish();

                        break;

                    case Resource.Id.menu_Add:

                        Save();

                        break;

                    case Resource.Id.menu_Print:

                        LoadReturned();

                        break;

                }
            }
            catch (Exception ex)
            {
                base.CatchException(ex);
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        private async void LoadReturned()
        {
            var repoz = repo.GetRepositoryZ();
            var devuelto = await repoz.GetLastReturn();
            var Adapter = new ReturnAdapter(this, devuelto, true);

            var dialog = new Dialog(this, Android.Resource.Style.ThemeTranslucentNoTitleBar);
            dialog.SetContentView(Resource.Layout.dialog_general_list);
            dialog.SetCancelable(false);

            var layout = dialog.FindViewById<LinearLayout>(Resource.Id.custom_dialog_first_rl);
            var txtview = dialog.FindViewById<TextView>(Resource.Id.TxtViewGeneral);
            var listview = dialog.FindViewById<ListView>(Resource.Id.lstGeneral);
            var backbutton = dialog.FindViewById<Button>(Resource.Id.btnBackDialog);

            txtview.Text = GetString(Resource.String.ReturnTitleReprint);

            backbutton.Click += (sender, args) =>
            {
                dialog.Dismiss();
                dialog.Dispose();
            };

            Adapter.OnPrint += () =>
            {
                dialog.Dismiss();
                dialog.Dispose();
            };

            layout.Background = Resources.GetDrawable(Resource.Color.gray_base, Theme);
            layout.Background.SetAlpha(175);

            listview.Adapter = Adapter;

            dialog.Show();
        }

        private async void Save()
        {
            try
            {
                ShowProgress(true);

                var repoz = repo.GetRepositoryZ();
                var proceso = SecurityManager.CurrentProcess;
                var materiales = Adapter.Materiales.Where(p => p.Quantity > 0).ToList();
                var actualConfig = await repoz.GetActualConfig(proceso.EquipmentID);
                var repoConsumo = repo.GetRepositoryConsumptions();
                var RepoTracking = repo.GetRepositoryTracking();
                var repoTra = repo.GetRepositoryTransactions();

                var buffer = new List<Consumptions>();
                var bufferTracking = new List<Tracking>();

                foreach (var item in materiales)
                {
                    var consumo = new Consumptions()
                    {
                        ProcessID = proceso.Process,
                        Center = proceso.Centro,
                        EquipmentID = proceso.EquipmentID,
                        SubEquipmentID = actualConfig.SubEquipmentID,
                        CustomFecha = Convert.ToInt32(item.Produccion.GetSapDateL()),
                        Sync = true,
                        SyncSQL = true,
                        TimeID = actualConfig.TimeID,
                        TurnID = item.TurnID,
                        Unit = item.MaterialUnit,
                        VerID = actualConfig.VerID,
                        ProductCode = actualConfig.ProductCode,
                        //Quantity = item.Quantity * -1,
                        Quantity = -1 *(!item.NeedPercent ? item.Quantity : item.Quantity * (item.EntryQuantity > 0 ? item.EntryQuantity : item.Acumulated)),
                        MaterialCode = item.MaterialCode,
                        Lot = item.Lot,
                        BatchID = !String.IsNullOrEmpty(item.TrayID) ? item.BatchID : String.Empty,
                        Logon = proceso.Logon,
                        BoxNumber = item.BoxNumber,
                        CustomID = await Secuences.AddMaterial(item.MaterialCode, item.Lot),
                        Fecha = DateTime.Now,
                        Produccion = item.Produccion.ToLocalTime(),
                        TrayID = !String.IsNullOrEmpty(item.TrayID) ? item.TrayID : String.Empty,
                        TrayEquipmentID = item.TrayEquipmentID,
                        ElaborateID = item.ElaborateID,
                        TrayDate = item.TrayDate
                    };

                    var stock = await repoz.ExistClosedStockAsync(item.Produccion, item.TurnID);

                    if (stock != null && stock.Status == Stocks._Status.Cerrado)
                    {
                        consumo.TurnID = caching.TurnoID;
                        consumo.Fecha = caching.GetProductionDate();
                        consumo.CustomFecha = Convert.ToInt32(caching.GetProductionDate().GetSapDate());
                    }

                    var tracking = await repoz.GetTrackingWithCount(item.Produccion, item.CustomID);

                    if (tracking.WasNull && tracking.Count > 0)
                    {
                        tracking = new Tracking();
                        tracking.FechaConsumption = consumo.Fecha;
                        tracking.ConsumptionID = consumo.CustomID;
                        tracking.FechaElaborate = caching.GetProductionDate();
                        tracking.ElaborateID = 1;
                    }
                    else
                    {
                        if (tracking.WasNull)
                        {
                            tracking = new Tracking();
                            tracking.FechaConsumption = consumo.Fecha;
                            tracking.FechaElaborate = caching.GetProductionDate();
                            tracking.ElaborateID = 1;
                        }

                        tracking.ConsumptionID = consumo.CustomID;
                    }

                    tracking.Sync = true;
                    tracking.SyncSQL = true;

                    buffer.Add(consumo);
                    bufferTracking.Add(tracking);

                    if (!String.IsNullOrEmpty(consumo.TrayID))
                    {
                        var repoTray = repo.GetRepositoryTrays();
                        String idBandeja = new String(consumo.TrayID.Where(Char.IsLetter).ToArray());
                        String secuenciaBandeja = new String(consumo.TrayID.Where(Char.IsDigit).ToArray());
                        var trayList = await repoz.GetEstatusBandeja(idBandeja, secuenciaBandeja.CastToShort());
                        var consumptionEquipment = new Equipments();
                        consumptionEquipment = await repoz.GetEquipment(consumo.TrayEquipmentID);
                        var Bandeja = new TraysProducts()
                        {
                            ID = trayList._TrayID.ToUpper(),
                            TrayID = trayList.TrayID.ToUpper(),
                            Status = TraysProducts._Status.Lleno,
                            Secuencia = trayList.Secuencia,
                            ProductCode = consumo.MaterialCode, //código de material de rolado. 
                            VerID = consumo.VerID,
                            TimeID = consumptionEquipment.TimeID,
                            Quantity = trayList.Status == TraysProducts._Status.Vacio ? Math.Abs(consumo.Quantity) : (trayList.Quantity + Math.Abs(consumo.Quantity)),
                            Unit = consumo.Unit,
                            BatchID = consumo.BatchID,
                            EquipmentID = consumptionEquipment.ID,
                            Fecha = consumo.Produccion,
                            UsuarioLlenada = proceso.Logon,
                            ElaborateID = 0,
                            Sync = true,
                            ModifyDate = DateTime.Now
                        };

                        TraysProducts[] traysProducts = new TraysProducts[] { Bandeja };

                        var repoBanP = repo.GetRepositoryTraysProducts();

                        var configuracionBandeja = await repoTray.GetAsyncByKey(trayList.TrayID.ToUpper());

                        if (configuracionBandeja != null)
                        {
                            if (configuracionBandeja.procesarSAP)
                            {
                                Bandeja.Sync = true;
                                await repoBanP.InsertOrReplaceAsync(Bandeja);
                            }
                            else
                            {
                                traysProducts[0].Sync = false;
                                try
                                {
                                    await repoBanP.InsertOrUpdateAsyncSql(traysProducts, true); //Guardando datos en línea con SQL Server
                                }
                                catch (WebException wEx)
                                {
                                    ShowWebExceptionDialog(wEx, "Ajuste de Salida de Producto");
                                    throw;
                                }
                            }

                        }
                    }
                }

                var transactiones = new List<Transactions>();

                var position = 0;

                foreach (var item in buffer)
                {
                    if (String.IsNullOrEmpty(item.Lot)) continue;

                    if (materiales.ElementAt(position).IgnoreStock) continue;

                    var newitem = new Transactions()
                    {
                        Unit = item.Unit,
                        Quantity = Math.Abs(item.Quantity),
                        BoxNumber = item.BoxNumber,
                        Logon = proceso.Logon,
                        Lot = item.Lot,
                        MaterialCode = item.MaterialCode,
                        Reason = GetString(Resource.String.ReceiptReturn),
                        Sync = true,
                        CustomFecha = Convert.ToInt32(caching.GetProductionDate().GetSapDate()),
                        Fecha = caching.GetProductionDate(),
                        TurnID = caching.Stock.TurnID
                    };

                    transactiones.Add(newitem);
                    position++;
                }

                await repoConsumo.InsertAsyncAll(buffer);
                await RepoTracking.InsertAsyncAll(bufferTracking);
                await repoTra.InsertAsyncAll(transactiones);

                var customdialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.ReturnMessages), CustomDialog.ButtonStyles.TwoButtonWithPrint);
                customdialog.OnAcceptPress += customdialog_OnAcceptPress;
                customdialog.OnCancelPress += customdialog_OnCancelPress;
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {
                ShowProgress(false);
            }
        }

        private async void customdialog_OnAcceptPress(bool IsCantidad, float Box, Single Cantidad)
        {
            var materiales = Adapter.Materiales.Where(p => p.Quantity > 0).ToList();
            var printer = new PrinterManager();
            var lista = new List<Etiquetas>();
            var repoz = repo.GetRepositoryZ();
            var repozilm = repo.GetRepositoryMaterialZilm();

            foreach (var material in materiales.Where(p => !String.IsNullOrEmpty(p.Lot) && !p.IgnoreStock && String.IsNullOrEmpty(p.TrayID)))
            {
                var etiqueta = new Etiquetas()
                {
                    Cantidad = 1,
                    Medida = (Decimal)material.Quantity,
                    Unidad = material.Unit,
                    Codigo = material.MaterialReference,
                    Material = material._MaterialCode,
                    Descripcion = material.MaterialName,
                    LoteInterno = material.Lot,
                    Secuencia = material.BoxNumber
                };

                var lote = repoz.GetLoteForMaterial(material.MaterialCode, material.Lot);

                etiqueta.LoteSuplidor = lote.Reference;

                if (lote.Expire.Year > 2000)
                {
                    etiqueta.Fecha = lote.Expire;
                }

                lista.Add(etiqueta);
            }

            if (lista.Any()) await printer.ExecutePrint(this, lista);

            Binding();
        }

        private void customdialog_OnCancelPress()
        {
            Binding();
        }
    }
}