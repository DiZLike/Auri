using Auri.Services;

namespace Auri.Audio.Encoder.Codec
{
    public class FlacEncoder : EncoderBase
    {
        protected override string EncoderSubPath => "flac";
        protected override string EncoderFileName => "flac.exe";

        public FlacEncoder(BassAudioService bass, AudioFile inputAudio)
            : base(bass, inputAudio)
        {
        }

        protected override string BuildArguments(EncoderSettings settings, string outputAudio)
        {
            int compress = settings.CustomParams?["compress"] as int? ?? 8;

            return $"-{compress} " +
                   $"- --output-name=\"{outputAudio}\"";
        }
    }
}