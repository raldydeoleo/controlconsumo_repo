using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFilePropertiesList']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/PrinterFilePropertiesList", DoNotGenerateAcw=true)]
	public partial class PrinterFilePropertiesList : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/PrinterFilePropertiesList", typeof (PrinterFilePropertiesList));
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

		protected PrinterFilePropertiesList (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFilePropertiesList']/constructor[@name='PrinterFilePropertiesList' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe PrinterFilePropertiesList ()
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

		static Delegate cb_add_Lcom_zebra_android_printer_internal_PrinterFileProperties_;
#pragma warning disable 0169
		static Delegate GetAdd_Lcom_zebra_android_printer_internal_PrinterFileProperties_Handler ()
		{
			if (cb_add_Lcom_zebra_android_printer_internal_PrinterFileProperties_ == null)
				cb_add_Lcom_zebra_android_printer_internal_PrinterFileProperties_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_Add_Lcom_zebra_android_printer_internal_PrinterFileProperties_);
			return cb_add_Lcom_zebra_android_printer_internal_PrinterFileProperties_;
		}

		static void n_Add_Lcom_zebra_android_printer_internal_PrinterFileProperties_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Com.Zebra.Android.Printer.Internal.PrinterFileProperties p0 = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFileProperties> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.Add (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFilePropertiesList']/method[@name='add' and count(parameter)=1 and parameter[1][@type='com.zebra.android.printer.internal.PrinterFileProperties']]"
		[Register ("add", "(Lcom/zebra/android/printer/internal/PrinterFileProperties;)V", "GetAdd_Lcom_zebra_android_printer_internal_PrinterFileProperties_Handler")]
		public virtual unsafe void Add (global::Com.Zebra.Android.Printer.Internal.PrinterFileProperties p0)
		{
			const string __id = "add.(Lcom/zebra/android/printer/internal/PrinterFileProperties;)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
		}

		static Delegate cb_filterByExtension_arrayLjava_lang_String_;
#pragma warning disable 0169
		static Delegate GetFilterByExtension_arrayLjava_lang_String_Handler ()
		{
			if (cb_filterByExtension_arrayLjava_lang_String_ == null)
				cb_filterByExtension_arrayLjava_lang_String_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr>) n_FilterByExtension_arrayLjava_lang_String_);
			return cb_filterByExtension_arrayLjava_lang_String_;
		}

		static IntPtr n_FilterByExtension_arrayLjava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
		{
			global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string[] p0 = (string[]) JNIEnv.GetArray (native_p0, JniHandleOwnership.DoNotTransfer, typeof (string));
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.FilterByExtension (p0));
			if (p0 != null)
				JNIEnv.CopyArray (p0, native_p0);
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFilePropertiesList']/method[@name='filterByExtension' and count(parameter)=1 and parameter[1][@type='java.lang.String[]']]"
		[Register ("filterByExtension", "([Ljava/lang/String;)Lcom/zebra/android/printer/internal/PrinterFilePropertiesList;", "GetFilterByExtension_arrayLjava_lang_String_Handler")]
		public virtual unsafe global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList FilterByExtension (string[] p0)
		{
			const string __id = "filterByExtension.([Ljava/lang/String;)Lcom/zebra/android/printer/internal/PrinterFilePropertiesList;";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

		static Delegate cb_get_I;
#pragma warning disable 0169
		static Delegate GetGet_IHandler ()
		{
			if (cb_get_I == null)
				cb_get_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, IntPtr>) n_Get_I);
			return cb_get_I;
		}

		static IntPtr n_Get_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.ToLocalJniHandle (__this.Get (p0));
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFilePropertiesList']/method[@name='get' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("get", "(I)Lcom/zebra/android/printer/internal/PrinterFileProperties;", "GetGet_IHandler")]
		public virtual unsafe global::Com.Zebra.Android.Printer.Internal.PrinterFileProperties Get (int p0)
		{
			const string __id = "get.(I)Lcom/zebra/android/printer/internal/PrinterFileProperties;";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFileProperties> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
			}
		}

		static Delegate cb_getFileNamesFromProperties;
#pragma warning disable 0169
		static Delegate GetGetFileNamesFromPropertiesHandler ()
		{
			if (cb_getFileNamesFromProperties == null)
				cb_getFileNamesFromProperties = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr>) n_GetFileNamesFromProperties);
			return cb_getFileNamesFromProperties;
		}

		static IntPtr n_GetFileNamesFromProperties (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.GetFileNamesFromProperties ());
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFilePropertiesList']/method[@name='getFileNamesFromProperties' and count(parameter)=0]"
		[Register ("getFileNamesFromProperties", "()[Ljava/lang/String;", "GetGetFileNamesFromPropertiesHandler")]
		public virtual unsafe string[] GetFileNamesFromProperties ()
		{
			const string __id = "getFileNamesFromProperties.()[Ljava/lang/String;";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, null);
				return (string[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (string));
			} finally {
			}
		}

		static Delegate cb_size;
#pragma warning disable 0169
		static Delegate GetSizeHandler ()
		{
			if (cb_size == null)
				cb_size = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int>) n_Size);
			return cb_size;
		}

		static int n_Size (IntPtr jnienv, IntPtr native__this)
		{
			global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.PrinterFilePropertiesList> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return __this.Size ();
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='PrinterFilePropertiesList']/method[@name='size' and count(parameter)=0]"
		[Register ("size", "()I", "GetSizeHandler")]
		public virtual unsafe int Size ()
		{
			const string __id = "size.()I";
			try {
				var __rm = _members.InstanceMethods.InvokeVirtualInt32Method (__id, this, null);
				return __rm;
			} finally {
			}
		}

	}
}
