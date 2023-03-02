package md5fa458722ae6626b6e9e2a25211189c8c;


public class ServiceConnection
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.content.ServiceConnection
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onServiceConnected:(Landroid/content/ComponentName;Landroid/os/IBinder;)V:GetOnServiceConnected_Landroid_content_ComponentName_Landroid_os_IBinder_Handler:Android.Content.IServiceConnectionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onServiceDisconnected:(Landroid/content/ComponentName;)V:GetOnServiceDisconnected_Landroid_content_ComponentName_Handler:Android.Content.IServiceConnectionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("ControlConsumo.Droid.Sync.ServiceConnection, ControlConsumo.Droid", ServiceConnection.class, __md_methods);
	}


	public ServiceConnection ()
	{
		super ();
		if (getClass () == ServiceConnection.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Sync.ServiceConnection, ControlConsumo.Droid", "", this, new java.lang.Object[] {  });
	}

	public ServiceConnection (md52d555fe420dd702263cbf279f0a03d31.MenuActivity p0)
	{
		super ();
		if (getClass () == ServiceConnection.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Sync.ServiceConnection, ControlConsumo.Droid", "ControlConsumo.Droid.Activities.MenuActivity, ControlConsumo.Droid", this, new java.lang.Object[] { p0 });
	}


	public void onServiceConnected (android.content.ComponentName p0, android.os.IBinder p1)
	{
		n_onServiceConnected (p0, p1);
	}

	private native void n_onServiceConnected (android.content.ComponentName p0, android.os.IBinder p1);


	public void onServiceDisconnected (android.content.ComponentName p0)
	{
		n_onServiceDisconnected (p0);
	}

	private native void n_onServiceDisconnected (android.content.ComponentName p0);

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
