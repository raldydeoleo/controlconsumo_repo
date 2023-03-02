using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Droid.Managers;
using System.IO;
using System.Threading.Tasks;
using Android.Bluetooth;
using ControlConsumo.Droid.Activities.Widgets;
using Android.Content.PM;
using Java.Lang.Reflect;
using Android.Hardware.Usb;
using LinkOS.Plugin;
using System.Threading;

namespace ControlConsumo.Droid.Activities
{
    [Activity(ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop)]
    public class ConfigActivity : BaseActivity
    {
        private const Int32 DEVICE_CLASS_PRINTER = 1664;
        private IEnumerable<Areas> areas { get; set; }
        private IEnumerable<Equipments> equipments { get; set; }
        private IEnumerable<EquipmentTypes> types { get; set; }
        private Button btnFinPrinters;
        private Equipments equipo { get; set; }
        private Equipments SubEquipo { get; set; }

        private TextView txtViewEquipment;
        private TextView txtViewPrinter;
        private EditText editScanEquipment;
        private LinearLayout layoutPlanification;
        private LinearLayout layoutInputOutputControl;
        private Switch SwtPlanification;
        private Switch SwtInputOutput;
        private RadioButton rbBluetooth;
        private RadioButton rbUsb;

        private Spinner spnSubEquipment { get; set; }
        private Boolean IsEquipment;
        private String BarCode { get; set; }
        private ProgressDialog dialog;
        protected ArrayAdapter<String> listDevices;
        protected List<BluetoothDevice> BluetoothDevices { get; set; }
        protected List<UsbDevice> UsbDevices { get; set; }
        private Receiver receiver;
        private PrinterManager.UsbReceiver usbReceiver;
        private BluetoothAdapter adapter;

        protected override async void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);

                SetContentView(Resource.Layout.ConfigActivity);
                SetTitle(Resource.String.ApplicationLabel);

                FindViewById<TextView>(Resource.Id.txtViewVersion).Text = String.Format("Versión : {0}", PackageManager.GetPackageInfo(PackageName, Android.Content.PM.PackageInfoFlags.Activities).VersionName);


                if (txtViewEquipment == null)
                {
                    BluetoothDevices = new List<BluetoothDevice>();
                    UsbDevices = new List<UsbDevice>();
                    listDevices = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1);
                    txtViewEquipment = FindViewById<TextView>(Resource.Id.txtViewEquipment);
                    editScanEquipment = FindViewById<EditText>(Resource.Id.editScanEquipment);
                    txtViewPrinter = FindViewById<TextView>(Resource.Id.txtViewPrinter);
                    btnFinPrinters = FindViewById<Button>(Resource.Id.btnFinPrinters);
                    spnSubEquipment = FindViewById<Spinner>(Resource.Id.spnSubEquipment);
                    layoutPlanification = FindViewById<LinearLayout>(Resource.Id.layoutPlanification);
                    layoutInputOutputControl = FindViewById<LinearLayout>(Resource.Id.layoutInputOutputControl);
                    SwtPlanification = FindViewById<Switch>(Resource.Id.SwtPlanification);
                    SwtInputOutput = FindViewById<Switch>(Resource.Id.SwtInputOutput);
                    rbBluetooth = FindViewById<RadioButton>(Resource.Id.rbBluetooth);
                    rbUsb = FindViewById<RadioButton>(Resource.Id.rbUsb);
                    editScanEquipment.KeyPress += editScanEquipment_KeyPress;
                    btnFinPrinters.Click += btnFinPrinters_Click;
                    rbBluetooth.Click += (arg, e) =>
                    {
                        ShowConnectedPrinters() ;
                    };

                    rbUsb.Click += (arg, e) =>
                    {
                        ShowConnectedPrinters();
                    };

                    Preload();
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void btnFinPrinters_Click(object sender, EventArgs e)
        {
            try
            {
                ShowConnectedPrinters();
            }
            catch(Exception ex)
            {
                await Util.SaveException(ex);
                throw;
            }
            
        }

