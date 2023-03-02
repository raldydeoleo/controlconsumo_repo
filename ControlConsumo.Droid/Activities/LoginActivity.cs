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
using ControlConsumo.Shared.Models.Process;
using System.Net;
using Newtonsoft.Json;
using ControlConsumo.Droid.Activities.Widgets;
using System.Threading.Tasks;
using Android.Content.PM;
using Android.Preferences;
using Android;

namespace ControlConsumo.Droid.Activities
{
    [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop)]
    public class LoginActivity : BaseActivity
    {
        private enum _Screen
        {
            InitialSync,
            MenuActivity
        }

        private Button btnlogin { get; set; }
        private Spinner spinner { get; set; }
        private TextView EditUserNameFull { get; set; }
        private EditText edituser { get; set; }
        private EditText editpass { get; set; }
        private Boolean AlreadyExecuted { get; set; }
        private DateTime FechaParaMantenimiento { get; set; }
        public TextView txtViewServer { get; set; }

        private IEnumerable<ProcessList> Procesos { get; set; }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            try
            {
                base.OnActivityResult(requestCode, resultCode, data);

                switch ((_Screen)requestCode)
                {
                    case _Screen.InitialSync:
                        if (resultCode == Result.Ok)
                        {
                            spinner.Visibility = ViewStates.Gone;
                        }

                        break;

                    case _Screen.MenuActivity:
                        Util.RecycleConnection();
                        GC.Collect();

                        break;
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected override async void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);

                SetContentView(Resource.Layout.LoginActivity);
                SetTitle(Resource.String.ApplicationLabel);
                FindViewById<TextView>(Resource.Id.txtViewVersion).Text = String.Format("Versión : {0}", PackageManager.GetPackageInfo(PackageName, Android.Content.PM.PackageInfoFlags.Activities).VersionName);


                if (btnlogin == null)
                {
                    edituser = FindViewById<EditText>(Resource.Id.EditUserName);
                    editpass = FindViewById<EditText>(Resource.Id.EditPassword);
                    txtViewServer = FindViewById<TextView>(Resource.Id.txtViewServer);
                    editpass.Enabled = false;
                    EditUserNameFull = FindViewById<TextView>(Resource.Id.EditUserNameFull);
                    btnlogin = FindViewById<Button>(Resource.Id.ButtonAccept);
                    btnlogin.Click += btnlogin_Click;
                    spinner = FindViewById<Spinner>(Resource.Id.Process);
                    edituser.KeyPress += edituser_KeyPress;
                }

                if (CheckPermissionGranted(Manifest.Permission.WriteExternalStorage))
                {
                    ValidaProcess();
                }
            }
            catch (Exception ex)
            {
                await base.CatchException(ex);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            ClearPreferences();
            if (CheckPermissionGranted(Manifest.Permission.WriteExternalStorage))
            {
                ValidaProcess();
                CorrerMantenimiento();
            }
        }

        /// <summary>
        /// Metodo para limpiar las configuraciones de la actidad menu.
        /// </summary>
        protected async void ClearPreferences()
        {
            try
            {

                var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                var editor = prefs.Edit();
                editor.PutString(MenuActivity._PROCESO, null);
                editor.PutInt(MenuActivity._ACTUALSCREEN, (Int32)MenuActivity.LScreens.Choose);
                editor.Commit();
                Util.ReleaseLock(this, Util.Locks.Turnos);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void edituser_KeyPress(object sender, View.KeyEventArgs e)
        {
            e.Handled = false;
            if (e.KeyCode == Keycode.Enter && e.Event.Action == KeyEventActions.Down)
            {
                try
                {
                    var repoz = repo.GetRepositoryZ();
                    var luser = await repoz.GetLogon(edituser.Text.ToUpper());

                    if (luser != null)
                    {
                        EditUserNameFull.Text = luser.Name;
                        editpass.Enabled = true;
                        editpass.RequestFocus();
                    }
                    else
                    {
                        EditUserNameFull.Text = String.Empty;
                        edituser.Text = String.Empty;
                    }
                }
                catch (SQLite.Net.SQLiteException)
                {
                    editpass.Enabled = true;
                    editpass.RequestFocus();
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

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckPermissionGranted(Manifest.Permission.WriteExternalStorage))
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Permiso requerido");
                    alert.SetMessage("Esta aplicación necesita el permiso de almacenamiento para poder continuar");
                    alert.SetPositiveButton("Solicitar permiso", (obj, args) =>
                    {
                        RequestStoragePermission();
                        if (!CheckPermissionGranted(Manifest.Permission.WriteExternalStorage))
                        {
                            var intent = new Intent(Android.Provider.Settings.ActionManageApplicationsSettings);
                            intent.AddFlags(ActivityFlags.NewTask);
                            StartActivityForResult(intent, REQUEST_STORAGE);
                        }
                    });

                    alert.SetNegativeButton("Cancelar", (obj, args) =>
                    {
                        Toast.MakeText(this, "¡Cancelado!", ToastLength.Long);
                    });

                    Dialog dialog = alert.Create();
                    dialog.Show();

                    return;
                }
                else
                {
                    if (spinner.Visibility == ViewStates.Visible)
                    {
                        repo.GetRepositoryZ().SetProcess(Procesos.ElementAt(spinner.SelectedItemPosition));
                        Sync();
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }

            TryLogin();
        }

        private async void TryLogin()
        {
            try
            {
                btnlogin.Enabled = false;

                if (String.IsNullOrEmpty(edituser.Text)) return;

                ShowFeedBack(true, GetString(Resource.String.FeedBackLogin));

                var repoz = repo.GetRepositoryZ();

                Users user = null;

                try
                {
                    user = await repoz.GetLogon(edituser.Text.ToUpper());
                }
                catch (InvalidOperationException)
                {
                    ShowFeedBack(true, GetString(Resource.String.FeedBackWrong), false);
                    return;
                }

                if (!user.Password.Equals(editpass.Text))
                {
                    ShowFeedBack(true, GetString(Resource.String.FeedBackWrong), false);
                }
                else
                {
                    if (!user.IsActive)
                    {
                        ShowFeedBack(true, GetString(Resource.String.FeedBackLock), false);
                        return;
                    }

                    if (user.Expire.HasValue && user.Expire.Value.Date <= DateTime.Now.Date)
                    {
                        var dialog = new ChangePassword(this, false, user.Logon);
                        dialog.evento += (ev, args) =>
                        {
                            Login(user);
                        };
                        return;
                    }

                    Login(user);
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
            finally
            {
                btnlogin.Enabled = true;
            }
        }

        private async void Login(Users user)
        {
            var repoSettings = repo.GetRepositorySettings();
            var werks = await repoSettings.GetAsyncByKey(Settings.Params.Werks);
            var werksName = await repoSettings.GetAsyncByKey(Settings.Params.WerksName);
            var Process = await repoSettings.GetAsyncByKey(Settings.Params.Process);
            var ProcessName = await repoSettings.GetAsyncByKey(Settings.Params.ProcessName);

            var Proceso = new ProcessList()
            {
                Logon = user.Logon,
                UserName = user.Name,
                Centro = werks.Value,
                CentroName = werksName.Value,
                Process = Process.Value,
                ProcessName = ProcessName.Value
            };

            repo.GetRepositoryZ().SetProcess(Proceso);
            repo.GetRepositoryZ().SetUser(user);
            var intent = new Intent(this, typeof(MenuActivity));
            StartActivityForResult(intent, (Int32)_Screen.MenuActivity);
            Finish();
        }

        private async void ShowFeedBack(Boolean value, String message, Boolean progress = true)
        {
            try
            {
                var translation = 0.0f;

                if (value == false)
                {
                    translation = Resources.GetDimension(Resource.Dimension.dp_60);
                }

                var container = FindViewById<ViewGroup>(Resource.Id.feedback_content);
                container.FindViewById<TextView>(Resource.Id.feedback_loading_text).Text = message;
                container.FindViewById<ProgressBar>(Resource.Id.feedback_Progress).Visibility = progress ? ViewStates.Visible : ViewStates.Gone;
                container.Animate().TranslationY(translation).SetDuration(500).Start();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void ValidaProcess()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                var repoSettings = repo.GetRepositorySettings();
                var server = repoz.GetServerName();

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


                var IsSync = await repoz.GetSettingAsync<Boolean>(Settings.Params.IsSynchronized, false);

                if (!IsSync)
                {
                    try
                    {
                        Procesos = await repoz.GetProcess();
                        spinner.Visibility = ViewStates.Visible;
                        var list = Procesos.Select(p => String.Format("{0} - {1}", p.ProcessName, p.Centro)).ToArray();
                        spinner.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleDropDownItem1Line, list);

                    }
                    catch (WebException wEx)
                    {
                        var webExceptionManager = new WebExceptionManager(wEx);
                        var mensajeError = webExceptionManager.ClasificarExcepcionWeb();
                        ShowFeedBack(true, mensajeError, false);
                        await Util.SaveException(wEx);
                    }
                    catch (Exception ex)
                    {
                        await Util.SaveException(ex);
                        throw;
                    }
                }
                else
                {
                    spinner.Visibility = ViewStates.Gone;
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void Sync()
        {
            try
            {
                var intent = new Intent(this, typeof(SyncActivity));
                var json = JsonConvert.SerializeObject(Procesos.ElementAt(spinner.SelectedItemPosition));
                intent.PutExtra(CustExtras.Proceso.ToString(), json);
                StartActivityForResult(intent, (Int32)_Screen.InitialSync);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void CorrerMantenimiento()
        {
            await Task.Delay(2000);

            var repoz = repo.GetRepositoryZ();

            var IsSync = await repoz.GetSettingAsync<Boolean>(Settings.Params.IsSynchronized, false);

            if (!IsSync) return;

            var FechaMantenimiento = await repoz.GetSettingAsync<String>(Settings.Params.FechaUltimoMantenimiento, DateTime.Now.AddDays(-1).GetSapDate());

            if (FechaMantenimiento != DateTime.Now.GetSapDate())
            {
                try
                {
                    ShowProgress(true, Resource.String.AlertPleaseWaitMaintance);
                    Util.CreateDataBaseBackup();
                    repoz = new Shared.Repositories.RepositoryZ(Util.GetConnection());
                    var backup = await repoz.CleanAllValues();

                    var repoSetting = repo.GetRepositorySettings();
                    await repoSetting.InsertOrReplaceAsync(new Settings()
                    {
                        Key = Settings.Params.FechaUltimoMantenimiento,
                        Value = DateTime.Now.GetSapDate()
                    });
                }
                catch (Exception ex)
                {
                    await CatchException(ex);
                }
                finally
                {
                    Util.RecycleConnection();
                    ShowProgress(false);
                }
            }
        }
    }
}