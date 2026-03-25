using Engine.Services;

namespace Engine.Audio.Encoder.Codec
{
    public class WaveEncoder : EncoderBase
    {
        protected override string EncoderSubPath { get; set; } = String.Empty;
        protected override string EncoderFileName { get; set; } = String.Empty;
        public override string Extension { get; } = "wav";
        public WaveEncoder(AudioEngineService bass, AudioFile inputAudio)
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
            bool isOk = _encoderService.WriteWaveFile(stream, $"{outputAudio}", settings.SampleRate, settings.Channels, settings.BitsPerSample);
            return isOk;
        }
    }
}