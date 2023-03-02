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
using System.Globalization;
using ControlConsumo.Shared.Tables;
using Android.Content.PM;
using Android.Support.V7.Widget;
using ControlConsumo.Droid.Activities.Adapters.Entities;

namespace ControlConsumo.Droid.Activities
{
    [Activity(ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop, WindowSoftInputMode = SoftInput.AdjustPan)]
    public class CloseActivity : BaseActivity
    {
        private RecyclerView listCierres;
        private RecyclerView.LayoutManager layoutManager;
        private CuadreAdapterList Adapter { get; set; }
        private Boolean IsNotify;
        private Byte TurnID;
        private DateTime Produccion;
        private TextView txtViewTitle;
        private TextView txtViewProductoLarge;      
        private Stocks stock { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            listCierres = FindViewById<RecyclerView>(Resource.Id.listCierres);

            SetContentView(Resource.Layout.cierre_activity);
            SetTitle(Resource.String.ApplicationLabel);

            if (listCierres == null)
            {
                txtViewTitle = FindViewById<TextView>(Resource.Id.txtViewTitle);
                txtViewProductoLarge = FindViewById<TextView>(Resource.Id.txtViewProductoLarge);
                IsNotify = Intent.GetBooleanExtra(CustExtras.IsNotify.ToString(), false);
                TurnID = Convert.ToByte(Intent.GetStringExtra(CustExtras.TurnID.ToString()));
                Produccion = DateTime.ParseExact(Intent.GetStringExtra(CustExtras.ProductionDate.ToString()), "yyyyMMdd", CultureInfo.InvariantCulture);
                listCierres = FindViewById<RecyclerView>(Resource.Id.listCierres);
                txtViewTitle.Text = string.Format(GetString(Resource.String.CloseTitle), TurnID, Produccion.ToString("dd MMMM yyyy"));
                LoadCierres();
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

                    Save();

                    break;
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        private async void LoadCierres()
        {
            try
            {
                String ProductCode = String.Empty;
                var repoz = repo.GetRepositoryZ();
                stock = await repoz.ExistClosedStockAsync(Produccion, TurnID);

                if (!String.IsNullOrEmpty(stock.ProductCode))
                {
                    ProductCode = stock.ProductCode;
                }
                else
                {
                    var config = await repoz.GetConfigByProduction(Produccion);
                    ProductCode = config.ProductCode;
                }

                var material = repoz.GetMaterialByCode(ProductCode);
                txtViewProductoLarge.Text = material._ProductName;
                var materials = await repoz.GetMaterialStockAsync(Produccion, TurnID, stock);

                layoutManager = new LinearLayoutManager(this);
                listCierres.SetLayoutManager(layoutManager);

                Adapter = new CuadreAdapterList(this, materials, IsNotify);
                listCierres.SetAdapter(Adapter);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void Save()
        {
            try
            {
                ShowProgress(true);               

                var repoz = repo.GetRepositoryZ();              

                var materiales = Adapter.lista.Where(p => p.EntryQuantity > 0).ToList();

                var bufferToInsert = new List<StocksDetails>();
                var bufferToUpdate = new List<StocksDetails>();

                foreach (var item in materiales)
                {
                    var detalle = await repoz.GetStockDetail(stock.ID, item.MaterialCode);

                    if (detalle != null)
                    {
                        detalle.Quantity = !item.NeedPercent ? item.EntryQuantity : item.EntryQuantity * item.Acumulated;
                        bufferToUpdate.Add(detalle);
                    }
                    else
                    {
                        bufferToInsert.Add(new StocksDetails()
                        {
                            BoxNumber = item.BoxNumber,
                            StockID = stock.ID,
                            MaterialCode = item.MaterialCode,
                            Quantity = !item.NeedPercent ? item.EntryQuantity : item.EntryQuantity * item.Acumulated,
                            Quantity2 = item.Acumulated,
                            Unit = item.MaterialUnit,
                            Lot = item.Lot
                        });
                    }
                }

                var repoStock = repo.GetRepositoryStocks();
                var repoDetalle = repo.GetRepositoryStocksDetails();

                stock.Sync = true;

                if (String.IsNullOrEmpty(stock.ProductCode))
                {
                    var config = await repoz.GetConfigByProduction(Produccion);

                    stock.VerID = config.VerID;
                    stock.TimeID = config.TimeID;
                    stock.SubEquipment = config.SubEquipmentID;
                    stock.ProductCode = config.ProductCode;
                }

                await repoStock.UpdateAsync(stock);
                await repoDetalle.UpdateAllAsync(bufferToUpdate);
                await repoDetalle.InsertAsyncAll(bufferToInsert);

                Toast.MakeText(this, Resource.String.CloseResult, ToastLength.Long).Show();
                LoadCierres();
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