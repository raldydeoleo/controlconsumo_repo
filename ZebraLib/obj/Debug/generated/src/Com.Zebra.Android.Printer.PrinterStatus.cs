using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/PrinterStatus", DoNotGenerateAcw=true)]
	public abstract partial class PrinterStatus : global::Java.Lang.Object {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='isHeadCold']"
		[Register ("isHeadCold")]
		public bool IsHeadCold {
			get {
				const string __id = "isHeadCold.Z";

				var __v = _members.InstanceFields.GetBooleanValue (__id, this);
				return __v;
			}
			set {
				const string __id = "isHeadCold.Z";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='isHeadOpen']"
		[Register ("isHeadOpen")]
		public bool IsHeadOpen {
			get {
				const string __id = "isHeadOpen.Z";

				var __v = _members.InstanceFields.GetBooleanValue (__id, this);
				return __v;
			}
			set {
				const string __id = "isHeadOpen.Z";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='isHeadTooHot']"
		[Register ("isHeadTooHot")]
		public bool IsHeadTooHot {
			get {
				const string __id = "isHeadTooHot.Z";

				var __v = _members.InstanceFields.GetBooleanValue (__id, this);
				return __v;
			}
			set {
				const string __id = "isHeadTooHot.Z";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='isPaperOut']"
		[Register ("isPaperOut")]
		public bool IsPaperOut {
			get {
				const string __id = "isPaperOut.Z";

				var __v = _members.InstanceFields.GetBooleanValue (__id, this);
				return __v;
			}
			set {
				const string __id = "isPaperOut.Z";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='isPartialFormatInProgress']"
		[Register ("isPartialFormatInProgress")]
		public bool IsPartialFormatInProgress {
			get {
				const string __id = "isPartialFormatInProgress.Z";

				var __v = _members.InstanceFields.GetBooleanValue (__id, this);
				return __v;
			}
			set {
				const string __id = "isPartialFormatInProgress.Z";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='isPaused']"
		[Register ("isPaused")]
		public bool IsPaused {
			get {
				const string __id = "isPaused.Z";

				var __v = _members.InstanceFields.GetBooleanValue (__id, this);
				return __v;
			}
			set {
				const string __id = "isPaused.Z";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='isReadyToPrint']"
		[Register ("isReadyToPrint")]
		public bool IsReadyToPrint {
			get {
				const string __id = "isReadyToPrint.Z";

				var __v = _members.InstanceFields.GetBooleanValue (__id, this);
				return __v;
			}
			set {
				const string __id = "isReadyToPrint.Z";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='isReceiveBufferFull']"
		[Register ("isReceiveBufferFull")]
		public bool IsReceiveBufferFull {
			get {
				const string __id = "isReceiveBufferFull.Z";

				var __v = _members.InstanceFields.GetBooleanValue (__id, this);
				return __v;
			}
			set {
				const string __id = "isReceiveBufferFull.Z";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='isRibbonOut']"
		[Register ("isRibbonOut")]
		public bool IsRibbonOut {
			get {
				const string __id = "isRibbonOut.Z";

				var __v = _members.InstanceFields.GetBooleanValue (__id, this);
				return __v;
			}
			set {
				const string __id = "isRibbonOut.Z";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='labelLengthInDots']"
		[Register ("labelLengthInDots")]
		public int LabelLengthInDots {
			get {
				const string __id = "labelLengthInDots.I";

				var __v = _members.InstanceFields.GetInt32Value (__id, this);
				return __v;
			}
			set {
				const string __id = "labelLengthInDots.I";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='labelsRemainingInBatch']"
		[Register ("labelsRemainingInBatch")]
		public int LabelsRemainingInBatch {
			get {
				const string __id = "labelsRemainingInBatch.I";

				var __v = _members.InstanceFields.GetInt32Value (__id, this);
				return __v;
			}
			set {
				const string __id = "labelsRemainingInBatch.I";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='numberOfFormatsInReceiveBuffer']"
		[Register ("numberOfFormatsInReceiveBuffer")]
		public int NumberOfFormatsInReceiveBuffer {
			get {
				const string __id = "numberOfFormatsInReceiveBuffer.I";

				var __v = _members.InstanceFields.GetInt32Value (__id, this);
				return __v;
			}
			set {
				const string __id = "numberOfFormatsInReceiveBuffer.I";

				try {
					_members.InstanceFields.SetValue (__id, this, value);
				} finally {
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='printMode']"
		[Register ("printMode")]
		public global::Com.Zebra.Android.Printer.ZplPrintMode PrintMode {
			get {
				const string __id = "printMode.Lcom/zebra/android/printer/ZplPrintMode;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.ZplPrintMode> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "printMode.Lcom/zebra/android/printer/ZplPrintMode;";

				IntPtr native_value = global::Android.Runtime.JNIEnv.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					global::Android.Runtime.JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/field[@name='printerConnection']"
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
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/PrinterStatus", typeof (PrinterStatus));
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

		protected PrinterStatus (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/constructor[@name='PrinterStatus' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe PrinterStatus (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
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

		static Delegate cb_updateStatus;
#pragma warning disable 0169
		static Delegate GetUpdateStatusHandler ()
		{
			if (cb_updateStatus == null)
				cb_updateStatus = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr>) n_UpdateStatus);
			return cb_updateStatus;
		}

		static void n_UpdateStatus (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.PrinterStatus __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.PrinterStatus> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.UpdateStatus ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/method[@name='updateStatus' and count(parameter)=0]"
		[Register ("updateStatus", "()V", "GetUpdateStatusHandler")]
		protected abstract void UpdateStatus ();

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/PrinterStatus", DoNotGenerateAcw=true)]
	internal partial class PrinterStatusInvoker : PrinterStatus {

		public PrinterStatusInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer) {}

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/PrinterStatus", typeof (PrinterStatusInvoker));

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/class[@name='PrinterStatus']/method[@name='updateStatus' and count(parameter)=0]"
		[Register ("updateStatus", "()V", "GetUpdateStatusHandler")]
		protected override unsafe void UpdateStatus ()
		{
			const string __id = "updateStatus.()V";
			try {
				_members.InstanceMethods.InvokeAbstractVoidMethod (__id, this, null);
			} finally {
			}
		}

	}

}
