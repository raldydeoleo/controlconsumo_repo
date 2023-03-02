package md518ee0a785a2d301aa5fa67e65b5179e8;


public class CuadreAdapter_CuadreAdapterHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ControlConsumo.Droid.Activities.Adapters.CuadreAdapter+CuadreAdapterHolder, ControlConsumo.Droid", CuadreAdapter_CuadreAdapterHolder.class, __md_methods);
	}


	public CuadreAdapter_CuadreAdapterHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == CuadreAdapter_CuadreAdapterHolder.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Activities.Adapters.CuadreAdapter+CuadreAdapterHolder, ControlConsumo.Droid", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
