using Auri.Audio.Encoder.Codec;
using Auri.Services;
using System;
namespace Auri.Audio.Encoder
{
    public static class EncoderFactory
    {
        public static IEncoder Create(string format, BassAudioService bass, AudioFile inputAudio)
        {
            switch (format.ToLower())
            {
                case "opus":
                    return new OpusEncoder(bass, inputAudio);
                case "mp3":
                    return new LameEncoder(bass, inputAudio);
                case "flac":
                    return new FlacEncoder(bass, inputAudio);
                case "wave":
                    return new WaveEncoder(bass, inputAudio);
                default:
                    throw new NotSupportedException($"Format {format} not supported");
            }
        }
    }
}