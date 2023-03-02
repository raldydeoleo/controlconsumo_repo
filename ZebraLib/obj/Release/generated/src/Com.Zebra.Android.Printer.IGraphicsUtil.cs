using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='GraphicsUtil']"
	[Register ("com/zebra/android/printer/GraphicsUtil", "", "Com.Zebra.Android.Printer.IGraphicsUtilInvoker")]
	public partial interface IGraphicsUtil : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='GraphicsUtil']/method[@name='printImage' and count(parameter)=6 and parameter[1][@type='android.graphics.Bitmap'] and parameter[2][@type='int'] and parameter[3][@type='int'] and parameter[4][@type='int'] and parameter[5][@type='int'] and parameter[6][@type='boolean']]"
		[Register ("printImage", "(Landroid/graphics/Bitmap;IIIIZ)V", "GetPrintImage_Landroid_graphics_Bitmap_IIIIZHandler:Com.Zebra.Android.Printer.IGraphicsUtilInvoker, ZebraLib")]
		void PrintImage (global::Android.Graphics.Bitmap p0, int p1, int p2, int p3, int p4, bool p5);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='GraphicsUtil']/method[@name='printImage' and count(parameter)=3 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int'] and parameter[3][@type='int']]"
		[Register ("printImage", "(Ljava/lang/String;II)V", "GetPrintImage_Ljava_lang_String_IIHandler:Com.Zebra.Android.Printer.IGraphicsUtilInvoker, ZebraLib")]
		void PrintImage (string p0, int p1, int p2);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='GraphicsUtil']/method[@name='printImage' and count(parameter)=6 and parameter[1][@type='java.lang.String'] and parameter[2][@type='int'] and parameter[3][@type='int'] and parameter[4][@type='int'] and parameter[5][@type='int'] and parameter[6][@type='boolean']]"
		[Register ("printImage", "(Ljava/lang/String;IIIIZ)V", "GetPrintImage_Ljava_lang_String_IIIIZHandler:Com.Zebra.Android.Printer.IGraphicsUtilInvoker, ZebraLib")]
		void PrintImage (string p0, int p1, int p2, int p3, int p4, bool p5);

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='GraphicsUtil']/method[@name='storeImage' and count(parameter)=4 and parameter[1][@type='java.lang.String'] and parameter[2][@type='android.graphics.Bitmap'] and parameter[3][@type='int'] and parameter[4][@type='int']]"
		[Register ("storeImage", "(Ljava/lang/String;Landroid/graphics/Bitmap;II)V", "GetStoreImage_Ljava_lang_String_Landroid_graphics_Bitmap_IIHandler:Com.Zebra.Android.Printer.IGraphicsUtilInvoker, ZebraLib")]
		void StoreImage (string p0, global::Android.Graphics.Bitmap p1, int p2, int p3);

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/GraphicsUtil", DoNotGenerateAcw=true)]
	internal class IGraphicsUtilInvoker : global::Java.Lang.Object, IGraphicsUtil {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/GraphicsUtil", typeof (IGraphicsUtilInvoker));

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

		public static IGraphicsUtil GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IGraphicsUtil> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.printer.GraphicsUtil"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IGraphicsUtilInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
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
			global::Com.Zebra.Android.Printer.IGraphicsUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IGraphicsUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Graphics.Bitmap p0 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.PrintImage (p0, p1, p2, p3, p4, p5);
		}
#pragma warning restore 0169

		IntPtr id_printImage_Landroid_graphics_Bitmap_IIIIZ;
		public unsafe void PrintImage (global::Android.Graphics.Bitmap p0, int p1, int p2, int p3, int p4, bool p5)
		{
			if (id_printImage_Landroid_graphics_Bitmap_IIIIZ == IntPtr.Zero)
				id_printImage_Landroid_graphics_Bitmap_IIIIZ = JNIEnv.GetMethodID (class_ref, "printImage", "(Landroid/graphics/Bitmap;IIIIZ)V");
			JValue* __args = stackalloc JValue [6];
			__args [0] = new JValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
			__args [1] = new JValue (p1);
			__args [2] = new JValue (p2);
			__args [3] = new JValue (p3);
			__args [4] = new JValue (p4);
			__args [5] = new JValue (p5);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_printImage_Landroid_graphics_Bitmap_IIIIZ, __args);
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
			global::Com.Zebra.Android.Printer.IGraphicsUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IGraphicsUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.PrintImage (p0, p1, p2);
		}
#pragma warning restore 0169

		IntPtr id_printImage_Ljava_lang_String_II;
		public unsafe void PrintImage (string p0, int p1, int p2)
		{
			if (id_printImage_Ljava_lang_String_II == IntPtr.Zero)
				id_printImage_Ljava_lang_String_II = JNIEnv.GetMethodID (class_ref, "printImage", "(Ljava/lang/String;II)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [3];
			__args [0] = new JValue (native_p0);
			__args [1] = new JValue (p1);
			__args [2] = new JValue (p2);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_printImage_Ljava_lang_String_II, __args);
			JNIEnv.DeleteLocalRef (native_p0);
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
			global::Com.Zebra.Android.Printer.IGraphicsUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IGraphicsUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.PrintImage (p0, p1, p2, p3, p4, p5);
		}
#pragma warning restore 0169

		IntPtr id_printImage_Ljava_lang_String_IIIIZ;
		public unsafe void PrintImage (string p0, int p1, int p2, int p3, int p4, bool p5)
		{
			if (id_printImage_Ljava_lang_String_IIIIZ == IntPtr.Zero)
				id_printImage_Ljava_lang_String_IIIIZ = JNIEnv.GetMethodID (class_ref, "printImage", "(Ljava/lang/String;IIIIZ)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [6];
			__args [0] = new JValue (native_p0);
			__args [1] = new JValue (p1);
			__args [2] = new JValue (p2);
			__args [3] = new JValue (p3);
			__args [4] = new JValue (p4);
			__args [5] = new JValue (p5);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_printImage_Ljava_lang_String_IIIIZ, __args);
			JNIEnv.DeleteLocalRef (native_p0);
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
			global::Com.Zebra.Android.Printer.IGraphicsUtil __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IGraphicsUtil> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			string p0 = JNIEnv.GetString (native_p0, JniHandleOwnership.DoNotTransfer);
			global::Android.Graphics.Bitmap p1 = global::Java.Lang.Object.GetObject<global::Android.Graphics.Bitmap> (native_p1, JniHandleOwnership.DoNotTransfer);
			__this.StoreImage (p0, p1, p2, p3);
		}
#pragma warning restore 0169

		IntPtr id_storeImage_Ljava_lang_String_Landroid_graphics_Bitmap_II;
		public unsafe void StoreImage (string p0, global::Android.Graphics.Bitmap p1, int p2, int p3)
		{
			if (id_storeImage_Ljava_lang_String_Landroid_graphics_Bitmap_II == IntPtr.Zero)
				id_storeImage_Ljava_lang_String_Landroid_graphics_Bitmap_II = JNIEnv.GetMethodID (class_ref, "storeImage", "(Ljava/lang/String;Landroid/graphics/Bitmap;II)V");
			IntPtr native_p0 = JNIEnv.NewString (p0);
			JValue* __args = stackalloc JValue [4];
			__args [0] = new JValue (native_p0);
			__args [1] = new JValue ((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p1).Handle);
			__args [2] = new JValue (p2);
			__args [3] = new JValue (p3);
			JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_storeImage_Ljava_lang_String_Landroid_graphics_Bitmap_II, __args);
			JNIEnv.DeleteLocalRef (native_p0);
		}

	}

}
