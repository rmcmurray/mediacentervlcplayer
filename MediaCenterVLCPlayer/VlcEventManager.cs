using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MediaCenterVLCPlayer
{
    public class VlcEventManager
    {
        private readonly List<VlcEventType> _AttachedEvents;
        public static IntPtr Handle;

        public void InitalizeEvents()
        {
            AttachEvent(VlcEventType.libvlc_MediaPlayerEndReached, MediaPlayerEnded, IntPtr.Zero);
        }

        internal VlcEventManager(IntPtr p_event_manager)
        {
            _AttachedEvents = new List<VlcEventType>();
            VlcEventManager.Handle = p_event_manager;
        }

        internal void AttachEvent(VlcEventType type, VLCLib.EventCallbackDelegate callback, IntPtr userdata)
        {
            VLCLib.libvlc_event_attach(VlcEventManager.Handle, (uint)type, callback, userdata, null);
            _AttachedEvents.Add(type);
        }

        internal void DetachEvent(VlcEventType type, VLCLib.EventCallbackDelegate callback, IntPtr userdata)
        {
            VLCLib.libvlc_event_detach(VlcEventManager.Handle, (uint)type, callback, userdata, string.Empty);
            _AttachedEvents.Remove(type);
        }

        internal bool IsAttached(VlcEventType type)
        {
            return _AttachedEvents.Contains(type);
        }

        public void MediaPlayerEnded(ref VlcEventType type, IntPtr userdata)
        {
            Form1.Closeapp();
        }
    }
}
