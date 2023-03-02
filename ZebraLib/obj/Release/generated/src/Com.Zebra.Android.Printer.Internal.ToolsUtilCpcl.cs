using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ToolsUtilCpcl']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/ToolsUtilCpcl", DoNotGenerateAcw=true)]
	public partial class ToolsUtilCpcl : global::Java.Lang.Object, global::Com.Zebra.Android.Printer.IToolsUtil {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ToolsUtilCpcl']/field[@name='connection']"
		[Register ("connection")]
		protected global::Com.Zebra.Android.Comm.IZebraPrinterConnection Connection {
			get {
				const string __id = "connection.Lcom/zebra/android/comm/ZebraPrinterConnection;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "connection.Lcom/zebra/android/comm/ZebraPrinterConnection;";

				IntPtr native_value = global::Android.Runtime.JNIEnv.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					global::Android.Runtime.JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/ToolsUtilCpcl", typeof (ToolsUtilCpcl));
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

		protected ToolsUtilCpcl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ToolsUtilCpcl']/constructor[@name='ToolsUtilCpcl' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe ToolsUtilCpcl (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
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

		static Delegate cb_calibrate;
#pragma warning disable 0169
		static Delegate GetCalibrateHandler ()
		{
			if (cb_calibrate == null)
				cb_calibrate = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Calibrate);
			return cb_calibrate;
		}

		static void n_Calibrate (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.ToolsUtilCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ToolsUtilCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Calibrate ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ToolsUtilCpcl']/method[@name='calibrate' and count(parameter)=0]"
		[Register ("calibrate", "()V", "GetCalibrateHandler")]
		public virtual unsafe void Calibrate ()
		{
			const string __id = "calibrate.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_printConfigurationLabel;
#pragma warning disable 0169
		static Delegate GetPrintConfigurationLabelHandler ()
		{
			if (cb_printConfigurationLabel == null)
				cb_printConfigurationLabel = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_PrintConfigurationLabel);
			return cb_printConfigurationLabel;
		}

		static void n_PrintConfigurationLabel (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.ToolsUtilCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ToolsUtilCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.PrintConfigurationLabel ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ToolsUtilCpcl']/method[@name='printConfigurationLabel' and count(parameter)=0]"
		[Register ("printConfigurationLabel", "()V", "GetPrintConfigurationLabelHandler")]
		public virtual unsafe void PrintConfigurationLabel ()
		{
			const string __id = "printConfigurationLabel.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_reset;
#pragma warning disable 0169
		static Delegate GetResetHandler ()
		{
			if (cb_reset == null)
				cb_reset = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Reset);
			return cb_reset;
		}

		static void n_Reset (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.ToolsUtilCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ToolsUtilCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Reset ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ToolsUtilCpcl']/method[@name='reset' and count(parameter)=0]"
		[Register ("reset", "()V", "GetResetHandler")]
		public virtual unsafe void Reset ()
		{
			const string __id = "reset.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_restoreDefaults;
#pragma warning disable 0169
		static Delegate GetRestoreDefaultsHandler ()
		{
			if (cb_restoreDefaults == null)
				cb_restoreDefaults = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_RestoreDefaults);
			return cb_restoreDefaults;
		}

		static void n_RestoreDefaults (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.ToolsUtilCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ToolsUtilCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.RestoreDefaults ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ToolsUtilCpcl']/method[@name='restoreDefaults' and count(parameter)=0]"
		[Register ("restoreDefaults", "()V", "GetRestoreDefaultsHandler")]
		public virtual unsafe void RestoreDefaults ()
		{
			const string __id = "restoreDefaults.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_sendCommand_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetSendCommand_Ljava_lang_String_Handler ()
		{
			if (cb_sendCommand_Ljava_lang_String_ == null)
				cb_sendCommand_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SendCommand_Ljava_lang_String_);
			return cb_sendCommand_Ljava_lang_String_;
		}

		static void n_SendCommand_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.Internal.ToolsUtilCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ToolsUtilCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SendCommand (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ToolsUtilCpcl']/method[@name='sendCommand' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("sendCommand", "(Ljava/lang/String;)V", "GetSendCommand_Ljava_lang_String_Handler")]
		public virtual unsafe void SendCommand (string p0)
		{
			const string __id = "sendCommand.(Ljava/lang/String;)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}
}
