using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']"
	[Register ("com/zebra/android/printer/FormatUtil", "", "Com.Zebra.Android.Printer.IFormatUtilInvoker")]
	public partial interface IFormatUtil : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='getVariableFields' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("getVariableFields", "(Ljava/lang/String;)[Lcom/zebra/android/printer/FieldDescriptionData;", "GetGetVariableFields_Ljava_lang_String_Handler:Com.Zebra.Android.Printer.IFormatUtilInvoker, ZebraLib")]
		global::Com.Zebra.Android.Printer.FieldDescriptionData[] GetVariableFields (string p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='printStoredFormat' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String[]']]"
		[Register ("printStoredFormat", "(Ljava/lang/String;[Ljava/lang/String;)V", "GetPrintStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Handler:Com.Zebra.Android.Printer.IFormatUtilInvoker, ZebraLib")]
		void PrintStoredFormat (string p0, string[] p1);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='printStoredFormat' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String[]'] and parameter[3][@type='java.lang.String']]"
		[Register ("printStoredFormat", "(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)V", "GetPrintStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_Handler:Com.Zebra.Android.Printer.IFormatUtilInvoker, ZebraLib")]
		void PrintStoredFormat (string p0, string[] p1, string p2);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='printStoredFormat' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.util.Map&lt;java.lang.Integer, java.lang.String&gt;']]"
		[Register ("printStoredFormat", "(Ljava/lang/String;Ljava/util/Map;)V", "GetPrintStoredFormat_Ljava_lang_String_Ljava_util_Map_Handler:Com.Zebra.Android.Printer.IFormatUtilInvoker, ZebraLib")]
		void PrintStoredFormat (string p0, global::System.Collections.Generic.IDictionary<global::Java.Lang.Integer, string> p1);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='printStoredFormat' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.util.Map&lt;java.lang.Integer, java.lang.String&gt;'] and parameter[3][@type='java.lang.String']]"
		[Register ("printStoredFormat", "(Ljava/lang/String;Ljava/util/Map;Ljava/lang/String;)V", "GetPrintStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_Handler:Com.Zebra.Android.Printer.IFormatUtilInvoker, ZebraLib")]
		void PrintStoredFormat (string p0, global::System.Collections.Generic.IDictionary<global::Java.Lang.Integer, string> p1, string p2);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='retrieveFormatFromPrinter' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("retrieveFormatFromPrinter", "(Ljava/lang/String;)[B", "GetRetrieveFormatFromPrinter_Ljava_lang_String_Handler:Com.Zebra.Android.Printer.IFormatUtilInvoker, ZebraLib")]
		byte[] RetrieveFormatFromPrinter (string p0);

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/FormatUtil", DoNotGenerateAcw=true)]
	internal class IFormatUtilInvoker : global::Java.Lang.Object, IFormatUtil {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/FormatUtil", typeof (IFormatUtilInvoker));

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

		public static IFormatUtil GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IFormatUtil> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.printer.FormatUtil"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IFormatUtilInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_getVariableFields_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetGetVariableFields_Ljava_lang_String_Handler ()
		{
			if (cb_getVariableFields_Ljava_lang_String_ == null)
				cb_getVariableFields_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr>) n_GetVariableFields_Ljava_lang_String_);
			return cb_getVariableFields_Ljava_lang_String_;
		}

		static IntPtr n_GetVariableFields_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.IFormatUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFormatUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.GetVariableFields (p0));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_getVariableFields_Ljava_lang_String_;
		public unsafe global::Com.Zebra.Android.Printer.FieldDescriptionData[] GetVariableFields (string p0)
		{
			if (id_getVariableFields_Ljava_lang_String_ == IntPtr.Zero)
				id_getVariableFields_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "getVariableFields", "(Ljava/lang/String;)[Lcom/zebra/android/printer/FieldDescriptionData;");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			global::Com.Zebra.Android.Printer.FieldDescriptionData[] __ret = (global::Com.Zebra.Android.Printer.FieldDescriptionData[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getVariableFields_Ljava_lang_String_, __args), JniHandleOwnership.TransferLocalRef, typeof (global::Com.Zebra.Android.Printer.FieldDescriptionData));
			JNIEnv.DeleteLocalRef (native_p0);
			return __ret;
		}

		static Delegate cb_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_;
