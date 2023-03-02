using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='ZebraPrinterFactory']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/ZebraPrinterFactory", DoNotGenerateAcw=true)]
	public partial class ZebraPrinterFactory : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/ZebraPrinterFactory", typeof (ZebraPrinterFactory));
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

		protected ZebraPrinterFactory (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='ZebraPrinterFactory']/method[@name='getInstance' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register ("getInstance", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)Lcom/zebra/android/printer/ZebraPrinter;", "")]
		public static unsafe global::Com.Zebra.Android.Printer.IZebraPrinter GetInstance (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
		{
			const string __id = "getInstance.(Lcom/zebra/android/comm/ZebraPrinterConnection;)Lcom/zebra/android/printer/ZebraPrinter;";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='ZebraPrinterFactory']/method[@name='getInstance' and count(parameter)=2 and parameter[1][@type='com.zebra.android.printer.PrinterLanguage'] and parameter[2][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register ("getInstance", "(Lcom/zebra/android/printer/PrinterLanguage;Lcom/zebra/android/comm/ZebraPrinterConnection;)Lcom/zebra/android/printer/ZebraPrinter;", "")]
		public static unsafe global::Com.Zebra.Android.Printer.IZebraPrinter GetInstance (global::Com.Zebra.Android.Printer.PrinterLanguage p0, global::Com.Zebra.Android.Comm.IZebraPrinterConnection p1)
		{
			const string __id = "getInstance.(Lcom/zebra/android/printer/PrinterLanguage;Lcom/zebra/android/comm/ZebraPrinterConnection;)Lcom/zebra/android/printer/ZebraPrinter;";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='ZebraPrinterFactory']/method[@name='getInstance' and count(parameter)=2 and parameter[1][@type='java.lang.String[]'] and parameter[2][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register ("getInstance", "([Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;)Lcom/zebra/android/printer/ZebraPrinter;", "")]
		public static unsafe global::Com.Zebra.Android.Printer.IZebraPrinter GetInstance (string[] p0, global::Com.Zebra.Android.Comm.IZebraPrinterConnection p1)
		{
			const string __id = "getInstance.([Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;)Lcom/zebra/android/printer/ZebraPrinter;";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

	}
}
