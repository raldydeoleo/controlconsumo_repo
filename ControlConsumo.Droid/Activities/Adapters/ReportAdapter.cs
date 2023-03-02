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
using ControlConsumo.Droid.Activities.Adapters.Entities;

namespace ControlConsumo.Droid.Activities.Adapters
{
    class ReportAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly LayoutInflater Inflater;
        private readonly IEnumerable<ReportEntry> Lista;

        public ReportAdapter(Context context, IEnumerable<ReportEntry> Lista)
        {
            this.context = context;
            this.Lista = Lista;
            this.Inflater = LayoutInflater.From(context);
        }

        public override int Count
        {
            get { return Lista.Count(); }
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
                convertView = Inflater.Inflate(Resource.Layout.grid_element, null);
                holder.grid_element_image = convertView.FindViewById<ImageView>(Resource.Id.grid_element_image);
                holder.grid_element_description = convertView.FindViewById<TextView>(Resource.Id.grid_element_description);
                convertView.Tag = holder;
            }
            else
            {
                holder = convertView.Tag as Holder;
            }

            var pos = Lista.ElementAt(position);

            holder.grid_element_image.SetBackgroundResource(pos.Imagen);
            holder.grid_element_description.Text = pos.Title;

            return convertView;
        }

        private class Holder : Java.Lang.Object
        {
            public ImageView grid_element_image;
            public TextView grid_element_description;
        }
    }
}