using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Discovery.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='FindPrinters']"
	[global::Android.Runtime.Register ("com/zebra/android/discovery/internal/FindPrinters", DoNotGenerateAcw=true)]
	public partial class FindPrinters : global::Com.Zebra.Android.Discovery.Internal.BroadcastA {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/discovery/internal/FindPrinters", typeof (FindPrinters));
		internal static new IntPtr class_ref {
			get {
				return _members.JniPeerType.PeerReference.Handle;
			}
		}

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override IntPtr ThresholdClass {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		protected FindPrinters (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='FindPrinters']/constructor[@name='FindPrinters' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe FindPrinters ()
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "()V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			try {
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), null);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_setSocketOptions_Lcom_zebra_android_discovery_internal_ZebraDiscoSocket_;
#pragma warning disable 0169
		static Delegate GetSetSocketOptions_Lcom_zebra_android_discovery_internal_ZebraDiscoSocket_Handler ()
		{
			if (cb_setSocketOptions_Lcom_zebra_android_discovery_internal_ZebraDiscoSocket_ == null)
				cb_setSocketOptions_Lcom_zebra_android_discovery_internal_ZebraDiscoSocket_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetSocketOptions_Lcom_zebra_android_discovery_internal_ZebraDiscoSocket_);
			return cb_setSocketOptions_Lcom_zebra_android_discovery_internal_ZebraDiscoSocket_;
		}

		static void n_SetSocketOptions_Lcom_zebra_android_discovery_internal_ZebraDiscoSocket_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Discovery.Internal.FindPrinters __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.FindPrinters> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket p0 = (global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket)global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetSocketOptions (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='FindPrinters']/method[@name='setSocketOptions' and count(parameter)=1 and parameter[1][@type='com.zebra.android.discovery.internal.ZebraDiscoSocket']]"
		[Register ("setSocketOptions", "(Lcom/zebra/android/discovery/internal/ZebraDiscoSocket;)V", "GetSetSocketOptions_Lcom_zebra_android_discovery_internal_ZebraDiscoSocket_Handler")]
		protected override unsafe void SetSocketOptions (global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket p0)
		{
			const string __id = "setSocketOptions.(Lcom/zebra/android/discovery/internal/ZebraDiscoSocket;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
		}

	}
}
