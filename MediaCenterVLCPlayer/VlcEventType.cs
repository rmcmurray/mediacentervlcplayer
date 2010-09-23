using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCenterVLCPlayer
{
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
}
