using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Discovery {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.discovery']/interface[@name='DiscoveryHandler']"
	[Register ("com/zebra/android/discovery/DiscoveryHandler", "", "Com.Zebra.Android.Discovery.IDiscoveryHandlerInvoker")]
	public partial interface IDiscoveryHandler : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/interface[@name='DiscoveryHandler']/method[@name='discoveryError' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("discoveryError", "(Ljava/lang/String;)V", "GetDiscoveryError_Ljava_lang_String_Handler:Com.Zebra.Android.Discovery.IDiscoveryHandlerInvoker, ZebraLib")]
		void DiscoveryError (string p0);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/interface[@name='DiscoveryHandler']/method[@name='discoveryFinished' and count(parameter)=0]"
		[Register ("discoveryFinished", "()V", "GetDiscoveryFinishedHandler:Com.Zebra.Android.Discovery.IDiscoveryHandlerInvoker, ZebraLib")]
		void DiscoveryFinished ();

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.discovery']/interface[@name='DiscoveryHandler']/method[@name='foundPrinter' and count(parameter)=1 and parameter[1][@type='com.zebra.android.discovery.DiscoveredPrinter']]"
		[Register ("foundPrinter", "(Lcom/zebra/android/discovery/DiscoveredPrinter;)V", "GetFoundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_Handler:Com.Zebra.Android.Discovery.IDiscoveryHandlerInvoker, ZebraLib")]
		void FoundPrinter (global::Com.Zebra.Android.Discovery.DiscoveredPrinter p0);

	}

	[global::Android.Runtime.Register ("com/zebra/android/discovery/DiscoveryHandler", DoNotGenerateAcw=true)]
	internal class IDiscoveryHandlerInvoker : global::Java.Lang.Object, IDiscoveryHandler {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/discovery/DiscoveryHandler", typeof (IDiscoveryHandlerInvoker));

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

		public static IDiscoveryHandler GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IDiscoveryHandler> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.discovery.DiscoveryHandler"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IDiscoveryHandlerInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_discoveryError_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetDiscoveryError_Ljava_lang_String_Handler ()
		{
			if (cb_discoveryError_Ljava_lang_String_ == null)
				cb_discoveryError_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_DiscoveryError_Ljava_lang_String_);
			return cb_discoveryError_Ljava_lang_String_;
		}

		static void n_DiscoveryError_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Discovery.IDiscoveryHandler __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.IDiscoveryHandler> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.DiscoveryError (p0);
		}
#pragma warning restore 0169

		IntPtr id_discoveryError_Ljava_lang_String_;
		public unsafe void DiscoveryError (string p0)
		{
			if (id_discoveryError_Ljava_lang_String_ == IntPtr.Zero)
				id_discoveryError_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "discoveryError", "(Ljava/lang/String;)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (native_p0);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_discoveryError_Ljava_lang_String_, __args);
			JNIEnv.DeleteLocalRef (native_p0);
		}

		static Delegate cb_discoveryFinished;
#pragma warning disable 0169
		static Delegate GetDiscoveryFinishedHandler ()
		{
			if (cb_discoveryFinished == null)
				cb_discoveryFinished = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_DiscoveryFinished);
			return cb_discoveryFinished;
		}

		static void n_DiscoveryFinished (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Discovery.IDiscoveryHandler __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.IDiscoveryHandler> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.DiscoveryFinished ();
		}
#pragma warning restore 0169

		IntPtr id_discoveryFinished;
		public unsafe void DiscoveryFinished ()
		{
			if (id_discoveryFinished == IntPtr.Zero)
				id_discoveryFinished = JNIEnv.GetMethodID (class_ref, "discoveryFinished", "()V");
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_discoveryFinished);
		}

		static Delegate cb_foundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_;
#pragma warning disable 0169
		static Delegate GetFoundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_Handler ()
		{
			if (cb_foundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_ == null)
				cb_foundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_FoundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_);
			return cb_foundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_;
		}

		static void n_FoundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Discovery.IDiscoveryHandler __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.IDiscoveryHandler> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Discovery.DiscoveredPrinter p0 = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Discovery.DiscoveredPrinter> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.FoundPrinter (p0);
		}
#pragma warning restore 0169

		IntPtr id_foundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_;
		public unsafe void FoundPrinter (global::Com.Zebra.Android.Discovery.DiscoveredPrinter p0)
		{
			if (id_foundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_ == IntPtr.Zero)
				id_foundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_ = JNIEnv.GetMethodID (class_ref, "foundPrinter", "(Lcom/zebra/android/discovery/DiscoveredPrinter;)V");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_foundPrinter_Lcom_zebra_android_discovery_DiscoveredPrinter_, __args);
		}

	}

}
