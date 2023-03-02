using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterStatusZpl']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/PrinterStatusZpl", DoNotGenerateAcw=true)]
	public partial class PrinterStatusZpl : global::Com.Zebra.Android.Printer.PrinterStatus {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/PrinterStatusZpl", typeof (PrinterStatusZpl));
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

		protected PrinterStatusZpl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterStatusZpl']/constructor[@name='PrinterStatusZpl' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe PrinterStatusZpl (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
			}
		}

		static Delegate cb_updateStatus;
#pragma warning disable 0169
		static Delegate GetUpdateStatusHandler ()
		{
			if (cb_updateStatus == null)
				cb_updateStatus = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_UpdateStatus);
			return cb_updateStatus;
		}

		static void n_UpdateStatus (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.PrinterStatusZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterStatusZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.UpdateStatus ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterStatusZpl']/method[@name='updateStatus' and count(parameter)=0]"
		[Register ("updateStatus", "()V", "GetUpdateStatusHandler")]
		protected override unsafe void UpdateStatus ()
		{
			const string __id = "updateStatus.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

	}
}
