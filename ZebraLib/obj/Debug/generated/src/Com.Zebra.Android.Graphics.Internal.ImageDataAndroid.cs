using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Graphics.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='ImageDataAndroid']"
	[global::Android.Runtime.Register ("com/zebra/android/graphics/internal/ImageDataAndroid", DoNotGenerateAcw=true)]
	public partial class ImageDataAndroid : global::Java.Lang.Object, global::Com.Zebra.Android.Graphics.Internal.IImageData {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/graphics/internal/ImageDataAndroid", typeof (ImageDataAndroid));
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

		protected ImageDataAndroid (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='ImageDataAndroid']/constructor[@name='ImageDataAndroid' and count(parameter)=1 and parameter[1][@type='android.graphics.Bitmap']]"
		[Register (".ctor", "(Landroid/graphics/Bitmap;)V", "")]
		public unsafe ImageDataAndroid (global::Android.Graphics.Bitmap p0)
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			const string __id = "(Landroid/graphics/Bitmap;)V";

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
			global::Com.Zebra.Android.Graphics.Internal.ImageDataAndroid __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Graphics.Internal.ImageDataAndroid> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.GetRow (p0));
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='ImageDataAndroid']/method[@name='getRow' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("getRow", "(I)[I", "GetGetRow_IHandler")]
		public virtual unsafe int[] GetRow (int p0)
		{
			const string __id = "getRow.(I)[I";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return (int[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (int));
			} finally {
			}
		}

	}
}
