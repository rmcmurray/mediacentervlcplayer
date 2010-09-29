using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace MediaCenterVLCPlayer
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        public MCRemoteControlManager remoteManager;
        VlcInstance instance;
        VlcMediaPlayer player;
        VlcEventManager eventmanager;
        public SettingsController settings;

        public Form1(string mediafile)
        {
            InitializeComponent();
            Form1.Instance = this;
            // set window location/sizing and force to top of z index
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Focus();
            this.TopMost = true;
            this.Refresh();

            // activate the remote control hooks
            remoteManager = new MCRemoteControlManager();
            remoteManager.form = this;
            remoteManager.AssignHandle(this.Handle);

            IntPtr panelHandle = panel1.Handle;

            string[] args = new string[] {
                "-I", "dummy", "--ignore-config",
                @"--plugin-path=" + MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath
            };

            if (!System.IO.Directory.Exists(MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath))
            {
                DialogResult result = MessageBox.Show("Error: we do NOT have a valid location for the VLC plugins folder", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
            // init VLC
            try
            {
                instance = new VlcInstance(args);
            }
            catch (VlcException e)
            {
                MessageBox.Show("Error creating VLC Instance:" + e.Message);
                Environment.Exit(0);
            }

            player = instance.CreatePlayer(mediafile);
            player.Drawable = panelHandle;
            // give the remote control lib access to the active panel so it gets keyboard/mouse/remote events
            remoteManager.playerHandle = player.Handle;

            IntPtr p_event_manager = VLCLib.libvlc_media_player_event_manager(player.Handle);
            eventmanager = new VlcEventManager(instance, p_event_manager);
            eventmanager.InitalizeEvents();
        }

        public void PlayMedia()
        {
            player.Play();
            while (VLCLib.libvlc_media_player_is_playing(player.Handle) < 1)
            {
                System.Threading.Thread.Sleep(1 * 100);
            }
            // activate the settings controller
            settings = new SettingsController();
            settings.Player = player;
            settings.SyncMenu();
            settings.ApplyDefaultSettings();
        }

        public void Closeapp()
        {
            if (VLCLib.libvlc_media_player_is_playing(player.Handle) > 0)
            {
                VLCLib.libvlc_media_player_stop(player.Handle);
            }

            if (player.Handle != IntPtr.Zero)
            {
                VLCLib.libvlc_media_release(player.Media.Handle);
            }

            if (instance != null && instance.Handle != IntPtr.Zero)
            {
                VLCLib.libvlc_release(instance.Handle);
            }

            if (Logger.Instance != null)
                Logger.Instance.Close();

            Environment.Exit(0);
        }

        public static bool DetectVLC()
        {
            string dSeparator = Convert.ToString(Path.DirectorySeparatorChar);
            if (Directory.Exists(String.Join(dSeparator, new string[] { Environment.SpecialFolder.ProgramFiles.ToString(), String.Join(dSeparator, new string[] { "videolan", "vlc" }) })))
            {
                if (File.Exists(String.Join(dSeparator, new string[] { Environment.SpecialFolder.ProgramFiles.ToString(), String.Join(dSeparator, new string[] { "videolan", "vlc", "vlc.exe" }) })))
                {
                    if (Directory.Exists(String.Join(dSeparator, new string[] { Environment.SpecialFolder.ProgramFiles.ToString(), String.Join(dSeparator, new string[] { "videolan", "vlc", "plugins" }) })))
                    {
                        MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath
                            = String.Join(dSeparator, new string[] { Environment.SpecialFolder.ProgramFiles.ToString(), String.Join(dSeparator, new string[] { "videolan", "vlc", "plugins" }) });
                        MediaCenterVLCPlayer.Properties.Settings.Default.Save();
                        return true;
                    }
                }
            }
            else if (Directory.Exists(String.Join(dSeparator, new string[] { Environment.SpecialFolder.ProgramFilesX86.ToString(), String.Join(dSeparator, new string[] { "videolan", "vlc" }) })))
            {
                if (File.Exists(String.Join(dSeparator, new string[] { Environment.SpecialFolder.ProgramFilesX86.ToString(), String.Join(dSeparator, new string[] { "videolan", "vlc", "vlc.exe" }) })))
                {
                    if (Directory.Exists(String.Join(dSeparator, new string[] { Environment.SpecialFolder.ProgramFilesX86.ToString(), String.Join(dSeparator, new string[] { "videolan", "vlc", "plugins" }) })))
                    {
                        MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath
                            = String.Join(dSeparator, new string[] { Environment.SpecialFolder.ProgramFilesX86.ToString(), String.Join(dSeparator, new string[] { "videolan", "vlc", "plugins" }) });
                        MediaCenterVLCPlayer.Properties.Settings.Default.Save();
                        return true;
                    }
                }
            }
            return false;
        }

        public int SecondsForRewind
        {
            get { return MediaCenterVLCPlayer.Properties.Settings.Default.SecondsForRewind; }
            set { MediaCenterVLCPlayer.Properties.Settings.Default.SecondsForRewind = value; }
        }
        public int SecondsForFastForward
        {
            get { return MediaCenterVLCPlayer.Properties.Settings.Default.SecondsForFastForward; }
            set { MediaCenterVLCPlayer.Properties.Settings.Default.SecondsForFastForward = value; }
        }
        public int DefaultAudioChannelsId
        {
            get { return MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioChannelsId; }
            set { MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioChannelsId = value; }
        }
        public int DefaultAudioDeviceId
        {
            get { return MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioDeviceId; }
            set { MediaCenterVLCPlayer.Properties.Settings.Default.DefaultAudioDeviceId = value; }
        }
        public void SaveDefaultSettings()
        {
            MediaCenterVLCPlayer.Properties.Settings.Default.Save();
        }
    }
}
