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
using System.Threading.Tasks;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using System.Globalization;
using ControlConsumo.Droid.Activities.Widgets;
using Android.Content.PM;
using System.Threading;
using ControlConsumo.Shared.Models.Z;

namespace ControlConsumo.Droid.Activities
{
    [Activity(ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop, WindowSoftInputMode = SoftInput.AdjustPan)]
    public class ReportVarillaActivity : BaseActivity
    {
        private TextView txtViewTitle;
        private ListView lstVarillas;
        private VarillaAdapter varillaAdapter;
        private Int32 ProductionDate;
        private DateTime Fecha;
        private Byte TurnID;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.reporte_varillas_activity);
            SetTitle(Resource.String.ApplicationLabel);

            if (lstVarillas == null)
            {
                txtViewTitle = FindViewById<TextView>(Resource.Id.txtViewTitle);
                lstVarillas = FindViewById<ListView>(Resource.Id.lstVarillas);
                ProductionDate = Intent.GetIntExtra(CustExtras.ProductionDate.ToString(), 0);
                TurnID = Convert.ToByte(Intent.GetStringExtra(CustExtras.TurnID.ToString()));
                Fecha = DateTime.Now.AddDays(ProductionDate);
                txtViewTitle.Text = String.Format(GetString(Resource.String.VarillaTitle), TurnID, Fecha.ToString("dd MMMM yyyy"));
                ThreadPool.QueueUserWorkItem(o => LoadVarillas());
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
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

                    PostWastes();

                    break;
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        public async void PostWastes()
        {
            try
            {
                ShowProgress(true);

                var repoWaste = repo.GetRepositoryWastes();
                var repoz = repo.GetRepositoryZ();
                var Proceso = SecurityManager.CurrentProcess;
                var materiales = varillaAdapter.Materiales.Where(p => p.Quantity > 0).ToList();
                foreach (var item in materiales.ToList())
                {
                    var materialVarilla = await repoz.GetMaterialByCodeOrReferenceAsync(item.MaterialCode2 ?? item.MaterialCode);
                    var lote = await repoz.GetLoteForMaterialAsync(materialVarilla.Code, item.Lot);
                    if (lote != null && !String.IsNullOrEmpty(lote.Reference))
                    {
                        item.LoteSuplidor = lote.Reference;
                        item.Expire = lote.Expire;
                    }
                }
                if (materiales.Any())
                {
                    var buffer = materiales.Select(p => new Wastes
                    {
                        TurnID = TurnID,
                        StockID = TurnID,
                        BoxNumber = p.BoxNumber,
                        Center = Proceso.Centro,
                        CustomFecha = Convert.ToInt32(Fecha.GetSapDate()),
                        Fecha = Fecha,
                        Logon = Proceso.Logon,
                        Lot = p.Lot,
                        LoteSuplidor = p.LoteSuplidor,
                        MaterialCode = p.MaterialCode,
                        MaterialCode2 = p.MaterialCode2,
                        ProcessID = Proceso.Process,
                        ProductCode = p.ProductCode,
                        Quantity = p.Quantity,
                        Sync = true,
                        Unit = p.MaterialUnit ?? p.Unit,
                        VerID = p.VerID,
                        ID = p.ID
                    }).ToList();

                    foreach (var item in buffer)
                    {
                        var materialVarilla = await repoz.GetMaterialByCodeOrReferenceAsync(item.MaterialCode2 ?? item.MaterialCode);
                        var lote = await repoz.GetLoteForMaterialAsync(materialVarilla.Code, item.Lot);
                        if (lote != null && !String.IsNullOrEmpty(lote.Reference))
                        {
                            item.LoteSuplidor = lote.Reference;
                            item.Expire = lote.Expire;
                            item.BoxNumber = (short)await repoz.Get_Next_Sequence(item.MaterialCode2 ?? item.MaterialCode, item.Lot, item.LoteSuplidor, 0);
                            materiales.Where(x => x.ID == item.ID).FirstOrDefault().BoxNumber = item.BoxNumber;
                        }

                    }
                    var proceso = await repoz.GetProces();
                    await repoz.Post_Secuences(buffer.Select(s => new PostSecuenceRequest
                    {
                        MATNR = s.MaterialCode,
                        CHARG = s.Lot,
                        LICHA = s.LoteSuplidor,
                        WERKS = proceso.Centro,
                        ESTADO = "A",
                        CREADOR = SecurityManager.CurrentProcess.Logon,
                        EMPAQUENO = s.BoxNumber
                    }).ToList());

                    await repoWaste.InsertAsyncAll(buffer);

                    var msg = GetString(Resource.String.VarillaMessages);

                    var customDialog = new CustomDialog(this, CustomDialog.Status.Good, msg, CustomDialog.ButtonStyles.TwoButtonWithPrint);
                    customDialog.OnAcceptPress += customDialog_OnAcceptPress;
                    customDialog.OnCancelPress += customDialog_OnCancelPress;

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
        }

        private async void customDialog_OnAcceptPress(bool isCantidad, float Box, Single Cantidad)
        {
            var repoz = repo.GetRepositoryZ();
            var reposilm = repo.GetRepositoryMaterialZilm();
            var materiales = varillaAdapter.Materiales.Where(p => p.Quantity > 0).ToList();
            var printer = new PrinterManager();
            var lista = new List<Etiquetas>();

            foreach (var material in materiales)
            {
                var etiqueta = new Etiquetas()
                {
                    Cantidad = 1,
                    Descripcion = material.MaterialName,
                    Medida = (decimal)material.Quantity,
                    Unidad = material.MaterialUnit,
                    Codigo = material.MaterialReference,
                    Material = material._MaterialCode,
                    LoteInterno = material.Lot,
                    LoteSuplidor = material.LoteSuplidor,
                    Fecha = material.Expire
                };

                //etiqueta.Secuencia = await repoz.Get_Next_Secuence(material.MaterialCode, etiqueta.LoteInterno, etiqueta.LoteSuplidor, 0);
                etiqueta.Secuencia = material.BoxNumber;

                /*var zmaterial = await reposilm.GetAsyncByKey(material.MaterialCode); // El cliente ya no maneja los materiales de esa forma. Actualmente los materiales se vencen ese mismo día. 
                if (zmaterial != null)
                {
                    etiqueta.Fecha = DateTime.Now.AddDays(zmaterial.Days).Date;
                }*/

                lista.Add(etiqueta);
            }

            await printer.ExecutePrint(this, lista);

            LoadVarillas();
        }

        private void customDialog_OnCancelPress()
        {
            LoadVarillas();
        }

        private async void LoadVarillas()
        {
            var repoz = repo.GetRepositoryZ();
            var fecha = Convert.ToInt32(Fecha.GetSapDate());
            var varillas = await repoz.GetVarillas(fecha, TurnID);

            varillaAdapter = new VarillaAdapter(this, varillas, TurnID, fecha);
            RunOnUiThread(() =>
            {
                lstVarillas.Adapter = varillaAdapter;
            });
        }
    }
}