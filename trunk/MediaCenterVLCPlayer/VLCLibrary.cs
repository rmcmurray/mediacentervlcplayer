using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MediaCenterVLCPlayer
{
    public class VLCLibrary
    {
        IntPtr pLibVlcCoreDll = IntPtr.Zero;
        IntPtr pLibVlcDll = IntPtr.Zero;
        string libvlc_dll_path = string.Empty;
        string libvlccore_dll_path = string.Empty;

        public static VLCLibrary Instance;
        #region delegate instances
        libvlc_new dlibvlc_new;
        libvlc_release dlibvlc_release;
        libvlc_media_new_location dlibvlc_media_new_location;
        libvlc_media_new_path dlibvlc_media_new_path;
        libvlc_media_release dlibvlc_media_release;
        libvlc_media_player_new_from_media dlibvlc_media_player_new_from_media;
        libvlc_media_player_release dlibvlc_media_player_release;
        libvlc_media_player_set_hwnd dlibvlc_media_player_set_hwnd;
        libvlc_media_player_get_media dlibvlc_media_player_get_media;
        libvlc_media_player_set_media dlibvlc_media_player_set_media;
        libvlc_media_player_play dlibvlc_media_player_play;
        libvlc_media_player_is_playing dlibvlc_media_player_is_playing;
        libvlc_media_player_pause dlibvlc_media_player_pause;
        libvlc_media_player_stop dlibvlc_media_player_stop;
        libvlc_media_player_get_state dlibvlc_media_player_get_state;
        libvlc_media_player_is_seekable dlibvlc_media_player_is_seekable;
        libvlc_media_player_get_time dlibvlc_media_player_get_time;
        libvlc_media_player_set_time dlibvlc_media_player_set_time;
        libvlc_media_player_get_length dlibvlc_media_player_get_length;
        libvlc_media_player_get_position dlibvlc_media_player_get_position;
        libvlc_media_player_set_position dlibvlc_media_player_set_position;
        libvlc_media_player_set_chapter dlibvlc_media_player_set_chapter;
        libvlc_media_player_get_chapter dlibvlc_media_player_get_chapter;
        libvlc_media_player_get_chapter_count dlibvlc_media_player_get_chapter_count;
        libvlc_media_player_get_chapter_count_for_title dlibvlc_media_player_get_chapter_count_for_title;
        libvlc_media_player_set_title dlibvlc_media_player_set_title;
        libvlc_media_player_previous_chapter dlibvlc_media_player_previous_chapter;
        libvlc_media_player_next_chapter dlibvlc_media_player_next_chapter;
        libvlc_media_player_next_frame dlibvlc_media_player_next_frame;
        libvlc_audio_toggle_mute dlibvlc_audio_toggle_mute;
        libvlc_audio_get_volume dlibvlc_audio_get_volume;
        libvlc_audio_set_volume dlibvlc_audio_set_volume;
        libvlc_clearerr dlibvlc_clearerr;
        libvlc_errmsg dlibvlc_errmsg;
        libvlc_media_player_event_manager dlibvlc_media_player_event_manager;
        libvlc_event_attach dlibvlc_event_attach;
        libvlc_event_detach dlibvlc_event_detach;
        libvlc_event_type_name dlibvlc_event_type_name;
        libvlc_log_open dlibvlc_log_open;
        libvlc_log_close dlibvlc_log_close;
        libvlc_set_log_verbosity dlibvlc_set_log_verbosity;
        libvlc_log_count dlibvlc_log_count;
        libvlc_video_get_marquee_int dlibvlc_video_get_marquee_int;
        libvlc_video_marquee_string dlibvlc_video_marquee_string;
        libvlc_video_set_marquee_int dlibvlc_video_set_marquee_int;
        libvlc_video_set_marquee_string dlibvlc_video_set_marquee_string;
        libvlc_video_get_spu dlibvlc_video_get_spu;
        libvlc_video_get_spu_count dlibvlc_video_get_spu_count;
        libvlc_video_get_spu_description dlibvlc_video_get_spu_description;
        libvlc_video_set_spu dlibvlc_video_set_spu;
        libvlc_video_set_subtitle_file dlibvlc_video_set_subtitle_file;
        libvlc_toggle_fullscreen dlibvlc_toggle_fullscreen;
        libvlc_set_fullscreen dlibvlc_set_fullscreen;
        libvlc_get_fullscreen dlibvlc_get_fullscreen;
        libvlc_video_get_size dlibvlc_video_get_size;
        libvlc_video_get_scale dlibvlc_video_get_scale;
        libvlc_video_set_scale dlibvlc_video_set_scale;
        libvlc_video_get_aspect_ratio dlibvlc_video_get_aspect_ratio;
        libvlc_video_set_aspect_ratio dlibvlc_video_set_aspect_ratio;
        libvlc_video_get_title_description dlibvlc_video_get_title_description;
        libvlc_video_get_chapter_description dlibvlc_video_get_chapter_description;
        libvlc_video_get_track_count dlibvlc_video_get_track_count;
        libvlc_video_get_track_description dlibvlc_video_get_track_description;
        libvlc_video_get_track dlibvlc_video_get_track;
        libvlc_video_set_track dlibvlc_video_set_track;
        libvlc_video_take_snapshot dlibvlc_video_take_snapshot;
        libvlc_video_set_deinterlace dlibvlc_video_set_deinterlace;
        libvlc_audio_output_list_get dlibvlc_audio_output_list_get;
        libvlc_audio_output_list_release dlibvlc_audio_output_list_release;
        libvlc_audio_output_set dlibvlc_audio_output_set;
        libvlc_audio_output_device_count dlibvlc_audio_output_device_count;
        libvlc_audio_output_device_longname dlibvlc_audio_output_device_longname;
        libvlc_audio_output_device_id dlibvlc_audio_output_device_id;
        libvlc_audio_output_device_set dlibvlc_audio_output_device_set;
        libvlc_audio_output_get_device_type dlibvlc_audio_output_get_device_type;
        libvlc_audio_output_set_device_type dlibvlc_audio_output_set_device_type;
        libvlc_audio_get_track_count dlibvlc_audio_get_track_count;
        libvlc_audio_get_track_description dlibvlc_audio_get_track_description;
        libvlc_audio_get_track dlibvlc_audio_get_track;
        libvlc_audio_set_track dlibvlc_audio_set_track;
        libvlc_audio_get_channel dlibvlc_audio_get_channel;
        libvlc_audio_set_channel dlibvlc_audio_set_channel;
        libvlc_audio_get_delay dlibvlc_audio_get_delay;
        libvlc_audio_set_delay dlibvlc_audio_set_delay;
        libvlc_video_get_logo_int dlibvlc_video_get_logo_int;
        libvlc_video_set_logo_int dlibvlc_video_set_logo_int;
        libvlc_video_set_logo_string dlibvlc_video_set_logo_string;
        libvlc_track_description_release dlibvlc_track_description_release;

        #endregion

        public VLCLibrary()
        {
            VLCLibrary.Instance = this;
        }

        ~VLCLibrary()
        {
            VLCLibrary.Instance = null;
            MSMethods.FreeLibrary(pLibVlcDll);
        }

        public bool DetectVLC()
        {
            string sProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string sProgramFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
             
            string dSeparator = Convert.ToString(Path.DirectorySeparatorChar);

            if (Directory.Exists(String.Join(dSeparator, new string[] { sProgramFiles, String.Join(dSeparator, new string[] { "videolan", "vlc" }) })))
            {
                libvlccore_dll_path = string.Join(dSeparator, new string[] { sProgramFiles, "videolan", "vlc", "libvlccore.dll" });
                libvlc_dll_path = string.Join(dSeparator, new string[] { sProgramFiles, "videolan", "vlc", "libvlc.dll" });
                MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath
                    = string.Join(dSeparator, new string[] { sProgramFiles, "videolan", "vlc", "plugins" });
                MediaCenterVLCPlayer.Properties.Settings.Default.Save();
                return true;
            }
            else if (Directory.Exists(String.Join(dSeparator, new string[] { sProgramFilesX86, String.Join(dSeparator, new string[] { "videolan", "vlc" }) })))
            {
                libvlccore_dll_path = string.Join(dSeparator, new string[] { sProgramFiles, "videolan", "vlc", "libvlccore.dll" });
                libvlc_dll_path = string.Join(dSeparator, new string[] { sProgramFiles, "videolan", "vlc", "libvlc.dll" });
                MediaCenterVLCPlayer.Properties.Settings.Default.VlcPluginsPath
                    = string.Join(dSeparator, new string[] { sProgramFilesX86, "videolan", "vlc", "plugins" });
                MediaCenterVLCPlayer.Properties.Settings.Default.Save();
                return true;
            }
            return false;
        }

        public void SetupVLC()
        {
            if (pLibVlcDll == IntPtr.Zero)
            {
                pLibVlcCoreDll = MSMethods.LoadLibrary(libvlccore_dll_path);
                pLibVlcDll = MSMethods.LoadLibrary(libvlc_dll_path);
                ConnectFunctionDelegates();
            }
        }

        void ConnectFunctionDelegates()
        {
            IntPtr pAddressOf_libvlc_new = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_new");
            dlibvlc_new = (libvlc_new)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_new, typeof(libvlc_new));

            IntPtr pAddressOf_libvlc_release = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_release");
            dlibvlc_release = (libvlc_release)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_release, typeof(libvlc_release));

            IntPtr pAddressOf_libvlc_media_new_location = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_new_location");
            dlibvlc_media_new_location = (libvlc_media_new_location)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_new_location, typeof(libvlc_media_new_location));

            IntPtr pAddressOf_libvlc_media_new_path = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_new_path");
            dlibvlc_media_new_path = (libvlc_media_new_path)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_new_path, typeof(libvlc_media_new_path));

            IntPtr pAddressOf_libvlc_media_release = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_release");
            dlibvlc_media_release = (libvlc_media_release)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_release, typeof(libvlc_media_release));

            IntPtr pAddressOf_libvlc_media_player_new_from_media = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_new_from_media");
            dlibvlc_media_player_new_from_media = (libvlc_media_player_new_from_media)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_new_from_media, typeof(libvlc_media_player_new_from_media));

            IntPtr pAddressOf_libvlc_media_player_release = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_release");
            dlibvlc_media_player_release = (libvlc_media_player_release)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_release, typeof(libvlc_media_player_release));

            IntPtr pAddressOf_libvlc_media_player_set_hwnd = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_set_hwnd");
            dlibvlc_media_player_set_hwnd = (libvlc_media_player_set_hwnd)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_set_hwnd, typeof(libvlc_media_player_set_hwnd));

            IntPtr pAddressOf_libvlc_media_player_get_media = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_get_media");
            dlibvlc_media_player_get_media = (libvlc_media_player_get_media)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_get_media, typeof(libvlc_media_player_get_media));

            IntPtr pAddressOf_libvlc_media_player_set_media = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_set_media");
            dlibvlc_media_player_set_media = (libvlc_media_player_set_media)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_set_media, typeof(libvlc_media_player_set_media));

            IntPtr pAddressOf_libvlc_media_player_play = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_play");
            dlibvlc_media_player_play = (libvlc_media_player_play)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_play, typeof(libvlc_media_player_play));

            IntPtr pAddressOf_libvlc_media_player_is_playing = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_is_playing");
            dlibvlc_media_player_is_playing = (libvlc_media_player_is_playing)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_is_playing, typeof(libvlc_media_player_is_playing));

            IntPtr pAddressOf_libvlc_media_player_pause = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_pause");
            dlibvlc_media_player_pause = (libvlc_media_player_pause)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_pause, typeof(libvlc_media_player_pause));

            IntPtr pAddressOf_libvlc_media_player_stop = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_stop");
            dlibvlc_media_player_stop = (libvlc_media_player_stop)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_stop, typeof(libvlc_media_player_stop));

            IntPtr pAddressOf_libvlc_media_player_get_state = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_get_state");
            dlibvlc_media_player_get_state = (libvlc_media_player_get_state)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_get_state, typeof(libvlc_media_player_get_state));

            IntPtr pAddressOf_libvlc_media_player_is_seekable = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_is_seekable");
            dlibvlc_media_player_is_seekable = (libvlc_media_player_is_seekable)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_is_seekable, typeof(libvlc_media_player_is_seekable));

            IntPtr pAddressOf_libvlc_media_player_get_time = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_get_time");
            dlibvlc_media_player_get_time = (libvlc_media_player_get_time)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_get_time, typeof(libvlc_media_player_get_time));

            IntPtr pAddressOf_libvlc_media_player_set_time = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_set_time");
            dlibvlc_media_player_set_time = (libvlc_media_player_set_time)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_set_time, typeof(libvlc_media_player_set_time));

            IntPtr pAddressOf_libvlc_media_player_get_length = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_get_length");
            dlibvlc_media_player_get_length = (libvlc_media_player_get_length)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_get_length, typeof(libvlc_media_player_get_length));

            IntPtr pAddressOf_libvlc_media_player_get_position = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_get_position");
            dlibvlc_media_player_get_position = (libvlc_media_player_get_position)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_get_position, typeof(libvlc_media_player_get_position));

            IntPtr pAddressOf_libvlc_media_player_set_position = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_set_position");
            dlibvlc_media_player_set_position = (libvlc_media_player_set_position)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_set_position, typeof(libvlc_media_player_set_position));

            IntPtr pAddressOf_libvlc_media_player_set_chapter = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_set_chapter");
            dlibvlc_media_player_set_chapter = (libvlc_media_player_set_chapter)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_set_chapter, typeof(libvlc_media_player_set_chapter));

            IntPtr pAddressOf_libvlc_media_player_get_chapter = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_get_chapter");
            dlibvlc_media_player_get_chapter = (libvlc_media_player_get_chapter)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_get_chapter, typeof(libvlc_media_player_get_chapter));

            IntPtr pAddressOf_libvlc_media_player_get_chapter_count = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_get_chapter_count");
            dlibvlc_media_player_get_chapter_count = (libvlc_media_player_get_chapter_count)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_get_chapter_count, typeof(libvlc_media_player_get_chapter_count));

            IntPtr pAddressOf_libvlc_media_player_get_chapter_count_for_title = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_get_chapter_count_for_title");
            dlibvlc_media_player_get_chapter_count_for_title = (libvlc_media_player_get_chapter_count_for_title)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_get_chapter_count_for_title, typeof(libvlc_media_player_get_chapter_count_for_title));

            IntPtr pAddressOf_libvlc_media_player_set_title = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_set_title");
            dlibvlc_media_player_set_title = (libvlc_media_player_set_title)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_set_title, typeof(libvlc_media_player_set_title));

            IntPtr pAddressOf_libvlc_media_player_previous_chapter = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_previous_chapter");
            dlibvlc_media_player_previous_chapter = (libvlc_media_player_previous_chapter)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_previous_chapter, typeof(libvlc_media_player_previous_chapter));

            IntPtr pAddressOf_libvlc_media_player_next_chapter = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_next_chapter");
            dlibvlc_media_player_next_chapter = (libvlc_media_player_next_chapter)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_next_chapter, typeof(libvlc_media_player_next_chapter));

            IntPtr pAddressOf_libvlc_media_player_next_frame = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_next_frame");
            dlibvlc_media_player_next_frame = (libvlc_media_player_next_frame)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_next_frame, typeof(libvlc_media_player_next_frame));

            IntPtr pAddressOf_libvlc_audio_toggle_mute = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_toggle_mute");
            dlibvlc_audio_toggle_mute = (libvlc_audio_toggle_mute)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_toggle_mute, typeof(libvlc_audio_toggle_mute));

            IntPtr pAddressOf_libvlc_audio_get_volume = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_get_volume");
            dlibvlc_audio_get_volume = (libvlc_audio_get_volume)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_get_volume, typeof(libvlc_audio_get_volume));

            IntPtr pAddressOf_libvlc_audio_set_volume = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_set_volume");
            dlibvlc_audio_set_volume = (libvlc_audio_set_volume)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_set_volume, typeof(libvlc_audio_set_volume));

            IntPtr pAddressOf_libvlc_clearerr = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_clearerr");
            dlibvlc_clearerr = (libvlc_clearerr)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_clearerr, typeof(libvlc_clearerr));

            IntPtr pAddressOf_libvlc_errmsg = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_errmsg");
            dlibvlc_errmsg = (libvlc_errmsg)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_errmsg, typeof(libvlc_errmsg));

            IntPtr pAddressOf_libvlc_media_player_event_manager = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_media_player_event_manager");
            dlibvlc_media_player_event_manager = (libvlc_media_player_event_manager)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_media_player_event_manager, typeof(libvlc_media_player_event_manager));

            IntPtr pAddressOf_libvlc_event_attach = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_event_attach");
            dlibvlc_event_attach = (libvlc_event_attach)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_event_attach, typeof(libvlc_event_attach));

            IntPtr pAddressOf_libvlc_event_detach = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_event_detach");
            dlibvlc_event_detach = (libvlc_event_detach)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_event_detach, typeof(libvlc_event_detach));

            IntPtr pAddressOf_libvlc_event_type_name = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_event_type_name");
            dlibvlc_event_type_name = (libvlc_event_type_name)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_event_type_name, typeof(libvlc_event_type_name));

            IntPtr pAddressOf_libvlc_log_open = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_log_open");
            dlibvlc_log_open = (libvlc_log_open)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_log_open, typeof(libvlc_log_open));

            IntPtr pAddressOf_libvlc_log_close = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_log_close");
            dlibvlc_log_close = (libvlc_log_close)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_log_close, typeof(libvlc_log_close));

            IntPtr pAddressOf_libvlc_set_log_verbosity = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_set_log_verbosity");
            dlibvlc_set_log_verbosity = (libvlc_set_log_verbosity)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_set_log_verbosity, typeof(libvlc_set_log_verbosity));

            IntPtr pAddressOf_libvlc_log_count = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_log_count");
            dlibvlc_log_count = (libvlc_log_count)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_log_count, typeof(libvlc_log_count));

            IntPtr pAddressOf_libvlc_video_get_marquee_int = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_marquee_int");
            dlibvlc_video_get_marquee_int = (libvlc_video_get_marquee_int)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_marquee_int, typeof(libvlc_video_get_marquee_int));

