using Auri.Services;
using System;
using System.IO;

namespace Auri.Audio.Encoder
{
    public abstract class EncoderBase : IEncoder
    {
        public event Action<string> OnError;
        public event Action<int, float> OnProgress;
        public event Action<int, bool> OnComplete;

        protected readonly BassAudioService _bass;
        protected readonly EncodeService _encoderService;
        protected readonly AudioFile _inputAudio;
        protected int _streamHandle;
        protected int _encoderHandle;
        protected string _encoderPath;
        protected abstract string EncoderSubPath { get; set; }
        protected abstract string EncoderFileName { get; set; }
        public abstract string Extension { get; }

        protected EncoderBase(BassAudioService bass, AudioFile inputAudio)
        {
            _bass = bass;
            _encoderService = new EncodeService(_bass);
            _inputAudio = inputAudio;

            _encoderService.OnProgress += (progress) =>
                OnProgress?.Invoke(_inputAudio.Index, progress);
            _encoderService.OnComplete += (status) =>
                OnComplete?.Invoke(_inputAudio.Index, status);
        }

        public virtual bool Encode(string outputAudio, EncoderPreset settings, int pass, int totalPass)
        {
            try
            {
                _inputAudio.Working = true;

                string args = BuildArguments(settings, outputAudio);
                LogService.LogInfo(args);

                _encoderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "encoders", EncoderSubPath, EncoderFileName);

                _streamHandle = _encoderService.CreateStream(
                    _inputAudio.FilePath, settings.SampleRate, settings.Channels);
                _encoderHandle = _encoderService.CreateEncoder(
                    _streamHandle, _encoderPath, args, settings.BitsPerSample);

                _encoderService.StartEncode(_streamHandle, _encoderHandle, pass, totalPass);

                _inputAudio.Working = false;
                _inputAudio.Completed = true;

                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"{GetType().Name} encoding error: {ex.Message}");
                return false;
            }
        }

        protected abstract string BuildArguments(EncoderPreset settings, string outputAudio);
        public void AbortEncode()
        {
            _encoderService.AbortEncode();
        }
    }
}