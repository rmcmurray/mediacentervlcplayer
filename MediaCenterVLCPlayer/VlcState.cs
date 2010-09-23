using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCenterVLCPlayer
{
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
}
