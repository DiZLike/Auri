using Auri.Services;

namespace Auri.Audio.Encoder.Codec
{
    public class OpusEncoder : EncoderBase
    {
        protected override string EncoderSubPath { get; set; } = "opus";
        protected override string EncoderFileName { get; set; } = "opusenc.exe";

        public OpusEncoder(BassAudioService bass, AudioFile inputAudio)
            : base(bass, inputAudio)
        {
        }

        protected override string BuildArguments(EncoderPreset settings, string outputAudio)
        {
            string mode = settings.CustomParams?["Mode"] as string ?? "vbr";
            string content = settings.CustomParams?["Content"] as string ?? "music";
            string complexity = settings.CustomParams?["Complexity"] as string ?? "10";
            string frameSize = settings.CustomParams?["Framesize"] as string ?? "20";
            string downmix = settings.Channels > 1 ? "stereo" : "mono";

            return $"--bitrate {settings.Bitrate} " +
                   $"--{mode} " +
                   $"--{content} " +
                   $"--comp {complexity} " +
                   $"--framesize {frameSize.ToString().Replace(",", ".")} " +
                   $"--downmix-{downmix} " +
                   $"- \"{outputAudio}\"";
        }
    }
}