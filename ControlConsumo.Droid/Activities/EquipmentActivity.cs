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
using ControlConsumo.Shared.Tables;
using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Models.Equipment;
using System.Globalization;
using System.Threading.Tasks;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Droid.Managers;
using Android.Graphics;
using Android.Content.PM;
using System.Threading;

namespace ControlConsumo.Droid.Activities
{
    [Activity(ScreenOrientation = ScreenOrientation.Landscape, LaunchMode = LaunchMode.SingleTop)]
    public class EquipmentActivity : BaseActivity
    {
        public List<String> SubEquipments { get; set; }
        protected Boolean ActionExecuted;
        private ActionMode actionMode { get; set; }
        protected ConfigAdapterExpandable Adapter { get; set; }
        private List<ConfigList> Configuraciones { get; set; }

        private IEnumerable<Times> Tiempos { get; set; }

        private IEnumerable<ConfigList> Lista { get; set; }

        #region Controls

        private ExpandableListView _ListView { get; set; }
        private TextView txtEquipmentID { get; set; }
        private TextView txtMaterial { get; set; }
        private TextView txtShort { get; set; }
        private Spinner spnVersion { get; set; }
        private EditText txtDate { get; set; }
        private EditText txtTime { get; set; }
        private Spinner spnSubEquipment { get; set; }
        private Spinner spnTiempo { get; set; }
        private Spinner spnMaterial { get; set; }
        private Button btnDate { get; set; }
        private Button btnTime { get; set; }
        private TextView txtDateMenu { get; set; }
        private SearchView search { get; set; }
        private Spinner spnEquipmentTypes { get; set; }

        #endregion

        private List<Equipments> subequipos { get; set; }
        private DateTime Fecha { get; set; }
        private DateTime Hora { get; set; }
        private List<EquipmentTypes> EquipmentsTypes { get; set; }
        private Boolean Cancel { get; set; }

        protected override async void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);

                SetContentView(Resource.Layout.equipment_activity);
                SetTitle(Resource.String.ApplicationLabel);
                SetFinishOnTouchOutside(false);

                if (_ListView == null)
                {
                    _ListView = FindViewById<ExpandableListView>(Resource.Id.ListConfig);
                    _ListView.ItemLongClick += _ListView_ItemLongClick;
                    _ListView.ChildClick += _ListView_ChildClick;
                }

