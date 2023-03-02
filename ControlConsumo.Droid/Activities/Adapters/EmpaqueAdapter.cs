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
using Java.Lang;
using ControlConsumo.Shared.Models.R;
using ControlConsumo.Shared.Tables;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class EmpaqueAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly List<EmpaqueImpresionResult> Lista;
        private readonly LayoutInflater Inflater;

        public delegate void Print(Elaborates salida, ProductsRoutes traza);
        public event Print OnPrint;

        public EmpaqueAdapter(Context context, List<EmpaqueImpresionResult> Lista)
        {
            this.context = context;
            this.Lista = Lista;
            this.Inflater = LayoutInflater.From(context);
        }

        public override int Count
        {
            get
            {
                return Lista.Count() + 1;
            }
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
                convertView = Inflater.Inflate(Resource.Layout.adapter_report_empaque_detail, null);
                holder.imgButtonPrint = convertView.FindViewById<ImageButton>(Resource.Id.imgButtonPrint);
                holder.txtViewSecuencia = convertView.FindViewById<TextView>(Resource.Id.txtViewSecuencia);
                holder.txtViewAlmacenamientoFiller = convertView.FindViewById<TextView>(Resource.Id.txtViewAlmacenamiento_Filler);
                holder.txtViewEmpaque = convertView.FindViewById<TextView>(Resource.Id.txtViewEmpaque);
                holder.txtViewHora = convertView.FindViewById<TextView>(Resource.Id.txtViewHora);
                holder.imgButtonPrint.Click += ImgButtonPrint_Click;
                convertView.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as Holder;
            }

            if (position == 0)
            {
                holder.position = -1;
                holder.txtViewSecuencia.Text = context.GetString(Resource.String.ReportTitleCounter);
                holder.txtViewSecuencia.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);

                holder.txtViewEmpaque.Text = context.GetString(Resource.String.ReportTitleEmpaque);
                holder.txtViewEmpaque.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);

                holder.txtViewAlmacenamientoFiller.Text = context.GetString(Resource.String.ReportTitleAlmacenamiento_Filler);
                holder.txtViewAlmacenamientoFiller.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                
                holder.txtViewHora.Text = context.GetString(Resource.String.ReportTitleHora);
                holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);

                holder.imgButtonPrint.Visibility = ViewStates.Invisible;
            }
            else
            {
                var pos = Lista.ElementAt(position -1);

                holder.position = position -1;
                holder.txtViewSecuencia.Text = pos.Salida.PackSequence == 0 ? pos.Traza.SecuenciaEmpaque.ToString("0000") : pos.Salida.PackSequence.ToString("0000");
                holder.txtViewSecuencia.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.txtViewAlmacenamientoFiller.Text = pos.Salida.Identifier;
                holder.txtViewAlmacenamientoFiller.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.txtViewEmpaque.Text = pos.Salida.PackID;
                holder.txtViewEmpaque.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.txtViewHora.Text = pos.Salida.Produccion.ToLocalTime().ToString("hh:mm");
                holder.txtViewHora.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);

                holder.imgButtonPrint.Visibility = ViewStates.Visible;
                holder.imgButtonPrint.Tag = holder;
            }

            return convertView;
        }

        private void ImgButtonPrint_Click(object sender, EventArgs e)
        {
            var obj = sender as ImageButton;
            var holder = obj.Tag as Holder;

            if (holder != null)
            {
                var position = Lista.ElementAt(holder.position);
                OnPrint.Invoke(position.Salida, position.Traza);
            }
        }

        private class Holder : Java.Lang.Object
        {
            public int position { get; set; }
            public ImageButton imgButtonPrint { get; set; }
            public TextView txtViewSecuencia { get; set; }
            public TextView txtViewAlmacenamientoFiller { get; set; }
            public TextView txtViewEmpaque { get; set; }
            public TextView txtViewHora { get; set; }
        }
    }
}