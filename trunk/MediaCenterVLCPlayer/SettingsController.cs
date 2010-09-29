using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MediaCenterVLCPlayer
{
    public struct AudioDevice
    {
        public string deviceId;
        public string deviceName;
    }

    public class SettingsController
    {
        public static SettingsController Instance;

        private int _currentAudioDeviceId;
        private int _currentAudioChannelId;
        private int _currentSubtitleTrackId;
        private int _currentAudioTrack;
        private int _currentAspectRatio;
        private String _currentAudioOutput;
        private VlcMediaPlayer _player;

        public VlcMediaPlayer Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public String CurrentAudioOutput
        {
            get { return _currentAudioOutput; }
            set
            {
                _currentAudioOutput = value;
            }
        }
        public int CurrentAspectRatio
        {
            get { return _currentAspectRatio; }
            set
            {
                _currentAspectRatio = value;
            }
        }
        public int CurrentAudioOutputDeviceId
        {
            get { return _currentAudioDeviceId; }
            set
            {
                _currentAudioDeviceId = value;
            }
        }
        public int CurrentAudioChannelId
        {
            get { return _currentAudioChannelId; }
            set
            {
                _currentAudioChannelId = value;
            }
        }
        public int CurrentSubtitleTrackId
        {
            get { return _currentSubtitleTrackId; }
            set
            {
                _currentSubtitleTrackId = value;
            }
        }
        public int CurrentAudioTrack
        {
            get { return _currentAudioTrack; }
            set
            {
                _currentAudioTrack = value;
            }
        }

        public SettingsController()
        {
            SettingsController.Instance = this;
        }

        public void SaveSettings()
        {
            MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioChannelsId = Convert.ToInt32(_currentAudioChannelId);
            MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioDeviceId = Convert.ToInt32(_currentAudioDeviceId);
            MediaCenterVLCPlayer.Properties.Settings.Default.Save();
        }

        public void ApplyDefaultSettings()
        {
            Logger.WriteToLog("Setting default settings for media");
            if (MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioDeviceId >= 0)
            {
                try
                {
                    VLCLib.libvlc_audio_output_set_device_type(_player.Handle,
                        MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioDeviceId);
                    CurrentAudioOutputDeviceId = MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioDeviceId;
                }
                catch (Exception)
                {
                    Logger.WriteToLog("Error setting default Audio Device to: " +
                        MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioDeviceId);
                }
            }
            if (MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioChannelsId >= 0)
            {
                try
                {
                    VLCLib.libvlc_audio_set_channel(_player.Handle,
                        MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioChannelsId);
                    CurrentAudioChannelId = MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioChannelsId;
                }
                catch (Exception)
                {
                    Logger.WriteToLog("Error setting default Audio Channels to: " +
                        MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioChannelsId);
                }
            }
        }

        public void SyncMenu()
        {
            // sync the Audio Outputs menu
            ToolStripMenuItem soundDevicesMenuItem = Form1.Instance.soundDevicesToolStripMenuItem;

            for (int i = 0; i < Player.AudioOutputs.Count; i++)
            {
                AudioOutput output = (AudioOutput)Player.AudioOutputs[i];
                if (output.Devices.Count > 0)
                {
                    ToolStripMenuItem mItem = new ToolStripMenuItem();
                    mItem.Name = output.RawOutput.psz_name;
                    mItem.Text = output.RawOutput.psz_description;
                    mItem.CheckOnClick = true;
                    mItem.Available = true;
                    mItem.Enabled = true;
                    mItem.Click += new EventHandler(mItem_Click);

                    ArrayList outputDevices = output.Devices;
                    for (int k = 0; k < outputDevices.Count; k++)
                    {
                        ToolStripMenuItem mDeviceItem = new ToolStripMenuItem();
                        mDeviceItem.Name = ((AudioDevice)outputDevices[k]).deviceId;
                        mDeviceItem.Text = ((AudioDevice)outputDevices[k]).deviceName;
                        mDeviceItem.Available = true;
                        mDeviceItem.Enabled = true;
                        mDeviceItem.CheckOnClick = true;
                        mDeviceItem.Click += new EventHandler(mDeviceItem_Click);
                        mItem.DropDownItems.Add(mDeviceItem);
                    }
                    soundDevicesMenuItem.DropDownItems.Add(mItem);
                }
            }

            SetCurrentActiveMenuItem(soundDevicesMenuItem, CurrentAudioOutputDeviceId.ToString());

            // sync the Audio Tracks menu
            ToolStripMenuItem audioTracksMenuItem = Form1.Instance.audioTracksToolStripMenuItem;
            ArrayList _audioTracks = _player.Media.AudioTracks;
            for (int a = 0; a < _audioTracks.Count; a++)
            {
                VLCLib.libvlc_track_description_t audioTrack = (VLCLib.libvlc_track_description_t)_audioTracks[a];
                ToolStripMenuItem aItem = new ToolStripMenuItem();
                aItem.Name = audioTrack.i_id.ToString();
                aItem.Text = audioTrack.psz_name;
                aItem.CheckOnClick = true;
                aItem.Click += new EventHandler(aItem_Click);
                audioTracksMenuItem.DropDownItems.Add(aItem);
            }
            SetCurrentActiveMenuItem(audioTracksMenuItem, VLCLib.libvlc_audio_get_track(_player.Handle).ToString());

            // sync the Subtitles menu
            ToolStripMenuItem subtitlesMenuItem = Form1.Instance.subtitlesToolStripMenuItem;
            ArrayList _subtitleTracks = _player.Media.SubtitleTracks;
            for (int j = 0; j < _subtitleTracks.Count; j++)
            {
                VLCLib.libvlc_track_description_t subtitleTrack = (VLCLib.libvlc_track_description_t)_subtitleTracks[j];
                ToolStripMenuItem subtitleMenu = new ToolStripMenuItem();
                subtitleMenu.Name = subtitleTrack.i_id.ToString();
                subtitleMenu.Text = subtitleTrack.psz_name;
                subtitleMenu.CheckOnClick = true;
                subtitleMenu.Click += new EventHandler(subtitleMenu_Click);
                subtitlesMenuItem.DropDownItems.Add(subtitleMenu);
            }

            if (_subtitleTracks.Count > 0)
                SetCurrentActiveMenuItem(subtitlesMenuItem, CurrentSubtitleTrackId.ToString());

            // sync aspect ratio menu
            ToolStripMenuItem apItem = Form1.Instance.aspectRatioToolStripMenuItem;
            for (int i = 0; i < Player.AspectRatios.Count; i++)
            {
                String aRatio = (string)Player.AspectRatios[i];
                ToolStripMenuItem mItem = new ToolStripMenuItem();
                mItem.Name = i.ToString();
                mItem.Text = aRatio;
                mItem.CheckOnClick = true;
                mItem.Available = true;
                mItem.Enabled = true;
                mItem.Click += new EventHandler(aspectRatio_Click);
                apItem.DropDownItems.Add(mItem);
            }
            SetCurrentActiveMenuItem(apItem, "0");
        }

        void aspectRatio_Click(object sender, EventArgs e)
        {
            string newRatio = ((ToolStripMenuItem)sender).Text;
            Logger.WriteToLog("Changing Aspect Ratio to " + newRatio);
            VLCLib.libvlc_video_set_aspect_ratio(_player.Handle, newRatio);
            int newValue = Form1.Instance.aspectRatioToolStripMenuItem.DropDownItems.IndexOf((ToolStripMenuItem)sender);
            CurrentAspectRatio = newValue;
            ToolStripMenuItem parent = (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem;
            SetCurrentActiveMenuItem(parent, newValue.ToString());
        }
        void aItem_Click(object sender, EventArgs e)
        {
            string id = ((ToolStripMenuItem)sender).Name;
            Logger.WriteToLog("Chaning Audio Track to: " + ((ToolStripMenuItem)sender).Text);
            VLCLib.libvlc_audio_set_track(_player.Handle, Convert.ToInt32(id));
            CurrentAudioTrack = Convert.ToInt32(id);
            ToolStripMenuItem parent = (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem;
            SetCurrentActiveMenuItem(parent, id);
        }
        void mItem_Click(object sender, EventArgs e)
        {
            string name = ((ToolStripMenuItem)sender).Text;
            Logger.WriteToLog("Changing Audio Output to: " + name);
            VLCLib.libvlc_audio_output_set(_player.Handle, name);
            CurrentAudioOutput = name;
            ToolStripMenuItem parent = (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem;
            SetCurrentActiveMenuItem(parent, name);
        }
        void mDeviceItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            String deviceId = menuItem.Name;

            ToolStripMenuItem parentItem = (ToolStripMenuItem)menuItem.OwnerItem;
            String audioOutputName = parentItem.Name;

            Logger.WriteToLog("Changing Audio Output to: " + audioOutputName);
            VLCLib.libvlc_audio_output_set(_player.Handle, audioOutputName);
            CurrentAudioOutput = audioOutputName;
            Logger.WriteToLog("Changing Audio Output Device to: " + menuItem.Text);
            VLCLib.libvlc_audio_output_device_set(_player.Handle, audioOutputName, deviceId);
            CurrentAudioOutputDeviceId = Convert.ToInt32(deviceId);
            ToolStripMenuItem parent = (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem;
            SetCurrentActiveMenuItem(parent, deviceId);
        }
        void subtitleMenu_Click(object sender, EventArgs e)
        {
            string id = ((ToolStripMenuItem)sender).Name;
            Logger.WriteToLog("Changing Subtitle track to: " + ((ToolStripMenuItem)sender).Text);
            VLCLib.libvlc_video_set_spu(_player.Handle, Convert.ToUInt32(id));
            CurrentSubtitleTrackId = Convert.ToInt32(id);
            ToolStripMenuItem parent = (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem;
            SetCurrentActiveMenuItem(parent, id.ToString());
        }

        private void SetCurrentActiveMenuItem(ToolStripMenuItem mItem, String currentValue)
        {
            for (int i = 0; i < mItem.DropDownItems.Count; i++)
            {
                ToolStripMenuItem subItem = (ToolStripMenuItem)mItem.DropDownItems[i];
                subItem.Checked = String.Compare(subItem.Name, currentValue) == 0 ? true : false;
            }
            Form1.Instance.Refresh();
        }
        public void seventPointOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(_player.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_7_1);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_7_1;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }
        public void sixPointOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(_player.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_6_1);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_6_1;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }
        public void fivePointOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(_player.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_5_1);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_5_1;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }
        public void stereoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(_player.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_Stereo);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_Stereo;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }
        public void monoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(_player.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_Mono);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_Mono;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }
        public void spdifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(_player.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_SPDIF);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_SPDIF;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }
        public void saveAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int audioChannelId = VLCLib.libvlc_audio_get_channel(_player.Handle);
            int audioDeviceId = VLCLib.libvlc_audio_output_get_device_type(_player.Handle);
            SettingsController.Instance.SaveSettings();
        }
        public void disabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_player.Media.SubtitleTracks.Count > 0)
            {
                VLCLib.libvlc_video_set_spu(_player.Handle, 0);
                SettingsController.Instance.CurrentSubtitleTrackId = 0;
                ToolStripMenuItem disabledItem = (ToolStripMenuItem)Form1.Instance.subtitlesToolStripMenuItem.DropDownItems[0];
                disabledItem.Checked = true;
                SettingsController.Instance.SetCurrentActiveMenuItem((ToolStripMenuItem)sender, "0");
            }
        }
    }
}
