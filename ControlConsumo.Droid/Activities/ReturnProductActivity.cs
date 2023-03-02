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
using System.Globalization;
using ControlConsumo.Droid.Activities.Adapters;
using ControlConsumo.Shared.Tables;
using Android.Content.PM;
using System.Threading;

namespace ControlConsumo.Droid.Activities
{
    [Activity(ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop)]
    public class ReturnProductActivity : BaseActivity
    {
        private Byte TurnoID;
        private DateTime ProductionDate;
        private CachingManager caching;
        private TextView txtViewReturnProductTitle;
        private ListView listDevolucion;
        private ReturnProductionAdapter adapter;
        private SecurityManager security;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.return_product_activity);

            if (listDevolucion == null)
            {
                security = new SecurityManager(this);
                caching = new CachingManager(this);
                TurnoID = Convert.ToByte(Intent.GetStringExtra(CustExtras.TurnID.ToString()));
                ProductionDate = DateTime.ParseExact(Intent.GetStringExtra(CustExtras.ProductionDate.ToString()), "yyyyMMdd", CultureInfo.InvariantCulture);
                txtViewReturnProductTitle = FindViewById<TextView>(Resource.Id.txtViewReturnProductTitle);
                listDevolucion = FindViewById<ListView>(Resource.Id.listDevolucion);
                txtViewReturnProductTitle.Text = String.Format(GetString(Resource.String.ReturnProductTitle), TurnoID, ProductionDate.ToString("dd MMMM yyyy"));
                ThreadPool.QueueUserWorkItem(o => LoadProduct());
            }
            // Create your application here
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
                        security.Response += (arg1, arg2) =>
                        {
                            if (arg1)
                            {
                                Save();
                            }
                        };
                        security.HaveAccess(RolsPermits.Permits.DEVOLUCION_PRODUCTO);

                        break;
                }
            }
            catch (Exception ex)
            {
                CatchException(ex);
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        private async void LoadProduct()
        {
            var elaborates = await repo.GetRepositoryZ().GetElaborates();
            adapter = new ReturnProductionAdapter(this, elaborates);
            RunOnUiThread(() =>
            {
                listDevolucion.Adapter = adapter;
            });
        }

        private async void Save()
        {
            try
            {
                ShowProgress(true);

                var repoRoutes = repo.GetRepositoryProductsRoutes();
                var repoz = repo.GetRepositoryZ();
                var adevolver = adapter.Elaborates.Where(p => p.IsActive).ToList();
                await repoz.SetListAsReturn(adevolver.Select(p => p.ID).ToList());
                var process = await repoz.GetProces();
                var usuario = SecurityManager.CurrentProcess != null ? SecurityManager.CurrentProcess.Logon : process.Logon;

                var buffer = new List<TraysProducts>();
                var bufferProducts = new List<ProductsRoutes>();

                foreach (var item in adevolver)
                {
                    if (!String.IsNullOrEmpty(item.TrayID))
                    {
                        var barcode = item.TrayID.GetBarCode();

                        var configuracionBandeja = await repo.GetRepositoryTrays().GetAsyncByKey(barcode.BarCode);
                        
                        if (configuracionBandeja != null)
                        {
                            if (configuracionBandeja.procesarSAP)
                            {
                                buffer.Add(new TraysProducts()
                                {
                                    ID = barcode.ID,
                                    TrayID = barcode.BarCode,
                                    Secuencia = barcode.Sequence,
                                    Status = TraysProducts._Status.Vacio,
                                    ProductCode = String.Empty,
                                    VerID = String.Empty,
                                    TimeID = String.Empty,
                                    Quantity = 0,
                                    Unit = String.Empty,
                                    BatchID = String.Empty,
                                    EquipmentID = String.Empty,
                                    ElaborateID = 0,
                                    Sync = configuracionBandeja.procesarSAP,
                                    Fecha = null,
                                    ModifyDate = DateTime.Now,
                                    FechaHoraVaciada = DateTime.Now,
                                    UsuarioVaciada = usuario,
                                    IdEquipoVaciado = process.EquipmentID,
                                    formaVaciada = "Ajuste de salida"
                                });
                            }
                            else
                            {
                                var trayList = await repoz.GetEstatusBandeja(barcode.BarCode, barcode.Sequence);

                                if (trayList.Status == TraysProducts._Status.Lleno && trayList.ProductCode.Equals(item.ProductCode) && trayList.BatchID.Equals(item.BatchID))
                                {
                                    buffer.Add(new TraysProducts()
                                    {
                                        ID = barcode.ID,
                                        TrayID = barcode.BarCode,
                                        Secuencia = barcode.Sequence,
                                        Status = TraysProducts._Status.Vacio,
                                        ProductCode = String.Empty,
                                        VerID = String.Empty,
                                        TimeID = String.Empty,
                                        Quantity = 0,
                                        Unit = String.Empty,
                                        BatchID = String.Empty,
                                        EquipmentID = String.Empty,
                                        ElaborateID = 0,
                                        Sync = configuracionBandeja.procesarSAP,
                                        Fecha = null,
                                        ModifyDate = DateTime.Now,
                                        FechaHoraVaciada = DateTime.Now,
                                        UsuarioVaciada = usuario,
                                        IdEquipoVaciado = process.EquipmentID,
                                        formaVaciada = "Ajuste de salida"
                                    });
                                }
                            }
                        }
                    }

                    var route = await repoz.LoadRoute(process.EquipmentID, item.ElaborateID, item.Produccion);

                    if (route != null)
                    {
                        route.Status = ProductsRoutes.RoutesStatus.Devuelto;
                        route.Sync = true;
                        bufferProducts.Add(route);
                    }
                }


                if (buffer.Any())
                {
                    await repo.GetRepositoryTraysProducts().UpdateAllAsync(buffer.Where(s=>s.Sync));
                    await repo.GetRepositoryTraysProducts().InsertOrUpdateAsyncSql(buffer.Where(s => !s.Sync).ToArray()); //Guardando datos en línea con SQL
                }

                if (bufferProducts.Any()) await repoRoutes.UpdateAllAsync(bufferProducts);

                if (adevolver.Any())
                {
                    Toast.MakeText(this, Resource.String.ReturnProductResult, ToastLength.Long).Show();
                }

                LoadProduct();
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
    }
}