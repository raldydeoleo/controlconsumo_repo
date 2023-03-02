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
using ControlConsumo.Shared.Models.Config;
using ControlConsumo.Shared.Models.Equipment;
using ControlConsumo.Shared.Tables;
using System.Threading;
using System.Globalization;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Droid.Managers;

namespace ControlConsumo.Droid.Activities.Adapters
{
    public class ConfigAdapterExpandable : BaseExpandableListAdapter, IFilterable
    {
        Context context { get; set; }
        private RepositoryFactory repo = new RepositoryFactory(Util.GetConnection());
        public List<EquipmentHolder> equipmentsAll { get; protected set; }
        protected List<EquipmentHolder> equipments { get; set; }
        public IEnumerable<Areas> areas { get; private set; }
        public IEnumerable<Equipments> subequipments { get; private set; }
        private NothingSelectedSpinnerAdapter areaAdapter { get; set; }
        private NothingSelectedSpinnerAdapter SubEquipmentAdapter { get; set; }
        private LayoutInflater Inflater { get; set; }
        private static List<Int32> Configs;
        private readonly String NoSubEquipment;
        private readonly Dictionary<String, int?> SpinnerCache;
        private readonly Dictionary<String, int?> SwitchCache;
        private readonly List<Int32> GrouptoShow = new List<Int32>();
        private DateTime? FechaChildFilter { get; set; }
        public String SelEquipmentID { get; set; }
        public Int32 SelConfigID { get; set; }

