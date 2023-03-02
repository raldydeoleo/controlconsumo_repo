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
using System.Net;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Droid.Managers;
using Newtonsoft.Json;
using ControlConsumo.Shared.Models.Process;
using Android.Content.PM;
using System.Threading.Tasks;

namespace ControlConsumo.Droid.Activities
{
    [Activity(Theme = "@android:style/Theme.Holo.Light.Dialog.NoActionBar", LaunchMode = LaunchMode.SingleTop)]
    public class SyncActivity : BaseActivity
    {
        private ProgressBar ProgressBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetTitle(Resource.String.ApplicationLabel);

            SetContentView(Resource.Layout.SyncActivity);
            SetFinishOnTouchOutside(false);

            ProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBarSyncing);
            SyncAll();
        }

        private async void SyncAll()
        {
            var proceso = JsonConvert.DeserializeObject<ProcessList>(Intent.GetStringExtra(CustExtras.Proceso.ToString()));
           
            #region Base de Datos creacion de Tablas

            if (!Util.GetConnection().Exist)
            {
                var database = repo.GetRepositoryDataBase();
                database.CreateDb();
            }

            #endregion

            #region Declaraciones

            var reposettings = repo.GetRepositorySettings();
            var repouser = repo.GetRepositoryUsers();
            var reporols = repo.GetRepositoryRols();
            var reporolpermi = repo.GetRepositoryRolsPermits();
            var repomaterial = repo.GetRepositoryMaterials();
            var repolotes = repo.GetRepositoryLots();
            var repoEquip = repo.GetRepositoryEquipments();
            var repoconfig = repo.GetRepositoryConfigs();
            var repoarea = repo.GetRepositoryAreas();
            var repoareaequ = repo.GetRepositoryAreasEquipments();
            var repoequimat = repo.GetRepositoryConfigMaterials();
            var repotime = repo.GetRepositoryTimes();
            var repoequptype = repo.GetRepositoryEquipmentTypes();
            var repoturn = repo.GetRepositoryTurns();
            var repomatzilm = repo.GetRepositoryMaterialZilm();
            var repoBand = repo.GetRepositoryTrays();
            var repoBandProduct = repo.GetRepositoryTraysProducts();
            var repoBandTiempo = repo.GetRepositoryTraysTimes();
            var repomatprocess = repo.GetRepositoryMaterialsProcess();
            var reporoute = repo.GetRepositoryProductsRoutes();
            var repoConfiguracionTiempoSalida = repo.GetRepositoryConfiguracionTiempoSalidas();
            var repoProductoTipoAlmacenamiento = repo.GetRepositoryProductoTipoAlmacenamientos();
            var repoTipoProductoTerminado = repo.GetRepositoryTipoProductoTerminados();
            var repoTipoAlmacenamientoProducto = repo.GetRepositoryTipoAlmacenamientoProductos();
            var repoConfiguracionSincronizacionTablas = repo.GetRepositoryConfiguracionSincronizacionTablas();
            var repoConfiguracionInicialControlSalidas = repo.GetRepositoryConfiguracionInicialControlSalidas();
            var repoMotivosReimpresionEtiquetas = repo.GetRepositoryLabelPrintingReasons();

            #endregion

            #region Metodos de Sincronizacion

            try
            {
                await Task.Run(async () =>
                 {
                     await repoConfiguracionSincronizacionTablas.SyncAsyncAll();
                     await reporols.SyncAsyncAll();
                     await repouser.SyncAsyncAll();
                     SyncProgress(10);

                     await reporolpermi.SyncAsyncAll();
                     await repomaterial.SyncAsyncAll();
                     SyncProgress(10);

                     await repolotes.SyncAsyncAll();
                     await repoEquip.SyncAsyncAll();
                     SyncProgress(10);

                     await repoconfig.SyncAsyncAll();
                     await repoarea.SyncAsyncAll();
                     await repoareaequ.SyncAsyncAll();
                     SyncProgress(10);

                     await repoequimat.SyncAsyncAll();
                     await repotime.SyncAsyncAll();
                     SyncProgress(10);

                     await repoequptype.SyncAsyncAll();
                     await repoturn.SyncAsyncAll();
                     await repomatzilm.SyncAsyncAll();
                     SyncProgress(10);

                     await repoBand.SyncAsyncAll();
                     await repoBandProduct.SyncAsyncAll();
                     SyncProgress(10);

                     await repoBandTiempo.SyncAsyncAll();
                     await repomatprocess.SyncAsyncAll();
                     await reporoute.SyncAsyncAll();
                     SyncProgress(10);

                     await repoConfiguracionTiempoSalida.SyncAsyncAll();
                     await repoTipoAlmacenamientoProducto.SyncAsyncAll();
                     await repoTipoProductoTerminado.SyncAsyncAll();
                     SyncProgress(10);

                     await repoProductoTipoAlmacenamiento.SyncAsyncAll();
                     await repoConfiguracionInicialControlSalidas.SyncAsyncAll();
                     await repoMotivosReimpresionEtiquetas.SyncAsyncAll();
                     SyncProgress(10);

                 });
                
                var Configuraciones = new List<Settings>();
                Configuraciones.Add(new Settings()
                {
                    Key = Settings.Params.IsSynchronized,
                    Value = "True"
                });

                Configuraciones.Add(new Settings()
                {
                    Key = Settings.Params.Werks,
                    Value = proceso.Centro
                });

                Configuraciones.Add(new Settings()
                {
                    Key = Settings.Params.WerksName,
                    Value = proceso.CentroName
                });

                Configuraciones.Add(new Settings()
                {
                    Key = Settings.Params.Process,
                    Value = proceso.Process
                });

                Configuraciones.Add(new Settings()
                {
                    Key = Settings.Params.ProcessName,
                    Value = proceso.ProcessName
                });

                Configuraciones.Add(new Settings()
                {
                    Key = Settings.Params.LastDailyUpdate,
                    Value = DateTime.Now.ToString("yyyyMMdd")
                });

                Configuraciones.Add(new Settings()
                {
                    Key = Settings.Params.FechaUltimoMantenimiento,
                    Value = DateTime.Now.GetSapDate()
                });

                await reposettings.InsertAsyncAll(Configuraciones);
                Util.RecycleConnection();

                SetResult(Result.Ok);
                Finish();
            }
            catch (WebException ex)
            {
                Toast.MakeText(this, "No hay Conexión con el Servidor", ToastLength.Long);
                await Util.SaveException(ex, "Carga inicial");
            }
            catch (Exception ex)
            {
                await base.CatchException(ex);
            }

            #endregion
        }
        private void SyncProgress(int currentProcess)
        {
            RunOnUiThread(() => {
                ProgressBar.Indeterminate = false;
                ProgressBar.Progress += currentProcess;
                ProgressBar.Max = 100;
            });
        }
    }
}