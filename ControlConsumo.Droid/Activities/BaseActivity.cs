using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Droid.Activities.Widgets;
using Android.Graphics;
using Android.Net;
using System.Threading.Tasks;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;
using Java.Interop;
using Android.Support.V4.Content;
using System.Net;

namespace ControlConsumo.Droid.Activities
{
    public class BaseActivity : Activity
    {
        private RepositoryFactory _repo { get; set; }
        protected RepositoryFactory repo
        {
            get
            {
                if (_repo == null || _repo.Connection == null || _repo.Connection.Connection == null)
                {
                    Util.RecycleConnection();
                    _repo = new RepositoryFactory(Util.GetConnection());
                }

                return _repo;
            }
        }

        private ProgressDialog dialog;
        protected LinearLayout lyHeader;
        protected TextView txtViewCenter;
        protected TextView txtViewProcess;
        protected TextView txtViewEquipment;
        protected TextView txtViewLogon;
        protected TextView txtViewTurn;
        protected TextView txtMessages;
        protected Button btnAceptarGeneral;
        protected TextView txtViewServer;
        protected TextView txtViewFecha;
        protected ImageView imgNoConnection;
        protected ImageView imgPrinterStatus;
        protected ViewGroup frmErrors;
        protected int REQUEST_STORAGE = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            TryToGetPermissions();
        }

        private void SetupRepoFactory()
        {
            if (_repo == null || _repo.Connection == null || _repo.Connection.Connection == null)
            {
                _repo = new RepositoryFactory(Util.GetConnection());
                AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            }
        }

