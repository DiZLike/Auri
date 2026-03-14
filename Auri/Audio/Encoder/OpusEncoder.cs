// OpusEncoder.cs (упрощенный)
using Auri.Services;
using System;
using System.Diagnostics;
using System.IO;
using Un4seen.Bass;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Auri.Audio.Encoder
{
    public class OpusEncoder : IEncoder
    {
        public event Action<string> OnError;
        public event Action<int, float> OnProgress;
        public event Action<int, bool> OnComplete;

        private readonly BassAudioService _bass;
        private readonly EncodeService _encoderService;
        private readonly AudioFile _inputAudio;
        private int _streamHandle;
        private int _encoderHandle;
        private readonly string _encoderPath;

        public OpusEncoder(BassAudioService bass, AudioFile inputAudio)
        {
            _bass = bass;
            _encoderService = new EncodeService(_bass);
            _inputAudio = inputAudio;
            _encoderService.OnProgress += (progress) => OnProgress?.Invoke(_inputAudio.Index, progress);
            _encoderService.OnComplete += (status) => OnComplete?.Invoke(_inputAudio.Index, status);
            _encoderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "encoders", "opus", "opusenc.exe");
        }
        public bool Encode(string outputAudio, EncoderSettings settings)
        {
            try
            {
                _inputAudio.Working = true;
                string args = BuildArguments(settings, outputAudio);
                _streamHandle = _encoderService.CreateStream(_inputAudio.FilePath, settings.Frequency, settings.Channels);
                _encoderHandle = _encoderService.CreateEncoder(_streamHandle, _encoderPath, args);
                _encoderService.StartEncode(_streamHandle, _encoderHandle);
                _inputAudio.Working = false;
                _inputAudio.Completed = true;
                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Opus encoding error: {ex.Message}");
                return false;
            }
        }

        private string BuildArguments(EncoderSettings settings, string outputAudio)
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

        public void Stop()
        {
            _encoderService.StopEncode();
        }
    }
}