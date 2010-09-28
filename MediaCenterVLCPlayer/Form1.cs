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

            // init VLC
            try
            {
                instance = new VlcInstance(args);
            }
            catch (VlcException e)
            {

            }

            IntPtr logger = VLCLib.libvlc_log_open(instance.Handle);
            VLCLib.libvlc_set_log_verbosity(instance.Handle, 10);

            player = instance.CreatePlayer(mediafile);
            player.Drawable = panelHandle;
            // give the remote control lib access to the active panel so it gets keyboard/mouse/remote events
            remoteManager.playerHandle = player.Handle;

            IntPtr p_event_manager = VLCLib.libvlc_media_player_event_manager(player.Handle);
            eventmanager = new VlcEventManager(p_event_manager);
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
            settings.LoadSettings();
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
    }
}
