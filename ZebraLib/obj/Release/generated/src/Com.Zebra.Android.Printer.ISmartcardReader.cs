using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='SmartcardReader']"
	[Register ("com/zebra/android/printer/SmartcardReader", "", "Com.Zebra.Android.Printer.ISmartcardReaderInvoker")]
	public partial interface ISmartcardReader : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='SmartcardReader']/method[@name='close' and count(parameter)=0]"
		[Register ("close", "()V", "GetCloseHandler:Com.Zebra.Android.Printer.ISmartcardReaderInvoker, ZebraLib")]
		void Close ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='SmartcardReader']/method[@name='doCommand' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("doCommand", "(Ljava/lang/String;)[B", "GetDoCommand_Ljava_lang_String_Handler:Com.Zebra.Android.Printer.ISmartcardReaderInvoker, ZebraLib")]
		byte[] DoCommand (string p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='SmartcardReader']/method[@name='getATR' and count(parameter)=0]"
		[Register ("getATR", "()[B", "GetGetATRHandler:Com.Zebra.Android.Printer.ISmartcardReaderInvoker, ZebraLib")]
		byte[] GetATR ();

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/SmartcardReader", DoNotGenerateAcw=true)]
	internal class ISmartcardReaderInvoker : global::Java.Lang.Object, ISmartcardReader {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/SmartcardReader", typeof (ISmartcardReaderInvoker));

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

		public static ISmartcardReader GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<ISmartcardReader> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.printer.SmartcardReader"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public ISmartcardReaderInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
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
			global::Com.Zebra.Android.Printer.ISmartcardReader __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.ISmartcardReader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
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
			global::Com.Zebra.Android.Printer.ISmartcardReader __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.ISmartcardReader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.DoCommand (p0));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_doCommand_Ljava_lang_String_;
		public unsafe byte[] DoCommand (string p0)
		{
			if (id_doCommand_Ljava_lang_String_ == IntPtr.Zero)
				id_doCommand_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "doCommand", "(Ljava/lang/String;)[B");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			byte[] __ret = (byte[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_doCommand_Ljava_lang_String_, __args), JniHandleOwnership.TransferLocalRef, typeof (byte));
			JNIEnv.DeleteLocalRef (native_p0);
			return __ret;
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
			global::Com.Zebra.Android.Printer.ISmartcardReader __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.ISmartcardReader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.GetATR ());
		}
#pragma warning restore 0169

		IntPtr id_getATR;
		public unsafe byte[] GetATR ()
		{
			if (id_getATR == IntPtr.Zero)
				id_getATR = JNIEnv.GetMethodID (class_ref, "getATR", "()[B");
			return (byte[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getATR), JniHandleOwnership.TransferLocalRef, typeof (byte));
		}

	}

}
