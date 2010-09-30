using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MediaCenterVLCPlayer
{
    public class VlcMediaPlayer : IDisposable
    {
        private ArrayList _audioOutputs;
        private ArrayList _aspectRatios;
        public IntPtr Handle;
        private IntPtr drawable;
        private bool playing, paused;
        private VlcMedia _media;
        private VlcInstance _instance;

        public ArrayList AudioOutputs
        {
            get { return _audioOutputs; }
        }
        public ArrayList AspectRatios
        {
            get { return _aspectRatios; }
        }
        public bool IsPlaying { get { return playing && !paused; } }
        public bool IsPaused { get { return playing && paused; } }
        public bool IsStopped { get { return !playing; } }
        public IntPtr Drawable
        {
            get { return drawable; }
            set
            {
                VLCLibrary.Instance.media_player_set_hwnd(Handle, value);
                drawable = value;
            }
        }
        public VlcMedia Media
        {
            get
            {
                if (_media == null)
                {
                    IntPtr mediaHandle = VLCLibrary.Instance.media_player_get_media(Handle);
                    if (mediaHandle == IntPtr.Zero) return null;
                    _media = new VlcMedia(mediaHandle);
                }
                return _media;
            }
        }

        public VlcMediaPlayer(VlcInstance instance, VlcMedia media)
        {
            this._instance = instance;
            this._media = media;
            Handle = VLCLibrary.Instance.media_player_new_from_media(media.Handle);
            if (Handle == IntPtr.Zero) throw new VlcException();
        }
        public void Dispose()
        {
            VLCLibrary.Instance.media_player_release(Handle);
        }
        public void Play()
        {
            int ret = VLCLibrary.Instance.media_player_play(Handle);
            if (ret == -1)
                throw new VlcException();

            playing = true;
            paused = false;

            while (VLCLibrary.Instance.media_player_is_playing(Handle) < 1)
            {
                System.Threading.Thread.Sleep(1 * 300);
            }
            Media.LoadMediaMetaData();
            loadAudioOutputs();
            loadAspectRatios();
        }
        public void Pause()
        {
            VLCLibrary.Instance.media_player_pause(Handle);

            if (playing)
                paused ^= true;
        }
        public void Stop()
        {
            VLCLibrary.Instance.media_player_stop(Handle);

            playing = false;
            paused = false;
        }
        public int Volume
        {
            get
            {
                return VLCLibrary.Instance.audio_get_volume(Handle);
            }
            set
            {
                VLCLibrary.Instance.audio_set_volume(Handle, value);
            }
        }
        private void loadAudioOutputs()
        {
            try
            {
                _audioOutputs = new ArrayList();
                IntPtr pOutputs = VLCLibrary.Instance.audio_output_list_get(_instance.Handle);
                if (pOutputs != IntPtr.Zero)
                {
                    VLCLibrary.libvlc_audio_output_t output;
                    do
                    {
                        output = (VLCLibrary.libvlc_audio_output_t)Marshal.PtrToStructure(pOutputs, typeof(VLCLibrary.libvlc_audio_output_t));
                        AudioOutput aOutput = new AudioOutput(output);
                        int mdeviceCount = VLCLibrary.Instance.audio_output_device_count(_instance.Handle, output.psz_name);
                        if (mdeviceCount > 0)
                        {
                            for (int k = 0; k < mdeviceCount; k++)
                            {
                                AudioDevice device = new AudioDevice();
                                IntPtr pId = VLCLibrary.Instance.audio_output_device_id(_instance.Handle, output.psz_name, k);
                                device.deviceId = Marshal.PtrToStringAnsi(pId);
                                IntPtr pLongName = VLCLibrary.Instance.audio_output_device_longname(_instance.Handle, output.psz_name, k);
                                device.deviceName = Marshal.PtrToStringAnsi(pLongName);
                                aOutput.AddDevice(device);
                            }
                            _audioOutputs.Add(aOutput);
                        }
                        pOutputs = output.p_next;
                    } while (output.p_next != IntPtr.Zero);
                }
                VLCLibrary.Instance.audio_output_list_release(pOutputs);
            }
            catch (Exception e)
            {
                Logger.WriteToLog("Error during loading Audio Outputs: " + e.Message);
            }
        }
        private void loadAspectRatios()
        {
        }
        public bool ChangeSubtitleTrack(int trackIndex)
        {
            if (trackIndex <= _media.SubtitleTracks.Count - 1)
            {
                VLCLibrary.libvlc_track_description_t subtitleTrack = (VLCLibrary.libvlc_track_description_t)_media.SubtitleTracks[trackIndex];
                VLCLibrary.Instance.video_set_spu(Handle, subtitleTrack.i_id);
                return true;
            }
            return false;
        }
    }
}
