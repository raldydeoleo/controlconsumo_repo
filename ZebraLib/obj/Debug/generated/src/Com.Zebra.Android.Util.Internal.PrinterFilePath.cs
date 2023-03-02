using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Util.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='PrinterFilePath']"
	[global::Android.Runtime.Register ("com/zebra/android/util/internal/PrinterFilePath", DoNotGenerateAcw=true)]
	public partial class PrinterFilePath : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/util/internal/PrinterFilePath", typeof (PrinterFilePath));
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

		protected PrinterFilePath (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='PrinterFilePath']/constructor[@name='PrinterFilePath' and count(parameter)=2 and parameter[1][@type='java.lang.String'] and parameter[2][@type='java.lang.String']]"
		[Register (".ctor", "(Ljava/lang/String;Ljava/lang/String;)V", "")]
		public unsafe PrinterFilePath (string p0, string p1)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Ljava/lang/String;Ljava/lang/String;)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			IntPtr native_p0 = JNIEnv.NewString (p0);
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (native_p1);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		static Delegate cb_getDrive;
#pragma warning disable 0169
		static Delegate GetGetDriveHandler ()
		{
			if (cb_getDrive == null)
				cb_getDrive = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetDrive);
			return cb_getDrive;
		}

		static IntPtr n_GetDrive (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Util.Internal.PrinterFilePath __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Util.Internal.PrinterFilePath> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.Drive);
		}
#pragma warning restore 0169

		public virtual unsafe string Drive {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='PrinterFilePath']/method[@name='getDrive' and count(parameter)=0]"
			[Register ("getDrive", "()Ljava/lang/String;", "GetGetDriveHandler")]
			get {
				const string __id = "getDrive.()Ljava/lang/String;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

		static Delegate cb_getFileName;
#pragma warning disable 0169
		static Delegate GetGetFileNameHandler ()
		{
			if (cb_getFileName == null)
				cb_getFileName = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetFileName);
			return cb_getFileName;
		}

		static IntPtr n_GetFileName (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Util.Internal.PrinterFilePath __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Util.Internal.PrinterFilePath> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.FileName);
		}
#pragma warning restore 0169

		public virtual unsafe string FileName {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='PrinterFilePath']/method[@name='getFileName' and count(parameter)=0]"
			[Register ("getFileName", "()Ljava/lang/String;", "GetGetFileNameHandler")]
			get {
				const string __id = "getFileName.()Ljava/lang/String;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

	}
}
