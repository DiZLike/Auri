using Auri.Services;

namespace Auri.Audio.Encoder.Codec
{
    public class LameEncoder : EncoderBase
    {
        protected override string EncoderSubPath => "mp3";
        protected override string EncoderFileName => "lame.exe";

        public LameEncoder(BassAudioService bass, AudioFile inputAudio)
            : base(bass, inputAudio)
        {
        }

        protected override string BuildArguments(EncoderSettings settings, string outputAudio)
        {
            string mode = settings.CustomParams?["mode"] as string ?? "cbr";
            string channelMode = settings.CustomParams?["channelMode"] as string ?? "j";
            int quality = settings.CustomParams?["quality"] as int? ?? 0;

            string bitrate;
            if (mode == "abr")
                bitrate = $"--abr {settings.Bitrate}";
            else if (mode == "vbr")
                bitrate = $"-V {settings.Bitrate}";
            else // cbr по умолчанию
                bitrate = $"-b {settings.Bitrate}";

            string resample = (settings.Frequency / 1000f).ToString().Replace(",", ".");

            return $"{bitrate} " +
                   $"-m {channelMode} " +
                   $"-q {quality} " +
                   $"--lowpass {settings.Frequency / 2} " +
                   $"--resample {resample} " +
                   $"- \"{outputAudio}\"";
        }
    }
}