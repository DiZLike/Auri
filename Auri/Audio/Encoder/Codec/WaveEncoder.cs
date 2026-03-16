using Auri.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auri.Audio.Encoder.Codec
{
    public class WaveEncoder : EncoderBase
    {
        protected override string EncoderSubPath { get; set; } = "mp3";
        protected override string EncoderFileName { get; set; } = "lame.exe";
        public WaveEncoder(BassAudioService bass, AudioFile inputAudio)
            : base(bass, inputAudio)
        {
        }
        protected override string BuildArguments(EncoderSettings settings, string outputAudio)
        {
            return String.Empty;
        }
        public override bool Encode(string outputAudio, EncoderSettings settings, int pass, int totalPass)
        {
            int stream = _encoderService.CreateStream(_inputAudio.FilePath, settings.Frequency, settings.Channels);
            _encoderService.StartWaveWrite(stream, settings.Frequency, settings.BitsPerSample, settings.Channels, outputAudio);

            return true;
        }
    }
}