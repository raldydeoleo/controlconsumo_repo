using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Discovery.Internal {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.discovery.internal']/interface[@name='ZebraDiscoSocket']"
	[Register ("com/zebra/android/discovery/internal/ZebraDiscoSocket", "", "Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocketInvoker")]
	public partial interface IZebraDiscoSocket : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/interface[@name='ZebraDiscoSocket']/method[@name='close' and count(parameter)=0]"
		[Register ("close", "()V", "GetCloseHandler:Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocketInvoker, ZebraLib")]
		void Close ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/interface[@name='ZebraDiscoSocket']/method[@name='joinGroup' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("joinGroup", "(Ljava/lang/String;)V", "GetJoinGroup_Ljava_lang_String_Handler:Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocketInvoker, ZebraLib")]
		void JoinGroup (string p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/interface[@name='ZebraDiscoSocket']/method[@name='receive' and count(parameter)=1 and parameter[1][@type='java.net.DatagramPacket']]"
		[Register ("receive", "(Ljava/net/DatagramPacket;)V", "GetReceive_Ljava_net_DatagramPacket_Handler:Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocketInvoker, ZebraLib")]
		void Receive (global::Java.Net.DatagramPacket p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/interface[@name='ZebraDiscoSocket']/method[@name='send' and count(parameter)=1 and parameter[1][@type='java.net.DatagramPacket']]"
		[Register ("send", "(Ljava/net/DatagramPacket;)V", "GetSend_Ljava_net_DatagramPacket_Handler:Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocketInvoker, ZebraLib")]
		void Send (global::Java.Net.DatagramPacket p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/interface[@name='ZebraDiscoSocket']/method[@name='setInterface' and count(parameter)=1 and parameter[1][@type='java.net.InetAddress']]"
		[Register ("setInterface", "(Ljava/net/InetAddress;)V", "GetSetInterface_Ljava_net_InetAddress_Handler:Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocketInvoker, ZebraLib")]
		void SetInterface (global::Java.Net.InetAddress p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/interface[@name='ZebraDiscoSocket']/method[@name='setSoTimeout' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("setSoTimeout", "(I)V", "GetSetSoTimeout_IHandler:Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocketInvoker, ZebraLib")]
		void SetSoTimeout (int p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/interface[@name='ZebraDiscoSocket']/method[@name='setTimeToLive' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("setTimeToLive", "(I)V", "GetSetTimeToLive_IHandler:Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocketInvoker, ZebraLib")]
		void SetTimeToLive (int p0);

	}

	[global::Android.Runtime.Register ("com/zebra/android/discovery/internal/ZebraDiscoSocket", DoNotGenerateAcw=true)]
	internal class IZebraDiscoSocketInvoker : global::Java.Lang.Object, IZebraDiscoSocket {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/discovery/internal/ZebraDiscoSocket", typeof (IZebraDiscoSocketInvoker));

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

		public static IZebraDiscoSocket GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IZebraDiscoSocket> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.discovery.internal.ZebraDiscoSocket"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IZebraDiscoSocketInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
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
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
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

		static Delegate cb_joinGroup_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetJoinGroup_Ljava_lang_String_Handler ()
		{
			if (cb_joinGroup_Ljava_lang_String_ == null)
				cb_joinGroup_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_JoinGroup_Ljava_lang_String_);
			return cb_joinGroup_Ljava_lang_String_;
		}

		static void n_JoinGroup_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.JoinGroup (p0);
		}
#pragma warning restore 0169

		IntPtr id_joinGroup_Ljava_lang_String_;
		public unsafe void JoinGroup (string p0)
		{
			if (id_joinGroup_Ljava_lang_String_ == IntPtr.Zero)
				id_joinGroup_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "joinGroup", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_joinGroup_Ljava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
		}

		static Delegate cb_receive_Ljava_net_DatagramPacket_;
#pragma warning disable 0169
		static Delegate GetReceive_Ljava_net_DatagramPacket_Handler ()
		{
			if (cb_receive_Ljava_net_DatagramPacket_ == null)
				cb_receive_Ljava_net_DatagramPacket_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_Receive_Ljava_net_DatagramPacket_);
			return cb_receive_Ljava_net_DatagramPacket_;
		}

		static void n_Receive_Ljava_net_DatagramPacket_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Java.Net.DatagramPacket p0 = global::Java.Lang.Object.GetObject<global::Java.Net.DatagramPacket> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.Receive (p0);
		}
#pragma warning restore 0169

		IntPtr id_receive_Ljava_net_DatagramPacket_;
		public unsafe void Receive (global::Java.Net.DatagramPacket p0)
		{
			if (id_receive_Ljava_net_DatagramPacket_ == IntPtr.Zero)
				id_receive_Ljava_net_DatagramPacket_ = JNIEnv.GetMethodID (class_ref, "receive", "(Ljava/net/DatagramPacket;)V");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_receive_Ljava_net_DatagramPacket_, __args);
		}

		static Delegate cb_send_Ljava_net_DatagramPacket_;
#pragma warning disable 0169
		static Delegate GetSend_Ljava_net_DatagramPacket_Handler ()
		{
			if (cb_send_Ljava_net_DatagramPacket_ == null)
				cb_send_Ljava_net_DatagramPacket_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_Send_Ljava_net_DatagramPacket_);
			return cb_send_Ljava_net_DatagramPacket_;
		}

		static void n_Send_Ljava_net_DatagramPacket_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Java.Net.DatagramPacket p0 = global::Java.Lang.Object.GetObject<global::Java.Net.DatagramPacket> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.Send (p0);
		}
