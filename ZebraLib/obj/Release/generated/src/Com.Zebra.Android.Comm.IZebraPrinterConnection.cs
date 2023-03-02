using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Comm {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']"
	[Register ("com/zebra/android/comm/ZebraPrinterConnection", "", "Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker")]
	public partial interface IZebraPrinterConnection : IJavaObject {

		bool IsConnected {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='isConnected' and count(parameter)=0]"
			[Register ("isConnected", "()Z", "GetIsConnectedHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")] get;
		}

		int MaxTimeoutForRead {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='getMaxTimeoutForRead' and count(parameter)=0]"
			[Register ("getMaxTimeoutForRead", "()I", "GetGetMaxTimeoutForReadHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")] get;
		}

		int TimeToWaitForMoreData {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='getTimeToWaitForMoreData' and count(parameter)=0]"
			[Register ("getTimeToWaitForMoreData", "()I", "GetGetTimeToWaitForMoreDataHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")] get;
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='bytesAvailable' and count(parameter)=0]"
		[Register ("bytesAvailable", "()I", "GetBytesAvailableHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")]
		int BytesAvailable ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='close' and count(parameter)=0]"
		[Register ("close", "()V", "GetCloseHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")]
		void Close ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='open' and count(parameter)=0]"
		[Register ("open", "()V", "GetOpenHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")]
		void Open ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='read' and count(parameter)=0]"
		[Register ("read", "()[B", "GetReadHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")]
		byte[] Read ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='toString' and count(parameter)=0]"
		[Register ("toString", "()Ljava/lang/String;", "GetToStringHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")]
		string ToString ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='waitForData' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("waitForData", "(I)V", "GetWaitForData_IHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")]
		void WaitForData (int p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='write' and count(parameter)=1 and parameter[1][@type='byte[]']]"
		[Register ("write", "([B)V", "GetWrite_arrayBHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")]
		void Write (byte[] p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm']/interface[@name='ZebraPrinterConnection']/method[@name='write' and count(parameter)=3 and parameter[1][@type='byte[]'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
		[Register ("write", "([BII)V", "GetWrite_arrayBIIHandler:Com.Zebra.Android.Comm.IZebraPrinterConnectionInvoker, ZebraLib")]
		void Write (byte[] p0, int p1, int p2);

	}

	[global::Android.Runtime.Register ("com/zebra/android/comm/ZebraPrinterConnection", DoNotGenerateAcw=true)]
	internal class IZebraPrinterConnectionInvoker : global::Java.Lang.Object, IZebraPrinterConnection {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/comm/ZebraPrinterConnection", typeof (IZebraPrinterConnectionInvoker));

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

		public static IZebraPrinterConnection GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IZebraPrinterConnection> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.comm.ZebraPrinterConnection"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IZebraPrinterConnectionInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
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
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.IsConnected;
		}