        private async void ShowConnectedPrinters()
        {
            listDevices.Clear();

            if (rbUsb.Checked)
            {
                //USB
                usbReceiver = new PrinterManager.UsbReceiver();
                var usbFilter = new IntentFilter(PrinterManager.ACTION_USB_PERMISSION);
                RegisterReceiver(usbReceiver, usbFilter);

                UsbManager usbManager = (UsbManager)GetSystemService(Context.UsbService);
                
                await Task.Run(() => 
                {
                    var discoveryHandler = new PrinterManager.UsbDiscoveryHandler();

                    //Encontrar impresoras de marca Zebra conectadas vía cable usb. 
                    UsbDiscoverer.Current.FindPrinters(this, discoveryHandler);
                    
                    while (!discoveryHandler.DiscoveryComplete)
                    {
                        Thread.Sleep(10);
                    }

                    foreach (var printer in discoveryHandler.DiscoveredPrinters)
                    {
                        var usbDevice = usbManager.DeviceList.FirstOrDefault(s => s.Value.DeviceName == printer.Address);
                        if (usbDevice.Value != null)
                        {
                            UsbDevices.Add(usbDevice.Value);
                            listDevices.Add(GetFriendlyName(usbDevice.Value));
                        }
                    }
                });
            }
            else //Bluetooth
            {
                receiver = new Receiver(this);
                var filter = new IntentFilter(BluetoothDevice.ActionFound);
                RegisterReceiver(receiver, filter);

                filter = new IntentFilter(BluetoothAdapter.ActionDiscoveryFinished);
                RegisterReceiver(receiver, filter);

                adapter = BluetoothAdapter.DefaultAdapter;
                var pairedDevices = adapter.BondedDevices;

                if (pairedDevices.Any())
                {
                    foreach (var item in pairedDevices.Where(w => Convert.ToInt32(w.BluetoothClass.DeviceClass) == DEVICE_CLASS_PRINTER))
                    {
                        BluetoothDevices.Add(item);
                        listDevices.Add(GetFriendlyName(item));
                    }
                }

                if (adapter.IsDiscovering) adapter.CancelDiscovery();

                adapter.StartDiscovery();
            }

            Dialog dialog = null;
            var builder = new AlertDialog.Builder(this);
            builder.SetTitle(Resource.String.EquipmentPrinterTitle);
            builder.SetIcon(Android.Resource.Drawable.StatSysDataBluetooth);
            var view = LayoutInflater.Inflate(Resource.Layout.dialog_lots, null);
            builder.SetView(view);

            var lstlots = view.FindViewById<ListView>(Resource.Id.lstlots);
            lstlots.Adapter = listDevices;

            lstlots.ItemClick += (s, args) =>
            {
                listDevices.Clear();

                if (rbUsb.Checked)
                {
                    var device = UsbDevices.ElementAt(args.Position);
                    txtViewPrinter.Text = GetFriendlyName(device);
                    txtViewPrinter.Tag = device.SerialNumber;
                }
                else
                {
                    //Bluetooth
                    if (adapter.IsDiscovering) adapter.CancelDiscovery();
                    var device = BluetoothDevices.ElementAt(args.Position);

                    if (device.BondState != Bond.Bonded)
                    {
                        Method method = device.Class.GetMethod("createBond", (Java.Lang.Class[])null);
                        method.Invoke(device, (Java.Lang.Class[])null);
                    }

                    txtViewPrinter.Text = GetFriendlyName(device);
                    txtViewPrinter.Tag = device.Address;
                }
                dialog.Dismiss();
                dialog.Dispose();
            };

            builder.SetNegativeButton(Resource.String.Cancel, (s, er) =>
            {
                if (rbBluetooth.Checked && adapter.IsDiscovering) adapter.CancelDiscovery();
                dialog.Dismiss();
                dialog.Dispose();
            });

            dialog = builder.Create();

            RunOnUiThread(() =>
            {
                dialog.Show();
            });
        }

        public static String GetFriendlyName(BluetoothDevice device)
        {
            return String.Format("{0} - {1}", device.Name, device.Address);
        }

        public static String GetFriendlyName(UsbDevice device)
        {
            return String.Format("{0} - {1}", device.SerialNumber, device.DeviceName);
        }

