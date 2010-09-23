using System;
using System.Text;
using System.Runtime.InteropServices;

namespace MediaCenterVLCPlayer
{
    public static class VLCLib
    {
        public static class AspectRatio
        {
            public static String FullScreen = @"4:3";
            public static String WideScreen = @"16:9";
            public static String DVD = @"1.85";
            public static String Cinemascope = @"2.35";
        }

        #region core
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_new(int argc, string[] argv);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_release(IntPtr instance);
        #endregion

        #region media
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_new_location(IntPtr p_instance, string psz_mrl);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_new_path(IntPtr p_instance, string path);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_release(IntPtr p_meta_desc);
        #endregion

        #region media player
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_player_new_from_media(IntPtr media);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_release(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_hwnd(IntPtr player, IntPtr drawable);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_media_player_get_media(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_media(IntPtr player, IntPtr media);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_play(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_is_playing(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_pause(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_stop(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern VlcState libvlc_media_player_get_state(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_is_seekable(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 libvlc_media_player_get_time(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_time(IntPtr player, Int64 time);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 libvlc_media_player_get_length(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern float libvlc_media_player_get_position(IntPtr player, Int64 time);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_position(IntPtr player, float pos);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_chapter(IntPtr player, int chapter);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_get_chapter(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_get_chapter_count(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_media_player_get_chapter_count_for_title(IntPtr player, int title);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_set_title(IntPtr player, int title);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_previous_chapter(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_next_chapter(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_media_player_next_frame(IntPtr player);
        #endregion

        #region Audio Controls
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_toggle_mute(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_get_volume(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_set_volume(IntPtr player, int newvolume);
        #endregion

        #region exception
        [DllImport("libvlc")]
        public static extern void libvlc_clearerr();

        [DllImport("libvlc")]
        public static extern IntPtr libvlc_errmsg();
        #endregion

        #region events
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public unsafe delegate void EventCallbackDelegate(ref VlcEventType type, IntPtr userdata);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling=true)]
        public static extern IntPtr libvlc_media_player_event_manager(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libvlc_event_attach(IntPtr p_event_manager, uint i_event_type, EventCallbackDelegate f_callback, IntPtr p_user_data, string data);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void libvlc_event_detach(IntPtr p_event_manager, uint i_event_type, EventCallbackDelegate f_callback, IntPtr p_user_data, string data);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        internal static extern IntPtr libvlc_event_type_name(VlcEventType event_type);
        #endregion

        #region logging
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_log_open(IntPtr logger);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_log_close(IntPtr logger);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_set_log_verbosity(IntPtr logger, uint level);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint libvlc_log_count(IntPtr logger);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_log_get_iterator(IntPtr logger);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_log_iterator_free(IntPtr logger);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_log_iterator_has_next(IntPtr iterator);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct libvlc_log_message_t {
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

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern libvlc_log_message_t libvlc_log_iterator_next(IntPtr iterator, ref libvlc_log_message_t buffer);
        #endregion

        #region marquee overlay
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_marquee_int(IntPtr player, libvlc_video_marquee_option_t option);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_video_marquee_string(IntPtr player, uint option);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_marquee_int(IntPtr player, uint option, int value);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_marquee_string(IntPtr player, uint option, string value);
        #endregion

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

        #region subtitles
        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_spu(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_spu_count(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl, SetLastError=true)]
        public static extern IntPtr libvlc_video_get_spu_description(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_set_spu(IntPtr player, uint i_spu);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_set_subtitle_file(IntPtr player, string subtitle_file);

        [StructLayout(LayoutKind.Sequential)]
        public struct libvlc_track_description_t
        {
            public uint i_id;
            [MarshalAs(UnmanagedType.LPStr)]
            public string psz_name;
            public IntPtr p_next;
        }
        #endregion

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_toggle_fullscreen(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_set_fullscreen(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_get_fullscreen(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_size(IntPtr player, uint num, ref uint x, ref uint y);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern float libvlc_video_get_scale(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_scale(IntPtr player, float factor);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern String libvlc_video_get_aspect_ratio(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_aspect_ratio(IntPtr player, string aspect);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_video_get_title_description(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_video_get_chapter_description(IntPtr player, int title);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_track_count(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_video_get_track_description(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_track(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_track(IntPtr player, int track);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_take_snapshot(IntPtr player, uint num, string path, uint width, uint height);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_deinterlace(IntPtr player, string mode);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_audio_output_list_get(IntPtr instance);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_output_list_release(IntPtr list);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_output_set(IntPtr player, string name);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_output_device_count(IntPtr instance, string AudioOutput);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_audio_output_device_longname(IntPtr instance, string AudioOutput, int device_id);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_audio_output_device_id(IntPtr instance, string AudioOutput, int device_id);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_output_device_set(IntPtr player, string AudioOutput, string device_id);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_output_get_device_type(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_audio_output_set_device_type(IntPtr player, int device_type);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_get_track_count(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr libvlc_audio_get_track_description(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_get_track(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_set_track(IntPtr player, int track);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_get_channel(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_set_channel(IntPtr player, int channel);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern Int64 libvlc_audio_get_delay(IntPtr player);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_audio_set_delay(IntPtr player, Int64 delay);

        [StructLayout(LayoutKind.Sequential)]
        public struct libvlc_audio_output_t
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string psz_name;
            [MarshalAs(UnmanagedType.LPStr)]
            public string psz_description;
            public IntPtr p_next;
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

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int libvlc_video_get_logo_int(IntPtr player, uint option);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_logo_int(IntPtr player, uint option, int value);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_video_set_logo_string(IntPtr player, uint option, string value);

        [DllImport("libvlc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void libvlc_track_description_release(IntPtr ptrack);

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
    }
}
