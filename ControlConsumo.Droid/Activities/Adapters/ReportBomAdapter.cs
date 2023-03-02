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
using Java.Lang;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class ReportBomAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly LayoutInflater Inflater;
        private readonly IEnumerable<MaterialReport> BomReports;

        public ReportBomAdapter(Context context, IEnumerable<MaterialReport> BomReports)
        {
            this.context = context;
            this.Inflater = LayoutInflater.From(context);
            this.BomReports = BomReports;
        }

        public override int Count
        {
            get
            {
                return BomReports.Count();
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
                convertView = Inflater.Inflate(Resource.Layout.adapter_bom_report, parent, false);

                holder = new Holder()
                {
                    txtViewCodeBOM = convertView.FindViewById<TextView>(Resource.Id.txtViewCodeBOM),
                    txtViewMaterialBOM = convertView.FindViewById<TextView>(Resource.Id.txtViewMaterialBOM),
                    txtViewUnidadBOM = convertView.FindViewById<TextView>(Resource.Id.txtViewUnidadBOM),
                    txtViewSupCodeBOM = convertView.FindViewById<TextView>(Resource.Id.txtViewSupCodeBOM)
                };

                convertView.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as Holder;
            }

            var pos = BomReports.ElementAt(position);

            holder.txtViewCodeBOM.Text = pos._MaterialCode;
            holder.txtViewCodeBOM.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewCodeBOM.SetTextColor(Android.Graphics.Color.Black);

            holder.txtViewMaterialBOM.Text = pos.MaterialName;
            holder.txtViewMaterialBOM.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewMaterialBOM.SetTextColor(Android.Graphics.Color.Black);

            holder.txtViewUnidadBOM.Text = pos.MaterialUnit ?? pos.Unit;
            holder.txtViewUnidadBOM.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewUnidadBOM.SetTextColor(Android.Graphics.Color.Black);

            holder.txtViewSupCodeBOM.Text = pos.MaterialReference;
            holder.txtViewSupCodeBOM.SetTypeface(null, Android.Graphics.TypefaceStyle.Normal);
            holder.txtViewSupCodeBOM.SetTextColor(Android.Graphics.Color.Black);

            return convertView;
        }

        private class Holder : Java.Lang.Object
        {
            public TextView txtViewCodeBOM;
            public TextView txtViewMaterialBOM;
            public TextView txtViewSupCodeBOM;
            public TextView txtViewUnidadBOM;
        }
    }
}