//            IntPtr pAddressOf_libvlc_video_marquee_string = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_marquee_string");
//            dlibvlc_video_marquee_string = (libvlc_video_marquee_string)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_marquee_string, typeof(libvlc_video_marquee_string));

            IntPtr pAddressOf_libvlc_video_set_marquee_int = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_set_marquee_int");
            dlibvlc_video_set_marquee_int = (libvlc_video_set_marquee_int)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_set_marquee_int, typeof(libvlc_video_set_marquee_int));

            IntPtr pAddressOf_libvlc_video_set_marquee_string = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_set_marquee_string");
            dlibvlc_video_set_marquee_string = (libvlc_video_set_marquee_string)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_set_marquee_string, typeof(libvlc_video_set_marquee_string));

            IntPtr pAddressOf_libvlc_video_get_spu = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_spu");
            dlibvlc_video_get_spu = (libvlc_video_get_spu)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_spu, typeof(libvlc_video_get_spu));

            IntPtr pAddressOf_libvlc_video_get_spu_count = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_spu_count");
            dlibvlc_video_get_spu_count = (libvlc_video_get_spu_count)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_spu_count, typeof(libvlc_video_get_spu_count));

            IntPtr pAddressOf_libvlc_video_get_spu_description = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_spu_description");
            dlibvlc_video_get_spu_description = (libvlc_video_get_spu_description)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_spu_description, typeof(libvlc_video_get_spu_description));

            IntPtr pAddressOf_libvlc_video_set_spu = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_set_spu");
            dlibvlc_video_set_spu = (libvlc_video_set_spu)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_set_spu, typeof(libvlc_video_set_spu));

            IntPtr pAddressOf_libvlc_video_set_subtitle_file = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_set_subtitle_file");
            dlibvlc_video_set_subtitle_file = (libvlc_video_set_subtitle_file)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_set_subtitle_file, typeof(libvlc_video_set_subtitle_file));

            IntPtr pAddressOf_libvlc_toggle_fullscreen = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_toggle_fullscreen");
            dlibvlc_toggle_fullscreen = (libvlc_toggle_fullscreen)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_toggle_fullscreen, typeof(libvlc_toggle_fullscreen));

            IntPtr pAddressOf_libvlc_set_fullscreen = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_set_fullscreen");
            dlibvlc_set_fullscreen = (libvlc_set_fullscreen)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_set_fullscreen, typeof(libvlc_set_fullscreen));

            IntPtr pAddressOf_libvlc_get_fullscreen = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_get_fullscreen");
            dlibvlc_get_fullscreen = (libvlc_get_fullscreen)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_get_fullscreen, typeof(libvlc_get_fullscreen));

            IntPtr pAddressOf_libvlc_video_get_size = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_size");
            dlibvlc_video_get_size = (libvlc_video_get_size)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_size, typeof(libvlc_video_get_size));

            IntPtr pAddressOf_libvlc_video_get_scale = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_scale");
            dlibvlc_video_get_scale = (libvlc_video_get_scale)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_scale, typeof(libvlc_video_get_scale));

            IntPtr pAddressOf_libvlc_video_set_scale = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_set_scale");
            dlibvlc_video_set_scale = (libvlc_video_set_scale)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_set_scale, typeof(libvlc_video_set_scale));

            IntPtr pAddressOf_libvlc_video_get_aspect_ratio = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_aspect_ratio");
            dlibvlc_video_get_aspect_ratio = (libvlc_video_get_aspect_ratio)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_aspect_ratio, typeof(libvlc_video_get_aspect_ratio));

            IntPtr pAddressOf_libvlc_video_set_aspect_ratio = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_set_aspect_ratio");
            dlibvlc_video_set_aspect_ratio = (libvlc_video_set_aspect_ratio)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_set_aspect_ratio, typeof(libvlc_video_set_aspect_ratio));

            IntPtr pAddressOf_libvlc_video_get_title_description = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_title_description");
            dlibvlc_video_get_title_description = (libvlc_video_get_title_description)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_title_description, typeof(libvlc_video_get_title_description));

            IntPtr pAddressOf_libvlc_video_get_chapter_description = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_chapter_description");
            dlibvlc_video_get_chapter_description = (libvlc_video_get_chapter_description)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_chapter_description, typeof(libvlc_video_get_chapter_description));

            IntPtr pAddressOf_libvlc_video_get_track_count = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_track_count");
            dlibvlc_video_get_track_count = (libvlc_video_get_track_count)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_track_count, typeof(libvlc_video_get_track_count));

            IntPtr pAddressOf_libvlc_video_get_track_description = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_track_description");
            dlibvlc_video_get_track_description = (libvlc_video_get_track_description)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_track_description, typeof(libvlc_video_get_track_description));

            IntPtr pAddressOf_libvlc_video_get_track = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_track");
            dlibvlc_video_get_track = (libvlc_video_get_track)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_track, typeof(libvlc_video_get_track));

            IntPtr pAddressOf_libvlc_video_set_track = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_set_track");
            dlibvlc_video_set_track = (libvlc_video_set_track)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_set_track, typeof(libvlc_video_set_track));

            IntPtr pAddressOf_libvlc_video_take_snapshot = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_take_snapshot");
            dlibvlc_video_take_snapshot = (libvlc_video_take_snapshot)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_take_snapshot, typeof(libvlc_video_take_snapshot));

            IntPtr pAddressOf_libvlc_video_set_deinterlace = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_set_deinterlace");
            dlibvlc_video_set_deinterlace = (libvlc_video_set_deinterlace)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_set_deinterlace, typeof(libvlc_video_set_deinterlace));

            IntPtr pAddressOf_libvlc_audio_output_list_get = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_output_list_get");
            dlibvlc_audio_output_list_get = (libvlc_audio_output_list_get)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_output_list_get, typeof(libvlc_audio_output_list_get));

            IntPtr pAddressOf_libvlc_audio_output_list_release = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_output_list_release");
            dlibvlc_audio_output_list_release = (libvlc_audio_output_list_release)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_output_list_release, typeof(libvlc_audio_output_list_release));

            IntPtr pAddressOf_libvlc_audio_output_set = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_output_set");
            dlibvlc_audio_output_set = (libvlc_audio_output_set)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_output_set, typeof(libvlc_audio_output_set));

            IntPtr pAddressOf_libvlc_audio_output_device_count = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_output_device_count");
            dlibvlc_audio_output_device_count = (libvlc_audio_output_device_count)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_output_device_count, typeof(libvlc_audio_output_device_count));

            IntPtr pAddressOf_libvlc_audio_output_device_longname = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_output_device_longname");
            dlibvlc_audio_output_device_longname = (libvlc_audio_output_device_longname)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_output_device_longname, typeof(libvlc_audio_output_device_longname));

            IntPtr pAddressOf_libvlc_audio_output_device_id = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_output_device_id");
            dlibvlc_audio_output_device_id = (libvlc_audio_output_device_id)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_output_device_id, typeof(libvlc_audio_output_device_id));

            IntPtr pAddressOf_libvlc_audio_output_device_set = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_output_device_set");
            dlibvlc_audio_output_device_set = (libvlc_audio_output_device_set)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_output_device_set, typeof(libvlc_audio_output_device_set));

            IntPtr pAddressOf_libvlc_audio_output_get_device_type = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_output_get_device_type");
            dlibvlc_audio_output_get_device_type = (libvlc_audio_output_get_device_type)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_output_get_device_type, typeof(libvlc_audio_output_get_device_type));

            IntPtr pAddressOf_libvlc_audio_output_set_device_type = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_output_set_device_type");
            dlibvlc_audio_output_set_device_type = (libvlc_audio_output_set_device_type)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_output_set_device_type, typeof(libvlc_audio_output_set_device_type));

            IntPtr pAddressOf_libvlc_audio_get_track_count = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_get_track_count");
            dlibvlc_audio_get_track_count = (libvlc_audio_get_track_count)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_get_track_count, typeof(libvlc_audio_get_track_count));

            IntPtr pAddressOf_libvlc_audio_get_track_description = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_get_track_description");
            dlibvlc_audio_get_track_description = (libvlc_audio_get_track_description)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_get_track_description, typeof(libvlc_audio_get_track_description));

            IntPtr pAddressOf_libvlc_audio_get_track = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_get_track");
            dlibvlc_audio_get_track = (libvlc_audio_get_track)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_get_track, typeof(libvlc_audio_get_track));

            IntPtr pAddressOf_libvlc_audio_set_track = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_set_track");
            dlibvlc_audio_set_track = (libvlc_audio_set_track)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_set_track, typeof(libvlc_audio_set_track));

            IntPtr pAddressOf_libvlc_audio_get_channel = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_get_channel");
            dlibvlc_audio_get_channel = (libvlc_audio_get_channel)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_get_channel, typeof(libvlc_audio_get_channel));

            IntPtr pAddressOf_libvlc_audio_set_channel = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_set_channel");
            dlibvlc_audio_set_channel = (libvlc_audio_set_channel)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_set_channel, typeof(libvlc_audio_set_channel));

            IntPtr pAddressOf_libvlc_audio_get_delay = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_get_delay");
            dlibvlc_audio_get_delay = (libvlc_audio_get_delay)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_get_delay, typeof(libvlc_audio_get_delay));

            IntPtr pAddressOf_libvlc_audio_set_delay = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_audio_set_delay");
            dlibvlc_audio_set_delay = (libvlc_audio_set_delay)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_audio_set_delay, typeof(libvlc_audio_set_delay));

            IntPtr pAddressOf_libvlc_video_get_logo_int = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_get_logo_int");
            dlibvlc_video_get_logo_int = (libvlc_video_get_logo_int)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_get_logo_int, typeof(libvlc_video_get_logo_int));

            IntPtr pAddressOf_libvlc_video_set_logo_int = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_set_logo_int");
            dlibvlc_video_set_logo_int = (libvlc_video_set_logo_int)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_set_logo_int, typeof(libvlc_video_set_logo_int));

            IntPtr pAddressOf_libvlc_video_set_logo_string = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_video_set_logo_string");
            dlibvlc_video_set_logo_string = (libvlc_video_set_logo_string)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_video_set_logo_string, typeof(libvlc_video_set_logo_string));

            IntPtr pAddressOf_libvlc_track_description_release = MSMethods.GetProcAddress(pLibVlcDll, "libvlc_track_description_release");
            dlibvlc_track_description_release = (libvlc_track_description_release)Marshal.GetDelegateForFunctionPointer(pAddressOf_libvlc_track_description_release, typeof(libvlc_track_description_release));
        }

        #region delegated instance menthods

        public IntPtr new_vlc(int argc, string[] argv)
        {
            return dlibvlc_new(argc, argv);
        }

        public void release_vlc(IntPtr vlc_instance)
        {
            dlibvlc_release(vlc_instance);
        }

        public IntPtr media_new_location(IntPtr p_instance, string psz_mrl)
        {
            return dlibvlc_media_new_location(p_instance, psz_mrl);
        }

        public IntPtr media_new_path(IntPtr p_instance, string path)
        {
            return dlibvlc_media_new_path(p_instance, path);
        }

        public void media_release(IntPtr p_meta_desc)
        {
            dlibvlc_media_release(p_meta_desc);
        }

        public IntPtr media_player_new_from_media(IntPtr media)
        {
            return dlibvlc_media_player_new_from_media(media);
        }

        public void media_player_release(IntPtr player)
        {
            dlibvlc_media_player_release(player);
        }

        public void media_player_set_hwnd(IntPtr player, IntPtr drawable)
        {
            dlibvlc_media_player_set_hwnd(player, drawable);
        }

        public IntPtr media_player_get_media(IntPtr player)
        {
            return dlibvlc_media_player_get_media(player);
        }

        public void media_player_set_media(IntPtr player, IntPtr media)
        {
            dlibvlc_media_player_set_media(player, media);
        }

        public int media_player_play(IntPtr player)
        {
            return dlibvlc_media_player_play(player);
        }

        public int media_player_is_playing(IntPtr player)
        {
            return dlibvlc_media_player_is_playing(player);
        }

        public void media_player_pause(IntPtr player)
        {
            dlibvlc_media_player_pause(player);
        }

        public void media_player_stop(IntPtr player)
        {
            dlibvlc_media_player_stop(player);
        }

        public VlcState media_player_get_state(IntPtr player)
        {
            return dlibvlc_media_player_get_state(player);
        }

        public int media_player_is_seekable(IntPtr player)
        {
            return dlibvlc_media_player_is_seekable(player);
        }

        public Int64 media_player_get_time(IntPtr player)
        {
            return dlibvlc_media_player_get_time(player);
        }

        public void media_player_set_time(IntPtr player, Int64 time)
        {
            dlibvlc_media_player_set_time(player, time);
        }

        public Int64 media_player_get_length(IntPtr player)
        {
            return dlibvlc_media_player_get_length(player);
        }

        public float media_player_get_position(IntPtr player, Int64 time)
        {
            return dlibvlc_media_player_get_position(player, time);
        }

        public void media_player_set_position(IntPtr player, float pos)
        {
            dlibvlc_media_player_set_position(player, pos);
        }

        public void media_player_set_chapter(IntPtr player, int chapter)
        {
            dlibvlc_media_player_set_chapter(player, chapter);
        }

        public int media_player_get_chapter(IntPtr player)
        {
            return dlibvlc_media_player_get_chapter(player);
        }

        public int media_player_get_chapter_count(IntPtr player)
        {
            return dlibvlc_media_player_get_chapter_count(player);
        }

        public int media_player_get_chapter_count_for_title(IntPtr player, int title)
        {
            return dlibvlc_media_player_get_chapter_count_for_title(player, title);
        }

        public void media_player_set_title(IntPtr player, int title)
        {
            dlibvlc_media_player_set_title(player, title);
        }

        public void media_player_previous_chapter(IntPtr player)
        {
            dlibvlc_media_player_previous_chapter(player);
        }

        public void media_player_next_chapter(IntPtr player)
        {
            dlibvlc_media_player_next_chapter(player);
        }

        public void media_player_next_frame(IntPtr player)
        {
            dlibvlc_media_player_next_frame(player);
        }

        public void audio_toggle_mute(IntPtr player)
        {
            dlibvlc_audio_toggle_mute(player);
        }

        public int audio_get_volume(IntPtr player)
        {
            return dlibvlc_audio_get_volume(player);
        }

        public void audio_set_volume(IntPtr player, int newvolume)
        {
            dlibvlc_audio_set_volume(player, newvolume);
        }

        public void clearerr()
        {
            dlibvlc_clearerr();
        }

        public IntPtr errmsg()
        {
            return dlibvlc_errmsg(); ;
        }

        public IntPtr media_player_event_manager(IntPtr player)
        {
            return dlibvlc_media_player_event_manager(player);
        }

        public void event_attach(IntPtr p_event_manager, uint i_event_type, EventCallbackDelegate f_callback, IntPtr p_user_data, string data)
        {
            dlibvlc_event_attach(p_event_manager, i_event_type, f_callback, p_user_data, data);
        }

        public void event_detach(IntPtr p_event_manager, uint i_event_type, EventCallbackDelegate f_callback, IntPtr p_user_data, string data)
        {
            dlibvlc_event_detach(p_event_manager, i_event_type, f_callback, p_user_data, data);
        }

        public IntPtr event_type_name(VlcEventType event_type)
        {
            return dlibvlc_event_type_name(event_type);
        }

        public IntPtr log_open(IntPtr logger)
        {
            return dlibvlc_log_open(logger);
        }

        public void log_close(IntPtr logger)
        {
            dlibvlc_log_close(logger);
        }

        public void set_log_verbosity(IntPtr logger, uint level)
        {
            dlibvlc_set_log_verbosity(logger, level);
        }

        public uint log_count(IntPtr logger)
        {
            return dlibvlc_log_count(logger);
        }

        public int video_get_marquee_int(IntPtr player, libvlc_video_marquee_option_t option)
        {
            return dlibvlc_video_get_marquee_int(player, option);
        }

        public IntPtr video_marquee_string(IntPtr player, uint option)
        {
            return dlibvlc_video_marquee_string(player, option);
        }

        public void video_set_marquee_int(IntPtr player, uint option, int value)
        {
            dlibvlc_video_set_marquee_int(player, option, value);
        }

        public void video_set_marquee_string(IntPtr player, uint option, string value)
        {
            dlibvlc_video_set_marquee_string(player, option, value);
        }

        public int video_get_spu(IntPtr player)
        {
            return dlibvlc_video_get_spu(player);
        }

        public int video_get_spu_count(IntPtr player)
        {
            return dlibvlc_video_get_spu_count(player);
        }

        public IntPtr video_get_spu_description(IntPtr player)
        {
            return dlibvlc_video_get_spu_description(player);
        }

        public int video_set_spu(IntPtr player, uint i_spu)
        {
            return dlibvlc_video_set_spu(player, i_spu);
        }

        public int video_set_subtitle_file(IntPtr player, string subtitle_file)
        {
            return dlibvlc_video_set_subtitle_file(player, subtitle_file);
        }

        public void toggle_fullscreen(IntPtr player)
        {
            dlibvlc_toggle_fullscreen(player);
        }

        public void set_fullscreen(IntPtr player)
        {
            dlibvlc_set_fullscreen(player);
        }

        public int get_fullscreen(IntPtr player)
        {
            return dlibvlc_get_fullscreen(player);
        }

        public int video_get_size(IntPtr player, uint num, ref uint x, ref uint y)
        {
            return dlibvlc_video_get_size(player, num, ref x, ref y);
        }

        public float video_get_scale(IntPtr player)
        {
            return dlibvlc_video_get_scale(player);
        }

        public void video_set_scale(IntPtr player, float factor)
        {
            dlibvlc_video_set_scale(player, factor);
        }

        public IntPtr video_get_aspect_ratio(IntPtr player)
        {
            return dlibvlc_video_get_aspect_ratio(player);
        }

        public void video_set_aspect_ratio(IntPtr player, string aspect)
        {
            dlibvlc_video_set_aspect_ratio(player, aspect);
        }

        public IntPtr video_get_title_description(IntPtr player)
        {
            return dlibvlc_video_get_title_description(player);
        }

        public IntPtr video_get_chapter_description(IntPtr player, int title)
        {
            return dlibvlc_video_get_chapter_description(player, title);
        }

        public int video_get_track_count(IntPtr player)
        {
            return dlibvlc_video_get_track_count(player);
        }

        public IntPtr video_get_track_description(IntPtr player)
        {
            return dlibvlc_video_get_track_description(player);
        }

        public int video_get_track(IntPtr player)
        {
            return dlibvlc_video_get_track(player);
        }

        public void video_set_track(IntPtr player, int track)
        {
            dlibvlc_video_set_track(player, track);
        }

        public int video_take_snapshot(IntPtr player, uint num, string path, uint width, uint height)
        {
            return dlibvlc_video_take_snapshot(player, num, path, width, height);
        }

        public void video_set_deinterlace(IntPtr player, string mode)
        {
            dlibvlc_video_set_deinterlace(player, mode);
        }

        public IntPtr audio_output_list_get(IntPtr instance)
        {
            return dlibvlc_audio_output_list_get(instance);
        }

        public void audio_output_list_release(IntPtr list)
        {
            dlibvlc_audio_output_list_release(list);
        }

        public int audio_output_set(IntPtr player, string name)
        {
            return dlibvlc_audio_output_set(player, name);
        }

        public int audio_output_device_count(IntPtr instance, string AudioOutput)
        {
            return dlibvlc_audio_output_device_count(instance, AudioOutput);
        }

        public IntPtr audio_output_device_longname(IntPtr instance, string AudioOutput, int device_id)
        {
            return dlibvlc_audio_output_device_longname(instance, AudioOutput, device_id);
        }

        public IntPtr audio_output_device_id(IntPtr instance, string AudioOutput, int device_id)
        {
            return dlibvlc_audio_output_device_id(instance, AudioOutput, device_id);
        }

        public void audio_output_device_set(IntPtr player, string AudioOutput, string device_id)
        {
            dlibvlc_audio_output_device_set(player, AudioOutput, device_id);
        }

        public int audio_output_get_device_type(IntPtr player)
        {
            return dlibvlc_audio_output_get_device_type(player);
        }

        public void audio_output_set_device_type(IntPtr player, int device_type)
        {
            dlibvlc_audio_output_set_device_type(player, device_type);
        }

        public int audio_get_track_count(IntPtr player)
        {
            return dlibvlc_audio_get_track_count(player);
        }

        public IntPtr audio_get_track_description(IntPtr player)
        {
            return dlibvlc_audio_get_track_description(player);
        }

        public int audio_get_track(IntPtr player)
        {
            return dlibvlc_audio_get_track(player);
        }

        public int audio_set_track(IntPtr player, int track)
        {
            return dlibvlc_audio_set_track(player, track);
        }

        public int audio_get_channel(IntPtr player)
        {
            return dlibvlc_audio_get_channel(player);
        }

        public int audio_set_channel(IntPtr player, int channel)
        {
            return dlibvlc_audio_set_channel(player, channel);
        }

        public Int64 audio_get_delay(IntPtr player)
        {
            return dlibvlc_audio_get_delay(player);
        }

        public int audio_set_delay(IntPtr player, Int64 delay)
        {
            return dlibvlc_audio_set_delay(player, delay);
        }

        public int video_get_logo_int(IntPtr player, uint option)
        {
            return dlibvlc_video_get_logo_int(player, option);
        }

        public void video_set_logo_int(IntPtr player, uint option, int value)
        {
            dlibvlc_video_set_logo_int(player, option, value);
        }

        public void video_set_logo_string(IntPtr player, uint option, string value)
        {
            dlibvlc_video_set_logo_string(player, option, value);
        }

        public void track_description_release(IntPtr ptrack)
        {
            dlibvlc_track_description_release(ptrack);
        }
        #endregion

        #region VLC Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_new(int argc, string[] argv);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_release(IntPtr instance);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_media_new_location(IntPtr p_instance, string psz_mrl);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_media_new_path(IntPtr p_instance, string path);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_release(IntPtr p_meta_desc);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_media_player_new_from_media(IntPtr media);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_release(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_set_hwnd(IntPtr player, IntPtr drawable);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_media_player_get_media(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_set_media(IntPtr player, IntPtr media);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_media_player_play(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_media_player_is_playing(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_pause(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_stop(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate VlcState libvlc_media_player_get_state(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_media_player_is_seekable(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate Int64 libvlc_media_player_get_time(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_set_time(IntPtr player, Int64 time);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate Int64 libvlc_media_player_get_length(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate float libvlc_media_player_get_position(IntPtr player, Int64 time);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_set_position(IntPtr player, float pos);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_set_chapter(IntPtr player, int chapter);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_media_player_get_chapter(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_media_player_get_chapter_count(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_media_player_get_chapter_count_for_title(IntPtr player, int title);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_set_title(IntPtr player, int title);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_previous_chapter(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_next_chapter(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_media_player_next_frame(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_audio_toggle_mute(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_audio_get_volume(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_audio_set_volume(IntPtr player, int newvolume);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_clearerr();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_errmsg();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void EventCallbackDelegate(ref VlcEventType type, IntPtr userdata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_media_player_event_manager(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_event_attach(IntPtr p_event_manager, uint i_event_type, EventCallbackDelegate f_callback, IntPtr p_user_data, string data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_event_detach(IntPtr p_event_manager, uint i_event_type, EventCallbackDelegate f_callback, IntPtr p_user_data, string data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_event_type_name(VlcEventType event_type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_log_open(IntPtr logger);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_log_close(IntPtr logger);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_set_log_verbosity(IntPtr logger, uint level);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate uint libvlc_log_count(IntPtr logger);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_video_get_marquee_int(IntPtr player, libvlc_video_marquee_option_t option);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_video_marquee_string(IntPtr player, uint option);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_video_set_marquee_int(IntPtr player, uint option, int value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_video_set_marquee_string(IntPtr player, uint option, string value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_video_get_spu(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_video_get_spu_count(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_video_get_spu_description(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_video_set_spu(IntPtr player, uint i_spu);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_video_set_subtitle_file(IntPtr player, string subtitle_file);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_toggle_fullscreen(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_set_fullscreen(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_get_fullscreen(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_video_get_size(IntPtr player, uint num, ref uint x, ref uint y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate float libvlc_video_get_scale(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_video_set_scale(IntPtr player, float factor);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_video_get_aspect_ratio(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_video_set_aspect_ratio(IntPtr player, string aspect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_video_get_title_description(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_video_get_chapter_description(IntPtr player, int title);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_video_get_track_count(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_video_get_track_description(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_video_get_track(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_video_set_track(IntPtr player, int track);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_video_take_snapshot(IntPtr player, uint num, string path, uint width, uint height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_video_set_deinterlace(IntPtr player, string mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_audio_output_list_get(IntPtr instance);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_audio_output_list_release(IntPtr list);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_audio_output_set(IntPtr player, string name);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_audio_output_device_count(IntPtr instance, string AudioOutput);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_audio_output_device_longname(IntPtr instance, string AudioOutput, int device_id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_audio_output_device_id(IntPtr instance, string AudioOutput, int device_id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_audio_output_device_set(IntPtr player, string AudioOutput, string device_id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_audio_output_get_device_type(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_audio_output_set_device_type(IntPtr player, int device_type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_audio_get_track_count(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate IntPtr libvlc_audio_get_track_description(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_audio_get_track(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_audio_set_track(IntPtr player, int track);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_audio_get_channel(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_audio_set_channel(IntPtr player, int channel);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate Int64 libvlc_audio_get_delay(IntPtr player);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_audio_set_delay(IntPtr player, Int64 delay);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int libvlc_video_get_logo_int(IntPtr player, uint option);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_video_set_logo_int(IntPtr player, uint option, int value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_video_set_logo_string(IntPtr player, uint option, string value);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void libvlc_track_description_release(IntPtr ptrack);
        #endregion

        #region structs
        [StructLayout(LayoutKind.Sequential)]
        public struct libvlc_audio_output_t
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string psz_name;
            [MarshalAs(UnmanagedType.LPStr)]
            public string psz_description;
            public IntPtr p_next;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct libvlc_track_description_t
        {
            public uint i_id;
            [MarshalAs(UnmanagedType.LPStr)]
            public string psz_name;
            public IntPtr p_next;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct libvlc_log_message_t
        {
            public uint sizeof_msg;
            public int i_severity;
            [MarshalAs(UnmanagedType.LPStr)]
            public string psz_type;
            [MarshalAs(UnmanagedType.LPStr)]
            public string psz_name;
            [MarshalAs(UnmanagedType.LPStr)]
            public string psz_header;
            [MarshalAs(UnmanagedType.LPStr)]
            public string psz_message;
        };
        #endregion

        #region enums
        public enum libvlc_video_logo_option_t
        {
            libvlc_logo_enable,
            libvlc_logo_file,
            libvlc_logo_x,
            libvlc_logo_y,
            libvlc_logo_delay,
            libvlc_logo_repeat,
            libvlc_logo_opacity,
            libvlc_logo_position
        }

        public enum libvlc_audio_output_channel_t
        {
            libvlc_AudioChannel_Stereo = 1,
            libvlc_AudioChannel_RStereo = 2,
            libvlc_AudioChannel_Left = 3,
            libvlc_AudioChannel_Right = 4,
            libvlc_AudioChannel_Dolbys = 5
        }

        public enum libvlc_audio_output_device_type_t
        {
            libvlc_AudioOutputDevice_Error = -1,
            libvlc_AudioOutputDevice_Mono = 1,
            libvlc_AudioOutputDevice_Stereo = 2,
            libvlc_AudioOutputDevice_2F2R = 4,
            libvlc_AudioOutputDevice_3F2R = 5,
            libvlc_AudioOutputDevice_5_1 = 6,
            libvlc_AudioOutputDevice_6_1 = 7,
            libvlc_AudioOutputDevice_7_1 = 8,
            libvlc_AudioOutputDevice_SPDIF = 10
        }

        public enum libvlc_video_marquee_option_t
        {
            libvlc_marquee_Enable = 0,
            libvlc_marquee_Text,        /** string argument */
            libvlc_marquee_Color,
            libvlc_marquee_Opacity,
            libvlc_marquee_Position,
            libvlc_marquee_Refresh,
            libvlc_marquee_Size,
            libvlc_marquee_Timeout,
            libvlc_marquee_X,
            libvlc_marquee_Y
        };

        public enum VlcState
        {
            NothingSpecial,
            Opening,
            Buffering,
            Playing,
            Paused,
            Stopped,
            Ended,
            Error
        }

        public enum VlcEventType
        {
            libvlc_MediaMetaChanged = 0,
            libvlc_MediaSubItemAdded,
            libvlc_MediaDurationChanged,
            libvlc_MediaParsedChanged,
            libvlc_MediaFreed,
            libvlc_MediaStateChanged,

            libvlc_MediaPlayerMediaChanged = 0x100,
            libvlc_MediaPlayerNothingSpecial,
            libvlc_MediaPlayerOpening,
            libvlc_MediaPlayerBuffering,
            libvlc_MediaPlayerPlaying,
            libvlc_MediaPlayerPaused,
            libvlc_MediaPlayerStopped,
            libvlc_MediaPlayerForward,
            libvlc_MediaPlayerBackward,
            libvlc_MediaPlayerEndReached,
            libvlc_MediaPlayerEncounteredError,
            libvlc_MediaPlayerTimeChanged,
            libvlc_MediaPlayerPositionChanged,
            libvlc_MediaPlayerSeekableChanged,
            libvlc_MediaPlayerPausableChanged,
            libvlc_MediaPlayerTitleChanged,
            libvlc_MediaPlayerSnapshotTaken,
            libvlc_MediaPlayerLengthChanged,

            libvlc_MediaListItemAdded = 0x200,
            libvlc_MediaListWillAddItem,
            libvlc_MediaListItemDeleted,
            libvlc_MediaListWillDeleteItem,

            libvlc_MediaListViewItemAdded = 0x300,
            libvlc_MediaListViewWillAddItem,
            libvlc_MediaListViewItemDeleted,
            libvlc_MediaListViewWillDeleteItem,

            libvlc_MediaListPlayerPlayed = 0x400,
            libvlc_MediaListPlayerNextItemSet,
            libvlc_MediaListPlayerStopped,

            libvlc_MediaDiscovererStarted = 0x500,
            libvlc_MediaDiscovererEnded,

            libvlc_VlmMediaAdded = 0x600,
            libvlc_VlmMediaRemoved,
            libvlc_VlmMediaChanged,
            libvlc_VlmMediaInstanceStarted,
            libvlc_VlmMediaInstanceStopped,
            libvlc_VlmMediaInstanceStatusInit,
            libvlc_VlmMediaInstanceStatusOpening,
            libvlc_VlmMediaInstanceStatusPlaying,
            libvlc_VlmMediaInstanceStatusPause,
            libvlc_VlmMediaInstanceStatusEnd,
            libvlc_VlmMediaInstanceStatusError
        }
        #endregion
    }

    public static class AspectRatio
    {
        public static String FullScreen = @"4:3";
        public static String WideScreen = @"16:9";
        public static String DVD = @"1.85";
        public static String Cinemascope = @"2.35";
    }

    static class MSMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
    }
}
