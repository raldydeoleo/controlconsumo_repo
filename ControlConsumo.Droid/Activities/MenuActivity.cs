using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Droid.Activities.Adapters.Entities;
using ControlConsumo.Droid.Activities.Adapters;
using Android.Support.V4.Widget;
using ControlConsumo.Shared.Models.Process;
using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Tables;
using System.Threading.Tasks;
using Android.Views.InputMethods;
using ControlConsumo.Shared.Models.Material;
using ControlConsumo.Shared.Models.Lot;
using ControlConsumo.Droid.Sync;
using ControlConsumo.Droid.Managers;
using Android.Content.Res;
using Android.Content.PM;
using Newtonsoft.Json;
using ControlConsumo.Shared.Models.Z;
using System.Globalization;
using Android.Preferences;
using ControlConsumo.Droid.Activities.Bundles;
using System.Threading;
using ControlConsumo.Shared.Models.TrayProduct;
using ControlConsumo.Shared;
using ControlConsumo.Shared.Repositories;
using static ControlConsumo.Shared.Tables.Syncro;
using System.Net;
using static ControlConsumo.Shared.Tables.Times;

namespace ControlConsumo.Droid.Activities
{
    [Activity(ScreenOrientation = ScreenOrientation.Landscape, WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateAlwaysHidden, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.Navigation | ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MenuActivity : BaseActivity
    {
        public const String _ACTUALSCREEN = "MenuActivity._ACTUALSCREEN";
        public const String _PROCESO = "MenuActivity._PROCESO";
        private const String _USER = "MenuActivity._USER";
        private const String _ACTUALCONFIG = "MenuActivity._ACTUALCONFIG";
        private const String _NEXTCONFIG = "MenuActivity._NEXTCONFIG";
        private const String _CACHING = "MenuActivity._CACHING";
        private const String _ROUTE = "MenuActivity._ROUTE";
        private const String _SECURITY = "MenuActivity._SECURITY";
        private const String _SECUENCES = "MenuActivity._SECUENCES";

        public ServiceBinder binder { get; set; }
        private Boolean isEventReady { get; set; }
        private Boolean isLoadingEquipment { get; set; }
        private ServiceConnection serviceConnection { get; set; }
        private IEnumerable<DrawerEntry> _drawerEntries { get; set; }
        private IEnumerable<ReportEntry> _reportEntries { get; set; }
        private DrawerLayout _drawerLayout { get; set; }
        private MyActionBarDrawerToggle _drawerToggle { get; set; }
        private LScreens LScreen { get; set; }
        private NextConfig nextconfig { get; set; }
        private ActualConfig actualConfig { get; set; }
        private Consumptions consumo { get; set; }
        public MaterialList material { get; set; }
        private Elaborates Salida { get; set; }
        public TraysList bandeja { get; set; }
        private SecurityManager Security { get; set; }
        private IMenu menu { get; set; }
        private WasteAdapter WasteAdapter { get; set; }
        private ElaborateTotal loteVencimiento;
        private Boolean IsExpired { get; set; }
        private CachingManager Caching;
        private CustomSequencesManager SequenceManager = new CustomSequencesManager();
        private RouteManager Routes { get; set; }
        // private PrinterManager printer { get; set; }
        private CuadreDialog._Options Option = CuadreDialog._Options.NONE;

        public enum LScreens
        {
            NoConfig,
            Active,
            Choose,
            ScanInput,
            ScanOutput,
            ReportOperator,
            Wastes,
            ReportEntrada,
            ReportSalida,
            ReportPeso,
            ReportInventario,
            ReportBillofMaterial,
            ReportInventatioResumen
        }

        #region Declaraciones de Controles Generales

        private ListView _drawerList { get; set; }
        private LinearLayout lyBodyCommon { get; set; }
        private ViewFlipper flipper { get; set; }
        private TextView txtViewProductoLarge { get; set; }
        private TextView txtViewProductoLargeBOM { get; set; }
        private TextView txtViewFCode { get; set; }
        private TextView txtViewCodeBOM { get; set; }
        private TextView txtViewMaterialBOM { get; set; }
        private TextView txtViewSupCodeBOM { get; set; }
        private TextView txtViewUnidadBOM { get; set; }
        private TextView txtViewProductoShort { get; set; }
        private TextView txtViewProductoShortBOM { get; set; }
        private TextView txtViewBatchID { get; set; }
        private TextView txtViewSecuencia { get; set; }
        private LinearLayout linearLayoutSecuencia { get; set; }
        private TextView txtViewAlmacenamiento { get; set; }
        private TextView txtViewPackageID { get; set; }
        private TextView txtContadorBandejasConsumidas { get; set; }        
        private LinearLayout linearLayoutEmpaque { get; set; }
        private LinearLayout linearLayoutBandejasConsumidas { get; set; }
        private LinearLayout linearLayoutCigarrosConsumidos { get; set; }  //LINEA AGREGADA PARA MOSTRAR CANTIDAD DE CIGARROS CONSUMIDOS POR PRODUCTO Y TURNO
        private TextView txtContadorCigarrosConsumidos { get; set; }  //LINEA AGREGADA PARA MOSTRAR CANTIDAD DE CIGARROS CONSUMIDOS POR PRODUCTO Y TURNO
        //private TextView txtViewCigarrosConsumidos { get; set; } //LINEA AGREGADA PARA MOSTRAR CANTIDAD DE CIGARROS CONSUMIDOS POR PRODUCTO Y TURNO


        #endregion

        #region Declaraciones de Activacion de Configuracion

        private TextView txtViewMessagesActivation { get; set; }
        private EditText editScan { get; set; }
        private EditText editScanEquipment { get; set; }
        private Button btnCancelActivate { get; set; }
        private ListView listConfigs { get; set; }

        #endregion

        #region Declaraciones de no configuracion

        private TextView txtViewNoEquipment { get; set; }
        private TextView txtViewNoConfig { get; set; }

        #endregion

        #region Declaraciones de Choose

        private Button btnEntrada { get; set; }
        private Button btnSalida { get; set; }
        private Button btnDesperdicios { get; set; }
        private Button btnConsultas { get; set; }

        #endregion

        #region Declaraciones de Entrada de Materiales

        private EditText editScanEntrada { get; set; }
        private TextView txtCodeEntrada { get; set; }
        private TextView txtViewDescripcionEntrada { get; set; }
        private TextView txtViewCantidadEntrada { get; set; }
        private TextView txtViewBatch { get; set; }
        private TextView txtViewBatchSap { get; set; }
        private Button btnCancelEntrada { get; set; }

        #endregion

        #region Declaraciones de Salida de Materiales

        private EditText editScanSalida { get; set; }
        private TextView txtViewBandeja { get; set; }
        private TextView txtViewCantidadSalida { get; set; }
        private TextView txtViewUnidadSalida { get; set; }
        private TextView txtViewSalidaLabel { get; set; }
        private TextView txtViewBandejaLabel { get; set; }
        private Button btnCancelSalida { get; set; }
        private TextView txtViewSalidaTitle { get; set; }
       
        

        #endregion

        #region Declaraciones de los Reportes

        private ListView lstProduccion { get; set; }
        private ListView lstConsumo { get; set; }
        private GridView gridView { get; set; }
        private ExpandableListView ListStock { get; set; }
        private ViewFlipper viewFlipperStock { get; set; }
        private ListView lstBomReport { get; set; }

        #endregion

        #region Declaraciones de los Desperdicios

        private ListView listDesperdicios { get; set; }
        private TextView txtViewWasteTitle { get; set; }

        #endregion

        protected override async void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);

                SetContentView(Resource.Layout.Menu_Activity);
                SetTitle(Resource.String.ApplicationLabel);

                ActionBar.SetDisplayHomeAsUpEnabled(true);
                ActionBar.SetHomeButtonEnabled(true);

                if (_drawerList == null)
                {
                    InitDrawerLayout();
                    InitControls();
                    InitControlsHeader();
                    Caching = new CachingManager(this);
                    Caching.OnLoaded += async (TurnoID, HasEquipment) =>
                    {
                        var repoz = repo.GetRepositoryZ();
                        await SetTurn();

                        if (HasEquipment)
                        {
                            var proceso = await repoz.GetProces();
                            actualConfig = await repoz.GetActualConfig(proceso.EquipmentID);
                            if (actualConfig != null && (await IsFinalProcess()))
                                AssignPackID(Caching.GetPackID(actualConfig.EquipmentID));
                        }
                    };
                    Security = new SecurityManager(this);
                    Security.Response += Security_Response;

                    //if (CachingManager.MaxMinutes == 0)
                    //{
                    //    var arregloTurno = Resources.GetStringArray(Resource.Array.MinutosTurno);
                    //    var MaxMinutes = arregloTurno.Max(p => Convert.ToByte(p));
                    //    Caching.SetMaxMinutes(MaxMinutes);
                    //}
                }

                serviceConnection = LastNonConfigurationInstance as ServiceConnection;

                if (serviceConnection != null)
                {
                    binder = serviceConnection.binder;
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void Service_ReviewWarningsAsync()
        {
            try
            {
                if (!Util.CheckLock(this, Util.Locks.Turnos) && Util.TryLock(this, Util.Locks.CallBackSync))
                {
                    ThreadPool.QueueUserWorkItem(o => ShowAlertNextConfig());
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void Service_ONReturnConsumptions(List<Consumptions> Consumos)
        {
            try
            {
                var repoz = repo.GetRepositoryZ();

                var Proceso = await repoz.GetProces();

                if (actualConfig == null)
                    actualConfig = await repoz.GetActualConfig(Proceso.EquipmentID);

                foreach (var item in Consumos)
                {
                    await SequenceManager.AddMaterial(item.MaterialCode, item.Lot, item.CustomID);
                }

                var bandeja = Consumos.OrderByDescending(o => o.Fecha).FirstOrDefault(w => !String.IsNullOrEmpty(w.TrayID));

                if (bandeja != null)
                {
                    var barcode = bandeja.TrayID.GetBarCode();

                    Routes.SetBandeja(new TraysList()
                    {
                        BarCode = bandeja.TrayID,
                        Secuencia = barcode.Sequence,
                        BatchID = bandeja.BatchID,
                        EquipmentID = bandeja.TrayEquipmentID,
                        ProductCode = actualConfig.ProductCode,
                        VerID = actualConfig.VerID,
                        Unit = actualConfig.ProductUnit,
                        Fecha = bandeja.TrayDate,
                        Status = TraysProducts._Status.Lleno,
                        ElaborateID = bandeja.ElaborateID,
                        Quantity = bandeja.Quantity,
                        TimeID = bandeja.TimeID,
                        TrayID = barcode.BarCode
                    });

                    if (Routes != null)
                        Routes.LoadRoute(bandeja.Produccion);

                    AssignBatchID(bandeja.BatchID);
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void Security_Response(bool IsAutorize, DrawerEntry Entry)
        {
            try
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();

                if (!IsAutorize)
                {
                    return;
                }

                switch (Entry.Permit)
                {
                    case RolsPermits.Permits.AUTORIZAR_SALIDA_PARCIAL:
                        try
                        {
                            Dialog dialogAuthorizePartialElaborate = null;
                            Switch SwtPartialElaborate;

                            var builderPartialElaborate = new AlertDialog.Builder(this);
                            builderPartialElaborate.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                            builderPartialElaborate.SetTitle(Resource.String.PartialElaborateConfig);

                            var viewPartialElaborate = LayoutInflater.Inflate(Resource.Layout.dialog_authorize_partial_elaborate, null);

                            SwtPartialElaborate = viewPartialElaborate.FindViewById<Switch>(Resource.Id.SwtPartialElaborate);
                            SwtPartialElaborate.Checked = Proceso.IsPartialElaborateAuthorized;

                            builderPartialElaborate.SetView(viewPartialElaborate);

                            builderPartialElaborate.SetPositiveButton(Resource.String.Accept, async (sender, args) =>
                            {
                                var repoSetting = repo.GetRepositorySettings();
                                await repoSetting.InsertOrReplaceAsync(new Settings()
                                {
                                    Key = Settings.Params.IsPartialElaborateAuthorized,
                                    Value = SwtPartialElaborate.Checked.ToString().ToLower()
                                });

                                Proceso.IsPartialElaborateAuthorized = SwtPartialElaborate.Checked;
                                repo.GetRepositoryZ().SetProcess(Proceso);

                                if (Proceso.IsPartialElaborateAuthorized)
                                {
                                    Toast.MakeText(this, "Salida Parcial Activada", ToastLength.Long).Show();
                                    txtViewSalidaTitle.Text = GetString(Resource.String.OutPartialElaborate);
                                }
                                else
                                {
                                    Toast.MakeText(this, "Salida Parcial Desactivada", ToastLength.Long).Show();
                                    txtViewSalidaTitle.Text = GetString(Resource.String.Out);
                                }
                            });

                            builderPartialElaborate.SetNegativeButton(Resource.String.Cancel, (sender, args) =>
                            {
                                dialogAuthorizePartialElaborate.Dismiss();
                                dialogAuthorizePartialElaborate.Dispose();
                            });


                            dialogAuthorizePartialElaborate = builderPartialElaborate.Create();

                            RunOnUiThread(() =>
                            {
                                dialogAuthorizePartialElaborate.Show();
                            });

                        }
                        catch(Exception ex)
                        {
                            await Util.SaveException(ex);
                            new CustomDialog(this, CustomDialog.Status.Error, "No se pudo autorizar la salida parcial");
                        }
                        break;

                    case RolsPermits.Permits.ACTUALIZAR_BANDEJAS:

                        try
                        {
                            var repoBandejas = repo.GetRepositoryTraysProducts();
                            ShowProgress(true, Resource.String.MessageUpdateBandejas);
                            await repoBandejas.SyncAsync(false);
                        }
                        catch (Exception ex)
                        {
                            await Util.SaveException(ex);
                            new CustomDialog(this, CustomDialog.Status.Warning, "No se pudo actualizar las bandejas, favor intentar nuevamente.");
                        }
                        finally
                        {
                            ShowProgress(false);
                        }

                        break;

                    case RolsPermits.Permits.ASIGNACION:

                        if (String.IsNullOrEmpty(Proceso.EquipmentID))
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.DrawerChangeErrorMessage));
                            return;
                        }

                        //if (Proceso.IsSubEquipment && serviceConnection.binder.Service.IsConnected)
                        //{
                        //    new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.DrawerChangeErrorMessageNoAvailable));
                        //    return;
                        //}

                        SetScreen(LScreens.Active);
                        break;

                    case RolsPermits.Permits.OPERACION:

                        if (actualConfig == null)
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.DrawerChangeErrorNoconfig));
                            return;
                        }
                        SetScreen(LScreens.Choose);

                        break;

                    case RolsPermits.Permits.DEVOLUCION:

                        /*
                        if (Proceso.IsSubEquipment)
                        {
                            new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.DrawerChangeErrorMessageNoAvailable));
                            return;
                        }*/

                        if (String.IsNullOrEmpty(Proceso.EquipmentID))
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.DrawerChangeErrorMessage));
                            return;
                        }

                        var intent = new Intent(this, Entry.activity);
                        intent.PutExtra(CustExtras.ProductionDate.ToString(), Caching.GetProductionDate().GetSapDate());
                        intent.PutExtra(CustExtras.TurnID.ToString(), Caching.Stock.TurnID.ToString());
                        StartActivity(intent);

                        break;

                    case RolsPermits.Permits.REPORTAR_VARILLAS:

                        if (Proceso.IsSubEquipment)
                        {
                            new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.DrawerChangeErrorMessageNoAvailable));
                            return;
                        }

                        Dialog dialog = null;

                        var builder = new AlertDialog.Builder(this);
                        builder.SetIcon(Android.Resource.Drawable.IcMenuAgenda);
                        builder.SetTitle(Resource.String.VarillaChooseTitle);

                        var view = LayoutInflater.Inflate(Resource.Layout.dialog_choose_turn_date, null);
                        var spnTurn = view.FindViewById<Spinner>(Resource.Id.spnTurn);
                        var spnFecha = view.FindViewById<Spinner>(Resource.Id.spnFecha);

                        builder.SetView(view);
                        builder.SetNegativeButton(Resource.String.Cancel, (sender, args) =>
                        {
                            dialog.Dismiss();
                            dialog.Dispose();
                        });

                        var turnos = Caching.turnos.Select(p => p.ID.ToString()).ToList();
                        var lista = new List<String>();
                        lista.Add(DateTime.Now.ToString("dd MMMM yyyy"));
                        lista.Add(DateTime.Now.AddDays(-1).ToString("dd MMMM yyyy"));
                        lista.Add(DateTime.Now.AddDays(-2).ToString("dd MMMM yyyy"));
                        lista.Add(DateTime.Now.AddDays(-3).ToString("dd MMMM yyyy"));
                        lista.Add(DateTime.Now.AddDays(-4).ToString("dd MMMM yyyy"));
                        lista.Add(DateTime.Now.AddDays(-5).ToString("dd MMMM yyyy"));

