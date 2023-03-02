using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Util.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']"
	[global::Android.Runtime.Register ("com/zebra/android/util/internal/ZPLUtilities", DoNotGenerateAcw=true)]
	public partial class ZPLUtilities : global::Java.Lang.Object {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='PRINTER_CALIBRATE']"
		[Register ("PRINTER_CALIBRATE")]
		public static string PrinterCalibrate {
			get {
				const string __id = "PRINTER_CALIBRATE.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='PRINTER_CONFIG_LABEL']"
		[Register ("PRINTER_CONFIG_LABEL")]
		public static string PrinterConfigLabel {
			get {
				const string __id = "PRINTER_CONFIG_LABEL.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='PRINTER_INFO']"
		[Register ("PRINTER_INFO")]
		public static string PrinterInfo {
			get {
				const string __id = "PRINTER_INFO.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='PRINTER_RESET']"
		[Register ("PRINTER_RESET")]
		public static string PrinterReset {
			get {
				const string __id = "PRINTER_RESET.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='PRINTER_RESTORE_DEFAULTS']"
		[Register ("PRINTER_RESTORE_DEFAULTS")]
		public static string PrinterRestoreDefaults {
			get {
				const string __id = "PRINTER_RESTORE_DEFAULTS.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='PRINTER_STATUS']"
		[Register ("PRINTER_STATUS")]
		public static string PrinterStatus {
			get {
				const string __id = "PRINTER_STATUS.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='ZPL_INTERNAL_COMMAND_PREFIX']"
		[Register ("ZPL_INTERNAL_COMMAND_PREFIX")]
		public static string ZplInternalCommandPrefix {
			get {
				const string __id = "ZPL_INTERNAL_COMMAND_PREFIX.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='ZPL_INTERNAL_COMMAND_PREFIX_CHAR']"
		[Register ("ZPL_INTERNAL_COMMAND_PREFIX_CHAR")]
		public const int ZplInternalCommandPrefixChar = (int) 16;


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='ZPL_INTERNAL_DELIMITER']"
		[Register ("ZPL_INTERNAL_DELIMITER")]
		public static string ZplInternalDelimiter {
			get {
				const string __id = "ZPL_INTERNAL_DELIMITER.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='ZPL_INTERNAL_DELIMITER_CHAR']"
		[Register ("ZPL_INTERNAL_DELIMITER_CHAR")]
		public const int ZplInternalDelimiterChar = (int) 31;


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='ZPL_INTERNAL_FORMAT_PREFIX']"
		[Register ("ZPL_INTERNAL_FORMAT_PREFIX")]
		public static string ZplInternalFormatPrefix {
			get {
				const string __id = "ZPL_INTERNAL_FORMAT_PREFIX.Ljava/lang/String;";

				var __v = _members.StaticFields.GetObjectValue (__id);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
		}

		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/field[@name='ZPL_INTERNAL_FORMAT_PREFIX_CHAR']"
		[Register ("ZPL_INTERNAL_FORMAT_PREFIX_CHAR")]
		public const int ZplInternalFormatPrefixChar = (int) 30;
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/util/internal/ZPLUtilities", typeof (ZPLUtilities));
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

		protected ZPLUtilities (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/constructor[@name='ZPLUtilities' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe ZPLUtilities ()
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

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/method[@name='decorateWithCommandPrefix' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("decorateWithCommandPrefix", "(Ljava/lang/String;)Ljava/lang/String;", "")]
		public static unsafe string DecorateWithCommandPrefix (string p0)
		{
			const string __id = "decorateWithCommandPrefix.(Ljava/lang/String;)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.util.internal']/class[@name='ZPLUtilities']/method[@name='decorateWithFormatPrefix' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("decorateWithFormatPrefix", "(Ljava/lang/String;)Ljava/lang/String;", "")]
		public static unsafe string DecorateWithFormatPrefix (string p0)
		{
			const string __id = "decorateWithFormatPrefix.(Ljava/lang/String;)Ljava/lang/String;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}
}
