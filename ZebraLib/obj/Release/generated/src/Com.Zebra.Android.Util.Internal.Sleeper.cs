using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Util.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='Sleeper']"
	[global::Android.Runtime.Register ("com/zebra/android/util/internal/Sleeper", DoNotGenerateAcw=true)]
	public partial class Sleeper : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/util/internal/Sleeper", typeof (Sleeper));
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

		protected Sleeper (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='Sleeper']/constructor[@name='Sleeper' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		protected unsafe Sleeper ()
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

		static Delegate cb_performSleep_J;
#pragma warning disable 0169
		static Delegate GetPerformSleep_JHandler ()
		{
			if (cb_performSleep_J == null)
				cb_performSleep_J = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, long>) n_PerformSleep_J);
			return cb_performSleep_J;
		}

		static void n_PerformSleep_J (IntPtr jnienv, IntPtr native__this, long p0)
		{
			global::Com.Zebra.Android.Util.Internal.Sleeper __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Util.Internal.Sleeper> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.PerformSleep (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='Sleeper']/method[@name='performSleep' and count(parameter)=1 and parameter[1][@type='long']]"
		[Register ("performSleep", "(J)V", "GetPerformSleep_JHandler")]
		protected virtual unsafe void PerformSleep (long p0)
		{
			const string __id = "performSleep.(J)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='Sleeper']/method[@name='sleep' and count(parameter)=1 and parameter[1][@type='long']]"
		[Register ("sleep", "(J)V", "")]
		public static unsafe void Sleep (long p0)
		{
			const string __id = "sleep.(J)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
			}
		}

	}
}
