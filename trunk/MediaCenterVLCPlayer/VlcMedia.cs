using System;

namespace MediaCenterVLCPlayer
{
    class VlcMedia : IDisposable
    {
        private int track_count;
        private Int64 length;
        public static IntPtr Handle;

        public VlcMedia(VlcInstance instance, string path)
        {
            VlcMedia.Handle = VLCLib.libvlc_media_new_path(VlcInstance.Handle, path);
            if (VlcMedia.Handle == IntPtr.Zero)
            {
                return;
            }
        }

        internal VlcMedia(IntPtr handle)
        {
            VlcMedia.Handle = handle;
        }

        public void Dispose()
        {
            VLCLib.libvlc_media_release(VlcMedia.Handle);
        }

        public Int64 Length
        {
            get { return this.length; }
            set { this.length = value; }
        }

        public int TrackCount
        {
            get { return this.track_count; }
            set { this.track_count = value; }
        }
    }
}