                LoadConfigs();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void _ListView_ChildClick(object sender, ExpandableListView.ChildClickEventArgs e)
        {
            try
            {
                if (actionMode != null)
                {
                    actionMode.Finish();
                    actionMode.Dispose();
                    actionMode = null;
                }

                Adapter.SetSelected(e.GroupPosition, e.ChildPosition);

                var config = Adapter.GetEquipmentSelect();

                var IsDisableEdit = config.Detalle.Where(p => !p.IsDeleted).ElementAt(e.ChildPosition).Begin < DateTime.Now;

                actionMode = StartActionMode(new ActionModeCallback(this, IsDisableEdit));
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void _ListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            try
            {
                AlertDialog dialog = null;

                var inflater = LayoutInflater.From(this);

                var equipo = (ConfigAdapterExpandable.EquipmentHolder)e.Parent.GetItemAtPosition(e.Position);

                if (equipo != null)
                {
                    var alertDialogBuilder = new AlertDialog.Builder(this);
                    alertDialogBuilder.SetTitle(Resource.String.ApplicationLabel);
                    alertDialogBuilder.SetCancelable(false);

                    Boolean planchar = false;

                    var builder = new StringBuilder();

                    var detalle = equipo.Detalle.Where(p => p.Begin.Value < DateTime.Now && !p.IsActive);

                    if (detalle.Any())
                    {
                        builder.AppendLine(String.Format("{0} {1}", GetString(Resource.String.dialog_new_config_planchar), equipo.Equipment));
                        planchar = true;
                    }
                    else
                    {
                        builder.AppendLine(String.Format(GetString(Resource.String.dialog_new_config), equipo.Equipment));
                    }

                    var view = inflater.Inflate(Resource.Layout.question_dialog, null);

                    view.FindViewById<TextView>(Resource.Id.txtQuestion).Text = builder.ToString();

                    alertDialogBuilder.SetView(view);
                    alertDialogBuilder.SetNegativeButton(Resource.String.Cancel, (sende, args) =>
                    {
                        dialog.Dispose();
                    });

                    alertDialogBuilder.SetPositiveButton(Resource.String.Accept, (sende, args) =>
                    {
                        dialog.Dismiss();
                        BuildShow(equipo, false, planchar);
                    });

                    dialog = alertDialogBuilder.Create();

                    RunOnUiThread(() =>
                    {
                        dialog.Show();
                    });
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected async void LoadConfigs()
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                var repoa = repo.GetRepositoryAreas();
                var equipments = await repoz.GetEquipments();
                subequipos = await repoz.GetSubEquipments();
                var areas = await repoa.GetAsyncAll();

                var pequipo = GetString(Resource.String.Select_prompt_subEquipment);
                var npequipo = GetString(Resource.String.UnSelect_prompt_subEquipment);
                var parea = GetString(Resource.String.Select_prompt_area);
                var nparea = GetString(Resource.String.UnSelect_prompt_area);

                var process = await repoz.GetProces();

                Adapter = new ConfigAdapterExpandable(this, equipments, subequipos, areas.Where(p => p.status == Areas.Status.Activa).ToList(), pequipo, parea, npequipo, nparea);

                _ListView.SetAdapter(Adapter);

                var equipo = process.EquipmentID;

                if (process.IsSubEquipment)
                {
                    equipo = process.SubEquipmentID;
                }

                if (!String.IsNullOrEmpty(equipo))
                {
                    search.SetQuery(String.Empty, true);
                    search.SetQuery(equipo, true);
                    _ListView.ExpandGroup(0);
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            try
            {
                MenuInflater.Inflate(Resource.Menu.ConfigMenu, menu);
                ConfigMenu(menu);
            }
            catch (Exception ex)
            {
                base.CatchException(ex);
            }

            return base.OnCreateOptionsMenu(menu);
        }

        private async void ConfigMenu(IMenu menu)
        {
            search = (SearchView)menu.FindItem(Resource.Id.menu_action_search).ActionView;
            search.SetQueryHint(GetString(Resource.String.action_equipment));
            search.QueryTextChange += search_QueryTextChange;

            var repoz = repo.GetRepositoryZ();
            EquipmentsTypes = await repoz.GetEquipmentTypesAsync();

            var menu_Filter = menu.FindItem(Resource.Id.menu_Filter_types);
            spnEquipmentTypes = (Spinner)menu_Filter.ActionView;
            spnEquipmentTypes.Adapter = new NothingSelectedSpinnerAdapter(this, EquipmentsTypes.Select(p => p.Name).ToList(), GetString(Resource.String.Select), GetString(Resource.String.Select));
            spnEquipmentTypes.ItemSelected += spnEquipmentTypes_ItemSelected;

            var menu_Filter_date = menu.FindItem(Resource.Id.menu_Filter_date);
            var ln = (LinearLayout)menu_Filter_date.ActionView;
            var btn = ln.FindViewById<ImageButton>(Resource.Id.btnDateMenu);
            btn.Click += btn_Click;
            txtDateMenu = ln.FindViewById<TextView>(Resource.Id.txtDateMenu);

            int id = search.Context.Resources.GetIdentifier("android:id/search_src_text", null, null);
            var text = (TextView)search.FindViewById(id);
            text.SetHintTextColor(Android.Graphics.Color.White);
        }

        private async void ApplyFilter()
        {
            try
            {
                if (Adapter != null)
                {
                    var builder = new StringBuilder();
                    builder.Append(String.Concat(search.Query ?? String.Empty, "|"));

                    if (txtDateMenu != null && txtDateMenu.Text != "Sin Fecha")
                    {
                        var fecha = DateTime.ParseExact(txtDateMenu.Text, "dd/MM/yy", CultureInfo.InvariantCulture);
                        builder.Append(String.Concat(fecha.ToString("yyyyMMdd"), "|"));
                    }
                    else
                    {
                        builder.Append(String.Concat(String.Empty, "|"));
                    }

                    if (spnEquipmentTypes.SelectedItemPosition > 0)
                    {
                        var tipo = EquipmentsTypes.ElementAt(spnEquipmentTypes.SelectedItemPosition - 1);
                        builder.Append(String.Concat("*_*_*", tipo.ID));
                    }
                    else
                    {
                        builder.Append(String.Empty);
                    }

                    Adapter.Filter.InvokeFilter(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void btn_Click(object sender, EventArgs e)
        {
            try
            {
                var fecha = DateTime.Now.AddMonths(-1);

                var Dialog = new DatePickerDialog(this, OnDateSet_btn, fecha.Year, fecha.Month, fecha.Day);
                Dialog.SetCancelable(false);
                Cancel = false;
                Dialog.DismissEvent += Dialog_DismissEvent;

                RunOnUiThread(() =>
                {
                    Dialog.Show();
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void Dialog_DismissEvent(object sender, EventArgs e)
        {
            try
            {
                if (!Cancel)
                {
                    txtDateMenu.Text = "No Date";
                    ApplyFilter();
                }
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void OnDateSet_btn(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            try
            {
                Cancel = true;
                txtDateMenu.Text = e.Date.ToString("dd/MM/yy");
                ApplyFilter();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void spnEquipmentTypes_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                ApplyFilter();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            try
            {
                switch (item.ItemId)
                {
                    case Resource.Id.menu_Add:
                        PostChange();

                        break;

                    case Resource.Id.menu_Cancel:
                        Adapter.Dispose();
                        Finish();
                        break;
                }
            }
            catch (Exception ex)
            {
                CatchException(ex);
            }
            return true;
        }

        private async void PostChange()
        {
            try
            {
                ShowProgress(true);

                var repoz = repo.GetRepositoryZ();
                var repoconfig = repo.GetRepositoryConfigs();
                var repomaterial = repo.GetRepositoryMaterials();
                var process = SecurityManager.CurrentProcess;
                var subequipment = await repoz.GetSettingAsync<String>(Settings.Params.Second_Equipment, null);

                var bufferconfig = Adapter.equipmentsAll.SelectMany(e => e.Detalle.Where(d => d.ConfigID < 0 && !d.IsDeleted).Select(d => new Configs
                {
                    ID = d.ConfigID,
                    EquipmentID = e.EquipmentID,
                    Status = Configs._Status.Actived,
                    ProductCode = d.Code,
                    SubEquipmentID = process.IsSubEquipment && e.EquipmentID.Equals(process.SubEquipmentID) ? process.EquipmentID : d.SubEquipmentID,
                    VerID = d.Version,
                    Begin = d.Begin.Value,
                    TimeID = d.TimeID,
                    Sync = process.IsSubEquipment && e.EquipmentID.Equals(process.SubEquipmentID) ? false : true,
                    Logon = process.Logon,
                    CreateDate = d.Create ?? DateTime.Now,
                    IsCold = false,

                })).ToList();

                if (bufferconfig.Any())
                {
                    await repoconfig.InsertAsyncAll(bufferconfig);
                }

                var bufferconfigup = Adapter.equipmentsAll.SelectMany(e => e.Detalle.Where(d => d.IsUpdated).Select(d => new Configs
                {
                    ID = d.ConfigID,
                    EquipmentID = e.EquipmentID,
                    Status = Configs._Status.Actived,
                    ProductCode = d.Code,
                    SubEquipmentID = process.IsSubEquipment && e.EquipmentID.Equals(process.SubEquipmentID) ? process.EquipmentID : d.SubEquipmentID,
                    VerID = d.Version,
                    Begin = d.Begin.Value,
                    TimeID = d.TimeID,
                    Sync = process.IsSubEquipment && e.EquipmentID.Equals(process.SubEquipmentID) ? false : true,
                    Logon = d.Logon ?? process.Logon,
                    Logon2 = process.Logon,
                    CreateDate = d.Create ?? DateTime.Now,
                    ModifyDate = DateTime.Now,
                    IsCold = false
                })).ToList();

                if (bufferconfigup.Any())
                {
                    await repoconfig.InsertOrReplaceAsyncAll(bufferconfigup);
                }

                var Equip_Updates = Adapter.equipmentsAll.Where(p => p.UpdateAreas || p.UpdateStatus).ToList();

                if (Equip_Updates.Any())
                {
                    await repoz.UpdateEquipment(Equip_Updates.Select(p => new EquipmentUpdate()
                    {
                        AreaID = (Byte)p.AreaID,
                        EquipmentID = p.EquipmentID,
                        IsActive = p.IsActive,
                        UpdateArea = p.UpdateAreas,
                        UpdateStatus = p.UpdateStatus
                    }).ToList());
                }

                var Ids = Adapter.equipmentsAll.SelectMany(m => m.Detalle.Where(p => p.IsDeleted && p.ConfigID > 0).Select(s => s.ConfigID)).ToList();

                if (Ids.Any()) /// Configuraciones Eliminadas
                {
                    await repoz.UpdateConfig(Ids);
                }

                Toast.MakeText(this, Resource.String.update_config, ToastLength.Long).Show();
                Adapter.Dispose();
                _ListView.SetAdapter(null);
                LoadConfigs();
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

        protected async void DeleteConfig(ConfigAdapterExpandable.EquipmentHolder equipmentconfig)
        {
            try
            {
                Adapter.DeleteSelected();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void search_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            try
            {
                ApplyFilter();
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        protected async void BuildShow(ConfigAdapterExpandable.EquipmentHolder equipo, Boolean edit = false, Boolean Planchar = false)
        {
            try
            {
                var repoz = repo.GetRepositoryZ();
                var repot = repo.GetRepositoryTimes();

                var inflater = LayoutInflater.From(this);

                Tiempos = await repot.GetAsyncAll();
                var process = await repoz.GetProces();

                var allConfigs = await repoz.GetConfigs();

                //Configuraciones.RemoveAll(p => String.IsNullOrEmpty(p.Short)); //// Linea Temporal para limpiar
                //Configuraciones.RemoveAll(p => p.Short.Substring(0, 1) != "R"); /// Linea Temporal para limpiar

                AlertDialog dialog = null;

                var alertDialogBuilder = new AlertDialog.Builder(this);
                alertDialogBuilder.SetTitle(Resource.String.ApplicationLabel);
                alertDialogBuilder.SetCancelable(false);

                #region Init layout

                var view = inflater.Inflate(Resource.Layout.equipment_new_config, null);

                txtEquipmentID = view.FindViewById<TextView>(Resource.Id.txtEquipmentID);
                view.FindViewById<TextView>(Resource.Id.txtEquipment).Text = equipo.Equipment;
                spnTiempo = view.FindViewById<Spinner>(Resource.Id.spnTiempo);
                spnMaterial = view.FindViewById<Spinner>(Resource.Id.spnMaterial);
                txtMaterial = view.FindViewById<TextView>(Resource.Id.txtMaterial);
                txtShort = view.FindViewById<TextView>(Resource.Id.txtShort);
                spnVersion = view.FindViewById<Spinner>(Resource.Id.spnVersion);
                spnSubEquipment = view.FindViewById<Spinner>(Resource.Id.spnSubEquipment);
                txtDate = view.FindViewById<EditText>(Resource.Id.editDate);
                txtTime = view.FindViewById<EditText>(Resource.Id.editTime);
                btnDate = view.FindViewById<Button>(Resource.Id.btnDate);
                btnTime = view.FindViewById<Button>(Resource.Id.btnTime);

                #endregion

                List<ConfigList> ccMaterial = null;

                spnTiempo.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, Tiempos.Select(p => p.Time).ToList());

                spnTiempo.ItemSelected += (o, a) =>
                {
                    ccMaterial = allConfigs.Where(f => f.TimeID.ToNumeric() == Tiempos.ElementAt(a.Position).ID.ToNumeric()).ToList();
                    spnMaterial.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, ccMaterial.Select(p => !String.IsNullOrEmpty(p.Short) ? String.Format("{0} - {1}", p.Short, p._Code) : p.Material).ToList());
                };

                spnMaterial.ItemSelected += (o, a) =>
                {
                    var newconfig = ccMaterial.ElementAt(spnMaterial.SelectedItemPosition);
                    txtMaterial.Text = newconfig._Code;
                    txtShort.Text = newconfig.Material;

                    var versionestmp = ccMaterial.Where(p => p.Code == newconfig.Code).Select(p => p.Version).ToList();
                    spnVersion.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, versionestmp);
                };

                var pequipo = GetString(Resource.String.Select_prompt_subEquipment);
                var npequipo = GetString(Resource.String.UnSelect_prompt_subEquipment);
                var subeq = subequipos.Select(p => String.Format("{0} - {1}", p.ID, p.Name)).ToList();
                var adap = new NothingSelectedSpinnerAdapter(this, subeq, pequipo, npequipo, true);
                var actualConfig = await repoz.GetActualConfig(equipo.EquipmentID);

                txtEquipmentID.Text = equipo.EquipmentID;

                var detalle = equipo.Detalle.SingleOrDefault(p => p.ConfigID == Adapter.SelConfigID);

                if (!edit)
                {
                    var pos = Tiempos.Select((p, n) => new { n, p }).FirstOrDefault(f => f.p.ID.ToNumeric() == equipo.TimeID.ToNumeric());

                    if (pos != null)
                    {
                        spnTiempo.SetSelection(pos.n, true);
                    }

                    txtDate.Text = DateTime.Now.ToString("MMM dd, yyyy");
                    txtTime.Text = DateTime.Now.AddHours(1).ToString("hh:mm");
                    Fecha = DateTime.Now;
                    Hora = DateTime.Now.AddHours(1);
                }
                else
                {
                    var pos = Tiempos.Select((p, n) => new { n, p }).FirstOrDefault(f => f.p.ID.ToNumeric() == detalle.TimeID.ToNumeric());

                    if (pos != null)
                    {
                        spnTiempo.SetSelection(pos.n, true);
                    }

                    if (detalle.ConfigID < 0)
                    {
                        txtDate.Text = detalle.Begin.Value.ToString("MMM dd, yyyy");
                        txtTime.Text = detalle.Begin.Value.ToString("hh:mm");
                        Fecha = detalle.Begin.Value;
                        Hora = detalle.Begin.Value;
                    }
                    else
                    {
                        txtDate.Text = detalle.Begin.Value.ToLocalTime().ToString("MMM dd, yyyy");
                        txtTime.Text = detalle.Begin.Value.ToLocalTime().ToString("hh:mm");
                        Fecha = detalle.Begin.Value.ToLocalTime();
                        Hora = detalle.Begin.Value.ToLocalTime();
                    }
                }

                if (!String.IsNullOrEmpty(process.SubEquipmentID) && (process.IsSubEquipment ? process.SubEquipmentID : process.EquipmentID) == equipo.EquipmentID)
                {
                    spnSubEquipment.Adapter = adap;
                    var sub = subequipos.Select((p, i) => new { p, i }).First(p => p.p.ID == (process.IsSubEquipment ? process.EquipmentID : process.SubEquipmentID)).i;
                    spnSubEquipment.SetSelection(sub + 1);
                }

                spnSubEquipment.Enabled = false;

                //spnMaterial.ItemSelected += (spnsender, args) =>
                //{
                //    if (!Initialized)
                //    {
                //        Initialized = true;

                //        if (String.IsNullOrEmpty(txtMaterial.Text))
                //        {
                //            var newconfig = Configuraciones.ElementAt(spnMaterial.SelectedItemPosition);
                //            txtMaterial.Text = newconfig._Code;
                //            txtShort.Text = newconfig.Material;

                //            var versionestmp = Configuraciones.Where(p => p.Code == newconfig.Code).Select(p => p.Version).ToList();
                //            spnVersion.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, versionestmp);
                //        }

                //        return;
                //    }

                //    var config = Configuraciones.ElementAt(spnMaterial.SelectedItemPosition);
                //    txtMaterial.Text = config._Code;
                //    txtShort.Text = config.Material;

                //    var versiones = ccMaterial.Where(p => p.Code == config.Code).Select(p => p.Version).ToList();
                //    spnVersion.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, versiones);
                //};

                btnDate.Click += btnDate_Click;
                btnTime.Click += btnTime_Click;

                alertDialogBuilder.SetView(view);
                alertDialogBuilder.SetNegativeButton(Resource.String.Cancel, (sende, args) =>
                {
                    Adapter.ClearSelected();
                    dialog.Dispose();
                });

                alertDialogBuilder.SetPositiveButton(Resource.String.Accept, async (sende, args) =>
                {
                    try
                    {
                        var l_fecha = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, Hora.Hour, Hora.Minute, 0);

                        if (l_fecha < DateTime.Now)
                        {
                            new CustomDialog(this, CustomDialog.Status.Error, GetString(Resource.String.dialog_new_config_no_old));
                            return;
                        }

                        var MatConfig = ccMaterial.ElementAt(spnMaterial.SelectedItemPosition); // SingleOrDefault(p => p.Short == spnMaterial.SelectedItem.ToString() || p.Material == spnMaterial.SelectedItem.ToString());

                        int ConfigID = 0;

                        if (detalle != null)
                        {
                            ConfigID = detalle.ConfigID;
                        }
                        else
                        {
                            ConfigID = (Adapter.equipmentsAll.Count(p => p.Detalle.Any(a => a.ConfigID < 0)) * -1);
                            ConfigID--;
                        }

                        // var timeid = Adapter.equipmentsAll.FirstOrDefault(p => p.TimeID.ToNumeric() == Tiempos.ElementAt(spnTiempo.SelectedItemPosition).ID.ToNumeric()).TimeID;

                        var newequipo = new EquipmentList()
                        {
                            ConfigID = ConfigID,
                            EquipmentID = txtEquipmentID.Text,
                            Code = MatConfig.Code,
                            Begin = l_fecha,
                            TimeID = Tiempos.ElementAt(spnTiempo.SelectedItemPosition).ID,
                            Short = MatConfig.Short,
                            Material = MatConfig.Material,
                            Version = spnVersion.SelectedItem.ToString(),
                            Create = detalle != null ? detalle.Create : DateTime.Now,
                            SubEquipmentID = !process.IsSubEquipment && (process.EquipmentID ?? String.Empty).Equals(txtEquipmentID.Text) ? process.SubEquipmentID : null,
                            IsEdit = edit
                        };

                        //if (spnSubEquipment.SelectedItemPosition > 0)
                        //{
                        //    var sub_equipo = subequipos.Single(p => String.Format("{0} - {1}", p.ID, p.Name) == spnSubEquipment.SelectedItem.ToString());
                        //    newequipo.SubEquipment = sub_equipo.Name;
                        //    newequipo.SubEquipmentID = sub_equipo.ID;
                        //}

                        Adapter.AddNewConfig(newequipo, Planchar);
                        Adapter.ClearSelected();

                        dialog.Dismiss();
                        dialog.Dispose();
                    }
                    catch (Exception ex)
                    {
                        await CatchException(ex);
                    }
                });

                dialog = alertDialogBuilder.Create();

                RunOnUiThread(() =>
                {
                    dialog.Show();
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void btnTime_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = new TimePickerDialog(this, OnTimeSet, DateTime.Now.Hour, DateTime.Now.Minute, false);

                RunOnUiThread(() =>
                {
                    dialog.Show();
                });
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void btnDate_Click(object sender, EventArgs e)
        {
            try
            {
                var Dialog = new DatePickerDialog(this, OnDateSet, DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
                var fecha = DateTime.Now;
                //Dialog.DatePicker.MinDate = ExtensionsMethodsHelper.CurrentTimeMillis();
                RunOnUiThread(() =>
                {
                    Dialog.Show();
                });
            }
            catch (Exception ex)
            {
                await base.CatchException(ex);
            }
        }

        private async void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            try
            {
                txtDate.Text = e.Date.ToString("MMM dd, yyyy");
                Fecha = e.Date;
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        private async void OnTimeSet(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            try
            {
                Hora = new DateTime(1970, 1, 1, e.HourOfDay, e.Minute, 0);
                txtTime.Text = Hora.ToString("hh:mm");
            }
            catch (Exception ex)
            {
                await CatchException(ex);
            }
        }

        public class ActionModeCallback : Java.Lang.Object, ActionMode.ICallback
        {
            private readonly EquipmentActivity cxt;
            private readonly Boolean OnlyDisable;

            public ActionModeCallback(EquipmentActivity activity, Boolean OnlyDisable)
            {
                this.cxt = activity;
                this.OnlyDisable = OnlyDisable;
            }

            public bool OnActionItemClicked(ActionMode mode, IMenuItem item)
            {
                try
                {
                    var equipmentconfig = cxt.Adapter.GetEquipmentSelect();

                    switch (item.ItemId)
                    {
                        case Resource.Id.Menu_A_edit:

                            cxt.BuildShow(equipmentconfig, true);

                            break;

                        case Resource.Id.Menu_A_delete:

                            cxt.DeleteConfig(equipmentconfig);
                            cxt.LoadConfigs();

                            break;
                    }

                    mode.Finish();
                    cxt.ActionExecuted = true;
                }
                catch (Exception ex)
                {
                    cxt.CatchException(ex);
                }

                return true;
            }

            public bool OnCreateActionMode(ActionMode mode, IMenu menu)
            {
                try
                {
                    if (!OnlyDisable)
                        mode.MenuInflater.Inflate(Resource.Menu.ConfigMenuChild, menu);
                    else
                        mode.MenuInflater.Inflate(Resource.Menu.ConfigMenuChildOnlyDisable, menu);
                }
                catch (Exception ex)
                {
                    cxt.CatchException(ex);
                }

                return true;
            }

            public async void OnDestroyActionMode(ActionMode mode)
            {
                try
                {
                    cxt.Adapter.ClearSelected();
                    cxt.ActionExecuted = true;
                }
                catch (Exception ex)
                {
                    await cxt.CatchException(ex);
                }
            }

            public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
            {
                return true;
            }
        }
    }
}