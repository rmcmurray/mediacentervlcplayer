﻿using System;
using System.Runtime.InteropServices;

namespace MediaCenterVLCPlayer
{
    class VlcException : Exception
    {
        protected string _err;

        public VlcException()
            : base()
        {
            IntPtr errorPointer = VLCLib.libvlc_errmsg();
            _err = errorPointer == IntPtr.Zero ? "VLC Exception"
                : Marshal.PtrToStringAuto(errorPointer);
        }

        public override string Message
        {
            get { return _err; }
        }
    }
}
