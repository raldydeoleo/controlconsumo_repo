using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatusMessages']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/PrinterStatusMessages", DoNotGenerateAcw=true)]
	public partial class PrinterStatusMessages : global::Java.Lang.Object {


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatusMessages']/field[@name='HEAD_OPEN_MSG']"
		[Register ("HEAD_OPEN_MSG")]
		public const string HeadOpenMsg = (string) "HEAD OPEN";

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatusMessages']/field[@name='HEAD_TOO_HOT_MSG']"
		[Register ("HEAD_TOO_HOT_MSG")]
		public const string HeadTooHotMsg = (string) "HEAD TOO HOT";

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatusMessages']/field[@name='NULL_MSG']"
		[Register ("NULL_MSG")]
		public const string NullMsg = (string) "INVALID STATUS";

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatusMessages']/field[@name='PAPER_OUT_MSG']"
		[Register ("PAPER_OUT_MSG")]
		public const string PaperOutMsg = (string) "PAPER OUT";

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatusMessages']/field[@name='PAUSE_MSG']"
		[Register ("PAUSE_MSG")]
		public const string PauseMsg = (string) "PAUSE";

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatusMessages']/field[@name='RECEIVE_BUFFER_FULL_MSG']"
		[Register ("RECEIVE_BUFFER_FULL_MSG")]
		public const string ReceiveBufferFullMsg = (string) "RECEIVE BUFFER FULL";

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatusMessages']/field[@name='RIBBON_OUT_MSG']"
		[Register ("RIBBON_OUT_MSG")]
		public const string RibbonOutMsg = (string) "RIBBON OUT";
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/PrinterStatusMessages", typeof (PrinterStatusMessages));
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

		protected PrinterStatusMessages (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatusMessages']/constructor[@name='PrinterStatusMessages' and count(parameter)=1 and parameter[1][@type='com.zebra.android.printer.PrinterStatus']]"
		[Register (".ctor", "(Lcom/zebra/android/printer/PrinterStatus;)V", "")]
		public unsafe PrinterStatusMessages (global::Com.Zebra.Android.Printer.PrinterStatus p0)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Lcom/zebra/android/printer/PrinterStatus;)V";

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

		static Delegate cb_getStatusMessage;
#pragma warning disable 0169
		static Delegate GetGetStatusMessageHandler ()
		{
			if (cb_getStatusMessage == null)
				cb_getStatusMessage = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetStatusMessage);
			return cb_getStatusMessage;
		}

		static IntPtr n_GetStatusMessage (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.PrinterStatusMessages __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.PrinterStatusMessages> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.GetStatusMessage ());
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatusMessages']/method[@name='getStatusMessage' and count(parameter)=0]"
		[Register ("getStatusMessage", "()[Ljava/lang/String;", "GetGetStatusMessageHandler")]
		public virtual unsafe string[] GetStatusMessage ()
		{
			const string __id = "getStatusMessage.()[Ljava/lang/String;";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
				return (string[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (string));
			} finally {
			}
		}

	}
}
