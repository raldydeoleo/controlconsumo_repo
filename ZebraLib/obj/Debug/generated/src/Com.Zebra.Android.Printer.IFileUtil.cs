using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FileUtil']"
	[Register ("com/zebra/android/printer/FileUtil", "", "Com.Zebra.Android.Printer.IFileUtilInvoker")]
	public partial interface IFileUtil : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FileUtil']/method[@name='retrieveFileNames' and count(parameter)=0]"
		[Register ("retrieveFileNames", "()[Ljava/lang/String;", "GetRetrieveFileNamesHandler:Com.Zebra.Android.Printer.IFileUtilInvoker, ZebraLib")]
		string[] RetrieveFileNames ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FileUtil']/method[@name='retrieveFileNames' and count(parameter)=1 and parameter[1][@type='java.lang.String[]']]"
		[Register ("retrieveFileNames", "([Ljava/lang/String;)[Ljava/lang/String;", "GetRetrieveFileNames_arrayLjava_lang_String_Handler:Com.Zebra.Android.Printer.IFileUtilInvoker, ZebraLib")]
		string[] RetrieveFileNames (string[] p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FileUtil']/method[@name='sendFileContents' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("sendFileContents", "(Ljava/lang/String;)V", "GetSendFileContents_Ljava_lang_String_Handler:Com.Zebra.Android.Printer.IFileUtilInvoker, ZebraLib")]
		void SendFileContents (string p0);

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/FileUtil", DoNotGenerateAcw=true)]
	internal class IFileUtilInvoker : global::Java.Lang.Object, IFileUtil {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/FileUtil", typeof (IFileUtilInvoker));

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

		public static IFileUtil GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IFileUtil> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.printer.FileUtil"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IFileUtilInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_retrieveFileNames;
#pragma warning disable 0169
		static Delegate GetRetrieveFileNamesHandler ()
		{
			if (cb_retrieveFileNames == null)
				cb_retrieveFileNames = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_RetrieveFileNames);
			return cb_retrieveFileNames;
		}

		static IntPtr n_RetrieveFileNames (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.IFileUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFileUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.RetrieveFileNames ());
		}
#pragma warning restore 0169

		IntPtr id_retrieveFileNames;
		public unsafe string[] RetrieveFileNames ()
		{
			if (id_retrieveFileNames == IntPtr.Zero)
				id_retrieveFileNames = JNIEnv.GetMethodID (class_ref, "retrieveFileNames", "()[Ljava/lang/String;");
			return (string[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_retrieveFileNames), JniHandleOwnership.TransferLocalRef, typeof (string));
		}

		static Delegate cb_retrieveFileNames_arrayLjava_lang_String_;
#pragma warning disable 0169
		static Delegate GetRetrieveFileNames_arrayLjava_lang_String_Handler ()
		{
			if (cb_retrieveFileNames_arrayLjava_lang_String_ == null)
				cb_retrieveFileNames_arrayLjava_lang_String_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr>) n_RetrieveFileNames_arrayLjava_lang_String_);
			return cb_retrieveFileNames_arrayLjava_lang_String_;
		}

		static IntPtr n_RetrieveFileNames_arrayLjava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.IFileUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFileUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string[] p0 = (string[]) JNIEnv.GetArray (native_p0, JniHandleOwnership.DoNotTransfer, typeof (string));
			IntPtr __ret = JNIEnv.NewArray (__this.RetrieveFileNames (p0));
			if (p0 != null)
				JNIEnv.CopyArray (p0, native_p0);
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_retrieveFileNames_arrayLjava_lang_String_;
		public unsafe string[] RetrieveFileNames (string[] p0)
		{
			if (id_retrieveFileNames_arrayLjava_lang_String_ == IntPtr.Zero)
				id_retrieveFileNames_arrayLjava_lang_String_ = JNIEnv.GetMethodID (class_ref, "retrieveFileNames", "([Ljava/lang/String;)[Ljava/lang/String;");
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			string[] __ret = (string[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_retrieveFileNames_arrayLjava_lang_String_, __args), JniHandleOwnership.TransferLocalRef, typeof (string));
			if (p0 != null) {
				JNIEnv.CopyArray (native_p0, p0);
				JNIEnv.DeleteLocalRef (native_p0);
			}
			return __ret;
		}

		static Delegate cb_sendFileContents_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetSendFileContents_Ljava_lang_String_Handler ()
		{
			if (cb_sendFileContents_Ljava_lang_String_ == null)
				cb_sendFileContents_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SendFileContents_Ljava_lang_String_);
			return cb_sendFileContents_Ljava_lang_String_;
		}

		static void n_SendFileContents_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.IFileUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFileUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SendFileContents (p0);
		}
#pragma warning restore 0169

		IntPtr id_sendFileContents_Ljava_lang_String_;
		public unsafe void SendFileContents (string p0)
		{
			if (id_sendFileContents_Ljava_lang_String_ == IntPtr.Zero)
				id_sendFileContents_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "sendFileContents", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_sendFileContents_Ljava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
		}

	}

}
