using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterLanguage']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/PrinterLanguage", DoNotGenerateAcw=true)]
	public sealed partial class PrinterLanguage : global::Java.Lang.Enum {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterLanguage']/field[@name='CPCL']"
		[Register ("CPCL")]
		public static global::Com.Zebra.Android.Printer.PrinterLanguage Cpcl {
			get {
				const string __id = "CPCL.Lcom/zebra/android/printer/PrinterLanguage;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.PrinterLanguage> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterLanguage']/field[@name='ZPL']"
		[Register ("ZPL")]
		public static global::Com.Zebra.Android.Printer.PrinterLanguage Zpl {
			get {
				const string __id = "ZPL.Lcom/zebra/android/printer/PrinterLanguage;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.PrinterLanguage> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/PrinterLanguage", typeof (PrinterLanguage));
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

		internal PrinterLanguage (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterLanguage']/method[@name='valueOf' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("valueOf", "(Ljava/lang/String;)Lcom/zebra/android/printer/PrinterLanguage;", "")]
		public static unsafe global::Com.Zebra.Android.Printer.PrinterLanguage ValueOf (string p0)
		{
			const string __id = "valueOf.(Ljava/lang/String;)Lcom/zebra/android/printer/PrinterLanguage;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.PrinterLanguage> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterLanguage']/method[@name='values' and count(parameter)=0]"
		[Register ("values", "()[Lcom/zebra/android/printer/PrinterLanguage;", "")]
		public static unsafe global::Com.Zebra.Android.Printer.PrinterLanguage[] Values ()
		{
			const string __id = "values.()[Lcom/zebra/android/printer/PrinterLanguage;";
			try {
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, null);
				return (global::Com.Zebra.Android.Printer.PrinterLanguage[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (global::Com.Zebra.Android.Printer.PrinterLanguage));
			} finally {
			}
		}

	}
}
