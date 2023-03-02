using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.printer.internal']/interface[@name='ZebraFileConnection']"
	[Register ("com/zebra/android/printer/internal/ZebraFileConnection", "", "Com.Zebra.Android.Printer.Internal.IZebraFileConnectionInvoker")]
	public partial interface IZebraFileConnection : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/interface[@name='ZebraFileConnection']/method[@name='close' and count(parameter)=0]"
		[Register ("close", "()V", "GetCloseHandler:Com.Zebra.Android.Printer.Internal.IZebraFileConnectionInvoker, ZebraLib")]
		void Close ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/interface[@name='ZebraFileConnection']/method[@name='fileSize' and count(parameter)=0]"
		[Register ("fileSize", "()I", "GetFileSizeHandler:Com.Zebra.Android.Printer.Internal.IZebraFileConnectionInvoker, ZebraLib")]
		int FileSize ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/interface[@name='ZebraFileConnection']/method[@name='openInputStream' and count(parameter)=0]"
		[Register ("openInputStream", "()Ljava/io/InputStream;", "GetOpenInputStreamHandler:Com.Zebra.Android.Printer.Internal.IZebraFileConnectionInvoker, ZebraLib")]
		global::System.IO.Stream OpenInputStream ();

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/ZebraFileConnection", DoNotGenerateAcw=true)]
	internal class IZebraFileConnectionInvoker : global::Java.Lang.Object, IZebraFileConnection {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/internal/ZebraFileConnection", typeof (IZebraFileConnectionInvoker));

		static IntPtr java_class_ref {
			get { return _members.JniPeerType.PeerReference.Handle; }
		}

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		IntPtr class_ref;

		public static IZebraFileConnection GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IZebraFileConnection> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.printer.internal.ZebraFileConnection"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IZebraFileConnectionInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
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
			global::Com.Zebra.Android.Printer.Internal.IZebraFileConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.IZebraFileConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Close ();
		}
#pragma warning restore 0169

		IntPtr id_close;
		public unsafe void Close ()
		{
			if (id_close == IntPtr.Zero)
				id_close = JNIEnv.GetMethodID (class_ref, "close", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_close);
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
			global::Com.Zebra.Android.Printer.Internal.IZebraFileConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.IZebraFileConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.FileSize ();
		}
#pragma warning restore 0169

		IntPtr id_fileSize;
		public unsafe int FileSize ()
		{
			if (id_fileSize == IntPtr.Zero)
				id_fileSize = JNIEnv.GetMethodID (class_ref, "fileSize", "()I");
			return JNIEnv.CallIntMethod (((global::Java.Lang.Object) this).Handle, id_fileSize);
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
			global::Com.Zebra.Android.Printer.Internal.IZebraFileConnection __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.IZebraFileConnection> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return global::Android.Runtime.InputStreamAdapter.ToLocalJniHandle (__this.OpenInputStream ());
		}
#pragma warning restore 0169

		IntPtr id_openInputStream;
		public unsafe global::System.IO.Stream OpenInputStream ()
		{
			if (id_openInputStream == IntPtr.Zero)
				id_openInputStream = JNIEnv.GetMethodID (class_ref, "openInputStream", "()Ljava/io/InputStream;");
			return global::Android.Runtime.InputStreamInvoker.FromJniHandle (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_openInputStream), JniHandleOwnership.TransferLocalRef);
		}

	}

}
