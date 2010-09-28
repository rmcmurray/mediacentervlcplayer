using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;

namespace MediaCenterVLCPlayer
{
    public class MCRemoteControlManager : NativeWindow
    {
        private enum RemoteKeys
        {
            WM_PLAY_PAUSE = 179,
            WM_STOP = 178,
            WM_PREVIOUS = 177,
            WM_NEXT = 176,
            WM_VOLUME_UP = 175,
            WM_VOLUME_DOWN = 174,
            WM_MUTE = 173,
            WM_FORWARD = 167,
            WM_BACK = 166,
            WM_PAUSE = 110,
            APPCOMMAND_MEDIA_CHANNEL_DOWN = 52,
            APPCOMMAND_MEDIA_CHANNEL_UP = 51,
            APPCOMMAND_MEDIA_REWIND = 50,
            APPCOMMAND_MEDIA_FAST_FORWARD = 49,
            APPCOMMAND_MEDIA_RECORD = 48,
            APPCOMMAND_MEDIA_PAUSE = 47,
            APPCOMMAND_MEDIA_PLAY = 46,
            WM_DOWN = 40,
            WM_RIGHT = 39,
            WM_UP = 38,
            WM_LEFT = 37,
            WM_CLEAR = 27,
            APPCOMMAND_MEDIA_SELECT = 16,
            APPCOMMAND_MEDIA_PLAY_PAUSE = 14,
            WM_ENTER = 13,
            //APPCOMMAND_MEDIA_STOP          = 13,
            APPCOMMAND_MEDIA_PREVIOUSTRACK = 12,
            APPCOMMAND_MEDIA_NEXTTRACK = 11,
            APPCOMMAND_VOLUME_MUTE = 10,
            APPCOMMAND_VOLUME_DOWN = 9,
            APPCOMMAND_VOLUME_UP = 8,
            WM_KEYDOWN = 0x0100,
            WM_APPCOMMAND = 0x0319
        }