        private async void editScanEquipment_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && !String.IsNullOrEmpty(editScanEquipment.Text) && e.Event.Action == KeyEventActions.Down)
            {
                try
                {
                    equipo = equipments.FirstOrDefault(p => p.ID == editScanEquipment.Text);
                    var area = areas.FirstOrDefault(p => p.Name == editScanEquipment.Text);

                    if (equipo != null)
                    {
                        IsEquipment = true;
                        txtViewEquipment.Text = equipo.Name;
                        BarCode = editScanEquipment.Text;

                        var Text = GetString(Resource.String.Select);
                        var subequipos = equipments.Where(w => w.IsSubEquipment == !equipo.IsSubEquipment).ToList();
                        var lista = subequipos.Select(s => s._Display).ToList();
                        lista.Insert(0, Text);
                        spnSubEquipment.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleDropDownItem1Line, lista);

                        layoutPlanification.Visibility = ViewStates.Gone;
                        layoutInputOutputControl.Visibility = ViewStates.Gone;

                        if (equipo.IsSubEquipment)
                        {
                            layoutPlanification.Visibility = ViewStates.Visible;
                            layoutInputOutputControl.Visibility = ViewStates.Visible;
                            SwtInputOutput.Checked = true;
                        }
                    }
                    else if (area != null)
                    {
                        IsEquipment = false;
                        txtViewEquipment.Text = area.Name;
                        BarCode = editScanEquipment.Text;
                    }
                    else
                    {
                        editScanEquipment.Text = String.Empty;
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
                MenuInflater.Inflate(Resource.Menu.SettingMenu, menu);
            }
            catch (Exception ex)
            {
                CatchException(ex);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        private async void Preload()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                var repot = repo.GetRepositoryEquipmentTypes();
                var repoe = repo.GetRepositoryEquipments();
                var repoa = repo.GetRepositoryAreas();
                types = await repot.GetAsyncAll();
                areas = await repoa.GetAsyncAll();

                var tmp = await repoe.GetAsyncAll();
                equipments = tmp.ToList();

                IsEquipment = await repoz.GetSettingAsync<Boolean>(Settings.Params.IsEquipment, false);
                var isSubEquipment = await repoz.GetSettingAsync<Boolean>(Settings.Params.IsSubEquipment, false);
                var subequipo = await repoz.GetSettingAsync<String>(Settings.Params.Second_Equipment, null);
                txtViewEquipment.Text = await repoz.GetSettingAsync<String>(Settings.Params.ConfigName, null);
                BarCode = await repoz.GetSettingAsync<String>(Settings.Params.ConfigID, null);
                var planification = await repoz.GetSettingAsync<Boolean>(Settings.Params.Planificacion_Automatica, false);
                var IsInputOutputControlActive = await repoz.GetSettingAsync<Boolean>(Settings.Params.IsInputOutputControlActive, true);
                var printerConnectivity = await repoz.GetSettingAsync<String>(Settings.Params.PrinterConnectivity, null);

                if (!String.IsNullOrEmpty(BarCode))
                    equipo = equipments.SingleOrDefault(p => p.ID == BarCode);

                layoutPlanification.Visibility = ViewStates.Gone;
                layoutInputOutputControl.Visibility = ViewStates.Gone;

                if (isSubEquipment)
                {
                    layoutPlanification.Visibility = ViewStates.Visible;
                    layoutInputOutputControl.Visibility = ViewStates.Visible;
                    SwtPlanification.Checked = planification;
                    SwtInputOutput.Checked = IsInputOutputControlActive;
                }

                if (equipo != null)
                {
                    var Text = GetString(Resource.String.Select);
                    var subequipos = equipments.Where(w => w.IsSubEquipment == !equipo.IsSubEquipment).ToList();
                    var lista = subequipos.Select(s => s._Display).ToList();
                    lista.Insert(0, Text);
                    spnSubEquipment.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleDropDownItem1Line, lista);

                    if (!String.IsNullOrEmpty(subequipo))
                    {
                        var position = subequipos.Select((s, p) => new { s, p }).Single(w => w.s.ID == subequipo).p;
                        spnSubEquipment.SetSelection(position + 1);
                    }
                }

                if (!String.IsNullOrWhiteSpace(printerConnectivity))
                {
                    if (printerConnectivity.Equals("Usb", StringComparison.InvariantCultureIgnoreCase))
                    {
                        rbUsb.Checked = true;
                    }
                    else
                    {
                        rbBluetooth.Checked = true;
                    }
                }
                txtViewPrinter.Text = await repoz.GetSettingAsync<String>(Settings.Params.PrinterName, null);
                txtViewPrinter.Tag = await repoz.GetSettingAsync<String>(Settings.Params.PrinterAddress, null);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_Cancel:

                    SetResult(Result.Canceled);

                    Finish();

                    break;

                case Resource.Id.menu_Add:

                    SetResult(Result.Ok);

                    Save();

                    break;

                case Resource.Id.menu_Update:

                    dialog = new ProgressDialog(this);
                    dialog.SetTitle(Resource.String.AlertDownloading);
                    dialog.Show();

                    Task.Run(() =>
                      {
                          UpdateApp();
                      });

                    break;

                case Resource.Id.menu_user:

                    AlertDialog _dialog = null;

                    var builder = new AlertDialog.Builder(this);
                    builder.SetTitle(Resource.String.EquipmentTitleUser);
                    builder.SetIcon(Android.Resource.Drawable.IcMenuView);
                    var viewBandejas = LayoutInflater.Inflate(Resource.Layout.dialog_scan_bandeja, null);

                    var editBandeja = viewBandejas.FindViewById<EditText>(Resource.Id.editBandeja);
                    viewBandejas.FindViewById<TextView>(Resource.Id.txtViewMessageScan).SetText(Resource.String.EquipmentReadUserCode);
                    Boolean Executed = false;

                    editBandeja.KeyPress += (s, ek) =>
                    {
                        ek.Handled = false;
                        if (ek.KeyCode == Keycode.Enter && !Executed)
                        {
                            Executed = true;
                            var repoz = repo.GetRepositoryZ();

                            var logon = repoz.GetLogonSync(editBandeja.Text);

                            if (logon != null)
                            {
                                var pdialog = new ChangePassword(this, false, logon.Logon, true);
                                pdialog.evento += (a, e) =>
                                {
                                    _dialog.Dismiss();
                                    _dialog.Dispose();
                                    Executed = false;
                                };
                            }
                            else
                            {
                                var wrongdialog = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.FeedBackWrongCode));
                                wrongdialog.OnCancelPress += () =>
                                {
                                    Executed = false;
                                    _dialog.Dismiss();
                                    _dialog.Dispose();
                                };
                            }

                            ek.Handled = true;
                        }
                    };

                    builder.SetView(viewBandejas);

                    builder.SetNegativeButton(Resource.String.Cancel, (s, a) =>
                    {
                        _dialog.Dismiss();
                        _dialog.Dispose();
                    });

                    _dialog = builder.Create();

                    RunOnUiThread(() =>
                    {
                        _dialog.Show();
                    });

                    break;
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        private async void Save()
        {
            try
            {
                ShowProgress(true);

                var repos = repo.GetRepositorySettings();
                var repoz = repo.GetRepositoryZ();

                var buffer = new List<Settings>();

                if (!String.IsNullOrEmpty(BarCode) || equipo != null)
                {
                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.IsEquipment,
                        Value = IsEquipment.ToString()
                    });

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.ConfigName,
                        Value = txtViewEquipment.Text
                    });

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.ConfigID,
                        Value = BarCode
                    });

                    var actualconfig = await repoz.GetActualConfig(BarCode);

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.ConfigIsActive,
                        Value = actualconfig != null ? "true" : "false"
                    });

                    var type = types.Single(p => p.ID == equipo.EquipmentTypeID);

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.IsLast,
                        Value = type.IsFinal.ToString()
                    });

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.NeedEan,
                        Value = type.NeedEan.ToString()
                    });

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.NeedGramo,
                        Value = type.NeedWeight.ToString()
                    });

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.IsSubEquipment,
                        Value = equipo.IsSubEquipment.ToString()
                    });

                    if (spnSubEquipment.SelectedItemPosition > 0)
                    {
                        var subequipo = equipments.Single(s => s._Display == spnSubEquipment.SelectedItem.ToString());

                        buffer.Add(new Settings()
                        {
                            Key = Settings.Params.Second_Equipment,
                            Value = subequipo.ID
                        });
                    }

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.Planificacion_Automatica,
                        Value = SwtPlanification.Checked.ToString().ToLower()
                    });

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.IsInputOutputControlActive,
                        Value = SwtInputOutput.Checked.ToString().ToLower()
                    });
                }

                if (!String.IsNullOrEmpty(txtViewPrinter.Text))
                {

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.PrinterConnectivity,
                        Value = rbBluetooth.Checked ? "Bluetooth" : "Usb"
                    });

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.PrinterName,
                        Value = txtViewPrinter.Text
                    });

                    buffer.Add(new Settings()
                    {
                        Key = Settings.Params.PrinterAddress,
                        Value = txtViewPrinter.Tag.ToString()
                    });
                }

                await repos.InsertOrReplaceAsyncAll(buffer);

                var process = await repoz.GetProces();

                process.EquipmentID = BarCode;
                process.Equipment = txtViewEquipment.Text;
                process.IsSubEquipment = equipo.IsSubEquipment;
                if (process.IsSubEquipment && spnSubEquipment.SelectedItemPosition > 0)
                {
                    process.SubEquipmentID = equipments.Single(s => s._Display == spnSubEquipment.SelectedItem.ToString()).ID;
                }

                repoz.SetProcess(process);

                SetResult(buffer.Any(a => a.Key == Settings.Params.ConfigID) ? Result.Ok : Result.FirstUser);

                Finish();
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

        private void UpdateApp()
        {
            try
            {
                var ruta = Path.Combine(Util.GetDownloadFolder(), String.Format("ControlConsumo_{0}.apk", DateTime.Now.ToString("yyyyMMdd")));

                var url = "http://ladostgsap07.la.local:8000/sap(bD1lbiZjPTQwMA==)/bc/bsp/sap/ztake_servicio/actualizar.htm";

                Util.DownloadFile(url, ruta);

                Intent promptInstall;
                if (Convert.ToInt32(Build.VERSION.Sdk) >= 23)
                {
                    promptInstall = new Intent(Intent.ActionInstallPackage);
                    promptInstall.AddFlags(ActivityFlags.GrantReadUriPermission);
                }
                else
                {
                    promptInstall = new Intent(Intent.ActionView);
                }
                promptInstall.AddFlags(ActivityFlags.NewTask);
                promptInstall.SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(ruta)), "application/vnd.android.package-archive");
                StartActivity(promptInstall);
            }
            catch (System.Net.WebException wex)
            {
                RunOnUiThread(async () =>
                {
                    await CatchException(wex);
                    Toast.MakeText(this, Resource.String.EquipmentupdateError, ToastLength.Long).Show();
                    dialog.Dismiss();
                    dialog.Dispose();
                });

            }
            catch (Exception ex)
            {
                RunOnUiThread(async () =>
                {
                    await CatchException(ex);
                });
            }
        }

        protected override void OnDestroy()
        {

            base.OnDestroy();   
        }

        public class Receiver : BroadcastReceiver
        {
            ConfigActivity _chat;
            List<String> Lista = new List<String>();

            public Receiver(ConfigActivity chat)
            {
                _chat = chat;
            }

            public override void OnReceive(Context context, Intent intent)
            {
                string action = intent.Action;

                // When discovery finds a device
                if (action == BluetoothDevice.ActionFound)
                {
                    BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

                    if (device.BondState != Bond.Bonded)
                    {
                        if (!Lista.Any(a => a == device.Address) && Convert.ToInt32(device.BluetoothClass.DeviceClass) == DEVICE_CLASS_PRINTER)
                        {
                            Lista.Add(device.Address);
                            _chat.BluetoothDevices.Add(device);
                            _chat.listDevices.Add(GetFriendlyName(device));
                        }
                    }
                }
                else if (action == BluetoothAdapter.ActionDiscoveryFinished)
                {
                    var str = _chat.Resources.GetString(Resource.String.EquipmentPrinterFound);

                    Toast.MakeText(_chat, String.Format(str, _chat.listDevices.Count), ToastLength.Long);
                }
            }
        }
    }
}