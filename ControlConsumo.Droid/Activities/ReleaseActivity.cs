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
using ControlConsumo.Droid.Managers;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Droid.Activities.Adapters;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using ControlConsumo.Shared.Models.Process;
using Android.Content.PM;
using System.Threading.Tasks;
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Shared.Models.Z;
using System.Net;

namespace ControlConsumo.Droid.Activities
{
    [Activity(WindowSoftInputMode = SoftInput.AdjustPan, ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop)]
    public class ReleaseActivity : BaseActivity
    {
        private const String _PROCESO = "ReleaseActivity._PROCESO";
        private const String _TURNID = "ReleaseActivity._TURNID";
        private CachingManager Caching;
        private EditText editScanBandejas;
        private ListView listBandejas;
        private TextView txtViewTotal;
        private TextView txtViewTitle;
        private Boolean AlreadyScaned;
        private TraysReleaseAdapter adapter;
        private Int32 TurnID;
        private String Fecha;

        protected override async void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);

                SetContentView(Resource.Layout.release_trays_activity);
                SetTitle(Resource.String.ApplicationLabel);

                if (listBandejas == null)
                {
                    Caching = new CachingManager(this);
                    editScanBandejas = FindViewById<EditText>(Resource.Id.editScanBandejas);
                    listBandejas = FindViewById<ListView>(Resource.Id.listBandejas);
                    txtViewTotal = FindViewById<TextView>(Resource.Id.txtViewTotal);
                    txtViewTitle = FindViewById<TextView>(Resource.Id.txtViewTitle);
                    editScanBandejas.KeyPress += editScanBandejas_KeyPress;
                    listBandejas.FocusChange += listBandejas_FocusChange;
                    TurnID = Intent.GetIntExtra(CustExtras.TurnID.ToString(), 0);
                    Fecha = Intent.GetStringExtra(CustExtras.ProductionDate.ToString());
                }

