using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Zebra.Android.Printer {

	// Metadata.xml XPath interface reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='MagCardReader']"
	[Register ("com/zebra/android/printer/MagCardReader", "", "Com.Zebra.Android.Printer.IMagCardReaderInvoker")]
	public partial interface IMagCardReader : IJavaObject {

		// Metadata.xml XPath method reference: path="/api/package[@name='com.zebra.android.printer']/interface[@name='MagCardReader']/method[@name='read' and count(parameter)=1 and parameter[1][@type='int']]"
		[Register ("read", "(I)[Ljava/lang/String;", "GetRead_IHandler:Com.Zebra.Android.Printer.IMagCardReaderInvoker, ZebraLib")]
		string[] Read (int p0);

	}

	[global::Android.Runtime.Register ("com/zebra/android/printer/MagCardReader", DoNotGenerateAcw=true)]
	internal class IMagCardReaderInvoker : global::Java.Lang.Object, IMagCardReader {

		internal    new     static  readonly    JniPeerMembers  _members    = new JniPeerMembers ("com/zebra/android/printer/MagCardReader", typeof (IMagCardReaderInvoker));

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

		public static IMagCardReader GetObject (IntPtr handle, JniHandleOwnership transfer)
		{
			return global::Java.Lang.Object.GetObject<IMagCardReader> (handle, transfer);
		}

		static IntPtr Validate (IntPtr handle)
		{
			if (!JNIEnv.IsInstanceOf (handle, java_class_ref))
				throw new InvalidCastException (string.Format ("Unable to convert instance of type '{0}' to type '{1}'.",
							JNIEnv.GetClassNameFromInstance (handle), "com.zebra.android.printer.MagCardReader"));
			return handle;
		}

		protected override void Dispose (bool disposing)
		{
			if (this.class_ref != IntPtr.Zero)
				JNIEnv.DeleteGlobalRef (this.class_ref);
			this.class_ref = IntPtr.Zero;
			base.Dispose (disposing);
		}

		public IMagCardReaderInvoker (IntPtr handle, JniHandleOwnership transfer) : base (Validate (handle), transfer)
		{
			IntPtr local_ref = JNIEnv.GetObjectClass (((global::Java.Lang.Object) this).Handle);
			this.class_ref = JNIEnv.NewGlobalRef (local_ref);
			JNIEnv.DeleteLocalRef (local_ref);
		}

		static Delegate cb_read_I;
#pragma warning disable 0169
		static Delegate GetRead_IHandler ()
		{
			if (cb_read_I == null)
				cb_read_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, int, IntPtr>) n_Read_I);
			return cb_read_I;
		}

		static IntPtr n_Read_I (IntPtr jnienv, IntPtr native__this, int p0)
		{
			global::Com.Zebra.Android.Printer.IMagCardReader __this = global::Java.Lang.Object.GetObject<global::Com.Zebra.Android.Printer.IMagCardReader> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			return JNIEnv.NewArray (__this.Read (p0));
		}
#pragma warning restore 0169

		IntPtr id_read_I;
		public unsafe string[] Read (int p0)
		{
			if (id_read_I == IntPtr.Zero)
				id_read_I = JNIEnv.GetMethodID (class_ref, "read", "(I)[Ljava/lang/String;");
			JValue* __args = stackalloc JValue [1];
			__args [0] = new JValue (p0);
			return (string[]) JNIEnv.GetArray (JNIEnv.CallObjectMethod (((global::Java.Lang.Object) this).Handle, id_read_I, __args), JniHandleOwnership.TransferLocalRef, typeof (string));
		}

	}

}
