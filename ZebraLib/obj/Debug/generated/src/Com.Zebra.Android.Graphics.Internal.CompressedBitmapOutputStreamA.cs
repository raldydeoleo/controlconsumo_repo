using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Graphics.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CompressedBitmapOutputStreamA']"
	[global::Android.Runtime.Register ("com/zebra/android/graphics/internal/CompressedBitmapOutputStreamA", DoNotGenerateAcw=true)]
	public abstract partial class CompressedBitmapOutputStreamA : global::Java.IO.OutputStream {



		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CompressedBitmapOutputStreamA']/field[@name='internalEncodedBuffer']"
		[Register ("internalEncodedBuffer")]
		protected global::Java.IO.ByteArrayOutputStream InternalEncodedBuffer {
			get {
				const string __id = "internalEncodedBuffer.Ljava/io/ByteArrayOutputStream;";

				var __v = _members.InstanceFields.GetObjectValue (__id, this);
				return global::Java.Lang.Object.GetObject<global::Java.IO.ByteArrayOutputStream> (__v.Handle, JniHandleOwnership.TransferLocalRef);
			}
			set {
				const string __id = "internalEncodedBuffer.Ljava/io/ByteArrayOutputStream;";

				IntPtr native_value = global::Android.Runtime.JNIEnv.ToLocalJniHandle (value);
				try {
					_members.InstanceFields.SetValue (__id, this, new JniObjectReference (native_value));
				} finally {
					global::Android.Runtime.JNIEnv.DeleteLocalRef (native_value);
				}
			}
		}


		// Metadata.xml XPath field reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CompressedBitmapOutputStreamA']/field[@name='printerConnection']"
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
		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/graphics/internal/CompressedBitmapOutputStreamA", typeof (CompressedBitmapOutputStreamA));
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

		protected CompressedBitmapOutputStreamA (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CompressedBitmapOutputStreamA']/constructor[@name='CompressedBitmapOutputStreamA' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe CompressedBitmapOutputStreamA ()
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

		static Delegate cb_bufferAndWrite_C;
#pragma warning disable 0169
		static Delegate GetBufferAndWrite_CHandler ()
		{
			if (cb_bufferAndWrite_C == null)
				cb_bufferAndWrite_C = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, char>) n_BufferAndWrite_C);
			return cb_bufferAndWrite_C;
		}

		static void n_BufferAndWrite_C (IntPtr jnienv, IntPtr native__this, char p0)
		{
			global::Com.Zebra.Android.Graphics.Internal.CompressedBitmapOutputStreamA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Graphics.Internal.CompressedBitmapOutputStreamA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.BufferAndWrite (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CompressedBitmapOutputStreamA']/method[@name='bufferAndWrite' and count(parameter)=1 and parameter[1][@type='char']]"
		[Register ("bufferAndWrite", "(C)V", "GetBufferAndWrite_CHandler")]
		protected virtual unsafe void BufferAndWrite (char p0)
		{
			const string __id = "bufferAndWrite.(C)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
		}

		static Delegate cb_write_I;
#pragma warning disable 0169
		static Delegate GetWrite_IHandler ()
		{
			if (cb_write_I == null)
				cb_write_I = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, int>) n_Write_I);
			return cb_write_I;
		}

		static void n_Write_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::Com.Zebra.Android.Graphics.Internal.CompressedBitmapOutputStreamA __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Graphics.Internal.CompressedBitmapOutputStreamA> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			__this.Write (p0);
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.graphics.internal']/class[@name='CompressedBitmapOutputStreamA']/method[@name='write' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("write", "(I)V", "GetWrite_IHandler")]
		public override unsafe void Write (int p0)
		{
			const string __id = "write.(I)V";
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [1];
				__args [0] = new JniArgumentValue (p0);
				_members.InstanceMethods.InvokeVirtualVoidMethod (__id, this, __args);
			} finally {
			}
		}

	}

	[global::Android.Runtime.Register ("com/zebra/android/graphics/internal/CompressedBitmapOutputStreamA", DoNotGenerateAcw=true)]
	internal partial class CompressedBitmapOutputStreamAInvoker : CompressedBitmapOutputStreamA {

		public CompressedBitmapOutputStreamAInvoker (IntPtr handle, JniHandleOwnership transfer) : base (handle, transfer) {}

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/graphics/internal/CompressedBitmapOutputStreamA", typeof (CompressedBitmapOutputStreamAInvoker));

		public override global::Java.Interop.JniPeerMembers JniPeerMembers {
			get { return _members; }
		}

		protected override global::System.Type ThresholdType {
			get { return _members.ManagedPeerType; }
		}

	}

}
