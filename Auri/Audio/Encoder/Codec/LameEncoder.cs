using Auri.Services;
using System;
using System.Globalization;

namespace Auri.Audio.Encoder.Codec
{
    public class LameEncoder : EncoderBase
    {
        protected override string EncoderSubPath { get; set; } = "mp3";
        protected override string EncoderFileName { get; set; } = "lame.exe";
        public override string Extension { get; } = "mp3";

        public LameEncoder(AudioEngineService bass, AudioFile inputAudio)
            : base(bass, inputAudio)
        {
        }

        protected override string BuildArguments(EncoderPreset settings, string outputAudio)
        {
            string mode = settings.CustomParams?["Mode"] as string ?? "cbr";
            string channelMode = settings.CustomParams?["ChannelMode"] as string ?? "j";
            int quality = Convert.ToInt32(settings.CustomParams?["Quality"] ?? 0);

            string bitrate;
            if (mode == "abr")
                bitrate = $"--abr {settings.Bitrate}";
            else if (mode == "vbr")
                bitrate = $"-V {settings.Bitrate}";
            else // cbr по умолчанию
                bitrate = $"-b {settings.Bitrate}";

            string resample = (settings.SampleRate / 1000f).ToString(CultureInfo.InvariantCulture);

            return $"{bitrate} " +
                   $"-m {channelMode} " +
                   $"-q {quality} " +
                   $"--lowpass {settings.SampleRate / 2} " +
                   $"--resample {resample} " +
                   $"- \"{outputAudio}\"";
        }
    }
}