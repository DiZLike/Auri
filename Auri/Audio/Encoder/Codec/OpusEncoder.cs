using Auri.Services;
using System.Globalization;

namespace Auri.Audio.Encoder.Codec
{
    public class OpusEncoder : EncoderBase
    {
        protected override string EncoderSubPath { get; set; } = "opus";
        protected override string EncoderFileName { get; set; } = "opusenc.exe";
        public override string Extension { get; } = "opus";

        public OpusEncoder(AudioEngineService bass, AudioFile inputAudio)
            : base(bass, inputAudio)
        {
        }

        protected override string BuildArguments(EncoderPreset settings, string outputAudio)
        {
            string mode = settings.CustomParams?["Mode"] as string ?? "vbr";
            string content = settings.CustomParams?["Content"] as string ?? "music";
            int complexity = Convert.ToInt32(settings.CustomParams?["Complexity"]) as int? ?? 10;
            int frameSize = Convert.ToInt32(settings.CustomParams?["Framesize"]) as int? ?? 20;
            string downmix = settings.Channels > 1 ? "stereo" : "mono";

            return $"--bitrate {settings.Bitrate} " +
                   $"--{mode} " +
                   $"--{content} " +
                   $"--comp {complexity} " +
                   $"--framesize {frameSize.ToString(CultureInfo.InvariantCulture)} " +
                   $"--downmix-{downmix} " +
                   $"- \"{outputAudio}\"";
        }
    }
}