        public ConfigAdapterExpandable(Context context, IEnumerable<EquipmentList> equipments, IEnumerable<Equipments> subequipments, IEnumerable<Areas> areas, String psubEquipment, String parea, String npsubEquipment, String nparea)
        {
            try
            {
                if (Configs == null)
                {
                    Configs = new List<Int32>();
                }

                SpinnerCache = new Dictionary<String, int?>();
                SwitchCache = new Dictionary<String, int?>();
                NoSubEquipment = npsubEquipment;
                this.context = context;
                this.subequipments = subequipments;
                this.equipmentsAll = equipments
                    .GroupBy(p => new { p.Equipment, p.EquipmentID, p.Type, p.TimeID, p.Status, p.Area, p.AreaID, p.EquipmentTypeID })
                    .Select(p => new EquipmentHolder
                    {
                        EquipmentID = p.Key.EquipmentID,
                        Equipment = p.Key.Equipment,
                        AreaID = p.Key.AreaID,
                        Type = p.Key.Type,
                        IsActive = p.Key.Status,
                        Area = p.Key.Area,
                        EquipmentTypeID = p.Key.EquipmentTypeID,
                        TimeID = p.Key.TimeID,
                        Detalle = p.Where(d => d.Begin.HasValue).Select(d => new EquipmentHolder.ConfigHolder
                        {
                            TimeID = d.TimeID,
                            Create = d.Create,
                            IsActive = d.IsActive,
                            IsDeleted = Configs.Any(c => c == d.ConfigID),
                            ConfigID = d.ConfigID,
                            Begin = d.Begin,
                            Code = d.Code,
                            Material = d.Material,
                            Version = d.Version,
                            Short = d.Short,
                            SubEquipment = d.SubEquipment,
                            SubEquipmentID = d.SubEquipmentID,
                            Logon = d.Logon
                        }).OrderBy(d => d.Begin).ToList()
                    }).ToList();
                this.equipments = equipmentsAll;
                this.areas = areas;
                Inflater = LayoutInflater.From(context);
                Filter = new EquipmentFilter(this);
                areaAdapter = new NothingSelectedSpinnerAdapter(this.context, areas.Select(p => p.Name).ToList(), parea, nparea);
                SubEquipmentAdapter = new NothingSelectedSpinnerAdapter(this.context, subequipments.Select(p => p.Name).ToList(), psubEquipment, npsubEquipment);
            }
            catch (Exception ex)
            {
                Util.CatchException(context, ex);
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return 0;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            View view = convertView;

            try
            {
                ChildHolder holder;

                if (view == null)
                {
                    view = Inflater.Inflate(Resource.Layout.adapter_config_config, parent, false);
                    holder = new ChildHolder()
                    {
                        txtMaterial = view.FindViewById<TextView>(Resource.Id.txtMaterial),
                        txtVersion = view.FindViewById<TextView>(Resource.Id.txtVersion),
                        txtDate = view.FindViewById<TextView>(Resource.Id.txtDate),
                        txtSubEquipment = view.FindViewById<TextView>(Resource.Id.txtSubEquipment),
                        txtStatus = view.FindViewById<TextView>(Resource.Id.txtStatus)
                    };

                    view.Tag = holder;
                }
                else
                {
                    holder = (ChildHolder)view.Tag;
                }

                var Equipment = equipments[groupPosition];

                var config = new EquipmentHolder.ConfigHolder();

                if (!FechaChildFilter.HasValue)
                    config = Equipment.Detalle.Where(p => !p.IsDeleted).ToList()[childPosition];
                else
                    config = Equipment.Detalle.Where(p => !p.IsDeleted && p._Begin == FechaChildFilter.Value.GetSapDate()).ToList()[childPosition];

                holder.txtMaterial.Text = config._Material;
                holder.txtVersion.Text = config.Version;

                if (config.ConfigID > 0)
                    holder.txtDate.Text = config.Begin.Value.ToLocalTime().ToString("MMM dd, yyyy     hh:mm tt");
                else
                    holder.txtDate.Text = config.Begin.Value.ToString("MMM dd, yyyy     hh:mm tt");

                if (!String.IsNullOrEmpty(config.SubEquipment))
                    holder.txtSubEquipment.Text = config.SubEquipment;
                else
                    holder.txtSubEquipment.Text = String.Empty;

                if (config.IsActive)
                {
                    holder.txtStatus.Text = "En Progreso";
                    holder.txtStatus.SetTextColor(Android.Graphics.Color.Green);
                }
                else
                {
                    if (childPosition == 1)
                    {
                        holder.txtStatus.SetTextColor(Android.Graphics.Color.Blue);
                    }
                    holder.txtStatus.Text = "Programado";
                }

                if (Equipment.EquipmentID == SelEquipmentID && config.ConfigID == SelConfigID)
                {
                    view.Background = context.Resources.GetDrawable(Resource.Color.blue_dark);
                }
                else
                {
                    view.Background = context.Resources.GetDrawable(Resource.Color.blue_base_translucent);
                }
            }
            catch (Exception ex)
            {
                Util.CatchException(context, ex);
            }

            return view;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            if (!FechaChildFilter.HasValue)
                return equipments[groupPosition].Detalle.Where(p => !p.IsDeleted).Count();
            else
                return equipments[groupPosition].Detalle.Where(p => !p.IsDeleted && p._Begin == FechaChildFilter.Value.GetSapDate()).Count();
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return equipments[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var view = convertView;

            try
            {
                Holder holder;

                if (view == null)
                {
                    view = Inflater.Inflate(Resource.Layout.adapter_config_equipment, parent, false);
                    holder = new Holder()
                    {
                        txtCodigo = view.FindViewById<TextView>(Resource.Id.txtCodigo),
                        txtName = view.FindViewById<TextView>(Resource.Id.txtName),
                        spnArea = view.FindViewById<Spinner>(Resource.Id.spnArea),
                        txtShort = view.FindViewById<TextView>(Resource.Id.txtShort),
                        txtVersion = view.FindViewById<TextView>(Resource.Id.txtVersion),
                        txtDate = view.FindViewById<TextView>(Resource.Id.txtDate),
                        SwtStatus = view.FindViewById<Switch>(Resource.Id.SwtStatus)
                    };

                    holder.spnArea.ItemSelected -= null;
                    holder.spnArea.ItemSelected += spnArea_ItemSelected;

                    holder.SwtStatus.CheckedChange += null;
                    holder.SwtStatus.CheckedChange += SwtStatus_CheckedChange;

                    view.Tag = holder;
                }
                else
                {
                    holder = (Holder)view.Tag;
                }

                var equipo = equipments[groupPosition];
                holder.SwtStatus.Tag = equipo;
                holder.spnArea.Tag = equipo;

                holder.txtCodigo.Text = equipo.EquipmentID;
                holder.txtName.Text = equipo.Equipment;
                SwitchCache.Remove(equipo.EquipmentID);
                holder.SwtStatus.Checked = equipo.IsActive;

                ControlConsumo.Droid.Activities.Adapters.ConfigAdapterExpandable.EquipmentHolder.ConfigHolder config = null;

                var actives = equipo.Detalle.Where(p => p.IsActive).ToList();

                if (actives.Count == 1)
                {
                    config = actives.Single();
                }
                else if (actives.Count > 1) ///Control para desactivar las duplicadas y activas
                {
                    config = equipo.Detalle.OrderByDescending(p => p.Begin).First();
                    var duplicates = equipo.Detalle.Where(p => p.ConfigID != config.ConfigID).ToList();
                    foreach (var item in duplicates)
                    {
                        item.IsActive = false;
                    }
                    UpdateDuplicate(duplicates);
                }

                if (config != null)
                {
                    holder.txtVersion.Text = config.Version;
                    holder.txtShort.Text = config.Short ?? config._Code;
                    holder.txtDate.Text = config.Begin.Value.ToLocalTime().ToString("MMM dd, yyyy     hh:mm tt");
                }
                else
                {
                    holder.txtVersion.Text = String.Empty;
                    holder.txtShort.Text = String.Empty;
                    holder.txtDate.Text = String.Empty;
                }

                SpinnerCache.Remove(equipo.EquipmentID);

                if (equipo.AreaID > 0)
                {
                    areaAdapter.SelectedText = equipo.Area;
                }
                else
                {
                    areaAdapter.SelectedText = String.Empty;
                }

                holder.spnArea.Adapter = areaAdapter;

                if (!equipo.IsActive) ///Control para que se active el evento no quitar
                {
                    SwtStatus_CheckedChange(holder.SwtStatus, new CompoundButton.CheckedChangeEventArgs(false));
                }

                /*

                var list = parent as ExpandableListView;

                if (list.IsGroupExpanded(groupPosition))
                {
                    GrouptoShow.RemoveAll(p => p == groupPosition);
                    GrouptoShow.Add(groupPosition);
                }
                else if (!list.IsGroupExpanded(groupPosition) && GrouptoShow.Any(p => p == groupPosition))
                {
                    list.ExpandGroup(groupPosition);
                }
                 */
            }
            catch (Exception ex)
            {
                Util.CatchException(context, ex);
            }

            return view;
        }

        private void SwtStatus_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var obj = sender as Switch;
            var holder = obj.Tag as EquipmentHolder;

            if (holder != null)
            {
                var cache = SwitchCache.FirstOrDefault(p => p.Key == holder.EquipmentID);

                if (cache.Key == null)
                {
                    SwitchCache.Add(holder.EquipmentID, -1);
                    return;
                }

                var equipo = equipmentsAll.Single(p => p.EquipmentID == holder.EquipmentID);
                equipo.IsActive = e.IsChecked;
                equipo.UpdateStatus = true;
            }
        }

        private async void spnArea_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var obj = sender as Spinner;
            var holder = obj.Tag as EquipmentHolder;
            try
            {
                if (holder != null)
                {
                    var equipo = equipmentsAll.Single(p => p.EquipmentID == holder.EquipmentID);

                    var cache = SpinnerCache.FirstOrDefault(p => p.Key == equipo.EquipmentID);

                    if (cache.Key == null)
                    {
                        SpinnerCache.Add(equipo.EquipmentID, -1);
                        return;
                    }

                    if (e.Position > 0)
                    {
                        var area = areas.ElementAt(e.Position - 1);
                        equipo.AreaID = area.ID;
                        equipo.Area = area.Name;
                        equipo.UpdateAreas = true;
                    }
                    else
                    {
                        if (equipo.AreaID > 0)
                        {
                            equipo.Area = String.Empty;
                            equipo.AreaID = 0;
                            equipo.UpdateAreas = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                await Util.SaveException(ex);
            }
        }

        public override int GroupCount
        {
            get { return equipments.Count(); }
        }

        public override bool HasStableIds
        {
            get { return true; }
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return !equipments[groupPosition].Detalle[childPosition].CanEdit;
        }

        protected override void Dispose(bool disposing)
        {
            equipmentsAll.Clear();
            equipments.Clear();
            areas = null;
            subequipments = null;
            Configs.Clear();
            base.Dispose(disposing);
        }

        public class EquipmentHolder : Java.Lang.Object
        {
            public String EquipmentID { get; set; }
            public String Equipment { get; set; }
            public String Type { get; set; }
            public String TimeID { get; set; }
            public Int32 AreaID { get; set; }
            public Int32 EquipmentTypeID { get; set; }
            public String Area { get; set; }
            public Boolean IsActive { get; set; }
            public Boolean UpdateStatus { get; set; }
            public Boolean UpdateAreas { get; set; }
            public List<ConfigHolder> Detalle { get; set; }
            public String _EquipmentTypeID
            {
                get
                {
                    return String.Concat("*_*_*", EquipmentTypeID.ToString());
                }
            }

            public class ConfigHolder
            {
                public String Logon { get; set; }
                public Boolean IsActive { get; set; }
                public Int32 ConfigID { get; set; }
                public String SubEquipmentID { get; set; }
                public String SubEquipment { get; set; }
                public String Material { get; set; }
                public String Code { get; set; }
                public String Short { get; set; }
                public String Version { get; set; }
                public DateTime? Begin { get; set; }
                public DateTime? Create { get; set; }
                public String TimeID { get; set; }
                public String _Begin
                {
                    get
                    {
                        if (Begin.HasValue)
                        {
                            return Begin.Value.GetSapDate();
                        }
                        else
                        {
                            return String.Empty;
                        }
                    }
                }
                public Boolean IsDeleted { get; set; }
                public Boolean IsUpdated { get; set; }
                public String _Code
                {
                    get
                    {
                        try
                        {
                            return Convert.ToInt32(Code).ToString();
                        }
                        catch (Exception)
                        {
                            return Code;
                        }
                    }
                }
                public String _Material
                {
                    get
                    {
                        if (String.IsNullOrEmpty(Short))
                            return String.Format("{0}{1}", _Code.PadRight(12, ' '), Material);
                        else
                            return String.Format("{0}{1}{2}", Short.Trim().PadRight(20, ' '), _Code.PadRight(12, ' '), Material);
                    }
                }
                public Boolean CanEdit
                {
                    get
                    {
                        return IsActive;
                    }
                }
            }
        }

        public class Holder : Java.Lang.Object
        {
            public TextView txtCodigo;
            public TextView txtName;
            public Spinner spnArea;
            public Switch SwtStatus;

            public TextView txtShort;
            public TextView txtVersion;
            public TextView txtDate;
        }

        public class ChildHolder : Java.Lang.Object
        {
            public TextView txtStatus;
            public TextView txtMaterial;
            public TextView txtVersion;
            public TextView txtDate;
            public TextView txtSubEquipment;
        }

        public Filter Filter { get; private set; }

        private class EquipmentFilter : Filter
        {
            private ConfigAdapterExpandable Adapter { get; set; }

            public EquipmentFilter(ConfigAdapterExpandable Adapter)
            {
                this.Adapter = Adapter;
            }

            protected override Filter.FilterResults PerformFiltering(Java.Lang.ICharSequence constraint)
            {
                var returnobj = new FilterResults();
                var result = new List<EquipmentHolder>();

                if (constraint == null) return returnobj;

                var split = constraint.ToString().ToLower().Split('|');

                var query = split[0].Trim().ToLower();
                var date = split[1].Trim();
                var subeq = split[2].Trim();

                Adapter.SetDate(date);

                if (Adapter.equipmentsAll != null && Adapter.equipmentsAll.Any())
                {
                    result.AddRange(Adapter.equipmentsAll.Where(p =>
                           (String.IsNullOrEmpty(query) || (!String.IsNullOrEmpty(query) && p.Equipment.ToLower().Contains(query) || p.EquipmentID.ToLower().Contains(query)))
                        && (String.IsNullOrEmpty(date) || (!String.IsNullOrEmpty(date) && p.Detalle.Any(d => d._Begin == date)))
                        && (String.IsNullOrEmpty(subeq) || (!String.IsNullOrEmpty(subeq) && p._EquipmentTypeID == subeq))));
                }

                returnobj.Values = FromArray(result.ToArray());
                returnobj.Count = result.Count;

                constraint.Dispose();

                return returnobj;
            }

            protected override void PublishResults(Java.Lang.ICharSequence constraint, Filter.FilterResults results)
            {
                try
                {
                    using (var values = results.Values)
                        Adapter.equipments = values.ToArray<EquipmentHolder>().ToList();

                    Adapter.NotifyDataSetChanged();

                    constraint.Dispose();
                    results.Dispose();

                }
                catch (Exception)
                {
                    
                }
            }
        }

        public void AddNewConfig(EquipmentList newConfig, Boolean planchar)
        {
            var equipo = equipments.Single(p => p.EquipmentID == newConfig.EquipmentID);
            var detalle = equipo.Detalle.SingleOrDefault(p => p.ConfigID == newConfig.ConfigID);

            if (detalle == null)
            {
                if (equipo.Detalle.Any(p => p.Begin == newConfig.Begin))
                {
                    new CustomDialog(context, CustomDialog.Status.Error, context.GetString(Resource.String.dialog_new_config_no_new));
                    return;
                }

                var config = new EquipmentHolder.ConfigHolder()
                       {
                           ConfigID = newConfig.ConfigID,
                           TimeID = newConfig.TimeID,
                           Begin = newConfig.Begin,
                           Code = newConfig.Code,
                           Material = newConfig.Material,
                           Short = newConfig.Short,
                           SubEquipment = newConfig.SubEquipment,
                           SubEquipmentID = newConfig.SubEquipmentID,
                           Version = newConfig.Version
                       };

                equipo.Detalle.Add(config);
            }
            else
            {
                detalle.Begin = newConfig.Begin;
                detalle.Code = newConfig.Code;
                detalle.Material = newConfig.Material;
                detalle.Short = newConfig.Short;
                detalle.SubEquipment = newConfig.SubEquipment;
                detalle.SubEquipmentID = newConfig.SubEquipmentID;
                detalle.Version = newConfig.Version;
                detalle.IsUpdated = newConfig.ConfigID > 0;
                detalle.TimeID = newConfig.TimeID;
            }

            if (planchar)
            {
                var detalle2 = equipo.Detalle.Where(p => p.Begin < DateTime.Now && !p.IsActive);
                foreach (var item in detalle2)
                {
                    item.IsDeleted = true;
                }
            }

            equipo.Detalle.Sort((x, y) => DateTime.Compare(x.Begin.Value, y.Begin.Value));

            NotifyDataSetChanged();
        }

        public void DeleteSelected()
        {
            var config = equipmentsAll
                .Single(p => p.EquipmentID == SelEquipmentID)
                .Detalle.Single(p => p.ConfigID == SelConfigID);

            if (config.ConfigID > 0)
            {
                Configs.Add(config.ConfigID);
                config.IsDeleted = true;
            }
            else
            {
                equipmentsAll.Single(p => p.EquipmentID == SelEquipmentID).Detalle.RemoveAll(p => p.ConfigID == SelConfigID);
            }

            ClearSelected();
            NotifyDataSetChanged();
        }

        public void ClearSelected()
        {
            SelEquipmentID = null;
            NotifyDataSetChanged();
        }

        public void SetSelected(Int32 GroupPosition, Int32 ChildPosition)
        {
            var equipo = equipments[GroupPosition];
            var detail = equipo.Detalle[ChildPosition];

            SelEquipmentID = equipo.EquipmentID;
            SelConfigID = detail.ConfigID;
            NotifyDataSetChanged();
        }

        public void SetDate(String fecha)
        {
            if (!String.IsNullOrEmpty(fecha))
            {
                FechaChildFilter = DateTime.ParseExact(fecha, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            else
            {
                FechaChildFilter = null;
            }
        }

        public EquipmentHolder GetEquipmentSelect()
        {
            return equipments.Single(p => p.EquipmentID == SelEquipmentID);
        }

        public async void UpdateDuplicate(List<ControlConsumo.Droid.Activities.Adapters.ConfigAdapterExpandable.EquipmentHolder.ConfigHolder> lst)
        {
            foreach (var item in lst)
            {


                await repo.GetRepositoryZ().UpdateConfigStatusAsync(item.ConfigID, Shared.Tables.Configs._Status.Completed);
            }
        }
    }
}