using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Sgd {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.sgd']/class[@name='SGD']"
	[global::Android.Runtime.Register ("com/zebra/android/sgd/SGD", DoNotGenerateAcw=true)]
	public partial class SGD : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/sgd/SGD", typeof (SGD));
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

		protected SGD (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.sgd']/class[@name='SGD']/method[@name='DO' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register ("DO", "(Ljava/lang/String;Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;)Ljava/lang/String;", "")]
		public static unsafe string DO (string p0, string p1, global::Com.Zebra.Android.Comm.IZebraPrinterConnection p2)
		{
			const string __id = "DO.(Ljava/lang/String;Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.sgd']/class[@name='SGD']/method[@name='DO' and count(parameter)=5 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='com.zebra.android.comm.ZebraPrinterConnection'] and parameter[4][@type='int'] and parameter[5][@type='int']]"
		[Register ("DO", "(Ljava/lang/String;Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;II)Ljava/lang/String;", "")]
		public static unsafe string DO (string p0, string p1, global::Com.Zebra.Android.Comm.IZebraPrinterConnection p2, int p3, int p4)
		{
			const string __id = "DO.(Ljava/lang/String;Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;II)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [5];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
				__args [3] = new JniArgumentValue (p3);
				__args [4] = new JniArgumentValue (p4);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.sgd']/class[@name='SGD']/method[@name='GET' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register ("GET", "(Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;)Ljava/lang/String;", "")]
		public static unsafe string GET (string p0, global::Com.Zebra.Android.Comm.IZebraPrinterConnection p1)
		{
			const string __id = "GET.(Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.sgd']/class[@name='SGD']/method[@name='GET' and count(parameter)=4 and parameter[1][@type='java.lang.String'] and parameter[2][@type='com.zebra.android.comm.ZebraPrinterConnection'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
		[Register ("GET", "(Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;II)Ljava/lang/String;", "")]
		public static unsafe string GET (string p0, global::Com.Zebra.Android.Comm.IZebraPrinterConnection p1, int p2, int p3)
		{
			const string __id = "GET.(Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;II)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [4];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
				__args [2] = new JniArgumentValue (p2);
				__args [3] = new JniArgumentValue (p3);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.sgd']/class[@name='SGD']/method[@name='SET' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int'] and parameter[3][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register ("SET", "(Ljava/lang/String;ILcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public static unsafe void SET (string p0, int p1, global::Com.Zebra.Android.Comm.IZebraPrinterConnection p2)
		{
			const string __id = "SET.(Ljava/lang/String;ILcom/zebra/android/comm/ZebraPrinterConnection;)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.sgd']/class[@name='SGD']/method[@name='SET' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register ("SET", "(Ljava/lang/String;Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public static unsafe void SET (string p0, string p1, global::Com.Zebra.Android.Comm.IZebraPrinterConnection p2)
		{
			const string __id = "SET.(Ljava/lang/String;Ljava/lang/String;Lcom/zebra/android/comm/ZebraPrinterConnection;)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

	}
}
