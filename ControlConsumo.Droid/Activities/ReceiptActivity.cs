using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using ControlConsumo.Droid.Activities.Adapters;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Shared.Models.Config;
using Android.Content.PM;
using System.Threading;

namespace ControlConsumo.Droid.Activities
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateAlwaysHidden, ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop)]
    public class ReceiptActivity : BaseActivity
    {
        private ReceiptAdapter Adapter;
        private StockOnFloorAdapter SAdapter;
        private ListView lstReceipt;
        private EditText editScanBandejas;
        private TextView txtViewTitle;
        private ActualConfig actualconfig;
        private Operaciones Operacion;
        private Int32 TurnID;
        private String Fecha;

        private CachingManager caching;
        private readonly CustomSequencesManager secuences = new CustomSequencesManager();

        public enum Operaciones
        {
            Entrega,
            Devolucion,
            Inventario
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.receipt_activity);
            SetTitle(Resource.String.ApplicationLabel);

            if (lstReceipt == null)
            {
                caching = new CachingManager(this);
                txtViewTitle = FindViewById<TextView>(Resource.Id.txtViewTitle);
                lstReceipt = FindViewById<ListView>(Resource.Id.lstReceipt);
                editScanBandejas = FindViewById<EditText>(Resource.Id.editScanBandejas);
                lstReceipt.FocusChange += lstReceipt_FocusChange;
                editScanBandejas.KeyPress += editScanBandejas_KeyPress;
                TurnID = Convert.ToInt32(Intent.GetStringExtra(CustExtras.TurnID.ToString()));
                Fecha = Intent.GetStringExtra(CustExtras.ProductionDate.ToString());
                ThreadPool.QueueUserWorkItem(o => Init());
            }
        }

        private async void Init()
        {
            try
            {
                var process = await repo.GetRepositoryZ().GetProces();
                var equipmentid = process.EquipmentID;
                Operacion = (Operaciones)Intent.GetIntExtra(CustExtras.Operacion.ToString(), 0);
                actualconfig = await repo.GetRepositoryZ().GetActualConfig(equipmentid);
                caching.SetProduct(actualconfig.ProductCode, actualconfig.VerID);
                var materials = await repo.GetRepositoryR().GetStockListForStock(GetString(Resource.String.ReceiptConceptReceive), GetString(Resource.String.ReceiptConsumption), GetString(Resource.String.ReceiptReturn), GetString(Resource.String.ReceiptRetiro), GetString(Resource.String.ReceiptAjust));

                RunOnUiThread(async () =>
                {
                    if (Operacion != Operaciones.Inventario)
                    {
                        var isKeyboardLocked = await LockTyping();
                        Adapter = new ReceiptAdapter(this, Operacion == Operaciones.Devolucion, actualconfig, caching, isKeyboardLocked);
                        Adapter.Onfinish += () =>
                        {
                            Clear();
                        };
                   }
                   else
                   {
                        SAdapter = new StockOnFloorAdapter(this, materials, actualconfig, caching);
                   }

                   var Title = String.Empty;

                   switch (Operacion)
                   {
                       case Operaciones.Entrega:
                           Title = GetString(Resource.String.ReceiptTitle);
                           break;

                       case Operaciones.Devolucion:
                           Title = GetString(Resource.String.ReceiptTitle2);
                           break;

                       case Operaciones.Inventario:
                           Title = GetString(Resource.String.ReceiptTitle3);
                           break;
                   }

                   txtViewTitle.Text = String.Format(Title, TurnID, Fecha);

                   Binding();
               });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private void lstReceipt_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                editScanBandejas.ClearFocus();
                Clear();
            }
        }

        private void Binding()
        {
            RunOnUiThread(() =>
            {
                if (Operacion != Operaciones.Inventario)
                    lstReceipt.Adapter = Adapter;
                else
                    lstReceipt.Adapter = SAdapter;
            });
        }

        private async void editScanBandejas_KeyPress(object sender, View.KeyEventArgs e)
        {            
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editScanBandejas.Text) && e.Event.Action == KeyEventActions.Down)
            {
                try
                {
                    if (Operacion != Operaciones.Inventario)
                    {
                        var keyboardManager = new KeyboardManager(this);
                        keyboardManager.HideSoftKeyboard(this);

                        Adapter.Add(editScanBandejas.Text);
                    }
                    else
                        SAdapter.Add(editScanBandejas.Text);

                    Clear();
                }
                catch (Exception ex)
                {
                    await CatchException(ex);
                }
                finally
                {
                    e.Handled = true;
                }
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (Operacion == Operaciones.Inventario)
                MenuInflater.Inflate(Resource.Menu.fixedMenu, menu);
            else
                MenuInflater.Inflate(Resource.Menu.GeneralMenu, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_Cancel:

                    Finish();

                    break;

                case Resource.Id.menu_Add:
                case Resource.Id.menu_Fix:

                    if (Util.TryLock(this, Util.Locks.ActividadRecepcion))
                    {
                        if (Operacion != Operaciones.Inventario)
                            UpdateStock();
                        else
                        {
                            Dialog dialog = null;
                            var builder = new AlertDialog.Builder(this);
                            var view = LayoutInflater.Inflate(Resource.Layout.question_dialog, null);
                            view.FindViewById<TextView>(Resource.Id.txtQuestion).Text = GetString(Resource.String.ReceiptConfirm);
                            builder.SetTitle(Resource.String.ApplicationLabel);
                            builder.SetView(view);

                            builder.SetPositiveButton(Resource.String.Accept, (sender, args) =>
                            {
                                Util.ReleaseLock(this, Util.Locks.ActividadRecepcion);
                                FixedStock();
                                dialog.Dismiss();
                                dialog.Dispose();
                            });

                            builder.SetNegativeButton(Resource.String.Cancel, (sender, args) =>
                            {
                                Util.ReleaseLock(this, Util.Locks.ActividadRecepcion);
                                dialog.Dismiss();
                                dialog.Dispose();
                            });

                            dialog = builder.Create();
                            builder.Show();
                        }
                    }

                    break;
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        public async void UpdateStock()
        {
            try
            {
                ShowProgress(true);

                if (Adapter.List.Any())
                {
                    var repot = repo.GetRepositoryTransactions();

                    var process = SecurityManager.CurrentProcess;

                    var transacciones = Adapter.List.SelectMany(m =>
                        m.Details.Select((d, n) => new Transactions()
                        {
                            TurnID = caching.Stock.TurnID,
                            CustomFecha = caching.GetProductionDate().GetDBDate(),
                            Fecha = caching.GetProductionDate().AddSeconds(n),
                            Lot = m.Lot,
                            MaterialCode = m.MaterialCode,
                            Quantity = Operacion == Operaciones.Entrega ? d.Quantity : d.Quantity * -1,
                            Sync = true,
                            Unit = m.MaterialUnit,
                            BoxNumber = d.BoxNumber,
                            Logon = process.Logon,
                            Reason = Operacion == Operaciones.Entrega ? GetString(Resource.String.ReceiptConceptReceive) : GetString(Resource.String.ReceiptRetiro)
                        })).ToList();

                    await repot.InsertAsyncAll(transacciones);

                    if (Operacion == Operaciones.Entrega)
                    {
                        Toast.MakeText(this, Resource.String.ReceiptMessages, ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, Resource.String.ReceiptMessages2, ToastLength.Long).Show();
                    }

                    Adapter.Clear();

                    Binding();
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {
                Util.ReleaseLock(this, Util.Locks.ActividadRecepcion);
                ShowProgress(false);
            }
        }

        private async void Clear()
        {
            try
            {
                if (!editScanBandejas.IsFocused)
                {
                    editScanBandejas.RequestFocus();
                    editScanBandejas.RequestFocusFromTouch();
                }
                editScanBandejas.Text = String.Empty;
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        public async void FixedStock()
        {
            var repot = repo.GetRepositoryTransactions();
            var repoc = repo.GetRepositoryConsumptions();
            var Stocks = SAdapter.List.Where(p => p.Total != 0).ToList();
            var process = SecurityManager.CurrentProcess;
            var ListTransaccions = new List<Transactions>();
            var ListConsumptions = new List<Consumptions>();

            foreach (var item in Stocks)
            {
                for (int i = 0; i < Math.Abs(item.Total); i++)
                {
                    var transaccion = new Transactions()
                    {
                        TurnID = caching.Stock.TurnID,
                        CustomFecha = caching.GetProductionDate().GetDBDate(),
                        Fecha = caching.GetProductionDate().AddSeconds(i),
                        Lot = item.Lot,
                        MaterialCode = item.ProductCode,
                        Quantity = item.Total > 0 ? item.AmountAjustado : item.AmountAjustado,
                        Unit = item.ProductUnit,
                        BoxNumber = 0,
                        Sync = true,
                        Logon = process.Logon,
                        Reason = GetString(Resource.String.ReceiptAjust)
                    };

                    ListTransaccions.Add(transaccion);

                    var consumo = new Consumptions()
                    {
                        ProcessID = process.Process,
                        Center = process.Centro,
                        Logon = process.Logon,
                        Produccion = caching.GetProductionDate().AddSeconds(i),
                        Fecha = DateTime.Now,
                        ProductCode = actualconfig.ProductCode,
                        MaterialCode = item.ProductCode,
                        Lot = item.Lot,
                        EquipmentID = process.EquipmentID,
                        SubEquipmentID = actualconfig.SubEquipmentID,
                        VerID = actualconfig.VerID,
                        Quantity = item.AmountAjustado * -1,
                        Unit = item.ProductUnit,
                        TurnID = caching.Stock.TurnID,
                        TimeID = actualconfig.TimeID,
                        BoxNumber = 0,
                        Sync = true,
                        SyncSQL =true,
                        CustomFecha = caching.GetProductionDate().GetDBDate(),
                        CustomID = await secuences.AddMaterial(item.ProductCode, item.Lot, true),
                        TrayID = String.Empty
                    };

                    ListConsumptions.Add(consumo);
                }
            }

            if (Stocks.Any())
            {
                await repot.InsertAsyncAll(ListTransaccions);
                await repoc.InsertAsyncAll(ListConsumptions);

                Toast.MakeText(this, Resource.String.ReceiptMessages3, ToastLength.Long).Show();
            }

            SetResult(Result.Ok);

            Finish();
        }
    }
}