#pragma warning restore 0169

		IntPtr id_isConnected;
		public unsafe bool IsConnected {
			get {
				if (id_isConnected == IntPtr.Zero)
					id_isConnected = JNIEnv.GetMethodID (class_ref, "isConnected", "()Z");
				return JNIEnv.CallBooleanMethod (((global::Java.Lang.Object) this).Handle, id_isConnected);
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
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.MaxTimeoutForRead;
		}
#pragma warning restore 0169

		IntPtr id_getMaxTimeoutForRead;
		public unsafe int MaxTimeoutForRead {
			get {
				if (id_getMaxTimeoutForRead == IntPtr.Zero)
					id_getMaxTimeoutForRead = JNIEnv.GetMethodID (class_ref, "getMaxTimeoutForRead", "()I");
				return JNIEnv.CallIntMethod (((global::Java.Lang.Object) this).Handle, id_getMaxTimeoutForRead);
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
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.TimeToWaitForMoreData;
		}
#pragma warning restore 0169

		IntPtr id_getTimeToWaitForMoreData;
		public unsafe int TimeToWaitForMoreData {
			get {
				if (id_getTimeToWaitForMoreData == IntPtr.Zero)
					id_getTimeToWaitForMoreData = JNIEnv.GetMethodID (class_ref, "getTimeToWaitForMoreData", "()I");
				return JNIEnv.CallIntMethod (((global::Java.Lang.Object) this).Handle, id_getTimeToWaitForMoreData);
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
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.BytesAvailable ();
		}
#pragma warning restore 0169

		IntPtr id_bytesAvailable;
		public unsafe int BytesAvailable ()
		{
			if (id_bytesAvailable == IntPtr.Zero)
				id_bytesAvailable = JNIEnv.GetMethodID (class_ref, "bytesAvailable", "()I");
			return JNIEnv.CallIntMethod (((global::Java.Lang.Object) this).Handle, id_bytesAvailable);
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
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Close ();
		}
#pragma warning restore 0169

		IntPtr id_close;
		public unsafe void Close ()
		{
			if (id_close == IntPtr.Zero)
				id_close = JNIEnv.GetMethodID (class_ref, "close", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_close);
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
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Open ();
		}
#pragma warning restore 0169

		IntPtr id_open;
		public unsafe void Open ()
		{
			if (id_open == IntPtr.Zero)
				id_open = JNIEnv.GetMethodID (class_ref, "open", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_open);
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
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.Read ());
		}
#pragma warning restore 0169

		IntPtr id_read;
		public unsafe byte[] Read ()
		{
			if (id_read == IntPtr.Zero)
				id_read = JNIEnv.GetMethodID (class_ref, "read", "()[B");
			return (byte[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_read), JniHandleOwnership.TransferLocalRef, typeof (byte));
		}

		static Delegate cb_toString;
#pragma warning disable 0169
		static Delegate GetToStringHandler ()
		{
			if (cb_toString == null)
				cb_toString = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_ToString);
			return cb_toString;
		}

		static IntPtr n_ToString (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.ToString ());
		}
#pragma warning restore 0169

		IntPtr id_toString;
		public unsafe string ToString ()
		{
			if (id_toString == IntPtr.Zero)
				id_toString = JNIEnv.GetMethodID (class_ref, "toString", "()Ljava/lang/String;");
			return JNIEnv.GetString (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_toString), JniHandleOwnership.TransferLocalRef);
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
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.WaitForData (p0);
		}
#pragma warning restore 0169

		IntPtr id_waitForData_I;
		public unsafe void WaitForData (int p0)
		{
			if (id_waitForData_I == IntPtr.Zero)
				id_waitForData_I = JNIEnv.GetMethodID (class_ref, "waitForData", "(I)V");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_waitForData_I, __args);
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
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			byte[] p0 = (byte[]) JNIEnv.GetArray (native_p0, JniHandleOwnership.DoNotTransfer, typeof (byte));
			__this.Write (p0);
			if (p0 != null)
				JNIEnv.CopyArray (p0, native_p0);
		}
#pragma warning restore 0169

		IntPtr id_write_arrayB;
		public unsafe void Write (byte[] p0)
		{
			if (id_write_arrayB == IntPtr.Zero)
				id_write_arrayB = JNIEnv.GetMethodID (class_ref, "write", "([B)V");
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_write_arrayB, __args);
			if (p0 != null) {
				JNIEnv.CopyArray (native_p0, p0);
				JNIEnv.DeleteLocalRef (native_p0);
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
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			byte[] p0 = (byte[]) JNIEnv.GetArray (native_p0, JniHandleOwnership.DoNotTransfer, typeof (byte));
			__this.Write (p0, p1, p2);
			if (p0 != null)
				JNIEnv.CopyArray (p0, native_p0);
		}
#pragma warning restore 0169

		IntPtr id_write_arrayBII;
		public unsafe void Write (byte[] p0, int p1, int p2)
		{
			if (id_write_arrayBII == IntPtr.Zero)
				id_write_arrayBII = JNIEnv.GetMethodID (class_ref, "write", "([BII)V");
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue (native_p0);
			__args [1] = new JValue (p1);
			__args [2] = new JValue (p2);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_write_arrayBII, __args);
			if (p0 != null) {
				JNIEnv.CopyArray (native_p0, p0);
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}

}
