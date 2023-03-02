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
using ControlConsumo.Shared.Repositories;
using ControlConsumo.Droid.Activities.Adapters;
using ControlConsumo.Shared.Models.R;
using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Shared.Models.Process;
using System.Threading.Tasks;
using Android.Support.V7.Widget;

namespace ControlConsumo.Droid.Activities.Widgets
{
    class CuadreDialog
    {
        private readonly RepositoryFactory Repo;
        public readonly Dialog dialog;
        private readonly Context context;
        private readonly _Accions Accion;
        private readonly ActualConfig Config;
        private readonly TextView txtViewHeader;
        private readonly TextView txtViewProduccionDate;
        private readonly TextView txtViewProducto;
        private readonly Button btnAceptDialog;
        private readonly Button btnAceptDialog2;
        private readonly Button btnCancelDialog;
        private readonly DateTime FechaProduccion;
        private readonly RecyclerView lstCuadre;
        private RecyclerView.LayoutManager layoutManager;
        private readonly ViewFlipper flipper_view;
        private readonly Byte TurnID;
        private ProcessList Proceso;
        private Stocks Stock;
        private Boolean CanClose;
        private CuadreAdapter Adapter { get; set; }

        public enum _Accions
        {
            OPEN,
            CLOSED,
            PARTIAL
        }

        public enum _Options
        {
            NONE,
            LAST_TRAY,
            WASTES,
            LAST_ONE
        }


        private _Options Option;

        public delegate void CancelPress();
        public delegate void AcceptPress(ProcessList Process, String msg);
        public delegate void DoLastAction(_Options Option);
        public event AcceptPress OnAcceptPress;
        public event DoLastAction OnDoLastAction;
        public event CancelPress OnCancelPress;

        public CuadreDialog(Context context, _Accions Accion, DateTime FechaProduccion, Byte TurnID, ActualConfig config, Stocks stock, ProcessList proceso = null, Boolean CanClose = false, _Options Option = _Options.NONE)
        {
            if (!Util.TryLock(context, Util.Locks.Turnos)) return;

            Repo = new RepositoryFactory(Util.GetConnection());

            this.context = context;
            this.Accion = Accion;
            this.FechaProduccion = FechaProduccion;
            this.Config = config;
            this.TurnID = TurnID;
            this.Stock = stock;
            this.Proceso = proceso;
            this.CanClose = CanClose;

            dialog = new Dialog(context, Android.Resource.Style.ThemeTranslucentNoTitleBar);
            dialog.SetContentView(Resource.Layout.dialog_cuadre);
            dialog.SetCancelable(false);

            var LayoutPass = dialog.FindViewById<LinearLayout>(Resource.Id.custom_dialog_first_rl);
            btnAceptDialog = dialog.FindViewById<Button>(Resource.Id.btnAceptDialog);
            btnAceptDialog2 = dialog.FindViewById<Button>(Resource.Id.btnAceptDialog2);
            btnCancelDialog = dialog.FindViewById<Button>(Resource.Id.btnCancelDialog);
            txtViewHeader = dialog.FindViewById<TextView>(Resource.Id.txtViewHeader);
            txtViewProduccionDate = dialog.FindViewById<TextView>(Resource.Id.txtViewProduccionDate);
            txtViewProducto = dialog.FindViewById<TextView>(Resource.Id.txtViewProducto);
            flipper_view = dialog.FindViewById<ViewFlipper>(Resource.Id.flipper_view);
            btnAceptDialog.Click += btnAceptDialog_Click;
            btnAceptDialog2.Click += btnAceptDialog2_Click;
            btnCancelDialog.Click += btnCancelDialog_Click;
            lstCuadre = dialog.FindViewById<RecyclerView>(Resource.Id.lstCuadre);
            var flipper_view_floor = dialog.FindViewById<ViewFlipper>(Resource.Id.flipper_view_floor);
            LayoutPass.Background = context.Resources.GetDrawable(Resource.Color.gray_base);
            LayoutPass.Background.SetAlpha(175);

            ProcessStock();
        }

