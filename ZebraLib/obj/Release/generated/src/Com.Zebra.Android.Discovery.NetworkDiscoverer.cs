using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Discovery {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='NetworkDiscoverer']"
	[global::Android.Runtime.Register ("com/zebra/android/discovery/NetworkDiscoverer", DoNotGenerateAcw=true)]
	public partial class NetworkDiscoverer : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/discovery/NetworkDiscoverer", typeof (NetworkDiscoverer));
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

		protected NetworkDiscoverer (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='NetworkDiscoverer']/method[@name='directedBroadcast' and count(parameter)=2 and parameter[1][@type='com.zebra.android.discovery.DiscoveryHandler'] and parameter[2][@type='java.lang.String']]"
		[Register ("directedBroadcast", "(Lcom/zebra/android/discovery/DiscoveryHandler;Ljava/lang/String;)V", "")]
		public static unsafe void DirectedBroadcast (global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0, string p1)
		{
			const string __id = "directedBroadcast.(Lcom/zebra/android/discovery/DiscoveryHandler;Ljava/lang/String;)V";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='NetworkDiscoverer']/method[@name='directedBroadcast' and count(parameter)=3 and parameter[1][@type='com.zebra.android.discovery.DiscoveryHandler'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='int']]"
		[Register ("directedBroadcast", "(Lcom/zebra/android/discovery/DiscoveryHandler;Ljava/lang/String;I)V", "")]
		public static unsafe void DirectedBroadcast (global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0, string p1, int p2)
		{
			const string __id = "directedBroadcast.(Lcom/zebra/android/discovery/DiscoveryHandler;Ljava/lang/String;I)V";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (p2);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='NetworkDiscoverer']/method[@name='findPrinters' and count(parameter)=1 and parameter[1][@type='com.zebra.android.discovery.DiscoveryHandler']]"
		[Register ("findPrinters", "(Lcom/zebra/android/discovery/DiscoveryHandler;)V", "")]
		public static unsafe void FindPrinters (global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0)
		{
			const string __id = "findPrinters.(Lcom/zebra/android/discovery/DiscoveryHandler;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='NetworkDiscoverer']/method[@name='localBroadcast' and count(parameter)=1 and parameter[1][@type='com.zebra.android.discovery.DiscoveryHandler']]"
		[Register ("localBroadcast", "(Lcom/zebra/android/discovery/DiscoveryHandler;)V", "")]
		public static unsafe void LocalBroadcast (global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0)
		{
			const string __id = "localBroadcast.(Lcom/zebra/android/discovery/DiscoveryHandler;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='NetworkDiscoverer']/method[@name='localBroadcast' and count(parameter)=2 and parameter[1][@type='com.zebra.android.discovery.DiscoveryHandler'] and parameter[2][@type='int']]"
		[Register ("localBroadcast", "(Lcom/zebra/android/discovery/DiscoveryHandler;I)V", "")]
		public static unsafe void LocalBroadcast (global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0, int p1)
		{
			const string __id = "localBroadcast.(Lcom/zebra/android/discovery/DiscoveryHandler;I)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (p1);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='NetworkDiscoverer']/method[@name='multicast' and count(parameter)=2 and parameter[1][@type='com.zebra.android.discovery.DiscoveryHandler'] and parameter[2][@type='int']]"
		[Register ("multicast", "(Lcom/zebra/android/discovery/DiscoveryHandler;I)V", "")]
		public static unsafe void Multicast (global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0, int p1)
		{
			const string __id = "multicast.(Lcom/zebra/android/discovery/DiscoveryHandler;I)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (p1);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='NetworkDiscoverer']/method[@name='multicast' and count(parameter)=3 and parameter[1][@type='com.zebra.android.discovery.DiscoveryHandler'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
		[Register ("multicast", "(Lcom/zebra/android/discovery/DiscoveryHandler;II)V", "")]
		public static unsafe void Multicast (global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0, int p1, int p2)
		{
			const string __id = "multicast.(Lcom/zebra/android/discovery/DiscoveryHandler;II)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (p2);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='NetworkDiscoverer']/method[@name='subnetSearch' and count(parameter)=2 and parameter[1][@type='com.zebra.android.discovery.DiscoveryHandler'] and parameter[2][@type='java.lang.String']]"
		[Register ("subnetSearch", "(Lcom/zebra/android/discovery/DiscoveryHandler;Ljava/lang/String;)V", "")]
		public static unsafe void SubnetSearch (global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0, string p1)
		{
			const string __id = "subnetSearch.(Lcom/zebra/android/discovery/DiscoveryHandler;Ljava/lang/String;)V";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/class[@name='NetworkDiscoverer']/method[@name='subnetSearch' and count(parameter)=3 and parameter[1][@type='com.zebra.android.discovery.DiscoveryHandler'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='int']]"
		[Register ("subnetSearch", "(Lcom/zebra/android/discovery/DiscoveryHandler;Ljava/lang/String;I)V", "")]
		public static unsafe void SubnetSearch (global::Com.Zebra.Android.Discovery.IDiscoveryHandler p0, string p1, int p2)
		{
			const string __id = "subnetSearch.(Lcom/zebra/android/discovery/DiscoveryHandler;Ljava/lang/String;I)V";
			IntPtr native_p1 = JNIEnv.NewString (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				__args [2] = new JniArgumentValue (p2);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

	}
}
