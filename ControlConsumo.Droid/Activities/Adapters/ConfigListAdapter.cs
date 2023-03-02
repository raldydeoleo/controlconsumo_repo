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
using ControlConsumo.Shared.Tables;
using ControlConsumo.Droid.Activities.Widgets;
using ControlConsumo.Droid.Managers;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class ConfigListAdapter : BaseAdapter
    {
        private readonly IEnumerable<NextConfig> NextConfigs;
        private readonly NextConfig nextConfig;
        private readonly LayoutInflater inflater;
        private readonly Context context;
        public delegate void Refresh();
        public event Refresh OnRefresh;
        protected RepositoryFactory repo;

        public ConfigListAdapter(Context context, RepositoryFactory repo, IEnumerable<NextConfig> NextConfigs, NextConfig nextConfig)
        {
            inflater = LayoutInflater.From(context);
            this.context = context;
            this.repo = repo;
            this.NextConfigs = NextConfigs;
            this.nextConfig = nextConfig;
        }

        public override int Count
        {
            get { return NextConfigs.Count(); }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            var holder = new Holder();

            if (view == null)
            {
                view = inflater.Inflate(Resource.Layout.adapter_next_configs, null);
                holder.txtMaterial = view.FindViewById<TextView>(Resource.Id.txtMaterial);
                holder.txtTime = view.FindViewById<TextView>(Resource.Id.txtTime);
                holder.txtStatus = view.FindViewById<TextView>(Resource.Id.txtStatus);
                holder.imgButtonChangeAlmacenamiento = view.FindViewById<ImageButton>(Resource.Id.imgButtonChangeAlmacenamiento);
                holder.imgButtonChangeAlmacenamiento.Click += ImgButtonChangeAlmacenamiento_Click;
                view.Tag = holder;
            }
            else
            {
                holder = view.Tag as Holder;
            }

            var config = NextConfigs.ElementAt(position);

            holder.nextConfig = config;
            holder.txtMaterial.Text = config._ProductName;
            holder.txtTime.Text = String.Format("{0} {1}", config.Begin.ToLocalTime().ToString("dddd").ToCapitalize(), config.Begin.ToLocalTime().ToString("dd, yyyy hh:mm tt"));

            var status = String.Empty;
            holder.imgButtonChangeAlmacenamiento.Visibility = ViewStates.Gone;

            holder.txtStatus.SetBackgroundResource(Resource.Drawable.selector_input_text);

            switch (config.Status)
            {
                case Configs._Status.Enabled:
                    holder.txtStatus.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                    status = "En Progreso";

                    if (NextConfigs.Any(a => a.Producto == Times.ProductTypes.Validar_Salida_y_Tipo_Almacenamiento || a.Producto == Times.ProductTypes.Validar_Tipo_Almacenamiento))
                    {
                        if (config.Producto == Times.ProductTypes.Validar_Salida_y_Tipo_Almacenamiento || config.Producto == Times.ProductTypes.Validar_Tipo_Almacenamiento)
                        {
                            holder.imgButtonChangeAlmacenamiento.Visibility = ViewStates.Visible;
                        }
                        else
                        {
                            holder.imgButtonChangeAlmacenamiento.Visibility = ViewStates.Invisible;
                        }
                    }

                    break;

                case Configs._Status.Actived:

                    if (config.ConfigID == nextConfig.ConfigID)
                        holder.txtStatus.SetBackgroundColor(Android.Graphics.Color.LightBlue);

                    status = "Programado";
                    break;
            }

            holder.txtStatus.Text = status;
            holder.imgButtonChangeAlmacenamiento.Tag = holder;

            return view;
        }

        private async void ImgButtonChangeAlmacenamiento_Click(object sender, EventArgs e)
        {
            var obj = sender as ImageButton;
            var holder = obj.Tag as Holder;

            if (holder != null)
            {
                var repoTime = new RepositoryFactory(Util.GetConnection()).GetRepositoryTimes();
                var repoConfig = new RepositoryFactory(Util.GetConnection()).GetRepositoryConfigs();
                var config = await repoConfig.GetAsyncByKey(holder.nextConfig.ConfigID);
                var Time = await repoTime.GetAsyncByKey(holder.nextConfig.TimeID);
                holder.nextConfig.Identifier = config.Identifier;
                holder.nextConfig.ProductType = config.ProductType;
                var tipoAlmacenamiento = new TipoAlmacenamientoDialog(this.context,repo,holder.nextConfig);
                tipoAlmacenamiento.OnStoreType += async (IsCold, ProductType, Identifier) =>
                {
                    config.Sync = true;
                    config.IsCold = IsCold;
                    config.ProductType = ProductType;
                    config.Identifier = Identifier;
                    await repoConfig.UpdateAsync(config);
                    OnRefresh.Invoke();
                };

                tipoAlmacenamiento.ShowDialogAsync(Time);
            }
        }

        private class Holder : Java.Lang.Object
        {
            public NextConfig nextConfig { get; set; }
            public TextView txtMaterial { get; set; }
            public TextView txtTime { get; set; }
            public TextView txtStatus { get; set; }
            public ImageButton imgButtonChangeAlmacenamiento { get; set; }
        }
    }
}