using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilCpcl']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/GraphicsUtilCpcl", DoNotGenerateAcw=true)]
	public partial class GraphicsUtilCpcl : global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilCpcl']/field[@name='printerConnection']"
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
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/GraphicsUtilCpcl", typeof (GraphicsUtilCpcl));
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

		protected GraphicsUtilCpcl (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilCpcl']/constructor[@name='GraphicsUtilCpcl' and count(parameter)=1 and parameter[1][@type='com.zebra.android.comm.ZebraPrinterConnection']]"
		[Register (".ctor", "(Lcom/zebra/android/comm/ZebraPrinterConnection;)V", "")]
		public unsafe GraphicsUtilCpcl (global::Com.Zebra.Android.Comm.IZebraPrinterConnection p0)
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

		static Delegate cb_createPcxHeader_II;
#pragma warning disable 0169
		static Delegate GetCreatePcxHeader_IIHandler ()
		{
			if (cb_createPcxHeader_II == null)
				cb_createPcxHeader_II = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, int, IntPtr>) n_CreatePcxHeader_II);
			return cb_createPcxHeader_II;
		}

		static IntPtr n_CreatePcxHeader_II (IntPtr jnienv, IntPtr native__this, int p0, int p1)
		{
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.CreatePcxHeader (p0, p1));
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilCpcl']/method[@name='createPcxHeader' and count(parameter)=2 and parameter[1][@type='int'] and parameter[2][@type='int']]"
		[Register ("createPcxHeader", "(II)[B", "GetCreatePcxHeader_IIHandler")]
		public virtual unsafe byte[] CreatePcxHeader (int p0, int p1)
		{
			const string __id = "createPcxHeader.(II)[B";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (p0);
				__args [1] = new JniArgumentValue (p1);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return (byte[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (byte));
			} finally {
			}
		}

		static Delegate cb_createPcxImage_IILandroid_graphics_Bitmap_;
#pragma warning disable 0169
		static Delegate GetCreatePcxImage_IILandroid_graphics_Bitmap_Handler ()
		{
			if (cb_createPcxImage_IILandroid_graphics_Bitmap_ == null)
				cb_createPcxImage_IILandroid_graphics_Bitmap_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, int, IntPtr, IntPtr>) n_CreatePcxImage_IILandroid_graphics_Bitmap_);
			return cb_createPcxImage_IILandroid_graphics_Bitmap_;
		}

		static IntPtr n_CreatePcxImage_IILandroid_graphics_Bitmap_ (IntPtr jnienv, IntPtr native__this, int p0, int p1, IntPtr native_p2)
		{
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Graphics.Bitmap p2 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.NewArray (__this.CreatePcxImage (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilCpcl']/method[@name='createPcxImage' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='int'] and parameter[3][@type='android.graphics.Bitmap']]"
		[Register ("createPcxImage", "(IILandroid/graphics/Bitmap;)[B", "GetCreatePcxImage_IILandroid_graphics_Bitmap_Handler")]
		public virtual unsafe byte[] CreatePcxImage (int p0, int p1, global::Android.Graphics.Bitmap p2)
		{
			const string __id = "createPcxImage.(IILandroid/graphics/Bitmap;)[B";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return (byte[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (byte));
			} finally {
			}
		}

		static Delegate cb_createPcxImage_IIarrayB;
#pragma warning disable 0169
		static Delegate GetCreatePcxImage_IIarrayBHandler ()
		{
			if (cb_createPcxImage_IIarrayB == null)
				cb_createPcxImage_IIarrayB = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, int, IntPtr, IntPtr>) n_CreatePcxImage_IIarrayB);
			return cb_createPcxImage_IIarrayB;
		}

		static IntPtr n_CreatePcxImage_IIarrayB (IntPtr jnienv, IntPtr native__this, int p0, int p1, IntPtr native_p2)
		{
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			byte[] p2 = (byte[]) JNIEnv.GetArray (native_p2, JniHandleOwnership.DoNotTransfer, typeof (byte));
			IntPtr __ret = JNIEnv.NewArray (__this.CreatePcxImage (p0, p1, p2));
			if (p2 != null)
				JNIEnv.CopyArray (p2, native_p2);
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilCpcl']/method[@name='createPcxImage' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='int'] and parameter[3][@type='byte[]']]"
		[Register ("createPcxImage", "(II[B)[B", "GetCreatePcxImage_IIarrayBHandler")]
		public virtual unsafe byte[] CreatePcxImage (int p0, int p1, byte[] p2)
		{
			const string __id = "createPcxImage.(II[B)[B";
			IntPtr native_p2 = JNIEnv.NewArray (p2);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (native_p2);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return (byte[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (byte));
			} finally {
				if (p2 != null) {
					JNIEnv.CopyArray (native_p2, p2);
					JNIEnv.DeleteLocalRef (native_p2);
				}
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
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Graphics.Bitmap p0 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.PrintImage (p0, p1, p2, p3, p4, p5);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilCpcl']/method[@name='printImage' and count(parameter)=6 and parameter[1][@type='android.graphics.Bitmap'] and parameter[2][@type='int'] and parameter[3][@type='int'] and parameter[4][@type='int'] and parameter[5][@type='int'] and parameter[6][@type='boolean']]"
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
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilCpcl __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilCpcl> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			global::Android.Graphics.Bitmap p1 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.StoreImage (p0, p1, p2, p3);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilCpcl']/method[@name='storeImage' and count(parameter)=4 and parameter[1][@type='java.lang.String'] and parameter[2][@type='android.graphics.Bitmap'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
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
