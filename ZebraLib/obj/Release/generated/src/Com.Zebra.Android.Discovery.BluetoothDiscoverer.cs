using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Discovery {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='BluetoothDiscoverer']"
	[global::Android.Runtime.Register ("com/zebra/android/discovery/BluetoothDiscoverer", DoNotGenerateAcw=true)]
	public partial class BluetoothDiscoverer : global::Java.Lang.Object {

		// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='BluetoothDiscoverer.BtRadioMonitor']"
		[global::Android.Runtime.Register ("com/zebra/android/discovery/BluetoothDiscoverer$BtRadioMonitor", DoNotGenerateAcw=true)]
		public partial class BtRadioMonitor : global::Android.Content.BroadcastReceiver {

			internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/discovery/BluetoothDiscoverer$BtRadioMonitor", typeof (BtRadioMonitor));
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

			protected BtRadioMonitor (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static Delegate cb_onReceive_Landroid_content_Context_Landroid_content_Intent_;
#pragma warning disable 0169
			static Delegate GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler ()
			{
				if (cb_onReceive_Landroid_content_Context_Landroid_content_Intent_ == null)
					cb_onReceive_Landroid_content_Context_Landroid_content_Intent_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>) n_OnReceive_Landroid_content_Context_Landroid_content_Intent_);
				return cb_onReceive_Landroid_content_Context_Landroid_content_Intent_;
			}

			static void n_OnReceive_Landroid_content_Context_Landroid_content_Intent_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
			{
				global::Com.Zebra.Android.Discovery.BluetoothDiscoverer.BtRadioMonitor __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.BluetoothDiscoverer.BtRadioMonitor> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Content.Context p0 = global::Java.Lang.Object.GetObject<global::Android.Content.Context> (native_p0, JniHandleOwnership.DoNotTransfer);
				global::Android.Content.Intent p1 = global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (native_p1, JniHandleOwnership.DoNotTransfer);
				__this.OnReceive (p0, p1);
			}
#pragma warning restore 0169

			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='BluetoothDiscoverer.BtRadioMonitor']/method[@name='onReceive' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='android.content.Intent']]"
			[Register ("onReceive", "(Landroid/content/Context;Landroid/content/Intent;)V", "GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler")]
			public override unsafe void OnReceive (global::Android.Content.Context p0, global::Android.Content.Intent p1)
			{
				const string __id = "onReceive.(Landroid/content/Context;Landroid/content/Intent;)V";
				try {
					JniArgumentValue* __args = stackalloc JniArgumentValue [2];
					__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
					__args [1] = new JniArgumentValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
					_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
				} finally {
				}
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='BluetoothDiscoverer.BtReceiver']"
		[global::Android.Runtime.Register ("com/zebra/android/discovery/BluetoothDiscoverer$BtReceiver", DoNotGenerateAcw=true)]
		public partial class BtReceiver : global::Android.Content.BroadcastReceiver {

			internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/discovery/BluetoothDiscoverer$BtReceiver", typeof (BtReceiver));
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

			protected BtReceiver (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static Delegate cb_onReceive_Landroid_content_Context_Landroid_content_Intent_;
#pragma warning disable 0169
			static Delegate GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler ()
			{
				if (cb_onReceive_Landroid_content_Context_Landroid_content_Intent_ == null)
					cb_onReceive_Landroid_content_Context_Landroid_content_Intent_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>) n_OnReceive_Landroid_content_Context_Landroid_content_Intent_);
				return cb_onReceive_Landroid_content_Context_Landroid_content_Intent_;
			}

			static void n_OnReceive_Landroid_content_Context_Landroid_content_Intent_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
			{
				global::Com.Zebra.Android.Discovery.BluetoothDiscoverer.BtReceiver __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.BluetoothDiscoverer.BtReceiver> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Content.Context p0 = global::Java.Lang.Object.GetObject<global::Android.Content.Context> (native_p0, JniHandleOwnership.DoNotTransfer);
				global::Android.Content.Intent p1 = global::Java.Lang.Object.GetObject<global::Android.Content.Intent> (native_p1, JniHandleOwnership.DoNotTransfer);
				__this.OnReceive (p0, p1);
			}
#pragma warning restore 0169

			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='BluetoothDiscoverer.BtReceiver']/method[@name='onReceive' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='android.content.Intent']]"
			[Register ("onReceive", "(Landroid/content/Context;Landroid/content/Intent;)V", "GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler")]
			public override unsafe void OnReceive (global::Android.Content.Context p0, global::Android.Content.Intent p1)
			{
				const string __id = "onReceive.(Landroid/content/Context;Landroid/content/Intent;)V";
				try {
					JniArgumentValue* __args = stackalloc JniArgumentValue [2];
					__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
					__args [1] = new JniArgumentValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
					_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
				} finally {
				}
			}

		}

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/discovery/BluetoothDiscoverer", typeof (BluetoothDiscoverer));
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

		protected BluetoothDiscoverer (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='BluetoothDiscoverer']/method[@name='findPrinters' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='com.zebra.android.discovery.DiscoveryHandler']]"
		[Register ("findPrinters", "(Landroid/content/Context;Lcom/zebra/android/discovery/DiscoveryHandler;)V", "")]
		public static unsafe void FindPrinters (global::Android.Content.Context p0, global::Com.Zebra.Android.Discovery.IDiscoveryHandler p1)
		{
			const string __id = "findPrinters.(Landroid/content/Context;Lcom/zebra/android/discovery/DiscoveryHandler;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
			}
		}

	}
}
