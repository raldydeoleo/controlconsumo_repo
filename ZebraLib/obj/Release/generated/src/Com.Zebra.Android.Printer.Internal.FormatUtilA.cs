using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FormatUtilA']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/FormatUtilA", DoNotGenerateAcw=true)]
	public abstract partial class FormatUtilA : global::Java.Lang.Object, global::Com.Zebra.Android.Printer.IFormatUtil {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FormatUtilA']/field[@name='printerConnection']"
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
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/FormatUtilA", typeof (FormatUtilA));
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

		protected FormatUtilA (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FormatUtilA']/constructor[@name='FormatUtilA' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe FormatUtilA (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
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
			global::Com.Zebra.Android.Printer.Internal.FormatUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FormatUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			string[] p1 = (string[]) JNIEnv.GetArray (native_p1, JniHandleOwnership.DoNotTransfer, typeof (string));
			__this.PrintStoredFormat (p0, p1);
			if (p1 != null)
				JNIEnv.CopyArray (p1, native_p1);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FormatUtilA']/method[@name='printStoredFormat' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String[]']]"
		[Register ("printStoredFormat", "(Ljava/lang/String;[Ljava/lang/String;)V", "GetPrintStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Handler")]
		public virtual unsafe void PrintStoredFormat (string p0, string[] p1)
		{
			const string __id = "printStoredFormat.(Ljava/lang/String;[Ljava/lang/String;)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewArray (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				if (p1 != null) {
					JNIEnv.CopyArray (native_p1, p1);
					JNIEnv.DeleteLocalRef (native_p1);
				}
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
			global::Com.Zebra.Android.Printer.Internal.FormatUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FormatUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			string[] p1 = (string[]) JNIEnv.GetArray (native_p1, JniHandleOwnership.DoNotTransfer, typeof (string));
			string p2 = JNIEnv.GetString (native_p2, JniHandleOwnership.DoNotTransfer);
			__this.PrintStoredFormat (p0, p1, p2);
			if (p1 != null)
				JNIEnv.CopyArray (p1, native_p1);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FormatUtilA']/method[@name='printStoredFormat' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String[]'] and parameter[3][@type='java.lang.String']]"
		[Register ("printStoredFormat", "(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)V", "GetPrintStoredFormat_Ljava_lang_String_arrayLjava_lang_String_Ljava_lang_String_Handler")]
		public virtual unsafe void PrintStoredFormat (string p0, string[] p1, string p2)
		{
			const string __id = "printStoredFormat.(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewArray (p1);
			IntPtr native_p2 = JNIEnv.NewString (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (native_p2);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				if (p1 != null) {
					JNIEnv.CopyArray (native_p1, p1);
					JNIEnv.DeleteLocalRef (native_p1);
				}
				JNIEnv.DeleteLocalRef (native_p2);
			}
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
			global::Com.Zebra.Android.Printer.Internal.FormatUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FormatUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.GetVariableFields (p0));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='getVariableFields' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("getVariableFields", "(Ljava/lang/String;)[Lcom/zebra/android/printer/FieldDescriptionData;", "GetGetVariableFields_Ljava_lang_String_Handler")]
		public abstract global::Com.Zebra.Android.Printer.FieldDescriptionData[] GetVariableFields (string p0);

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
			global::Com.Zebra.Android.Printer.Internal.FormatUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FormatUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = global::Android.Runtime.JavaDictionary<global::Java.Lang.Integer, string>.FromJniHandle (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.PrintStoredFormat (p0, p1);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='printStoredFormat' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.util.Map&lt;java.lang.Integer, java.lang.String&gt;']]"
		[Register ("printStoredFormat", "(Ljava/lang/String;Ljava/util/Map;)V", "GetPrintStoredFormat_Ljava_lang_String_Ljava_util_Map_Handler")]
		public abstract void PrintStoredFormat (string p0, global::System.Collections.Generic.IDictionary<global::Java.Lang.Integer, string> p1);

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
			global::Com.Zebra.Android.Printer.Internal.FormatUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FormatUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			var p1 = global::Android.Runtime.JavaDictionary<global::Java.Lang.Integer, string>.FromJniHandle (native_p1, JniHandleOwnership.DoNotTransfer);
			string p2 = JNIEnv.GetString (native_p2, JniHandleOwnership.DoNotTransfer);
			__this.PrintStoredFormat (p0, p1, p2);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='printStoredFormat' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.util.Map&lt;java.lang.Integer, java.lang.String&gt;'] and parameter[3][@type='java.lang.String']]"
		[Register ("printStoredFormat", "(Ljava/lang/String;Ljava/util/Map;Ljava/lang/String;)V", "GetPrintStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_Handler")]
		public abstract void PrintStoredFormat (string p0, global::System.Collections.Generic.IDictionary<global::Java.Lang.Integer, string> p1, string p2);

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
			global::Com.Zebra.Android.Printer.Internal.FormatUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FormatUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.RetrieveFormatFromPrinter (p0));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='retrieveFormatFromPrinter' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("retrieveFormatFromPrinter", "(Ljava/lang/String;)[B", "GetRetrieveFormatFromPrinter_Ljava_lang_String_Handler")]
		public abstract byte[] RetrieveFormatFromPrinter (string p0);

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/FormatUtilA", DoNotGenerateAcw=true)]
	internal partial class FormatUtilAInvoker : FormatUtilA {

		public FormatUtilAInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer) {}

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/internal/FormatUtilA", typeof (FormatUtilAInvoker));

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='getVariableFields' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("getVariableFields", "(Ljava/lang/String;)[Lcom/zebra/android/printer/FieldDescriptionData;", "GetGetVariableFields_Ljava_lang_String_Handler")]
		public override unsafe global::Com.Zebra.Android.Printer.FieldDescriptionData[] GetVariableFields (string p0)
		{
			const string __id = "getVariableFields.(Ljava/lang/String;)[Lcom/zebra/android/printer/FieldDescriptionData;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod (__id, this, __args);
				return (global::Com.Zebra.Android.Printer.FieldDescriptionData[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (global::Com.Zebra.Android.Printer.FieldDescriptionData));
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='printStoredFormat' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.util.Map&lt;java.lang.Integer, java.lang.String&gt;']]"
		[Register ("printStoredFormat", "(Ljava/lang/String;Ljava/util/Map;)V", "GetPrintStoredFormat_Ljava_lang_String_Ljava_util_Map_Handler")]
		public override unsafe void PrintStoredFormat (string p0, global::System.Collections.Generic.IDictionary<global::Java.Lang.Integer, string> p1)
		{
			const string __id = "printStoredFormat.(Ljava/lang/String;Ljava/util/Map;)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = global::Android.Runtime.JavaDictionary<global::Java.Lang.Integer, string>.ToLocalJniHandle (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				_members.InstanceMethods.InvokeAbstractVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='printStoredFormat' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.util.Map&lt;java.lang.Integer, java.lang.String&gt;'] and parameter[3][@type='java.lang.String']]"
		[Register ("printStoredFormat", "(Ljava/lang/String;Ljava/util/Map;Ljava/lang/String;)V", "GetPrintStoredFormat_Ljava_lang_String_Ljava_util_Map_Ljava_lang_String_Handler")]
		public override unsafe void PrintStoredFormat (string p0, global::System.Collections.Generic.IDictionary<global::Java.Lang.Integer, string> p1, string p2)
		{
			const string __id = "printStoredFormat.(Ljava/lang/String;Ljava/util/Map;Ljava/lang/String;)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = global::Android.Runtime.JavaDictionary<global::Java.Lang.Integer, string>.ToLocalJniHandle (p1);
			IntPtr native_p2 = JNIEnv.NewString (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (native_p2);
				_members.InstanceMethods.InvokeAbstractVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='FormatUtil']/method[@name='retrieveFormatFromPrinter' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("retrieveFormatFromPrinter", "(Ljava/lang/String;)[B", "GetRetrieveFormatFromPrinter_Ljava_lang_String_Handler")]
		public override unsafe byte[] RetrieveFormatFromPrinter (string p0)
		{
			const string __id = "retrieveFormatFromPrinter.(Ljava/lang/String;)[B";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod (__id, this, __args);
				return (byte[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (byte));
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}

}
