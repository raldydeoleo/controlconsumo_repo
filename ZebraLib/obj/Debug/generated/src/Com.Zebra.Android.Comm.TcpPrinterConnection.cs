using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Comm {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='TcpPrinterConnection']"
	[global::Android.Runtime.Register ("com/zebra/android/comm/TcpPrinterConnection", DoNotGenerateAcw=true)]
	public partial class TcpPrinterConnection : global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA {


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='TcpPrinterConnection']/field[@name='DEFAULT_CPCL_TCP_PORT']"
		[Register ("DEFAULT_CPCL_TCP_PORT")]
		public const int DefaultCpclTcpPort = (int) 6101;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='TcpPrinterConnection']/field[@name='DEFAULT_ZPL_TCP_PORT']"
		[Register ("DEFAULT_ZPL_TCP_PORT")]
		public const int DefaultZplTcpPort = (int) 9100;
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/comm/TcpPrinterConnection", typeof (TcpPrinterConnection));
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

		protected TcpPrinterConnection (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='TcpPrinterConnection']/constructor[@name='TcpPrinterConnection' and count(parameter)=3 and parameter[1][@type='com.zebra.android.comm.internal.ZebraConnector'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/internal/ZebraConnector;II)V", "")]
		protected unsafe TcpPrinterConnection (global::Com.Zebra.Android.Comm.Internal.IZebraConnector p0, int p1, int p2)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Lcom/zebra/android/comm/internal/ZebraConnector;II)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (p2);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
			}
		}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='TcpPrinterConnection']/constructor[@name='TcpPrinterConnection' and count(parameter)=4 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
		[Register (".ctor", "(Ljava/lang/String;III)V", "")]
		public unsafe TcpPrinterConnection (string p0, int p1, int p2, int p3)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Ljava/lang/String;III)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [4];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (p2);
				__args [3] = new JniArgumentValue (p3);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='TcpPrinterConnection']/constructor[@name='TcpPrinterConnection' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int']]"
		[Register (".ctor", "(Ljava/lang/String;I)V", "")]
		public unsafe TcpPrinterConnection (string p0, int p1)
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

		static Delegate cb_getAddress;
#pragma warning disable 0169
		static Delegate GetGetAddressHandler ()
		{
			if (cb_getAddress == null)
				cb_getAddress = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetAddress);
			return cb_getAddress;
		}

		static IntPtr n_GetAddress (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.TcpPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.TcpPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.Address);
		}
#pragma warning restore 0169

		public virtual unsafe string Address {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='TcpPrinterConnection']/method[@name='getAddress' and count(parameter)=0]"
			[Register ("getAddress", "()Ljava/lang/String;", "GetGetAddressHandler")]
			get {
				const string __id = "getAddress.()Ljava/lang/String;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

		static Delegate cb_getPortNumber;
#pragma warning disable 0169
		static Delegate GetGetPortNumberHandler ()
		{
			if (cb_getPortNumber == null)
				cb_getPortNumber = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetPortNumber);
			return cb_getPortNumber;
		}

		static IntPtr n_GetPortNumber (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.TcpPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.TcpPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.PortNumber);
		}
#pragma warning restore 0169

		public virtual unsafe string PortNumber {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='TcpPrinterConnection']/method[@name='getPortNumber' and count(parameter)=0]"
			[Register ("getPortNumber", "()Ljava/lang/String;", "GetGetPortNumberHandler")]
			get {
				const string __id = "getPortNumber.()Ljava/lang/String;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

	}
}
