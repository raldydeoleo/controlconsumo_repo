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
using ControlConsumo.Shared.Models.R;
using ControlConsumo.Droid.Managers;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class VarillaAdapterReprint : BaseAdapter
    {
        private readonly Context context;
        private readonly LayoutInflater Inflater;
        private readonly IEnumerable<MaterialReport> List;
        private readonly RepositoryFactory repo = new RepositoryFactory(Util.GetConnection());

        public VarillaAdapterReprint(Context context, IEnumerable<MaterialReport> List)
        {
            this.context = context;
            this.List = List;
            this.Inflater = LayoutInflater.From(context);
        }

        public override int Count
        {
            get { return List.Count(); }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var holder = new Holder();

            if (convertView == null)
            {
                convertView = Inflater.Inflate(Resource.Layout.adapter_report_varilla_detail, null);
                holder.imgButtonPrint = convertView.FindViewById<ImageButton>(Resource.Id.imgButtonPrint);
                holder.txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora);
                holder.txtViewLote = convertView.FindViewById<TextView>(Resource.Id.txtViewLote);
                holder.txtViewCantidad = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                holder.imgButtonPrint.Click += imgButtonPrint_Click;
                convertView.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as Holder;
            }

            var pos = List.ElementAt(position);

            holder.imgButtonPrint.Tag = holder;
            holder.Position = position;
            holder.txtViewLote.Text = pos.Lot;
            holder.txtViewHora.Text = pos.Produccion.ToLocalTime().ToString("hh:mm tt");
            holder.txtViewCantidad.Text = pos.EntryQuantity.ToString("N3");

            return convertView;
        }

        private async void imgButtonPrint_Click(object sender, EventArgs e)
        {
            var repoz = repo.GetRepositoryZ();
            var button = sender as ImageButton;
            var holder = button.Tag as Holder;

            var position = List.ElementAt(holder.Position);

            var zmaterial = new RepositoryFactory(Util.GetConnection()).GetRepositoryMaterialZilm();

            var etiqueta = new Etiquetas()
            {
                Cantidad = 1,
                Codigo = position.MaterialReference ?? String.Empty,
                Descripcion = position.MaterialName,
                Secuencia = position.BoxNumber,
                Medida = (decimal)position.EntryQuantity,
                Unidad = position.MaterialUnit,
                LoteInterno = position.Lot,
                Material = position._MaterialCode,
                LoteSuplidor = position.LoteSuplidor,
                Fecha = position.Expire
            };

            if (OnPrint != null)
            {
                OnPrint.Invoke(etiqueta);
            }
        }

        public delegate void Print(Etiquetas etiqueta);
        public event Print OnPrint;

        private class Holder : Java.Lang.Object
        {
            public Int32 Position { get; set; }
            public ImageButton imgButtonPrint;
            public TextView txtViewHora;
            public TextView txtViewLote;
            public TextView txtViewCantidad;
        }
    }
}