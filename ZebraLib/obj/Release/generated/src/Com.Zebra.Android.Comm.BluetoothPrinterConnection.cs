using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Comm {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='BluetoothPrinterConnection']"
	[global::Android.Runtime.Register ("com/zebra/android/comm/BluetoothPrinterConnection", DoNotGenerateAcw=true)]
	public partial class BluetoothPrinterConnection : global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='BluetoothPrinterConnection']/field[@name='macAddress']"
		[Register ("macAddress")]
		protected string MacAddress {
			get {
				const string __id = "macAddress.Ljava/lang/String;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "macAddress.Ljava/lang/String;";

				IntPtr native_value = JNIEnv.NewString (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/comm/BluetoothPrinterConnection", typeof (BluetoothPrinterConnection));
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

		protected BluetoothPrinterConnection (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='BluetoothPrinterConnection']/constructor[@name='BluetoothPrinterConnection' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register (".ctor", "(Ljava/lang/String;)V", "")]
		public unsafe BluetoothPrinterConnection (string p0)
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

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='BluetoothPrinterConnection']/constructor[@name='BluetoothPrinterConnection' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
		[Register (".ctor", "(Ljava/lang/String;II)V", "")]
		public unsafe BluetoothPrinterConnection (string p0, int p1, int p2)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Ljava/lang/String;II)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (p2);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='BluetoothPrinterConnection']/constructor[@name='BluetoothPrinterConnection' and count(parameter)=4 and parameter[1][@type='com.zebra.android.comm.internal.ZebraConnector'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/internal/ZebraConnector;Ljava/lang/String;II)V", "")]
		protected unsafe BluetoothPrinterConnection (global::Com.Zebra.Android.Comm.Internal.IZebraConnector p0, string p1, int p2, int p3)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Lcom/zebra/android/comm/internal/ZebraConnector;Ljava/lang/String;II)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [4];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (p2);
				__args [3] = new JniArgumentValue (p3);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static Delegate cb_getFriendlyName;
#pragma warning disable 0169
		static Delegate GetGetFriendlyNameHandler ()
		{
			if (cb_getFriendlyName == null)
				cb_getFriendlyName = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetFriendlyName);
			return cb_getFriendlyName;
		}

		static IntPtr n_GetFriendlyName (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.BluetoothPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.BluetoothPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.FriendlyName);
		}
#pragma warning restore 0169

		public virtual unsafe string FriendlyName {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='BluetoothPrinterConnection']/method[@name='getFriendlyName' and count(parameter)=0]"
			[Register ("getFriendlyName", "()Ljava/lang/String;", "GetGetFriendlyNameHandler")]
			get {
				const string __id = "getFriendlyName.()Ljava/lang/String;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

		static Delegate cb_getMACAddress;
#pragma warning disable 0169
		static Delegate GetGetMACAddressHandler ()
		{
			if (cb_getMACAddress == null)
				cb_getMACAddress = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetMACAddress);
			return cb_getMACAddress;
		}

		static IntPtr n_GetMACAddress (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.BluetoothPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.BluetoothPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.MACAddress);
		}
#pragma warning restore 0169

		public virtual unsafe string MACAddress {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='BluetoothPrinterConnection']/method[@name='getMACAddress' and count(parameter)=0]"
			[Register ("getMACAddress", "()Ljava/lang/String;", "GetGetMACAddressHandler")]
			get {
				const string __id = "getMACAddress.()Ljava/lang/String;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

	}
}