        private async Task<ProcessList> GetProcess()
        {
            try
            {
                var Repoz = Repo.GetRepositoryZ();
                var proceso = await Repoz.GetProces();
                return proceso;
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex);
                throw;
            }
        }

        private async void ProcessStock()
        {
            try
            {
                if (Proceso == null)
                {
                    Proceso = await GetProcess();
                }
                if (Accion == _Accions.OPEN)
                    txtViewHeader.Text = context.GetString(Resource.String.DialogOpenCuadre);
                else
                {
                    if (Proceso.IsSubEquipment)
                    {
                        txtViewHeader.Text = context.GetString(Resource.String.DialogCloseCuadreProducts);
                    }
                    else
                    {
                        txtViewHeader.Text = context.GetString(Resource.String.DialogCloseCuadre);
                    }
                }

                var strturn = context.GetString(Resource.String.Turn);

                if (Stock != null && Stock.Status == Stocks._Status.Abierto)
                    txtViewProduccionDate.Text = String.Format("{0} - {1} {2}", Stock.Begin.ToLocalTime().ToString("MMM dd, yyyy"), strturn, Stock.TurnID);
                else
                    txtViewProduccionDate.Text = String.Format("{0} - {1} {2}", FechaProduccion.ToString("MMM dd, yyyy"), strturn, TurnID);

                if (Config != null)
                {
                    txtViewProducto.Text = Config._ProductName;
                }

                LoadMaterial(FechaProduccion, Stock == null ? TurnID : Stock.TurnID);

                /*if (!this.proceso.IsSubEquipment)
                {
                    LoadMaterial(FechaProduccion, stock == null ? TurnID : stock.TurnID);
                }*/

                try
                {
                    dialog.Show();
                }
                catch (Exception e)
                {
                    await Util.SaveException(e);
                }

                Option++;

                if (!CanClose && Accion == _Accions.CLOSED && Option == _Options.LAST_TRAY)
                {
                    await ShowMessagesAsync(_Options.LAST_TRAY);
                    return;
                }

                if (!CanClose && Accion == _Accions.CLOSED && Option == _Options.WASTES)
                {
                    await ShowMessagesAsync(_Options.WASTES);
                }
            }
            catch (Exception ex)
            {
                await Util.SaveException(ex);
                throw;
            }
        }

