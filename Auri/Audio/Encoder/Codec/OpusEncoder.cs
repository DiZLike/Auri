using Auri.Services;

namespace Auri.Audio.Encoder.Codec
{
    public class OpusEncoder : EncoderBase
    {
        protected override string EncoderSubPath => "opus";
        protected override string EncoderFileName => "opusenc.exe";

        public OpusEncoder(BassAudioService bass, AudioFile inputAudio)
            : base(bass, inputAudio)
        {
        }

        protected override string BuildArguments(EncoderSettings settings, string outputAudio)
        {
            string mode = settings.CustomParams?["mode"] as string ?? "vbr";
            string content = settings.CustomParams?["content"] as string ?? "music";
            int complexity = settings.CustomParams?["complexity"] as int? ?? 10;
            float frameSize = settings.CustomParams?["framesize"] as float? ?? 20;
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