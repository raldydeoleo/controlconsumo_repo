using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilZpl']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/FileUtilZpl", DoNotGenerateAcw=true)]
	public partial class FileUtilZpl : global::Com.Zebra.Android.Printer.Internal.FileUtilA {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/FileUtilZpl", typeof (FileUtilZpl));
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

		protected FileUtilZpl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilZpl']/constructor[@name='FileUtilZpl' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe FileUtilZpl (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V";

			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
				SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
				_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
			} finally {
			}
		}

		static Delegate cb_extractFilePropertiesFromDirResult_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetExtractFilePropertiesFromDirResult_Ljava_lang_String_Handler ()
		{
			if (cb_extractFilePropertiesFromDirResult_Ljava_lang_String_ == null)
				cb_extractFilePropertiesFromDirResult_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr>) n_ExtractFilePropertiesFromDirResult_Ljava_lang_String_);
			return cb_extractFilePropertiesFromDirResult_Ljava_lang_String_;
		}

		static IntPtr n_ExtractFilePropertiesFromDirResult_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.Internal.FileUtilZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FileUtilZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.ExtractFilePropertiesFromDirResult (p0));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilZpl']/method[@name='extractFilePropertiesFromDirResult' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("extractFilePropertiesFromDirResult", "(Ljava/lang/String;)Lcom/zebra/android/printer/internal/PrinterFilePropertiesList;", "GetExtractFilePropertiesFromDirResult_Ljava_lang_String_Handler")]
		public override unsafe global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList ExtractFilePropertiesFromDirResult (string p0)
		{
			const string __id = "extractFilePropertiesFromDirResult.(Ljava/lang/String;)Lcom/zebra/android/printer/internal/PrinterFilePropertiesList;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}
}
