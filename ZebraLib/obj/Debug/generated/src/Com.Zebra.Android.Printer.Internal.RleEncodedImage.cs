using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer.Internal {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='RleEncodedImage']"
	[global::Android.Runtime.Register ("com/zebra/android/printer/internal/RleEncodedImage", DoNotGenerateAcw=true)]
	public partial class RleEncodedImage : global::Java.Lang.Object {

		// Metadata.xml XPath class reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='RleEncodedImage.DataBuffer']"
		[global::Android.Runtime.Register ("com/zebra/android/printer/internal/RleEncodedImage$DataBuffer", DoNotGenerateAcw=true)]
		public partial class DataBuffer : global::Java.Lang.Object {

			internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/RleEncodedImage$DataBuffer", typeof (DataBuffer));
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

			protected DataBuffer (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='RleEncodedImage.DataBuffer']/constructor[@name='RleEncodedImage.DataBuffer' and count(parameter)=2 and parameter[1][@type='com.zebra.android.printer.internal.RleEncodedImage'] and parameter[2][@type='byte[]']]"
			[Register (".ctor", "(Lcom/zebra/android/printer/internal/RleEncodedImage;[B)V", "")]
			public unsafe DataBuffer (global::Com.Zebra.Android.Printer.Internal.RleEncodedImage __self, byte[] p1)
				: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
			{
				string __id = "(L" + global::Android.Runtime.JNIEnv.GetJniName (GetType ().DeclaringType) + ";[B)V";

				if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
					return;

				IntPtr native_p1 = JNIEnv.NewArray (p1);
				try {
					JniArgumentValue* __args = stackalloc JniArgumentValue [2];
					__args [0] = new JniArgumentValue ((__self == null) ? IntPtr.Zero : ((global::Java.Lang.Object) __self).Handle);
					__args [1] = new JniArgumentValue (native_p1);
					var __r = _members.InstanceMethods.StartCreateInstance (__id, ((object) this).GetType (), __args);
					SetHandle (__r.Handle, JniHandleOwnership.TransferLocalRef);
					_members.InstanceMethods.FinishCreateInstance (__id, this, __args);
				} finally {
					if (p1 != null) {
						JNIEnv.CopyArray (native_p1, p1);
						JNIEnv.DeleteLocalRef (native_p1);
					}
				}
			}

			static Delegate cb_getByte;
#pragma warning disable 0169
			static Delegate GetGetByteHandler ()
			{
				if (cb_getByte == null)
					cb_getByte = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, sbyte>) n_GetByte);
				return cb_getByte;
			}

			static sbyte n_GetByte (IntPtr jnienv, IntPtr native__this)
			{
				global::Com.Zebra.Android.Printer.Internal.RleEncodedImage.DataBuffer __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.RleEncodedImage.DataBuffer> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				return __this.Byte;
			}
#pragma warning restore 0169

			public virtual unsafe sbyte Byte {
				// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='RleEncodedImage.DataBuffer']/method[@name='getByte' and count(parameter)=0]"
				[Register ("getByte", "()B", "GetGetByteHandler")]
				get {
					const string __id = "getByte.()B";
					try {
						var __rm = _members.InstanceMethods.InvokeVirtualSByteMethod (__id, this, null);
						return __rm;
					} finally {
					}
				}
			}

		}

		internal    new     static  readonly    JniPeerMembers  _members    = new XAPeerMembers ("com/zebra/android/printer/internal/RleEncodedImage", typeof (RleEncodedImage));
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

		protected RleEncodedImage (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='RleEncodedImage']/constructor[@name='RleEncodedImage' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe RleEncodedImage ()
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

		static Delegate cb_rleEncoding_arrayBI;
#pragma warning disable 0169
		static Delegate GetRleEncoding_arrayBIHandler ()
		{
			if (cb_rleEncoding_arrayBI == null)
				cb_rleEncoding_arrayBI = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, int, IntPtr>) n_RleEncoding_arrayBI);
			return cb_rleEncoding_arrayBI;
		}

		static IntPtr n_RleEncoding_arrayBI (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int p1)
		{
			global::Com.Zebra.Android.Printer.Internal.RleEncodedImage __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.Internal.RleEncodedImage> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			byte[] p0 = (byte[]) JNIEnv.GetArray (native_p0, JniHandleOwnership.DoNotTransfer, typeof (byte));
			IntPtr __ret = JNIEnv.NewArray (__this.RleEncoding (p0, p1));
			if (p0 != null)
				JNIEnv.CopyArray (p0, native_p0);
			return __ret;
		}
#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer.internal']/class[@name='RleEncodedImage']/method[@name='rleEncoding' and count(parameter)=2 and parameter[1][@type='byte[]'] and parameter[2][@type='int']]"
		[Register ("rleEncoding", "([BI)[B", "GetRleEncoding_arrayBIHandler")]
		public virtual unsafe byte[] RleEncoding (byte[] p0, int p1)
		{
			const string __id = "rleEncoding.([BI)[B";
			IntPtr native_p0 = JNIEnv.NewArray (p0);
			try {
				JniArgumentValue* __args = stackalloc JniArgumentValue [2];
				__args [0] = new JniArgumentValue (native_p0);
				__args [1] = new JniArgumentValue (p1);
				var __rm = _members.InstanceMethods.InvokeVirtualObjectMethod (__id, this, __args);
				return (byte[]) JNIEnv.GetArray (__rm.Handle, JniHandleOwnership.TransferLocalRef, typeof (byte));
			} finally {
				if (p0 != null) {
					JNIEnv.CopyArray (native_p0, p0);
					JNIEnv.DeleteLocalRef (native_p0);
				}
			}
		}

	}
}
