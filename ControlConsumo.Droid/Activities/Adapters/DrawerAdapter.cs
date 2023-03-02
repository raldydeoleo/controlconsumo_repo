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
    class DrawerAdapter : BaseAdapter
    {
        private IEnumerable<DrawerHolder> Entries { get; set; }
        private LayoutInflater _Inflater { get; set; }
        private Context Cxt { get; set; }

        public DrawerAdapter(Context cxt, IEnumerable<DrawerEntry> entries)
        {
            Cxt = cxt;
            Entries = entries.Select(p => new DrawerHolder(p));
        }

        public override int Count
        {
            get { return Entries.Count(); }
        }     

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (_Inflater == null)
            {
                _Inflater = (LayoutInflater)Cxt.GetSystemService(Context.LayoutInflaterService);
            }

            convertView = _Inflater.Inflate(Resource.Layout.entry_drawer_layout, parent, false);
            var icon = convertView.FindViewById<ImageView>(Resource.Id.DrawerIcon);
            var title = convertView.FindViewById<TextView>(Resource.Id.DrawerTitle);
            var Entry = Entries.ElementAt(position);
            icon.SetImageDrawable(Cxt.Resources.GetDrawable(Entry.Icon));
            title.Text = Entry.Description;
            convertView.Tag = Entry;

            return convertView;
        }

        public class DrawerHolder : Java.Lang.Object
        {
            private DrawerEntry Entry { get; set; }

            public DrawerHolder(DrawerEntry entry)
            {
                Entry = entry;
            }

            public Type activity { get { return Entry.activity; } }
            public int Icon { get { return Entry.Icon; } }
            public String Description { get { return Entry.Description; } }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return Entries.ElementAt(position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }
    }
}