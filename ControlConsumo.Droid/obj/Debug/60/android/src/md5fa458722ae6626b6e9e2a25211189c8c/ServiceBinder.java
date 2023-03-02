package md5fa458722ae6626b6e9e2a25211189c8c;


public class ServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ControlConsumo.Droid.Sync.ServiceBinder, ControlConsumo.Droid", ServiceBinder.class, __md_methods);
	}


	public ServiceBinder ()
	{
		super ();
		if (getClass () == ServiceBinder.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Sync.ServiceBinder, ControlConsumo.Droid", "", this, new java.lang.Object[] {  });
	}

	public ServiceBinder (md5fa458722ae6626b6e9e2a25211189c8c.SyncService p0)
	{
		super ();
		if (getClass () == ServiceBinder.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Sync.ServiceBinder, ControlConsumo.Droid", "ControlConsumo.Droid.Sync.SyncService, ControlConsumo.Droid", this, new java.lang.Object[] { p0 });
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
