using Auri.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                default:
                    throw new NotSupportedException($"Format {format} not supported");
            }
        }
    }
}