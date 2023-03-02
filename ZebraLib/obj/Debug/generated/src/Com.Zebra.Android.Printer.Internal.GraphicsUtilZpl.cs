using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilZpl']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/GraphicsUtilZpl", DoNotGenerateAcw=true)]
	public partial class GraphicsUtilZpl : global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilZpl']/field[@name='printerConnection']"
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
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/GraphicsUtilZpl", typeof (GraphicsUtilZpl));
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

		protected GraphicsUtilZpl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilZpl']/constructor[@name='GraphicsUtilZpl' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe GraphicsUtilZpl (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
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

		static Delegate cb_printImage_Landroid_graphics_Bitmap_IIIIZ;
#pragma warning disable 0169
		static Delegate GetPrintImage_Landroid_graphics_Bitmap_IIIIZHandler ()
		{
			if (cb_printImage_Landroid_graphics_Bitmap_IIIIZ == null)
				cb_printImage_Landroid_graphics_Bitmap_IIIIZ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, int, int, int, int, bool>) n_PrintImage_Landroid_graphics_Bitmap_IIIIZ);
			return cb_printImage_Landroid_graphics_Bitmap_IIIIZ;
		}

		static void n_PrintImage_Landroid_graphics_Bitmap_IIIIZ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int p1, int p2, int p3, int p4, bool p5)
		{
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Graphics.Bitmap p0 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.PrintImage (p0, p1, p2, p3, p4, p5);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilZpl']/method[@name='printImage' and count(parameter)=6 and parameter[1][@type='android.graphics.Bitmap'] and parameter[2][@type='int'] and parameter[3][@type='int'] and parameter[4][@type='int'] and parameter[5][@type='int'] and parameter[6][@type='boolean']]"
		[Register ("printImage", "(Landroid/graphics/Bitmap;IIIIZ)V", "GetPrintImage_Landroid_graphics_Bitmap_IIIIZHandler")]
		public override unsafe void PrintImage (global::Android.Graphics.Bitmap p0, int p1, int p2, int p3, int p4, bool p5)
		{
			const string __id = "printImage.(Landroid/graphics/Bitmap;IIIIZ)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [6];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (p2);
				__args [3] = new JniArgumentValue (p3);
				__args [4] = new JniArgumentValue (p4);
				__args [5] = new JniArgumentValue (p5);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
		}

		static Delegate cb_storeImage_Ljava_lang_String_Landroid_graphics_Bitmap_II;
#pragma warning disable 0169
		static Delegate GetStoreImage_Ljava_lang_String_Landroid_graphics_Bitmap_IIHandler ()
		{
			if (cb_storeImage_Ljava_lang_String_Landroid_graphics_Bitmap_II == null)
				cb_storeImage_Ljava_lang_String_Landroid_graphics_Bitmap_II = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr, int, int>) n_StoreImage_Ljava_lang_String_Landroid_graphics_Bitmap_II);
			return cb_storeImage_Ljava_lang_String_Landroid_graphics_Bitmap_II;
		}

		static void n_StoreImage_Ljava_lang_String_Landroid_graphics_Bitmap_II (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, int p2, int p3)
		{
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilZpl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilZpl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			global::Android.Graphics.Bitmap p1 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.StoreImage (p0, p1, p2, p3);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilZpl']/method[@name='storeImage' and count(parameter)=4 and parameter[1][@type='java.lang.String'] and parameter[2][@type='android.graphics.Bitmap'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
		[Register ("storeImage", "(Ljava/lang/String;Landroid/graphics/Bitmap;II)V", "GetStoreImage_Ljava_lang_String_Landroid_graphics_Bitmap_IIHandler")]
		public override unsafe void StoreImage (string p0, global::Android.Graphics.Bitmap p1, int p2, int p3)
		{
			const string __id = "storeImage.(Ljava/lang/String;Landroid/graphics/Bitmap;II)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [4];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
				__args [2] = new JniArgumentValue (p2);
				__args [3] = new JniArgumentValue (p3);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}
}
