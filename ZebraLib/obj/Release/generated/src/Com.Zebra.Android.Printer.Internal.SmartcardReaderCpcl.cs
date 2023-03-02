using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='SmartcardReaderCpcl']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/SmartcardReaderCpcl", DoNotGenerateAcw=true)]
	public partial class SmartcardReaderCpcl : global::Java.Lang.Object, global::Com.Zebra.Android.Printer.ISmartcardReader {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='SmartcardReaderCpcl']/field[@name='printerConnection']"
		[Register ("printerConnection")]
		protected global::Com.Zebra.Android.Comm.IZebraPrinterConnection PrinterConnection {
			get {
				const string __id = "printerConnection.Lcom/zebra/android/comm/ZebraPrinterConnection;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "printerConnection.Lcom/zebra/android/comm/ZebraPrinterConnection;";

				IntPtr native_value = global::Android.Runtime.JNIEnv.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					global::Android.Runtime.JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/SmartcardReaderCpcl", typeof (SmartcardReaderCpcl));
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

		protected SmartcardReaderCpcl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='SmartcardReaderCpcl']/constructor[@name='SmartcardReaderCpcl' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe SmartcardReaderCpcl (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
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
			global::Com.Zebra.Android.Printer.Internal.SmartcardReaderCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.SmartcardReaderCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Close ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='SmartcardReaderCpcl']/method[@name='close' and count(parameter)=0]"
		[Register ("close", "()V", "GetCloseHandler")]
		public virtual unsafe void Close ()
		{
			const string __id = "close.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_doCommand_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetDoCommand_Ljava_lang_String_Handler ()
		{
			if (cb_doCommand_Ljava_lang_String_ == null)
				cb_doCommand_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr>) n_DoCommand_Ljava_lang_String_);
			return cb_doCommand_Ljava_lang_String_;
		}

		static IntPtr n_DoCommand_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.Internal.SmartcardReaderCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.SmartcardReaderCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.DoCommand (p0));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='SmartcardReaderCpcl']/method[@name='doCommand' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("doCommand", "(Ljava/lang/String;)[B", "GetDoCommand_Ljava_lang_String_Handler")]
		public virtual unsafe byte[] DoCommand (string p0)
		{
			const string __id = "doCommand.(Ljava/lang/String;)[B";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return (byte[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (byte));
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static Delegate cb_getATR;
#pragma warning disable 0169
		static Delegate GetGetATRHandler ()
		{
			if (cb_getATR == null)
				cb_getATR = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetATR);
			return cb_getATR;
		}

		static IntPtr n_GetATR (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.SmartcardReaderCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.SmartcardReaderCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.GetATR ());
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='SmartcardReaderCpcl']/method[@name='getATR' and count(parameter)=0]"
		[Register ("getATR", "()[B", "GetGetATRHandler")]
		public virtual unsafe byte[] GetATR ()
		{
			const string __id = "getATR.()[B";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
				return (byte[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (byte));
			} finally {
			}
		}

	}
}
