using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Comm {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']"
	[global::Android.Runtime.Register ("com/zebra/android/comm/ZebraPrinterConnectionA", DoNotGenerateAcw=true)]
	public abstract partial class ZebraPrinterConnectionA : global::Java.Lang.Object, global::Com.Zebra.Android.Comm.IZebraPrinterConnection {


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/field[@name='DEFAULT_MAX_TIMEOUT_FOR_READ']"
		[Register ("DEFAULT_MAX_TIMEOUT_FOR_READ")]
		protected const int DefaultMaxTimeoutForRead = (int) 5000;

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/field[@name='DEFAULT_TIME_TO_WAIT_FOR_MORE_DATA']"
		[Register ("DEFAULT_TIME_TO_WAIT_FOR_MORE_DATA")]
		protected const int DefaultTimeToWaitForMoreData = (int) 500;


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/field[@name='commLink']"
		[Register ("commLink")]
		protected global::Com.Zebra.Android.Comm.Internal.IZebraSocket CommLink {
			get {
				const string __id = "commLink.Lcom/zebra/android/comm/internal/ZebraSocket;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.IZebraSocket> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "commLink.Lcom/zebra/android/comm/internal/ZebraSocket;";

				IntPtr native_value = global::Android.Runtime.JNIEnv.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					global::Android.Runtime.JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/field[@name='inputStream']"
		[Register ("inputStream")]
		protected global::System.IO.Stream InputStream {
			get {
				const string __id = "inputStream.Ljava/io/InputStream;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Android.Runtime.InputStreamInvoker.FromJniHandle (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "inputStream.Ljava/io/InputStream;";

				IntPtr native_value = global::Android.Runtime.InputStreamAdapter.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/field[@name='outputStream']"
		[Register ("outputStream")]
		protected global::System.IO.Stream OutputStream {
			get {
				const string __id = "outputStream.Ljava/io/OutputStream;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Android.Runtime.OutputStreamInvoker.FromJniHandle (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "outputStream.Ljava/io/OutputStream;";

				IntPtr native_value = global::Android.Runtime.OutputStreamAdapter.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/field[@name='zebraConnector']"
		[Register ("zebraConnector")]
		protected global::Com.Zebra.Android.Comm.Internal.IZebraConnector ZebraConnector {
			get {
				const string __id = "zebraConnector.Lcom/zebra/android/comm/internal/ZebraConnector;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.IZebraConnector> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "zebraConnector.Lcom/zebra/android/comm/internal/ZebraConnector;";

				IntPtr native_value = global::Android.Runtime.JNIEnv.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					global::Android.Runtime.JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/comm/ZebraPrinterConnectionA", typeof (ZebraPrinterConnectionA));
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

		protected ZebraPrinterConnectionA (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/constructor[@name='ZebraPrinterConnectionA' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		protected unsafe ZebraPrinterConnectionA ()
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

		static Delegate cb_isConnected;
#pragma warning disable 0169
		static Delegate GetIsConnectedHandler ()
		{
			if (cb_isConnected == null)
				cb_isConnected = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, bool>) n_IsConnected);
			return cb_isConnected;
		}

		static bool n_IsConnected (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.IsConnected;
		}
#pragma warning restore 0169

		public virtual unsafe bool IsConnected {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/method[@name='isConnected' and count(parameter)=0]"
			[Register ("isConnected", "()Z", "GetIsConnectedHandler")]
			get {
				const string __id = "isConnected.()Z";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualBooleanMethod (__id, this, null);
					return __rm;
				} finally {
				}
			}
		}

		static Delegate cb_getMaxTimeoutForRead;
#pragma warning disable 0169
		static Delegate GetGetMaxTimeoutForReadHandler ()
		{
			if (cb_getMaxTimeoutForRead == null)
				cb_getMaxTimeoutForRead = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int>) n_GetMaxTimeoutForRead);
			return cb_getMaxTimeoutForRead;
		}

		static int n_GetMaxTimeoutForRead (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.MaxTimeoutForRead;
		}
#pragma warning restore 0169

		public virtual unsafe int MaxTimeoutForRead {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/method[@name='getMaxTimeoutForRead' and count(parameter)=0]"
			[Register ("getMaxTimeoutForRead", "()I", "GetGetMaxTimeoutForReadHandler")]
			get {
				const string __id = "getMaxTimeoutForRead.()I";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualInt32Method (__id, this, null);
					return __rm;
				} finally {
				}
			}
		}

		static Delegate cb_getTimeToWaitForMoreData;
#pragma warning disable 0169
		static Delegate GetGetTimeToWaitForMoreDataHandler ()
		{
			if (cb_getTimeToWaitForMoreData == null)
				cb_getTimeToWaitForMoreData = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int>) n_GetTimeToWaitForMoreData);
			return cb_getTimeToWaitForMoreData;
		}

		static int n_GetTimeToWaitForMoreData (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.TimeToWaitForMoreData;
		}
#pragma warning restore 0169

		public virtual unsafe int TimeToWaitForMoreData {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/method[@name='getTimeToWaitForMoreData' and count(parameter)=0]"
			[Register ("getTimeToWaitForMoreData", "()I", "GetGetTimeToWaitForMoreDataHandler")]
			get {
				const string __id = "getTimeToWaitForMoreData.()I";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualInt32Method (__id, this, null);
					return __rm;
				} finally {
				}
			}
		}

		static Delegate cb_bytesAvailable;
#pragma warning disable 0169
		static Delegate GetBytesAvailableHandler ()
		{
			if (cb_bytesAvailable == null)
				cb_bytesAvailable = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int>) n_BytesAvailable);
			return cb_bytesAvailable;
		}

		static int n_BytesAvailable (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.BytesAvailable ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/method[@name='bytesAvailable' and count(parameter)=0]"
		[Register ("bytesAvailable", "()I", "GetBytesAvailableHandler")]
		public virtual unsafe int BytesAvailable ()
		{
			const string __id = "bytesAvailable.()I";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualInt32Method (__id, this, null);
				return __rm;
			} finally {
			}
		}

		static Delegate cb_close;
#pragma warning disable 0169
		static Delegate GetCloseHandler ()
		{
			if (cb_close == null)
				cb_close = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Close);
			return cb_close;
		}

		static void n_Close (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Close ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/method[@name='close' and count(parameter)=0]"
		[Register ("close", "()V", "GetCloseHandler")]
		public virtual unsafe void Close ()
		{
			const string __id = "close.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_open;
#pragma warning disable 0169
		static Delegate GetOpenHandler ()
		{
			if (cb_open == null)
				cb_open = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Open);
			return cb_open;
		}

		static void n_Open (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Open ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/method[@name='open' and count(parameter)=0]"
		[Register ("open", "()V", "GetOpenHandler")]
		public virtual unsafe void Open ()
		{
			const string __id = "open.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_read;
#pragma warning disable 0169
		static Delegate GetReadHandler ()
		{
			if (cb_read == null)
				cb_read = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_Read);
			return cb_read;
		}

		static IntPtr n_Read (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.Read ());
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/method[@name='read' and count(parameter)=0]"
		[Register ("read", "()[B", "GetReadHandler")]
		public virtual unsafe byte[] Read ()
		{
			const string __id = "read.()[B";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
				return (byte[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (byte));
			} finally {
			}
		}

		static Delegate cb_waitForData_I;
#pragma warning disable 0169
		static Delegate GetWaitForData_IHandler ()
		{
			if (cb_waitForData_I == null)
				cb_waitForData_I = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, int>) n_WaitForData_I);
			return cb_waitForData_I;
		}

		static void n_WaitForData_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.WaitForData (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/method[@name='waitForData' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("waitForData", "(I)V", "GetWaitForData_IHandler")]
		public virtual unsafe void WaitForData (int p0)
		{
			const string __id = "waitForData.(I)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
		}

		static Delegate cb_write_arrayB;
#pragma warning disable 0169
		static Delegate GetWrite_arrayBHandler ()
		{
			if (cb_write_arrayB == null)
				cb_write_arrayB = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_Write_arrayB);
			return cb_write_arrayB;
		}

		static void n_Write_arrayB (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			byte[] p0 = (byte[]) JNIEnv.GetArray (native_p0, JniHandleOwnership.DoNotTransfer, typeof (byte));
			__this.Write (p0);
			if (p0 != null)
				JNIEnv.CopyArray (p0, native_p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/method[@name='write' and count(parameter)=1 and parameter[1][@type='byte[]']]"
		[Register ("write", "([B)V", "GetWrite_arrayBHandler")]
		public virtual unsafe void Write (byte[] p0)
		{
			const string __id = "write.([B)V";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

		static Delegate cb_write_arrayBII;
#pragma warning disable 0169
		static Delegate GetWrite_arrayBIIHandler ()
		{
			if (cb_write_arrayBII == null)
				cb_write_arrayBII = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, int, int>) n_Write_arrayBII);
			return cb_write_arrayBII;
		}

		static void n_Write_arrayBII (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int p1, int p2)
		{
			global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.ZebraPrinterConnectionA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			byte[] p0 = (byte[]) JNIEnv.GetArray (native_p0, JniHandleOwnership.DoNotTransfer, typeof (byte));
			__this.Write (p0, p1, p2);
			if (p0 != null)
				JNIEnv.CopyArray (p0, native_p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/class[@name='ZebraPrinterConnectionA']/method[@name='write' and count(parameter)=3 and parameter[1][@type='byte[]'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
		[Register ("write", "([BII)V", "GetWrite_arrayBIIHandler")]
		public virtual unsafe void Write (byte[] p0, int p1, int p2)
		{
			const string __id = "write.([BII)V";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (p2);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

	}

	[global::Android.Runtime.Register ("com/zebra/android/comm/ZebraPrinterConnectionA", DoNotGenerateAcw=true)]
	internal partial class ZebraPrinterConnectionAInvoker : ZebraPrinterConnectionA {

		public ZebraPrinterConnectionAInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer) {}

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/comm/ZebraPrinterConnectionA", typeof (ZebraPrinterConnectionAInvoker));

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

	}

}