#pragma warning restore 0169

		IntPtr id_send_Ljava_net_DatagramPacket_;
		public unsafe void Send (global::Java.Net.DatagramPacket p0)
		{
			if (id_send_Ljava_net_DatagramPacket_ == IntPtr.Zero)
				id_send_Ljava_net_DatagramPacket_ = JNIEnv.GetMethodID (class_ref, "send", "(Ljava/net/DatagramPacket;)V");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_send_Ljava_net_DatagramPacket_, __args);
		}

		static Delegate cb_setInterface_Ljava_net_InetAddress_;
#pragma warning disable 0169
		static Delegate GetSetInterface_Ljava_net_InetAddress_Handler ()
		{
			if (cb_setInterface_Ljava_net_InetAddress_ == null)
				cb_setInterface_Ljava_net_InetAddress_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SetInterface_Ljava_net_InetAddress_);
			return cb_setInterface_Ljava_net_InetAddress_;
		}

		static void n_SetInterface_Ljava_net_InetAddress_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Java.Net.InetAddress p0 = global::Java.Lang.Object.GetObject<global::Java.Net.InetAddress> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetInterface (p0);
		}
#pragma warning restore 0169

		IntPtr id_setInterface_Ljava_net_InetAddress_;
		public unsafe void SetInterface (global::Java.Net.InetAddress p0)
		{
			if (id_setInterface_Ljava_net_InetAddress_ == IntPtr.Zero)
				id_setInterface_Ljava_net_InetAddress_ = JNIEnv.GetMethodID (class_ref, "setInterface", "(Ljava/net/InetAddress;)V");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setInterface_Ljava_net_InetAddress_, __args);
		}

		static Delegate cb_setSoTimeout_I;
#pragma warning disable 0169
		static Delegate GetSetSoTimeout_IHandler ()
		{
			if (cb_setSoTimeout_I == null)
				cb_setSoTimeout_I = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, int>) n_SetSoTimeout_I);
			return cb_setSoTimeout_I;
		}

		static void n_SetSoTimeout_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.SetSoTimeout (p0);
		}
#pragma warning restore 0169

		IntPtr id_setSoTimeout_I;
		public unsafe void SetSoTimeout (int p0)
		{
			if (id_setSoTimeout_I == IntPtr.Zero)
				id_setSoTimeout_I = JNIEnv.GetMethodID (class_ref, "setSoTimeout", "(I)V");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setSoTimeout_I, __args);
		}

		static Delegate cb_setTimeToLive_I;
#pragma warning disable 0169
		static Delegate GetSetTimeToLive_IHandler ()
		{
			if (cb_setTimeToLive_I == null)
				cb_setTimeToLive_I = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, int>) n_SetTimeToLive_I);
			return cb_setTimeToLive_I;
		}

		static void n_SetTimeToLive_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.SetTimeToLive (p0);
		}
#pragma warning restore 0169

		IntPtr id_setTimeToLive_I;
		public unsafe void SetTimeToLive (int p0)
		{
			if (id_setTimeToLive_I == IntPtr.Zero)
				id_setTimeToLive_I = JNIEnv.GetMethodID (class_ref, "setTimeToLive", "(I)V");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_setTimeToLive_I, __args);
		}

	}

}
