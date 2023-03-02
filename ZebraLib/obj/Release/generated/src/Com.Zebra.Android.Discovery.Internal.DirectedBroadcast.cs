using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Discovery.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='DirectedBroadcast']"
	[global::Android.Runtime.Register ("com/zebra/android/discovery/internal/DirectedBroadcast", DoNotGenerateAcw=true)]
	public partial class DirectedBroadcast : global::Com.Zebra.Android.Discovery.Internal.BroadcastA {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/discovery/internal/DirectedBroadcast", typeof (DirectedBroadcast));
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

		protected DirectedBroadcast (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='DirectedBroadcast']/constructor[@name='DirectedBroadcast' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register (".ctor", "(Ljava/lang/String;)V", "")]
		public unsafe DirectedBroadcast (string p0)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Ljava/lang/String;)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='DirectedBroadcast']/constructor[@name='DirectedBroadcast' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int']]"
		[Register (".ctor", "(Ljava/lang/String;I)V", "")]
		public unsafe DirectedBroadcast (string p0, int p1)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Ljava/lang/String;I)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (p1);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
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
			global::Com.Zebra.Android.Discovery.Internal.DirectedBroadcast __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.DirectedBroadcast> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket p0 = (global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket)global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetSocketOptions (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='DirectedBroadcast']/method[@name='setSocketOptions' and count(parameter)=1 and parameter[1][@type='com.zebra.android.discovery.internal.ZebraDiscoSocket']]"
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
