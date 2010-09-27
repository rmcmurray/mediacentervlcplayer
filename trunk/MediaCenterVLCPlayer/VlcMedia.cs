using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MediaCenterVLCPlayer
{
    public class VlcMedia : IDisposable
    {
        public static IntPtr Handle;
        public static VlcMedia Instance;
        private ArrayList _subtitleTracks;
        private ArrayList _audioTracks;
        private string _defaultAspectRatio;
        private string _currentAspectRatio;

        private void loadTitles() { }
        private void loadChapters() { }
        private void loadSubtitlesFromMedia()
        {
            Logger.WriteToLog("Loading Subtitle Tracks");
            try
            {
                _subtitleTracks = new ArrayList();
                int subCount = VLCLib.libvlc_video_get_spu_count(VlcMediaPlayer.Handle);
                IntPtr p_first_track = VLCLib.libvlc_video_get_spu_description(VlcMediaPlayer.Handle);
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
                int subCount = VLCLib.libvlc_audio_get_track_count(VlcMediaPlayer.Handle);
                IntPtr p_first_track = VLCLib.libvlc_audio_get_track_description(VlcMediaPlayer.Handle);
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
        internal VlcMedia(IntPtr handle)
        {
            VlcMedia.Handle = handle;
        }

        public VlcMedia(VlcInstance instance, string path)
        {
            VlcMedia.Instance = this;
            _defaultAspectRatio = string.Empty;
            _currentAspectRatio = string.Empty;

            VlcMedia.Handle = VLCLib.libvlc_media_new_path(VlcInstance.Handle, path);
            if (VlcMedia.Handle == IntPtr.Zero)
            {
                return;
            }
        }
        public void LoadMediaMetaData()
        {
            loadTitles();
            loadChapters();
            loadAudioTracksFromMedia();
            loadSubtitlesFromMedia();
        }
        public void Dispose()
        {
            VLCLib.libvlc_media_release(VlcMedia.Handle);
        }
        public long Length
        {
            get { return VLCLib.libvlc_media_player_get_length(VlcMediaPlayer.Handle); }
        }
        public int TrackCount
        {
            get { return VLCLib.libvlc_audio_get_track_count(VlcMediaPlayer.Handle); }
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
                    VLCLib.libvlc_video_get_size(VlcMediaPlayer.Handle, 0, ref w, ref h);
                    float width = (float)Convert.ToDecimal(w);
                    float height = (float)Convert.ToDecimal(h);
                    if (width > 0.0 && height > 0.0)
                    {
                        string currentRatio = (width / height).ToString();
                        if (currentRatio.Length > 4)
                            currentRatio = currentRatio.Substring(0, 4);
                        _defaultAspectRatio = currentRatio;
                    }
                }
                return _currentAspectRatio == string.Empty ? _defaultAspectRatio : _currentAspectRatio;
            }
            set
            {
                VLCLib.libvlc_video_set_aspect_ratio(VlcMediaPlayer.Handle, value);
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
    }
}
