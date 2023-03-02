using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Discovery.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='ZebraDiscoSocketImpl']"
	[global::Android.Runtime.Register ("com/zebra/android/discovery/internal/ZebraDiscoSocketImpl", DoNotGenerateAcw=true)]
	public partial class ZebraDiscoSocketImpl : global::Java.Lang.Object, global::Com.Zebra.Android.Discovery.Internal.IZebraDiscoSocket {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/discovery/internal/ZebraDiscoSocketImpl", typeof (ZebraDiscoSocketImpl));
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

		protected ZebraDiscoSocketImpl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='ZebraDiscoSocketImpl']/constructor[@name='ZebraDiscoSocketImpl' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe ZebraDiscoSocketImpl ()
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
			global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Close ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='ZebraDiscoSocketImpl']/method[@name='close' and count(parameter)=0]"
		[Register ("close", "()V", "GetCloseHandler")]
		public virtual unsafe void Close ()
		{
			const string __id = "close.()V";
			try {
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, null);
			} finally {
			}
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
			global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.JoinGroup (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='ZebraDiscoSocketImpl']/method[@name='joinGroup' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("joinGroup", "(Ljava/lang/String;)V", "GetJoinGroup_Ljava_lang_String_Handler")]
		public virtual unsafe void JoinGroup (string p0)
		{
			const string __id = "joinGroup.(Ljava/lang/String;)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
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
			global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Java.Net.DatagramPacket p0 = global::Java.Lang.Object.GetObject<global::Java.Net.DatagramPacket> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.Receive (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='ZebraDiscoSocketImpl']/method[@name='receive' and count(parameter)=1 and parameter[1][@type='java.net.DatagramPacket']]"
		[Register ("receive", "(Ljava/net/DatagramPacket;)V", "GetReceive_Ljava_net_DatagramPacket_Handler")]
		public virtual unsafe void Receive (global::Java.Net.DatagramPacket p0)
		{
			const string __id = "receive.(Ljava/net/DatagramPacket;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
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
			global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Java.Net.DatagramPacket p0 = global::Java.Lang.Object.GetObject<global::Java.Net.DatagramPacket> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.Send (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='ZebraDiscoSocketImpl']/method[@name='send' and count(parameter)=1 and parameter[1][@type='java.net.DatagramPacket']]"
		[Register ("send", "(Ljava/net/DatagramPacket;)V", "GetSend_Ljava_net_DatagramPacket_Handler")]
		public virtual unsafe void Send (global::Java.Net.DatagramPacket p0)
		{
			const string __id = "send.(Ljava/net/DatagramPacket;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
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
			global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Java.Net.InetAddress p0 = global::Java.Lang.Object.GetObject<global::Java.Net.InetAddress> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SetInterface (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='ZebraDiscoSocketImpl']/method[@name='setInterface' and count(parameter)=1 and parameter[1][@type='java.net.InetAddress']]"
		[Register ("setInterface", "(Ljava/net/InetAddress;)V", "GetSetInterface_Ljava_net_InetAddress_Handler")]
		public virtual unsafe void SetInterface (global::Java.Net.InetAddress p0)
		{
			const string __id = "setInterface.(Ljava/net/InetAddress;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
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
			global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.SetSoTimeout (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='ZebraDiscoSocketImpl']/method[@name='setSoTimeout' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("setSoTimeout", "(I)V", "GetSetSoTimeout_IHandler")]
		public virtual unsafe void SetSoTimeout (int p0)
		{
			const string __id = "setSoTimeout.(I)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
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
			global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.Internal.ZebraDiscoSocketImpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.SetTimeToLive (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery.internal']/class[@name='ZebraDiscoSocketImpl']/method[@name='setTimeToLive' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("setTimeToLive", "(I)V", "GetSetTimeToLive_IHandler")]
		public virtual unsafe void SetTimeToLive (int p0)
		{
			const string __id = "setTimeToLive.(I)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
		}

	}
}
