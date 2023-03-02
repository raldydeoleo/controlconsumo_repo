using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Graphics.Internal {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.graphics.internal']/interface[@name='ImageData']"
	[Register ("com/zebra/android/graphics/internal/ImageData", "", "Com.Zebra.Android.Graphics.Internal.IImageDataInvoker")]
	public partial interface IImageData : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/interface[@name='ImageData']/method[@name='getRow' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getRow", "(I)[I", "GetGetRow_IHandler:Com.Zebra.Android.Graphics.Internal.IImageDataInvoker, ZebraLib")]
		int[] GetRow (int p0);

	}

	[global::Android.Runtime.Register ("com/zebra/android/graphics/internal/ImageData", DoNotGenerateAcw=true)]
	internal class IImageDataInvoker : global::Java.Lang.Object, IImageData {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/graphics/internal/ImageData", typeof (IImageDataInvoker));

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

		public static IImageData GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IImageData> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.graphics.internal.ImageData"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IImageDataInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_getRow_I;
#pragma warning disable 0169
		static Delegate GetGetRow_IHandler ()
		{
			if (cb_getRow_I == null)
				cb_getRow_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, IntPtr>) n_GetRow_I);
			return cb_getRow_I;
		}

		static IntPtr n_GetRow_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::Com.Zebra.Android.Graphics.Internal.IImageData __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Graphics.Internal.IImageData> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.GetRow (p0));
		}
#pragma warning restore 0169

		IntPtr id_getRow_I;
		public unsafe int[] GetRow (int p0)
		{
			if (id_getRow_I == IntPtr.Zero)
				id_getRow_I = JNIEnv.GetMethodID (class_ref, "getRow", "(I)[I");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (p0);
			return (int[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_getRow_I, __args), JniHandleOwnership.TransferLocalRef, typeof (int));
		}

	}

}
