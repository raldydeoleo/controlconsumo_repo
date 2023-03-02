package md52d555fe420dd702263cbf279f0a03d31;


public class SyncActivity
	extends md52d555fe420dd702263cbf279f0a03d31.BaseActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ControlConsumo.Droid.Activities.SyncActivity, ControlConsumo.Droid", SyncActivity.class, __md_methods);
	}


	public SyncActivity ()
	{
		super ();
		if (getClass () == SyncActivity.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Activities.SyncActivity, ControlConsumo.Droid", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
