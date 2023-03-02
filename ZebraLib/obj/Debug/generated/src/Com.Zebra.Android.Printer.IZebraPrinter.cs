using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ZebraPrinter']"
	[Register ("com/zebra/android/printer/ZebraPrinter", "", "Com.Zebra.Android.Printer.IZebraPrinterInvoker")]
	public partial interface IZebraPrinter : IJavaObject {

		global::Com.Zebra.Android.Printer.PrinterStatus CurrentStatus {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ZebraPrinter']/method[@name='getCurrentStatus' and count(parameter)=0]"
			[Register ("getCurrentStatus", "()Lcom/zebra/android/printer/PrinterStatus;", "GetGetCurrentStatusHandler:Com.Zebra.Android.Printer.IZebraPrinterInvoker, ZebraLib")] get;
		}

		global::Com.Zebra.Android.Printer.IFileUtil FileUtil {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ZebraPrinter']/method[@name='getFileUtil' and count(parameter)=0]"
			[Register ("getFileUtil", "()Lcom/zebra/android/printer/FileUtil;", "GetGetFileUtilHandler:Com.Zebra.Android.Printer.IZebraPrinterInvoker, ZebraLib")] get;
		}

		global::Com.Zebra.Android.Printer.IFormatUtil FormatUtil {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ZebraPrinter']/method[@name='getFormatUtil' and count(parameter)=0]"
			[Register ("getFormatUtil", "()Lcom/zebra/android/printer/FormatUtil;", "GetGetFormatUtilHandler:Com.Zebra.Android.Printer.IZebraPrinterInvoker, ZebraLib")] get;
		}

		global::Com.Zebra.Android.Printer.IGraphicsUtil GraphicsUtil {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ZebraPrinter']/method[@name='getGraphicsUtil' and count(parameter)=0]"
			[Register ("getGraphicsUtil", "()Lcom/zebra/android/printer/GraphicsUtil;", "GetGetGraphicsUtilHandler:Com.Zebra.Android.Printer.IZebraPrinterInvoker, ZebraLib")] get;
		}

		global::Com.Zebra.Android.Printer.IMagCardReader MagCardReader {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ZebraPrinter']/method[@name='getMagCardReader' and count(parameter)=0]"
			[Register ("getMagCardReader", "()Lcom/zebra/android/printer/MagCardReader;", "GetGetMagCardReaderHandler:Com.Zebra.Android.Printer.IZebraPrinterInvoker, ZebraLib")] get;
		}

		global::Com.Zebra.Android.Printer.PrinterLanguage PrinterControlLanguage {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ZebraPrinter']/method[@name='getPrinterControlLanguage' and count(parameter)=0]"
			[Register ("getPrinterControlLanguage", "()Lcom/zebra/android/printer/PrinterLanguage;", "GetGetPrinterControlLanguageHandler:Com.Zebra.Android.Printer.IZebraPrinterInvoker, ZebraLib")] get;
		}

		global::Com.Zebra.Android.Printer.ISmartcardReader SmartcardReader {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ZebraPrinter']/method[@name='getSmartcardReader' and count(parameter)=0]"
			[Register ("getSmartcardReader", "()Lcom/zebra/android/printer/SmartcardReader;", "GetGetSmartcardReaderHandler:Com.Zebra.Android.Printer.IZebraPrinterInvoker, ZebraLib")] get;
		}

		global::Com.Zebra.Android.Printer.IToolsUtil ToolsUtil {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='ZebraPrinter']/method[@name='getToolsUtil' and count(parameter)=0]"
			[Register ("getToolsUtil", "()Lcom/zebra/android/printer/ToolsUtil;", "GetGetToolsUtilHandler:Com.Zebra.Android.Printer.IZebraPrinterInvoker, ZebraLib")] get;
		}

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/ZebraPrinter", DoNotGenerateAcw=true)]
	internal class IZebraPrinterInvoker : global::Java.Lang.Object, IZebraPrinter {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/ZebraPrinter", typeof (IZebraPrinterInvoker));

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

		public static IZebraPrinter GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IZebraPrinter> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.printer.ZebraPrinter"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IZebraPrinterInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_getCurrentStatus;
#pragma warning disable 0169
		static Delegate GetGetCurrentStatusHandler ()
		{
			if (cb_getCurrentStatus == null)
				cb_getCurrentStatus = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetCurrentStatus);
			return cb_getCurrentStatus;
		}

		static IntPtr n_GetCurrentStatus (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.IZebraPrinter __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.CurrentStatus);
		}
