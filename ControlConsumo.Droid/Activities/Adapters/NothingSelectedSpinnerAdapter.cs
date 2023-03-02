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
    public class NothingSelectedSpinnerAdapter : BaseAdapter, ISpinnerAdapter
    {
        private List<String> mCollections { get; set; }
        private ArrayAdapter<String> Collections { get; set; }
        protected Context context { get; set; }
        protected LayoutInflater layoutInflater;
        private int SelectedIndex;
        private String NoText;
        private readonly Boolean NativeLayout;

        public NothingSelectedSpinnerAdapter(Context context, List<String> Collections, String Text = null, String NoText = null, Boolean NativeLayout = false)
        {
            this.NativeLayout = NativeLayout;
            this.NoText = NoText ?? "[No One]";
            this.context = context;
            Collections.Insert(0, Text ?? "[Select One]");
            mCollections = Collections;
            this.Collections = new ArrayAdapter<String>(this.context, NativeLayout ? Android.Resource.Layout.SimpleSpinnerDropDownItem : Resource.Layout.spinner_custom_layout, Collections.ToArray());
            this.SelectedIndex = -1;
            if (layoutInflater == null)
                layoutInflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
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

        public View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            if (position == 0)
            {
                var view = Collections.GetDropDownView(position, null, parent);

                TextView Text = null; //(TextView)base.GetDropDownView(position, convertView, parent);

                if (!NativeLayout)
                {
                    Text = view.FindViewById<TextView>(Resource.Id.text1);
                }
                else
                {
                    int id = view.Resources.GetIdentifier("android:id/text1", null, null);
                    Text = view.FindViewById<TextView>(id);
                }

                Text.Text = NoText;
                return view;
            }

            return Collections.GetDropDownView(position, null, parent);
        }

        public override int Count
        {
            get { return Collections.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position == 0 ? null : Collections.GetItem(position);
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public int GetItemViewType(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (position == 0 && SelectedIndex > -1)
            {
                return Collections.GetView(SelectedIndex, null, parent);
            }

            return Collections.GetView(position, null, parent);
        }

        public bool HasStableIds
        {
            get { return Collections.HasStableIds; }
        }

        public bool IsEmpty
        {
            get { return Collections.IsEmpty; }
        }

        public void RegisterDataSetObserver(Android.Database.DataSetObserver observer)
        {
            Collections.RegisterDataSetObserver(observer);
        }

        public void UnregisterDataSetObserver(Android.Database.DataSetObserver observer)
        {
            Collections.UnregisterDataSetObserver(observer);
        }

        public int ViewTypeCount
        {
            get { return 1; }
        }

        public void Dispose()
        {
            Collections.Dispose();
        }
    }
}