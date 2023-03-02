using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Comm.Internal {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.comm.internal']/interface[@name='ZebraConnector']"
	[Register ("com/zebra/android/comm/internal/ZebraConnector", "", "Com.Zebra.Android.Comm.Internal.IZebraConnectorInvoker")]
	public partial interface IZebraConnector : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm.internal']/interface[@name='ZebraConnector']/method[@name='open' and count(parameter)=0]"
		[Register ("open", "()Lcom/zebra/android/comm/internal/ZebraSocket;", "GetOpenHandler:Com.Zebra.Android.Comm.Internal.IZebraConnectorInvoker, ZebraLib")]
		global::Com.Zebra.Android.Comm.Internal.IZebraSocket Open ();

	}

	[global::Android.Runtime.Register ("com/zebra/android/comm/internal/ZebraConnector", DoNotGenerateAcw=true)]
	internal class IZebraConnectorInvoker : global::Java.Lang.Object, IZebraConnector {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/comm/internal/ZebraConnector", typeof (IZebraConnectorInvoker));

		static IntPtr java_class_ref {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		IntPtr class_ref;

		public static IZebraConnector GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IZebraConnector> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.comm.internal.ZebraConnector"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IZebraConnectorInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_open;
#pragma warning disable 0169
		static Delegate GetOpenHandler ()
		{
			if (cb_open == null)
				cb_open = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_Open);
			return cb_open;
		}

		static IntPtr n_Open (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.Internal.IZebraConnector __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.IZebraConnector> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.Open ());
		}
#pragma warning restore 0169

		IntPtr id_open;
		public unsafe global::Com.Zebra.Android.Comm.Internal.IZebraSocket Open ()
		{
			if (id_open == IntPtr.Zero)
				id_open = JNIEnv.GetMethodID (class_ref, "open", "()Lcom/zebra/android/comm/internal/ZebraSocket;");
			return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.IZebraSocket> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_open), JniHandleOwnership.TransferLocalRef);
		}

	}

}
