using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Comm.Internal {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.comm.internal']/interface[@name='ZebraSocket']"
	[Register ("com/zebra/android/comm/internal/ZebraSocket", "", "Com.Zebra.Android.Comm.Internal.IZebraSocketInvoker")]
	public partial interface IZebraSocket : IJavaObject {

		global::System.IO.Stream InputStream {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm.internal']/interface[@name='ZebraSocket']/method[@name='getInputStream' and count(parameter)=0]"
			[Register ("getInputStream", "()Ljava/io/InputStream;", "GetGetInputStreamHandler:Com.Zebra.Android.Comm.Internal.IZebraSocketInvoker, ZebraLib")] get;
		}

		global::System.IO.Stream OutputStream {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm.internal']/interface[@name='ZebraSocket']/method[@name='getOutputStream' and count(parameter)=0]"
			[Register ("getOutputStream", "()Ljava/io/OutputStream;", "GetGetOutputStreamHandler:Com.Zebra.Android.Comm.Internal.IZebraSocketInvoker, ZebraLib")] get;
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm.internal']/interface[@name='ZebraSocket']/method[@name='close' and count(parameter)=0]"
		[Register ("close", "()V", "GetCloseHandler:Com.Zebra.Android.Comm.Internal.IZebraSocketInvoker, ZebraLib")]
		void Close ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.comm.internal']/interface[@name='ZebraSocket']/method[@name='connect' and count(parameter)=0]"
		[Register ("connect", "()V", "GetConnectHandler:Com.Zebra.Android.Comm.Internal.IZebraSocketInvoker, ZebraLib")]
		void Connect ();

	}

	[global::Android.Runtime.Register ("com/zebra/android/comm/internal/ZebraSocket", DoNotGenerateAcw=true)]
	internal class IZebraSocketInvoker : global::Java.Lang.Object, IZebraSocket {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/comm/internal/ZebraSocket", typeof (IZebraSocketInvoker));

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

		public static IZebraSocket GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IZebraSocket> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.comm.internal.ZebraSocket"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IZebraSocketInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_getInputStream;
#pragma warning disable 0169
		static Delegate GetGetInputStreamHandler ()
		{
			if (cb_getInputStream == null)
				cb_getInputStream = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetInputStream);
			return cb_getInputStream;
		}

		static IntPtr n_GetInputStream (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.Internal.IZebraSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.IZebraSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return global::Android.Runtime.InputStreamAdapter.ToLocalJniHandle (__this.InputStream);
		}
#pragma warning restore 0169

		IntPtr id_getInputStream;
		public unsafe global::System.IO.Stream InputStream {
			get {
				if (id_getInputStream == IntPtr.Zero)
					id_getInputStream = JNIEnv.GetMethodID (class_ref, "getInputStream", "()Ljava/io/InputStream;");
				return global::Android.Runtime.InputStreamInvoker.FromJniHandle (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getInputStream), JniHandleOwnership.TransferLocalRef);
			}
		}

		static Delegate cb_getOutputStream;
#pragma warning disable 0169
		static Delegate GetGetOutputStreamHandler ()
		{
			if (cb_getOutputStream == null)
				cb_getOutputStream = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetOutputStream);
			return cb_getOutputStream;
		}

		static IntPtr n_GetOutputStream (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.Internal.IZebraSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.IZebraSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return global::Android.Runtime.OutputStreamAdapter.ToLocalJniHandle (__this.OutputStream);
		}
#pragma warning restore 0169

		IntPtr id_getOutputStream;
		public unsafe global::System.IO.Stream OutputStream {
			get {
				if (id_getOutputStream == IntPtr.Zero)
					id_getOutputStream = JNIEnv.GetMethodID (class_ref, "getOutputStream", "()Ljava/io/OutputStream;");
				return global::Android.Runtime.OutputStreamInvoker.FromJniHandle (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getOutputStream), JniHandleOwnership.TransferLocalRef);
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
			global::Com.Zebra.Android.Comm.Internal.IZebraSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.IZebraSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
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

		static Delegate cb_connect;
#pragma warning disable 0169
		static Delegate GetConnectHandler ()
		{
			if (cb_connect == null)
				cb_connect = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_Connect);
			return cb_connect;
		}

		static void n_Connect (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Comm.Internal.IZebraSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.Internal.IZebraSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Connect ();
		}
#pragma warning restore 0169

		IntPtr id_connect;
		public unsafe void Connect ()
		{
			if (id_connect == IntPtr.Zero)
				id_connect = JNIEnv.GetMethodID (class_ref, "connect", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_connect);
		}

	}

}
