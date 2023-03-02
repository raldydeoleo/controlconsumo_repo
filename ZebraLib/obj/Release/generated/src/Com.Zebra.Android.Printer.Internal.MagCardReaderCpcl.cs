using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='MagCardReaderCpcl']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/MagCardReaderCpcl", DoNotGenerateAcw=true)]
	public partial class MagCardReaderCpcl : global::Java.Lang.Object, global::Com.Zebra.Android.Printer.IMagCardReader {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='MagCardReaderCpcl']/field[@name='printerConnection']"
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
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/MagCardReaderCpcl", typeof (MagCardReaderCpcl));
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

		protected MagCardReaderCpcl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='MagCardReaderCpcl']/constructor[@name='MagCardReaderCpcl' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe MagCardReaderCpcl (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
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

		static Delegate cb_read_I;
#pragma warning disable 0169
		static Delegate GetRead_IHandler ()
		{
			if (cb_read_I == null)
				cb_read_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, IntPtr>) n_Read_I);
			return cb_read_I;
		}

		static IntPtr n_Read_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::Com.Zebra.Android.Printer.Internal.MagCardReaderCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.MagCardReaderCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.Read (p0));
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='MagCardReaderCpcl']/method[@name='read' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("read", "(I)[Ljava/lang/String;", "GetRead_IHandler")]
		public virtual unsafe string[] Read (int p0)
		{
			const string __id = "read.(I)[Ljava/lang/String;";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return (string[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (string));
			} finally {
			}
		}

	}
}
