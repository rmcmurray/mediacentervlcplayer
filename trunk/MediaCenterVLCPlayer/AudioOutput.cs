using System;
using System.Collections;
using System.Collections.Generic;

namespace MediaCenterVLCPlayer
{
    class AudioOutput
    {
        private VLCLib.libvlc_audio_output_t _output;
        private ArrayList _outputDevices;

        public ArrayList Devices
        {
            get { return _outputDevices; }
        }

        public VLCLib.libvlc_audio_output_t RawOutput
        {
            get { return _output; }
        }

        public AudioOutput(VLCLib.libvlc_audio_output_t output)
        {
            _output = output;
            _outputDevices = new ArrayList();
        }

        public void AddDevice(AudioDevice device)
        {
            _outputDevices.Add(device);
        }
    }
}
