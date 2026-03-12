using Auri.Audio.Encoder.Opus;
using Auri.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auri.Audio.Encoder
{
    public class OpusEncoder
    {
        public event Action<string> OnError;

        private string _opusEncoderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encoders", "opus", "opusenc.exe");

        private int _streamHandle;
        private int _encoderHandle;

        private BassAudioService _bass;

        public OpusEncoder(BassAudioService bass)
        {
            _bass = bass;
            _bass.OnError += (msg) => OnError?.Invoke(msg);
        }
        public void StartEncode(string inputAudio, string outputAudio, OpusParams args)
        {
            string arguments = $"--bitrate {args.Bitrate} " +
                $"--{args.Mode} " +
                $"--comp {args.Complexity} " +
                $"--framesize {args.FrameSize} " +
                $"- \"{outputAudio}\"";
            int stream = _bass.Encoder.CreateStream(inputAudio);
            int encoder = _bass.Encoder.CreateEncoder(stream, _opusEncoderPath, arguments);
            _bass.Encoder.StartEncode(stream, encoder);
        }
        public void StopEncode()
        {
            _bass.Encoder.StopEncode();
        }
    }
}