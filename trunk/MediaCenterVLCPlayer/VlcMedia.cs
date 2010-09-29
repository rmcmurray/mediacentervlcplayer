using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MediaCenterVLCPlayer
{
    public class VlcMedia : IDisposable
    {
        public IntPtr Handle;
        private VlcInstance _instance;
        private ArrayList _subtitleTracks;
        private ArrayList _audioTracks;
        private string _defaultAspectRatio;
        private string _currentAspectRatio;

        public long Length
        {
            get { return VLCLib.libvlc_media_player_get_length(_instance.Player.Handle); }
        }
        public long Time
        {
            get { return VLCLib.libvlc_media_player_get_time(Handle); }
            set { VLCLib.libvlc_media_player_set_time(Handle, value); }
        }
        public float Position
        {
            get
            {
                long time = VLCLib.libvlc_media_player_get_time(Handle);
                return VLCLib.libvlc_media_player_get_position(Handle, time);
            }
            set
            {
                VLCLib.libvlc_media_player_set_position(Handle, value);
            }
        }
        public int TrackCount
        {
            get { return VLCLib.libvlc_audio_get_track_count(_instance.Player.Handle); }
        }
        public string AspectRatio
        {
            get
            {
                if (_defaultAspectRatio == string.Empty)
                {
                    string currentAspectRatio = string.Empty;
                    // current video aspect ratio
                    int i = 0;
                    uint w = 0, h = 0;
                    VLCLib.libvlc_video_get_size(_instance.Player.Handle, 0, ref w, ref h);
                    float width = (float)Convert.ToDecimal(w);
                    float height = (float)Convert.ToDecimal(h);
                    if (width > 0.0 && height > 0.0)
                    {
                        string currentRatio = (width / height).ToString();
                        if (currentRatio.Length > 4)
                            currentRatio = currentRatio.Substring(0, 4);
                        _defaultAspectRatio = currentRatio;
                        _currentAspectRatio = _defaultAspectRatio;
                    }
                }
                return _currentAspectRatio;
            }
            set
            {
                VLCLib.libvlc_video_set_aspect_ratio(_instance.Player.Handle, value);
            }
        }
        public ArrayList SubtitleTracks
        {
            get { return _subtitleTracks; }
        }
        public ArrayList AudioTracks
        {
            get { return _audioTracks; }
        }

        public VlcMedia(VlcInstance instance, string path)
        {
            _instance = instance;
            _defaultAspectRatio = string.Empty;
            _currentAspectRatio = string.Empty;

            Handle = VLCLib.libvlc_media_new_path(instance.Handle, path);
            if (Handle == IntPtr.Zero)
            {
                return;
            }
        }
        internal VlcMedia(IntPtr handle)
        {
            Handle = handle;
        }
        public void Dispose()
        {
            VLCLib.libvlc_media_release(Handle);
        }
        private void loadTitles() { }
        private void loadChapters() { }
        private void loadSubtitlesFromMedia()
        {
            Logger.WriteToLog("Loading Subtitle Tracks");
            try
            {
                _subtitleTracks = new ArrayList();
                int subCount = VLCLib.libvlc_video_get_spu_count(_instance.Player.Handle);
                IntPtr p_first_track = VLCLib.libvlc_video_get_spu_description(_instance.Player.Handle);
                IntPtr p_track = p_first_track;
                for (int i = 0; i < subCount; i++)
                {
                    VLCLib.libvlc_track_description_t desc =
                        (VLCLib.libvlc_track_description_t)Marshal.PtrToStructure(p_track,
                        typeof(VLCLib.libvlc_track_description_t));

                    _subtitleTracks.Add(desc);
                    if (desc.p_next != IntPtr.Zero)
                        p_track = desc.p_next;
                }

                VLCLib.libvlc_track_description_release(p_first_track);
            }
            catch (Exception e)
            {
                Logger.WriteToLog("Error loading subtitles: " + e.Message);
            }
        }
        private void loadAudioTracksFromMedia()
        {
            Logger.WriteToLog("Loading Audio Outputs and Devices");
            try
            {
                _audioTracks = new ArrayList();
                int subCount = VLCLib.libvlc_audio_get_track_count(_instance.Player.Handle);
                IntPtr p_first_track = VLCLib.libvlc_audio_get_track_description(_instance.Player.Handle);
                IntPtr p_track = p_first_track;
                for (int i = 0; i < subCount; i++)
                {
                    VLCLib.libvlc_track_description_t desc =
                        (VLCLib.libvlc_track_description_t)Marshal.PtrToStructure(p_track,
                        typeof(VLCLib.libvlc_track_description_t));

                    _audioTracks.Add(desc);
                    if (desc.p_next != IntPtr.Zero)
                        p_track = desc.p_next;
                }
                VLCLib.libvlc_track_description_release(p_first_track);
            }
            catch (Exception e)
            {
                Logger.WriteToLog("Error loading audio tracks: " + e.Message);
            }
        }
        public void LoadMediaMetaData()
        {
            loadTitles();
            loadChapters();
            loadAudioTracksFromMedia();
            loadSubtitlesFromMedia();
        }
    }
}