#pragma warning disable 0169
		static Delegate GetPrintStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Handler ()
		{
			if (cb_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_ == null)
				cb_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>) n_PrintStoredFormat_Ljava_lang_String_arrayLjava_lang_String_);
			return cb_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_;
		}

		static void n_PrintStoredFormat_Ljava_lang_String_arrayLjava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
		{
			global::Com.Zebra.Android.Printer.IFormatUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFormatUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			string[] p1 = (string[]) JNIEnv.GetArray (native_p1, JniHandleOwnership.DoNotTransfer, typeof (string));
			__this.PrintStoredFormat (p0, p1);
			if (p1 != null)
				JNIEnv.CopyArray (p1, native_p1);
		}
#pragma warning restore 0169

		IntPtr id_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_;
		public unsafe void PrintStoredFormat (string p0, string[] p1)
		{
			if (id_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_ == IntPtr.Zero)
				id_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_ = JNIEnv.GetMethodID (class_ref, "printStoredFormat", "(Ljava/lang/String;[Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewArray (p1);
			JValue* __args = stackalloc JValue [2];
			__args [0] = new JValue (native_p0);
			__args [1] = new JValue (native_p1);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
			if (p1 != null) {
				JNIEnv.CopyArray (native_p1, p1);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static Delegate cb_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetPrintStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_Handler ()
		{
			if (cb_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_ == null)
				cb_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr>) n_PrintStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_);
			return cb_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_;
		}

		static void n_PrintStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, IntPtr native_p2)
		{
			global::Com.Zebra.Android.Printer.IFormatUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFormatUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			string[] p1 = (string[]) JNIEnv.GetArray (native_p1, JniHandleOwnership.DoNotTransfer, typeof (string));
			string p2 = JNIEnv.GetString (native_p2, JniHandleOwnership.DoNotTransfer);
			__this.PrintStoredFormat (p0, p1, p2);
			if (p1 != null)
				JNIEnv.CopyArray (p1, native_p1);
		}
#pragma warning restore 0169

		IntPtr id_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_;
		public unsafe void PrintStoredFormat (string p0, string[] p1, string p2)
		{
			if (id_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_ == IntPtr.Zero)
				id_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "printStoredFormat", "(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewArray (p1);
			IntPtr native_p2 = JNIEnv.NewString (p2);
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue (native_p0);
			__args [1] = new JValue (native_p1);
			__args [2] = new JValue (native_p2);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_printStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
			if (p1 != null) {
				JNIEnv.CopyArray (native_p1, p1);
				JNIEnv.DeleteLocalRef (native_p1);
			}
			JNIEnv.DeleteLocalRef (native_p2);
		}

		static Delegate cb_printStoredFormat_Ljava_lang_String_Ljava_util_Map_;
