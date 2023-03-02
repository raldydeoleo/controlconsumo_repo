using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Comm.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.comm.internal']/class[@name='PrinterCommandImpl']"
	[global::Android.Runtime.Register ("com/zebra/android/comm/internal/PrinterCommandImpl", DoNotGenerateAcw=true)]
	public partial class PrinterCommandImpl : global::Java.Lang.Object, global::Com.Zebra.Android.Comm.Internal.IPrinterCommand {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/comm/internal/PrinterCommandImpl", typeof (PrinterCommandImpl));
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

		protected PrinterCommandImpl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.comm.internal']/class[@name='PrinterCommandImpl']/constructor[@name='PrinterCommandImpl' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register (".ctor", "(Ljava/lang/String;)V", "")]
		public unsafe PrinterCommandImpl (string p0)
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
			global::Com.Zebra.Android.Comm.Internal.PrinterCommandImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.PrinterCommandImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0 = (global::Com.Zebra.Android.Comm.IZebraPrinterConnection)global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.SendAndWaitForResponse (p0));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm.internal']/class[@name='PrinterCommandImpl']/method[@name='sendAndWaitForResponse' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register ("sendAndWaitForResponse", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)[B", "GetSendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_Handler")]
		public virtual unsafe byte[] SendAndWaitForResponse (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
		{
			const string __id = "sendAndWaitForResponse.(Lcom/zebra/android/comm/ZebraPrinterConnection;)[B";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return (byte[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (byte));
			} finally {
			}
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
			global::Com.Zebra.Android.Comm.Internal.PrinterCommandImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.PrinterCommandImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0 = (global::Com.Zebra.Android.Comm.IZebraPrinterConnection)global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.SendAndWaitForResponse (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm.internal']/class[@name='PrinterCommandImpl']/method[@name='sendAndWaitForResponse' and count(parameter)=3 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
		[Register ("sendAndWaitForResponse", "(Lcom/zebra/android/comm/ZebraPrinterConnection;II)[B", "GetSendAndWaitForResponse_Lcom_zebra_android_comm_ZebraPrinterConnection_IIHandler")]
		public virtual unsafe byte[] SendAndWaitForResponse (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0, int p1, int p2)
		{
			const string __id = "sendAndWaitForResponse.(Lcom/zebra/android/comm/ZebraPrinterConnection;II)[B";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (p2);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return (byte[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (byte));
			} finally {
			}
		}

	}
}
