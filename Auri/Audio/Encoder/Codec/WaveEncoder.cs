using Auri.Services;
using System;

namespace Auri.Audio.Encoder.Codec
{
    public class WaveEncoder : EncoderBase
    {
        protected override string EncoderSubPath { get; set; } = String.Empty;
        protected override string EncoderFileName { get; set; } = String.Empty;
        public override string Extension { get; } = "wav";
        public WaveEncoder(BassAudioService bass, AudioFile inputAudio)
            : base(bass, inputAudio)
        {
        }
        protected override string BuildArguments(EncoderPreset settings, string outputAudio)
        {
            return String.Empty;
        }
        public override bool Encode(string outputAudio, EncoderPreset settings, int pass, int totalPass)
        {
            int stream = _encoderService.CreateStream(_inputAudio.FilePath, settings.SampleRate, settings.Channels);
            _encoderService.WriteWaveFile(stream, $"{outputAudio}{Extension}", settings.SampleRate, settings.Channels, settings.BitsPerSample);
            return true;
        }
    }
}