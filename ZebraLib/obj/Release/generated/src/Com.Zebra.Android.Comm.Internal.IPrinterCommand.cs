using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Comm.Internal {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.comm.internal']/interface[@name='PrinterCommand']"
	[Register ("com/zebra/android/comm/internal/PrinterCommand", "", "Com.Zebra.Android.Comm.Internal.IPrinterCommandInvoker")]
	public partial interface IPrinterCommand : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm.internal']/interface[@name='PrinterCommand']/method[@name='sendAndWaitForResponse' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register ("sendAndWaitForResponse", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)[B", "GetSendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_Handler:Com.Zebra.Android.Comm.Internal.IPrinterCommandInvoker, ZebraLib")]
		byte[] SendAndWaitForResponse (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm.internal']/interface[@name='PrinterCommand']/method[@name='sendAndWaitForResponse' and count(parameter)=3 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
		[Register ("sendAndWaitForResponse", "(Lcom/zebra/android/comm/ZebraPrinterConnection;II)[B", "GetSendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_IIHandler:Com.Zebra.Android.Comm.Internal.IPrinterCommandInvoker, ZebraLib")]
		byte[] SendAndWaitForResponse (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0, int p1, int p2);

	}

	[global::Android.Runtime.Register ("com/zebra/android/comm/internal/PrinterCommand", DoNotGenerateAcw=true)]
	internal class IPrinterCommandInvoker : global::Java.Lang.Object, IPrinterCommand {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/comm/internal/PrinterCommand", typeof (IPrinterCommandInvoker));

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

		public static IPrinterCommand GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IPrinterCommand> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.comm.internal.PrinterCommand"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IPrinterCommandInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_;
#pragma warning disable 0169
		static Delegate GetSendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_Handler ()
		{
			if (cb_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_ == null)
				cb_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr>) n_SendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_);
			return cb_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_;
		}

		static IntPtr n_SendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Comm.Internal.IPrinterCommand __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.IPrinterCommand> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0 = (global::Com.Zebra.Android.Comm.IZebraPrinterConnection)global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.SendAndWaitForResponse (p0));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_;
		public unsafe byte[] SendAndWaitForResponse (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
		{
			if (id_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_ == IntPtr.Zero)
				id_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_ = JNIEnv.GetMethodID (class_ref, "sendAndWaitForResponse", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)[B");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			byte[] __ret = (byte[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_, __args), JniHandleOwnership.TransferLocalRef, typeof (byte));
			return __ret;
		}

		static Delegate cb_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_II;
#pragma warning disable 0169
		static Delegate GetSendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_IIHandler ()
		{
			if (cb_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_II == null)
				cb_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_II = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, int, int, IntPtr>) n_SendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_II);
			return cb_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_II;
		}

		static IntPtr n_SendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_II (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int p1, int p2)
		{
			global::Com.Zebra.Android.Comm.Internal.IPrinterCommand __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.IPrinterCommand> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0 = (global::Com.Zebra.Android.Comm.IZebraPrinterConnection)global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.SendAndWaitForResponse (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_II;
		public unsafe byte[] SendAndWaitForResponse (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0, int p1, int p2)
		{
			if (id_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_II == IntPtr.Zero)
				id_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_II = JNIEnv.GetMethodID (class_ref, "sendAndWaitForResponse", "(Lcom/zebra/android/comm/ZebraPrinterConnection;II)[B");
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue (p1);
			__args [2] = new JValue (p2);
			byte[] __ret = (byte[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_sendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_II, __args), JniHandleOwnership.TransferLocalRef, typeof (byte));
			return __ret;
		}

	}

}
