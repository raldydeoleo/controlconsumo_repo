using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Util.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ThreadExecutor']"
	[global::Android.Runtime.Register ("com/zebra/android/util/internal/ThreadExecutor", DoNotGenerateAcw=true)]
	public partial class ThreadExecutor : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/util/internal/ThreadExecutor", typeof (ThreadExecutor));
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

		protected ThreadExecutor (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ThreadExecutor']/method[@name='execute' and count(parameter)=2 and parameter[1][@type='java.lang.Thread[]'] and parameter[2][@type='int']]"
		[Register ("execute", "([Ljava/lang/Thread;I)V", "")]
		public static unsafe void Execute (global::Java.Lang.Thread[] p0, int p1)
		{
			const string __id = "execute.([Ljava/lang/Thread;I)V";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (p1);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

	}
}
