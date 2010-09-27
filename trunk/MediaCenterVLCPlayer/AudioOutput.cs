using System;
using System.Collections;
using System.Collections.Generic;

namespace MediaCenterVLCPlayer
{
    class AudioOutput
    {
        private VLCLib.libvlc_audio_output_t _output;
        private ArrayList _outputDevices;

        public AudioOutput(VLCLib.libvlc_audio_output_t output)
        {
            _output = output;
            _outputDevices = new ArrayList();
        }

        public void AddDevice(object device)
        {
            _outputDevices.Add(device);
        }
    }
}