        private Boolean IsReady()
        {
            Boolean NoMaterial = false;

            if (Adapter != null && Adapter.Materiales != null && Adapter.Materiales.Any())
            {
                NoMaterial = Adapter.Materiales.All(p => p.Quantity == 0);
            }

            if (NoMaterial && Accion == _Accions.CLOSED)
            {
                Dialog ldialog = null;
                var view = LayoutInflater.From(context).Inflate(Resource.Layout.question_dialog, null);
                view.FindViewById<TextView>(Resource.Id.txtQuestion).Text = context.GetString(Resource.String.AlertEmptyCuadre);
                var builder = new AlertDialog.Builder(context);
                builder.SetView(view);
                builder.SetIcon(Resource.Drawable.ic_settings);
                builder.SetTitle(Resource.String.ApplicationLabel);
                builder.SetCancelable(false);
                builder.SetPositiveButton(Resource.String.Yes, (se, ee) =>
                {
                    PostCuadre();
                });

                builder.SetNegativeButton(Resource.String.No, (se, ee) =>
                {
                    ldialog.Dismiss();
                    ldialog.Dispose();
                });

                ldialog = builder.Create();
                ldialog.Show();

                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnAceptDialog2_Click(object sender, EventArgs e)
        {
            if (IsReady()) PostCuadre();
        }

        private void btnCancelDialog_Click(object sender, EventArgs e)
        {
            dialog.Dismiss();
            dialog.Dispose();
            if (OnCancelPress != null)
                OnCancelPress.Invoke();
        }

        private async void LoadMaterial(DateTime FechaProduccion, Byte TurnID)
        {
            var Repoz = Repo.GetRepositoryZ();
            var repoMaterials = Repo.GetRepositoryMaterials();
            var repoMaterialZSilms = Repo.GetRepositoryMaterialZilm();

            var Materiales = new List<MaterialReport>();

            if (Accion == _Accions.CLOSED)
            {
                if (Proceso.IsSubEquipment)
                {
                    var materialReport = new MaterialReport();

                    //Se agrega la última bandeja consumida por el equipo de entrada.
                    var consumo = await Repoz.GetLastBandejaEntrada(false);
                    if (consumo != null)
                    {
                        var material = new Materials();
                        material = await repoMaterials.GetAsyncByKey(consumo.ProductCode);
                        if (material != null)
                        {
                            materialReport.MaterialName = material.Name;
                            materialReport.MaterialUnit = material.Unit;

                            materialReport.ID = consumo.ID;
                            materialReport.TrayID = consumo.TrayID;
                            materialReport.CustomID = consumo.CustomID;
                            var Secuencia = new string(consumo.TrayID.Where(Char.IsDigit).ToArray());
                            if (Secuencia.Length > 0)
                            {
                                materialReport.BoxNumber = Convert.ToInt16(Secuencia);
                                materialReport.Lot = consumo.BatchID;
                            }

                            materialReport.BatchID = consumo.BatchID;
                            materialReport.MaterialCode = consumo.ProductCode;
                            materialReport.ProductCode = consumo.ProductCode;
                            materialReport.TurnID = consumo.TurnID;
                            materialReport.VerID = consumo.VerID;

                            var materialZSilm = new MaterialsZilm();
                            materialZSilm = await repoMaterialZSilms.GetAsyncByKey(consumo.ProductCode);
                            if (materialZSilm != null)
                            {
                                materialReport.NeedPercent = materialZSilm.Percent;
                                materialReport.EntryQuantity = consumo.Quantity;
                                materialReport.Acumulated = consumo.Quantity;
                                materialReport.Unit = consumo.Unit;
                            }
                            Materiales.Add(materialReport);
                        }
                    }
                }
                else
                {
                    if (!Proceso.EquipmentID.StartsWith("S"))
                    {
                        Materiales = await Repoz.GetLastMaterial(Config.ProductCode, Config.VerID);

                        //Agregando la última bandeja consumida al listado de materiales consumidos en el turno.
                        var consumo = await Repoz.GetLastBandejaEntrada();

                        if (consumo != null)
                        {
                            var materialReport = new MaterialReport();
                            var material = new Materials();
                            material = await repoMaterials.GetAsyncByKey(consumo.MaterialCode);
                            if (material != null)
                            {
                                materialReport.MaterialName = material.Name;
                            }
                            materialReport.MaterialUnit = material.Unit;
                            materialReport.ID = consumo.ID;
                            materialReport.TrayID = consumo.TrayID;
                            materialReport.CustomID = consumo.CustomID;
                            var Secuencia = new string(consumo.TrayID.Where(Char.IsDigit).ToArray());
                            if (Secuencia.Length > 0)
                            {
                                materialReport.BoxNumber = Convert.ToInt16(Secuencia);
                                materialReport.Lot = consumo.BatchID;
                            }

                            materialReport.BatchID = consumo.BatchID;
                            materialReport.MaterialCode = consumo.MaterialCode;
                            materialReport.ProductCode = consumo.ProductCode;
                            materialReport.TurnID = consumo.TurnID;
                            materialReport.VerID = consumo.VerID;

                            var materialZSilm = new MaterialsZilm();
                            materialZSilm = await repoMaterialZSilms.GetAsyncByKey(consumo.MaterialCode);
                            if (materialZSilm != null)
                            {
                                materialReport.NeedPercent = materialZSilm.Percent;
                                var hopperCigarsQuantity = Repoz.GetHopperCigarsQuantity(Proceso.Process, Config.TimeID);
                                materialReport.EntryQuantity = consumo.Quantity + hopperCigarsQuantity;
                                materialReport.Acumulated = consumo.Quantity + hopperCigarsQuantity;
                                materialReport.Unit = consumo.Unit;
                            }
                            Materiales.Add(materialReport);
                        }
                    }
                }
            }
            else
                Materiales = await Repoz.GetInitialMaterial();

            if (Materiales.Any())
            {
                flipper_view.DisplayedChild = 0;

                layoutManager = new LinearLayoutManager(context);
                lstCuadre.SetLayoutManager(layoutManager);

                Adapter = new CuadreAdapter(context, Materiales, Accion == _Accions.OPEN, Proceso.IsSubEquipment);
                lstCuadre.SetAdapter(Adapter);
            }
            else
                flipper_view.DisplayedChild = 1;
        }

        private void btnAceptDialog_Click(object sender, EventArgs e)
        {
            if (IsReady()) PostCuadre();
        }

        private async void PostCuadre()
        {
            var process = await Repo.GetRepositoryZ().GetProces();
            var repos = Repo.GetRepositoryStocks();
            var repoz = Repo.GetRepositoryZ();
            var reposecu = Repo.GetRepositoryCustomSecuences();

            try
            {
                if (Accion == _Accions.OPEN)
                {
                    Stock = new Stocks()
                    {
                        Begin = FechaProduccion,
                        Status = Stocks._Status.Abierto,
                        Logon = process.Logon,
                        CustomFecha = FechaProduccion.GetDBDate(),
                        Center = process.Centro,
                        ProcessID = process.Process,
                        TimeID = Config.TimeID,
                        TurnID = TurnID,
                        VerID = Config.VerID,
                        Sync = true,
                        ProductCode = Config.ProductCode,
                        Equipment = process.EquipmentID,
                        SubEquipment = Config.SubEquipment,
                        IsNotified = false
                    };

                    var stock = await repoz.ExistClosedStockAsync(FechaProduccion, TurnID);

                    if (stock != null)
                    {
                        stock.Status = Stocks._Status.Abierto;
                        await repos.UpdateAsync(stock);
                    }
                    else if (stock == null)
                    {
                        await repos.InsertAsync(Stock);
                    }
                }
                else
                {
                    Stock.End = FechaProduccion;
                    Stock.Status = Stocks._Status.Cerrado;
                    Stock.Sync = true;
                    Stock.ProductCode = Config.ProductCode;
                    Stock.VerID = Config.VerID;
                    Stock.TimeID = Config.TimeID;
                    Stock.SubEquipment = Config.SubEquipment;
                    Stock.IsNotified = false;

                    if (Stock.CustomID == 0)
                    {
                        var stock = await repoz.ExistClosedStockAsync(FechaProduccion, Stock.TurnID);
                        if (stock != null)
                        {
                            Stock.CustomID = stock.CustomID;
                        }
                    }

                    if (Adapter != null)
                    {
                        var repodetalle = Repo.GetRepositoryStocksDetails();

                        var Materiales = Adapter.Materiales.ToList();

                        var buffer = new List<StocksDetails>();

                        for (int i = 0; i < Materiales.Count; i++)
                        {
                            buffer.Add(new StocksDetails
                            {
                                BoxNumber = Materiales[i].BoxNumber,
                                StockID = Stock.ID,
                                MaterialCode = Materiales[i].MaterialCode,
                                Quantity = !Materiales[i].NeedPercent ? Materiales[i].Quantity : Materiales[i].Quantity * (Materiales[i].EntryQuantity > 0 ? Materiales[i].EntryQuantity : Materiales[i].Acumulated),
                                Quantity2 = Materiales[i].EntryQuantity,
                                Unit = Materiales[i].MaterialUnit,
                                Lot = Materiales[i].Lot
                            });
                        }

                        await repodetalle.InsertAsyncAll(buffer);

                        ///Comentado ya no se borrar los materiales que no se le den captura.

                        //Materiales = Adapter.Materiales.Where(p => p.Quantity == 0).ToList();

                        //foreach (var item in Materiales)
                        //{
                        //    await reposecu.DeleteAsync(new CustomSecuences()
                        //    {
                        //        MaterialCode = item.MaterialCode
                        //    });
                        //}
                    }

                    await repos.UpdateAsync(Stock);

                    var repoInventories = Repo.GetRepositoryTransactions();
                    var Inventories = await repoz.GetAvailableStock();
                    var bufferTransations = new List<Transactions>();
                    var turnos = await repoz.GetAllTurnsAsync();

                    foreach (var item in Inventories)
                    {
                        var unidad = await repoz.GetMaterialByCodeAsync(item.MaterialCode);

                        var turno = turnos.SingleOrDefault(s => s.ID == Stock.TurnID);

                        var dewdate = turno != null ? new DateTime(Stock.Begin.Year, Stock.Begin.Month, Stock.Begin.Day, turno.EndH - 1, 59, 59) : DateTime.Now;

                        if (unidad != null)
                        {
                            var newt = new Transactions()
                            {
                                BoxNumber = item.BoxNumber,
                                CustomFecha = Stock.CustomID,
                                Fecha = dewdate,
                                Logon = process.Logon,
                                Lot = item.Lot,
                                MaterialCode = item.MaterialCode,
                                Quantity = item.Quantity * -1,
                                Reason = context.GetString(Resource.String.ReceiptClose),
                                Sync = true,
                                TurnID = Stock.TurnID,
                                Unit = unidad.Unit
                            };

                            bufferTransations.Add(newt);
                        }
                    }

                    await repoInventories.InsertAsyncAll(bufferTransations);
                }

                if (OnAcceptPress != null)
                {
                    OnAcceptPress.Invoke(process, String.Empty);
                }

                dialog.Dismiss();
                dialog.Dispose();
            }
            catch (Exception e)
            {
                await Util.SaveException(e);
            }
        }

        private async Task ShowMessagesAsync(_Options Option)
        {
            Dialog ldialog = null;
            String msg = String.Empty;

            try
            {
                switch (Option)
                {
                    case _Options.LAST_TRAY:
                        msg = context.GetString(Resource.String.AlertLastTray);

                        break;

                    case _Options.WASTES:
                        msg = context.GetString(Resource.String.AlertDeperdicios);

                        break;
                }

                var view = LayoutInflater.From(context).Inflate(Resource.Layout.question_dialog, null);
                view.FindViewById<TextView>(Resource.Id.txtQuestion).Text = msg;
                var builder = new AlertDialog.Builder(context);
                builder.SetView(view);
                builder.SetIcon(Resource.Drawable.ic_settings);
                builder.SetTitle(Resource.String.ApplicationLabel);
                builder.SetCancelable(false);
                builder.SetPositiveButton(Resource.String.Yes, (se, ee) =>
                {
                    if (OnDoLastAction != null) OnDoLastAction.Invoke(Option);

                    ldialog.Dismiss();
                    ldialog.Dispose();
                    dialog.Dismiss();
                    dialog.Dispose();
                });

                builder.SetNegativeButton(Resource.String.No, async (se, ee) =>
                {
                    Option++;

                    if (Option != _Options.LAST_ONE)
                        await ShowMessagesAsync(Option);

                    try
                    {
                        ldialog.Dismiss();
                        ldialog.Dispose();
                    }
                    catch (Java.Lang.NullPointerException)
                    { }
                    catch (Exception)
                    {
                        throw;
                    }
                });

                ldialog = builder.Create();
                try
                {
                    ldialog.Show();
                }
                catch (WindowManagerBadTokenException ex)
                {
                    await Util.SaveException(ex);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}