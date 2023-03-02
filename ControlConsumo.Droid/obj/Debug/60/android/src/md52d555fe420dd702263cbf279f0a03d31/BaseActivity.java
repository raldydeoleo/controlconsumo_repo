package md52d555fe420dd702263cbf279f0a03d31;


public class BaseActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_CheckPermissionGranted:(Ljava/lang/String;)Z:__export__\n" +
			"n_onRequestPermissionsResult:(I[Ljava/lang/String;[I)V:GetOnRequestPermissionsResult_IarrayLjava_lang_String_arrayIHandler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"";
		mono.android.Runtime.register ("ControlConsumo.Droid.Activities.BaseActivity, ControlConsumo.Droid", BaseActivity.class, __md_methods);
	}


	public BaseActivity ()
	{
		super ();
		if (getClass () == BaseActivity.class)
			mono.android.TypeManager.Activate ("ControlConsumo.Droid.Activities.BaseActivity, ControlConsumo.Droid", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public boolean CheckPermissionGranted (java.lang.String p0)
	{
		return n_CheckPermissionGranted (p0);
	}

	private native boolean n_CheckPermissionGranted (java.lang.String p0);


	public void onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2)
	{
		n_onRequestPermissionsResult (p0, p1, p2);
	}

	private native void n_onRequestPermissionsResult (int p0, java.lang.String[] p1, int[] p2);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();

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
