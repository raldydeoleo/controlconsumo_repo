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
using OxyPlot.Xamarin.Android;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Droid.Managers;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using ControlConsumo.Shared.Models.Process;
using Android.Content.PM;

namespace ControlConsumo.Droid.Activities
{
    [Activity(ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop)]
    public class QualityActivity : BaseActivity
    {
        private const String _PROCESO = "QualityActivity._PROCESO";
        private const String _TURNID = "QualityActivity._TURNID";
        private PlotView plotView;
        private PlotView plotView2;
        private String ProductCode;
        private Screens Screen;
        private Byte TurnID;
        private Boolean Finished;

        private enum Screens
        {
            None,
            Peso,
            Diametro,
            Tiro
        }

        protected override async void OnSaveInstanceState(Bundle outState)
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                outState.PutString(_PROCESO, JsonConvert.SerializeObject(repoz.GetProces()));
                outState.PutShort(_TURNID, TurnID);

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
                TurnID = (Byte)savedInstanceState.GetShort(_TURNID);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.quality_activity);
            SetTitle(Resource.String.ApplicationLabel);

            if (plotView == null)
            {
                ProductCode = Intent.GetStringExtra(CustExtras.ProductCode.ToString());
                TurnID = (Byte)Intent.GetIntExtra(CustExtras.TurnID.ToString(), 0);
                plotView = FindViewById<PlotView>(Resource.Id.plotView);
                plotView2 = FindViewById<PlotView>(Resource.Id.plotView2);
                BindingReport(Screens.Peso);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.QualityMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_Cancel:

                    Finish();

                    break;

                case Resource.Id.menu_peso:

                    BindingReport(Screens.Peso);

                    break;

                case Resource.Id.menu_diametro:

                    BindingReport(Screens.Diametro);

                    break;

                case Resource.Id.menu_tiro:

                    BindingReport(Screens.Tiro);

                    break;
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        private async void BindingReport(Screens pScreen)
        {
            try
            {
                Boolean throwThread = true;

                if (this.Screen == pScreen)
                {
                    throwThread = false;
                }

                this.Screen = pScreen;
                var Proceso = await repo.GetRepositoryZ().GetProces();

                var report = new ReportsManager(this, Proceso.EquipmentID, ProductCode, TurnID);

                if (!IsThereConnection)
                {
                    var noPesada = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.NoConection));
                    noPesada.OnAcceptPress += (Boolean IsCantidad, Single Box, Single Cantidad) =>
                    {
                        Finish();
                    };
                    return;
                }

                ReportsManager.PlotModels retorno = null;

                switch (Screen)
                {
                    case Screens.Peso:

                        retorno = await report.GetPesoReport();

                        break;

                    case Screens.Diametro:

                        retorno = await report.GetDiametroReport();

                        break;

                    case Screens.Tiro:

                        retorno = await report.GetTiroReport();

                        break;
                }

                if (retorno == null)
                {
                    var noPesada = new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.ReportNoOperacion));
                    noPesada.OnAcceptPress += (Boolean IsCantidad, Single Box, Single Cantidad) =>
                    {
                        Finish();
                    };
                    return;
                }

                plotView.Model = retorno.plotModel1;
                plotView2.Model = retorno.plotModel2;

                if (throwThread)
                {
                    await Task.Run(async () =>
                      {
                          await ChangeLayout();
                      });
                }
            }
            catch (WebException wEx)
            {
                var webExceptionManager = new WebExceptionManager(wEx);
                var mensajeError = webExceptionManager.ClasificarExcepcionWeb();
                var noPesada = new CustomDialog(this, CustomDialog.Status.Error, mensajeError);
                noPesada.OnAcceptPress += (Boolean IsCantidad, Single Box, Single Cantidad) =>
                {
                    Finish();
                };
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async Task ChangeLayout()
        {
            Screens myscreen = Screen;

            await Task.Delay(15000);

            if (Finished) return;

            if (myscreen != Screen) return;

            switch (Screen)
            {
                case Screens.Peso:
                case Screens.Diametro:
                    myscreen = Screen + 1;

                    break;

                case Screens.Tiro:
                    myscreen = Screens.Peso;

                    break;
            }

            RunOnUiThread(() =>
            {
                BindingReport(myscreen);
            });
        }

        public override void Finish()
        {
            base.Finish();
            Finished = true;
        }
    }
}