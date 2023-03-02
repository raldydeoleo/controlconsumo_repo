using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFileProperties']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/PrinterFileProperties", DoNotGenerateAcw=true)]
	public abstract partial class PrinterFileProperties : global::Java.Lang.Object {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFileProperties']/field[@name='drivePrefix']"
		[Register ("drivePrefix")]
		protected string DrivePrefix {
			get {
				const string __id = "drivePrefix.Ljava/lang/String;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "drivePrefix.Ljava/lang/String;";

				IntPtr native_value = JNIEnv.NewString (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFileProperties']/field[@name='extension']"
		[Register ("extension")]
		protected string Extension {
			get {
				const string __id = "extension.Ljava/lang/String;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "extension.Ljava/lang/String;";

				IntPtr native_value = JNIEnv.NewString (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFileProperties']/field[@name='fileName']"
		[Register ("fileName")]
		protected string FileName {
			get {
				const string __id = "fileName.Ljava/lang/String;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return JNIEnv.GetString (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "fileName.Ljava/lang/String;";

				IntPtr native_value = JNIEnv.NewString (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/PrinterFileProperties", typeof (PrinterFileProperties));
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

		protected PrinterFileProperties (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFileProperties']/constructor[@name='PrinterFileProperties' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe PrinterFileProperties ()
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

		static Delegate cb_getExt;
#pragma warning disable 0169
		static Delegate GetGetExtHandler ()
		{
			if (cb_getExt == null)
				cb_getExt = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetExt);
			return cb_getExt;
		}

		static IntPtr n_GetExt (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.PrinterFileProperties __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFileProperties> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.Ext);
		}
#pragma warning restore 0169

		protected virtual unsafe string Ext {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFileProperties']/method[@name='getExt' and count(parameter)=0]"
			[Register ("getExt", "()Ljava/lang/String;", "GetGetExtHandler")]
			get {
				const string __id = "getExt.()Ljava/lang/String;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

		static Delegate cb_getFullName;
#pragma warning disable 0169
		static Delegate GetGetFullNameHandler ()
		{
			if (cb_getFullName == null)
				cb_getFullName = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetFullName);
			return cb_getFullName;
		}

		static IntPtr n_GetFullName (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.PrinterFileProperties __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFileProperties> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewString (__this.FullName);
		}
#pragma warning restore 0169

		public virtual unsafe string FullName {
			// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFileProperties']/method[@name='getFullName' and count(parameter)=0]"
			[Register ("getFullName", "()Ljava/lang/String;", "GetGetFullNameHandler")]
			get {
				const string __id = "getFullName.()Ljava/lang/String;";
				try {
					var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
					return JNIEnv.GetString (__rm.Handle, JniHandleOwnership.TransferLocalRef);
				} finally {
				}
			}
		}

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/PrinterFileProperties", DoNotGenerateAcw=true)]
	internal partial class PrinterFilePropertiesInvoker : PrinterFileProperties {

		public PrinterFilePropertiesInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer) {}

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/internal/PrinterFileProperties", typeof (PrinterFilePropertiesInvoker));

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

	}

}
