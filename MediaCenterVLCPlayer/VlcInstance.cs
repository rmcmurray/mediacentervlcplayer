using System;

namespace MediaCenterVLCPlayer
{
    class VlcInstance : IDisposable
    {
        public static IntPtr Handle;

        public VlcInstance(string[] args)
        {
            VlcInstance.Handle = VLCLib.libvlc_new(args.Length, args);
            if (VlcInstance.Handle == IntPtr.Zero) throw new VlcException();
        }

        public void Dispose()
        {
            VLCLib.libvlc_release(VlcInstance.Handle);
        }
    }
}
