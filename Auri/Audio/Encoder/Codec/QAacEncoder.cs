using Auri.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auri.Audio.Encoder.Codec
{
    public class QAacEncoder : EncoderBase
    {
        protected override string EncoderSubPath { get; set; } = "qaac";
        protected override string EncoderFileName { get; set; } = "qaac64.exe";
        public override string Extension { get; } = "m4a";
        public QAacEncoder(AudioEngineService bass, AudioFile inputAudio)
            : base(bass, inputAudio)
        {

        }

        protected override string BuildArguments(EncoderPreset settings, string outputAudio)
        {
            int sampleRate = settings.SampleRate;
            int bitrate = settings.Bitrate;
            string mode = settings.CustomParams?["Mode"] as string ?? "cbr";
            int vbrBitrate = Convert.ToInt32(settings.CustomParams?["VbrBitrate"] ?? 67);
            bool he = Convert.ToBoolean(settings.CustomParams?["He"] as bool? ?? false);
            int quality = Convert.ToInt32(settings.CustomParams?["Quality"] ?? 1);

            var bitrateStr = String.Empty;
            var heStr = String.Empty;

            // Bitrate Mode
            if (mode == "vbr")
                bitrateStr = $"--tvbr {vbrBitrate} ";
            else if (mode == "abr")
                bitrateStr = $"--abr {bitrate} ";
            else if (mode == "cvbr")
                bitrateStr = $"--cvbr {bitrate} ";
            else if (mode == "cbr")
                bitrateStr = $"--cbr {bitrate} ";

            // High Efficiency
            if (he)
                heStr = "--he ";

            return $"{bitrateStr}" +
                   $"{heStr}" +
                   $"--quality {quality} " +
                   $"--rate {sampleRate} " +
                   $"- -o \"{outputAudio}{Extension}\"";
        }
    }
}
