package md518ee0a785a2d301aa5fa67e65b5179e8;


public class StockAdapterExpandable_Stocks
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ControlConsumo.Droid.Activities.Adapters.StockAdapterExpandable+Stocks, ControlConsumo.Droid", StockAdapterExpandable_Stocks.class, __md_methods);
	}


	public StockAdapterExpandable_Stocks ()
	{
		super ();
		if (getClass () == StockAdapterExpandable_Stocks.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Activities.Adapters.StockAdapterExpandable+Stocks, ControlConsumo.Droid", "", this, new java.lang.Object[] {  });
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
