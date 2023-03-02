using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Discovery.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='DiscoveryPacketDecoder']"
	[global::Android.Runtime.Register ("com/zebra/android/discovery/internal/DiscoveryPacketDecoder", DoNotGenerateAcw=true)]
	public partial class DiscoveryPacketDecoder : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/discovery/internal/DiscoveryPacketDecoder", typeof (DiscoveryPacketDecoder));
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

		protected DiscoveryPacketDecoder (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='DiscoveryPacketDecoder']/constructor[@name='DiscoveryPacketDecoder' and count(parameter)=1 and parameter[1][@type='byte[]']]"
		[Register (".ctor", "([B)V", "")]
		public unsafe DiscoveryPacketDecoder (byte[] p0)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "([B)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

		static Delegate cb_getDnsName;
#pragma warning disable 0169
		static Delegate GetGetDnsNameHandler ()
		{
			if (cb_getDnsName == null)
				cb_getDnsName = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetDnsName);
			return cb_getDnsName;
		}

		static IntPtr n_GetDnsName (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Discovery.Internal.DiscoveryPacketDecoder __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.DiscoveryPacketDecoder> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.DnsName);
		}
#pragma warning restore 0169

		public virtual unsafe string DnsName {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='DiscoveryPacketDecoder']/method[@name='getDnsName' and count(parameter)=0]"
			[Register ("getDnsName", "()Ljava/lang/String;", "GetGetDnsNameHandler")]
			get {
				const string __id = "getDnsName.()Ljava/lang/String;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

		static Delegate cb_getIpAddress;
#pragma warning disable 0169
		static Delegate GetGetIpAddressHandler ()
		{
			if (cb_getIpAddress == null)
				cb_getIpAddress = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetIpAddress);
			return cb_getIpAddress;
		}

		static IntPtr n_GetIpAddress (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Discovery.Internal.DiscoveryPacketDecoder __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.DiscoveryPacketDecoder> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.IpAddress);
		}
#pragma warning restore 0169

		public virtual unsafe string IpAddress {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='DiscoveryPacketDecoder']/method[@name='getIpAddress' and count(parameter)=0]"
			[Register ("getIpAddress", "()Ljava/lang/String;", "GetGetIpAddressHandler")]
			get {
				const string __id = "getIpAddress.()Ljava/lang/String;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

		static Delegate cb_getPrinterPort;
#pragma warning disable 0169
		static Delegate GetGetPrinterPortHandler ()
		{
			if (cb_getPrinterPort == null)
				cb_getPrinterPort = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int>) n_GetPrinterPort);
			return cb_getPrinterPort;
		}

		static int n_GetPrinterPort (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Discovery.Internal.DiscoveryPacketDecoder __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.DiscoveryPacketDecoder> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.PrinterPort;
		}
#pragma warning restore 0169

		public virtual unsafe int PrinterPort {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='DiscoveryPacketDecoder']/method[@name='getPrinterPort' and count(parameter)=0]"
			[Register ("getPrinterPort", "()I", "GetGetPrinterPortHandler")]
			get {
				const string __id = "getPrinterPort.()I";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualInt32Method (__id, this, null);
					return __rm;
				} finally {
				}
			}
		}

		static Delegate cb_getDiscoveryEntryValue_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetGetDiscoveryEntryValue_Ljava_lang_String_Handler ()
		{
			if (cb_getDiscoveryEntryValue_Ljava_lang_String_ == null)
				cb_getDiscoveryEntryValue_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr>) n_GetDiscoveryEntryValue_Ljava_lang_String_);
			return cb_getDiscoveryEntryValue_Ljava_lang_String_;
		}

		static IntPtr n_GetDiscoveryEntryValue_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Discovery.Internal.DiscoveryPacketDecoder __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.DiscoveryPacketDecoder> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewString (__this.GetDiscoveryEntryValue (p0));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='DiscoveryPacketDecoder']/method[@name='getDiscoveryEntryValue' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("getDiscoveryEntryValue", "(Ljava/lang/String;)Ljava/lang/String;", "GetGetDiscoveryEntryValue_Ljava_lang_String_Handler")]
		public virtual unsafe string GetDiscoveryEntryValue (string p0)
		{
			const string __id = "getDiscoveryEntryValue.(Ljava/lang/String;)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}
}
