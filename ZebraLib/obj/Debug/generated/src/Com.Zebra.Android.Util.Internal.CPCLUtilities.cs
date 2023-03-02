using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Util.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='CPCLUtilities']"
	[global::Android.Runtime.Register ("com/zebra/android/util/internal/CPCLUtilities", DoNotGenerateAcw=true)]
	public partial class CPCLUtilities : global::Java.Lang.Object {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='CPCLUtilities']/field[@name='PRINTER_CONFIG_LABEL']"
		[Register ("PRINTER_CONFIG_LABEL")]
		public static string PrinterConfigLabel {
			get {
				const string __id = "PRINTER_CONFIG_LABEL.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='CPCLUtilities']/field[@name='PRINTER_FORM_FEED']"
		[Register ("PRINTER_FORM_FEED")]
		public static string PrinterFormFeed {
			get {
				const string __id = "PRINTER_FORM_FEED.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='CPCLUtilities']/field[@name='PRINTER_STATUS']"
		[Register ("PRINTER_STATUS")]
		public static string PrinterStatus {
			get {
				const string __id = "PRINTER_STATUS.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='CPCLUtilities']/field[@name='VERSION_PREFIXES']"
		[Register ("VERSION_PREFIXES")]
		public static IList<string> VersionPrefixes {
			get {
				const string __id = "VERSION_PREFIXES.[Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return global::Android.Runtime.JavaArray<string>.FromJniHandle (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/util/internal/CPCLUtilities", typeof (CPCLUtilities));
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

		protected CPCLUtilities (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='CPCLUtilities']/constructor[@name='CPCLUtilities' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe CPCLUtilities ()
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

	}
}
