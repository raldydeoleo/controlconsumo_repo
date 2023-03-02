using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraPrinterZpl']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/ZebraPrinterZpl", DoNotGenerateAcw=true)]
	public partial class ZebraPrinterZpl : global::Com.Zebra.Android.Printer.Internal.ZebraPrinterA {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/ZebraPrinterZpl", typeof (ZebraPrinterZpl));
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

		protected ZebraPrinterZpl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraPrinterZpl']/constructor[@name='ZebraPrinterZpl' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe ZebraPrinterZpl (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
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
			global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.CurrentStatus);
		}
#pragma warning restore 0169

		public override unsafe global::Com.Zebra.Android.Printer.PrinterStatus CurrentStatus {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraPrinterZpl']/method[@name='getCurrentStatus' and count(parameter)=0]"
			[Register ("getCurrentStatus", "()Lcom/zebra/android/printer/PrinterStatus;", "GetGetCurrentStatusHandler")]
			get {
				const string __id = "getCurrentStatus.()Lcom/zebra/android/printer/PrinterStatus;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.PrinterStatus> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
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
			global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.FileUtil);
		}
#pragma warning restore 0169

		public override unsafe global::Com.Zebra.Android.Printer.IFileUtil FileUtil {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraPrinterZpl']/method[@name='getFileUtil' and count(parameter)=0]"
			[Register ("getFileUtil", "()Lcom/zebra/android/printer/FileUtil;", "GetGetFileUtilHandler")]
			get {
				const string __id = "getFileUtil.()Lcom/zebra/android/printer/FileUtil;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFileUtil> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
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
			global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.FormatUtil);
		}
#pragma warning restore 0169

		public override unsafe global::Com.Zebra.Android.Printer.IFormatUtil FormatUtil {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraPrinterZpl']/method[@name='getFormatUtil' and count(parameter)=0]"
			[Register ("getFormatUtil", "()Lcom/zebra/android/printer/FormatUtil;", "GetGetFormatUtilHandler")]
			get {
				const string __id = "getFormatUtil.()Lcom/zebra/android/printer/FormatUtil;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IFormatUtil> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
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
			global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.GraphicsUtil);
		}
#pragma warning restore 0169

		public override unsafe global::Com.Zebra.Android.Printer.IGraphicsUtil GraphicsUtil {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraPrinterZpl']/method[@name='getGraphicsUtil' and count(parameter)=0]"
			[Register ("getGraphicsUtil", "()Lcom/zebra/android/printer/GraphicsUtil;", "GetGetGraphicsUtilHandler")]
			get {
				const string __id = "getGraphicsUtil.()Lcom/zebra/android/printer/GraphicsUtil;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IGraphicsUtil> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
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
			global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.MagCardReader);
		}
#pragma warning restore 0169

		public override unsafe global::Com.Zebra.Android.Printer.IMagCardReader MagCardReader {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraPrinterZpl']/method[@name='getMagCardReader' and count(parameter)=0]"
			[Register ("getMagCardReader", "()Lcom/zebra/android/printer/MagCardReader;", "GetGetMagCardReaderHandler")]
			get {
				const string __id = "getMagCardReader.()Lcom/zebra/android/printer/MagCardReader;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IMagCardReader> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
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
			global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.PrinterControlLanguage);
		}
#pragma warning restore 0169

		public override unsafe global::Com.Zebra.Android.Printer.PrinterLanguage PrinterControlLanguage {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraPrinterZpl']/method[@name='getPrinterControlLanguage' and count(parameter)=0]"
			[Register ("getPrinterControlLanguage", "()Lcom/zebra/android/printer/PrinterLanguage;", "GetGetPrinterControlLanguageHandler")]
			get {
				const string __id = "getPrinterControlLanguage.()Lcom/zebra/android/printer/PrinterLanguage;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.PrinterLanguage> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
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
			global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.SmartcardReader);
		}
#pragma warning restore 0169

		public override unsafe global::Com.Zebra.Android.Printer.ISmartcardReader SmartcardReader {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraPrinterZpl']/method[@name='getSmartcardReader' and count(parameter)=0]"
			[Register ("getSmartcardReader", "()Lcom/zebra/android/printer/SmartcardReader;", "GetGetSmartcardReaderHandler")]
			get {
				const string __id = "getSmartcardReader.()Lcom/zebra/android/printer/SmartcardReader;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.ISmartcardReader> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
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
			global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.ZebraPrinterZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.ToolsUtil);
		}
#pragma warning restore 0169

		public override unsafe global::Com.Zebra.Android.Printer.IToolsUtil ToolsUtil {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='ZebraPrinterZpl']/method[@name='getToolsUtil' and count(parameter)=0]"
			[Register ("getToolsUtil", "()Lcom/zebra/android/printer/ToolsUtil;", "GetGetToolsUtilHandler")]
			get {
				const string __id = "getToolsUtil.()Lcom/zebra/android/printer/ToolsUtil;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IToolsUtil> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

	}
}
