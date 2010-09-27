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

        private void loadAudioOutputs()
        {
            Logger.WriteToLog("Loading Audio Outputs and Devices");
            try
            {
                _audioOutputs = new ArrayList();
                IntPtr pOutputs = VLCLib.libvlc_audio_output_list_get(VlcInstance.Handle);
                if (pOutputs != IntPtr.Zero)
                {
                    int i = 0;
                    VLCLib.libvlc_audio_output_t output = (VLCLib.libvlc_audio_output_t)Marshal.PtrToStructure(pOutputs, typeof(VLCLib.libvlc_audio_output_t));
                    Logger.WriteToLog("Audio Output: " + output.psz_name);
                    int deviceCount = VLCLib.libvlc_audio_output_device_count(VlcInstance.Handle, output.psz_name);
                    Logger.WriteToLog("Found " + deviceCount.ToString() + " output devices for " + output.psz_name);
                    if (deviceCount > 0)
                        _audioOutputs.Add(new AudioOutput(output));

                    while (output.p_next != IntPtr.Zero)
                    {
                        i++;
                        output = (VLCLib.libvlc_audio_output_t)Marshal.PtrToStructure(output.p_next,
                            typeof(VLCLib.libvlc_audio_output_t));
                        Logger.WriteToLog("Audio Output: " + output.psz_name);
                        int mdeviceCount = VLCLib.libvlc_audio_output_device_count(VlcInstance.Handle, output.psz_name);
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
    }
}