#pragma warning disable 0169
		static Delegate GetPrintStoredFormat_Ljava_lang_String_Ljava_util_Map_Handler ()
		{
			if (cb_printStoredFormat_Ljava_lang_String_Ljava_util_Map_ == null)
				cb_printStoredFormat_Ljava_lang_String_Ljava_util_Map_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>) n_PrintStoredFormat_Ljava_lang_String_Ljava_util_Map_);
			return cb_printStoredFormat_Ljava_lang_String_Ljava_util_Map_;
		}

		static void n_PrintStoredFormat_Ljava_lang_String_Ljava_util_Map_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
		{
			global::Com.Zebra.Android.Printer.IFormatUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFormatUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = global::Android.Runtime.JavaDictionary<global::Java.Lang.Integer, string>.FromJniHandle (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.PrintStoredFormat (p0, p1);
		}
#pragma warning restore 0169

		IntPtr id_printStoredFormat_Ljava_lang_String_Ljava_util_Map_;
		public unsafe void PrintStoredFormat (string p0, global::System.Collections.Generic.IDictionary<global::Java.Lang.Integer, string> p1)
		{
			if (id_printStoredFormat_Ljava_lang_String_Ljava_util_Map_ == IntPtr.Zero)
				id_printStoredFormat_Ljava_lang_String_Ljava_util_Map_ = JNIEnv.GetMethodID (class_ref, "printStoredFormat", "(Ljava/lang/String;Ljava/util/Map;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = global::Android.Runtime.JavaDictionary<global::Java.Lang.Integer, string>.ToLocalJniHandle (p1);
			JValue* __args = stackalloc JValue [2];
			__args [0] = new JValue (native_p0);
			__args [1] = new JValue (native_p1);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_printStoredFormat_Ljava_lang_String_Ljava_util_Map_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
			JNIEnv.DeleteLocalRef (native_p1);
		}

		static Delegate cb_printStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetPrintStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_Handler ()
		{
			if (cb_printStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_ == null)
				cb_printStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr>) n_PrintStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_);
			return cb_printStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_;
		}

		static void n_PrintStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, IntPtr native_p2)
		{
			global::Com.Zebra.Android.Printer.IFormatUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFormatUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = global::Android.Runtime.JavaDictionary<global::Java.Lang.Integer, string>.FromJniHandle (native_p1, JniHandleOwnership.DoNotTransfer);
			string p2 = JNIEnv.GetString (native_p2, JniHandleOwnership.DoNotTransfer);
			__this.PrintStoredFormat (p0, p1, p2);
		}
#pragma warning restore 0169

		IntPtr id_printStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_;
		public unsafe void PrintStoredFormat (string p0, global::System.Collections.Generic.IDictionary<global::Java.Lang.Integer, string> p1, string p2)
		{
			if (id_printStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_ == IntPtr.Zero)
				id_printStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "printStoredFormat", "(Ljava/lang/String;Ljava/util/Map;Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = global::Android.Runtime.JavaDictionary<global::Java.Lang.Integer, string>.ToLocalJniHandle (p1);
			IntPtr native_p2 = JNIEnv.NewString (p2);
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue (native_p0);
			__args [1] = new JValue (native_p1);
			__args [2] = new JValue (native_p2);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_printStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
			JNIEnv.DeleteLocalRef (native_p1);
			JNIEnv.DeleteLocalRef (native_p2);
		}

		static Delegate cb_retrieveFormatFromPrinter_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetRetrieveFormatFromPrinter_Ljava_lang_String_Handler ()
		{
			if (cb_retrieveFormatFromPrinter_Ljava_lang_String_ == null)
				cb_retrieveFormatFromPrinter_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr>) n_RetrieveFormatFromPrinter_Ljava_lang_String_);
			return cb_retrieveFormatFromPrinter_Ljava_lang_String_;
		}

		static IntPtr n_RetrieveFormatFromPrinter_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.IFormatUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFormatUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.RetrieveFormatFromPrinter (p0));
			return __ret;
		}
#pragma warning restore 0169

		IntPtr id_retrieveFormatFromPrinter_Ljava_lang_String_;
		public unsafe byte[] RetrieveFormatFromPrinter (string p0)
		{
			if (id_retrieveFormatFromPrinter_Ljava_lang_String_ == IntPtr.Zero)
				id_retrieveFormatFromPrinter_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "retrieveFormatFromPrinter", "(Ljava/lang/String;)[B");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			byte[] __ret = (byte[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_retrieveFormatFromPrinter_Ljava_lang_String_, __args), JniHandleOwnership.TransferLocalRef, typeof (byte));
			JNIEnv.DeleteLocalRef (native_p0);
			return __ret;
		}

	}

}
