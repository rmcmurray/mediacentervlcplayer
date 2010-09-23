using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MediaCenterVLCPlayer
{
    class VlcMediaPlayer : IDisposable
    {
        public static IntPtr Handle;
        private IntPtr drawable;
        private bool playing, paused;
        private VlcMedia media;

        public VlcMediaPlayer(VlcMedia media)
        {
            this.media = media;
            VlcMediaPlayer.Handle = VLCLib.libvlc_media_player_new_from_media(VlcMedia.Handle);
            if (VlcMediaPlayer.Handle == IntPtr.Zero) throw new VlcException();
        }

        public void Dispose()
        {
            VLCLib.libvlc_media_player_release(VlcMediaPlayer.Handle);
        }

        public IntPtr Drawable
        {
            get { return drawable; }
            set
            {
                VLCLib.libvlc_media_player_set_hwnd(VlcMediaPlayer.Handle, value);
                drawable = value;
            }
        }

        public VlcMedia Media
        {
            get
            {
                IntPtr media = VLCLib.libvlc_media_player_get_media(VlcMediaPlayer.Handle);
                if (media == IntPtr.Zero) return null;
                return new VlcMedia(media);
            }
        }

        public void Play()
        {
            int ret = VLCLib.libvlc_media_player_play(VlcMediaPlayer.Handle);
            if (ret == -1)
                throw new VlcException();

            playing = true;
            paused = false;
        }

        public bool IsPlaying { get { return playing && !paused; }}
        public bool IsPaused { get { return playing && paused; }}
        public bool IsStopped { get { return !playing; }}

        public void Pause()
        {
            VLCLib.libvlc_media_player_pause(VlcMediaPlayer.Handle);

            if (playing)
                paused ^= true;
        }

        public void Stop()
        {
            VLCLib.libvlc_media_player_stop(VlcMediaPlayer.Handle);

            playing = false;
            paused = false;
        }

        public int Volume
        {
            get
            {
                return VLCLib.libvlc_audio_get_volume(VlcMediaPlayer.Handle);
            }
            set
            {
                VLCLib.libvlc_audio_set_volume(VlcMediaPlayer.Handle, value);
            }
        }
    }
}
