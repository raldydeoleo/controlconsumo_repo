package md518ee0a785a2d301aa5fa67e65b5179e8;


public class ConfigAdapterExpandable_EquipmentFilter
	extends android.widget.Filter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_performFiltering:(Ljava/lang/CharSequence;)Landroid/widget/Filter$FilterResults;:GetPerformFiltering_Ljava_lang_CharSequence_Handler\n" +
			"n_publishResults:(Ljava/lang/CharSequence;Landroid/widget/Filter$FilterResults;)V:GetPublishResults_Ljava_lang_CharSequence_Landroid_widget_Filter_FilterResults_Handler\n" +
			"";
		mono.android.Runtime.register ("ControlConsumo.Droid.Activities.Adapters.ConfigAdapterExpandable+EquipmentFilter, ControlConsumo.Droid", ConfigAdapterExpandable_EquipmentFilter.class, __md_methods);
	}


	public ConfigAdapterExpandable_EquipmentFilter ()
	{
		super ();
		if (getClass () == ConfigAdapterExpandable_EquipmentFilter.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Activities.Adapters.ConfigAdapterExpandable+EquipmentFilter, ControlConsumo.Droid", "", this, new java.lang.Object[] {  });
	}

	public ConfigAdapterExpandable_EquipmentFilter (md518ee0a785a2d301aa5fa67e65b5179e8.ConfigAdapterExpandable p0)
	{
		super ();
		if (getClass () == ConfigAdapterExpandable_EquipmentFilter.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Activities.Adapters.ConfigAdapterExpandable+EquipmentFilter, ControlConsumo.Droid", "ControlConsumo.Droid.Activities.Adapters.ConfigAdapterExpandable, ControlConsumo.Droid", this, new java.lang.Object[] { p0 });
	}


	public android.widget.Filter.FilterResults performFiltering (java.lang.CharSequence p0)
	{
		return n_performFiltering (p0);
	}

	private native android.widget.Filter.FilterResults n_performFiltering (java.lang.CharSequence p0);


	public void publishResults (java.lang.CharSequence p0, android.widget.Filter.FilterResults p1)
	{
		n_publishResults (p0, p1);
	}

	private native void n_publishResults (java.lang.CharSequence p0, android.widget.Filter.FilterResults p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
