﻿using System;
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
        public Dictionary<int, VLCLib.libvlc_audio_output_t> _audioOutputs;
        public Dictionary<int, AudioDevice[]> _audioOutputDevices;
        public Dictionary<int, String> _aspectRations;

        private int _currentAudioDeviceId;
        private int _currentAudioChannelId;
        private int _currentSubtitleTrackId;
        private int _currentAudioTrack;
        private int _currentAspectRatio;
        private String _currentAudioOutput;


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

        public void LoadSettings()
        {
            _loadAudioOutputs();
            _loadAudioOutputDevices();
            _loadAudioTracks();
            _loadSubtitles();
            _loadAspectRatios();
        }

        private void _loadAspectRatios()
        {
            Logger.WriteToLog("Loading Aspect Ratios");
            try
            {
                _aspectRations = new Dictionary<int, string>();

                // current video aspect ratio
                int i = 0;
                _aspectRations.Add(i++, VlcMedia.Instance.AspectRatio);
                _aspectRations.Add(i++, VLCLib.AspectRatio.FullScreen);
                _aspectRations.Add(i++, VLCLib.AspectRatio.WideScreen);
                _aspectRations.Add(i++, VLCLib.AspectRatio.DVD);
                _aspectRations.Add(i++, VLCLib.AspectRatio.Cinemascope);
            }
            catch (Exception e)
            {
                Logger.WriteToLog("Error adding aspect ratios: " + e.Message);
            }
        }
        private void _loadAudioOutputs()
        {
        }
        private void _loadAudioTracks()
        {
        }
        private void _loadSubtitles()
        {
        }
        private void _loadAudioOutputDevices()
        {
        }

        public void ApplyDefaultSettings()
        {
            Logger.WriteToLog("Setting default settings for media");
            if (MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioDeviceId >= 0)
            {
                try
                {
                    VLCLib.libvlc_audio_output_set_device_type(VlcMediaPlayer.Handle,
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
                    VLCLib.libvlc_audio_set_channel(VlcMediaPlayer.Handle,
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
            // activate the current audio channels
            int currentId = VLCLib.libvlc_audio_get_channel(VlcMediaPlayer.Handle);
            CurrentAudioChannelId = currentId;
            SetCurrentActiveMenuItem(Form1.Instance.audioChannelsToolStripMenuItem, Form1.Instance.audioChannelsToolStripMenuItem.DropDownItems[currentId].Name);

            // sync the Audio Tracks menu
            Logger.WriteToLog("Syncing Audio Tracks menu");
            ToolStripMenuItem audioTracksMenuItem = Form1.Instance.audioTracksToolStripMenuItem;
            ArrayList _audioTracks = VlcMedia.Instance.AudioTracks;
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
            SetCurrentActiveMenuItem(audioTracksMenuItem, VLCLib.libvlc_audio_get_track(VlcMediaPlayer.Handle).ToString());

            // sync the Audio Outputs menu
            Logger.WriteToLog("Syncing sound outputs menu");
            ToolStripMenuItem soundDevicesMenuItem = Form1.Instance.soundDevicesToolStripMenuItem;

            for (int i = 0; i < _audioOutputs.Count; i++)
            {
                VLCLib.libvlc_audio_output_t output = (VLCLib.libvlc_audio_output_t)_audioOutputs[i];
                ToolStripMenuItem mItem = new ToolStripMenuItem();
                mItem.Name = output.psz_name;
                mItem.Text = output.psz_description;
                mItem.CheckOnClick = true;
                mItem.Available = true;
                mItem.Enabled = true;
                mItem.Click += new EventHandler(mItem_Click);
                AudioDevice[] devices = _audioOutputDevices[i];
                Logger.WriteToLog("Adding " + devices.Length + " devices");
                for (int k = 0; k < devices.Length; k++)
                {
                    Logger.WriteToLog("Syncing sound devices for audio output: " + output.psz_description + ": Device: " + devices[k].deviceName);
                    ToolStripMenuItem mDeviceItem = new ToolStripMenuItem();
                    mDeviceItem.Name = devices[k].deviceId;
                    mDeviceItem.Text = devices[k].deviceName;
                    mDeviceItem.Available = true;
                    mDeviceItem.Enabled = true;
                    mDeviceItem.CheckOnClick = true;
                    mDeviceItem.Click += new EventHandler(mDeviceItem_Click);
                    mItem.DropDownItems.Add(mDeviceItem);
                }
                soundDevicesMenuItem.DropDownItems.Add(mItem);
                Logger.WriteToLog("Done Syncing audio output device: " + output.psz_description);
            }

            SetCurrentActiveMenuItem(soundDevicesMenuItem, CurrentAudioOutputDeviceId.ToString());

            // sync the Audio Subtitles menu
            Logger.WriteToLog("Syncing subtitles menu");
            ToolStripMenuItem subtitlesMenuItem = Form1.Instance.subtitlesToolStripMenuItem;
            ArrayList _subtitleTracks = VlcMedia.Instance.SubtitleTracks;
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

            Logger.WriteToLog("Syncing aspect ratios menu");
            ToolStripMenuItem apItem = Form1.Instance.aspectRatioToolStripMenuItem;
            for (int i = 0; i < _aspectRations.Count; i++)
            {
                String aRatio = _aspectRations[i];
                Logger.WriteToLog("Adding Aspect Ratio: " + aRatio);
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
            VLCLib.libvlc_video_set_aspect_ratio(VlcMediaPlayer.Handle, newRatio);
            int newValue = Form1.Instance.aspectRatioToolStripMenuItem.DropDownItems.IndexOf((ToolStripMenuItem)sender);
            CurrentAspectRatio = newValue;
            ToolStripMenuItem parent = (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem;
            SetCurrentActiveMenuItem(parent, newValue.ToString());
        }

        void aItem_Click(object sender, EventArgs e)
        {
            string id = ((ToolStripMenuItem)sender).Name;
            Logger.WriteToLog("Chaning Audio Track to: " + ((ToolStripMenuItem)sender).Text);
            VLCLib.libvlc_audio_set_track(VlcMediaPlayer.Handle, Convert.ToInt32(id));
            CurrentAudioTrack = Convert.ToInt32(id);
            ToolStripMenuItem parent = (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem;
            SetCurrentActiveMenuItem(parent, id);
        }

        void mItem_Click(object sender, EventArgs e)
        {
            string name = ((ToolStripMenuItem)sender).Text;
            Logger.WriteToLog("Changing Audio Output to: " + name);
            VLCLib.libvlc_audio_output_set(VlcMediaPlayer.Handle, name);
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
            VLCLib.libvlc_audio_output_set(VlcMediaPlayer.Handle, audioOutputName);
            CurrentAudioOutput = audioOutputName;
            Logger.WriteToLog("Changing Audio Output Device to: " + menuItem.Text);
            VLCLib.libvlc_audio_output_device_set(VlcMediaPlayer.Handle, audioOutputName, deviceId);
            CurrentAudioOutputDeviceId = Convert.ToInt32(deviceId);
            ToolStripMenuItem parent = (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem;
            SetCurrentActiveMenuItem(parent, deviceId);
        }

        void subtitleMenu_Click(object sender, EventArgs e)
        {
            string id = ((ToolStripMenuItem)sender).Name;
            Logger.WriteToLog("Changing Subtitle track to: " + ((ToolStripMenuItem)sender).Text);
            VLCLib.libvlc_video_set_spu(VlcMediaPlayer.Handle, Convert.ToUInt32(id));
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

        public static void seventPointOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(VlcMediaPlayer.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_7_1);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_7_1;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }

        public static void sixPointOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(VlcMediaPlayer.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_6_1);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_6_1;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }

        public static void fivePointOneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(VlcMediaPlayer.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_5_1);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_5_1;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }

        public static void stereoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(VlcMediaPlayer.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_Stereo);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_Stereo;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }

        public static void monoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(VlcMediaPlayer.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_Mono);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_Mono;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }

        public static void spdifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VLCLib.libvlc_audio_output_set_device_type(VlcMediaPlayer.Handle, (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_SPDIF);
            SettingsController.Instance.CurrentAudioOutputDeviceId = (int)VLCLib.libvlc_audio_output_device_type_t.libvlc_AudioOutputDevice_SPDIF;
            SettingsController.Instance.SetCurrentActiveMenuItem(
                (ToolStripMenuItem)((ToolStripMenuItem)sender).OwnerItem,
                ((ToolStripMenuItem)sender).Name);
        }

        public static void saveAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int audioChannelId = VLCLib.libvlc_audio_get_channel(VlcMediaPlayer.Handle);
            int audioDeviceId = VLCLib.libvlc_audio_output_get_device_type(VlcMediaPlayer.Handle);
            SettingsController.Instance.SaveSettings();
        }

        public static void disabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VlcMedia.Instance.SubtitleTracks.Count > 0)
            {
                VLCLib.libvlc_video_set_spu(VlcMediaPlayer.Handle, 0);
                SettingsController.Instance.CurrentSubtitleTrackId = 0;
                ToolStripMenuItem disabledItem = (ToolStripMenuItem)Form1.Instance.subtitlesToolStripMenuItem.DropDownItems[0];
                disabledItem.Checked = true;
                SettingsController.Instance.SetCurrentActiveMenuItem((ToolStripMenuItem)sender, "0");
            }
        }
    }
}