using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Graphics.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='DitheredImageProvider']"
	[global::Android.Runtime.Register ("com/zebra/android/graphics/internal/DitheredImageProvider", DoNotGenerateAcw=true)]
	public partial class DitheredImageProvider : global::Java.Lang.Object {

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/graphics/internal/DitheredImageProvider", typeof (DitheredImageProvider));
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

		protected DitheredImageProvider (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='DitheredImageProvider']/constructor[@name='DitheredImageProvider' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe DitheredImageProvider ()
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

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='DitheredImageProvider']/method[@name='getDitheredImage' and count(parameter)=2 and parameter[1][@type='android.graphics.Bitmap'] and parameter[2][@type='java.io.OutputStream']]"
		[Register ("getDitheredImage", "(Landroid/graphics/Bitmap;Ljava/io/OutputStream;)V", "")]
		public static unsafe void GetDitheredImage (global::Android.Graphics.Bitmap p0, global::System.IO.Stream p1)
		{
			const string __id = "getDitheredImage.(Landroid/graphics/Bitmap;Ljava/io/OutputStream;)V";
			IntPtr native_p1 = global::Android.Runtime.OutputStreamAdapter.ToLocalJniHandle (p1);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue ((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p0).Handle);
				__args [1] = new JniArgumentValue (native_p1);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p1);
			}
		}

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='DitheredImageProvider']/method[@name='getDitheredImage' and count(parameter)=4 and parameter[1][@type='int'] and parameter[2][@type='int'] and parameter[3][@type='com.zebra.android.graphics.internal.ImageData'] and parameter[4][@type='java.io.OutputStream']]"
		[Register ("getDitheredImage", "(IILcom/zebra/android/graphics/internal/ImageData;Ljava/io/OutputStream;)V", "")]
		protected static unsafe void GetDitheredImage (int p0, int p1, global::Com.Zebra.Android.Graphics.Internal.IImageData p2, global::System.IO.Stream p3)
		{
			const string __id = "getDitheredImage.(IILcom/zebra/android/graphics/internal/ImageData;Ljava/io/OutputStream;)V";
			IntPtr native_p3 = global::Android.Runtime.OutputStreamAdapter.ToLocalJniHandle (p3);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [4];
				__args [0] = new JniArgumentValue (p0);
				__args [1] = new JniArgumentValue (p1);
				__args [2] = new JniArgumentValue ((p2 == null) ? IntPtr.Zero : ((global::Java.Lang.Object) p2).Handle);
				__args [3] = new JniArgumentValue (native_p3);
				_members.StaticMethods.InvokeVoidMethod (__id, __args);
			} finally {
				JNIEnv.DeleteLocalRef (native_p3);
			}
		}

	}
}
