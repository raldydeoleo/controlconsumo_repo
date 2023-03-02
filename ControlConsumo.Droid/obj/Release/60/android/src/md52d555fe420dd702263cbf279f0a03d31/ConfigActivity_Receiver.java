package md52d555fe420dd702263cbf279f0a03d31;


public class ConfigActivity_Receiver
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("ControlConsumo.Droid.Activities.ConfigActivity+Receiver, ControlConsumo.Droid", ConfigActivity_Receiver.class, __md_methods);
	}


	public ConfigActivity_Receiver ()
	{
		super ();
		if (getClass () == ConfigActivity_Receiver.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Activities.ConfigActivity+Receiver, ControlConsumo.Droid", "", this, new java.lang.Object[] {  });
	}

	public ConfigActivity_Receiver (md52d555fe420dd702263cbf279f0a03d31.ConfigActivity p0)
	{
		super ();
		if (getClass () == ConfigActivity_Receiver.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Activities.ConfigActivity+Receiver, ControlConsumo.Droid", "ControlConsumo.Droid.Activities.ConfigActivity, ControlConsumo.Droid", this, new java.lang.Object[] { p0 });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

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
