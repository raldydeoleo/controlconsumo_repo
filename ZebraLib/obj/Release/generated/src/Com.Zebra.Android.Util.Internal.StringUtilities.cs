using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Util.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='StringUtilities']"
	[global::Android.Runtime.Register ("com/zebra/android/util/internal/StringUtilities", DoNotGenerateAcw=true)]
	public partial class StringUtilities : global::Java.Lang.Object {


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='StringUtilities']/field[@name='CRLF']"
		[Register ("CRLF")]
		public const string Crlf = (string) "\u000D\u000A";

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='StringUtilities']/field[@name='LF']"
		[Register ("LF")]
		public const string Lf = (string) "\u000A";
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/util/internal/StringUtilities", typeof (StringUtilities));
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

		protected StringUtilities (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='StringUtilities']/constructor[@name='StringUtilities' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe StringUtilities ()
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

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='StringUtilities']/method[@name='countSubstringOccurences' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String']]"
		[Register ("countSubstringOccurences", "(Ljava/lang/String;Ljava/lang/String;)I", "")]
		public static unsafe int CountSubstringOccurences (string p0, string p1)
		{
			const string __id = "countSubstringOccurences.(Ljava/lang/String;Ljava/lang/String;)I";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				var __rm = _members.StaticMethods.InvokeInt32Method (__id, __args);
				return __rm;
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='StringUtilities']/method[@name='doesPrefixExistInArray' and count(parameter)=2 and parameter[1][@type='java.lang.String[]'] and parameter[2][@type='java.lang.String']]"
		[Register ("doesPrefixExistInArray", "([Ljava/lang/String;Ljava/lang/String;)Z", "")]
		public static unsafe bool DoesPrefixExistInArray (string[] p0, string p1)
		{
			const string __id = "doesPrefixExistInArray.([Ljava/lang/String;Ljava/lang/String;)Z";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				var __rm = _members.StaticMethods.InvokeBooleanMethod (__id, __args);
				return __rm;
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='StringUtilities']/method[@name='indexOf' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String[]'] and parameter[3][@type='int']]"
		[Register ("indexOf", "(Ljava/lang/String;[Ljava/lang/String;I)I", "")]
		public static unsafe int IndexOf (string p0, string[] p1, int p2)
		{
			const string __id = "indexOf.(Ljava/lang/String;[Ljava/lang/String;I)I";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewArray (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (p2);
				var __rm = _members.StaticMethods.InvokeInt32Method (__id, __args);
				return __rm;
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				if (p1 != null) {
					JNIEnv.CopyArray (native_p1, p1);
					JNIEnv.DeleteLocalRef (native_p1);
				}
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='StringUtilities']/method[@name='padWithChar' and count(parameter)=4 and parameter[1][@type='java.lang.String'] and parameter[2][@type='char'] and parameter[3][@type='int'] and parameter[4][@type='boolean']]"
		[Register ("padWithChar", "(Ljava/lang/String;CIZ)Ljava/lang/String;", "")]
		public static unsafe string PadWithChar (string p0, char p1, int p2, bool p3)
		{
			const string __id = "padWithChar.(Ljava/lang/String;CIZ)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [4];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (p2);
				__args [3] = new JniArgumentValue (p3);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='StringUtilities']/method[@name='split' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String']]"
		[Register ("split", "(Ljava/lang/String;Ljava/lang/String;)[Ljava/lang/String;", "")]
		public static unsafe string[] Split (string p0, string p1)
		{
			const string __id = "split.(Ljava/lang/String;Ljava/lang/String;)[Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return (string[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (string));
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='StringUtilities']/method[@name='stripQuotes' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("stripQuotes", "(Ljava/lang/String;)Ljava/lang/String;", "")]
		public static unsafe string StripQuotes (string p0)
		{
			const string __id = "stripQuotes.(Ljava/lang/String;)Ljava/lang/String;";
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

	}
}
