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

namespace ControlConsumo.Droid.Activities.Adapters
{
    class SelectedSpinnerAdapter : BaseAdapter, ISpinnerAdapter
    {
        private readonly List<String> mCollections;
        private readonly ArrayAdapter<String> Collections;
        private readonly Context context;
        private readonly LayoutInflater layoutInflater;
        private readonly Boolean NativeLayout;
        private int SelectedIndex;

        public SelectedSpinnerAdapter(Context context, List<String> Collections, Boolean NativeLayout = false)
        {
            this.NativeLayout = NativeLayout;
            this.mCollections = Collections;
            this.context = context;
            this.Collections = new ArrayAdapter<string>(context, NativeLayout ? Android.Resource.Layout.SimpleSpinnerDropDownItem : Resource.Layout.spinner_custom_layout, Collections);
            layoutInflater = LayoutInflater.From(context);
            SelectedIndex = -1;
        }

        public String SelectedText
        {
            set
            {
                if (value == String.Empty)
                {
                    SelectedIndex = -1;
                }
                else
                {
                    SelectedIndex = mCollections.IndexOf(value);
                }
            }
        }

        public override int Count
        {
            get { return mCollections.Count(); }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return Collections.GetItem(position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (SelectedIndex > -1)
            {
                var tmp = SelectedIndex;
                SelectedIndex = -1;
                return Collections.GetView(tmp, null, parent);
            }

            return Collections.GetView(position, null, parent);
        }

        View ISpinnerAdapter.GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            return Collections.GetDropDownView(position, null, parent);
        }

        int IAdapter.Count
        {
            get { return Collections.Count; }
        }

        Java.Lang.Object IAdapter.GetItem(int position)
        {
            return position;
        }

        long IAdapter.GetItemId(int position)
        {
            return position;
        }

        int IAdapter.GetItemViewType(int position)
        {
            return 0;
        }

        View IAdapter.GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }

        bool IAdapter.HasStableIds
        {
            get { return Collections.HasStableIds; }
        }

        bool IAdapter.IsEmpty
        {
            get { return Collections.IsEmpty; }
        }

        void IAdapter.RegisterDataSetObserver(Android.Database.DataSetObserver observer)
        {
            Collections.RegisterDataSetObserver(observer);
        }

        void IAdapter.UnregisterDataSetObserver(Android.Database.DataSetObserver observer)
        {
            Collections.UnregisterDataSetObserver(observer);
        }

        int IAdapter.ViewTypeCount
        {
            get { return 1; }
        }

        void IDisposable.Dispose()
        {
            Collections.Dispose();
        }
    }
}