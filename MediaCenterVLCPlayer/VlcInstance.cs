using System;

namespace MediaCenterVLCPlayer
{
    public class VlcInstance : IDisposable
    {
        public IntPtr Handle;
        private VlcMediaPlayer _player = null;
        private Logger _logger = null;

        public VlcMediaPlayer Player
        {
            get { return _player; }
            set { _player = value; }
        }
        public Logger Logger
        {
            get { return _logger; }
        }

        public VlcInstance(string[] args)
        {
            Handle = VLCLib.libvlc_new(args.Length, args);
            if (Handle == IntPtr.Zero) throw new VlcException();
            _logger = new Logger();
        }
        public void Dispose()
        {
            VLCLib.libvlc_release(Handle);
            if (_logger != null)
                _logger.Close();
        }
        public VlcMediaPlayer CreatePlayer(string mediaPath)
        {
            if (!System.IO.File.Exists(mediaPath))
                return null;

            VlcMedia media;
            try
            {
                System.IO.FileAttributes attr = System.IO.File.GetAttributes(System.IO.Path.GetFullPath(mediaPath));
                if ((attr & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
                {
                    media = new VlcMedia(this, @"dvd://" + mediaPath);
                }
                else
                {
                    media = new VlcMedia(this, mediaPath);
                }
            }
            catch (Exception e)
            {
                media = new VlcMedia(this, mediaPath);
            }


            VlcMediaPlayer player = new VlcMediaPlayer(this, media);
            if (player != null) {
                _player = player;
            }

            return player;
        }
    }
}
