using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MediaCenterVLCPlayer
{
    public class VlcEventManager
    {
        private readonly List<VLCLibrary.VlcEventType> _AttachedEvents;
        private VlcInstance _instance;
        public static IntPtr Handle;

        public void InitalizeEvents()
        {
            _instance.Logger.writeToLog("Starting up Event Manager");
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaPlayerEndReached, MediaPlayerEnded, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaListPlayerPlayed, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaListPlayerStopped, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaPlayerBackward, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaPlayerBuffering, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaPlayerEncounteredError, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaPlayerForward, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaPlayerPaused, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaPlayerPlaying, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaPlayerPositionChanged, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaPlayerStopped, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaPlayerTimeChanged, LogEvent, IntPtr.Zero);
            AttachEvent(VLCLibrary.VlcEventType.libvlc_MediaStateChanged, LogEvent, IntPtr.Zero);
        }

        internal VlcEventManager(VlcInstance instance, IntPtr p_event_manager)
        {
            _instance = instance;
            _AttachedEvents = new List<VLCLibrary.VlcEventType>();
            VlcEventManager.Handle = p_event_manager;
        }

        internal void AttachEvent(VLCLibrary.VlcEventType type, VLCLibrary.EventCallbackDelegate callback, IntPtr userdata)
        {
            VLCLibrary.Instance.event_attach(VlcEventManager.Handle, (uint)type, callback, userdata, null);
            _AttachedEvents.Add(type);
        }

        internal void DetachEvent(VLCLibrary.VlcEventType type, VLCLibrary.EventCallbackDelegate callback, IntPtr userdata)
        {
            VLCLibrary.Instance.event_detach(VlcEventManager.Handle, (uint)type, callback, userdata, string.Empty);
            _AttachedEvents.Remove(type);
        }

        internal bool IsAttached(VLCLibrary.VlcEventType type)
        {
            return _AttachedEvents.Contains(type);
        }

        public void MediaPlayerEnded(ref VLCLibrary.VlcEventType type, IntPtr userdata)
        {
            Form1.Instance.Closeapp();
        }

        public void LogEvent(ref VLCLibrary.VlcEventType type, IntPtr userdata)
        {
            _instance.Logger.writeToLog("[Event]: " + type.ToString());
        }
    }
}