        private int iChar;
        public Form1 form;
        public IntPtr playerHandle;
        private int lastiChar;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)RemoteKeys.WM_APPCOMMAND)
            {
                iChar = (int)((uint)m.LParam >> 16 & ~0xf000);
                switch (iChar)
                {
                    case (int)RemoteKeys.APPCOMMAND_MEDIA_PAUSE:
                        VLCLib.libvlc_media_player_pause(playerHandle);
//                        Form1.WriteMarquee("Pause");
                        lastiChar = (int)RemoteKeys.APPCOMMAND_MEDIA_PAUSE;
                        return;
                    case (int)RemoteKeys.APPCOMMAND_MEDIA_PLAY_PAUSE:
                        VLCLib.libvlc_media_player_pause(playerHandle);
                        lastiChar = (int)RemoteKeys.APPCOMMAND_MEDIA_PLAY_PAUSE;
                        return;
                    case (int)RemoteKeys.APPCOMMAND_MEDIA_FAST_FORWARD:
                        if (VLCLib.libvlc_media_player_is_seekable(playerHandle) > 0)
                        {
                            VLCLib.libvlc_media_player_set_time(playerHandle, VLCLib.libvlc_media_player_get_time(playerHandle)
                                + MilliSecondsFromSettings(Properties.Settings.Default.SecondsForFastForward));
                        }
                        lastiChar = (int)RemoteKeys.APPCOMMAND_MEDIA_FAST_FORWARD;
                        return;
                    case (int)RemoteKeys.APPCOMMAND_MEDIA_REWIND:
                        if (VLCLib.libvlc_media_player_is_seekable(playerHandle) > 0)
                        {
                            VLCLib.libvlc_media_player_set_time(playerHandle, VLCLib.libvlc_media_player_get_time(playerHandle)
                                - MilliSecondsFromSettings(Properties.Settings.Default.SecondsForFastForward));
                        }
                        lastiChar = (int)RemoteKeys.APPCOMMAND_MEDIA_REWIND;
                        return;
                    case (int)RemoteKeys.APPCOMMAND_MEDIA_PLAY:
                        VLCLib.libvlc_media_player_play(playerHandle);
                        lastiChar = (int)RemoteKeys.APPCOMMAND_MEDIA_PLAY;
                        return;
                    default:
                        lastiChar = iChar;
                        break;
                }
            }
            if (m.Msg == (int)RemoteKeys.WM_KEYDOWN)
            {
                iChar = m.WParam.ToInt32();

                switch (iChar)
                {
                    case (int)RemoteKeys.WM_STOP:
                        VLCLib.libvlc_media_player_stop(playerHandle);
                        lastiChar = (int)RemoteKeys.WM_STOP;
                        return;
                    case (int)RemoteKeys.WM_PREVIOUS:
                        if (VLCLib.libvlc_media_player_is_seekable(playerHandle) > 0)
                        {
                            VLCLib.libvlc_media_player_set_time(playerHandle, VLCLib.libvlc_media_player_get_time(playerHandle)
                                - MilliSecondsFromSettings(Properties.Settings.Default.SecondsForFastForward));
                        }
                        lastiChar = (int)RemoteKeys.WM_PREVIOUS;
                        return;
                    case (int)RemoteKeys.WM_NEXT:
                        if (VLCLib.libvlc_media_player_is_seekable(playerHandle) > 0)
                        {
                            VLCLib.libvlc_media_player_set_time(playerHandle, VLCLib.libvlc_media_player_get_time(playerHandle)
                                + MilliSecondsFromSettings(Properties.Settings.Default.SecondsForFastForward));
                        }
                        lastiChar = (int)RemoteKeys.WM_NEXT;
                        return;
                    case (int)RemoteKeys.WM_FORWARD:
                        if (VLCLib.libvlc_media_player_is_seekable(playerHandle) > 0)
                        {
                            VLCLib.libvlc_media_player_set_time(playerHandle, VLCLib.libvlc_media_player_get_time(playerHandle)
                                + MilliSecondsFromSettings(Properties.Settings.Default.SecondsForFastForward));
                        }
                        lastiChar = (int)RemoteKeys.WM_FORWARD;
                        return;
                    case (int)RemoteKeys.WM_VOLUME_UP:
                        if (playerHandle != IntPtr.Zero)
                        {
                            int volume = VLCLib.libvlc_audio_get_volume(playerHandle);
                            volume++;
                            VLCLib.libvlc_audio_set_volume(playerHandle, volume);
//                            Form1.WriteMarquee("Volume: " + volume.ToString());
                        }
                        lastiChar = (int)RemoteKeys.WM_VOLUME_UP;
                        return;
                    case (int)RemoteKeys.WM_VOLUME_DOWN:
                        if (playerHandle != IntPtr.Zero)
                        {
                            int volume = VLCLib.libvlc_audio_get_volume(playerHandle);
                            volume--;

                            VLCLib.libvlc_audio_set_volume(playerHandle, volume);
//                            Form1.WriteMarquee("Volume: " + volume.ToString());
                        }
                        lastiChar = (int)RemoteKeys.WM_VOLUME_DOWN;
                        return;
                    case (int)RemoteKeys.WM_MUTE:
                        if (playerHandle != IntPtr.Zero)
                        {
                            VLCLib.libvlc_audio_toggle_mute(playerHandle);
                        }
                        lastiChar = (int)RemoteKeys.WM_MUTE;
                        return;
                    case (int)RemoteKeys.WM_BACK:
                        lastiChar = (int)RemoteKeys.WM_BACK;
                        Form1.Instance.Closeapp();
                        return;
                    case (int)Keys.Escape:
                        lastiChar = (int)Keys.Escape;
                        Form1.Instance.Closeapp();
                        return;
                    case (int)RemoteKeys.WM_ENTER:
                        VLCLib.libvlc_media_player_play(playerHandle);
                        lastiChar = (int)RemoteKeys.WM_ENTER;
                        return;
                    case (int)RemoteKeys.WM_PLAY_PAUSE:
                        VLCLib.libvlc_media_player_pause(playerHandle);
                        lastiChar = (int)RemoteKeys.WM_PLAY_PAUSE;
                        return;
                    case (int)Keys.P:
                        if (lastiChar == (int)Keys.Control || lastiChar == (int)Keys.ControlKey)
                        {
                            VLCLib.libvlc_media_player_pause(playerHandle);
//                            Form1.WriteMarquee("Pause");
                        }
                        lastiChar = (int)RemoteKeys.WM_PAUSE;
                        return;
                    case (int)Keys.D0:
                        Form1.Instance.Options.Visible = true;
                        Form1.Instance.soundDevicesToolStripMenuItem.Select();
                        lastiChar = (int)Keys.D0;
                        return;
                    default:
                        lastiChar = iChar;
                        break;
                }
            }
            base.WndProc(ref m);
        }

        private int MilliSecondsFromSettings(int seconds)
        {
            return (int)TimeSpan.FromSeconds((double)seconds).TotalMilliseconds;
        }
    }
}
