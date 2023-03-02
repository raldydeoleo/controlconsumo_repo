using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Discovery.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='MulticastBroadcast']"
	[global::Android.Runtime.Register ("com/zebra/android/discovery/internal/MulticastBroadcast", DoNotGenerateAcw=true)]
	public partial class MulticastBroadcast : global::Com.Zebra.Android.Discovery.Internal.BroadcastA {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/discovery/internal/MulticastBroadcast", typeof (MulticastBroadcast));
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

		protected MulticastBroadcast (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='MulticastBroadcast']/constructor[@name='MulticastBroadcast' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register (".ctor", "(I)V", "")]
		public unsafe MulticastBroadcast (int p0)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(I)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
			}
		}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='MulticastBroadcast']/constructor[@name='MulticastBroadcast' and count(parameter)=2 and parameter[1][@type='int'] and parameter[2][@type='int']]"
		[Register (".ctor", "(II)V", "")]
		public unsafe MulticastBroadcast (int p0, int p1)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(II)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (p0);
				__args [1] = new JniArgumentValue (p1);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
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
			global::Com.Zebra.Android.Discovery.Internal.MulticastBroadcast __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.MulticastBroadcast> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket p0 = (global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket)global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetSocketOptions (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='MulticastBroadcast']/method[@name='setSocketOptions' and count(parameter)=1 and parameter[1][@type='com.zebra.android.discovery.internal.ZebraDiscoSocket']]"
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
