using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
        Logger myLogger;

        public Form1(string mediafile)
        {
            myLogger = new Logger();
            InitializeComponent();
            Form1.Instance = this;
            // set window location/sizing and force to top of z index
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Focus();
            this.TopMost = true;
            this.Refresh();

            // activate the remote control hooks
            myLogger.writeToLog("Creating new RemoteControlManager");
            remoteManager = new MCRemoteControlManager();
            remoteManager.form = this;
            remoteManager.AssignHandle(this.Handle);

            IntPtr panelHandle = panel1.Handle;

            string[] args = new string[] {
                "-I", "dummy", "--ignore-config",
                @"--plugin-path=" + MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath
            };

            // init VLC
            myLogger.writeToLog("Creating instance of VLC");
            instance = new VlcInstance(args);
            IntPtr logger = VLCLib.libvlc_log_open(VlcInstance.Handle);
            VLCLib.libvlc_set_log_verbosity(VlcInstance.Handle, 10);

            myLogger.writeToLog("Creating instance of VLC Media Player");
            player = instance.CreatePlayer(mediafile);
            player.Drawable = panelHandle;
            // give the remote control lib access to the active panel so it gets keyboard/mouse/remote events
            remoteManager.playerHandle = VlcMediaPlayer.Handle;

            myLogger.writeToLog("Activating event manager on VLC Media Player");
            IntPtr p_event_manager = VLCLib.libvlc_media_player_event_manager(VlcMediaPlayer.Handle);
            eventmanager = new VlcEventManager(p_event_manager);
            eventmanager.InitalizeEvents();
            myLogger.writeToLog("Setting media to play");
        }

        public void PlayMedia()
        {
            player.Play();
            System.Threading.Thread.Sleep(1 * 1000);
            while (VLCLib.libvlc_media_player_is_playing(VlcMediaPlayer.Handle) < 1)
            {
                System.Threading.Thread.Sleep(1 * 100);
            }
            myLogger.writeToLog("Loading settings now that the media is playing");
            // activate the settings controller
            settings = new SettingsController();
            settings.LoadSettings();
            myLogger.writeToLog("Syncing settings menu");
            settings.SyncMenu();
            myLogger.writeToLog("Loading default settings");
            settings.ApplyDefaultSettings();
        }

        public static void Closeapp()
        {
            if (VLCLib.libvlc_media_player_is_playing(VlcMediaPlayer.Handle) > 0)
            {
                VLCLib.libvlc_media_player_stop(VlcMediaPlayer.Handle);
            }

            if (VlcMediaPlayer.Handle != IntPtr.Zero)
            {
                VLCLib.libvlc_media_release(VlcMedia.Handle);
            }

            if (VlcInstance.Handle != IntPtr.Zero)
            {
                VLCLib.libvlc_release(VlcInstance.Handle);
            }

            if (Logger.Instance != null)
                Logger.Instance.Close();

            Environment.Exit(0);
        }

        public static void WriteMarquee(string text, int opacity, int fontsize, Color color, int timeout)
        {
            VLCLib.libvlc_video_set_marquee_string(VlcMediaPlayer.Handle, (uint)VLCLib.libvlc_video_marquee_option_t.libvlc_marquee_Text, text);
            VLCLib.libvlc_video_set_marquee_int(VlcMediaPlayer.Handle, (uint)VLCLib.libvlc_video_marquee_option_t.libvlc_marquee_Opacity, opacity);
            VLCLib.libvlc_video_set_marquee_int(VlcMediaPlayer.Handle, (uint)VLCLib.libvlc_video_marquee_option_t.libvlc_marquee_Size, fontsize);
            VLCLib.libvlc_video_set_marquee_int(VlcMediaPlayer.Handle, (uint)VLCLib.libvlc_video_marquee_option_t.libvlc_marquee_X, 50);
            VLCLib.libvlc_video_set_marquee_int(VlcMediaPlayer.Handle, (uint)VLCLib.libvlc_video_marquee_option_t.libvlc_marquee_Y, 20);
            VLCLib.libvlc_video_set_marquee_int(VlcMediaPlayer.Handle, (uint)VLCLib.libvlc_video_marquee_option_t.libvlc_marquee_Color, color.ToArgb() & 0x00ffffff);
            VLCLib.libvlc_video_set_marquee_int(VlcMediaPlayer.Handle, (uint)VLCLib.libvlc_video_marquee_option_t.libvlc_marquee_Timeout, timeout);
            VLCLib.libvlc_video_set_marquee_int(VlcMediaPlayer.Handle, (uint)VLCLib.libvlc_video_marquee_option_t.libvlc_marquee_Enable, 1);
        }

        public static void WriteMarquee(string text)
        {
            WriteMarquee(text, 100, 40, Color.White, 500);
        }
    }
}
