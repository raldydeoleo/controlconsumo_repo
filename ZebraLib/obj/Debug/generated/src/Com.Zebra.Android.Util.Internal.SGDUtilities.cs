using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Util.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='SGDUtilities']"
	[global::Android.Runtime.Register ("com/zebra/android/util/internal/SGDUtilities", DoNotGenerateAcw=true)]
	public partial class SGDUtilities : global::Java.Lang.Object {


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='SGDUtilities']/field[@name='APPL_NAME']"
		[Register ("APPL_NAME")]
		public const string ApplName = (string) "appl.name";
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/util/internal/SGDUtilities", typeof (SGDUtilities));
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

		protected SGDUtilities (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='SGDUtilities']/constructor[@name='SGDUtilities' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe SGDUtilities ()
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

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='SGDUtilities']/method[@name='decorateWithGetCommand' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("decorateWithGetCommand", "(Ljava/lang/String;)Ljava/lang/String;", "")]
		public static unsafe string DecorateWithGetCommand (string p0)
		{
			const string __id = "decorateWithGetCommand.(Ljava/lang/String;)Ljava/lang/String;";
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
