using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilA']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/GraphicsUtilA", DoNotGenerateAcw=true)]
	public abstract partial class GraphicsUtilA : global::Java.Lang.Object, global::Com.Zebra.Android.Printer.IGraphicsUtil {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/GraphicsUtilA", typeof (GraphicsUtilA));
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

		protected GraphicsUtilA (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilA']/constructor[@name='GraphicsUtilA' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe GraphicsUtilA ()
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

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilA']/method[@name='getImage' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
		[Register ("getImage", "(Ljava/lang/String;)Landroid/graphics/Bitmap;", "")]
		public static unsafe global::Android.Graphics.Bitmap GetImage (string p0)
		{
			const string __id = "getImage.(Ljava/lang/String;)Landroid/graphics/Bitmap;";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (native_p0);
				var __rm = _members.StaticMethods.InvokeObjectMethod (__id, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static Delegate cb_printImage_Ljava_lang_String_II;
#pragma warning disable 0169
		static Delegate GetPrintImage_Ljava_lang_String_IIHandler ()
		{
			if (cb_printImage_Ljava_lang_String_II == null)
				cb_printImage_Ljava_lang_String_II = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, int, int>) n_PrintImage_Ljava_lang_String_II);
			return cb_printImage_Ljava_lang_String_II;
		}

		static void n_PrintImage_Ljava_lang_String_II (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int p1, int p2)
		{
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.PrintImage (p0, p1, p2);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilA']/method[@name='printImage' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
		[Register ("printImage", "(Ljava/lang/String;II)V", "GetPrintImage_Ljava_lang_String_IIHandler")]
		public virtual unsafe void PrintImage (string p0, int p1, int p2)
		{
			const string __id = "printImage.(Ljava/lang/String;II)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (p2);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static Delegate cb_printImage_Ljava_lang_String_IIIIZ;
#pragma warning disable 0169
		static Delegate GetPrintImage_Ljava_lang_String_IIIIZHandler ()
		{
			if (cb_printImage_Ljava_lang_String_IIIIZ == null)
				cb_printImage_Ljava_lang_String_IIIIZ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, int, int, int, int, bool>) n_PrintImage_Ljava_lang_String_IIIIZ);
			return cb_printImage_Ljava_lang_String_IIIIZ;
		}

		static void n_PrintImage_Ljava_lang_String_IIIIZ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int p1, int p2, int p3, int p4, bool p5)
		{
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.PrintImage (p0, p1, p2, p3, p4, p5);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilA']/method[@name='printImage' and count(parameter)=6 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int'] and parameter[3][@type='int'] and parameter[4][@type='int'] and parameter[5][@type='int'] and parameter[6][@type='boolean']]"
		[Register ("printImage", "(Ljava/lang/String;IIIIZ)V", "GetPrintImage_Ljava_lang_String_IIIIZHandler")]
		public virtual unsafe void PrintImage (string p0, int p1, int p2, int p3, int p4, bool p5)
		{
			const string __id = "printImage.(Ljava/lang/String;IIIIZ)V";
			IntPtr native_p0 = JNIEnv.NewString (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [6];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue (p2);
				__args [3] = new JniArgumentValue (p3);
				__args [4] = new JniArgumentValue (p4);
				__args [5] = new JniArgumentValue (p5);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

		static Delegate cb_scaleImage_IILandroid_graphics_Bitmap_;
#pragma warning disable 0169
		static Delegate GetScaleImage_IILandroid_graphics_Bitmap_Handler ()
		{
			if (cb_scaleImage_IILandroid_graphics_Bitmap_ == null)
				cb_scaleImage_IILandroid_graphics_Bitmap_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, int, IntPtr, IntPtr>) n_ScaleImage_IILandroid_graphics_Bitmap_);
			return cb_scaleImage_IILandroid_graphics_Bitmap_;
		}

		static IntPtr n_ScaleImage_IILandroid_graphics_Bitmap_ (IntPtr jnienv, IntPtr native__this, int p0, int p1, IntPtr native_p2)
		{
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Graphics.Bitmap p2 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p2, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.ScaleImage (p0, p1, p2));
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='GraphicsUtilA']/method[@name='scaleImage' and count(parameter)=3 and parameter[1][@type='int'] and parameter[2][@type='int'] and parameter[3][@type='android.graphics.Bitmap']]"
		[Register ("scaleImage", "(IILandroid/graphics/Bitmap;)Landroid/graphics/Bitmap;", "GetScaleImage_IILandroid_graphics_Bitmap_Handler")]
		protected virtual unsafe global::Android.Graphics.Bitmap ScaleImage (int p0, int p1, global::Android.Graphics.Bitmap p2)
		{
			const string __id = "scaleImage.(IILandroid/graphics/Bitmap;)Landroid/graphics/Bitmap;";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [3];
				__args [0] = new JniArgumentValue (p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (__rm.Handle, JniHandleOwnership.TransferLocalRef);
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
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Graphics.Bitmap p0 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.PrintImage (p0, p1, p2, p3, p4, p5);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='GraphicsUtil']/method[@name='printImage' and count(parameter)=6 and parameter[1][@type='android.graphics.Bitmap'] and parameter[2][@type='int'] and parameter[3][@type='int'] and parameter[4][@type='int'] and parameter[5][@type='int'] and parameter[6][@type='boolean']]"
		[Register ("printImage", "(Landroid/graphics/Bitmap;IIIIZ)V", "GetPrintImage_Landroid_graphics_Bitmap_IIIIZHandler")]
		public abstract void PrintImage (global::Android.Graphics.Bitmap p0, int p1, int p2, int p3, int p4, bool p5);

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
			global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.GraphicsUtilA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			global::Android.Graphics.Bitmap p1 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.StoreImage (p0, p1, p2, p3);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='GraphicsUtil']/method[@name='storeImage' and count(parameter)=4 and parameter[1][@type='java.lang.String'] and parameter[2][@type='android.graphics.Bitmap'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
		[Register ("storeImage", "(Ljava/lang/String;Landroid/graphics/Bitmap;II)V", "GetStoreImage_Ljava_lang_String_Landroid_graphics_Bitmap_IIHandler")]
		public abstract void StoreImage (string p0, global::Android.Graphics.Bitmap p1, int p2, int p3);

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/GraphicsUtilA", DoNotGenerateAcw=true)]
	internal partial class GraphicsUtilAInvoker : GraphicsUtilA {

		public GraphicsUtilAInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer) {}

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/internal/GraphicsUtilA", typeof (GraphicsUtilAInvoker));

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='GraphicsUtil']/method[@name='printImage' and count(parameter)=6 and parameter[1][@type='android.graphics.Bitmap'] and parameter[2][@type='int'] and parameter[3][@type='int'] and parameter[4][@type='int'] and parameter[5][@type='int'] and parameter[6][@type='boolean']]"
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
				_members.InstanceMethods.InvokeAbstractVoidMethod (__id, this, __args);
			} finally {
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='GraphicsUtil']/method[@name='storeImage' and count(parameter)=4 and parameter[1][@type='java.lang.String'] and parameter[2][@type='android.graphics.Bitmap'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
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
				_members.InstanceMethods.InvokeAbstractVoidMethod (__id, this, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p0);
			}
		}

	}

}
