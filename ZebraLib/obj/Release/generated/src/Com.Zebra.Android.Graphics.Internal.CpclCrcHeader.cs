using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Graphics.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CpclCrcHeader']"
	[global::Android.Runtime.Register ("com/zebra/android/graphics/internal/CpclCrcHeader", DoNotGenerateAcw=true)]
	public partial class CpclCrcHeader : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/graphics/internal/CpclCrcHeader", typeof (CpclCrcHeader));
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

		protected CpclCrcHeader (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CpclCrcHeader']/constructor[@name='CpclCrcHeader' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe CpclCrcHeader ()
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "()V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			try {
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), null);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, null);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CpclCrcHeader']/method[@name='byte2int' and count(parameter)=1 and parameter[1][@type='byte']]"
		[Register ("byte2int", "(B)I", "")]
		public static unsafe int Byte2int (sbyte p0)
		{
			const string __id = "byte2int.(B)I";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				var __rm = _members.StaticMethods.InvokeInt32Method (__id, __args);
				return __rm;
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CpclCrcHeader']/method[@name='convertTo8dot3' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("convertTo8dot3", "(Ljava/lang/String;)Ljava/lang/String;", "")]
		public static unsafe string ConvertTo8dot3 (string p0)
		{
			const string __id = "convertTo8dot3.(Ljava/lang/String;)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CpclCrcHeader']/method[@name='getCRC16ForCertificateFilesOnly' and count(parameter)=1 and parameter[1][@type='byte[]']]"
		[Register ("getCRC16ForCertificateFilesOnly", "([B)Ljava/lang/String;", "")]
		public static unsafe string GetCRC16ForCertificateFilesOnly (byte[] p0)
		{
			const string __id = "getCRC16ForCertificateFilesOnly.([B)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CpclCrcHeader']/method[@name='getWChecksum' and count(parameter)=1 and parameter[1][@type='byte[]']]"
		[Register ("getWChecksum", "([B)Ljava/lang/String;", "")]
		public static unsafe string GetWChecksum (byte[] p0)
		{
			const string __id = "getWChecksum.([B)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CpclCrcHeader']/method[@name='stringPadToPlaces' and count(parameter)=4 and parameter[1][@type='int'] and parameter[2][@type='char'] and parameter[3][@type='java.lang.String'] and parameter[4][@type='boolean']]"
		[Register ("stringPadToPlaces", "(ICLjava/lang/String;Z)Ljava/lang/String;", "")]
		public static unsafe string StringPadToPlaces (int p0, char p1, string p2, bool p3)
		{
			const string __id = "stringPadToPlaces.(ICLjava/lang/String;Z)Ljava/lang/String;";
			IntPtr native_p2 = JNIEnv.NewString (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [4];
				__args [0] = new JniArgumentValue (p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (native_p2);
				__args [3] = new JniArgumentValue (p3);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CpclCrcHeader']/method[@name='stringPadToPlaces' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.lang.String']]"
		[Register ("stringPadToPlaces", "(ILjava/lang/String;Ljava/lang/String;)Ljava/lang/String;", "")]
		public static unsafe string StringPadToPlaces (int p0, string p1, string p2)
		{
			const string __id = "stringPadToPlaces.(ILjava/lang/String;Ljava/lang/String;)Ljava/lang/String;";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			IntPtr native_p2 = JNIEnv.NewString (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (p0);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (native_p2);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
				JNIEnv.DeleteLocalRef (native_p2);
			}
		}

	}
}
