using Engine.Managers;
using Engine.Services;

namespace Engine.Audio.Encoder
{
    public abstract class EncoderBase : IEncoder
    {
        public event Action<int, float> OnProgress;
        public event Action<int, bool> OnComplete;

        protected readonly AudioEngineService _bass;
        protected readonly EncoderService _encoderService;
        protected readonly AudioFile _inputAudio;
        protected int _streamHandle;
        protected int _encoderHandle;
        protected string _encoderPath;
        protected abstract string EncoderSubPath { get; set; }
        protected abstract string EncoderFileName { get; set; }
        public abstract string Extension { get; }

        protected EncoderBase(AudioEngineService bass, AudioFile inputAudio)
        {
            _bass = bass;
            _encoderService = new EncoderService(_bass);
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
                if (!File.Exists(_encoderPath))
                {
                    ExceptionManager.RaiseError(Error.ENCODER_FILE_IS_MISSING, EncoderFileName);
                    return false;
                }

                _streamHandle = _encoderService.CreateStream(
                    _inputAudio.FilePath, settings.SampleRate, settings.Channels);
                if (_streamHandle == 0)
                {
                    _inputAudio.Working = false;
                    return false; // Не удалось создать поток
                }

                _encoderHandle = _encoderService.CreateEncoder(
                    _streamHandle, _encoderPath, args, settings.BitsPerSample);
                if (_encoderHandle == 0)
                {
                    _inputAudio.Working = false;
                    return false; // Не удалось создать энкодер
                }

                bool success = _encoderService.StartEncode(_streamHandle, _encoderHandle, pass, totalPass);

                _inputAudio.Working = false;
                _inputAudio.Completed = success;

                return success;
            }
            catch (Exception ex)
            {
                ExceptionManager.RaiseError(Error.ENCODE_FAILED, ex.Message);
                _inputAudio.Working = false;
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