#pragma warning restore 0169

		IntPtr id_getCurrentStatus;
		public unsafe global::Com.Zebra.Android.Printer.PrinterStatus CurrentStatus {
			get {
				if (id_getCurrentStatus == IntPtr.Zero)
					id_getCurrentStatus = JNIEnv.GetMethodID (class_ref, "getCurrentStatus", "()Lcom/zebra/android/printer/PrinterStatus;");
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.PrinterStatus> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getCurrentStatus), JniHandleOwnership.TransferLocalRef);
			}
		}

		static Delegate cb_getFileUtil;
#pragma warning disable 0169
		static Delegate GetGetFileUtilHandler ()
		{
			if (cb_getFileUtil == null)
				cb_getFileUtil = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetFileUtil);
			return cb_getFileUtil;
		}

		static IntPtr n_GetFileUtil (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.IZebraPrinter __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.FileUtil);
		}
#pragma warning restore 0169

		IntPtr id_getFileUtil;
		public unsafe global::Com.Zebra.Android.Printer.IFileUtil FileUtil {
			get {
				if (id_getFileUtil == IntPtr.Zero)
					id_getFileUtil = JNIEnv.GetMethodID (class_ref, "getFileUtil", "()Lcom/zebra/android/printer/FileUtil;");
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFileUtil> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getFileUtil), JniHandleOwnership.TransferLocalRef);
			}
		}

		static Delegate cb_getFormatUtil;
#pragma warning disable 0169
		static Delegate GetGetFormatUtilHandler ()
		{
			if (cb_getFormatUtil == null)
				cb_getFormatUtil = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetFormatUtil);
			return cb_getFormatUtil;
		}

		static IntPtr n_GetFormatUtil (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.IZebraPrinter __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.FormatUtil);
		}
#pragma warning restore 0169

		IntPtr id_getFormatUtil;
		public unsafe global::Com.Zebra.Android.Printer.IFormatUtil FormatUtil {
			get {
				if (id_getFormatUtil == IntPtr.Zero)
					id_getFormatUtil = JNIEnv.GetMethodID (class_ref, "getFormatUtil", "()Lcom/zebra/android/printer/FormatUtil;");
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFormatUtil> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getFormatUtil), JniHandleOwnership.TransferLocalRef);
			}
		}

		static Delegate cb_getGraphicsUtil;
#pragma warning disable 0169
		static Delegate GetGetGraphicsUtilHandler ()
		{
			if (cb_getGraphicsUtil == null)
				cb_getGraphicsUtil = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetGraphicsUtil);
			return cb_getGraphicsUtil;
		}

		static IntPtr n_GetGraphicsUtil (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.IZebraPrinter __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.GraphicsUtil);
		}
#pragma warning restore 0169

		IntPtr id_getGraphicsUtil;
		public unsafe global::Com.Zebra.Android.Printer.IGraphicsUtil GraphicsUtil {
			get {
				if (id_getGraphicsUtil == IntPtr.Zero)
					id_getGraphicsUtil = JNIEnv.GetMethodID (class_ref, "getGraphicsUtil", "()Lcom/zebra/android/printer/GraphicsUtil;");
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IGraphicsUtil> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getGraphicsUtil), JniHandleOwnership.TransferLocalRef);
			}
		}

		static Delegate cb_getMagCardReader;
