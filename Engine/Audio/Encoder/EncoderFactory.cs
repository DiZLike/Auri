using Engine.Audio.Encoder.Codec;
using Engine.Services;

namespace Engine.Audio.Encoder
{
    public static class EncoderFactory
    {
        public static IEncoder Create(string format, AudioEngineService bass, AudioFile inputAudio)
        {
            switch (format.ToLower())
            {
                case "opus":
                    return new OpusEncoder(bass, inputAudio);
                case "mp3":
                    return new LameEncoder(bass, inputAudio);
                case "flac":
                    return new FlacEncoder(bass, inputAudio);
                case "wav":
                    return new WaveEncoder(bass, inputAudio);
                case "qaac":
                    return new QAacEncoder(bass, inputAudio);
                default:
                    throw new NotSupportedException($"Format {format} not supported");
            }
        }
    }
}