using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using ControlConsumo.Droid.Activities.Adapters;
using ControlConsumo.Droid.Activities.Adapters.Entities;
using ControlConsumo.Droid.Activities.Bundles;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Shared.Models.Elaborate;
using ControlConsumo.Shared.Models.Process;
using ControlConsumo.Shared.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ControlConsumo.Droid.Activities
{
    [Activity(ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop)]
    public class ReportActivity : BaseActivity
    {
        private const String _USER = "ReportActivity.USER";
        private const String _PROCESO = "ReportActivity.PROCESO";
        private const String DateMask = "dd/MM/yy";
        private const String TimeMask = "hh:mm tt";
        private ViewFlipper viewFlipper { get; set; }
        private ListView listView { get; set; }
        private ListView listView2 { get; set; }
        private GridView gridView { get; set; }
        private Spinner spnEquipment { get; set; }
        private CheckBox chkTurn1 { get; set; }
        private CheckBox chkTurn2 { get; set; }
        private CheckBox chkTurn3 { get; set; }
        private TextView txtViewFechaFrom { get; set; }
        private TextView txtViewFechaTo { get; set; }
        private ImageButton btnDate1 { get; set; }
        private ImageButton btnDate2 { get; set; }
        private Spinner spnProducto { get; set; }
        private Spinner spnBatchID { get; set; }
        private TextView txtViewHoraFrom { get; set; }
        private TextView txtViewHoraTo { get; set; }
        private TextView txtViewTotal { get; set; }
        private TextView txtViewFull { get; set; }
        private TextView txtViewInEquipment { get; set; }
        private TextView txtViewRelease { get; set; }
        private ImageButton btnTime1 { get; set; }
        private ImageButton btnTime2 { get; set; }
        private List<ElaboratesReport> Results { get; set; }
        private IEnumerable<Equipments> Equipos { get; set; }
        private IEnumerable<String> BatchIDs { get; set; }
        private IEnumerable<Materials> materiales { get; set; }

        private IEnumerable<ReportEntry> _reportEntries { get; set; }
        private Screen _Screen { get; set; }

        private TextView txtViewProduccion;
        private TextView txtViewTurn;
        private TextView txtViewMaterial;
        private TextView txtViewBatchID;
        private TextView txtViewBandejas;
        private TextView txtViewPeso;
        private TextView txtViewHora;
        private TextView txtViewEquipo;
        private TextView txtViewFecha1;
        private TextView txtViewHora1;
        private TextView txtViewBandejaConsumo;

        private enum Screen
        {
            General = 0,
            ListView = 1,
            ListViewWithHead = 2
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.reports_general_activity);
            SetTitle(Resource.String.ApplicationLabel);

            if (viewFlipper == null)
            {
                viewFlipper = FindViewById<ViewFlipper>(Resource.Id.viewFlipper);
                listView = FindViewById<ListView>(Resource.Id.listView);
                listView2 = FindViewById<ListView>(Resource.Id.listView2);
                gridView = FindViewById<GridView>(Resource.Id.gridView);
                spnEquipment = FindViewById<Spinner>(Resource.Id.spnEquipment);
                chkTurn1 = FindViewById<CheckBox>(Resource.Id.chkTurn1);
                chkTurn2 = FindViewById<CheckBox>(Resource.Id.chkTurn2);
                chkTurn3 = FindViewById<CheckBox>(Resource.Id.chkTurn3);
                txtViewFechaFrom = FindViewById<TextView>(Resource.Id.txtViewFechaFrom);
                txtViewFechaTo = FindViewById<TextView>(Resource.Id.txtViewFechaTo);
                btnDate1 = FindViewById<ImageButton>(Resource.Id.btnDate1);
                btnDate2 = FindViewById<ImageButton>(Resource.Id.btnDate2);
                spnProducto = FindViewById<Spinner>(Resource.Id.spnProducto);
                spnBatchID = FindViewById<Spinner>(Resource.Id.spnBatchID);
                txtViewHoraFrom = FindViewById<TextView>(Resource.Id.txtViewHoraFrom);
                txtViewHoraTo = FindViewById<TextView>(Resource.Id.txtViewHoraTo);
                btnTime1 = FindViewById<ImageButton>(Resource.Id.btnTime1);
                btnTime2 = FindViewById<ImageButton>(Resource.Id.btnTime2);
                txtViewTotal = FindViewById<TextView>(Resource.Id.txtViewTotal);
                txtViewFull = FindViewById<TextView>(Resource.Id.txtViewFull);
                txtViewInEquipment = FindViewById<TextView>(Resource.Id.txtViewInEquipment);
                txtViewRelease = FindViewById<TextView>(Resource.Id.txtViewRelease);
                txtViewBandejaConsumo = FindViewById<TextView>(Resource.Id.txtViewBandejaConsumo);
                btnDate1.Click += btnDate1_Click;
                btnDate2.Click += btnDate2_Click;
                btnTime1.Click += btnTime1_Click;
                btnTime2.Click += btnTime2_Click;
                gridView.ItemClick += gridView_ItemClick;
                chkTurn1.Click += chkTurn1_Click;
                chkTurn2.Click += chkTurn2_Click;
                chkTurn3.Click += chkTurn3_Click;
                spnEquipment.ItemSelected += spnEquipment_ItemSelected;

                ThreadPool.QueueUserWorkItem(async o => await PreloadEquipments());

                _reportEntries = new List<ReportEntry>
                {
                    new ReportEntry{  Type = ReportEntry.EntryTypes.ReporteTickets, Imagen = Resource.Drawable.ic_ticket, Title = GetString(Resource.String.ReportTitleTicket) },
                    new ReportEntry{ Type = ReportEntry.EntryTypes.ReporteProduction, Imagen = Resource.Drawable.ic_production, Title = GetString(Resource.String.ReportTitleProductionBandeja)}
                };

                gridView.Adapter = new ReportAdapter(this, _reportEntries);

                setHeaderList();
            }
        }

        private async void spnEquipment_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            await Search();
        }

        protected override async void OnSaveInstanceState(Bundle outState)
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                outState.PutString(_PROCESO, JsonConvert.SerializeObject(repoz.GetProces()));
                outState.PutString(_USER, JsonConvert.SerializeObject(repoz.GetUser()));
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
                    if (savedInstanceState.GetString(_USER) != null) repoz.SetUser(JsonConvert.DeserializeObject<Users>(savedInstanceState.GetString(_USER)));
                    if (savedInstanceState.GetString(_PROCESO) != null) repoz.SetProcess(JsonConvert.DeserializeObject<ProcessList>(savedInstanceState.GetString(_PROCESO)));
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void chkTurn3_Click(object sender, EventArgs e)
        {
            await Search();
        }

        private async void chkTurn2_Click(object sender, EventArgs e)
        {
            await Search();
        }

        private async void chkTurn1_Click(object sender, EventArgs e)
        {
            await Search();
        }

        public override void OnBackPressed()
        {
            switch (_Screen)
            {
                case Screen.General:
                    Finish();
                    break;

                default:
                    SetScreen(Screen.General);
                    break;
            }
        }

        private void btnTime2_Click(object sender, EventArgs e)
        {
            var str = !String.IsNullOrEmpty(txtViewHoraTo.Text) ? txtViewHoraTo.Text : DateTime.Now.ToString(TimeMask);
            var fecha = DateTime.ParseExact(str, TimeMask, CultureInfo.InvariantCulture);
            Boolean IsCanceled = true;
            var dialog = new TimePickerDialog(this, async (s, arg) =>
            {
                IsCanceled = false;
                txtViewHoraTo.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, arg.HourOfDay, arg.Minute, 1).ToString(TimeMask);
                await Search();
            }, fecha.Hour, fecha.Minute, false);

            dialog.DismissEvent += async (s, arg) =>
            {
                if (IsCanceled)
                {
                    txtViewHoraFrom.Text = String.Empty;
                    txtViewHoraTo.Text = String.Empty;
                    await Search();
                }
            };

            RunOnUiThread(() =>
            {
                dialog.Show();
            });
        }

        private void btnTime1_Click(object sender, EventArgs e)
        {
            var str = !String.IsNullOrEmpty(txtViewHoraFrom.Text) ? txtViewHoraFrom.Text : DateTime.Now.ToString(TimeMask);
            var fecha = DateTime.ParseExact(str, TimeMask, CultureInfo.InvariantCulture);
            Boolean IsCanceled = true;
            var dialog = new TimePickerDialog(this, async (s, arg) =>
            {
                IsCanceled = false;
                txtViewHoraFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, arg.HourOfDay, arg.Minute, 1).ToString(TimeMask);
                btnTime2.Enabled = true;
                await Search();
            }, fecha.Hour, fecha.Minute, false);

            dialog.DismissEvent += async (s, arg) =>
            {
                if (IsCanceled && String.IsNullOrEmpty(txtViewHoraTo.Text))
                {
                    txtViewHoraFrom.Text = String.Empty;
                    btnTime2.Enabled = false;
                    await Search();
                }
            };

            RunOnUiThread(() =>
            {
                dialog.Show();
            });
        }

        private void btnDate2_Click(object sender, EventArgs e)
        {
            var str = !String.IsNullOrEmpty(txtViewFechaTo.Text) ? txtViewFechaTo.Text : DateTime.Now.ToString(DateMask);
            var fecha = DateTime.ParseExact(str, DateMask, CultureInfo.InvariantCulture);
            Boolean IsCanceled = true;
            var Dialog = new DatePickerDialog(this, async (s, arg) =>
            {
                IsCanceled = false;
                txtViewFechaTo.Text = arg.Date.ToString(DateMask);
                await Search();
            }, fecha.Year, fecha.Month - 1, fecha.Day);

            Dialog.DismissEvent += async (s, arg) =>
            {
                if (IsCanceled)
                {
                    txtViewFechaFrom.Text = DateTime.Now.ToString(DateMask);
                    txtViewFechaTo.Text = String.Empty;
                    await Search();
                }
            };

            RunOnUiThread(() =>
            {
                Dialog.Show();
            });
        }

        private void btnDate1_Click(object sender, EventArgs e)
        {
            var fecha = DateTime.ParseExact(txtViewFechaFrom.Text, DateMask, CultureInfo.InvariantCulture);
            Boolean IsCanceled = true;
            var Dialog = new DatePickerDialog(this, async (s, arg) =>
            {
                IsCanceled = false;
                txtViewFechaFrom.Text = arg.Date.ToString(DateMask);
                await Search();
            }, fecha.Year, fecha.Month - 1, fecha.Day);

            Dialog.DismissEvent += async (s, arg) =>
            {
                if (IsCanceled && String.IsNullOrEmpty(txtViewFechaTo.Text))
                {
                    txtViewFechaFrom.Text = DateTime.Now.ToString(DateMask);
                    await Search();
                }
            };

            RunOnUiThread(() =>
            {
                Dialog.Show();
            });
        }

        private async Task<ElaborateReportRequest> GetRequest()
        {
            var request = new ElaborateReportRequest();

            try
            {
                request.FECHAFROM = DateTime.ParseExact(txtViewFechaFrom.Text, DateMask, CultureInfo.InvariantCulture).GetSapDate();

                //if (Count == 0 && !String.IsNullOrEmpty(repo.GetRepositoryZ().GetProces().EquipmentID))
                //{
                //    request.IDEQUIPO = repo.GetRepositoryZ().GetProces().EquipmentID;
                //}
                //else
                //{
                var equipo = Equipos.First(p => p.Name == spnEquipment.SelectedItem.ToString());
                request.IDEQUIPO = equipo.ID;
                //}

                //Count++;

                if (!String.IsNullOrEmpty(txtViewFechaTo.Text))
                {
                    request.FECHATO = DateTime.ParseExact(txtViewFechaTo.Text, DateMask, CultureInfo.InvariantCulture).GetSapDate();
                }

                if (!String.IsNullOrEmpty(txtViewHoraFrom.Text))
                {
                    request.HORAFROM = DateTime.ParseExact(txtViewHoraFrom.Text, TimeMask, CultureInfo.InvariantCulture).GetSapHora();
                }

                if (!String.IsNullOrEmpty(txtViewHoraTo.Text))
                {
                    request.HORATO = DateTime.ParseExact(txtViewHoraTo.Text, TimeMask, CultureInfo.InvariantCulture).GetSapHora();
                }

                if (chkTurn1.Checked) request.IDTURNS.Add(1);
                if (chkTurn2.Checked) request.IDTURNS.Add(2);
                if (chkTurn3.Checked) request.IDTURNS.Add(3);

                //if (spnProducto.SelectedItemPosition > 0)
                //{
                //    var producto = materiales.ElementAt(spnProducto.SelectedItemPosition - 1);
                //    request.MATNR = producto.Code;
                //}

                //if (spnBatchID.SelectedItemPosition > 0)
                //{
                //    request.BATCHID = spnBatchID.SelectedItem.ToString();
                //}
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }

            return request;
        }

        private async Task Search(List<ElaboratesReport> fResults = null)
        {
            try
            {
                ShowProgress(true);

                if (!IsThereConnection)
                {
                    new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.NoConection));
                    OnBackPressed();
                    return;
                }

                var repoz = repo.GetRepositoryZ();
                var repor = repo.GetRepositoryR();
                var request = await GetRequest();
                var result = fResults ?? await repor.GetElaborateReport(request);

                if (fResults == null) Results = result;

                txtViewTotal.Text = String.Empty;
                txtViewFull.Text = String.Empty;
                txtViewInEquipment.Text = String.Empty;
                txtViewRelease.Text = String.Empty;
                listView2.Adapter = null;

                var TimeID = Convert.ToByte(Equipos.ElementAt(spnEquipment.SelectedItemPosition).TimeID);

                if (result != null && result.Any())
                {
                    var first = result.First();

                    if (String.IsNullOrEmpty(first.TrayID))
                    {
                        txtViewBandejas.Text = GetString(Resource.String.ReportTitleEmpaque);
                        txtViewPeso.Text = GetString(Resource.String.ReportTitleSecuence);
                        txtViewPeso.Visibility = ViewStates.Visible;
                        txtViewFecha1.Visibility = ViewStates.Gone;
                        txtViewHora1.Visibility = ViewStates.Gone;
                        txtViewBandejaConsumo.Visibility = ViewStates.Visible;
                    }
                    else if (TimeID > 1)
                    {
                        txtViewBandejas.Text = GetString(Resource.String.ReportTitleBandejas);
                        txtViewPeso.Visibility = ViewStates.Gone;
                        txtViewFecha1.Visibility = ViewStates.Visible;
                        txtViewHora1.Visibility = ViewStates.Visible;
                        txtViewBandejaConsumo.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        txtViewBandejas.Text = GetString(Resource.String.ReportTitleBandejas);
                        txtViewPeso.Text = GetString(Resource.String.ReportTitlePeso);
                        txtViewPeso.Visibility = ViewStates.Visible;
                        txtViewFecha1.Visibility = ViewStates.Visible;
                        txtViewHora1.Visibility = ViewStates.Visible;
                        txtViewBandejaConsumo.Visibility = ViewStates.Gone;
                    }

                    if (fResults == null)
                    {
                        var Text = GetString(Resource.String.Select);

                        //Equipos = result.Select(p => p.EquipmentID).Distinct().OrderBy(o => o).ToList();

                        //spnEquipment.Adapter = new SelectedSpinnerAdapter(this, Equipos.ToList(), true);
                        //spnEquipment.ItemSelected += async (s, args) =>
                        //{
                        //    Search(Results.Where(p => p.EquipmentID == spnEquipment.SelectedItem.ToString()).ToList());

                        materiales = await repoz.GetMaterials(result.Select(p => p.ProductCode.ToSapCode()).ToList());

                        spnProducto.Adapter = new NothingSelectedSpinnerAdapter(this, materiales.Select(p => p._Short).ToList(), Text, "[No One]", true);
                        spnProducto.ItemSelected += async (s1, args1) =>
                        {
                            if (args1.Position > 0)
                            {
                                var material = materiales.ElementAt(args1.Position - 1);
                                await Search(Results.Where(p => p.ProductCode == material.Code).ToList());
                                BatchIDs = Results.Where(p => p.ProductCode == material.Code).Select(p => p.BatchID).OrderByDescending(o => o).Distinct().ToList();
                                spnBatchID.Adapter = new NothingSelectedSpinnerAdapter(this, BatchIDs.ToList(), Text, "[No One]", true);
                                spnBatchID.ItemSelected += async (s2, args2) =>
                                {
                                    if (args2.Position > 0)
                                    {
                                        var batchid = BatchIDs.ElementAt(args2.Position - 1);
                                        await Search(Results.Where(p => p.ProductCode == material.Code && p.BatchID == batchid).ToList());
                                    }
                                    else
                                    {
                                        await Search(Results.Where(p => p.ProductCode == material.Code).ToList());
                                    }
                                };
                            }
                            else
                            {
                                spnBatchID.Adapter = null;
                                await Search(Results);
                            }
                        };
                        //};
                    }

                    listView2.Adapter = new ReportAdapterProduccionSap(this, result, TimeID);

                    txtViewTotal.Text = result.Count().ToString();
                    txtViewFull.Text = result.Count(c => c.Status == ProductsRoutes.RoutesStatus.EnTransito).ToString();
                    txtViewInEquipment.Text = result.Count(c => c.Status == ProductsRoutes.RoutesStatus.EnEquipo).ToString();
                    txtViewRelease.Text = result.Count(c => c.Status == ProductsRoutes.RoutesStatus.Procesado || c.Status == ProductsRoutes.RoutesStatus.Cancelada).ToString();
                }
                else
                {
                    Toast.MakeText(this, Resource.String.ReportNoInfo, ToastLength.Short).Show();
                }
            }
            catch (WebException wEx)
            {
                ShowWebExceptionDialog(wEx, "Consulta de reportes de salidas - SAP");
                OnBackPressed();
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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_back, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menu_back)
                OnBackPressed();

            return base.OnMenuItemSelected(featureId, item);
        }

        private async void gridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                var pos = _reportEntries.ElementAt(e.Position);

                switch (pos.Type)
                {
                    case ReportEntry.EntryTypes.ReporteTickets:

                        Dialog dialog = null;

                        var builder = new AlertDialog.Builder(this);
                        builder.SetTitle(Resource.String.ReportTitleTicket);
                        builder.SetIcon(Resource.Drawable.ic_ticket);
                        var viewBandejas = LayoutInflater.Inflate(Resource.Layout.dialog_scan_bandeja, null);

                        var editBandejaTicket = viewBandejas.FindViewById<EditText>(Resource.Id.editBandeja);
                        Boolean Executed = false;

                        editBandejaTicket.KeyPress += async (s, ek) =>
                        {
                            ek.Handled = false;
                            if (ek.KeyCode == Keycode.Enter && !Executed && ek.Event.Action == KeyEventActions.Down)
                            {
                                Executed = true;
                                SetScreen(Screen.ListView);

                                if (!IsThereConnection)
                                {
                                    new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.NoConection));
                                    dialog.Dismiss();
                                    dialog.Dispose();
                                    OnBackPressed();
                                    return;
                                }

                                var tickets = await repo.GetRepositoryR().GetTicketReport(editBandejaTicket.Text.ToUpper());

                                if (tickets == null)
                                {
                                    new CustomDialog(this, CustomDialog.Status.Warning, GetString(Resource.String.ReportTitleNoProductTraza));
                                    dialog.Dismiss();
                                    dialog.Dispose();
                                    OnBackPressed();
                                    return;
                                }

                                var adapterTicket = new TicketsAdapter(this, tickets);
                                listView.Adapter = adapterTicket;
                                dialog.Dismiss();
                                dialog.Dispose();

                                ek.Handled = true;
                            }
                        };

                        builder.SetView(viewBandejas);

                        builder.SetNegativeButton(Resource.String.Cancel, (s, a) =>
                        {
                            dialog.Dismiss();
                            dialog.Dispose();
                        });

                        dialog = builder.Create();

                        RunOnUiThread(() =>
                        {
                            dialog.Show();
                        });

                        break;

                    case ReportEntry.EntryTypes.ReporteProduction:

                        await CleanReport();

                        await PreloadEquipments();

                        SetScreen(Screen.ListViewWithHead);

                        break;
                }

            }
            catch (WebException wEx)
            {
                ShowWebExceptionDialog(wEx, "Consulta de Reportes de Trazabilidad por Bandejas");
                OnBackPressed();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private void SetScreen(Screen Screen)
        {
            this._Screen = Screen;
            viewFlipper.DisplayedChild = (Byte)Screen;
        }

        private async void setHeaderList()
        {
            try
            {
                txtViewProduccion = FindViewById<TextView>(Resource.Id.txtViewProduccion);
                txtViewTurn = FindViewById<TextView>(Resource.Id.txtViewTurn);
                txtViewMaterial = FindViewById<TextView>(Resource.Id.txtViewMaterial);
                txtViewBatchID = FindViewById<TextView>(Resource.Id.txtViewBatchID);
                txtViewBandejas = FindViewById<TextView>(Resource.Id.txtViewBandejas);
                txtViewPeso = FindViewById<TextView>(Resource.Id.txtViewPeso);
                txtViewHora = FindViewById<TextView>(Resource.Id.txtViewHora);
                txtViewEquipo = FindViewById<TextView>(Resource.Id.txtViewEquipo);
                txtViewFecha1 = FindViewById<TextView>(Resource.Id.txtViewFecha1);
                txtViewHora1 = FindViewById<TextView>(Resource.Id.txtViewHora1);

                txtViewProduccion.Text = GetString(Resource.String.ReportTitleProduccion).Replace("-", "").Trim();
                txtViewProduccion.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewProduccion.SetTextColor(Android.Graphics.Color.Black);
                txtViewProduccion.SetBackgroundResource(Resource.Drawable.selector_input_text);

                txtViewBandejaConsumo.Text = GetString(Resource.String.ReportTitleUltimaCantidad);
                txtViewBandejaConsumo.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewBandejaConsumo.SetTextColor(Android.Graphics.Color.Black);
                txtViewBandejaConsumo.SetBackgroundResource(Resource.Drawable.selector_input_text);

                txtViewTurn.Text = GetString(Resource.String.ReportTitleTurn);
                txtViewTurn.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewTurn.SetTextColor(Android.Graphics.Color.Black);
                txtViewTurn.SetBackgroundResource(Resource.Drawable.selector_input_text);

                txtViewMaterial.Text = GetString(Resource.String.ReportTitleProducto);
                txtViewMaterial.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewMaterial.SetTextColor(Android.Graphics.Color.Black);
                txtViewMaterial.SetBackgroundResource(Resource.Drawable.selector_input_text);

                txtViewBatchID.Text = GetString(Resource.String.ReportTitleBatchID);
                txtViewBatchID.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewBatchID.SetTextColor(Android.Graphics.Color.Black);
                txtViewBatchID.SetBackgroundResource(Resource.Drawable.selector_input_text);

                txtViewBandejas.Text = GetString(Resource.String.ReportTitleBandejas);
                txtViewBandejas.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewBandejas.SetTextColor(Android.Graphics.Color.Black);
                txtViewBandejas.SetBackgroundResource(Resource.Drawable.selector_input_text);

                txtViewPeso.Text = GetString(Resource.String.ReportTitlePeso);
                txtViewPeso.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewPeso.SetTextColor(Android.Graphics.Color.Black);
                txtViewPeso.SetBackgroundResource(Resource.Drawable.selector_input_text);

                txtViewHora.Text = GetString(Resource.String.ReportTitleHora);
                txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewHora.SetTextColor(Android.Graphics.Color.Black);
                txtViewHora.SetBackgroundResource(Resource.Drawable.selector_input_text);

                txtViewEquipo.Text = GetString(Resource.String.ReportTitleLocated);
                txtViewEquipo.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewEquipo.SetTextColor(Android.Graphics.Color.Black);
                txtViewEquipo.SetBackgroundResource(Resource.Drawable.selector_input_text);

                txtViewFecha1.Text = GetString(Resource.String.ReportTitleFecha);
                txtViewFecha1.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewFecha1.SetTextColor(Android.Graphics.Color.Black);
                txtViewFecha1.SetBackgroundResource(Resource.Drawable.selector_input_text);

                txtViewHora1.Text = GetString(Resource.String.ReportTitleHora);
                txtViewHora1.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                txtViewHora1.SetTextColor(Android.Graphics.Color.Black);
                txtViewHora1.SetBackgroundResource(Resource.Drawable.selector_input_text);
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async Task CleanReport()
        {
            try
            {
                txtViewFechaTo.Text = String.Empty;
                txtViewFechaFrom.Text = DateTime.Now.ToString(DateMask);
                txtViewHoraFrom.Text = String.Empty;
                txtViewHoraTo.Text = String.Empty;
                txtViewTotal.Text = String.Empty;
                txtViewFull.Text = String.Empty;
                txtViewInEquipment.Text = String.Empty;
                txtViewRelease.Text = String.Empty;
                chkTurn1.Checked = true;
                chkTurn2.Checked = true;
                chkTurn3.Checked = true;
                spnBatchID.Adapter = null;
                spnProducto.Adapter = null;
                listView2.Adapter = null;
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async Task PreloadEquipments()
        {
            try
            {
                if (Equipos == null)
                {
                    var repoequip = repo.GetRepositoryEquipments();
                    var rmp = await repoequip.GetAsyncAll();
                    Equipos = rmp.Where(p => !p.IsSubEquipment).ToList();
                }

                RunOnUiThread(() =>
                {
                    spnEquipment.Adapter = new SelectedSpinnerAdapter(this, Equipos.Select(p => p.Name).ToList(), true);
                });

                var proceso = await repo.GetRepositoryZ().GetProces();

                if (!String.IsNullOrEmpty(proceso.EquipmentID))
                {
                    if (!proceso.IsSubEquipment)
                    {
                        RunOnUiThread(() =>
                        {
                            var index = Equipos.Select((p, n) => new { p.Name, n }).Where(p => p.Name == proceso.Equipment).First().n;
                            spnEquipment.SetSelection(index);
                        });
                    }
                    else
                    {
                        var actualConfig = await repo.GetRepositoryZ().GetActualConfig(proceso.EquipmentID);
                        RunOnUiThread(() =>
                       {
                           var index = Equipos.Select((p, n) => new { p.Name, n }).Where(p => p.Name == actualConfig.Equipment).First().n;
                           spnEquipment.SetSelection(index);
                       });
                    }
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }
    }
}