        protected void TryToGetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                if (!CheckPermissionGranted(Manifest.Permission.WriteExternalStorage))
                {
                    RequestStoragePermission();
                }
                else
                {
                    //Llamada funcionalidad de instanciar objeto de repositorio Factory.
                    SetupRepoFactory();
                }
            }
            else
            {
                //Llamada funcionalidad de instanciar objeto de repositorio Factory. 
                SetupRepoFactory();
            }
        }

        [Export]
        public bool CheckPermissionGranted(string Permissions)
        {
            // Chequear si el permiso está otorgado.
            if (ActivityCompat.CheckSelfPermission(this, Permissions) != Permission.Granted)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void RequestStoragePermission()
        {
            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.WriteExternalStorage))
            {
                // Provee un rationale adicional al usuario si el permiso no ha sido otorgado.
                // y el usuario se beneficiaría de un contexto adicional para el uso del permiso.
                // Por ejemplo, si el usuario ha denegado previamente el permiso.
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage.ToString() },
                    REQUEST_STORAGE);

            }
            else
            {
                // Aún no se ha otorgado el permiso de Almacenamiento.
                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage.ToString() },
                    REQUEST_STORAGE);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
                     Permission[] grantResults)
        {
            if (requestCode == REQUEST_STORAGE)
            {
                if (grantResults.Length <= 0)
                {
                    // Si la interacción del usuario ha sido interrumpida, la solicitud de permiso es cancelada y 
                    // se recibe el arreglo vacío.
                    Toast.MakeText(this, "El proceso fue interrumpido. Favor de intentar nuevamente.", ToastLength.Long).Show();
                }
                else if (grantResults[0] == PermissionChecker.PermissionGranted)
                {
                    //Permiso fue otorgado.
                    Toast.MakeText(this, "Permiso otorgado", ToastLength.Long).Show();
                    SetupRepoFactory();
                }
                else
                {
                    // Permiso fue rechazado.
                    Toast.MakeText(this, "Permiso rechazado", ToastLength.Long).Show();
                }
            }
        }

        protected void InitControlsHeader()
        {
            lyHeader = FindViewById<LinearLayout>(Resource.Id.lyHeader);
            txtViewCenter = FindViewById<TextView>(Resource.Id.txtViewCenter);
            txtViewProcess = FindViewById<TextView>(Resource.Id.txtViewProcess);
            txtViewEquipment = FindViewById<TextView>(Resource.Id.txtViewEquipment);
            txtViewLogon = FindViewById<TextView>(Resource.Id.txtViewLogon);
            txtViewTurn = FindViewById<TextView>(Resource.Id.txtViewTurn);
            txtViewFecha = FindViewById<TextView>(Resource.Id.txtViewFecha);
            txtViewServer = FindViewById<TextView>(Resource.Id.txtViewServer);
            imgNoConnection = FindViewById<ImageView>(Resource.Id.imgNoConnection);
            imgPrinterStatus = FindViewById<ImageView>(Resource.Id.imgPrinterStatus);

            txtViewEquipment.Tag = 0;
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (CheckPermissionGranted(Manifest.Permission.WriteExternalStorage))
            {
                if (_repo == null || _repo.Connection == null || _repo.Connection.Connection == null)
                    _repo = new RepositoryFactory(Util.GetConnection());
            }
        }

        private async void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            await Util.SaveException(e.Exception);
        }

        protected enum CustExtras
        {
            Proceso,
            Caching,
            TurnID,
            ProductionDate,
            MacAddress,
            ProductCode,
            Operacion,
            IsNotify
        }

        protected async void SetHeader()
        {
            try
            {
                //VolveraReferenciarControles:

                //if (lyHeader == null)
                //{
                //    Task.Delay(500).Wait();
                //    InitControlsHeader();
                //}

                var process = await repo.GetRepositoryZ().GetProces();
                if (process != null)
                {
                    //if (txtViewLogon == null || txtViewProcess == null || txtViewCenter == null || txtViewServer == null || txtViewEquipment == null) /// Todavia no esta pintada la interfaz Grafica
                    //{
                    //    goto VolveraReferenciarControles;
                    //}

                    RunOnUiThread(() =>
                    {
                        txtViewLogon.Text = process.UserName;
                        txtViewProcess.Text = String.Format("{0} {1}", process.Process, process.ProcessName);
                        txtViewCenter.Text = String.Format("{0} {1}", process.Centro, (process.CentroName ?? String.Empty).Replace("-JMC", ""));

                        var server = repo.GetRepositoryZ().GetServerName();

                        switch (server)
                        {
                            case "ladostgsap01.la.local":
                            case "ladostgsap08.la.local":
                                txtViewServer.Text = "Productivo"; break;

                            case "ladostgsap07.la.local":
                                txtViewServer.Text = "Calidad"; break;

                            case "ladostgsap06.la.local":
                                txtViewServer.Text = "Desarrollo"; break;

                            case "ladostgsap02.la.local":
                                txtViewServer.Text = "SandBox"; break;

                            case "ladostgsap05.la.local":
                                txtViewServer.Text = "SandBox 2"; break;

                            case "ladostgsap03.la.local":
                                txtViewServer.Text = "Produccion 2"; break;
                        }

                        if (!String.IsNullOrEmpty(process.EquipmentID))
                        {
                            if (!String.IsNullOrEmpty(process.SubEquipmentID))
                            {
                                txtViewEquipment.Text = String.Format("{0} - {1} ({2})", process.EquipmentID, process.Equipment, process.SubEquipmentID);
                            }
                            else
                            {
                                txtViewEquipment.Text = String.Format("{0} - {1}", process.EquipmentID, process.Equipment);
                            }

                            txtViewEquipment.Tag = 1;
                        }
                        else
                        {
                            txtViewEquipment.Text = GetString(Resource.String.NoEquipment);
                            txtViewEquipment.Tag = 0;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected enum Status
        {
            Good,
            Warning,
            Error
        }

        public Boolean IsThereConnection
        {
            get
            {
                var cm = (ConnectivityManager)GetSystemService(Context.ConnectivityService);
                var netInfo = cm.ActiveNetworkInfo;
                return netInfo != null && netInfo.IsConnectedOrConnecting;
            }
        }

        protected void SetFooter(Boolean show, Status status = Status.Good, String message = "")
        {
            RunOnUiThread(() =>
            {
                var translation = 0.0f;

                if (show == false)
                {
                    translation = Resources.GetDimension(Resource.Dimension.dp_80);
                }

                switch (status)
                {
                    case Status.Good:
                        frmErrors.Background = Resources.GetDrawable(Android.Resource.Color.HoloGreenLight);
                        break;

                    case Status.Warning:
                        frmErrors.Background = Resources.GetDrawable(Resource.Color.yellow_light);
                        break;

                    case Status.Error:
                        frmErrors.Background = Resources.GetDrawable(Resource.Color.red_light); 
                        break;
                }

                txtMessages.Text = message;
                frmErrors.Animate().TranslationY(translation).SetDuration(500).Start();
            });
        }

        protected async Task CatchException(Exception ex)
        {
            try
            {
                RunOnUiThread(async () =>
                {
                    var errorDialog = new ErrorDialog(this, ex);
                    errorDialog.OnOkButtonPress += errorDialog_OnOkButtonPress;
                    await Util.SaveException(ex);
                });
            }
            catch(Exception ex1)
            {
                await Util.SaveException(ex);
                await Util.SaveException(ex1);
            }
        }

        private void errorDialog_OnOkButtonPress()
        {
            StartActivity(typeof(LoginActivity));
            Finish();
        }

        protected Int32 GetWidthPercent(Single Percent)
        {
            var displayRectangle = new Rect();
            Window.DecorView.GetWindowVisibleDisplayFrame(displayRectangle);
            float width = displayRectangle.Width();
            return (Int32)(width * Percent);
        }

        protected Int32 GetHeightPercent(Single Percent)
        {
            var displayRectangle = new Rect();
            Window.DecorView.GetWindowVisibleDisplayFrame(displayRectangle);
            float height = displayRectangle.Height();
            return (Int32)(height * Percent);
        }

        protected void ShowProgress(Boolean Show, Int32? Message = null)
        {
            try
            {
                RunOnUiThread(() =>
                {
                    if (Show)
                    {
                        dialog = ProgressDialog.Show(this, GetString(Resource.String.AlertWait), GetString(Message ?? Resource.String.AlertPleaseWait), true, false);
                    }
                    else
                    {
                        if (dialog != null)
                        {
                            dialog.Dismiss();
                            dialog.Dispose();
                            dialog = null;
                        }
                    }
                });
            }
            catch (WindowManagerBadTokenException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        protected async Task<String> GetApplicationEnvironment()
        {
            var server = repo.GetRepositoryZ().GetServerName();
            var environment = "";
            try
            {
                switch (server)
                {
                    case "ladostgsap01.la.local":
                    case "ladostgsap08.la.local":
                        environment = "Productivo"; break;

                    case "ladostgsap07.la.local":
                        environment = "Calidad"; break;

                    case "ladostgsap06.la.local":
                        environment = "Desarrollo"; break;

                    case "ladostgsap02.la.local":
                        environment = "SandBox"; break;

                    case "ladostgsap05.la.local":
                        environment = "SandBox 2"; break;

                    case "ladostgsap03.la.local":
                        environment = "Produccion 2"; break;
                }
            }
            catch(Exception ex)
            {
                await Util.SaveException(ex);
                throw;
            }
            return environment;
        }


        protected async Task<Boolean> LockTyping()
        {
            var isTypingLocked = false;
            var appEnvironment = await GetApplicationEnvironment();
            var Proceso = await repo.GetRepositoryZ().GetProces();

            if (appEnvironment.Equals("Productivo") && !Proceso.Process.Equals("2302"))
            {
                isTypingLocked = true;
            }
            return isTypingLocked;
        }

        protected async void ShowWebExceptionDialog(WebException wEx, string Step)
        {
            try
            {
                var webExceptionManager = new WebExceptionManager(wEx);
                var mensajeError = webExceptionManager.ClasificarExcepcionWeb();
                await Util.SaveException(wEx, Step, false);
                new CustomDialog(this, CustomDialog.Status.Error, mensajeError); //Lanzar ventana emergente con mensaje de error
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}