using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Discovery.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']"
	[global::Android.Runtime.Register ("com/zebra/android/discovery/internal/BroadcastA", DoNotGenerateAcw=true)]
	public abstract partial class BroadcastA : global::Java.Lang.Object {


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/field[@name='DEFAULT_LATE_ARRIVAL_DELAY']"
		[Register ("DEFAULT_LATE_ARRIVAL_DELAY")]
		protected const int DefaultLateArrivalDelay = (int) 6000;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/field[@name='MAX_DATAGRAM_SIZE']"
		[Register ("MAX_DATAGRAM_SIZE")]
		public const int MaxDatagramSize = (int) 600;


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/field[@name='broadcastIpAddresses']"
		[Register ("broadcastIpAddresses")]
		protected IList<Java.Net.InetAddress> BroadcastIpAddresses {
			get {
				const string __id = "broadcastIpAddresses.[Ljava/net/InetAddress;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Android.Runtime.JavaArray<global::Java.Net.InetAddress>.FromJniHandle (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "broadcastIpAddresses.[Ljava/net/InetAddress;";

				IntPtr native_value = global::Android.Runtime.JavaArray<global::Java.Net.InetAddress>.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					global::Android.Runtime.JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/field[@name='discoveryHandler']"
		[Register ("discoveryHandler")]
		protected global::Com.Zebra.Android.Discovery.IDiscoveryHandler DiscoveryHandler {
			get {
				const string __id = "discoveryHandler.Lcom/zebra/android/discovery/DiscoveryHandler;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.IDiscoveryHandler> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "discoveryHandler.Lcom/zebra/android/discovery/DiscoveryHandler;";

				IntPtr native_value = global::Android.Runtime.JNIEnv.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					global::Android.Runtime.JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/discovery/internal/BroadcastA", typeof (BroadcastA));
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

		protected BroadcastA (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/constructor[@name='BroadcastA' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register (".ctor", "(I)V", "")]
		protected unsafe BroadcastA (int p0)
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

		static Delegate cb_createDiscoSocket;
#pragma warning disable 0169
		static Delegate GetCreateDiscoSocketHandler ()
		{
			if (cb_createDiscoSocket == null)
				cb_createDiscoSocket = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_CreateDiscoSocket);
			return cb_createDiscoSocket;
		}

		static IntPtr n_CreateDiscoSocket (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Discovery.Internal.BroadcastA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.BroadcastA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.CreateDiscoSocket ());
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/method[@name='createDiscoSocket' and count(parameter)=0]"
		[Register ("createDiscoSocket", "()Lcom/zebra/android/discovery/internal/ZebraDiscoSocket;", "GetCreateDiscoSocketHandler")]
		protected virtual unsafe global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket CreateDiscoSocket ()
		{
			const string __id = "createDiscoSocket.()Lcom/zebra/android/discovery/internal/ZebraDiscoSocket;";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}

		static Delegate cb_doBroadcast_Lcom_zebra_android_discovery_DiscoveryHandler_;
#pragma warning disable 0169
		static Delegate GetDoBroadcast_Lcom_zebra_android_discovery_DiscoveryHandler_Handler ()
		{
			if (cb_doBroadcast_Lcom_zebra_android_discovery_DiscoveryHandler_ == null)
				cb_doBroadcast_Lcom_zebra_android_discovery_DiscoveryHandler_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_DoBroadcast_Lcom_zebra_android_discovery_DiscoveryHandler_);
			return cb_doBroadcast_Lcom_zebra_android_discovery_DiscoveryHandler_;
		}

		static void n_DoBroadcast_Lcom_zebra_android_discovery_DiscoveryHandler_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Discovery.Internal.BroadcastA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.BroadcastA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0 = (global::Com.Zebra.Android.Discovery.IDiscoveryHandler)global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.IDiscoveryHandler> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.DoBroadcast (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/method[@name='doBroadcast' and count(parameter)=1 and parameter[1][@type='com.zebra.android.discovery.DiscoveryHandler']]"
		[Register ("doBroadcast", "(Lcom/zebra/android/discovery/DiscoveryHandler;)V", "GetDoBroadcast_Lcom_zebra_android_discovery_DiscoveryHandler_Handler")]
		public virtual unsafe void DoBroadcast (global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0)
		{
			const string __id = "doBroadcast.(Lcom/zebra/android/discovery/DiscoveryHandler;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
		}

		static Delegate cb_doDiscovery;
#pragma warning disable 0169
		static Delegate GetDoDiscoveryHandler ()
		{
			if (cb_doDiscovery == null)
				cb_doDiscovery = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, bool>) n_DoDiscovery);
			return cb_doDiscovery;
		}

		static bool n_DoDiscovery (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Discovery.Internal.BroadcastA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.BroadcastA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.DoDiscovery ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/method[@name='doDiscovery' and count(parameter)=0]"
		[Register ("doDiscovery", "()Z", "GetDoDiscoveryHandler")]
		protected virtual unsafe bool DoDiscovery ()
		{
			const string __id = "doDiscovery.()Z";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualBooleanMethod (__id, this, null);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/method[@name='getDnsNameFromDiscoveryPacket' and count(parameter)=1 and parameter[1][@type='byte[]']]"
		[Register ("getDnsNameFromDiscoveryPacket", "([B)Ljava/lang/String;", "")]
		protected static unsafe string GetDnsNameFromDiscoveryPacket (byte[] p0)
		{
			const string __id = "getDnsNameFromDiscoveryPacket.([B)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/method[@name='getIpAddressFromDiscoveryPacket' and count(parameter)=1 and parameter[1][@type='byte[]']]"
		[Register ("getIpAddressFromDiscoveryPacket", "([B)Ljava/lang/String;", "")]
		protected static unsafe string GetIpAddressFromDiscoveryPacket (byte[] p0)
		{
			const string __id = "getIpAddressFromDiscoveryPacket.([B)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/method[@name='getPortBasedOnProductName' and count(parameter)=1 and parameter[1][@type='byte[]']]"
		[Register ("getPortBasedOnProductName", "([B)I", "")]
		protected static unsafe int GetPortBasedOnProductName (byte[] p0)
		{
			const string __id = "getPortBasedOnProductName.([B)I";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeInt32Method (__id, __args);
				return __rm;
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
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
			global::Com.Zebra.Android.Discovery.Internal.BroadcastA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.BroadcastA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket p0 = (global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket)global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetSocketOptions (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/method[@name='setSocketOptions' and count(parameter)=1 and parameter[1][@type='com.zebra.android.discovery.internal.ZebraDiscoSocket']]"
		[Register ("setSocketOptions", "(Lcom/zebra/android/discovery/internal/ZebraDiscoSocket;)V", "GetSetSocketOptions_Lcom_zebra_android_discovery_internal_ZebraDiscoSocket_Handler")]
		protected abstract void SetSocketOptions (global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket p0);

	}

	[global::Android.Runtime.Register ("com/zebra/android/discovery/internal/BroadcastA", DoNotGenerateAcw=true)]
	internal partial class BroadcastAInvoker : BroadcastA {

		public BroadcastAInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer) {}

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/discovery/internal/BroadcastA", typeof (BroadcastAInvoker));

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='BroadcastA']/method[@name='setSocketOptions' and count(parameter)=1 and parameter[1][@type='com.zebra.android.discovery.internal.ZebraDiscoSocket']]"
		[Register ("setSocketOptions", "(Lcom/zebra/android/discovery/internal/ZebraDiscoSocket;)V", "GetSetSocketOptions_Lcom_zebra_android_discovery_internal_ZebraDiscoSocket_Handler")]
		protected override unsafe void SetSocketOptions (global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket p0)
		{
			const string __id = "setSocketOptions.(Lcom/zebra/android/discovery/internal/ZebraDiscoSocket;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				_members.InstanceMethods.InvokeAbstractVoidMethod (__id, this, __args);
			} finally {
			}
		}

	}

}
