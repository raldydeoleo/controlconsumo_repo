using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraFileConnectionImpl']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/ZebraFileConnectionImpl", DoNotGenerateAcw=true)]
	public partial class ZebraFileConnectionImpl : global::Java.Lang.Object, global::Com.Zebra.Android.Printer.Internal.IZebraFileConnection {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/ZebraFileConnectionImpl", typeof (ZebraFileConnectionImpl));
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

		protected ZebraFileConnectionImpl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraFileConnectionImpl']/constructor[@name='ZebraFileConnectionImpl' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register (".ctor", "(Ljava/lang/String;)V", "")]
		public unsafe ZebraFileConnectionImpl (string p0)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Ljava/lang/String;)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static Delegate cb_close;
#pragma warning disable 0169
		static Delegate GetCloseHandler ()
		{
			if (cb_close == null)
				cb_close = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Close);
			return cb_close;
		}

		static void n_Close (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.ZebraFileConnectionImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraFileConnectionImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Close ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraFileConnectionImpl']/method[@name='close' and count(parameter)=0]"
		[Register ("close", "()V", "GetCloseHandler")]
		public virtual unsafe void Close ()
		{
			const string __id = "close.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
		}

		static Delegate cb_fileSize;
#pragma warning disable 0169
		static Delegate GetFileSizeHandler ()
		{
			if (cb_fileSize == null)
				cb_fileSize = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int>) n_FileSize);
			return cb_fileSize;
		}

		static int n_FileSize (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.ZebraFileConnectionImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraFileConnectionImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.FileSize ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraFileConnectionImpl']/method[@name='fileSize' and count(parameter)=0]"
		[Register ("fileSize", "()I", "GetFileSizeHandler")]
		public virtual unsafe int FileSize ()
		{
			const string __id = "fileSize.()I";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualInt32Method (__id, this, null);
				return __rm;
			} finally {
			}
		}

		static Delegate cb_openInputStream;
#pragma warning disable 0169
		static Delegate GetOpenInputStreamHandler ()
		{
			if (cb_openInputStream == null)
				cb_openInputStream = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_OpenInputStream);
			return cb_openInputStream;
		}

		static IntPtr n_OpenInputStream (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.ZebraFileConnectionImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraFileConnectionImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return global::Android.Runtime.InputStreamAdapter.ToLocalJniHandle (__this.OpenInputStream ());
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraFileConnectionImpl']/method[@name='openInputStream' and count(parameter)=0]"
		[Register ("openInputStream", "()Ljava/io/InputStream;", "GetOpenInputStreamHandler")]
		public virtual unsafe global::System.IO.Stream OpenInputStream ()
		{
			const string __id = "openInputStream.()Ljava/io/InputStream;";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
				return global::Android.Runtime.InputStreamInvoker.FromJniHandle (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}

	}
}