                txtViewTitle.Text = String.Format(GetString(Resource.String.ReleaseEntryTitle), TurnID, Fecha);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected override async void OnSaveInstanceState(Bundle outState)
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                outState.PutString(_PROCESO, JsonConvert.SerializeObject(repoz.GetProces()));
                outState.PutInt(_TURNID, TurnID);
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
                TurnID = savedInstanceState.GetInt(_TURNID);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private void listBandejas_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                editScanBandejas.ClearFocus();
                Clear();
            }
        }

        //private async void Preload()
        //{
        //    var repom = repo.GetRepositoryMaterials();
        //    Materiales = await repom.GetAsyncAll();
        //}

        private async void editScanBandejas_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editScanBandejas.Text) && !AlreadyScaned && e.Event.Action == KeyEventActions.Down)
            {
                try
                {
                    AlreadyScaned = true;

                    if (adapter == null)
                    {
                        adapter = new TraysReleaseAdapter(this);
                        listBandejas.Adapter = adapter;
                    }

                    var barcode = editScanBandejas.Text.GetBarCode();

                    var bandejas = Caching.bandejas;

                    if (!bandejas.Any(p => p.ID == barcode.BarCode && (p.Desde <= barcode.Sequence || p.Hasta >= barcode.Sequence)))
                    {
                        new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.OutBarCodeWrong), editScanBandejas.Text));
                        Clear();
                        return;
                    }

                    var repoz = repo.GetRepositoryZ();

                    try
                    {
                        var repoTray = repo.GetRepositoryTrays();

                        var BarCode = barcode.BarCode;

                        var bandeja = new TraysList();

                        var configuracionBandeja = await repoTray.GetAsyncByKey(barcode.BarCode);
                        if (configuracionBandeja != null)
                        {
                            if (configuracionBandeja.procesarSAP)
                            {
                                bandeja = await repoz.GetBandejaSalida(barcode.BarCode, (Int16)barcode.Sequence);
                            }
                            else
                            {
                                try
                                {
                                    bandeja = await repoz.GetEstatusBandeja(barcode.BarCode, barcode.Sequence);
                                }
                                catch (WebException wEx)
                                {
                                    ShowWebExceptionDialog(wEx, "Consulta de estatus de bandeja - Liberación de bandejas");
                                    return;
                                }
                            }
                        }

                        if (!ValidarBandejaLlena(bandeja))
                        {
                            return;
                        }

                        bandeja.BarCode = barcode.FullBarCode;

                        adapter.Add(bandeja);

                        txtViewTotal.Text = adapter.GetTotal().ToString();
                    }
                    catch (WebException wEx)
                    {
                        ShowWebExceptionDialog(wEx, "Liberación de bandejas");
                    }
                    finally
                    {
                        Clear();
                    }
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
            try
            {
                MenuInflater.Inflate(Resource.Menu.GeneralMenu, menu);
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
                }
            }
            catch (Exception ex)
            {
                CatchException(ex);
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        private async void Save()
        {
            try
            {
                ShowProgress(true);

                if (adapter == null) return;

                var bandejas = adapter.GetTrays;
                var buffer = new List<TraysProducts>();
                var repot = repo.GetRepositoryTraysProducts();
                var repop = repo.GetRepositoryTraysRelease();
                var Proceso = await repo.GetRepositoryZ().GetProces();

                if (bandejas == null) return;

                var traza = adapter.Bandejas.Where(p => !String.IsNullOrEmpty(p.EquipmentID)).ToList();
                var repoTray = repo.GetRepositoryTrays();

                await repo.GetRepositoryZ().ReleaseTraza(traza);
                var usuario = SecurityManager.CurrentProcess != null ? SecurityManager.CurrentProcess.Logon : Proceso.Logon;
                foreach (var bandeja in bandejas)
                {
                    var barcode = bandeja.GetBarCode();
                    var configuracionBandeja = new Trays();
                    configuracionBandeja = await repoTray.GetAsyncByKey(barcode.BarCode);
                    if (configuracionBandeja != null)
                    {
                        buffer.Add(new TraysProducts()
                        {
                            ID = barcode.ID,
                            TrayID = barcode.BarCode,
                            Secuencia = (short)barcode.Sequence,
                            ModifyDate = DateTime.Now,
                            Quantity = 0,
                            Status = TraysProducts._Status.Vacio,
                            Sync = configuracionBandeja.procesarSAP,
                            Unit = String.Empty,
                            VerID = String.Empty,
                            TimeID = String.Empty,
                            ProductCode = String.Empty,
                            EquipmentID = String.Empty,
                            Fecha = null,
                            ElaborateID = 0,
                            FechaHoraVaciada = DateTime.Now,
                            UsuarioVaciada = usuario,
                            IdEquipoVaciado = Proceso.EquipmentID,
                            formaVaciada = "Manual"
                        });
                    }
                }

                if (IsThereConnection)
                {
                    
                    foreach (var tray in buffer.ToArray())
                    {
                        var configuracionBandeja = new Trays();
                        configuracionBandeja = await repoTray.GetAsyncByKey(tray.TrayID);
                        if (configuracionBandeja != null)
                        {
                            if (!configuracionBandeja.procesarSAP)
                            {
                                try
                                {
                                    TraysProducts[] traysProducts = new TraysProducts[] { tray };
                                    await repot.InsertOrUpdateAsyncSql(traysProducts); //Guardando datos en línea con SQL Server
                                }
                                catch (WebException wEx)
                                {
                                    ShowWebExceptionDialog(wEx, "Liberación de bandejas");
                                    return;
                                }
                                catch (System.OperationCanceledException ex)
                                {
                                    CustomDialog dialogErr = new CustomDialog(this, CustomDialog.Status.Error, ex.Message);
                                    await Util.SaveException(ex);
                                    return;
                                }
                            }
                            else
                            {
                                await repot.InsertOrReplaceAsync(tray);
                            }
                        }
                    }
                }
                else
                {
                    var dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.NoConection));
                    return;
                }

                //await repot.InsertOrReplaceAsyncAll(buffer);

                await repop.InsertAsync(new TraysRelease()
                {
                    Fecha = DateTime.Now,
                    Logon = SecurityManager.CurrentProcess.Logon,
                    Sync = true,
                    Positions = bandejas.Select(p => new TraysReleasePosition
                    {
                        TrayID = p
                    }).ToList()
                });

                adapter.Clear();

                txtViewTotal.Text = String.Empty;

                editScanBandejas.RequestFocus();

                Toast.MakeText(this, Resource.String.ReleaseEntryFinished, ToastLength.Long).Show();
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

        private async void Clear()
        {
            try
            {
                AlreadyScaned = false;
                editScanBandejas.RequestFocus();
                editScanBandejas.RequestFocusFromTouch();
                editScanBandejas.Text = String.Empty;
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private bool ValidarBandejaLlena(TraysList bandeja)
        {
            if(bandeja.Status == TraysProducts._Status.Vacio)
            {
                var dialog = new CustomDialog(this, CustomDialog.Status.EmptyTray, $"Bandeja {bandeja.TrayID + bandeja.Secuencia.ToString("00")} sin producto",
                    CustomDialog.ButtonStyles.NoButton);
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}