                        spnTurn.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, turnos);
                        spnFecha.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, lista);

                        builder.SetPositiveButton(Resource.String.Accept, (sender, args) =>
                        {
                            intent = new Intent(this, Entry.activity);
                            intent.PutExtra(CustExtras.ProductionDate.ToString(), (spnFecha.SelectedItemPosition * -1));
                            intent.PutExtra(CustExtras.TurnID.ToString(), spnTurn.SelectedItem.ToString());
                            StartActivityForResult(intent, (int)Entry.Permit);
                            dialog.Dismiss();
                            dialog.Dispose();
                        });

                        dialog = builder.Create();

                        RunOnUiThread(() =>
                        {
                            dialog.Show();
                        });

                        break;

                    case RolsPermits.Permits.ENTREGA_MATERIALES:

                        if (String.IsNullOrEmpty(Proceso.EquipmentID))
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.DrawerChangeErrorMessage));
                            return;
                        }

                        //if (Proceso.IsSubEquipment)
                        //{
                        //    new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.DrawerChangeErrorMessageNoAvailable));
                        //    return;
                        //}

                        dialog = null;

                        builder = new AlertDialog.Builder(this);
                        builder.SetIcon(Android.Resource.Drawable.IcMenuAgenda);
                        builder.SetTitle(Resource.String.ReceiptTypeTitle);

                        view = LayoutInflater.Inflate(Resource.Layout.dialog_lots, null);

                        builder.SetView(view);

                        builder.SetNegativeButton(Resource.String.Cancel, (sender, args) =>
                        {
                            dialog.Dismiss();
                            dialog.Dispose();
                        });

                        lista = new List<String>();
                        lista.Add(GetString(Resource.String.ReceiptTypeEquipo));
                        lista.Add(GetString(Resource.String.ReceiptTypeBuffer));
                        lista.Add(GetString(Resource.String.ReceiptTypeStock));

                        var list = view.FindViewById<ListView>(Resource.Id.lstlots);
                        list.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, lista);

                        list.ItemClick += (sender, item) =>
                        {
                            intent = new Intent(this, Entry.activity);
                            intent.PutExtra(CustExtras.TurnID.ToString(), ((Int32)Caching.TurnoID).ToString());
                            intent.PutExtra(CustExtras.ProductionDate.ToString(), Caching.GetProductionDate().ToString("dd MMMM yyyy"));

                            switch (item.Position)
                            {
                                case 0:
                                    intent.PutExtra(CustExtras.Operacion.ToString(), (Int32)ReceiptActivity.Operaciones.Entrega);
                                    break;

                                case 1:
                                    intent.PutExtra(CustExtras.Operacion.ToString(), (Int32)ReceiptActivity.Operaciones.Devolucion);
                                    break;

                                case 2:
                                    intent.PutExtra(CustExtras.Operacion.ToString(), (Int32)ReceiptActivity.Operaciones.Inventario);
                                    break;
                            }

                            StartActivityForResult(intent, (int)Entry.Permit);
                            dialog.Dismiss();
                            dialog.Dispose();
                        };

                        dialog = builder.Create();

                        RunOnUiThread(() =>
                        {
                            dialog.Show();
                        });

                        break;

                    //case RolsPermits.Permits.DEVOLUCION_PRODUCTO:

                    //    intent = new Intent(this, Entry.activity);

                    //    if (Caching.Stock != null)
                    //    {
                    //        intent.PutExtra(CustExtras.TurnID.ToString(), Caching.Stock.TurnID.ToString());
                    //        intent.PutExtra(CustExtras.ProductionDate.ToString(), Caching.GetProductionDate().ToString("yyyyMMdd"));
                    //    }

                    //    StartActivityForResult(intent, (int)Entry.Permit);

                    //    break;

                    case RolsPermits.Permits.CIERRES:

                        if (String.IsNullOrEmpty(Proceso.EquipmentID))
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.DrawerChangeErrorMessage));
                            return;
                        }

                        var listTurns = await repo.GetRepositoryZ().GetDateandTurns();

                        dialog = null;

                        builder = new AlertDialog.Builder(this);
                        builder.SetIcon(Android.Resource.Drawable.IcMenuWeek);
                        builder.SetTitle(Resource.String.DrawerClose);

                        #region Init Layout

                        view = LayoutInflater.Inflate(Resource.Layout.dialog_choose_date_turn, null);
                        spnFecha = view.FindViewById<Spinner>(Resource.Id.spnFecha);
                        spnTurn = view.FindViewById<Spinner>(Resource.Id.spnTurn);

                        builder.SetView(view);

                        #endregion

                        spnFecha.Adapter = new SelectedSpinnerAdapter(this, listTurns.Select(p => p.Produccion.ToString("dd MMMM yyyy")).Distinct().ToList(), true);
                        spnFecha.ItemSelected += (s, args) =>
                        {
                            var turno = listTurns.ElementAt(args.Position);
                            var turnos2 = listTurns.Where(p => p.Produccion == turno.Produccion).Select(p => p.TurnID.ToString()).ToList();
                            spnTurn.Adapter = new SelectedSpinnerAdapter(this, turnos2, true);
                        };

                        builder.SetNegativeButton(Resource.String.Cancel, (sender, args) =>
                        {
                            dialog.Dismiss();
                            dialog.Dispose();
                        });

                        builder.SetPositiveButton(Resource.String.Accept, (sender, args) =>
                        {
                            if (listTurns.Any())
                            {
                                var turno = listTurns.Single(p => p.Produccion.ToString("dd MMMM yyyy") == spnFecha.SelectedItem.ToString() && p.TurnID.ToString() == spnTurn.SelectedItem.ToString());
                                intent = new Intent(this, Entry.activity);
                                intent.PutExtra(CustExtras.ProductionDate.ToString(), turno.Produccion.GetDBDateL().ToString());
                                intent.PutExtra(CustExtras.TurnID.ToString(), spnTurn.SelectedItem.ToString());
                                intent.PutExtra(CustExtras.IsNotify.ToString(), turno.IsNotified);
                                StartActivityForResult(intent, (int)Entry.Permit);
                                dialog.Dismiss();
                                dialog.Dispose();
                            }
                        });

                        dialog = builder.Create();

                        RunOnUiThread(() =>
                        {
                            dialog.Show();
                        });

                        break;

                    case RolsPermits.Permits.PLANIFICACION:

                        //if (Proceso.IsSubEquipment && serviceConnection.binder.Service.IsConnected)
                        //{
                        //    new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.DrawerChangeErrorMessageNoAvailable));
                        //    return;
                        //}

                        intent = new Intent(this, Entry.activity);
                        if (Caching.Stock != null)
                        {
                            intent.PutExtra(CustExtras.TurnID.ToString(), (Int32)Caching.Stock.TurnID);
                            intent.PutExtra(CustExtras.ProductionDate.ToString(), Caching.GetProductionDate().ToString("dd MMMM yyyy"));
                        }

                        StartActivityForResult(intent, (int)Entry.Permit);

                        break;

                    default:
                        if (Entry.activity != null)
                        {
                            intent = new Intent(this, Entry.activity);
                            if (Caching.Stock != null)
                            {
                                intent.PutExtra(CustExtras.TurnID.ToString(), (Int32)Caching.Stock.TurnID);
                                intent.PutExtra(CustExtras.ProductionDate.ToString(), Caching.GetProductionDate().ToString("dd MMMM yyyy"));
                            }

                            StartActivityForResult(intent, (int)Entry.Permit);
                        }
                        break;
                }

                _drawerLayout.CloseDrawers();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected override async void OnStart()
        {
            try
            {
                base.OnStart();
                BindService();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void BindService()
        {
            try
            {
                var ServiceIntent = new Intent(this, typeof(SyncService));
                serviceConnection = new ServiceConnection(this);
                BindService(ServiceIntent, serviceConnection, Bind.AutoCreate);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        public async void BindEventos()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();

                var Proceso = await repoz.GetProces();
                var setting = await repoz.GetSettingAsync<Boolean>(Settings.Params.EquipmentSynced, false);

                if (binder != null && binder.Service != null && setting)
                {
                    isEventReady = true;
                    binder.Service.ReviewWarnings += Service_ReviewWarningsAsync;
                    binder.Service.OnMasterDataUpdated += Service_OnMasterDataUpdated;
                    if (Proceso.IsSubEquipment)
                    {
                        binder.Service.ONReturnConsumtions += Service_ONReturnConsumptions;
                    }
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void Service_OnMasterDataUpdated()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(o => LoadActualConfig(true));
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        public async void UnBindEventos()
        {
            try
            {
                if (binder != null)
                {
                    var Proceso = await repo.GetRepositoryZ().GetProces();
                    binder.Service.ReviewWarnings -= Service_ReviewWarningsAsync;
                    binder.Service.OnMasterDataUpdated -= Service_OnMasterDataUpdated;
                    if (Proceso.IsSubEquipment)
                    {
                        binder.Service.ONReturnConsumtions -= Service_ONReturnConsumptions;
                    }
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected override async void OnResume()
        {
            try
            {
                base.OnResume();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Util.ReleaseLock(this, Util.Locks.Reimprimir, Util.Locks.Turnos, Util.Locks.ChangeProduct);
                await Task.Run(async () =>
                {
                    var repoz = repo.GetRepositoryZ();
                    var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                    var proceso = await repoz.GetProces();
                    if (proceso == null)
                    {
                        var value = prefs.GetString(_PROCESO, null);
                        if (value != null)
                        {
                            proceso = JsonConvert.DeserializeObject<ProcessList>(prefs.GetString(_PROCESO, null));
                            repoz.SetProcess(proceso);
                        }
                    }

                    if (Routes == null)
                    {
                        Routes = new RouteManager();
                    }

                    Routes.Load();
                    BindService();
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        [Obsolete]
        public override  Java.Lang.Object OnRetainNonConfigurationInstance()
        {
            try
            {
                return base.OnRetainNonConfigurationInstance();
            }
            catch (Exception ex)
            {
                CatchException(ex);
            }

            return serviceConnection;
        }

        protected override async void OnSaveInstanceState(Bundle outState)
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                outState.PutString(_PROCESO, JsonConvert.SerializeObject(repoz.GetProcesSync()));
                outState.PutString(_ACTUALCONFIG, JsonConvert.SerializeObject(actualConfig));
                outState.PutString(_NEXTCONFIG, JsonConvert.SerializeObject(nextconfig));
                outState.PutString(_USER, JsonConvert.SerializeObject(repoz.GetUser()));

                var bundle = new MenuBundles()
                {
                    isEventReady = isEventReady,
                    ProductCode = Caching.ProductCode,
                    VerID = Caching.VerID,
                    bandejas = Caching.bandejas,
                    Copies = Caching.Copies,
                    turnos = Caching.turnos,
                    Stock = Caching.Stock,
                    Configs = Caching.Configs ?? new List<NextConfig>()
                    // Materials = Caching.Materials, No agregar saca la actividad por la alta carga
                    // Batches = Caching.Batches
                };

                outState.PutString(_CACHING, JsonConvert.SerializeObject(bundle));
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
                if (savedInstanceState != null)
                {
                    var repoz = repo.GetRepositoryZ();
                    if (savedInstanceState.GetString(_ACTUALCONFIG) != null) actualConfig = JsonConvert.DeserializeObject<ActualConfig>(savedInstanceState.GetString(_ACTUALCONFIG));
                    if (savedInstanceState.GetString(_NEXTCONFIG) != null) nextconfig = JsonConvert.DeserializeObject<NextConfig>(savedInstanceState.GetString(_NEXTCONFIG));
                    if (savedInstanceState.GetString(_USER) != null) repoz.SetUser(JsonConvert.DeserializeObject<Users>(savedInstanceState.GetString(_USER)));
                    if (savedInstanceState.GetString(_PROCESO) != null) repoz.SetProcess(JsonConvert.DeserializeObject<ProcessList>(savedInstanceState.GetString(_PROCESO)));
                    if (!String.IsNullOrEmpty(savedInstanceState.GetString(_CACHING)))
                    {
                        var bundle = JsonConvert.DeserializeObject<MenuBundles>(savedInstanceState.GetString(_CACHING));
                        isEventReady = bundle.isEventReady;
                        Caching.SetBundle(bundle);
                    }

                    await Task.Run(async () =>
                    {
                        var proceso = await repoz.GetProces();
                        if (actualConfig == null) actualConfig = await repoz.GetActualConfig(proceso.EquipmentID);
                        if (nextconfig == null) nextconfig = await repoz.GetNextConfig(proceso.EquipmentID);
                    });
                }

                BindService();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected override async void OnPause()
        {
            try
            {
                base.OnPause();
                await Task.Run(async () =>
                {
                    var repoz = repo.GetRepositoryZ();
                    var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                    var editor = prefs.Edit();
                    var proceso = await repoz.GetProces();
                    editor.PutString(_PROCESO, JsonConvert.SerializeObject(proceso));
                    if (!String.IsNullOrEmpty(proceso.EquipmentID) && LScreen >= LScreens.Choose)
                    {
                        editor.PutInt(_ACTUALSCREEN, (Int32)LScreen); /// Pantalla Actual Antes de la Pausa
                    }
                    editor.Commit();
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        public override void OnBackPressed()
        {
            RunBackPressed();
        }

        private async void RunBackPressed()
        {
            linearLayoutCigarrosConsumidos.Visibility = ViewStates.Gone; //Ocultar Cantidad Cigarros linearLayoutCigarrosConsumidos

            if (Option != CuadreDialog._Options.NONE)
            {
                CierraCuadre(Caching.Stock);
                return;
            }

            try
            {
                _drawerLayout.CloseDrawers();

                switch (LScreen)
                {
                    case LScreens.ScanInput:
                    case LScreens.ScanOutput:
                        SetScreen(LScreens.Choose);

                        break;

                    case LScreens.ReportOperator:
                        SetScreen(LScreens.Choose);
                        frmErrors.Visibility = ViewStates.Visible;
                        break;

                    case LScreens.ReportEntrada:
                    case LScreens.ReportSalida:
                    case LScreens.ReportInventario:
                        SetScreen(LScreens.ReportOperator);
                        break;

                    case LScreens.ReportBillofMaterial:
                        SetScreen(LScreens.ReportOperator);

                        break;

                    case LScreens.Wastes:
                        menu.RemoveItem(Resource.Id.menu_Cancel);
                        menu.RemoveItem(Resource.Id.menu_Add);
                        frmErrors.Visibility = ViewStates.Visible;
                        SetScreen(LScreens.Choose);
                        break;
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        public override async void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            try
            {
                base.OnConfigurationChanged(newConfig);
                _drawerToggle.OnConfigurationChanged(newConfig);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            try
            {
                var repoz = repo.GetRepositoryZ();

                var drawerOpen = _drawerLayout.IsDrawerOpen(_drawerList);

                this.menu = menu;

                ThreadPool.QueueUserWorkItem(async o => await LoadProceso());

                var prefs = PreferenceManager.GetDefaultSharedPreferences(this);

                var screen = (LScreens)prefs.GetInt(_ACTUALSCREEN, 2);

                var sett = repoz.GetSetting<Boolean>(Settings.Params.EquipmentSynced, false);

                if (sett && screen >= LScreens.Choose)
                {
                    ThreadPool.QueueUserWorkItem(o => SetScreen(screen));
                }
            }
            catch (Exception ex)
            {
                CatchException(ex);
            }
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            try
            {
                switch (item.ItemId)
                {
                    case Resource.Id.menu_Cancel:

                        OnBackPressed();

                        break;

                    case Resource.Id.menu_Add:

                        AsyncHelper.RunSync(() => PostWastes());

                        break;

                    case Resource.Id.menu_back:

                        OnBackPressed();

                        break;
                }

                if (_drawerToggle.OnOptionsItemSelected(item))
                    return true;
            }
            catch (Exception ex)
            {
                base.CatchException(ex);
            }

            return false;
        }

        private async Task<bool> IsFinalProcess()
        {
            var repotimes = repo.GetRepositoryTimes();
            var condicion = false;
            var tiempo = new Times();
            try
            {
                if (actualConfig != null)
                {
                    tiempo = AsyncHelper.RunSync<Times>(() => repotimes.GetAsyncByKey(actualConfig.TimeID));
                }
                if (tiempo != null)
                {
                    if (tiempo.Producto > 0)
                    {
                        condicion = (tiempo.Producto == Times.ProductTypes.Validar_Salida_y_Tipo_Almacenamiento || tiempo.Producto == Times.ProductTypes.Validar_Tipo_Almacenamiento);
                    }
                }
            }
            catch (Exception e)
            {
                await CatchException(e);
            }
            return condicion;
        }

        protected override async void OnDestroy()
        {
            try
            {
                base.OnDestroy();

                if (serviceConnection != null)
                {
                    UnbindService(serviceConnection);
                }
            }
            catch (Exception ex)
            {
                await base.CatchException(ex);
            }
        }

        private async Task LoadProceso()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                var Proceso = await repoz.GetProces();
                var repoe = repo.GetRepositorySettings();

                var screen = LScreens.NoConfig;

                try
                {
                    Proceso.ConfigActive = await repoz.GetSettingAsync<Boolean>(Settings.Params.ConfigIsActive, false);

                    if (Proceso.ConfigActive) screen = LScreens.Choose;

                    Proceso.EquipmentID = await repoz.GetSettingAsync<String>(Settings.Params.ConfigID, null);
                    Proceso.Equipment = await repoz.GetSettingAsync<String>(Settings.Params.ConfigName, null);
                    Proceso.IsLast = await repoz.GetSettingAsync<Boolean>(Settings.Params.IsLast, false);
                    Proceso.NeedEan = await repoz.GetSettingAsync<Boolean>(Settings.Params.NeedEan, false);
                    Proceso.NeedGramo = await repoz.GetSettingAsync<Boolean>(Settings.Params.NeedGramo, false);
                    Proceso.IsSubEquipment = await repoz.GetSettingAsync<Boolean>(Settings.Params.IsSubEquipment, false);
                    Proceso.SubEquipmentID = await repoz.GetSettingAsync<String>(Settings.Params.Second_Equipment, null);
                    Proceso.IsInputOutputControlActive = await repoz.GetSettingAsync<Boolean>(Settings.Params.IsInputOutputControlActive, true);
                    Proceso.IsPartialElaborateAuthorized = await repoz.GetSettingAsync<Boolean>(Settings.Params.IsPartialElaborateAuthorized, false);

                    repoz.SetProcess(Proceso);

                    var BatchID = await repoz.GetSettingAsync<String>(Settings.Params.BatchID, null);

                    RunOnUiThread(() =>
                    {
                        txtViewBatchID.Text = Util.MaskBatchID(BatchID);
                    });

                    if (!String.IsNullOrEmpty(Proceso.EquipmentID))
                    {
                        actualConfig = await repoz.GetActualConfig(Proceso.EquipmentID);
                        nextconfig = await repoz.GetNextConfig(Proceso.EquipmentID);

                        if (actualConfig != null)
                        {
                            screen = LScreens.Choose;

                            var lastsalida = await repoz.GetLastSalidaAsync();

                            if (lastsalida != null)
                            {
                                RunOnUiThread(async () =>
                                {
                                    if (String.IsNullOrEmpty(BatchID)) txtViewBatchID.Text = Util.MaskBatchID(lastsalida.BatchID);

                                    else
                                    {
                                        if (!BatchID.Equals(lastsalida.BatchID, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            //Se muestra en pantalla el batch id de la última salida.
                                            txtViewBatchID.Text = Util.MaskBatchID(lastsalida.BatchID);
                                            AssignBatchID(lastsalida.BatchID);
                                        }
                                    }

                                    if (!String.IsNullOrEmpty(lastsalida.PackID) && (await IsFinalProcess()))
                                    {
                                        linearLayoutEmpaque.Visibility = ViewStates.Visible;
                                    }
                                });
                            }
                        }
                        else if (nextconfig != null)
                        {
                            screen = LScreens.Active;
                        }
                    }
                }
                catch (Exception)
                { }

                var macaddress = await repoz.GetSettingAsync<String>(Settings.Params.PrinterAddress, null);

                if (!String.IsNullOrEmpty(macaddress))
                {
                    RunOnUiThread(() =>
                    {
                        imgPrinterStatus.Visibility = ViewStates.Visible;
                    });
                }

                SetScreen(screen);
                var printer = PrinterManager.GetUniqueInstance();
                imgPrinterStatus.Click += (obj, arg) =>
                {
                    if (printer.LastStatus == PrinterManager._PrinterStatus.Connected || printer.LastStatus == PrinterManager._PrinterStatus.Connecting || printer.LastStatus == PrinterManager._PrinterStatus.Configured) return;

                    var mensaje = String.Format(GetString(printer.LastStatus == PrinterManager._PrinterStatus.No_Paired ? Resource.String.PrinterMessageNoPairing : Resource.String.PrinterMessageConnectProblem), printer.PrinterName);

                    new CustomDialog(this, CustomDialog.Status.Warning, mensaje, CustomDialog.ButtonStyles.OneButton);
                };
                printer.ON_Get_Status += (status) =>
                {
                    RunOnUiThread(() =>
                    {
                        switch (status)
                        {
                            case PrinterManager._PrinterStatus.Connected:
                                imgPrinterStatus.SetImageResource(Resource.Drawable.ic_printer_working);

                                break;

                            case PrinterManager._PrinterStatus.Error:
                            case PrinterManager._PrinterStatus.No_Paired:
                                imgPrinterStatus.SetImageResource(Resource.Drawable.ic_printer_error);

                                break;

                            case PrinterManager._PrinterStatus.Connecting:
                                imgPrinterStatus.SetImageResource(Resource.Drawable.ic_bluetooth_connect);

                                break;
                        }
                    });
                };
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void InitControls()
        {
            linearLayoutCigarrosConsumidos = FindViewById<LinearLayout>(Resource.Id.linearLayoutCigarrosConsumidos); //Linea agregada
            txtContadorCigarrosConsumidos = FindViewById<TextView>(Resource.Id.txtContadorCigarrosConsumidos); //Linea agregada            
            frmErrors = FindViewById<ViewGroup>(Resource.Id.frmErrors);
            btnAceptarGeneral = FindViewById<Button>(Resource.Id.btnAceptarGeneral);
            txtMessages = frmErrors.FindViewById<TextView>(Resource.Id.txtMessages);
            flipper = FindViewById<ViewFlipper>(Resource.Id.flipper_view);
            lyBodyCommon = FindViewById<LinearLayout>(Resource.Id.lyBodyCommon);
            linearLayoutSecuencia = FindViewById<LinearLayout>(Resource.Id.linearLayoutSecuencia);
            txtViewNoEquipment = FindViewById<TextView>(Resource.Id.txtViewNoEquipment);
            txtViewNoConfig = FindViewById<TextView>(Resource.Id.txtViewNoConfig);
            txtViewProductoLarge = FindViewById<TextView>(Resource.Id.txtViewProductoLarge);
            txtViewProductoLargeBOM = FindViewById<TextView>(Resource.Id.txtViewProductoLargeBOM);
            txtViewProductoShort = FindViewById<TextView>(Resource.Id.txtViewProductoShort);
            txtViewProductoShortBOM = FindViewById<TextView>(Resource.Id.txtViewProductoShortBOM);
            txtViewFCode = FindViewById<TextView>(Resource.Id.txtViewFCode);
            txtViewCodeBOM = FindViewById<TextView>(Resource.Id.txtViewCodeBOM);
            txtViewMaterialBOM = FindViewById<TextView>(Resource.Id.txtViewMaterialBOM);
            txtViewSupCodeBOM = FindViewById<TextView>(Resource.Id.txtViewSupCodeBOM);
            txtViewUnidadBOM = FindViewById<TextView>(Resource.Id.txtViewUnidadBOM);
            txtViewMessagesActivation = FindViewById<TextView>(Resource.Id.txtViewMessagesActivation);
            editScan = FindViewById<EditText>(Resource.Id.editScan);
            editScanEquipment = FindViewById<EditText>(Resource.Id.editScanEquipment);
            btnEntrada = FindViewById<Button>(Resource.Id.btnEntrada);
            btnSalida = FindViewById<Button>(Resource.Id.btnSalida);
            editScanEntrada = FindViewById<EditText>(Resource.Id.editScanEntrada);
            txtCodeEntrada = FindViewById<TextView>(Resource.Id.txtCodeEntrada);
            txtViewDescripcionEntrada = FindViewById<TextView>(Resource.Id.txtViewDescripcionEntrada);
            txtViewCantidadEntrada = FindViewById<TextView>(Resource.Id.txtViewCantidadEntrada);
            txtViewBatch = FindViewById<TextView>(Resource.Id.txtViewBatch);
            txtViewBatchSap = FindViewById<TextView>(Resource.Id.txtViewBatchSap);
            btnCancelEntrada = FindViewById<Button>(Resource.Id.btnCancelEntrada);
            editScanSalida = FindViewById<EditText>(Resource.Id.editScanSalida);  
            txtViewBandeja = FindViewById<TextView>(Resource.Id.txtViewBandeja);
            txtViewCantidadSalida = FindViewById<TextView>(Resource.Id.txtViewCantidadSalida);
            btnCancelSalida = FindViewById<Button>(Resource.Id.btnCancelSalida);
            txtViewUnidadSalida = FindViewById<TextView>(Resource.Id.txtViewUnidadSalida);
            btnCancelActivate = FindViewById<Button>(Resource.Id.btnCancelActivate);
            listConfigs = FindViewById<ListView>(Resource.Id.listConfigs);
            btnDesperdicios = FindViewById<Button>(Resource.Id.btnDesperdicios);
            btnConsultas = FindViewById<Button>(Resource.Id.btnConsultas);
            lstProduccion = FindViewById<ListView>(Resource.Id.lstProduccion);
            lstConsumo = FindViewById<ListView>(Resource.Id.lstConsumo);
            listDesperdicios = FindViewById<ListView>(Resource.Id.listDesperdicios);
            lstBomReport = FindViewById<ListView>(Resource.Id.lstBomReport);
            gridView = FindViewById<GridView>(Resource.Id.gridView);
            txtViewSalidaLabel = FindViewById<TextView>(Resource.Id.txtViewSalidaLabel);
            txtViewBandejaLabel = FindViewById<TextView>(Resource.Id.txtViewBandejaLabel);
            txtViewWasteTitle = FindViewById<TextView>(Resource.Id.txtViewWasteTitle);
            ListStock = FindViewById<ExpandableListView>(Resource.Id.ListStock);
            viewFlipperStock = FindViewById<ViewFlipper>(Resource.Id.viewFlipperStock);
            txtViewBatchID = FindViewById<TextView>(Resource.Id.txtViewBatchID);
            txtViewPackageID = FindViewById<TextView>(Resource.Id.txtViewPackageID);
            txtViewSecuencia = FindViewById<TextView>(Resource.Id.txtViewSecuencia);
            linearLayoutEmpaque = FindViewById<LinearLayout>(Resource.Id.linearLayoutEmpaque);
            txtViewAlmacenamiento = FindViewById<TextView>(Resource.Id.txtViewAlmacenamiento);
            linearLayoutBandejasConsumidas = FindViewById<LinearLayout>(Resource.Id.linearLayoutBandejasConsumidas);
            txtContadorBandejasConsumidas = FindViewById<TextView>(Resource.Id.txtContadorBandejasConsumidas);
            txtViewSalidaTitle = FindViewById<TextView>(Resource.Id.txtViewSalidaTitle);
            btnAceptarGeneral.Click += btnAceptarGeneral_Click;
            btnCancelActivate.Click += btnCancelActivate_Click;
            editScanEntrada.KeyPress += editScanEntrada_KeyPress;
            editScan.KeyPress += editScan_KeyPress;
            editScanEquipment.KeyPress += editScanEquipment_KeyPress;
            btnEntrada.Click += btnEntrada_Click;
            btnSalida.Click += btnSalida_Click;
            btnConsultas.Click += btnConsultas_Click;
            btnDesperdicios.Click += btnDesperdicios_Click;
            btnCancelEntrada.Click += btnCancelEntrada_Click;
            btnCancelSalida.Click += btnCancelSalida_Click;
            editScanSalida.LongClickable = false;
            if (await LockTyping())
            {
                editScanSalida.Touch += EditScanSalida_Touch;
            }
            editScanSalida.KeyPress += editScanSalida_KeyPress;
            gridView.ItemClick += gridView_ItemClick;
        }

        private async void EditScanSalida_Touch(object sender, View.TouchEventArgs e)
        {
            try
            {
                RunOnUiThread(() =>
                {
                    var hideAndShowKeyboard = new KeyboardManager(this);
                    //Ocultamiento de teclado en el campo de salida de material
                    hideAndShowKeyboard.HideSoftKeyboard(this);
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void gridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();

                var pos = _reportEntries.ElementAt(e.Position);
                var repor = repo.GetRepositoryR();

                switch (pos.Type)
                {
                    case ReportEntry.EntryTypes.Entrada:

                        SetScreen(LScreens.ReportEntrada);
                        var consumo = await repor.GetConsumptions(Caching.Stock.TurnID, Caching.GetProductionDate());
                        lstConsumo.Adapter = new ReportAdapterConsumo(this, consumo, Caching.GetProductionDate());

                        break;

                    case ReportEntry.EntryTypes.Salida:

                        SetScreen(LScreens.ReportSalida);
                        var produccion = await repor.GetProduccion(Caching.Stock.TurnID, Caching.GetProductionDate());

                        var rproduction = new ReportAdapterProduccion(this, produccion, Proceso, Caching.TurnoID, Caching.GetProductionDate());
                        lstProduccion.Adapter = rproduction;

                        lstProduccion.ItemClick += async (s, args) =>
                        {
                            //Debido a que los datos comienzan a partir de la tercera fila, de tal forma que el evento click sea de una fila que contenga datos. 
                            if (args.Position > 2)
                            {
                                if (Proceso.IsLast)
                                {
                                    //Se valida que la lista de salida de productos tenga elementos                      
                                    if (produccion.Any())
                                    {
                                        Dialog lotdialog = null;
                                        var lotbuilder = new AlertDialog.Builder(this);
                                        lotbuilder.SetTitle(Resource.String.OutEntryPrintTitle);
                                        lotbuilder.SetIcon(Android.Resource.Drawable.IcMenuAgenda);
                                        var view = LayoutInflater.Inflate(Resource.Layout.dialog_lots, null);
                                        var lstlots = view.FindViewById<ListView>(Resource.Id.lstlots);
                                        var detalle = await repor.GetProduccionDetail(Caching.TurnoID, produccion.ElementAt(args.Position - 3).Fecha);
                                        var adapter = new ReportAdapterProduccionDetails(this, detalle);
                                        adapter.OnPrint += async (etiqueta) =>
                                        {
                                            var lista = new List<Etiquetas>();
                                            lista.Add(etiqueta);
                                            var printer = PrinterManager.GetUniqueInstance();
                                            await printer.ExecutePrint(this, lista);
                                            lotdialog.Dismiss();
                                            lotdialog.Dispose();
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

                                var ValidationProductType = actualConfig.Producto;

                                if (ValidationProductType == Times.ProductTypes.Validar_Salida_y_Tipo_Almacenamiento || ValidationProductType == Times.ProductTypes.Validar_Tipo_Almacenamiento)
                                {
                                    if (!Util.TryLock(this, Util.Locks.Reimprimir)) return;

                                    var sec_Manager = new SecurityManager(this);
                                    sec_Manager.Response += async (arg, arg1) =>
                                    {
                                        if (arg)
                                        {
                                            Dialog lotdialog = null;
                                            var lotbuilder = new AlertDialog.Builder(this);
                                            lotbuilder.SetTitle(Resource.String.OutEntryPrintTitle);
                                            lotbuilder.SetIcon(Android.Resource.Drawable.IcMenuAgenda);
                                            lotbuilder.SetCancelable(false);
                                            var view = LayoutInflater.Inflate(Resource.Layout.dialog_lots, null);
                                            var lstlots = view.FindViewById<ListView>(Resource.Id.lstlots);
                                            var elaborate = new Shared.Models.R.ProductionReport();
                                            //Indice del elemento seleccionado debe ser mayor que 2 debido a que las primeras 3 filas contienen los nombres de las columnas del reporte. 
                                            if (args.Position > 2)
                                            {
                                                var position = args.Position - 3;
                                                elaborate = produccion[position];
                                            }
                                            var detalle = await repor.GetProduccionDetailEmpaque(actualConfig.EquipmentID, Caching.TurnoID, elaborate.Fecha, elaborate.ProductCode);
                                            var adapter = new EmpaqueAdapter(this, detalle);
                                            adapter.OnPrint += (salida, traza) =>
                                            {
                                                var dialogPrintConfirmation = new PrintConfirmationDialog(this, repo, salida, traza);
                                                dialogPrintConfirmation.OnPrintLabel += async (Byte cantidadImpresiones, Byte idMotivoReimpresion) =>
                                                {
                                                    var repoz = repo.GetRepositoryZ();
                                                    var secuenciaEmpaque = salida.PackSequence == 0 ? traza.SecuenciaEmpaque : salida.PackSequence;
                                                    var cantidadReimpresionesEtiqueta = await repoz.GetCantidadReimpresionesEtiquetaAsync(
                                                        salida._Produccion, salida.TurnID, salida.PackID, secuenciaEmpaque);

                                                    if (cantidadReimpresionesEtiqueta < 1)
                                                    {
                                                        Util.ReleaseLock(this, Util.Locks.Reimprimir);
                                                        PrintEtiquetaSuplidor(this, salida, traza, cantidadImpresiones, true, idMotivoReimpresion);
                                                    }
                                                    else
                                                    {
                                                        var mensajeError = "";
                                                        if (cantidadReimpresionesEtiqueta == 1)
                                                        {
                                                            mensajeError = "Esta etiqueta se ha reimpreso {0} vez.";
                                                        }
                                                        else
                                                        {
                                                            mensajeError = "Esta etiqueta se ha reimpreso {0} veces.";
                                                        }
                                                        var errorDialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(mensajeError, cantidadReimpresionesEtiqueta),
                                                            CustomDialog.ButtonStyles.TwoButtonWithAutorizeReprint);
                                                        errorDialog.OnAcceptPress += (Boolean isCantidad, Single Box, Single Cantidad) =>
                                                        {
                                                            var sec_Manager1 = new SecurityManager(this);
                                                            sec_Manager1.Response += (arg3, arg4) =>
                                                            {
                                                                if (arg3)
                                                                {
                                                                    Util.ReleaseLock(this, Util.Locks.Reimprimir);
                                                                    PrintEtiquetaSuplidor(this, salida, traza, cantidadImpresiones, true, idMotivoReimpresion);
                                                                }
                                                            };

                                                            sec_Manager1.HaveAccess(RolsPermits.Permits.REIMPRIMIR_REIMPRESION_ETIQUETAS);

                                                        };

                                                        errorDialog.OnCancelPress += async () =>
                                                        {
                                                            try
                                                            {
                                                                ClearEntrada();
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                await Util.SaveException(ex);
                                                            }
                                                        };

                                                        return;
                                                    }
                                                };
                                                dialogPrintConfirmation.ShowDialog();

                                            };

                                            lstlots.Adapter = adapter;
                                            lotbuilder.SetView(view);
                                            lotbuilder.SetNegativeButton(Resource.String.Cancel, (se, t) =>
                                            {
                                                Util.ReleaseLock(this, Util.Locks.Reimprimir);
                                                lotdialog.Dismiss();
                                                lotdialog.Dispose();
                                            });

                                            lotdialog = lotbuilder.Create();

                                            lotdialog.Show();
                                        }
                                    };

                                    sec_Manager.HaveAccess(RolsPermits.Permits.REIMPRIMIR_ETIQUETAS);
                                }
                            }
                        };

                        break;

                    case ReportEntry.EntryTypes.Inventario:

                        SetScreen(LScreens.ReportInventario);

                        var transaction = await repor.GetReportStock(Caching.Stock.TurnID, Caching.GetProductionDate());

                        if (transaction.Any())
                        {
                            viewFlipperStock.DisplayedChild = 0;
                            var ladapter = new StockAdapterExpandable(this, transaction);
                            ListStock.SetAdapter(ladapter);
                        }
                        else
                            viewFlipperStock.DisplayedChild = 1;

                        break;

                    case ReportEntry.EntryTypes.InventarioResumen:

                        var inventaries = await repo.GetRepositoryR().GetStockResumeList(Caching.GetProductionDate(), Caching.Stock.TurnID, GetString(Resource.String.ReceiptConceptReceive), GetString(Resource.String.ReceiptConsumption), GetString(Resource.String.ReceiptReturn), GetString(Resource.String.ReceiptRetiro), GetString(Resource.String.ReceiptAjust));

                        SetScreen(LScreens.ReportEntrada);

                        if (inventaries.Any())
                        {
                            viewFlipperStock.DisplayedChild = 0;
                            var radapter = new StockResumenAdapter(this, inventaries, Caching.Stock.TurnID, Caching.GetProductionDate());
                            lstConsumo.Adapter = radapter;
                        }
                        else
                            viewFlipperStock.DisplayedChild = 1;

                        break;

                    case ReportEntry.EntryTypes.ReportedeBom:
                        SetScreen(LScreens.ReportBillofMaterial);

                        var reporteBom = await repo.GetRepositoryZ().GetBomMaterial(actualConfig.ProductCode, actualConfig.VerID);

                        txtViewProductoShortBOM.Text = (actualConfig.ProductShort ?? actualConfig.ProductName).Trim();
                        txtViewProductoLargeBOM.Text = actualConfig._ProductName2.Trim();
                        txtViewFCode.Text = actualConfig.ProductReference;

                        txtViewCodeBOM.Text = GetString(Resource.String.EntryMaterialCodeHit);
                        txtViewCodeBOM.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        txtViewCodeBOM.SetTextColor(Android.Graphics.Color.Black);

                        txtViewMaterialBOM.Text = GetString(Resource.String.ReportTitleMaterial);
                        txtViewMaterialBOM.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        txtViewMaterialBOM.SetTextColor(Android.Graphics.Color.Black);

                        txtViewUnidadBOM.Text = GetString(Resource.String.ReportTitleUnidad);
                        txtViewUnidadBOM.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        txtViewUnidadBOM.SetTextColor(Android.Graphics.Color.Black);

                        txtViewSupCodeBOM.Text = GetString(Resource.String.ReportTitleCodigoSupl);
                        txtViewSupCodeBOM.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        txtViewSupCodeBOM.SetTextColor(Android.Graphics.Color.Black);

                        if (reporteBom.Any())
                        {
                            viewFlipperStock.DisplayedChild = 0;
                            var radapter = new ReportBomAdapter(this, reporteBom);
                            lstBomReport.Adapter = radapter;
                        }
                        else
                            viewFlipperStock.DisplayedChild = 1;

                        break;
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {

            }
        }

        private void DialogPrintConfirmation_OnPrintLabel(DateTime printedDate, string packId, int packSequence)
        {
            throw new NotImplementedException();
        }

        private async void SaveLabelPrintingLog(Elaborates salida, ProductsRoutes traza, int Cantidad, int IdMotivoReimpresion, String usuarioReimpresion)
        {
            try
            {
                var repoLabelPrintingLogs = repo.GetRepositoryLabelPrintingLogs();

                var labelPrintingLog = new LabelPrintingLogs();
                labelPrintingLog.EquipmentID = salida.SubEquipmentID;
                labelPrintingLog.PackSequence = salida.PackSequence == 0 ? traza.SecuenciaEmpaque : salida.PackSequence;
                labelPrintingLog.LabelPrintingReasonID = IdMotivoReimpresion;
                labelPrintingLog.PackID = salida.PackID;
                labelPrintingLog.Identifier = salida.Identifier;
                labelPrintingLog.ProductionDate = salida._Produccion;
                labelPrintingLog.TurnID = salida.TurnID;
                labelPrintingLog.User = usuarioReimpresion;
                labelPrintingLog.Quantity = Cantidad;
                labelPrintingLog.LabelReprintedDate = DateTime.Now;
                labelPrintingLog.SyncSQL = true;

                await repoLabelPrintingLogs.InsertAsync(labelPrintingLog);
            }
            catch (Exception e)
            {
                await Util.SaveException(e, "Guardado de registro de reimpresión de etiquetas");
            }
        }

        private async void btnAceptarGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                var minutosTurno = (int)Caching.GetNextChangeDate().Subtract(DateTime.Now).TotalMinutes;

                var arregloTurno = Resources.GetStringArray(Resource.Array.MinutosTurno);

                if ((minutosTurno <= 0 || arregloTurno.Any(p => p == minutosTurno.ToString())) && Caching.Stock.Status == Stocks._Status.Abierto)
                {
                    var Proceso = await repo.GetRepositoryZ().GetProces();

                    var cuadreCloseDialog = new CuadreDialog(this, CuadreDialog._Accions.CLOSED, Caching.GetProductionDate(), Caching.Stock.TurnID, actualConfig, Caching.Stock, Proceso, true);
                    cuadreCloseDialog.OnAcceptPress += cuadreCloseDialog_OnAcceptPress;
                    if (cuadreCloseDialog.dialog != null)
                    {
                        UnBindEventos();
                    }
                    cuadreCloseDialog.OnCancelPress += () =>
                    {
                        Util.ReleaseLock(this, Util.Locks.Turnos, Util.Locks.CallBackSync);
                        BindEventos();
                    };
                }
                else
                    SetFooter(false);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void PassDialog_OnAcceptPress(ProcessList Process)
        {
            try
            {
                SetFooter(false);
                var repoz = repo.GetRepositoryZ();
                repoz.SetProcess(Process);
                txtViewTurn.Text = String.Format("{0} {1}", GetString(Resource.String.Turn), Caching.TurnoID);
                txtViewLogon.Text = Process.UserName;
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void InitDrawerLayout()
        {
            try
            {
                _drawerList = FindViewById<ListView>(Resource.Id.drawer_list);
                _drawerList.ItemClick += _drawerList_ItemClick;
                _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                _drawerLayout.SetDrawerShadow(Resource.Drawable.drawer_shadow, (int)GravityFlags.Start);

                _drawerEntries = await GetDrawerEntries();
                var drawerAdapter = new DrawerAdapter(this, _drawerEntries);
                _drawerList.Adapter = drawerAdapter;

                _drawerToggle = new MyActionBarDrawerToggle(this, _drawerLayout, Resource.Drawable.ic_drawer, Resource.String.open_mode, Resource.String.closed_mode);

                _drawerToggle.DrawerClosed += delegate
                {
                    // InvalidateOptionsMenu();
                };

                _drawerToggle.DrawerOpened += delegate
                {
                    // InvalidateOptionsMenu();
                };

                _drawerLayout.SetDrawerListener(_drawerToggle);

                _drawerToggle.SyncState();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void _drawerList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();

                var Entry = _drawerEntries.ElementAt(e.Position);

                if (Option != CuadreDialog._Options.NONE && Entry.Permit != RolsPermits.Permits.NONE)
                {
                    new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.CloseProcess));
                    return;
                }

                if (Entry.Permit == RolsPermits.Permits.NONE)
                {
                    Finish();
                    StartActivity(typeof(LoginActivity));
                }
                //else if (Entry.Permit == RolsPermits.Permits.ACTUALIZAR_BANDEJAS)
                //{
                //    try
                //    {
                //        var repoBandejas = repo.GetRepositoryTraysProducts();
                //        ShowProgress(true, Resource.String.MessageUpdateBandejas);
                //        await repoBandejas.SyncAsync();
                //    }
                //    catch (Exception ex)
                //    {
                //        CathException(ex);
                //    }
                //    finally
                //    {
                //        ShowProgress(false);
                //    }
                //}
                else if (Entry.Permit == RolsPermits.Permits.CHANGE_PASS)
                {
                    new ChangePassword(this, true, Proceso.Logon);
                }
                else if (Entry.Permit == RolsPermits.Permits.QUALITY_SCREEN)
                {
                    if (String.IsNullOrEmpty(Proceso.EquipmentID))
                    {
                        new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.DrawerChangeErrorMessage));
                        return;
                    }

                    var intent = new Intent(this, Entry.activity);
                    intent.PutExtra(CustExtras.ProductCode.ToString(), actualConfig._ProductCode);
                    intent.PutExtra(CustExtras.TurnID.ToString(), (Int32)Caching.Stock.TurnID);
                    StartActivity(intent);
                }
                else if (Entry.Permit == RolsPermits.Permits.DEVOLUCION)
                {
                    if (String.IsNullOrEmpty(Proceso.EquipmentID))
                    {
                        new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.DrawerChangeErrorMessage));
                        return;
                    }

                    Security.HaveAccess(Entry);
                }
                else if (Entry.Permit == RolsPermits.Permits.GENERAL_REPORTS)
                {
                    var intent = new Intent(this, Entry.activity);
                    StartActivity(intent);
                }
                else if (Entry.Permit == RolsPermits.Permits.DEVOLUCION_PRODUCTO)
                {
                    var intent = new Intent(this, Entry.activity);

                    if (Caching.Stock != null)
                    {
                        intent.PutExtra(CustExtras.TurnID.ToString(), Caching.Stock.TurnID.ToString());
                        intent.PutExtra(CustExtras.ProductionDate.ToString(), Caching.GetProductionDate().ToString("yyyyMMdd"));
                    }

                    StartActivityForResult(intent, (int)Entry.Permit);
                }
                else
                    Security.HaveAccess(Entry);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            try
            {
                base.OnActivityResult(requestCode, resultCode, data);

                var Proceso = repo.GetRepositoryZ().GetProcesSync();

                SecurityManager.SetProcess(Proceso);

                switch ((RolsPermits.Permits)requestCode)
                {
                    case RolsPermits.Permits.CONFIGURACION:

                        var sett = repo.GetRepositoryZ().GetSetting<Boolean>(Settings.Params.EquipmentSynced, false);

                        if (resultCode == Result.Ok && !sett)
                        {
                            var macaddress = repo.GetRepositoryZ().GetSetting<String>(Settings.Params.PrinterAddress, null);

                            if (!String.IsNullOrEmpty(macaddress))
                            {
                                imgPrinterStatus.Visibility = ViewStates.Visible;
                            }

                            ThreadPool.QueueUserWorkItem(o=>
                            {
                                DownloadSequences();
                                RunOnUiThread(() =>
                                {
                                    InitDrawerLayout();
                                });
                            });
                        }

                        break;

                    case RolsPermits.Permits.PLANIFICACION:

                        AsyncHelper.RunSync(() => LoadProceso());

                        break;

                    case RolsPermits.Permits.DEVOLUCION:
                    case RolsPermits.Permits.ENTREGA_MATERIALES:

                        SequenceManager.ReloadSequences();

                        break;
                }

                BindService();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void DownloadSequences()
        {
            try
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();

                ShowProgress(true);

                var repoStock = repo.GetRepositoryStocks();
                var repoEntrada = repo.GetRepositoryConsumptions();
                var repoSalida = repo.GetRepositoryElaborates();
                var repoWaste = repo.GetRepositoryWastes();
                var repoWasteMaterial = repo.GetRepositoryWasteMaterials();
                var repoInv = repo.GetRepositoryInventories();
                var repoTran = repo.GetRepositoryTransactions();
                var repoSecuences = repo.GetRepositoryCustomSecuences();

                await repoStock.SyncAsyncAll(false);
                await repoSalida.SyncAsyncAll(false);
                await repoEntrada.SyncAsyncAll(false);
                await repoWaste.SyncAsyncAll(false);
                await repoWasteMaterial.SyncAsyncAll(false);
                await repoInv.SyncAsyncAll(false);
                await repoTran.SyncAsyncAll(false);

                await Caching.ReloadStock();
                SequenceManager.ReloadSequences();

                var repoSetti = repo.GetRepositorySettings();
                await repoSetti.InsertOrReplaceAsync(new Settings()
                {
                    Key = Settings.Params.EquipmentSynced,
                    Value = "True"
                });

                await LoadProceso();

                ShowProgress(false);

                if (actualConfig == null)
                {
                    await Task.Delay(5000);
                    actualConfig = await repo.GetRepositoryZ().GetActualConfig(Proceso.EquipmentID);
                }

                if (actualConfig != null)
                    Routes.Reload(actualConfig.EquipmentID);

                BindEventos();
            }
            catch (WebException ex)
            {
                await Util.SaveException(ex);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {
            }
        }

        private async Task<List<DrawerEntry>> GetDrawerEntries()
        {
            var Proceso = await repo.GetRepositoryZ().GetProces();
            var time = new Times();
            actualConfig = await repo.GetRepositoryZ().GetActualConfig(Proceso.EquipmentID);

            if (actualConfig != null)
            {
                time = await repo.GetRepositoryTimes().GetAsyncByKey(actualConfig.TimeID);
            }
                
            var retorno = new List<DrawerEntry>();
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuDirections, GetString(Resource.String.DrawerOperacions), null, RolsPermits.Permits.OPERACION));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcPopupSync, GetString(Resource.String.DrawerPlan), typeof(EquipmentActivity), RolsPermits.Permits.PLANIFICACION));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuToday, GetString(Resource.String.DrawerChange), null, RolsPermits.Permits.ASIGNACION));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMediaPlay, GetString(Resource.String.DrawerReceipt), typeof(ReceiptActivity), RolsPermits.Permits.ENTREGA_MATERIALES));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuUpload, GetString(Resource.String.DrawerReturn), typeof(ReturnActivity), RolsPermits.Permits.DEVOLUCION));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuAgenda, GetString(Resource.String.DrawerReport), typeof(ReportVarillaActivity), RolsPermits.Permits.REPORTAR_VARILLAS));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMediaPrevious, GetString(Resource.String.DrawerReturnProduct), typeof(ReturnProductActivity), RolsPermits.Permits.DEVOLUCION_PRODUCTO));

            if (time.Producto != ProductTypes.None && ! await IsFinalProcess())
            {
                //Mostrar opción de salida parcial si no es un proceso final Y si no es el proceso primario, por ej: maker.
                retorno.Add(new DrawerEntry(Proceso.IsPartialElaborateAuthorized ? Android.Resource.Drawable.RadiobuttonOnBackground : Android.Resource.Drawable.RadiobuttonOffBackground
                    , GetString(Resource.String.PartialElaborate), null, RolsPermits.Permits.AUTORIZAR_SALIDA_PARCIAL));
            }
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuSend, GetString(Resource.String.DrawerClose), typeof(CloseActivity), RolsPermits.Permits.CIERRES));
            //retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuWeek, GetString(Resource.String.DrawerTrayUpdate), null, RolsPermits.Permits.ACTUALIZAR_BANDEJAS));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuSetAs, GetString(Resource.String.DrawerRelease), typeof(ReleaseActivity), RolsPermits.Permits.LIBERACION));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMediaNext, GetString(Resource.String.DrawerQuality), typeof(QualityActivity), RolsPermits.Permits.QUALITY_SCREEN));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuMapmode, GetString(Resource.String.DrawerReports), typeof(ReportActivity), RolsPermits.Permits.GENERAL_REPORTS));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuView, GetString(Resource.String.dialogpassTitle), null, RolsPermits.Permits.CHANGE_PASS));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuPreferences, GetString(Resource.String.DrawerConfig), typeof(ConfigActivity), RolsPermits.Permits.CONFIGURACION));
            retorno.Add(new DrawerEntry(Android.Resource.Drawable.IcMenuRevert, GetString(Resource.String.DrawerQuit), typeof(LoginActivity), RolsPermits.Permits.NONE));

            return retorno;
        }

        private async Task SetTurn()
        {
            try
            {
                RunOnUiThread(() =>
                {
                    if (txtViewTurn == null)
                        InitControlsHeader();

                    if (Caching.Stock == null)
                    {
                        txtViewTurn.Text = String.Format("{0} {1}", GetString(Resource.String.Turn), Caching.TurnoID);
                        txtViewTurn.Tag = new Java.Lang.Integer(Caching.TurnoID);
                    }
                    else
                    {
                        txtViewTurn.Text = String.Format("{0} {1}", GetString(Resource.String.Turn), Caching.Stock.TurnID);
                        txtViewTurn.Tag = new Java.Lang.Integer(Caching.Stock.TurnID);
                    }

                    txtViewFecha.Text = Caching.GetProductionDate().ToString("dd MMMM yyyy");
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void SetTraysCounter()
        {
            try
            {
                RunOnUiThread(async () =>
                {
                    var Proceso = await repo.GetRepositoryZ().GetProces();
                    if (txtContadorBandejasConsumidas != null)
                    {
                        var contadorBandejasConsumidas = await repo.GetRepositoryZ()
                        .GetCantidadBandejasConsumidasAsync(Proceso.IsSubEquipment ? Proceso.SubEquipmentID : Proceso.EquipmentID
                        , Caching.GetProductionDate(), Caching.Stock != null ? Caching.Stock.TurnID : Caching.TurnoID);
                        txtContadorBandejasConsumidas.Text = contadorBandejasConsumidas.ToString();
                    }
                });
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex, "Actualizando valor de contador de bandejas");
                throw;
            }
        }

        private async void SetScreen(LScreens screen)
        {
            try
            {
                RunOnUiThread(async () =>
                {
                    var Proceso = await repo.GetRepositoryZ().GetProces();

                    LScreen = screen;

                    flipper.DisplayedChild = (Int32)LScreen;
                    linearLayoutSecuencia.Visibility = ViewStates.Gone;
                    linearLayoutBandejasConsumidas.Visibility = ViewStates.Gone;

                    if (Convert.ToInt32(txtViewEquipment.Tag) == 0) SetHeader();

                    //if (!String.IsNullOrEmpty(Proceso.SubEquipmentID) && Convert.ToInt32(txtViewEquipment.Tag) < 2)
                    //{
                    //    txtViewEquipment.Text = String.Forma6t("{0} ({1})", Proceso.Equipment, !Proceso.IsSubEquipment ? Proceso.SubEquipmentID : Proceso.EquipmentID);
                    //    txtViewEquipment.Tag = 2;
                    //}

                    var numero = txtViewTurn.Tag as Java.Lang.Integer;
                    if (Caching != null)
                    {
                        if (numero != null)
                        {
                            if (numero.IntValue() != Caching.TurnoID)
                            {
                                await SetTurn();
                            }
                        }
                    }

                    if (menu != null) menu.Clear();

                    switch (LScreen)
                    {
                        case LScreens.NoConfig:
                            LoadNoConfig();
                            ShowBodyCommon(false);
                            break;

                        case LScreens.Active:
                            LoadNextConfig();
                            ShowBodyCommon(false);
                            break;

                        case LScreens.Choose:
                            LoadActualConfig();
                            ShowBodyCommon(true);
                            break;

                        case LScreens.ScanInput:
                            ClearEntrada();
                            ShowBodyCommon(true);

                            if (Proceso.Process == "2302" || Proceso.Process == "2304" || (Proceso.Process == "2301" && actualConfig.TimeID == "01"))
                            {
                                //Si el proceso configurado es Pipe Tobacco, PCR o Producción tips cigarros de Maker, se oculta el layout de bandejas consumidas. 
                                linearLayoutCigarrosConsumidos.Visibility = ViewStates.Gone;
                                linearLayoutBandejasConsumidas.Visibility = ViewStates.Gone;
                            }
                            else
                            {
                                linearLayoutBandejasConsumidas.Visibility = ViewStates.Visible;
                                linearLayoutCigarrosConsumidos.Visibility = ViewStates.Gone;

                                var repoZ = repo.GetRepositoryZ();

                                var cantidadBandejasConsumidasPorTurno = 0.0;
                                if (Proceso.IsSubEquipment)
                                {
                                    try
                                    {
                                        cantidadBandejasConsumidasPorTurno =
                                        await repoZ.GetOnlineCountTraysConsumptions
                                        (Proceso.SubEquipmentID,
                                        Caching.GetProductionDate(), Caching.Stock != null ? Caching.Stock.TurnID : Caching.TurnoID);
                                    }
                                    catch (Exception e)
                                    {
                                        await Util.SaveException(e);
                                    }
                                }
                                else
                                {
                                    cantidadBandejasConsumidasPorTurno =
                                    await repoZ.GetCantidadBandejasConsumidasAsync
                                    (Proceso.EquipmentID,
                                    Caching.GetProductionDate(), Caching.Stock != null ? Caching.Stock.TurnID : Caching.TurnoID);
                                }
                                txtContadorBandejasConsumidas.Text = cantidadBandejasConsumidasPorTurno.ToString();
                            }

                            break;

                        case LScreens.ScanOutput:

                            if (Proceso.IsLast)
                            {
                                var repoz = repo.GetRepositoryZ();

                                var bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID);

                                txtViewSalidaLabel.Text = GetString(Resource.String.OutBarCode2);
                                txtViewBandejaLabel.Text = GetString(Resource.String.OutBarCodeValue2);
                                editScanSalida.Hint = String.Format(GetString(Resource.String.OutBarCodeHit), bandeja.Unit);
                                editScanSalida.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
                            }
                            else
                            {

                                linearLayoutCigarrosConsumidos.Visibility = ViewStates.Visible;

                                if (Proceso.IsPartialElaborateAuthorized)
                                {
                                    txtViewSalidaTitle.Text = GetString(Resource.String.OutPartialElaborate);
                                }
                                else
                                {
                                    txtViewSalidaTitle.Text = GetString(Resource.String.Out);
                                }

                                //CODIGO AGREGADO POR RALDY PARA MOSTRAR CANTIDAD DE CIGARROS CONSUMIDOS
                                //Ultima revision 01-feb-2023
                                var repoZ = repo.GetRepositoryZ();

                                var cantidadCigarrosConsumidosPorTurno = 0.0;                                
                                if (Proceso.IsSubEquipment)
                                {
                                    linearLayoutCigarrosConsumidos.Visibility = ViewStates.Gone;
                                    try
                                    {
                                        //query a realizar si es un subequipo
                                        cantidadCigarrosConsumidosPorTurno = await repoZ.GetConsumptionsSumByMaterialCode(Proceso.EquipmentID, Caching.ProductCode, Caching.GetProductionDate(), Caching.Stock != null ? Caching.Stock.TurnID : Caching.TurnoID);
                                    }
                                    catch (Exception e)
                                    {
                                        await Util.SaveException(e);
                                    }
                                }
                                else
                                {
                                    try
                                    {    
                                        
                                        cantidadCigarrosConsumidosPorTurno = await repoZ.GetCantidadCigarrosConsumidosAsync(Proceso.EquipmentID, Caching.GetProductionDate(), Caching.Stock != null ? Caching.Stock.TurnID : Caching.TurnoID);
                                        //double invfinal = await ValidarInventarioFinalTurno(Proceso.EquipmentID, Caching.GetProductionDate(), Caching.Stock != null ? Caching.Stock.TurnID : Caching.TurnoID);
                                    }
                                    catch (Exception e)
                                    {
                                        await Util.SaveException(e);
                                    }
                                }                                
                                txtContadorCigarrosConsumidos.Text = cantidadCigarrosConsumidosPorTurno.ToString("N3");                                
                            }

                            ClearSalida();
                            ShowBodyCommon(true);

                            break;

                        case LScreens.ReportOperator:
                        case LScreens.ReportInventario:
                        case LScreens.ReportSalida:
                        case LScreens.ReportEntrada:

                            frmErrors.Visibility = ViewStates.Gone;
                            MenuInflater.Inflate(Resource.Menu.menu_back, menu);

                            ShowBodyCommon(false);

                            break;

                        case LScreens.Wastes:

                            txtViewWasteTitle.Text = String.Format(GetString(Resource.String.WastesTitle), actualConfig.ProductShort ?? actualConfig._ProductCode);
                            frmErrors.Visibility = ViewStates.Gone;
                            MenuInflater.Inflate(Resource.Menu.wate_menu, menu);

                            LoadWastes();
                            ShowBodyCommon(false);

                            break;

                        case LScreens.ReportBillofMaterial:
                            frmErrors.Visibility = ViewStates.Gone;
                            MenuInflater.Inflate(Resource.Menu.menu_back, menu);

                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }
        private async void LoadNextConfig()
        {
            try
            {
                RunOnUiThread(async () =>
                {
                    var Proceso = await repo.GetRepositoryZ().GetProces();

                    var repoz = repo.GetRepositoryZ();

                    editScanEquipment.Text = String.Empty;
                    editScan.Text = String.Empty;
                    editScan.Enabled = true;
                    editScan.RequestFocus();

                    if (!String.IsNullOrEmpty(Proceso.EquipmentID))
                    {
                        if (nextconfig == null)
                            nextconfig = await repoz.GetNextConfig(Proceso.EquipmentID);

                        await Caching.LoadNextConfigs(Proceso.EquipmentID);

                        var adapter = new Adapters.ConfigListAdapter(this, repo, Caching.Configs, nextconfig);
                        adapter.OnRefresh += () =>
                        {
                            LoadActualConfig(true);
                        };

                        listConfigs.Adapter = adapter;

                        if (actualConfig != null)
                        {
                            btnCancelActivate.Visibility = ViewStates.Visible;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {

            }
        }

        private async void LoadNoConfig()
        {
            try
            {
                RunOnUiThread(async () =>
                {
                    var Proceso = await repo.GetRepositoryZ().GetProces();

                    if (Proceso == null)
                    {
                        txtViewNoConfig.Visibility = ViewStates.Visible;
                        txtViewNoEquipment.Visibility = ViewStates.Visible;
                        return;
                    }

                    if (!String.IsNullOrEmpty(Proceso.EquipmentID))
                    {
                        txtViewNoEquipment.Visibility = ViewStates.Gone;
                        txtViewNoConfig.Visibility = ViewStates.Visible;
                    }

                    if (nextconfig != null)
                    {
                        txtViewNoEquipment.Visibility = ViewStates.Gone;
                        txtViewNoConfig.Visibility = ViewStates.Visible;
                    }
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void LoadActualConfig(Boolean Clean = false)
        {
            try
            {
                var repotime = repo.GetRepositoryTimes();
                var Proceso = await repo.GetRepositoryZ().GetProces();

                var repoz = repo.GetRepositoryZ();
                if (actualConfig == null || Clean)
                {
                    actualConfig = await repoz.GetActualConfig(Proceso.EquipmentID);
                    Caching.Clean();
                }

                if (actualConfig != null)
                {
                    var time = await repotime.GetAsyncByKey(actualConfig.TimeID);

                    if (!Caching.IsInitialized)
                    {
                        Caching.SetProduct(actualConfig.ProductCode, actualConfig.VerID);
                    }


                    var repoProductoTipoAlmacenamientos = repo.GetRepositoryProductoTipoAlmacenamientos();
                    var repoTipoAlmacenamientoProducto = repo.GetRepositoryTipoAlmacenamientoProductos();

                    var productoTipoAlmacenamiento = new ProductoTipoAlmacenamiento();
                    var tipoAlmacenamientoProducto = new TipoAlmacenamientoProducto();


                    productoTipoAlmacenamiento = await repoz.GetAlmFillerByIdentifier(actualConfig);

                    if (productoTipoAlmacenamiento != null)
                    {
                        tipoAlmacenamientoProducto = await repoTipoAlmacenamientoProducto.GetAsyncByKey(productoTipoAlmacenamiento.idTipoAlmacenamientoProducto);
                    }


                    RunOnUiThread(() =>
                    {
                        txtViewProductoLarge.Text = actualConfig._ProductName2.Trim();
                        txtViewProductoShort.Text = (actualConfig.ProductShort ?? actualConfig.ProductName).Trim();

                        if (time.Producto == Times.ProductTypes.Validar_Salida_y_Tipo_Almacenamiento || time.Producto == Times.ProductTypes.Validar_Tipo_Almacenamiento)
                        {
                            txtViewAlmacenamiento.Visibility = ViewStates.Visible;

                            if (tipoAlmacenamientoProducto != null)
                            {
                                txtViewAlmacenamiento.Text = actualConfig.ProductType + " - " + tipoAlmacenamientoProducto.nombre + " (" + actualConfig.Identifier + ")";
                            }
                            if (tipoAlmacenamientoProducto == null)
                            {
                                txtViewAlmacenamiento.Text = actualConfig.ProductType + " - " + " (" + actualConfig.Identifier + ")";
                            }
                        }
                        else
                        {
                            txtViewAlmacenamiento.Visibility = ViewStates.Gone;
                        }

                        if (binder != null) binder.Service.actualConfig = actualConfig;

                        ThreadPool.QueueUserWorkItem(async o => await IsCuadreReady());
                    });
                }
                else
                {
                    SetScreen(LScreens.NoConfig);
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {

            }
        }

        //OBTENER INVENTARIO FINAL TURNO CONSUMOS BANDEJAS EN PMB - RALDY - 21-02-2023 
        private async Task<double> ValidarInventarioFinalTurno(String IdEquipo, DateTime fecha, Int64 turno)
        {
            var InventarioFinalConsumo = 0.0;
            try
            {
                var repositorioConsumos = repo.GetRepositoryConsumptions();
                var repoz = repo.GetRepositoryZ();
                var ultimaBandejaConsumida = new Consumptions();               
                var cantidadCigarrosConsumo = 0.0;
                
                    // Consumo de producto más reciente 
                    ultimaBandejaConsumida = await repoz.GetLastConsumoBandejaAsync();

                    if (ultimaBandejaConsumida != null)
                    {
                    InventarioFinalConsumo = ultimaBandejaConsumida.Quantity;
                    }
                
            }
            catch (Exception e)
            {
                await CatchException(e);
            }           

            return InventarioFinalConsumo;
        }


        private async Task<Boolean> IsCuadreReady()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();

                var produccionDate = Caching.GetProductionDate();
                var TurnoID = Caching.TurnoID;

                if (actualConfig != null)
                {
                    if (Caching.Stock == null)
                    {
                        await Caching.ReloadStock();
                    }

                    if (Caching.Stock == null)
                    {
                        RunOnUiThread(() =>
                        {
                            var cuadreDialog = new CuadreDialog(this, CuadreDialog._Accions.OPEN, Caching.GetProductionDate(true), Caching.GetNextTurn(), actualConfig, null);
                            if (cuadreDialog.dialog != null)
                            {
                                cuadreDialog.OnAcceptPress += cuadreDialog_OnAcceptPress;
                                cuadreDialog.OnCancelPress += () =>
                                {
                                    Util.ReleaseLock(this, Util.Locks.Turnos, Util.Locks.CallBackSync);
                                };
                                UnBindEventos();
                            }
                        });

                        return true;
                    }
                    else if (Caching.Stock.CustomFecha != Convert.ToInt32(produccionDate.GetDBDate()) || Caching.Stock.TurnID != TurnoID)
                    {
                        var stock = await repoz.ExistClosedStockAsync(produccionDate, TurnoID);

                        if (stock != null && stock.Status == Stocks._Status.Abierto)
                        {
                            CierraCuadre(Caching.Stock);
                            return true;
                        }

                        stock = await repoz.ExistClosedStockAsync(Caching.Stock.Begin.ToLocalTime(), Caching.Stock.TurnID);

                        var minutos = DateTime.Now.Subtract(Caching.Stock.Begin.ToLocalTime()).TotalMinutes;

                        var MaxMinutes = CachingManager.MaxMinutes;

                        if (stock != null && stock.Status == Stocks._Status.Abierto && minutos > MaxMinutes)
                        {
                            CierraCuadre(Caching.Stock);
                            return true;
                        }
                    }
                    else if (Caching.Stock.Status == Stocks._Status.Abierto && Caching.FinishTheTurn())
                    {
                        CierraCuadre(Caching.Stock);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }

            return false;
        }

        private async void passdialog_OnAcceptPress(ProcessList Process)
        {
            try
            {
                var cuadreCloseDialog = new CuadreDialog(this, CuadreDialog._Accions.CLOSED, Caching.GetProductionDate(), Caching.Stock.TurnID, actualConfig, Caching.Stock, Process);
                binder.Service.ReviewWarnings -= Service_ReviewWarningsAsync;
                cuadreCloseDialog.OnAcceptPress += cuadreCloseDialog_OnAcceptPress;
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void cuadreDialog_OnAcceptPress(ProcessList Process, String msg)
        {
            Util.ReleaseLock(this, Util.Locks.Turnos, Util.Locks.CallBackSync);

            var repoSetting = repo.GetRepositorySettings();

            var setting = new Settings()
            {
                Key = Settings.Params.AllComponentAreInside,
                Value = "false"
            };

            await repoSetting.InsertOrReplaceAsync(setting);
            await Caching.ReloadStock();
            await SetTurn();
            BindEventos();
        }

        private async void CierraCuadre(Stocks stock)
        {
            try
            {
                RunOnUiThread(async () =>
                {
                    var Proceso = await repo.GetRepositoryZ().GetProces();
                    var cuadreCloseDialog = new CuadreDialog(this, CuadreDialog._Accions.CLOSED, Caching.GetProductionDate(), stock.TurnID, actualConfig, stock, Proceso, false, Option);
                    cuadreCloseDialog.OnAcceptPress += cuadreCloseDialog_OnAcceptPress;
                    cuadreCloseDialog.OnDoLastAction += cuadreCloseDialog_OnDoLastAction;
                    if (cuadreCloseDialog.dialog != null && binder != null)
                    {
                        UnBindEventos();
                    }
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private void cuadreCloseDialog_OnDoLastAction(CuadreDialog._Options Option)
        {
            Util.ReleaseLock(this, Util.Locks.Turnos, Util.Locks.CallBackSync);

            RunOnUiThread(() =>
            {
                switch (Option)
                {
                    case CuadreDialog._Options.LAST_TRAY:
                        SetScreen(LScreens.ScanOutput);

                        break;

                    case CuadreDialog._Options.WASTES:
                        SetScreen(LScreens.Wastes);

                        break;
                }

                this.Option = Option;
            });
        }

        public async void ShowAlertNextConfig()
        {
            try
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();

                if (SyncService.IsRunning(this))
                {
                    await Task.Run(() => imgNoConnection.Visibility = !serviceConnection.binder.Service.IsConnected ? ViewStates.Visible : ViewStates.Gone);
                    /*RunOnUiThread(() =>
                    {
                        imgNoConnection.Visibility = !serviceConnection.binder.Service.IsConnected ? ViewStates.Visible : ViewStates.Gone;
                    });*/
                }

                var cuadreresult = await IsCuadreReady();

                if (cuadreresult) return;

                var repoz = repo.GetRepositoryZ();

                nextconfig = await repoz.GetNextConfig(Proceso.EquipmentID);

                if (actualConfig == null)
                    actualConfig = await repoz.GetActualConfig(Proceso.EquipmentID);

                if (nextconfig != null && actualConfig != null)
                {
                    var lfecha = nextconfig.Begin.ToLocalTime();
                    if (nextconfig.ProductCode == actualConfig.ProductCode && lfecha <= DateTime.Now)
                    {
                        await repoz.UpdateConfigStatusAsync(actualConfig.ConfigID, Configs._Status.Completed, null, actualConfig.IsCold, actualConfig.Identifier, actualConfig.ProductType, Proceso.IsSubEquipment);
                        await repoz.UpdateConfigStatusAsync(nextconfig.ConfigID, Configs._Status.Enabled, null, actualConfig.IsCold, actualConfig.Identifier, actualConfig.ProductType, Proceso.IsSubEquipment);
                        actualConfig = await repoz.GetActualConfig(Proceso.EquipmentID);
                    }
                }

                var planificacion = await repoz.GetSettingAsync<Boolean>(Settings.Params.Planificacion_Automatica, false);

                if (planificacion && Proceso.IsSubEquipment && actualConfig != null)
                {
                    var changeConfig = await repoz.GetActualConfig(Proceso.EquipmentID);

                    if (changeConfig != null && (actualConfig.ConfigID != changeConfig.ConfigID || actualConfig.IsCold != changeConfig.IsCold))
                    {
                        if (actualConfig.ProductCode.Equals(changeConfig.ProductCode)) /// Si el producto baja dos veces por problema de comunicacion
                        {
                            LoadActualConfig(true);
                        }
                        else if (Util.TryLock(this, Util.Locks.ChangeProduct))
                        {
                            RunOnUiThread(() =>
                            {
                                var chagenconfigdialog = new CustomDialog(this, CustomDialog.Status.Warning, String.Format(GetString(Resource.String.AlertNewConfig), actualConfig.ProductShort, changeConfig.ProductShort), CustomDialog.ButtonStyles.OneButton);
                                chagenconfigdialog.OnCancelPress += () =>
                                {
                                    Util.ReleaseLock(this, Util.Locks.ChangeProduct);
                                };
                                chagenconfigdialog.OnAcceptPress += (Boolean IsCantidad, Single Box, Single Cantidad) =>
                                {
                                    Util.ReleaseLock(this, Util.Locks.ChangeProduct);
                                    SequenceManager.CleanSequences();
                                    LoadActualConfig(true);
                                    BindEventos();
                                    ClearSalida();
                                };
                                UnBindEventos();
                            });
                        }
                    }
                }

                var minutosTurno = (int)Caching.GetNextChangeDate().Subtract(DateTime.Now).TotalMinutes;

                var arregloTurno = Resources.GetStringArray(Resource.Array.MinutosTurno);

                if (Caching.Stock != null)
                {
                    if (actualConfig != null && Convert.ToInt32(actualConfig.TimeID) >= 4)
                    {
                        var empaquedb = await repoz.GetSettingAsync<String>(Settings.Params.PackID, String.Empty);

                        if (!String.IsNullOrEmpty(empaquedb))
                        {
                            var empaque = Caching.GetPackID(actualConfig.EquipmentID);

                            if (txtViewPackageID.Text != empaque)
                            {
                                AssignPackID(empaque);
                            }
                        }
                    }

                    if (actualConfig != null)
                    {
                        RunOnUiThread(async () =>
                        {
                            txtViewBatchID.Text = Util.MaskBatchID(await Routes.GetBatchID(actualConfig.EquipmentID, Caching.Stock.TurnID, Caching.GetProductionDate(), actualConfig.TimeID));
                        });
                    }

                    var stock = await repoz.ExistClosedStockAsync(Caching.GetProductionDate(), Caching.TurnoID);

                    if (stock != null && stock.Status == Stocks._Status.Abierto)
                    {
                        if (arregloTurno.Any(p => p == minutosTurno.ToString()) && Caching.Stock.Status == Stocks._Status.Abierto)
                        {
                            var msg = GetString(Resource.String.AlertChangeTurnDialog);
                            SetFooter(true, Status.Good, String.Format(msg, minutosTurno));

                            //Sincronizar consumos de materiales
                            if (Proceso.IsSubEquipment)
                            {
                                try
                                {
                                    await repoz.SyncConsumptions(Caching.GetProductionDate(), Caching.TurnoID);
                                }
                                catch(Exception ex)
                                {
                                    await Util.SaveException(ex, "Sincronización de consumos antes de cerrar el turno.", false);
                                }
                            }
                            return;
                        }
                    }
                }

                if (nextconfig != null)
                {
                    var minutos = (Int16)(DateTime.Now.Subtract(nextconfig.Begin).TotalMinutes * -1);

                    var arreglo = Resources.GetStringArray(Resource.Array.MinutosCambio);

                    if (arreglo.Any(p => p == minutos.ToString()))
                    {
                        if (minutos < 0)
                            SetFooter(true, Status.Good, String.Format(GetString(Resource.String.AlertChangeTurn), minutos.ToString(), nextconfig.ProductShort ?? nextconfig._ProductCode));
                        else
                            SetFooter(true, Status.Good, String.Format(GetString(Resource.String.AlertChangeTurnNow), nextconfig.ProductShort ?? nextconfig._ProductCode));
                    }
                }
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex);
            }
            finally
            {
                Util.ReleaseLock(this, Util.Locks.CallBackSync);
            }
        }

        private async void cuadreCloseDialog_OnAcceptPress(ProcessList Process, String msg)
        {
            try
            {
                Util.ReleaseLock(this, Util.Locks.Turnos, Util.Locks.CallBackSync);
                var gooddialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.DialogClosedCuadred));
                gooddialog.OnAcceptPress += (Boolean IsCantidad, Single Box, Single Cantidad) =>
                {
                    Finish();
                    StartActivity(typeof(LoginActivity));
                };
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void ShowBodyCommon(Boolean value)
        {           
            try
            {
                RunOnUiThread(() =>
                {
                    lyBodyCommon.Visibility = value ? ViewStates.Visible : ViewStates.Gone;
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        #region Metodos de captura de Desperdicios

        private async Task PostWastes()
        {
            try
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();

                ShowProgress(true);

                var repoWaste = repo.GetRepositoryWastes();
                var materiales = WasteAdapter.Materiales.Where(p => p.Quantity > 0).ToList();

                if (materiales.Any())
                {
                    DateTime ProductionDate = Caching.GetProductionDate();

                    var buffer = materiales.Select(p => new Wastes
                    {
                        BoxNumber = p.BoxNumber,
                        Center = Proceso.Centro,
                        Equipment = Proceso.EquipmentID,
                        Logon = Proceso.Logon,
                        Lot = p.Lot,
                        MaterialCode = p.MaterialCode,
                        ProcessID = Proceso.Process,
                        ProductCode = actualConfig.ProductCode,
                        Quantity = p.Quantity,
                        StockID = Caching.Stock.ID,
                        SubEquipment = actualConfig.SubEquipmentID,
                        Sync = true,
                        TimeID = actualConfig.TimeID,
                        TurnID = Caching.Stock.TurnID,
                        Unit = p.MaterialUnit ?? p.Unit,
                        VerID = actualConfig.VerID
                    }).ToList();

                    var contador = 0;

                    if (Option != CuadreDialog._Options.NONE)
                    {
                        ProductionDate = Caching.Stock.Begin.Date.AddMinutes(-1);
                    }

                    foreach (var item in buffer)
                    {
                        item.CustomFecha = ProductionDate.GetDBDate();
                        item.Fecha = ProductionDate.AddSeconds(contador);
                        contador++;
                    }

                    await repoWaste.InsertAsyncAll(buffer);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {
                ShowProgress(false);
            }

            RunOnUiThread(() =>
            {
                Toast.MakeText(this, Resource.String.WastesMessages, ToastLength.Long).Show();
            });

            if (Option == CuadreDialog._Options.NONE)
                LoadWastes();
            else
                CierraCuadre(Caching.Stock);
        }

        private async void LoadWastes()
        {
            try
            {
                RunOnUiThread(async () =>
                {
                    var repoz = repo.GetRepositoryZ();
                    var lista = await repoz.GetWastesMaterial(Caching.GetProductionDate(), Caching.TurnoID, actualConfig.ProductCode, actualConfig.VerID, Caching.Stock.ID);
                    WasteAdapter = new WasteAdapter(this, lista);
                    listDesperdicios.Adapter = WasteAdapter;
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {

            }
        }

        #endregion

        #region Metodos de Entrada de Materiales

        private async void btnCancelEntrada_Click(object sender, EventArgs e)
        {
            try
            {
                OnBackPressed();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void ClearEntrada()
        {
            try
            {
                RunOnUiThread(() =>
                {
                    if (consumo != null) consumo = null;
                    editScanEntrada.Text = String.Empty;
                    txtCodeEntrada.Text = String.Empty;
                    txtViewDescripcionEntrada.Text = String.Empty;
                    txtViewCantidadEntrada.Text = String.Empty;
                    txtViewBatch.Text = String.Empty;
                    txtViewBatchSap.Text = String.Empty;
                    editScanEntrada.Enabled = true;
                    editScanEntrada.RequestFocus();

                    var imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromInputMethod(editScanEntrada.WindowToken, HideSoftInputFlags.None);
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void editScanEntrada_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editScanEntrada.Text) && e.Event.Action == KeyEventActions.Up)
            {
                try
                {
                    var repoz = repo.GetRepositoryZ();
                    var Proceso = await repoz.GetProces();

                    CustomDialog dialog = null;

                    var barcode = editScanEntrada.Text.GetBarCode();
                    var Materials = Caching.Materials;
                    var bandejas = Caching.bandejas;
                    Single disponible = 0;
                    Single disponiblePorTransacciones = 0;

                   

                    material = Materials.SingleOrDefault(p => p._MaterialCode == barcode.BarCode || p.MaterialReference == barcode.BarCode || p.units.Any(u => u.Ean == barcode.BarCode));

                    LotsList lote = null;
                    IsExpired = false;

                   

                    if (material != null && barcode.Quantity > 0)
                    {
                        if (barcode.IsCustom)
                        {
                            var Batches = Caching.Batches;

                            lote = Batches.FirstOrDefault(p => p.MaterialCode == material.MaterialCode && p.Code == barcode.Lot);

                            if (lote == null)
                            {
                                if (!material.AllowLotDay)
                                {
                                    if (barcode.Sequence == 0 || barcode.IsLotInternal)
                                        new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.MessageWrongEntryNoBatchExist));
                                    else
                                        new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.MessageWrongEntryNoBatchExistSecuence));

                                    ClearEntrada();
                                    return;
                                }
                                else
                                {
                                    if (barcode.Fecha.HasValue)
                                    {
                                        lote = new LotsList()
                                        {
                                            Reference = barcode.Lot,
                                            Code = barcode.Lot,
                                            Expire = barcode.Fecha.Value.AddDays(material.ExpireDay).ToUniversalTime() /// el problema del local Time
                                        };
                                    }
                                    else
                                    {
                                        new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.MessageWrongEntryNoExist));
                                        ClearEntrada();
                                        return;
                                    }
                                }
                            }

                            if (!material.IgnoreStock)
                            {
                                if (barcode.Sequence > 0)
                                {
                                    var last_Transaction = await repoz.Get_Last_Transaction(material.MaterialCode, lote.Code, barcode.Sequence, Caching.Stock.CustomFecha, Caching.Stock.TurnID);

                                    if (last_Transaction == null)
                                    {
                                        dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.EntryMaterialNoStockBox), material._DisplayCode, lote.Code, barcode.Sequence));
                                        dialog.OnCancelPress += () =>
                                        {
                                            ClearEntrada();
                                        };
                                        return;
                                    }

                                    switch (last_Transaction.Get_Type(this))
                                    {
                                        case Transactions.Types.Devolucion_Buffer:
                                            dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(this.GetString(Resource.String.EntryMaterialAlreadyReturn), material._DisplayCode, barcode.Lot, barcode.Sequence));
                                            dialog.OnCancelPress += () =>
                                            {
                                                ClearEntrada();
                                            };
                                            return;

                                        case Transactions.Types.Consumo_Material:
                                            dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(this.GetString(Resource.String.EntryMaterialAlreadyRead), material._DisplayCode, barcode.Lot, barcode.Sequence, Proceso.EquipmentID, last_Transaction.Fecha.ToLocalTime().ToString("dd/MM/yyyy"), last_Transaction.Fecha.ToLocalTime().ToString("hh:mm")));
                                            dialog.OnCancelPress += () =>
                                            {
                                                ClearEntrada();
                                            };
                                            return;
                                    }
                                }

                                disponible = await repoz.GetStockAvailableAsync(material.MaterialCode, lote.Code, barcode.Sequence);

                                disponiblePorTransacciones = await repoz.GetStockAvailableByTransactionsAsync(material.MaterialCode, lote.Code, barcode.Sequence);

                                if (disponible <= 0 && disponiblePorTransacciones <= 0)
                                {
                                    if (barcode.Sequence == 0)
                                        dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.EntryMaterialNoStock), material._DisplayCode, lote.Code));
                                    else
                                        dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.EntryMaterialNoStockBox), material._DisplayCode, lote.Code, barcode.Sequence));

                                    ClearEntrada();
                                    return;
                                }
                            }

                            if (lote._Expire.HasValue && lote.Expire.ToLocalTime().Date < DateTime.Now.Date)
                            {
                                var str = String.Format(GetString(Resource.String.MessageExpiredEntry), material._DisplayCode, lote.Reference, lote.Expire.ToLocalTime().ToString("dd/MM/yy"));
                                dialog = new CustomDialog(this, CustomDialog.Status.Error, str, CustomDialog.ButtonStyles.TwoButtonWithAutorize);
                                IsExpired = true;
                                await Util.AddError(Proceso, actualConfig, material, barcode, Caching);
                            }

                            RunOnUiThread(() =>
                            {
                                txtViewBatch.Text = lote.Reference.Trim();
                                txtViewBatchSap.Text = lote.Code.Trim();
                            });
                        }

                        RunOnUiThread(() =>
                        {
                            txtCodeEntrada.Text = material._MaterialCode;
                            txtViewDescripcionEntrada.Text = material.MaterialName;
                            txtViewCantidadEntrada.Text = String.Format("{0} {1}", barcode.Quantity.ToCustomString(), material.MaterialUnit);
                        });

                       
                        if (!IsExpired)
                        {
                            if (barcode.Sequence == 0 && material.NeedBoxNo)
                            {
                                if (disponible < barcode.Quantity && !material.IgnoreStock)
                                {
                                    dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.EntryMaterialNoAvailable), material._DisplayCode, lote.Code, disponible.ToString("N3")));
                                    ClearEntrada();
                                    return;
                                }

                                dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.MessageGoodEntry), CustomDialog.ButtonStyles.TwoButtonWithBoxPeso, material.MaterialCode, lote.Code);
                            }
                            else if (material.NeedCantidad)
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.MessageGoodEntry), CustomDialog.ButtonStyles.TwoButtonWithBoxCantidad, null, null, material.MaterialUnit);
                            }
                            else if (material.units.Any(s => !String.IsNullOrEmpty(s.Ean)))
                            {
                                var isKeyBoardLocked = await LockTyping();
                                var keyboardManager = new KeyboardManager(this);

                                if (isKeyBoardLocked)
                                {
                                    keyboardManager.HideSoftKeyboard(this);
                                }

                                var eanCode = Convert.ToDouble(material.units.First(s => !String.IsNullOrEmpty(s.Ean)).Ean);
                                dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.MessageGoodEntry), CustomDialog.ButtonStyles.TwoButtonWithUpcCode, material.MaterialCode, barcode.Lot, material.MaterialUnit, eanCode, isKeyBoardLocked);
                            }
                            else
                            {
                                if ((disponible < barcode.Quantity && disponiblePorTransacciones < barcode.Quantity)
                                    && !material.IgnoreStock)
                                {
                                    dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.EntryMaterialNoAvailable), material._DisplayCode, lote.Code, disponible.ToString("N3")));
                                    ClearEntrada();
                                    return;
                                }
                                //else
                                //{
                                //    ///Validacion de Cajas y no Caja en nueva version de consumo

                                //    if (barcode.Secuence == 0)
                                //    {
                                //        var las_transaction = await repoz.Get_Last_Transaction(material.MaterialCode, barcode.Lot, 0, Caching.TurnoID);
                                //        var type = las_transaction.Get_Type(this);

                                //        if (type == Transactions.Types.)
                                //    }
                                //}

                                if (!barcode.IsLotInternal)
                                    dialog = new CustomDialog(this, CustomDialog.Status.GoodValidateBox, GetString(Resource.String.MessageGoodEntry), CustomDialog.ButtonStyles.TwoButton);
                                else
                                    dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.MessageGoodEntry), material.NeedBoxNo ? CustomDialog.ButtonStyles.TwoButtonWithBoxPeso : CustomDialog.ButtonStyles.TwoButton, material.MaterialCode, lote.Code);
                            }
                        }

                        dialog.OnAcceptPress += dialoggood_OnAcceptPress;
                        dialog.OnCancelPress += dialoggood_OnCancelPress;
                        dialog.ONValidaSecuencia += Dialog_ONValidaSecuencia;

                        consumo = new Consumptions()
                        {
                            ProcessID = Proceso.Process,
                            Center = Proceso.Centro,
                            Logon = Proceso.Logon,
                            Produccion = Caching.GetProductionDate(),
                            Fecha = DateTime.Now,
                            Lot = lote.Code,
                            EquipmentID = actualConfig.EquipmentID,
                            SubEquipmentID = actualConfig.SubEquipmentID,
                            Sync = true,
                            SyncSQL = true,
                            MaterialCode = material.MaterialCode,
                            ProductCode = actualConfig.ProductCode,
                            VerID = actualConfig.VerID,
                            Quantity = barcode.Quantity,
                            Unit = material.MaterialUnit,
                            TurnID = Caching.Stock.TurnID,
                            TimeID = actualConfig.TimeID,
                            BoxNumber = barcode.Sequence,
                            Bandeja = null,
                            IsLotInternal = barcode.IsLotInternal
                        };

                        RunOnUiThread(() =>
                        {
                            editScanEntrada.Enabled = false;
                        });
                    }
                    else if (bandejas.Any(p => p.ID.ToUpper() == barcode.BarCode && (barcode.Sequence >= p.Desde && barcode.Sequence <= p.Hasta)))
                    {
                        if (!serviceConnection.binder.Service.IsConnected)
                        {
                            switch (serviceConnection.binder.Service.DisconectionType)
                            {
                                case SyncService.DisconectionsType.WifiOff:
                                    dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.AlertErrorNoWifi));
                                    dialog.OnCancelPress += dialoggood_OnCancelPress;
                                    return;

                                case SyncService.DisconectionsType.ServerDown:
                                    dialog = new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.AlertErrorServerDown), CustomDialog.ButtonStyles.TwoButtonWithContinue);
                                    dialog.OnAcceptPress += dialoggood_OnAcceptPressContigencia;
                                    dialog.OnCancelPress += dialoggood_OnCancelPress;
                                    return;
                            }
                        }
                        var repoTray = repo.GetRepositoryTrays();

                        var BarCode = barcode.BarCode;

                        var bandeja = new TraysList();

                        var configuracionBandeja = await repoTray.GetAsyncByKey(barcode.BarCode);
                        if (configuracionBandeja != null)
                        {
                            

                            if (configuracionBandeja.procesarSAP)
                            {
                                bandeja = await repoz.GetBandejaSalida(barcode.BarCode, barcode.Sequence);
                            }
                            else
                            {
                                try
                                {
                                    bandeja = await repoz.GetEstatusBandeja(barcode.BarCode, barcode.Sequence);


                                }
                                catch (WebException wEx)
                                {
                                    ShowWebExceptionDialog(wEx, "Consulta de estatus de bandeja - Consumo Material");
                                    ClearEntrada();
                                    return;
                                }
                                catch (Exception ex)
                                {
                                    await Util.SaveException(ex);
                                    var errorDialog = new CustomDialog(this, CustomDialog.Status.Error, "Error interno.");
                                    return;
                                }
                            }
                        }



                        Int32 Tiempos = 0;

                        Materials = Caching.Materials;
                        material = Materials.SingleOrDefault(p => p.MaterialCode == bandeja.ProductCode);

                        if (bandeja.Status == TraysProducts._Status.Vacio)
                        {
                            var lastBandeja = await repoz.GetLastBandejaEntrada();

                            if (lastBandeja == null)
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.MessageEmptyTray));
                                dialog.OnCancelPress += dialoggood_OnCancelPress;
                                // AssingBatchID(String.Empty);
                                // Routes.Clean();
                                return;
                            }
                            else if (lastBandeja.TrayID == bandeja._TrayID)
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Warning, String.Format(GetString(Resource.String.AlertErrorTrayAlreadyReaded), bandeja._TrayID));
                                dialog.OnCancelPress += () =>
                                {
                                    ClearEntrada();
                                };
                                dialog.OnAcceptPress += (arg2, arg3, arg4) =>
                                {
                                    ClearEntrada();
                                };
                                return;
                            }
                            else
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.MessageEmptyTray));
                                dialog.OnCancelPress += dialoggood_OnCancelPress;
                                // AssingBatchID(String.Empty);
                                // Routes.Clean();
                                return;
                            }
                        }
                        else if (material == null)
                        {
                            // AssingBatchID(String.Empty);

                            Tiempos = actualConfig.TimeID.CastToShort() - bandeja.TimeID.CastToShort();

                            if (bandeja._ProductCode != actualConfig._ProductCode || bandeja.TimeID == actualConfig.TimeID)
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.MessageWrongEntry), bandeja._ProductCode, actualConfig._DisplayCode));
                                dialog.OnCancelPress += dialoggood_OnCancelPress;
                                Routes.Clean();
                                return;
                            }

                            material = new MaterialList()
                            {
                                MaterialCode = actualConfig.ProductCode,
                                MaterialUnit = bandeja.Unit
                            };

                            dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.MessageGoodEntry), CustomDialog.ButtonStyles.TwoButton);
                            dialog.OnAcceptPress += dialoggoodConsumoBandejas_OnAcceptPress;
                            dialog.OnCancelPress += dialoggood_OnCancelPress;
                        }
                        else
                        {
                            // AssingBatchID(String.Empty);

                            //Tiempos = actualConfig.TimeID.CastToShort() - bandeja.TimeID.CastToShort();

                            //if (Tiempos > 2)
                            //{
                            //    dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.MessageWrongEntry), bandeja._ProductCode, actualConfig._DisplayCode));
                            //    dialog.OnCancelPress += dialoggood_OnCancelPress;
                            //    Routes.Clean();
                            //    return;
                            //}

                            //INICIO LINEAS AGREGADA POR PARA VALIDAR TIEMPO CONSUMO EN PMB
                            var cumpleValidacionTiempoConsumoPMB = await ValidarTiempoConsumo(Proceso.Process, actualConfig.TimeID);
                            if (cumpleValidacionTiempoConsumoPMB)
                            {                               
                                dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.MessageGoodEntry), CustomDialog.ButtonStyles.TwoButton);
                                dialog.OnAcceptPress += dialoggoodConsumoBandejas_OnAcceptPress;
                                dialog.OnCancelPress += dialoggood_OnCancelPress;
                            }
                            else
                            {                               
                                
                                ClearEntrada();
                                ShowTimeConsumptionsDialog();
                                return;                                
                            //FIN LINEAS AGREGADA PARA VALIDAR TIEMPO CONSUMO EN PMB.

                            //ESTAS LINEAS COMENTADAS PARA MOSTRAR DIALOGO CON MENSAJE ESTABA AQUI ANTES DE RALDY MODIFICAR.
                                //dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.MessageGoodEntry), CustomDialog.ButtonStyles.TwoButton);
                                //dialog.OnAcceptPress += dialoggoodConsumoBandejas_OnAcceptPress;
                                //dialog.OnCancelPress += dialoggood_OnCancelPress;
                            }

                        var producto = repoz.GetMaterialByCode(bandeja.ProductCode);

                        if (producto != null)
                        {
                            

                            RunOnUiThread(() =>
                            {
                                txtCodeEntrada.Text = producto._Code;
                                txtViewDescripcionEntrada.Text = producto.Name;
                                txtViewCantidadEntrada.Text = String.Format("{0} {1}", bandeja.Quantity.ToString("n"), bandeja.Unit);
                                txtViewBatchSap.Text = bandeja.BatchID;
                            });

                          }
                        }

                       



                        Routes.SetBandeja(bandeja);

                        consumo = new Consumptions()
                        {
                            ProcessID = Proceso.Process,
                            Center = Proceso.Centro,
                            Logon = Proceso.Logon,
                            Produccion = Caching.GetProductionDate(),
                            Fecha = DateTime.Now,
                            Lot = String.Empty,
                            EquipmentID = actualConfig.EquipmentID,
                            SubEquipmentID = actualConfig.SubEquipmentID,
                            Sync = true,
                            SyncSQL = true,
                            MaterialCode = material.MaterialCode,
                            ProductCode = actualConfig.ProductCode,
                            VerID = actualConfig.VerID,
                            Quantity = bandeja.Quantity,
                            Unit = material.MaterialUnit,
                            TurnID = Caching.Stock.TurnID,
                            TimeID = actualConfig.TimeID,
                            TrayID = barcode.ID.ToString(),
                            TrayEquipmentID = bandeja.EquipmentID,
                            TrayDate = bandeja.Fecha,
                            ElaborateID = bandeja.ElaborateID,
                            BatchID = bandeja.BatchID,
                            BoxNumber = barcode.Sequence,
                            Bandeja = new TraysProducts()
                            {
                                ID = bandeja._TrayID.ToUpper(),
                                ModifyDate = DateTime.Now,
                                UsuarioVaciada = Proceso.Logon,
                                FechaHoraVaciada = DateTime.Now,
                                IdEquipoVaciado = actualConfig.EquipmentID,
                                formaVaciada = "Consumo",
                                ProductCode = String.Empty,
                                Quantity = 0,
                                Secuencia = bandeja.Secuencia,
                                Sync = true,
                                TrayID = bandeja.TrayID.ToUpper(),
                                Unit = String.Empty,
                                VerID = String.Empty,
                                ElaborateID = 0,
                                Fecha = null,
                                BatchID = String.Empty,
                                EquipmentID = String.Empty,
                                Status = TraysProducts._Status.Vacio
                            }
                        };
                    }
                    else
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
                            new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.MessageWrongEntry), MaterialWrong._DisplayCode, actualConfig.ProductShort ?? actualConfig._ProductCode));
                            await Util.AddError(Proceso, actualConfig, MaterialWrong, barcode, Caching);
                        }
                        else
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.MessageWrongEntryNoExist));
                        }

                        ClearEntrada();
                    }

                   


                    RunOnUiThread(() =>
                    {
                        editScanEntrada.Text = String.Empty;
                    });

                  

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

        private void Dialog_ONValidaSecuencia()
        {
            RunOnUiThread(async () =>
            {
                var barcode = editScanEntrada.Text.GetBarCode();

                var repor = repo.GetRepositoryR();
                CustomDialog dialog = null;

                ShowProgress(true);

                var result = await repor.ValidaConsumoenLinea(consumo);

                ShowProgress(false);

                if (result == null || result.Quantity < 0)
                {
                    dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.MessageGoodEntry), material.NeedCantidad ? CustomDialog.ButtonStyles.TwoButtonWithBoxCantidad : CustomDialog.ButtonStyles.TwoButton);
                    dialog.OnAcceptPress += dialoggood_OnAcceptPress;
                    dialog.OnCancelPress += dialoggood_OnCancelPress;
                }
                else
                {
                    dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(this.GetString(Resource.String.EntryMaterialAlreadyRead), ExtensionsMethodsHelper.GetSapCode(result.MaterialCode), result.Lot, result.BoxNumber, result.EquipmentID, result.Produccion.ToString("dd/MM/yyyy"), result.Produccion.ToString("hh:mm")));
                    dialog.OnCancelPress += dialoggood_OnCancelPress;
                }
            });
        }

        private async void ToggleColorsTextView(TextView obj)
        {
            var listaColores = new List<Android.Graphics.Color>();
            listaColores.Add(Android.Graphics.Color.Blue);
            listaColores.Add(Android.Graphics.Color.Red);
            listaColores.Add(Android.Graphics.Color.YellowGreen);
            listaColores.Add(Android.Graphics.Color.Green);
            listaColores.Add(Android.Graphics.Color.Black);
            listaColores.Add(Android.Graphics.Color.Brown);
            listaColores.Add(Android.Graphics.Color.Gray);
            listaColores.Add(Android.Graphics.Color.Pink);
            listaColores.Add(Android.Graphics.Color.Yellow);

            var rnd = new System.Random();

            var Tiempo = DateTime.Now;

            do
            {
                var color = rnd.Next(0, listaColores.Count - 1);
                await Task.Delay(150);
                RunOnUiThread(() =>
                {
                    obj.SetTextColor(listaColores.ElementAt(color));
                });
            } while (DateTime.Now.Subtract(Tiempo).TotalSeconds < 16);

            RunOnUiThread(() =>
            {
                obj.SetTextColor(Android.Graphics.Color.Black);
            });
        }

        /// <summary>
        /// Funcion para Asignar el BathID
        /// </summary>
        /// <param name="Value">BathID</param>
        private async void AssignBatchID(String Value)
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                var repoSettings = repo.GetRepositorySettings();
                if (!String.IsNullOrEmpty(Value))
                {
                    await repoSettings.InsertOrReplaceAsync(new Settings() { Key = Settings.Params.BatchID, Value = Value });
                }

                RunOnUiThread(() =>
                {
                    ThreadPool.QueueUserWorkItem(o => ToggleColorsTextView(txtViewBatchID));
                    txtViewBatchID.Text = String.IsNullOrEmpty(Value) ? String.Empty : Util.MaskBatchID(Value);
                });

                //else if (Caching != null && Caching.Stock != null && actualConfig != null)
                //{
                //    RunOnUiThread(async () =>
                //   {
                //       txtViewBatchID.Text = Util.MaskBatchID(await Routes.GetBatchID(actualConfig.EquipmentID, Caching.Stock.TurnID, Caching.GetProductionDate(), actualConfig.TimeID));
                //   });
                //}
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void AssignPackID(String Value)
        {
            try
            {
                if (await IsFinalProcess())
                {
                    var repoz = repo.GetRepositoryZ();
                    var repoSettings = repo.GetRepositorySettings();
                    var oldValue = "";
                    oldValue = await repoz.GetSettingAsync<String>(Settings.Params.PackID, String.Empty);
                    await repoSettings.InsertOrReplaceAsync(new Settings() { Key = Settings.Params.PackID, Value = Value });
                    RunOnUiThread(() =>
                    {
                        linearLayoutEmpaque.Visibility = String.IsNullOrEmpty(Value) ? ViewStates.Gone : ViewStates.Visible;
                        if (oldValue != null)
                        {
                            if (!oldValue.Equals(Value))
                            {
                                ThreadPool.QueueUserWorkItem(o => ToggleColorsTextView(txtViewPackageID));
                            }
                        }
                        txtViewPackageID.Text = Value;
                    });
                }
                else
                {
                    RunOnUiThread(() =>
                    {
                        linearLayoutEmpaque.Visibility = ViewStates.Gone;
                    });
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void dialoggood_OnCancelPress()
        {
            try
            {
                ClearEntrada();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void dialoggood_OnAcceptPress(Boolean IsCantidad, Single Box, Single Cantidad)
        {
            try
            {
                if (IsExpired)
                {
                    var material = Caching.Materials.Single(p => p.MaterialCode == consumo.MaterialCode);

                    if (material.NeedCantidad)
                    {
                        CatchQuantity(true, material.MaterialUnit);
                        return;
                    }
                    else if (material.NeedBoxNo)
                    {
                        CatchQuantity(false, material.MaterialUnit);
                        return;
                    }
                }


                //CONTROLAR CANTIDAD DE TIPS CONSUMIDOS POR ETIQUETA O CAJA EN CUALQUIER PMB. 
                // Raldy de Jesus 29-12-2022  - ACTUALIZADO 15/02/2023    
                var repor = repo.GetRepositoryR();
                var materialConsumidoAnyMachine = await repor.GetMaterialReport(material.MaterialCode, consumo.Lot, consumo.BoxNumber); //LINEA PARA CONSULTAR CONSUMOS EN CUALQUIER PMB (sql).
                var repoz = repo.GetRepositoryZ();
               // var materialConsumido = await repoz.GetQuantityMaterialConsumedAsync(material.MaterialCode, consumo.Lot, consumo.BoxNumber); //CONSULTAR CANTIDAD TIPS CONSUMIDOS DE UNA ETIQUETA. Tabla Consumptions(sqlite)
                
                if (materialConsumidoAnyMachine > 0)
                {  
                    Single materialDisponible = consumo.Quantity - materialConsumidoAnyMachine;
                    if (Box > consumo.Quantity)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Error. Cantidad de Material Digitada Excede la disponible");
                        String alert1 = "Cantidad Maxima que contiene esta etiqueta: " + consumo.Quantity;
                        String alert2 = "Consumir cantidad igual o menor al total de esta etiqueta (caja) para continuar.";
                        String alert3 = "Material: " + consumo.MaterialCode;
                        String alert4 = "Lote: " + consumo.Lot;
                        alert.SetMessage(alert1 + "\n" + alert2 + "\n" + alert3 + "\n" + alert4);

                        alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
                        {
                            Toast.MakeText(this, "Boton Aceptar Seleccionado!", ToastLength.Short).Show();
                        });

                        Dialog dialogg = alert.Create();
                        dialogg.Show();
                        ClearEntrada();
                        return;
                    }
                    if (materialDisponible < 0)
                    {
                        materialDisponible = 0;
                    }
                    if (materialDisponible < Box)
                    {

                        AlertDialog.Builder alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Error. Cantidad de Material Digitada Excede la disponible");
                        String alert1 = "Cantidad disponible de este material: " + materialDisponible;
                        String alert2 = "Consumir cantidad disponible o leer otra etiqueta (caja) para continuar.";
                        String alert3 = "Material: " + consumo.MaterialCode;
                        String alert4 = "Lote: " + consumo.Lot;
                        alert.SetMessage(alert1 + "\n" + alert2 + "\n" + alert3 + "\n" + alert4);

                        alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
                        {
                            Toast.MakeText(this, "Boton Aceptar Seleccionado!", ToastLength.Short).Show();
                        });

                        Dialog dialogg = alert.Create();
                        dialogg.Show();
                        ClearEntrada();
                        return;
                       

                    }
                }
                else
                {
                    var materialConsumido = await repoz.GetQuantityMaterialConsumedAsync(material.MaterialCode, consumo.Lot, consumo.BoxNumber); //CONSULTAR CANTIDAD TIPS CONSUMIDOS DE UNA ETIQUETA. Tabla Consumptions(sqlite)
                    if (materialConsumido > 0)
                       {
                        
                        Single materialDisponible = consumo.Quantity - materialConsumido;
                        if (Box > consumo.Quantity)
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(this);
                            alert.SetTitle("Error. Cantidad de Material Digitada Excede la disponible");
                            String alert1 = "Cantidad Maxima que contiene esta etiqueta: " + consumo.Quantity;
                            String alert2 = "Consumir cantidad igual o menor al total de esta etiqueta (caja) para continuar.";
                            String alert3 = "Material: " + consumo.MaterialCode;
                            String alert4 = "Lote: " + consumo.Lot;
                            alert.SetMessage(alert1 + "\n" + alert2 + "\n" + alert3 + "\n" + alert4);

                            alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
                            {
                                Toast.MakeText(this, "Boton Aceptar Seleccionado!", ToastLength.Short).Show();
                            });

                            Dialog dialogg = alert.Create();
                            dialogg.Show();
                            ClearEntrada();
                            return;
                        }
                        if (materialDisponible < 0)
                        {
                            materialDisponible = 0;
                        }
                        if (materialDisponible < Box)
                        {
                               AlertDialog.Builder alert = new AlertDialog.Builder(this);
                               alert.SetTitle("Error. Cantidad de Material Digitada Excede la disponible");
                               String alert1 = "Cantidad disponible de este material: " + materialDisponible;
                               String alert2 = "Consumir cantidad disponible o leer otra etiqueta (caja) para continuar.";
                               String alert3 = "Material: " + consumo.MaterialCode;
                               String alert4 = "Lote: " + consumo.Lot;
                               alert.SetMessage(alert1 + "\n" + alert2 + "\n" + alert3 + "\n" + alert4);

                               alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
                               {
                                   Toast.MakeText(this, "Boton Aceptar Seleccionado!", ToastLength.Short).Show();
                               });

                               Dialog dialogg = alert.Create();
                               dialogg.Show();
                               ClearEntrada();
                               return;
                        }
                        else
                        {
                            await SaveEntrada(IsCantidad, Box);
                        }
                    }
                    else
                    {
                        if (Box > consumo.Quantity)
                        {
                            AlertDialog.Builder alert = new AlertDialog.Builder(this);
                            alert.SetTitle("Error. Cantidad de Material Digitada Excede la disponible");
                            String alert1 = "Cantidad Maxima que contiene esta etiqueta: " + consumo.Quantity;
                            String alert2 = "Consumir cantidad igual o menor al total de esta etiqueta (caja) para continuar.";
                            String alert3 = "Material: " + consumo.MaterialCode;
                            String alert4 = "Lote: " + consumo.Lot;
                            alert.SetMessage(alert1 + "\n" + alert2 + "\n" + alert3 + "\n" + alert4);

                            alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
                            {
                                Toast.MakeText(this, "Boton Aceptar Seleccionado!", ToastLength.Short).Show();
                            });

                            Dialog dialogg = alert.Create();
                            dialogg.Show();
                            ClearEntrada();
                            return;
                        }
                        else
                        {
                            await SaveEntrada(IsCantidad, Box);
                        }
                    }
                }

               // await SaveEntrada(IsCantidad, Box);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void dialoggoodConsumoBandejas_OnAcceptPress(Boolean IsCantidad, Single Box, Single Cantidad)
        {
            try
            {
                //Control de acceso al consumo de bandejas
                var sec_Manager = new SecurityManager(this);
                sec_Manager.Response += async (arg, arg1) =>
                {
                    if (arg)
                    {
                        if (IsExpired)
                        {
                            var material = Caching.Materials.Single(p => p.MaterialCode == consumo.MaterialCode);

                            if (material.NeedCantidad)
                            {
                                CatchQuantity(true, material.MaterialUnit);
                                return;
                            }
                            else if (material.NeedBoxNo)
                            {
                                CatchQuantity(false, material.MaterialUnit);
                                return;
                            }
                        }
                        if (SecurityManager.CurrentProcess.Logon != null)
                        {
                            consumo.Logon = SecurityManager.CurrentProcess.Logon;
                            consumo.Bandeja.UsuarioVaciada = consumo.Logon;
                        }
                        await SaveEntrada(IsCantidad, Box);
                    }
                    if (!arg)//Si presiona el botón cancelar para no autenticarse
                    {
                        ClearEntrada();
                    }
                };

                sec_Manager.HaveAccess(RolsPermits.Permits.CONSUMIR_BANDEJAS);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void CatchQuantity(Boolean IsCantidad, String Unit)
        {
            try
            {
                RunOnUiThread(() =>
                {
                    Dialog ldialog = null;
                    var builder = new AlertDialog.Builder(this);

                    #region Init

                    var view = LayoutInflater.Inflate(Resource.Layout.dialog_catch_quantity, null);
                    var editQuantity = view.FindViewById<EditText>(Resource.Id.editQuantity);

                    if (IsCantidad)
                    {
                        view.FindViewById<TextView>(Resource.Id.txtViewTitle).Text = String.Format(GetString(Resource.String.EntryBoxQuantity), Unit);
                    }
                    else
                    {
                        view.FindViewById<TextView>(Resource.Id.txtViewTitle).Text = GetString(Resource.String.EntryBoxNo);
                        editQuantity.InputType = Android.Text.InputTypes.ClassNumber;
                    }

                    var btnAceptar = view.FindViewById<Button>(Resource.Id.btnAceptar);

                    #endregion

                    btnAceptar.Click += async (sender, args) =>
                    {
                        try
                        {
                            if (editQuantity.Text.CastToSingle() == 0 || editQuantity.Text.CastToSingle() > 1000)
                            {
                                editQuantity.Text = String.Empty;
                                editQuantity.RequestFocus();
                                return;
                            }

                            if (!IsCantidad)
                            {
                                var repoz = repo.GetRepositoryZ();
                                var exist = repoz.ValidaBoxNumber(consumo.MaterialCode, consumo.Lot, editQuantity.Text.CastToShort());

                                if (exist)
                                {
                                    new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.MessageExistBoxNumber), editQuantity.Text));
                                    return;
                                }
                            }

                            await SaveEntrada(IsCantidad, editQuantity.Text.CastToSingle());
                            ldialog.Dismiss();
                            ldialog.Dispose();
                        }
                        catch (Exception ex)
                        {
                            await CatchException(ex);
                        }
                    };

                    builder.SetView(view);
                    builder.SetCancelable(false);

                    ldialog = builder.Create();
                    ldialog.Show();
                    editQuantity.RequestFocus();
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {

            }
        }

        private async Task SaveEntrada(Boolean IsCantidad, Single Box)
        {
            try
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();
                var repomzilm = repo.GetRepositoryMaterialZilm();
                var repoc = repo.GetRepositoryConsumptions();
                var repoConfiguracionSincronizacionTablas = repo.GetRepositoryConfiguracionSincronizacionTablas();

                var materialzilm = await repomzilm.GetAsyncByKey(consumo.MaterialCode);

                if (!HasNetConnection()) //Verificando conexión 
                {
                    return;
                }

                if (IsCantidad)
                {
                    if (!materialzilm.IgnoreStock)
                    {
                        var disponible = await repo.GetRepositoryZ().GetStockAvailableAsync(consumo.MaterialCode, consumo.Lot, consumo.BoxNumber);

                        if (disponible < Box)
                        {
                            var dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.EntryMaterialNoAvailable), ExtensionsMethodsHelper.GetSapCode(consumo.MaterialCode), consumo.Lot, disponible.ToString("N3")));
                            ClearEntrada();
                            return;
                        }
                    }

                    consumo.Quantity = Box;
                }

                if (IsExpired) /// Le asigno quien aprobo el consumo
                {
                    consumo.Logon = SecurityManager.CurrentProcess.Logon;
                }

                var transaction = new Transactions()
                {
                    TurnID = Caching.Stock.TurnID,
                    Reason = GetString(Resource.String.ReceiptConsumption),
                    MaterialCode = consumo.MaterialCode,
                    Lot = consumo.Lot,
                    Unit = consumo.Unit,
                    Sync = true,
                    CustomFecha = consumo.Produccion.GetDBDate(),
                    Fecha = consumo.Produccion,
                    BoxNumber = (Box > 0 && !consumo.IsLotInternal) || (IsCantidad && consumo.BoxNumber == 0) ? (short)0 : consumo.BoxNumber,
                    Quantity = consumo.Quantity * -1,
                    Logon = consumo.Logon
                };

                consumo.CustomID = await SequenceManager.AddMaterial(consumo.MaterialCode, consumo.Lot, Proceso.IsLast);

                if (materialzilm.NeedBoxNo) consumo.BoxNumber = (Int16)Box;

                //Si el consumo es de un material, se procede a registrarse en la tabla de consumos
                if (consumo.Bandeja == null)
                    await repoc.InsertAsync(consumo);

                if (consumo.Bandeja != null)//Si el consumo de material es de bandeja, se consume de acuerdo a su configuración. 
                {
                    var wasTrayProductProcessed = false;
                    wasTrayProductProcessed = await ProcessInputTrayProductAsync(Proceso);
                    if (!wasTrayProductProcessed)
                    {
                        return;
                    }
                    else
                    {
                        await repoc.InsertAsync(consumo);
                        Routes.LoadRoute(consumo.Produccion);
                        SetTraysCounter();//Actualizando contador de bandejas

                        if (Proceso.EquipmentID.StartsWith("S", StringComparison.InvariantCultureIgnoreCase))
                        {
                            try
                            {
                                await repoc.SyncAsync(false);// Sincronización inmediata de consumos de bandejas en Equipos de Scandia
                            }
                            catch (WebException wex)
                            {
                                await Util.SaveException(wex, "Sincronización inmediata de consumos de bandejas", false);
                            }
                            catch (Exception e)
                            {
                                await Util.SaveException(e, "Sincronización inmediata de consumos de bandejas", false);
                            }
                        }
                    }
                }
                else if (!materialzilm.IgnoreStock)
                {
                    var repotran = repo.GetRepositoryTransactions();
                    await repotran.InsertAsync(transaction);
                }

                if (!String.IsNullOrEmpty(consumo.BatchID)) AssignBatchID(consumo.BatchID);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {
                ShowProgress(false);
                ClearEntrada();
            }
        }

        private async void dialoggood_OnAcceptPressContigencia(Boolean IsCantidad, Single Box, Single Cantidad)
        {
            try
            {
                var pass = new PassDialog(this, PassDialog.Motivos.Necesita_Autorizacion, RolsPermits.Permits.BANDEJAS_EN_CONTIGENCIA);
                pass.OnCancelPress += () =>
                {
                    ClearEntrada();
                };
                pass.OnAcceptPress += async (ProcessList Process) =>
                {
                    var Proceso = await repo.GetRepositoryZ().GetProces();
                    const String DateMask = "dd/MM/yy";
                    AlertDialog dialog = null;
                    var builder = new AlertDialog.Builder(this);
                    builder.SetCancelable(false);
                    var view = LayoutInflater.Inflate(Resource.Layout.dialog_catch_material, null);

                    var txtViewBandeja = view.FindViewById<TextView>(Resource.Id.txtViewBandeja);
                    var spnEquipment = view.FindViewById<Spinner>(Resource.Id.spnEquipment);
                    var spnTurn = view.FindViewById<Spinner>(Resource.Id.spnTurn);
                    var spnProduct = view.FindViewById<Spinner>(Resource.Id.spnProduct);
                    var txtViewDate = view.FindViewById<TextView>(Resource.Id.txtViewDate);
                    var btnDate = view.FindViewById<ImageButton>(Resource.Id.btnDate);
                    var btnAceptDialog = view.FindViewById<Button>(Resource.Id.btnAceptDialog);
                    var btnCancelDialog = view.FindViewById<Button>(Resource.Id.btnCancelDialog);

                    var repoEquipments = repo.GetRepositoryEquipments();
                    var repoTurns = repo.GetRepositoryTurns();
                    var repoz = repo.GetRepositoryZ();

                    var allEquipments = await repoEquipments.GetAsyncAll();
                    var AllTurns = await repoTurns.GetAsyncAll();
                    var Allproducts = await repoz.GetConfigsByTray(editScanEntrada.Text.GetBarCode().BarCode);

                    var equipments = new List<String>() { "[Select One]" };
                    var turns = new List<String>() { "[Select One]" };
                    var products = new List<String>() { "[Select One]" };

                    allEquipments = allEquipments.Where(p => Convert.ToInt32(p.TimeID) == 1).ToList();
                    equipments.AddRange(allEquipments.Select(p => String.Format("{0} - {1}", p.ID, p.Name)));
                    turns.AddRange(AllTurns.Select(p => p.ID.ToString()));
                    products.AddRange(Allproducts.Select(p => !String.IsNullOrEmpty(p.Short) ? String.Format("{0} - {1}", p.Short, p._Code) : p._Code));

                    txtViewBandeja.Text = String.Format("{0} : {1}", GetString(Resource.String.ReportTitleBandeja), editScanEntrada.Text);
                    spnEquipment.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, equipments);
                    spnTurn.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, turns);
                    spnProduct.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, products);

                    btnAceptDialog.Click += async (obj, args) =>
                    {
                        if (String.IsNullOrEmpty(txtViewDate.Text))
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.MessageDate));
                            return;
                        }

                        if (spnEquipment.SelectedItemPosition == 0)
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.MessageEquipment));
                            return;
                        }

                        if (spnTurn.SelectedItemPosition == 0)
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.MessageTurn));
                            return;
                        }

                        if (spnProduct.SelectedItemPosition == 0)
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.MessageProduct));
                            return;
                        }

                        var producto = Allproducts.ElementAt(spnProduct.SelectedItemPosition - 1);

                        if (!Caching.Materials.Any(p => p.MaterialCode == producto.Code) && (Convert.ToInt16(actualConfig.TimeID) != 1 && producto.Code != actualConfig.ProductCode))
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.MessageWrongEntry), producto.Short ?? producto._Code, actualConfig.ProductShort ?? actualConfig._ProductCode));
                            return;
                        }

                        var bandeja = editScanEntrada.Text.GetBarCode();

                        var trayData = await repoz.GetTimeByTray(bandeja.BarCode);

                        consumo = new Consumptions()
                        {
                            ProcessID = Proceso.Process,
                            Center = Proceso.Centro,
                            Logon = Proceso.Logon,
                            Produccion = Caching.GetProductionDate(),
                            Fecha = DateTime.Now,
                            Lot = String.Empty,
                            EquipmentID = Proceso.EquipmentID,
                            SubEquipmentID = actualConfig.SubEquipmentID,
                            Sync = true,
                            SyncSQL = true,
                            MaterialCode = producto.Code,
                            ProductCode = actualConfig.ProductCode,
                            VerID = actualConfig.VerID,
                            Quantity = trayData.Quantity,
                            Unit = trayData.Unit,
                            TurnID = Caching.Stock.TurnID,
                            TimeID = actualConfig.TimeID,
                            TrayID = bandeja.FullBarCode,
                            Bandeja = null,
                            TrayEquipmentID = allEquipments.ElementAt(spnEquipment.SelectedItemPosition - 1).ID,
                            TrayDate = DateTime.ParseExact(txtViewDate.Text, DateMask, CultureInfo.InvariantCulture),
                            BatchID = await Routes.GetBatchID(allEquipments.ElementAt(spnEquipment.SelectedItemPosition - 1).ID, AllTurns.ElementAt(spnTurn.SelectedItemPosition - 1).ID, DateTime.ParseExact(txtViewDate.Text, DateMask, CultureInfo.InvariantCulture), "1")
                        };

                        await SaveEntrada(false, 0);
                        ClearEntrada();
                        Routes.Clean();

                        dialog.Dismiss();
                        dialog.Dispose();
                    };

                    btnCancelDialog.Click += (obj, args) =>
                    {
                        ClearEntrada();
                        dialog.Dismiss();
                        dialog.Dispose();
                    };

                    btnDate.Click += (obj, args) =>
                    {
                        var str = !String.IsNullOrEmpty(txtViewDate.Text) ? txtViewDate.Text : DateTime.Now.ToString(DateMask);
                        var fecha = DateTime.ParseExact(str, DateMask, CultureInfo.InvariantCulture);
                        var Dialog = new DatePickerDialog(this, (s, arg) =>
                        {
                            txtViewDate.Text = arg.Date.ToString(DateMask);
                        }, fecha.Year, fecha.Month - 1, fecha.Day);
                        Dialog.DatePicker.SetMinDate(DateTime.Now.AddDays(-7).Date);
                        Dialog.DatePicker.SetMaxDate(DateTime.Now.AddDays(1).Date);
                        Dialog.Show();
                    };

                    builder.SetView(view);
                    dialog = builder.Create();
                    dialog.Show();
                };
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }



        #endregion

        #region Metodos de Salida de Materiales

        private async void ClearSalida()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                var proceso = await repoz.GetProces();                

                if (actualConfig == null)
                    actualConfig = await repoz.GetActualConfig(proceso.EquipmentID);

                if (actualConfig != null)
                {                
                 
                    var estadistica = await repoz.GetTotalElaborate(Caching.GetProductionDate(), actualConfig.ProductCode, Caching.Stock != null ? Caching.Stock.TurnID : Caching.TurnoID);

                    RunOnUiThread(async () =>
                    {
                        if (estadistica != null)
                        {
                            txtViewBandeja.Text = estadistica.Quantity.ToString();
                            txtViewCantidadSalida.Text = estadistica.Amounts.ToString("N3");
                            txtViewUnidadSalida.Text = estadistica.Unit;
                            txtViewSecuencia.Text = estadistica.SecuenciaEmpaque.ToString();
                            if ((await IsFinalProcess()) && linearLayoutSecuencia.Visibility != ViewStates.Visible)
                            {
                                linearLayoutSecuencia.Visibility = ViewStates.Visible;
                            }
                        }
                        editScanSalida.Text = String.Empty;
                        editScanSalida.Enabled = true;
                        editScanSalida.RequestFocus();                       
                        var imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                        imm.HideSoftInputFromInputMethod(editScanSalida.WindowToken, HideSoftInputFlags.None);
                    });
                }
                else
                {
                    RunOnUiThread(() =>
                    {
                        var estadistica = new ElaborateTotal();
                        txtViewBandeja.Text = estadistica.Quantity.ToString();
                        txtViewCantidadSalida.Text = estadistica.Amounts.ToString("N3");
                        txtViewUnidadSalida.Text = estadistica.Unit;
                    });
                }
                if (Salida != null) Salida = null;
            }
            catch (Exception ex)
            {
                await CatchException(ex);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private async void editScanSalida_Click(object sender, EventArgs eventArgs)
        {
            try
            {
                RunOnUiThread(() => {
                    var hideAndShowKeyboard = new KeyboardManager(this);
                    //Ocultamiento de teclado en el campo de salida de material
                    hideAndShowKeyboard.HideSoftKeyboard(this);
                });
            }
            catch (Exception e)
            {
                await CatchException(e);
            }

        }

        private async void editScanSalida_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editScanSalida.Text) && e.Event.Action == KeyEventActions.Up)
            {
                try
                {

                    RunOnUiThread(() =>
                    {
                        editScanSalida.Enabled = false;
                    });

                    var Proceso = await repo.GetRepositoryZ().GetProces();

                    CustomDialog dialog = null;
                    String str = null;
                    Boolean IsBox = false;
                    Boolean JustPrint = false;

                    var repoz = repo.GetRepositoryZ();

                    var barcode = editScanSalida.Text.GetBarCode();

                    //Generar Registro 
                    Salida = new Elaborates()
                    {
                        EquipmentID = actualConfig.EquipmentID,
                        Produccion = Caching.GetProductionDate(),
                        Fecha = DateTime.Now,
                        Logon = Proceso.Logon,
                        Sync = true,
                        SyncSQL = true,
                        VerID = actualConfig.VerID,
                        //TrayID = IsBox || Proceso.IsLast ? String.Empty : barcode.ID,
                        SubEquipmentID = actualConfig.SubEquipmentID,
                        TurnID = Caching.Stock.TurnID,
                        BatchID = await Routes.GetBatchID(actualConfig.EquipmentID, Caching.Stock.TurnID, Caching.GetProductionDate(), actualConfig.TimeID),
                        //Unit = bandeja.Unit,
                        //Quantity = !Proceso.IsLast ? bandeja.Quantity : barcode.BarCode.CastToSingle(),
                        ProductCode = actualConfig.ProductCode,
                        Center = Proceso.Centro,
                        ProcessID = Proceso.Process,
                        TimeID = actualConfig.TimeID,
                        Lot = String.Empty,
                        Reference = String.Empty,
                        ExpireDate = null,
                        //PackID = !IsBox ? String.Empty : Caching.GetPackID(actualConfig.EquipmentID),
                        IsCold = actualConfig.IsCold,
                        Identifier = actualConfig.Identifier,
                        Source = Elaborates.Sources.Memory
                    };

                    var BatchId = await repo.GetRepositoryZ().GetSettingAsync<String>(Settings.Params.BatchID, null);

                    if (!String.IsNullOrEmpty(BatchId))
                    {
                        if (!BatchId.Equals(Salida.BatchID, StringComparison.InvariantCultureIgnoreCase))
                        {
                            //Se actualiza el batch id en pantalla
                            txtViewBatchID.Text = Util.MaskBatchID(Salida.BatchID);
                            //Se actualiza el batch id en la configuracion. 
                            AssignBatchID(Salida.BatchID);
                        }
                    }

                    //Validación de conexión a red
                    if (!HasNetConnection())
                    {
                        ClearSalida();
                        return;
                    }

                    //Validación de producto. 
                    var isProductTypedCorrectly = await ValidateProduct(Proceso, barcode, repoz);

                    if (!isProductTypedCorrectly)
                    {
                        ClearSalida();
                        return;
                    }

                    if (!Proceso.IsPartialElaborateAuthorized)
                    {
                        //Control de entrada y salida de productos. 
                        var resultInputOutput = await ValidateInputOutput(Proceso, barcode, repoz);

                        if (!resultInputOutput)
                        {
                            return;
                        }
                    }

                    //Control de tiempo de salida de productos. 
                    if (actualConfig.units.Any(p => p.Ean == barcode.Ean))
                    {
                        IsBox = true;

                        Salida.PackID = !IsBox ? String.Empty : Caching.GetPackID(actualConfig.EquipmentID);

                        //Actualización de empaque. 
                        if (!String.IsNullOrEmpty(Salida.PackID) && (await IsFinalProcess()))
                        {
                            RunOnUiThread(() =>
                            {
                                linearLayoutEmpaque.Visibility = ViewStates.Visible;
                                txtViewPackageID.Text = Salida.PackID;
                            });
                        }

                        Salida.TrayID = IsBox || Proceso.IsLast ? String.Empty : barcode.ID;

                        var unit = actualConfig.units.First(p => p.Ean == barcode.Ean);
                        bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID, unit.Unit);

                        if (bandeja == null)
                        {
                            dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.OutNoGoodBox), unit.Unit));
                            ClearSalida();
                            return;
                        }
                        else
                        {
                            // Se valida que el tiempo de lectura de salida de producto sea igual o mayor al mínimo establecido
                            // de acuerdo al proceso y tiempo que esté ejecutando.
                            var cumpleConValidacionTiempoSalida = await ValidarTiempoSalida(Proceso.Process, actualConfig.TimeID);
                            if (cumpleConValidacionTiempoSalida)
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.OutGoodTray), CustomDialog.ButtonStyles.NoButton);
                                bandeja.Status = TraysProducts._Status.Vacio;
                                bandeja.Quantity = Util.CtoUnidad(unit, 1, actualConfig.units.Single(p => p.Unit == bandeja.Unit), actualConfig.units);

                                dialog.OnAcceptPress += dialog_OnAcceptPress;
                                dialog.OnCancelPress += dialog_OnCancelPress;

                                return;
                            }
                            else
                            {
                                ClearSalida();

                                //Si el tiempo de lectura es menor al tiempo mínimo establecido del PROCESO Y TIEMPO en ejecución, se muestra un mensaje indicando esta situación
                                ShowTimeElaboratesDialog();

                                return;
                            }
                        }
                    }
                    else if (!Proceso.IsLast)
                    {
                        var bandejas = Caching.bandejas;
                        var configuracionBandeja = new Trays();
                        configuracionBandeja = bandejas.Where(s => s.ID.ToUpper() == barcode.BarCode)
                            .Where(s => barcode.Sequence >= s.Desde)
                            .Where(s => barcode.Sequence <= s.Hasta).SingleOrDefault();

                        if (configuracionBandeja == null)
                        {
                            dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.OutBarSecuenceWrond), editScanSalida.Text));
                            dialog.OnCancelPress += dialog_OnCancelPress;
                            ClearSalida();
                            return;
                        }

                        if (configuracionBandeja != null)
                        {
                            switch (configuracionBandeja.procesarSAP)
                            {
                                case true:
                                    bandeja = await repoz.GetBandejaSalida(barcode.BarCode, (Int16)barcode.Sequence, actualConfig.TimeID);
                                    break;

                                case false://Procesar Bandeja por SQL Server
                                    try
                                    {
                                        bandeja = await repoz.GetEstatusBandeja(barcode.BarCode, barcode.Sequence, actualConfig.TimeID);
                                    }
                                    catch (WebException wEx)
                                    {
                                        ShowWebExceptionDialog(wEx, "Consulta de estatus de bandeja - Salida de Producto");
                                        return;
                                    }
                                    catch (Exception ex)
                                    {
                                        await Util.SaveException(ex);
                                        var errorDialog = new CustomDialog(this, CustomDialog.Status.Error, "Error interno.");
                                        return;
                                    }
                                    break;
                            }

                            if (bandeja == null)
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutBarCodeWrong));
                                dialog.OnCancelPress += dialog_OnCancelPress;
                                ClearSalida();
                                return;
                            }
                            else if (bandeja.Status == TraysProducts._Status.Vacio)
                            {
                                Salida.Unit = bandeja.Unit;
                                Salida.Quantity = bandeja.Quantity;
                                Salida.TrayID = IsBox || Proceso.IsLast ? String.Empty : barcode.ID;

                                // Se valida que el tiempo de lectura de salida de producto sea igual o mayor al mínimo establecido
                                // de acuerdo al proceso y tiempo que esté ejecutando.
                                var cumpleConValidacionTiempoSalida = await ValidarTiempoSalida(Proceso.Process, actualConfig.TimeID);
                                if (cumpleConValidacionTiempoSalida)
                                {
                                    str = GetString(Resource.String.OutGoodTray);
                                    if (Proceso.NeedGramo)
                                        dialog = new CustomDialog(this, CustomDialog.Status.Good, str, CustomDialog.ButtonStyles.TwoButtonWithBoxGramo);
                                    else if (Proceso.IsPartialElaborateAuthorized)
                                    {
                                        var TrayCigarsMaxQuantity = repoz.GetMaxQuantityForPartialElaborates(Proceso.Process, actualConfig.TimeID);
                                        dialog = new CustomDialog(this, CustomDialog.Status.Good, str, CustomDialog.ButtonStyles.TwoButtonWithTrayCantidad, bandeja.TrayID, null, bandeja.Unit, 0, false, TrayCigarsMaxQuantity, actualConfig, Caching.GetProductionDate(), Caching.Stock != null ? Caching.Stock.TurnID : Caching.TurnoID);
                                    }
                                    else
                                        dialog = new CustomDialog(this, CustomDialog.Status.Good, str, CustomDialog.ButtonStyles.NoButton);

                                    dialog.OnAcceptPress += dialog_OnAcceptPress;
                                    dialog.OnCancelPress += dialog_OnCancelPress;

                                    return;
                                }
                                else
                                {
                                    ClearSalida();

                                    //Si el tiempo de lectura es menor al tiempo mínimo establecido del PROCESO Y TIEMPO en ejecución, se muestra un mensaje indicando esta situación
                                    ShowTimeElaboratesDialog();

                                    return;
                                }
                            }

                            else if (bandeja.Status == TraysProducts._Status.Lleno && !IsBox && dialog == null && !JustPrint)
                            {
                                var fecha = bandeja.Fecha.HasValue ? bandeja.Fecha.Value.ToString("dd/MM/yyyy") : String.Empty;
                                string hora;
                                if (bandeja.Fecha.HasValue)
                                {
                                    if (bandeja.Fecha.Value.Kind == DateTimeKind.Utc)
                                    {
                                        hora = bandeja.Fecha.Value.ToLocalTime().ToString("hh:mm tt");
                                    }
                                    else
                                    {
                                        hora = bandeja.Fecha.Value.ToString("hh:mm tt");
                                    }
                                }
                                else
                                {
                                    hora = String.Empty;
                                }
                                //var hora = bandeja.Fecha.HasValue ? bandeja.Fecha.Value.ToString("hh:mm tt") : String.Empty;
                                //dialog = new CustomDialog(this, Proceso._IsDoubleEquipment && !bandeja.EquipmentID.Equals(Proceso.EquipmentID) ? CustomDialog.Status.ErrorBalidateTray : CustomDialog.Status.Error, String.Format(GetString(Resource.String.OutWrongTray), bandeja._ProductCode, bandeja.EquipmentID, fecha, hora));
                                ClearSalida();

                                dialog = new CustomDialog(this, !bandeja.EquipmentID.Equals(Proceso.EquipmentID) ? CustomDialog.Status.Error : CustomDialog.Status.Warning, String.Format(GetString(Resource.String.OutWrongTray), bandeja._TrayID, bandeja._ProductCode, bandeja.EquipmentID, fecha, hora));
                                dialog.ONValidaBandeja += Dialog_ONValidaBandeja;
                                dialog.OnCancelPress += dialog_OnCancelPress;

                                return;
                            }
                        }
                    }
                    else
                    {
                        bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID);

                        str = String.Format(GetString(Resource.String.OutGoodTray2), barcode.FullBarCode, bandeja.Unit);

                        if (editScanSalida.Text.CastToSingle() > 1000 || editScanSalida.Text.CastToSingle() == 0)
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.OutBarCodeDialogNoQuantity), editScanSalida.Text));
                            ClearSalida();
                            return;
                        }

                        Salida.Quantity = barcode.BarCode.CastToSingle();
                        Salida.Unit = bandeja.Unit;
                        Salida.TrayID = IsBox || Proceso.IsLast ? String.Empty : barcode.ID;

                        var listlots = await repoz.GetLotList(Caching.GetProductionDate(), actualConfig.ProductCode);

                        JustPrint = true;

                        if (listlots.Count == 0)
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutBarCodeDialogNoMaterial));
                            ClearSalida();
                            return;
                        }
                        else if (listlots.Count > 1) /// Si Hay mas de un lote le mostramo la lista para que lo seleccione
                        {
                            Dialog lotdialog = null;
                            var lotbuilder = new AlertDialog.Builder(this);
                            lotbuilder.SetTitle(Resource.String.OutBarCodeDialogLot);
                            lotbuilder.SetIcon(Android.Resource.Drawable.IcMenuAgenda);
                            var view = LayoutInflater.Inflate(Resource.Layout.dialog_lots, null);
                            var lstlots = view.FindViewById<ListView>(Resource.Id.lstlots);
                            lstlots.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, listlots.Select(p => p.Lot).ToList());
                            lstlots.ItemClick += (s, Args) =>
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Good, str, CustomDialog.ButtonStyles.TwoButton);
                                dialog.OnAcceptPress += dialog_OnAcceptPress;
                                dialog.OnCancelPress += dialog_OnCancelPress;
                                loteVencimiento = listlots.ElementAt(Args.Position);
                                ValidaVencimiento(ref loteVencimiento);
                                Salida.Lot = loteVencimiento.Lot;
                                Salida.Reference = loteVencimiento.Reference;
                                Salida.ExpireDate = loteVencimiento.Expire;
                                lotdialog.Dismiss();
                                lotdialog.Dispose();
                            };

                            lotbuilder.SetView(view);
                            lotbuilder.SetNegativeButton(Resource.String.Cancel, (se, s) =>
                            {
                                ClearSalida();
                                lotdialog.Dismiss();
                                lotdialog.Dispose();
                            });

                            lotdialog = lotbuilder.Create();

                            RunOnUiThread(() =>
                            {
                                lotdialog.Show();
                            });
                        }
                        else
                        {
                            loteVencimiento = listlots.First();
                            ValidaVencimiento(ref loteVencimiento);
                            Salida.Lot = loteVencimiento.Lot;
                            Salida.Reference = loteVencimiento.Reference;
                            Salida.ExpireDate = loteVencimiento.Expire;

                            dialog = new CustomDialog(this, CustomDialog.Status.Good, str, CustomDialog.ButtonStyles.TwoButton);
                            dialog.OnAcceptPress += dialog_OnAcceptPress;
                            dialog.OnCancelPress += dialog_OnCancelPress;
                            return;
                        }
                    }
                }
                catch(Exception ex)
                {
                    await CatchException(ex);
                }
                finally
                {
                    e.Handled = true;
                }
            }
                                                                                                                                                          }

        private async Task<Boolean> ValidateProduct(ProcessList Proceso, BarCodeResult barcode, RepositoryZ repoz)
        { 
            var isCorrect = false;
            var disconnected = false;            
            
            if (actualConfig.units.Any(p => p.Ean == barcode.Ean))
                {                
                var unit = actualConfig.units.First(p => p.Ean == barcode.Ean);
                var bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID, unit.Unit);
                if (bandeja != null)
                {
                    isCorrect = true;
                }
            }
            else if (!Proceso.IsLast)
            {
                var bandejas = Caching.bandejas;

                var configuracionBandeja = new Trays();

                configuracionBandeja = bandejas.Where(s => s.ID.ToUpper() == barcode.BarCode)
                    .Where(s => barcode.Sequence >= s.Desde)
                    .Where(s => barcode.Sequence <= s.Hasta).SingleOrDefault();

                if (configuracionBandeja != null)
                {
                    if (configuracionBandeja.procesarSAP)
                    {
                        bandeja = await repoz.GetBandejaSalida(barcode.BarCode, (Int16)barcode.Sequence, actualConfig.TimeID);
                    }
                    if (!configuracionBandeja.procesarSAP)//Procesar Bandeja por SQL
                    {
                        try
                        {
                            bandeja = await repoz.GetEstatusBandeja(barcode.BarCode, barcode.Sequence, actualConfig.TimeID);
                        }
                        catch (WebException wEx)
                        {
                            disconnected = true;
                            ShowWebExceptionDialog(wEx, "Validación de producto de bandeja");
                        }
                        catch (Exception ex)
                        {
                            await Util.SaveException(ex);
                            var errorDialog = new CustomDialog(this, CustomDialog.Status.Error, "Error interno.");
                        }
                    }

                    if (bandeja != null && !disconnected)
                    {
                        isCorrect = true;
                    }
                }
            }
            else
            {
                bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID);

                var str = String.Format(GetString(Resource.String.OutGoodTray2), barcode.FullBarCode, bandeja.Unit);

                if (editScanSalida.Text.CastToSingle() > 0 || editScanSalida.Text.CastToSingle() < 1000)
                {
                    isCorrect = true;
                }
            }

            if (!isCorrect && !Proceso.IsLast && !disconnected)
            {
                var _dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutBarCodeWrong));
                ClearSalida();
            }
            if (!isCorrect && Proceso.IsLast && !disconnected)
            {
                new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.OutBarCodeDialogNoQuantity), editScanSalida.Text));
                ClearSalida();
            }

            return isCorrect;
        }

        private async Task<Boolean> ValidateInputOutput(ProcessList Proceso, BarCodeResult barcode, RepositoryZ repoz)
        {
            var resultinout = false;
            //Si es un sub equipo, debe validar el control de salida de acuerdo al consumo de materiales del equipo principal. 

            switch (Proceso.IsSubEquipment)
            {
                case true:
                    if (actualConfig.units.Any(p => p.Ean == barcode.Ean))
                    {
                        var unit = actualConfig.units.First(p => p.Ean == barcode.Ean);
                        var bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID, unit.Unit);
                        if (bandeja != null)
                        {
                            bandeja.Quantity = Util.CtoUnidad(unit, 1, actualConfig.units.Single(p => p.Unit == bandeja.Unit), actualConfig.units);
                            Salida.Quantity = bandeja.Quantity;
                            Salida.Unit = bandeja.Unit;
                            try
                            {
                                resultinout = await repoz.Get_InputOutputControl_ByCigarsConsumed(actualConfig, bandeja.Quantity, Caching.GetProductionDate(), Caching.Stock != null ? Caching.Stock.TurnID : Caching.TurnoID, unit.Unit);
                            }
                            catch (WebException wEx)
                            {
                                ShowWebExceptionDialog(wEx, "Control de entrada y salidas");
                            }
                        }
                        else
                        {
                            var _dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutBarCodeWrong));
                            ClearSalida();
                        }
                    }
                    break;

                default:
                    /*Si no es sub equipo, el control de validación de salida es de 1 a 1, es decir, 1 consumo -> 1 salida. 
                    resultinout = await repoz.Get_Input_Output(actualConfig, false);*/
                    bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID);
                    Salida.Quantity = bandeja.Quantity;
                    Salida.Unit = bandeja.Unit;
                    //Control de salida de acuerdo a cigarros consumidos. 
                    resultinout = await repoz.Get_InputOutputControl_ByCigarsConsumed(actualConfig, bandeja.Quantity, Caching.GetProductionDate(), Caching.Stock != null ? Caching.Stock.TurnID : Caching.TurnoID, bandeja.Unit);
                    break;
            }

            if (!resultinout)
            {
                var _dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.need_new_tray));
                ClearSalida();
            }

            return resultinout;
        }

        private async void editScanSalida_KeyPress_OLD(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editScanSalida.Text) && e.Event.Action == KeyEventActions.Up)
            {

                try
                {
                    RunOnUiThread(() =>
                    {
                        editScanSalida.Enabled = false;
                    });

                    var Proceso = await repo.GetRepositoryZ().GetProces();

                    CustomDialog dialog = null;
                    String str = null;
                    String Batch = null;
                    DateTime? Expire = null;
                    Boolean IsBox = false;
                    Boolean JustPrint = false;

                    var repoz = repo.GetRepositoryZ();

                    var barcode = editScanSalida.Text.GetBarCode();

                    if (Option != CuadreDialog._Options.LAST_TRAY)
                    {
                        var resultinout = false;
                        //Si es un sub equipo, debe validar el control de salida de acuerdo al consumo de materiales del equipo principal. 

                        switch (Proceso.IsSubEquipment)
                        {
                            case true:
                                if (actualConfig.units.Any(p => p.Ean == barcode.Ean))
                                {
                                    var unit = actualConfig.units.First(p => p.Ean == barcode.Ean);
                                    var bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID, unit.Unit);
                                    if (bandeja != null)
                                    {
                                        bandeja.Quantity = Util.CtoUnidad(unit, 1, actualConfig.units.Single(p => p.Unit == bandeja.Unit), actualConfig.units);
                                        try
                                        {
                                            resultinout = await repoz.Get_InputOutputControl_ByCigarsConsumed(actualConfig, bandeja.Quantity, Caching.GetProductionDate(), Caching.TurnoID, unit.Unit);
                                        }
                                        catch (Exception ex)
                                        {
                                            var errorDialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.NoConection));
                                            await Util.SaveException(ex);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        var _dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutBarCodeWrong));
                                        ClearSalida();
                                        return;
                                    }
                                }
                                else
                                {
                                    var _dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutBarCodeWrong));
                                    ClearSalida();
                                    return;
                                }
                                break;

                            default:
                                /*Si no es sub equipo, el control de validación de salida es de 1 a 1, es decir, 1 consumo -> 1 salida. 
                                resultinout = await repoz.Get_Input_Output(actualConfig, false);*/
                                bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID);
                                //Control de salida de acuerdo a cigarros consumidos. 
                                resultinout = await repoz.Get_InputOutputControl_ByCigarsConsumed(actualConfig, bandeja.Quantity, Caching.GetProductionDate(), Caching.TurnoID, bandeja.Unit);
                                break;
                        }
                        if (!resultinout)
                        {
                            var _dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.need_new_tray));
                            ClearSalida();
                            return;
                        }
                    }

                    if (actualConfig.units.Any(p => p.Ean == barcode.Ean))
                    {
                        var unit = actualConfig.units.First(p => p.Ean == barcode.Ean);
                        bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID, unit.Unit);

                        if (bandeja == null)
                        {
                            dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.OutNoGoodBox), unit.Unit));
                            ClearSalida();
                            return;
                        }
                        else
                        {
                            // Se valida que el tiempo de lectura de salida de producto sea igual o mayor al mínimo establecido
                            // de acuerdo al proceso y tiempo que esté ejecutando.
                            var cumpleConValidacionTiempoSalida = await ValidarTiempoSalida(Proceso.Process, actualConfig.TimeID);
                            if (cumpleConValidacionTiempoSalida)
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.OutGoodTray), CustomDialog.ButtonStyles.NoButton);
                                bandeja.Status = TraysProducts._Status.Vacio;
                                bandeja.Quantity = Util.CtoUnidad(unit, 1, actualConfig.units.Single(p => p.Unit == bandeja.Unit), actualConfig.units);

                                dialog.OnAcceptPress += dialog_OnAcceptPress;
                                dialog.OnCancelPress += dialog_OnCancelPress;
                            }
                            else
                            {
                                ClearSalida();

                                //Si el tiempo de lectura es menor al tiempo mínimo establecido del PROCESO Y TIEMPO en ejecución, se muestra un mensaje indicando esta situación
                                ShowTimeElaboratesDialog();

                                return;
                            }
                        }

                        IsBox = true;
                    }
                    else if (!Proceso.IsLast)
                    {
                        var bandejas = Caching.bandejas;
                        var configuracionBandeja = new Trays();
                        configuracionBandeja = bandejas.Where(s => s.ID.ToUpper() == barcode.BarCode)
                            .Where(s => barcode.Sequence >= s.Desde)
                            .Where(s => barcode.Sequence <= s.Hasta).SingleOrDefault();

                        if (configuracionBandeja == null)
                        {
                            dialog = new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.OutBarSecuenceWrond), editScanSalida.Text));
                            dialog.OnCancelPress += dialog_OnCancelPress;
                            ClearSalida();
                            return;
                        }

                        if (configuracionBandeja != null)
                        {
                            if (configuracionBandeja.procesarSAP)
                            {
                                bandeja = await repoz.GetBandejaSalida(barcode.BarCode, (Int16)barcode.Sequence, actualConfig.TimeID);
                            }
                            if (!configuracionBandeja.procesarSAP)//Procesar Bandeja por SQL
                            {
                                try
                                {
                                    bandeja = await repoz.GetEstatusBandeja(barcode.BarCode, barcode.Sequence, actualConfig.TimeID);
                                }
                                catch (WebException wEx)
                                {
                                    await Util.SaveException(wEx);
                                    var errorDialog = new CustomDialog(this, CustomDialog.Status.Error, "Error de comunicación.");
                                    return;
                                }
                                catch (Exception ex)
                                {
                                    await Util.SaveException(ex);
                                    var errorDialog = new CustomDialog(this, CustomDialog.Status.Error, "Error interno.");
                                    return;
                                }
                            }
                            if (bandeja == null)
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutBarCodeWrong));
                                dialog.OnCancelPress += dialog_OnCancelPress;
                                ClearSalida();
                                return;
                            }
                            else if (bandeja.Status == TraysProducts._Status.Vacio)
                            {

                                // Se valida que el tiempo de lectura de salida de producto sea igual o mayor al mínimo establecido
                                // de acuerdo al proceso y tiempo que esté ejecutando.
                                var cumpleConValidacionTiempoSalida = await ValidarTiempoSalida(Proceso.Process, actualConfig.TimeID);
                                if (cumpleConValidacionTiempoSalida)
                                {
                                    str = GetString(Resource.String.OutGoodTray);
                                    if (Proceso.NeedGramo)
                                        dialog = new CustomDialog(this, CustomDialog.Status.Good, str, CustomDialog.ButtonStyles.TwoButtonWithBoxGramo);
                                    else
                                        dialog = new CustomDialog(this, CustomDialog.Status.Good, str, CustomDialog.ButtonStyles.NoButton);

                                    dialog.OnAcceptPress += dialog_OnAcceptPress;
                                    dialog.OnCancelPress += dialog_OnCancelPress;
                                }
                                else
                                {
                                    ClearSalida();

                                    //Si el tiempo de lectura es menor al tiempo mínimo establecido del PROCESO Y TIEMPO en ejecución, se muestra un mensaje indicando esta situación
                                    ShowTimeElaboratesDialog();

                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        bandeja = await repoz.GetBandejaConfig(Proceso.Process, actualConfig.TimeID);

                        str = String.Format(GetString(Resource.String.OutGoodTray2), barcode.FullBarCode, bandeja.Unit);

                        if (editScanSalida.Text.CastToSingle() > 1000 || editScanSalida.Text.CastToSingle() == 0)
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, String.Format(GetString(Resource.String.OutBarCodeDialogNoQuantity), editScanSalida.Text));
                            ClearSalida();
                            return;
                        }

                        var listlots = await repoz.GetLotList(Caching.GetProductionDate(), actualConfig.ProductCode);

                        JustPrint = true;

                        if (listlots.Count == 0)
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutBarCodeDialogNoMaterial));
                            return;
                        }
                        else if (listlots.Count > 1) /// Si Hay mas de un lote le mostramo la lista para que lo seleccione
                        {
                            Dialog lotdialog = null;
                            var lotbuilder = new AlertDialog.Builder(this);
                            lotbuilder.SetTitle(Resource.String.OutBarCodeDialogLot);
                            lotbuilder.SetIcon(Android.Resource.Drawable.IcMenuAgenda);
                            var view = LayoutInflater.Inflate(Resource.Layout.dialog_lots, null);
                            var lstlots = view.FindViewById<ListView>(Resource.Id.lstlots);
                            lstlots.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, listlots.Select(p => p.Lot).ToList());
                            lstlots.ItemClick += (s, Args) =>
                            {
                                dialog = new CustomDialog(this, CustomDialog.Status.Good, str, CustomDialog.ButtonStyles.TwoButton);
                                dialog.OnAcceptPress += dialog_OnAcceptPress;
                                dialog.OnCancelPress += dialog_OnCancelPress;
                                loteVencimiento = listlots.ElementAt(Args.Position);
                                ValidaVencimiento(ref loteVencimiento);
                                Salida.Lot = loteVencimiento.Lot;
                                Salida.ExpireDate = loteVencimiento.Expire;
                                lotdialog.Dismiss();
                                lotdialog.Dispose();
                            };

                            lotbuilder.SetView(view);
                            lotbuilder.SetNegativeButton(Resource.String.Cancel, (se, s) =>
                            {
                                ClearSalida();
                                lotdialog.Dismiss();
                                lotdialog.Dispose();
                            });

                            lotdialog = lotbuilder.Create();

                            RunOnUiThread(() =>
                            {
                                lotdialog.Show();
                            });
                        }
                        else
                        {
                            dialog = new CustomDialog(this, CustomDialog.Status.Good, str, CustomDialog.ButtonStyles.TwoButton);
                            dialog.OnAcceptPress += dialog_OnAcceptPress;
                            dialog.OnCancelPress += dialog_OnCancelPress;
                            loteVencimiento = listlots.First();
                            ValidaVencimiento(ref loteVencimiento);
                        }
                    }

                    Salida = new Elaborates()
                    {
                        EquipmentID = actualConfig.EquipmentID,
                        Produccion = Caching.GetProductionDate(),
                        Fecha = DateTime.Now,
                        Logon = Proceso.Logon,
                        Sync = true,
                        SyncSQL = true,
                        VerID = actualConfig.VerID,
                        TrayID = IsBox || Proceso.IsLast ? String.Empty : barcode.ID,
                        SubEquipmentID = actualConfig.SubEquipmentID,
                        TurnID = Caching.Stock.TurnID,
                        BatchID = await Routes.GetBatchID(actualConfig.EquipmentID, Caching.Stock.TurnID, Caching.GetProductionDate(), actualConfig.TimeID),
                        Unit = bandeja.Unit,
                        Quantity = !Proceso.IsLast ? bandeja.Quantity : barcode.BarCode.CastToSingle(),
                        ProductCode = actualConfig.ProductCode,
                        Center = Proceso.Centro,
                        ProcessID = Proceso.Process,
                        TimeID = actualConfig.TimeID,
                        Lot = loteVencimiento?.Lot ?? String.Empty,
                        Reference = loteVencimiento?.Reference ?? String.Empty,
                        ExpireDate = loteVencimiento?.Expire ?? null,
                        PackID = !IsBox ? String.Empty : Caching.GetPackID(actualConfig.EquipmentID),
                        IsCold = actualConfig.IsCold,
                        Identifier = actualConfig.Identifier,
                        Source = Elaborates.Sources.Memory
                    };

                    var BatchId = await repo.GetRepositoryZ().GetSettingAsync<String>(Settings.Params.BatchID, null);

                    if (!String.IsNullOrEmpty(BatchId))
                    {
                        if (!BatchId.Equals(Salida.BatchID, StringComparison.InvariantCultureIgnoreCase))
                        {
                            //Se actualiza el batch id en pantalla
                            txtViewBatchID.Text = Util.MaskBatchID(Salida.BatchID);
                            //Se actualiza el batch id en la configuracion. 
                            AssignBatchID(Salida.BatchID);
                        }
                    }

                    if (!String.IsNullOrEmpty(Salida.PackID) && (await IsFinalProcess()))
                    {
                        RunOnUiThread(() =>
                        {
                            linearLayoutEmpaque.Visibility = ViewStates.Visible;
                            txtViewPackageID.Text = Salida.PackID;
                        });
                    }

                    if (Option != CuadreDialog._Options.NONE)
                    {
                        var fechaturno = Caching.Stock.Begin;
                        Salida.Produccion = new DateTime(fechaturno.Year, fechaturno.Month, fechaturno.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        Salida.CustomFecha = Convert.ToInt32(Salida.Produccion.GetDBDate());

                        RunOnUiThread(() =>
                        {
                            txtViewUnidadSalida.Text = bandeja.Unit;
                            editScanSalida.Enabled = false;
                        });
                    }
                    else if (!IsBox && dialog == null && !JustPrint)
                    {
                        var fecha = bandeja.Fecha.HasValue ? bandeja.Fecha.Value.ToString("dd/MM/yyyy") : String.Empty;
                        string hora;
                        if (bandeja.Fecha.HasValue)
                        {
                            if (bandeja.Fecha.Value.Kind == DateTimeKind.Utc)
                            {
                                hora = bandeja.Fecha.Value.ToLocalTime().ToString("hh:mm tt");
                            }
                            else
                            {
                                hora = bandeja.Fecha.Value.ToString("hh:mm tt");
                            }
                        }
                        else
                        {
                            hora = String.Empty;
                        }
                        //var hora = bandeja.Fecha.HasValue ? bandeja.Fecha.Value.ToString("hh:mm tt") : String.Empty;
                        //dialog = new CustomDialog(this, Proceso._IsDoubleEquipment && !bandeja.EquipmentID.Equals(Proceso.EquipmentID) ? CustomDialog.Status.ErrorBalidateTray : CustomDialog.Status.Error, String.Format(GetString(Resource.String.OutWrongTray), bandeja._ProductCode, bandeja.EquipmentID, fecha, hora));
                        dialog = new CustomDialog(this, !bandeja.EquipmentID.Equals(Proceso.EquipmentID) ? CustomDialog.Status.Error : CustomDialog.Status.Warning, String.Format(GetString(Resource.String.OutWrongTray), bandeja._TrayID, bandeja._ProductCode, bandeja.EquipmentID, fecha, hora));
                        dialog.ONValidaBandeja += Dialog_ONValidaBandeja;
                        dialog.OnCancelPress += dialog_OnCancelPress;
                    }

                    RunOnUiThread(() =>
                    {
                        editScanSalida.Text = String.Empty;
                        editScanSalida.Enabled = true;
                    });

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

        private async void Dialog_ONValidaBandeja()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();

                var result = await repoz.ValidaBandejaEnLinea(bandeja);

                switch (result.Result)
                {
                    case TrayProductRoute._Status.Comunicacion:
                        new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutWrongTray));
                        return;

                    case TrayProductRoute._Status.Correcto:

                        if (result.Bandeja.Status == TraysProducts._Status.Lleno)
                        {
                            if (!result.Bandeja.ProductCode.Equals(actualConfig.ProductCode))
                            {
                                var _dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutWrongTray));
                                _dialog.OnCancelPress += dialog_OnCancelPress;
                                return;
                            }
                            else
                            {
                                var _dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.OutGoodTray), CustomDialog.ButtonStyles.NoButton);
                                _dialog.OnAcceptPress += dialog_OnAcceptPress;
                            }
                        }
                        else
                        {
                            var _dialog = new CustomDialog(this, CustomDialog.Status.Good, GetString(Resource.String.OutGoodTray), CustomDialog.ButtonStyles.NoButton);
                            _dialog.OnAcceptPress += dialog_OnAcceptPress;
                        }

                        if (result.Entrada.BatchID != txtViewBatch.Text)
                        {
                            Salida.BatchID = result.Entrada.BatchID;
                            AssignBatchID(result.Entrada.BatchID);
                        }

                        if (Routes.Ruta.UniqueKey != result.Traza.UniqueKey)
                        {
                            Routes.SetTraza(result.Traza);
                        }

                        break;

                    case TrayProductRoute._Status.Incorrecto:
                        var dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.OutBarCodeWrong));
                        dialog.OnCancelPress += dialog_OnCancelPress;

                        break;
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private void dialog_OnCancelPress()
        {
            ClearSalida();
        }

        private async void dialog_OnAcceptPress(Boolean IsCantidad, Single Box, Single Cantidad)
        {
            try
            {
                if (HasNetConnection()) //Verificando conexión wifi
                {
                    await SaveSalida(Box, Cantidad);
                    if (Option == CuadreDialog._Options.LAST_TRAY) CierraCuadre(Caching.Stock);
                    ClearSalida();
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        /// <summary>
        /// Método para verificar que existe conexión para la sincronización en línea con SQL
        /// </summary>
        /// <returns></returns>
        private bool HasNetConnection()
        {
            CustomDialog dialog = null;
            if (!serviceConnection.binder.Service.IsConnected)
            {
                switch (serviceConnection.binder.Service.DisconectionType)
                {
                    case SyncService.DisconectionsType.WifiOff:
                        RunOnUiThread(() =>
                        {
                            dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.AlertErrorNoWifi));
                        });
                        return false;

                    case SyncService.DisconectionsType.ServerDown:
                        RunOnUiThread(() =>
                        {
                            dialog = new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.AlertErrorServerDown));
                        });
                        return false;
                }
            }
            return true;
        }

        private async Task<Boolean> ValidarTiempoSalida(String idProceso, String idTiempo)
        {
            var cumpleConValidacionTiempoSalida = true;
            try
            {
                var repositorioSalidas = repo.GetRepositoryElaborates();
                var repoz = repo.GetRepositoryZ();
                var ultimaSalida = new Elaborates();
                var configuracionTiempoSalida = new ConfiguracionTiempoSalida();
                var listaConfiguracionesTiempoSalida = Caching.ConfiguracionesTiempoSalida;
                var segundosLectura = 0.0;

                if (listaConfiguracionesTiempoSalida != null)
                {
                    //Configuración de tiempo de salida de acuerdo al proceso Y tiempo que esté ejecutándose. 
                    configuracionTiempoSalida = listaConfiguracionesTiempoSalida.Where(s => s.idProceso.Trim().Equals(idProceso, StringComparison.CurrentCultureIgnoreCase))
                    .Where(s => s.idTiempo.Trim().Equals(idTiempo, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                    // Salida de producto más reciente 
                    ultimaSalida = await repoz.GetLastSalidaAsync();

                    if (configuracionTiempoSalida != null && ultimaSalida != null)
                    {
                        //Segundos de lectura desde la última salida hasta la actual. 
                        segundosLectura = Math.Abs(ultimaSalida._Fecha.Subtract(DateTime.Now).TotalSeconds);

                        if (idProceso.Trim().Equals(configuracionTiempoSalida.idProceso, StringComparison.InvariantCultureIgnoreCase)
                            && idTiempo.Trim().Equals(configuracionTiempoSalida.idTiempo, StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (segundosLectura < configuracionTiempoSalida.tiempoMinimo
                                && configuracionTiempoSalida.unidadTiempo.Trim().Equals("Seg", StringComparison.InvariantCulture))
                            {
                                cumpleConValidacionTiempoSalida = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                await CatchException(e);
            }
            return cumpleConValidacionTiempoSalida;
        }


        /// FUNCIONALIDAD CREADA POR RALDY PARA CONTROLAR EL TIEMPO DE CONSUMO DE BANDEJAS EN PMB
        /// Creado el 14-02-2023
        /// Actualizado el 17-02-2023
        /// <param name="idProceso"></param>
        /// <param name="idTiempo"></param>
        /// <returns> cumpleConValidacionTiempoConsumo </returns>
        private async Task<Boolean> ValidarTiempoConsumo(String idProceso, String idTiempo)
        {
            var cumpleConValidacionTiempoConsumo = true;
            try
            {
                var repositorioConsumos = repo.GetRepositoryConsumptions();
                var repoz = repo.GetRepositoryZ();
                var ultimoConsumo = new Consumptions();
                var configuracionTiempoConsumo = new ConfiguracionTiempoConsumo();
                var listaConfiguracionesTiempoConsumo = Caching.ConfiguracionesTiempoConsumo; 
                var segundosConsumo = 0.0; 
               

                if (listaConfiguracionesTiempoConsumo != null)
                {
                    //Configuración de tiempo de consumo de acuerdo al proceso Y tiempo que esté ejecutándose. 
                    configuracionTiempoConsumo = listaConfiguracionesTiempoConsumo.Where(s => s.idProceso.Trim().Equals(idProceso, StringComparison.CurrentCultureIgnoreCase))
                    .Where(s => s.idTiempo.Trim().Equals(idTiempo, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                    // Consumo de producto más reciente 
                    ultimoConsumo = await repoz.GetLastConsumoAsync(); 
                    
                    if (configuracionTiempoConsumo != null && ultimoConsumo != null)
                    {
                        //Segundos de lectura desde el último consumo hasta la actual.                         
                        DateTime now = DateTime.Now;
                        var lastC = ultimoConsumo._Fecha.ToLocalTime();
                        segundosConsumo = now.Subtract(lastC).TotalSeconds;
                       
                        if (idProceso.Trim().Equals(configuracionTiempoConsumo.idProceso, StringComparison.InvariantCultureIgnoreCase)
                            && idTiempo.Trim().Equals(configuracionTiempoConsumo.idTiempo, StringComparison.InvariantCultureIgnoreCase))
                        {                            
                            if (segundosConsumo < configuracionTiempoConsumo.tiempoMinimo
                                && configuracionTiempoConsumo.unidadTiempo.Trim().Equals("Seg", StringComparison.InvariantCulture))
                            {
                                cumpleConValidacionTiempoConsumo = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                await CatchException(e);
            }
            return cumpleConValidacionTiempoConsumo;
        }

        public void ShowTimeElaboratesDialog()
        {
            //Si el tiempo de lectura es menor al tiempo mínimo establecido del PROCESO Y TIEMPO en ejecución, se muestra un mensaje indicando esta situación

            RunOnUiThread(async () =>
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();
                var mensaje = "";
                switch (Proceso.IsSubEquipment)
                {
                    case true:
                        mensaje = "Continúe con el empaque de acuerdo al procedimiento establecido.";
                        break;

                    case false:
                        mensaje = "Favor seguir el Procedimiento de lectura de bandejas.";
                        break;
                }
                var dialogTiempoSalidas = new CustomDialog(this, CustomDialog.Status.Error, String.Format(mensaje));
            });
        }

        public void ShowTimeConsumptionsDialog()
        {
            //Si el tiempo de lectura es menor al tiempo mínimo establecido del PROCESO Y TIEMPO en ejecución, se muestra un mensaje indicando esta situación

            RunOnUiThread(async () =>
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();
                var mensaje = "Continúe con el consumo  de acuerdo al procedimiento establecido.";               
                var dialogTiempoSalidas = new CustomDialog(this, CustomDialog.Status.Error, String.Format(mensaje));
            });
        }

        private async Task SaveSalida(Single Peso, Single Cantidad = 0)
        {
            try
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();
                var repoSa = repo.GetRepositoryElaborates();

                if (Option != CuadreDialog._Options.LAST_TRAY)
                    Salida.CustomID = await SequenceManager.AddSalida();
                else
                    Salida.CustomID = await SequenceManager.AddSalida(Caching.Stock.Begin);

                if (!Proceso.IsLast)
                {
                    Salida.Peso = Peso;

                    var barcode = Salida.TrayID.GetBarCode();
                    var usuario = SecurityManager.CurrentProcess != null ? SecurityManager.CurrentProcess.Logon : Proceso.Logon;

                    if (Proceso.IsPartialElaborateAuthorized && Cantidad > 0.0)
                    {
                        //Guardado de usuario que autorizó la salida parcial
                        Salida.Quantity = Cantidad;
                        Salida.Logon = usuario;
                    }
                    
                    if (barcode.Sequence != 0)//Validación de si es una bandeja 
                    {
                        if (!await ProcessOutputTrayProductAsync(barcode, usuario))
                        {
                            ClearSalida();
                            return;
                        }
                    }
                }

                if (SyncService.IsRunning(this))
                    Routes.IsThereConnection = serviceConnection.binder.Service.IsConnected;
                else
                    Routes.IsThereConnection = false;

                if (!String.IsNullOrEmpty(Salida.PackID))
                {
                    Salida.PackSequence = await repo.GetRepositoryZ().GetNextSequenceAsync(Salida.Produccion, Salida.TurnID, Salida.EquipmentID);
                }

                await repoSa.InsertAsync(Salida);
                var traza = await Routes.WriteOutput(Salida);

                if (!String.IsNullOrEmpty(Salida.PackID) && Salida.PackSequence != 0 && (actualConfig.Producto == Times.ProductTypes.Validar_Salida_y_Tipo_Almacenamiento || actualConfig.Producto == Times.ProductTypes.Validar_Tipo_Almacenamiento))
                {
                    PrintEtiquetaSuplidor(this, Salida, traza, actualConfig.Copias);
                }
                else if (Proceso.IsLast)
                {
                    await Task.Run(() =>
                    {
                        PrintLabelAsync(this);
                    });
                }
                
                if (Proceso.IsPartialElaborateAuthorized) 
                {
                    //Desactivación automática de salida parcial
                    var repoSetting = repo.GetRepositorySettings();
                    await repoSetting.InsertOrReplaceAsync(new Settings()
                    {
                        Key = Settings.Params.IsPartialElaborateAuthorized,
                        Value = false.ToString()
                    });
                   
                    //Cambio de titulo de pantalla de salida de producto para indicarle al usuario que la proxima salida sera normal. 
                    txtViewSalidaTitle.Text = GetString(Resource.String.Out);
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async Task<bool> ProcessInputTrayProductAsync(ProcessList Proceso)
        {
            var wasTrayProductProcessed = false;

            try
            {
                var repoc = repo.GetRepositoryConsumptions();
                var repob = repo.GetRepositoryTraysProducts();
                var repoTray = repo.GetRepositoryTrays();
                TraysProducts[] traysProducts = new TraysProducts[] { consumo.Bandeja };
                ShowProgress(true, Resource.String.UpdatingBandejaSql);
                var configuracionBandeja = await repoTray.GetAsyncByKey(traysProducts[0].TrayID.ToString());
                if (configuracionBandeja != null)
                {
                    if (configuracionBandeja.procesarSAP)
                    {
                        consumo.Bandeja.Sync = true;
                        await repob.InsertOrReplaceAsync(consumo.Bandeja);
                        await repoc.InsertAsync(consumo);
                        wasTrayProductProcessed = true;
                    }
                    else
                    {
                        consumo.Bandeja.Sync = false;
                        try
                        {
                            await repob.InsertOrUpdateAsyncSql(traysProducts); //Guardando datos en línea con SQL
                            wasTrayProductProcessed = true;
                        }
                        catch (WebException wEx)
                        {
                            try
                            {
                                await Task.Delay(5000);
                                var estatusBandeja = await repo.GetRepositoryZ().GetEstatusBandeja(consumo.Bandeja.TrayID, consumo.Bandeja.Secuencia);
                                if (estatusBandeja.Status == TraysProducts._Status.Vacio)//Si la bandeja está vacía, indica que la petición web anterior fue procesada.
                                {
                                    wasTrayProductProcessed = true;
                                }
                            }
                            catch (Exception ex1)
                            {
                                await Util.SaveException(ex1, "Consulta de estatus de bandeja en proceso de Consumo", false);
                            }
                            if (!wasTrayProductProcessed)
                            {
                                ShowWebExceptionDialog(wEx, "Consumo de bandeja");
                            }
                        }
                        catch (Exception e)
                        {
                            CustomDialog errorDialog = new CustomDialog(this, CustomDialog.Status.Error, "Error interno.");
                            await Util.SaveException(e, "Consumo de bandejas", false);
                        }
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
            return wasTrayProductProcessed;
        }

        private async Task<bool> ProcessOutputTrayProductAsync(BarCodeResult barcode, string usuario)
        {
            var wasTrayProductProcessed = false;

            var trayProduct = new TraysProducts()
            {
                ID = barcode.ID,
                TrayID = barcode.BarCode,
                Status = TraysProducts._Status.Lleno,
                Secuencia = barcode.Sequence,
                ProductCode = Salida.ProductCode,
                VerID = Salida.VerID,
                TimeID = Salida.TimeID,
                Quantity = Salida.Quantity,
                Unit = Salida.Unit,
                BatchID = Salida.BatchID,
                EquipmentID = Salida.EquipmentID,
                Fecha = Salida.Produccion,
                UsuarioLlenada = usuario,
                ElaborateID = (Int16)Salida.CustomID,
                Sync = true, //Guseppe 15-10-19, desactivar para no sincronizar con SAP   
                ModifyDate = DateTime.Now
            };

            try
            {
                TraysProducts[] traysProducts = new TraysProducts[] { trayProduct };

                ShowProgress(true, Resource.String.UpdatingBandejaSql);
                var repoTray = repo.GetRepositoryTrays();
                var repoBanP = repo.GetRepositoryTraysProducts();
                var configuracionBandeja = await repoTray.GetAsyncByKey(traysProducts[0].TrayID.ToString());
                if (configuracionBandeja != null)
                {
                    if (configuracionBandeja.procesarSAP)
                    {
                        trayProduct.Sync = true;
                        await repoBanP.InsertOrReplaceAsync(trayProduct);
                        wasTrayProductProcessed = true;
                    }
                    else
                    {
                        trayProduct.Sync = false;
                        try
                        {
                            await repoBanP.InsertOrUpdateAsyncSql(traysProducts); //Guardando datos en línea con SQL Server
                            wasTrayProductProcessed = true;
                        }
                        catch (WebException wEx)  //System.Net.WebException => no wifi y connection timeout
                        {
                            try
                            {
                                await Task.Delay(5000);
                                var estatusBandeja = await repo.GetRepositoryZ().GetEstatusBandeja(barcode.BarCode, barcode.Sequence);
                                if (estatusBandeja.Status == TraysProducts._Status.Lleno && estatusBandeja.ProductCode.Equals(trayProduct.ProductCode)
                                    && estatusBandeja.BatchID.Equals(trayProduct.BatchID))//Si la bandeja está LLENA, indica que la petición anterior fue procesada.
                                {
                                    wasTrayProductProcessed = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                await Util.SaveException(ex, "Consulta de estado de bandeja en proceso de Salida.", false);
                            }
                            if (!wasTrayProductProcessed)
                            {
                                RunOnUiThread(() =>
                                {
                                    ShowWebExceptionDialog(wEx, "Salida de bandeja");
                                });
                            }
                        }
                        catch (Exception e)
                        {
                            RunOnUiThread(() =>
                            {
                                CustomDialog errorDialog = new CustomDialog(this, CustomDialog.Status.Error, "Error de servidor.");
                            });
                            await Util.SaveException(e, "Salida de bandejas");
                        }
                    }
                }

                //RunOnUiThread(() =>
                //{
                //    CustomDialog dialogUpdated = new CustomDialog(this, CustomDialog.Status.Good, validationMessage);
                //});
            }
            catch (System.OperationCanceledException ex)
            {
                RunOnUiThread(() =>
                {
                    CustomDialog dialogNoValid = new CustomDialog(this, CustomDialog.Status.Error, ex.Message);
                });
                await Util.SaveException(ex);
            }
            finally
            {
                ShowProgress(false);
            }
            return wasTrayProductProcessed;
        }

        private async void PrintLabelAsync(Context context)
        {
            try
            {
                var lista = new List<Etiquetas>();

                var material = Caching.Materials.Single(p => p.MaterialCode == loteVencimiento.MaterialCode);

                var etiqueta = new Etiquetas()
                {
                    Codigo = actualConfig.ProductReference,
                    Descripcion = actualConfig.ProductName,
                    Unidad = txtViewUnidadSalida.Text,
                    Material = actualConfig._ProductCode,
                    LoteInterno = loteVencimiento.Lot,
                    LoteSuplidor = loteVencimiento.Reference,
                    Medida = (Decimal)Salida.Quantity,
                    Cantidad = 1
                };

                if (loteVencimiento.Expire.Value.Year > 2000)
                {
                    etiqueta.Fecha = loteVencimiento.Expire.Value.ToLocalTime();
                }

                lista.Add(etiqueta);
                var printer = PrinterManager.GetUniqueInstance();
                await printer.ExecutePrint(context, lista);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }
        
        private async void PrintEtiquetaSuplidor(Context context, Elaborates salida, ProductsRoutes traza, Byte Copias, Boolean Reimpresion = false, int IdMotivoReimpresion = 0)
        {
            try
            {
                var MacAddress = await repo.GetRepositoryZ().GetSettingAsync<String>(Settings.Params.PrinterAddress, null);

                if (String.IsNullOrEmpty(MacAddress))
                {
                    RunOnUiThread(() =>
                    {
                        imgPrinterStatus.Visibility = ViewStates.Gone;
                    });
                    return;
                }

                await Task.Run(async () =>
                {
                    var printer = PrinterManager.GetUniqueInstance();
                    if (!Reimpresion)
                    {
                        await printer.PrintLastEtiqueta(context, salida, traza, Copias);
                    }
                    else
                    {
                        var copiasImpresiones = (byte)(Copias - 1);
                        await printer.PrintLastEtiqueta(context, salida, traza, copiasImpresiones);
                        if (printer.LastStatus == PrinterManager._PrinterStatus.Connected)
                        {
                            //Se guarda el registro de reimpresion después de confirmar que su ultimo estatus es conectado.  
                            SaveLabelPrintingLog(salida, traza, Copias, IdMotivoReimpresion, SecurityManager.CurrentProcess.Logon);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void btnCancelSalida_Click(object sender, EventArgs e)
        {
            try
            {
                OnBackPressed();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private void ValidaVencimiento(ref ElaborateTotal lote)
        {
            if (lote.Expire.HasValue && lote.Expire.Value.Year > 2000)
                return;

            var materialcode = lote.MaterialCode;
            var lot = lote.Lot;

            var material = Caching.Materials.Single(p => p.MaterialCode == materialcode);

            var barcode = String.Format("{0}-{1}-1", material.MaterialReference, lot).GetBarCode();

            if (barcode.Fecha.HasValue)
            {
                lote.Expire = barcode.Fecha.Value.AddDays(material.ExpireDay).ToUniversalTime();
            }
        }

        #endregion

        #region Método de Activación de Configuración

        private void btnCancelActivate_Click(object sender, EventArgs e)
        {
            SetScreen(LScreens.Choose);
        }

        private async void btnSalida_Click(object sender, EventArgs e)
        {
            try
            {
                var Proceso = await repo.GetRepositoryZ().GetProces();

                if (!String.IsNullOrEmpty(actualConfig.SubEquipmentID) && !Proceso.IsSubEquipment)
                {
                    new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.DrawerChangeErrorMessageNoAvailable));
                    return;
                }

                var repoTimes = repo.GetRepositoryTimes();
                var repoz = repo.GetRepositoryZ();

                var Time = await repoTimes.GetAsyncByKey(actualConfig.TimeID);

                var setting = await repoz.GetSettingAsync<Boolean>(Settings.Params.AllComponentAreInside, false);

                if (Time.Valid_Out && !setting)
                {
                    var lacktrack = await repoz.Get_Lack_Material(actualConfig.ProductCode, actualConfig.VerID, Time);

                    var count = lacktrack.Count(p => (!p.IsObligatory && !String.IsNullOrEmpty(p.Lot) || p.IsObligatory && !String.IsNullOrEmpty(p.BatchID)) && !p.Group.Equals(Time.Group));

                    var bomdialog = new BomDialog(this);

                    if (Time.Min > 0 && Time.Min > count)
                    {
                        bomdialog.ShowDialog(lacktrack, false);
                    }
                    else
                    {
                        bomdialog.ShowDialog(lacktrack, true);
                    }

                    bomdialog.OnShot += async () =>
                    {
                        var repoSett = repo.GetRepositorySettings();
                        await repoSett.InsertOrReplaceAsync(new Settings()
                        {
                            Key = Settings.Params.AllComponentAreInside,
                            Value = "true"
                        });

                        if (!String.IsNullOrEmpty(actualConfig.SubEquipmentID) && !Proceso.IsSubEquipment)
                        {
                            new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.DrawerChangeErrorMessageNoAvailable));
                            return;
                        }

                        SetScreen(LScreens.ScanOutput);
                    };
                }
                else
                {
                    SetScreen(LScreens.ScanOutput);
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void editScan_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editScan.Text) && e.Event.Action == KeyEventActions.Down)
            {
                try
                {
                    var exist = Caching.Configs.FirstOrDefault(p => p._ProductCode == editScan.Text);

                    if (exist == null)
                    {
                        var dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.ChangeProductoWrong));
                        editScan.Text = String.Empty;
                    }
                    else if (exist != null && actualConfig != null && exist._ProductCode == actualConfig._ProductCode)
                    {
                        var dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.ChangeProductoWorking));
                        editScan.Text = String.Empty;
                    }
                    else if (exist != null && exist._ProductCode != nextconfig._ProductCode)
                    {
                        var dialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.ChangeProductoNoNext));
                        editScan.Text = String.Empty;
                    }
                    else
                    {
                        var minutes = exist.Begin.ToLocalTime().Subtract(DateTime.Now).TotalMinutes;

                        if (minutes >= 30)
                        {
                            var msg = GetString(Resource.String.ChangeProductoSoon);
                            var Soondialog = new CustomDialog(this, CustomDialog.Status.Warning, String.Format(msg, exist.ProductShort ?? exist._ProductCode, exist.Begin.ToLocalTime().ToString("MMM dd"), exist.Begin.ToLocalTime().ToString("hh:mm tt")), CustomDialog.ButtonStyles.TwoButton);
                            Soondialog.OnCancelPress += Soondialog_OnCancelPress;
                            Soondialog.OnAcceptPress += Soondialog_OnAcceptPress;
                            return;
                        }

                        Soondialog_OnAcceptPress(false, 0, 0);
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

        private async void Soondialog_OnAcceptPress(Boolean IsCantidad, float Box, Single Cantidad)
        {
            try
            {
                editScan.Enabled = false;
                editScanEquipment.Enabled = true;
                editScanEquipment.RequestFocus();
                SequenceManager.CleanSequences();
                Routes.Clean();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private void Soondialog_OnCancelPress()
        {
            editScan.Text = String.Empty;
        }

        private void btnEntrada_Click(object sender, EventArgs e)
        {
            //if (Proceso.IsSubEquipment)
            //{
            //    new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.DrawerChangeErrorMessageNoAvailable));
            //    return;
            //}

            SetScreen(LScreens.ScanInput);
        }

        private async void editScanEquipment_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editScanEquipment.Text) && e.Event.Action == KeyEventActions.Down)
            {
                try
                {
                    var Proceso = await repo.GetRepositoryZ().GetProces();

                    if (Proceso.EquipmentID == editScanEquipment.Text)
                    {
                        var repoz = repo.GetRepositoryZ();
                        var repos = repo.GetRepositorySettings();
                        var repoT = repo.GetRepositoryTimes();
                        var repoConfig = repo.GetRepositoryConfigs();
                        String Logon = String.Empty;

                        if (SecurityManager.CurrentProcess != null)
                        {
                            Logon = SecurityManager.CurrentProcess.Logon;
                        }
                        else
                        {
                            Logon = Proceso.Logon;
                        }
                        var config = await repoConfig.GetAsyncByKey(nextconfig.ConfigID);
                        nextconfig.Identifier = config.Identifier;
                        var dialog = new TipoAlmacenamientoDialog(this, repo, nextconfig);
                        dialog.OnStoreType += async (Boolean IsCold, String ProductType, String Identifier) =>
                        {
                            if (actualConfig != null)
                            {
                                await repoz.UpdateConfigStatusAsync(actualConfig.ConfigID, Configs._Status.Completed, Logon, false, Identifier, ProductType, Proceso.IsSubEquipment);
                                actualConfig = null;
                            }

                            await repoz.UpdateConfigStatusAsync(nextconfig.ConfigID, Configs._Status.Enabled, Logon, IsCold, Identifier, ProductType, Proceso.IsSubEquipment);

                            var sett = new Settings()
                            {
                                Key = Settings.Params.ConfigIsActive,
                                Value = "true"
                            };

                            Routes.Clean();
                            Caching.Clean();
                            SequenceManager.CleanSequences();
                            AssignBatchID(String.Empty);
                            await repos.InsertOrReplaceAsync(sett);
                            SetScreen(LScreens.Choose);
                            LoadActualConfig(true);
                        };

                        var tiempo = await repoT.GetAsyncByKey(nextconfig.TimeID);
                        dialog.ShowDialogAsync(tiempo);
                    }
                    else
                    {
                        editScanEquipment.Text = String.Empty;
                        new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.ChangeProductoWrongEquipment));
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

        private async void btnConsultas_Click(object sender, EventArgs e)
        {
            try
            {
                SetScreen(LScreens.ReportOperator);

                if (_reportEntries == null)
                {
                    _reportEntries = new List<ReportEntry>
                    {
                        new ReportEntry{ Type = ReportEntry.EntryTypes.Entrada, Imagen = Resource.Drawable.ic_entrada, Title = GetString(Resource.String.ReportTitleEntrada)  },
                        new ReportEntry{ Type = ReportEntry.EntryTypes.Salida, Imagen = Resource.Drawable.ic_salida, Title = GetString(Resource.String.ReportTitleSalida)  },
                        new ReportEntry{ Type = ReportEntry.EntryTypes.Inventario, Imagen = Resource.Drawable.ic_peso, Title = GetString(Resource.String.ReportTitleStock)  },
                        new ReportEntry{ Type = ReportEntry.EntryTypes.InventarioResumen, Imagen = Resource.Drawable.ic_peso, Title = GetString(Resource.String.ReportTitleStockDetail) },
                        new ReportEntry{ Type = ReportEntry.EntryTypes.ReportedeBom, Imagen = Resource.Drawable.ic_bom_report, Title = GetString(Resource.String.ReportTitleBOM) }
                    };
                }

                gridView.Adapter = new ReportAdapter(this, _reportEntries);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void btnDesperdicios_Click(object sender, EventArgs e)
        {
            var Proceso = await repo.GetRepositoryZ().GetProces();

            if (Proceso.IsSubEquipment)
            {
                new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.DrawerChangeErrorMessageNoAvailable));
                return;
            }

            SetScreen(LScreens.Wastes);
        }

        #endregion

    }
}