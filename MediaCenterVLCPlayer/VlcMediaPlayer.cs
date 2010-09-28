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
        private ArrayList _audioOutputDevices;
        private ArrayList _aspectRatios;
        public IntPtr Handle;
        private IntPtr drawable;
        private bool playing, paused;
        private VlcMedia _media;
        private VlcInstance _instance;

        public VlcMediaPlayer(VlcInstance instance, VlcMedia media)
        {
            this._instance = instance;
            this._media = media;
            Handle = VLCLib.libvlc_media_player_new_from_media(media.Handle);
            if (Handle == IntPtr.Zero) throw new VlcException();
        }

        public void Dispose()
        {
            VLCLib.libvlc_media_player_release(Handle);
        }

        public IntPtr Drawable
        {
            get { return drawable; }
            set
            {
                VLCLib.libvlc_media_player_set_hwnd(Handle, value);
                drawable = value;
            }
        }

        public VlcMedia Media
        {
            get
            {
                if (_media == null)
                {
                    IntPtr mediaHandle = VLCLib.libvlc_media_player_get_media(Handle);
                    if (mediaHandle == IntPtr.Zero) return null;
                    _media = new VlcMedia(mediaHandle);
                }
                return _media;
            }
        }

        public void Play()
        {
            int ret = VLCLib.libvlc_media_player_play(Handle);
            if (ret == -1)
                throw new VlcException();

            playing = true;
            paused = false;

            while (VLCLib.libvlc_media_player_is_playing(Handle) < 1)
            {
                System.Threading.Thread.Sleep(1 * 1000);
            }
            Media.LoadMediaMetaData();
        }

        public bool IsPlaying { get { return playing && !paused; }}
        public bool IsPaused { get { return playing && paused; }}
        public bool IsStopped { get { return !playing; }}

        public void Pause()
        {
            VLCLib.libvlc_media_player_pause(Handle);

            if (playing)
                paused ^= true;
        }

        public void Stop()
        {
            VLCLib.libvlc_media_player_stop(Handle);

            playing = false;
            paused = false;
        }

        public int Volume
        {
            get
            {
                return VLCLib.libvlc_audio_get_volume(Handle);
            }
            set
            {
                VLCLib.libvlc_audio_set_volume(Handle, value);
            }
        }

        private void loadAudioOutputs()
        {
            Logger.WriteToLog("Loading Audio Outputs and Devices");
            try
            {
                _audioOutputs = new ArrayList();
                IntPtr pOutputs = VLCLib.libvlc_audio_output_list_get(_instance.Handle);
                if (pOutputs != IntPtr.Zero)
                {
                    int i = 0;
                    VLCLib.libvlc_audio_output_t output = (VLCLib.libvlc_audio_output_t)Marshal.PtrToStructure(pOutputs, typeof(VLCLib.libvlc_audio_output_t));
                    Logger.WriteToLog("Audio Output: " + output.psz_name);
                    int deviceCount = VLCLib.libvlc_audio_output_device_count(_instance.Handle, output.psz_name);
                    Logger.WriteToLog("Found " + deviceCount.ToString() + " output devices for " + output.psz_name);
                    if (deviceCount > 0)
                        _audioOutputs.Add(new AudioOutput(output));

                    while (output.p_next != IntPtr.Zero)
                    {
                        i++;
                        output = (VLCLib.libvlc_audio_output_t)Marshal.PtrToStructure(output.p_next,
                            typeof(VLCLib.libvlc_audio_output_t));
                        Logger.WriteToLog("Audio Output: " + output.psz_name);
                        int mdeviceCount = VLCLib.libvlc_audio_output_device_count(_instance.Handle, output.psz_name);
                        Logger.WriteToLog("Found " + mdeviceCount.ToString() + " output devices for " + output.psz_name);
                        if (mdeviceCount > 0)
                            _audioOutputs.Add(new AudioOutput(output));
                    }
                }
                VLCLib.libvlc_audio_output_list_release(pOutputs);
            }
            catch (Exception e)
            {
                Logger.WriteToLog("Error during loading Audio Outputs: " + e.Message);
            }
        }

        /*
        private void loadAudioOutputDevices()
        {
            Logger.WriteToLog("Loading Audio Output Devices");
            try
            {
                _audioOutputDevices = new System.Collections.Generic.Dictionary<int, AudioDevice[]>();
                for (int i = 0; i < _audioOutputs.Count; i++)
                {
                    int deviceCount = VLCLib.libvlc_audio_output_device_count(VlcInstance.Handle, _audioOutputs[i].psz_name);
                    Logger.WriteToLog("Found " + deviceCount.ToString() + " devices for Audio Output: " + _audioOutputs[i].psz_name);
                    if (deviceCount > 0)
                    {
                        _audioOutputDevices.Add(i, new AudioDevice[deviceCount]);
                        for (int d = 0; d < deviceCount; d++)
                        {
                            IntPtr pDeviceId = VLCLib.libvlc_audio_output_device_id(VlcInstance.Handle, _audioOutputs[i].psz_name, d);
                            IntPtr pDeviceName = VLCLib.libvlc_audio_output_device_longname(VlcInstance.Handle, _audioOutputs[i].psz_name, d);
                            String strDeviceId = Marshal.PtrToStringAnsi(pDeviceId);
                            String strDeviceName = Marshal.PtrToStringAnsi(pDeviceName);
                            Logger.WriteToLog("Adding Device: " + strDeviceName);
                            AudioDevice device = new AudioDevice();
                            device.deviceId = strDeviceId;
                            device.deviceName = strDeviceName;
                            _audioOutputDevices[i][d] = device;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteToLog("Error loading audio output devices: " + e.Message);
            }
        }
        */
        private void loadAspectRatios()
        {
        }

        public bool ChangeSubtitleTrack(int trackIndex)
        {
            if (trackIndex <= _media.SubtitleTracks.Count - 1)
            {
                VLCLib.libvlc_track_description_t subtitleTrack = (VLCLib.libvlc_track_description_t)_media.SubtitleTracks[trackIndex];
                VLCLib.libvlc_video_set_spu(Handle, subtitleTrack.i_id);
                return true;
            }
            return false;
        }
    }
}
