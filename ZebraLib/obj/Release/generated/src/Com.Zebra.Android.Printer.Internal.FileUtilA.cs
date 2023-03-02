using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilA']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/FileUtilA", DoNotGenerateAcw=true)]
	public abstract partial class FileUtilA : global::Java.Lang.Object, global::Com.Zebra.Android.Printer.IFileUtil {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilA']/field[@name='printerConnection']"
		[Register ("printerConnection")]
		protected global::Com.Zebra.Android.Comm.IZebraPrinterConnection PrinterConnection {
			get {
				const string __id = "printerConnection.Lcom/zebra/android/comm/ZebraPrinterConnection;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Comm.IZebraPrinterConnection> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "printerConnection.Lcom/zebra/android/comm/ZebraPrinterConnection;";

				IntPtr native_value = global::Android.Runtime.JNIEnv.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					global::Android.Runtime.JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/FileUtilA", typeof (FileUtilA));
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

		protected FileUtilA (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilA']/constructor[@name='FileUtilA' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe FileUtilA (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
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
			global::Com.Zebra.Android.Printer.Internal.FileUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FileUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.ExtractFilePropertiesFromDirResult (p0));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilA']/method[@name='extractFilePropertiesFromDirResult' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("extractFilePropertiesFromDirResult", "(Ljava/lang/String;)Lcom/zebra/android/printer/internal/PrinterFilePropertiesList;", "GetExtractFilePropertiesFromDirResult_Ljava_lang_String_Handler")]
		public abstract global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList ExtractFilePropertiesFromDirResult (string p0);

		static Delegate cb_getFileConnection_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetGetFileConnection_Ljava_lang_String_Handler ()
		{
			if (cb_getFileConnection_Ljava_lang_String_ == null)
				cb_getFileConnection_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr>) n_GetFileConnection_Ljava_lang_String_);
			return cb_getFileConnection_Ljava_lang_String_;
		}

		static IntPtr n_GetFileConnection_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.Internal.FileUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FileUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.GetFileConnection (p0));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilA']/method[@name='getFileConnection' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("getFileConnection", "(Ljava/lang/String;)Lcom/zebra/android/printer/internal/ZebraFileConnection;", "GetGetFileConnection_Ljava_lang_String_Handler")]
		protected virtual unsafe global::Com.Zebra.Android.Printer.Internal.IZebraFileConnection GetFileConnection (string p0)
		{
			const string __id = "getFileConnection.(Ljava/lang/String;)Lcom/zebra/android/printer/internal/ZebraFileConnection;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.IZebraFileConnection> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static Delegate cb_retrieveFileNames;
#pragma warning disable 0169
		static Delegate GetRetrieveFileNamesHandler ()
		{
			if (cb_retrieveFileNames == null)
				cb_retrieveFileNames = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_RetrieveFileNames);
			return cb_retrieveFileNames;
		}

		static IntPtr n_RetrieveFileNames (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.FileUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FileUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.RetrieveFileNames ());
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilA']/method[@name='retrieveFileNames' and count(parameter)=0]"
		[Register ("retrieveFileNames", "()[Ljava/lang/String;", "GetRetrieveFileNamesHandler")]
		public virtual unsafe string[] RetrieveFileNames ()
		{
			const string __id = "retrieveFileNames.()[Ljava/lang/String;";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
				return (string[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (string));
			} finally {
			}
		}

		static Delegate cb_retrieveFileNames_arrayLjava_lang_String_;
#pragma warning disable 0169
		static Delegate GetRetrieveFileNames_arrayLjava_lang_String_Handler ()
		{
			if (cb_retrieveFileNames_arrayLjava_lang_String_ == null)
				cb_retrieveFileNames_arrayLjava_lang_String_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr>) n_RetrieveFileNames_arrayLjava_lang_String_);
			return cb_retrieveFileNames_arrayLjava_lang_String_;
		}

		static IntPtr n_RetrieveFileNames_arrayLjava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.Internal.FileUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FileUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string[] p0 = (string[]) JNIEnv.GetArray (native_p0, JniHandleOwnership.DoNotTransfer, typeof (string));
			IntPtr __ret = JNIEnv.NewArray (__this.RetrieveFileNames (p0));
			if (p0 != null)
				JNIEnv.CopyArray (p0, native_p0);
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilA']/method[@name='retrieveFileNames' and count(parameter)=1 and parameter[1][@type='java.lang.String[]']]"
		[Register ("retrieveFileNames", "([Ljava/lang/String;)[Ljava/lang/String;", "GetRetrieveFileNames_arrayLjava_lang_String_Handler")]
		public virtual unsafe string[] RetrieveFileNames (string[] p0)
		{
			const string __id = "retrieveFileNames.([Ljava/lang/String;)[Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return (string[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (string));
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

		static Delegate cb_sendFileContents_Ljava_lang_String_;
#pragma warning disable 0169
		static Delegate GetSendFileContents_Ljava_lang_String_Handler ()
		{
			if (cb_sendFileContents_Ljava_lang_String_ == null)
				cb_sendFileContents_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_SendFileContents_Ljava_lang_String_);
			return cb_sendFileContents_Ljava_lang_String_;
		}

		static void n_SendFileContents_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.Internal.FileUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.FileUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.SendFileContents (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilA']/method[@name='sendFileContents' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("sendFileContents", "(Ljava/lang/String;)V", "GetSendFileContents_Ljava_lang_String_Handler")]
		public virtual unsafe void SendFileContents (string p0)
		{
			const string __id = "sendFileContents.(Ljava/lang/String;)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/FileUtilA", DoNotGenerateAcw=true)]
	internal partial class FileUtilAInvoker : FileUtilA {

		public FileUtilAInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer) {}

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/internal/FileUtilA", typeof (FileUtilAInvoker));

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='FileUtilA']/method[@name='extractFilePropertiesFromDirResult' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("extractFilePropertiesFromDirResult", "(Ljava/lang/String;)Lcom/zebra/android/printer/internal/PrinterFilePropertiesList;", "GetExtractFilePropertiesFromDirResult_Ljava_lang_String_Handler")]
		public override unsafe global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList ExtractFilePropertiesFromDirResult (string p0)
		{
			const string __id = "extractFilePropertiesFromDirResult.(Ljava/lang/String;)Lcom/zebra/android/printer/internal/PrinterFilePropertiesList;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.InstanceMethods.InvokeAbstractObjectMethod (__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}

}
