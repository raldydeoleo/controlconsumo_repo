using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ToolsUtil']"
	[Register ("com/zebra/android/printer/ToolsUtil", "", "Com.Zebra.Android.Printer.IToolsUtilInvoker")]
	public partial interface IToolsUtil : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ToolsUtil']/method[@name='calibrate' and count(parameter)=0]"
		[Register ("calibrate", "()V", "GetCalibrateHandler:Com.Zebra.Android.Printer.IToolsUtilInvoker, ZebraLib")]
		void Calibrate ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ToolsUtil']/method[@name='printConfigurationLabel' and count(parameter)=0]"
		[Register ("printConfigurationLabel", "()V", "GetPrintConfigurationLabelHandler:Com.Zebra.Android.Printer.IToolsUtilInvoker, ZebraLib")]
		void PrintConfigurationLabel ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ToolsUtil']/method[@name='reset' and count(parameter)=0]"
		[Register ("reset", "()V", "GetResetHandler:Com.Zebra.Android.Printer.IToolsUtilInvoker, ZebraLib")]
		void Reset ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ToolsUtil']/method[@name='restoreDefaults' and count(parameter)=0]"
		[Register ("restoreDefaults", "()V", "GetRestoreDefaultsHandler:Com.Zebra.Android.Printer.IToolsUtilInvoker, ZebraLib")]
		void RestoreDefaults ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ToolsUtil']/method[@name='sendCommand' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("sendCommand", "(Ljava/lang/String;)V", "GetSendCommand_Ljava_lang_String_Handler:Com.Zebra.Android.Printer.IToolsUtilInvoker, ZebraLib")]
		void SendCommand (string p0);

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/ToolsUtil", DoNotGenerateAcw=true)]
	internal class IToolsUtilInvoker : global::Java.Lang.Object, IToolsUtil {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/ToolsUtil", typeof (IToolsUtilInvoker));

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

		public static IToolsUtil GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IToolsUtil> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.printer.ToolsUtil"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IToolsUtilInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
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
			global::Com.Zebra.Android.Printer.IToolsUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IToolsUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Calibrate ();
		}
#pragma warning restore 0169

		IntPtr id_calibrate;
		public unsafe void Calibrate ()
		{
			if (id_calibrate == IntPtr.Zero)
				id_calibrate = JNIEnv.GetMethodID (class_ref, "calibrate", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_calibrate);
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
			global::Com.Zebra.Android.Printer.IToolsUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IToolsUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.PrintConfigurationLabel ();
		}
#pragma warning restore 0169

		IntPtr id_printConfigurationLabel;
		public unsafe void PrintConfigurationLabel ()
		{
			if (id_printConfigurationLabel == IntPtr.Zero)
				id_printConfigurationLabel = JNIEnv.GetMethodID (class_ref, "printConfigurationLabel", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_printConfigurationLabel);
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
			global::Com.Zebra.Android.Printer.IToolsUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IToolsUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Reset ();
		}
#pragma warning restore 0169

		IntPtr id_reset;
		public unsafe void Reset ()
		{
			if (id_reset == IntPtr.Zero)
				id_reset = JNIEnv.GetMethodID (class_ref, "reset", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_reset);
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
			global::Com.Zebra.Android.Printer.IToolsUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IToolsUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.RestoreDefaults ();
		}
#pragma warning restore 0169

		IntPtr id_restoreDefaults;
		public unsafe void RestoreDefaults ()
		{
			if (id_restoreDefaults == IntPtr.Zero)
				id_restoreDefaults = JNIEnv.GetMethodID (class_ref, "restoreDefaults", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_restoreDefaults);
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
			global::Com.Zebra.Android.Printer.IToolsUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IToolsUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SendCommand (p0);
		}
#pragma warning restore 0169

		IntPtr id_sendCommand_Ljava_lang_String_;
		public unsafe void SendCommand (string p0)
		{
			if (id_sendCommand_Ljava_lang_String_ == IntPtr.Zero)
				id_sendCommand_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "sendCommand", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_sendCommand_Ljava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
		}

	}

}
