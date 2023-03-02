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
using ControlConsumo.Shared.Models.Z;
using ControlConsumo.Shared.Tables;
using ControlConsumo.Droid.Managers;
using ControlConsumo.Shared.Repositories;

namespace ControlConsumo.Droid.Activities.Adapters
{
    public class TraysReleaseAdapter : BaseAdapter
    {
        Context context;
        LayoutInflater inflater;
        public readonly List<TraysList> Bandejas = new List<TraysList>();
        private readonly RepositoryZ repoz = new RepositoryZ(Util.GetConnection());
        //private IEnumerable<Materials> Materiales { get; set; }

        public TraysReleaseAdapter(Context context)
        {
            this.context = context;
            inflater = LayoutInflater.From(context);
            // this.Materiales = Materiales;
        }

        public override int Count
        {
            get { return Bandejas.Count(); }
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
            var view = convertView;
            var holder = new Holder();

            if (view == null)
            {
                view = inflater.Inflate(Resource.Layout.adapter_trays_release, null);
                holder.txtSecuencia = view.FindViewById<TextView>(Resource.Id.txtSecuencia);
                holder.txtBandeja = view.FindViewById<TextView>(Resource.Id.txtBandeja);
                holder.txtMaterialBandeja = view.FindViewById<TextView>(Resource.Id.txtMaterialBandeja);
                view.Tag = holder;
            }
            else
            {
                holder = view.Tag as Holder;
            }

            var bande = Bandejas.ElementAt(position);

            holder.txtSecuencia.Text = (position + 1).ToString();
            holder.txtBandeja.Text = bande.BarCode;

            try
            {
                if (!String.IsNullOrEmpty(bande.ProductCode))
                {
                    var mat = repoz.GetMaterialByCode(bande.ProductCode);

                    if (mat != null)
                    {
                        holder.txtMaterialBandeja.Text = mat._ProductName + " (" + bande.EquipmentID + ")";
                    }
                }
            }
            catch (Exception)
            { }

            return view;
        }

        private class Holder : Java.Lang.Object
        {
            public TextView txtSecuencia;
            public TextView txtBandeja;
            public TextView txtMaterialBandeja;
        }

        public void Clear()
        {
            Bandejas.Clear();
            NotifyDataSetChanged();
        }

        public void Add(TraysList bandeja)
        {
            if (!Bandejas.Any(p => p.BarCode == bandeja.BarCode))
            {
                Bandejas.Add(bandeja);
                NotifyDataSetInvalidated();
            }
        }

        public List<String> GetTrays
        {
            get
            {
                return Bandejas.Any() ? Bandejas.Select(p => p.BarCode).ToList() : null;
            }
        }

        public Int32 GetTotal()
        {
            return Bandejas.Count;
        }
    }
}