#pragma warning disable 0169
		static Delegate GetGetMagCardReaderHandler ()
		{
			if (cb_getMagCardReader == null)
				cb_getMagCardReader = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetMagCardReader);
			return cb_getMagCardReader;
		}

		static IntPtr n_GetMagCardReader (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.IZebraPrinter __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.MagCardReader);
		}
#pragma warning restore 0169

		IntPtr id_getMagCardReader;
		public unsafe global::Com.Zebra.Android.Printer.IMagCardReader MagCardReader {
			get {
				if (id_getMagCardReader == IntPtr.Zero)
					id_getMagCardReader = JNIEnv.GetMethodID (class_ref, "getMagCardReader", "()Lcom/zebra/android/printer/MagCardReader;");
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IMagCardReader> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getMagCardReader), JniHandleOwnership.TransferLocalRef);
			}
		}

		static Delegate cb_getPrinterControlLanguage;
#pragma warning disable 0169
		static Delegate GetGetPrinterControlLanguageHandler ()
		{
			if (cb_getPrinterControlLanguage == null)
				cb_getPrinterControlLanguage = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetPrinterControlLanguage);
			return cb_getPrinterControlLanguage;
		}

		static IntPtr n_GetPrinterControlLanguage (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.IZebraPrinter __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.PrinterControlLanguage);
		}
#pragma warning restore 0169

		IntPtr id_getPrinterControlLanguage;
		public unsafe global::Com.Zebra.Android.Printer.PrinterLanguage PrinterControlLanguage {
			get {
				if (id_getPrinterControlLanguage == IntPtr.Zero)
					id_getPrinterControlLanguage = JNIEnv.GetMethodID (class_ref, "getPrinterControlLanguage", "()Lcom/zebra/android/printer/PrinterLanguage;");
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.PrinterLanguage> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getPrinterControlLanguage), JniHandleOwnership.TransferLocalRef);
			}
		}

		static Delegate cb_getSmartcardReader;
#pragma warning disable 0169
		static Delegate GetGetSmartcardReaderHandler ()
		{
			if (cb_getSmartcardReader == null)
				cb_getSmartcardReader = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetSmartcardReader);
			return cb_getSmartcardReader;
		}

		static IntPtr n_GetSmartcardReader (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.IZebraPrinter __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.SmartcardReader);
		}
#pragma warning restore 0169

		IntPtr id_getSmartcardReader;
		public unsafe global::Com.Zebra.Android.Printer.ISmartcardReader SmartcardReader {
			get {
				if (id_getSmartcardReader == IntPtr.Zero)
					id_getSmartcardReader = JNIEnv.GetMethodID (class_ref, "getSmartcardReader", "()Lcom/zebra/android/printer/SmartcardReader;");
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.ISmartcardReader> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getSmartcardReader), JniHandleOwnership.TransferLocalRef);
			}
		}

		static Delegate cb_getToolsUtil;
#pragma warning disable 0169
		static Delegate GetGetToolsUtilHandler ()
		{
			if (cb_getToolsUtil == null)
				cb_getToolsUtil = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetToolsUtil);
			return cb_getToolsUtil;
		}

		static IntPtr n_GetToolsUtil (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.IZebraPrinter __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IZebraPrinter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.ToolsUtil);
		}
#pragma warning restore 0169

		IntPtr id_getToolsUtil;
		public unsafe global::Com.Zebra.Android.Printer.IToolsUtil ToolsUtil {
			get {
				if (id_getToolsUtil == IntPtr.Zero)
					id_getToolsUtil = JNIEnv.GetMethodID (class_ref, "getToolsUtil", "()Lcom/zebra/android/printer/ToolsUtil;");
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IToolsUtil> (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getToolsUtil), JniHandleOwnership.TransferLocalRef);
			}
		}

	}

}
