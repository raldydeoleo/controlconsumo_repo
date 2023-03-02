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
    class ReportAdapterProduccionDetails : BaseAdapter
    {
        public readonly Context context;
        public readonly IEnumerable<ProductionReport> list;
        public readonly LayoutInflater Inflater;

        public ReportAdapterProduccionDetails(Context context, IEnumerable<ProductionReport> list)
        {
            this.context = context;
            this.Inflater = LayoutInflater.From(context);
            this.list = list;
        }

        public override int Count
        {
            get { return list.Count(); }
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
                convertView = Inflater.Inflate(Resource.Layout.adapter_report_produccion_detail, null);
                holder.imgButtonPrint = convertView.FindViewById<ImageButton>(Resource.Id.imgButtonPrint);
                holder.txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora);
                holder.txtViewCantidad = convertView.FindViewById<TextView>(Resource.Id.txtViewCantidad);
                holder.imgButtonPrint.Click += imgButtonPrint_Click;
                convertView.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as Holder;
            }

            var pos = list.ElementAt(position);

            holder.imgButtonPrint.Tag = holder;
            holder.Position = position;
            holder.txtViewHora.Text = pos.Fecha.ToLocalTime().ToString("hh:mm tt");
            holder.txtViewCantidad.Text = pos.Quantity.ToString("N3");

            return convertView;
        }

        private async void imgButtonPrint_Click(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var holder = button.Tag as Holder;

            var repoElaborates = new RepositoryFactory(Util.GetConnection()).GetRepositoryElaborates();

            var repoz = new RepositoryFactory(Util.GetConnection()).GetRepositoryZ();

            var position = list.ElementAt(holder.Position);

            var elaborate = await repoElaborates.GetAsyncByKey(position.ElaborateID);
                        
            if (String.IsNullOrEmpty(elaborate.Reference))
            {
                var lote = repoz.GetLoteForMaterial(position.ProductCode, elaborate.Lot);
                if (lote != null && lote.Reference!=null)
                {
                    elaborate.Reference = lote.Reference; //Lote de suplidor
                }
            }
            
            var etiqueta = new Etiquetas()
            {
                Codigo = position.ProductReference,
                Fecha = elaborate.ExpireDate.Value,
                LoteInterno = elaborate.Lot,
                LoteSuplidor = elaborate.Reference,
                Cantidad = 1,
                Descripcion = position.ProductName,
                Material = position._ProductCode,
                Unidad = elaborate.Unit,
                Medida = (Decimal)elaborate.Quantity
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
            public TextView txtViewCantidad;
        }
    }
}