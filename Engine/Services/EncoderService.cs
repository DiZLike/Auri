using Engine.Managers;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.Misc;

namespace Engine.Services
{
    public class EncoderService
    {
        public event Action<float> OnProgress;
        public event Action<bool> OnComplete;
        private AudioEngineService _bass;
        private volatile bool _isUserStopped;
        public bool IsAborted => _isUserStopped;
        public EncoderService(AudioEngineService bass)
        {
            _bass = bass;
        }

        public int CreateStream(string audioFile, int sampleRate, int chans)
        {
            if (!File.Exists(audioFile))
            {
                ExceptionManager.RaiseError(Error.FILE_NOT_FOUND, audioFile);
                return 0;
            }
            int stream = Bass.BASS_StreamCreateFile(audioFile, 0, 0,
                BASSFlag.BASS_DEFAULT | BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT);
            if (stream == 0)
                ExceptionManager.RaiseBassError(Error.STREAM_CREATE_FAILED, audioFile);

            int mixerHandel = CreateMixerStream(stream, sampleRate, chans);
            if (mixerHandel == 0)
            {
                Bass.BASS_StreamFree(stream);
                return 0;
            }
            return mixerHandel;
        }
        private int CreateMixerStream(int sourceStream, int targetSampleRate, int targetChannels)
        {
            int mixerHandel = BassMix.BASS_Mixer_StreamCreate(targetSampleRate, targetChannels, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT);
            if (mixerHandel == 0)
            { 
                ExceptionManager.RaiseBassError(Error.MIXER_CREATE_FAILED);
                return 0;
            }

            bool isError = !BassMix.BASS_Mixer_StreamAddChannel(mixerHandel, sourceStream, BASSFlag.BASS_DEFAULT);
            if (isError)
                ExceptionManager.RaiseBassError(Error.MIXER_ADD_CHANNEL_FAILED);
            return mixerHandel;
        }
        public int CreateEncoder(int stream, string encoderPath, string args, int bitsPerSample)
        {
            BASSEncode flags = BASSEncode.BASS_ENCODE_DEFAULT;
            if (bitsPerSample == 32)
                flags |= BASSEncode.BASS_ENCODE_FP_32BIT;
            else if (bitsPerSample == 24)
                flags |= BASSEncode.BASS_ENCODE_FP_24BIT;
            else if (bitsPerSample == 16)
                flags |= BASSEncode.BASS_ENCODE_FP_16BIT;
            else if (bitsPerSample == 8)
                flags = BASSEncode.BASS_ENCODE_FP_8BIT;

            int encoder = BassEnc.BASS_Encode_Start(
                stream, $"\"{encoderPath}\" {args}", flags, null, IntPtr.Zero);
            if (encoder == 0)
                ExceptionManager.RaiseBassError(Error.ENCODER_START_FAILED);
            return encoder;
        }
        public bool StartEncode(int stream, int encoder, int pass, int totalPass)
        {
            bool isError = false;
            _isUserStopped = false;
            long fileLength = Bass.BASS_ChannelGetLength(stream);
            float[] buffer = new float[65536];
            int bytesRead;

            float lastProgress = -1;
            const float progressThreshold = 0.1f;

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            const int minUpdateIntervalMs = 100;
            try
            {
                do
                {
                    if (_isUserStopped)
                        break;
                    bytesRead = Bass.BASS_ChannelGetData(stream, buffer, buffer.Length);
                    if (bytesRead > 0)
                    {
                        if (stopwatch.ElapsedMilliseconds >= minUpdateIntervalMs)
                        {
                            long currentPos = Bass.BASS_ChannelGetPosition(stream);
                            float currentPassProgress = (currentPos * 100f) / fileLength;

                            float totalProgress;

                            if (totalPass > 1)
                            {
                                float passWeight = 100f / totalPass;
                                float completedPassesProgress = (pass - 1) * passWeight;
                                float currentPassContribution = (currentPassProgress * passWeight) / 100f;
                                totalProgress = completedPassesProgress + currentPassContribution;
                            }
                            else
                            {
                                // Если только один проход, используем обычный прогресс
                                totalProgress = currentPassProgress;
                            }

                            if (Math.Abs(totalProgress - lastProgress) >= progressThreshold)
                            {
                                lastProgress = totalProgress;
                                OnProgress?.Invoke((float)Math.Round(totalProgress, 2));
                                stopwatch.Restart();
                            }
                        }
                    }
                }
                while (bytesRead > 0 && !_isUserStopped);

                // Убеждаемся, что отправляем 100% только когда все проходы завершены
                if (pass == totalPass && !_isUserStopped)
                {
                    OnProgress?.Invoke(100f);
                    OnComplete.Invoke(true);
                }
                else if (_isUserStopped)
                {
                    ExceptionManager.RaiseError(Error.OPERATION_ABORTED);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                ExceptionManager.RaiseBassError(Error.DECODE_FAILED, ex.Message);
                OnComplete?.Invoke(false);
                return false;
            }
            finally
            {
                StopEncode(stream, encoder);
            }
            return !isError;
        }
        public bool WriteWaveFile(int stream, string outputFile, int sampleRate, int channels, int bitsPerSample)
        {
            _isUserStopped = false;
            bool isError = false;
            WaveWriter waveWriter = null;

            long fileLength = Bass.BASS_ChannelGetLength(stream);
            float[] buffer = new float[65536];
            int bytesRead;

            float lastProgress = -1;
            const float progressThreshold = 0.1f;

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            const int minUpdateIntervalMs = 100;

            try
            {
                waveWriter = CreateWaveWriter(outputFile, channels, sampleRate, bitsPerSample);

                do
                {
                    if (_isUserStopped)
                        break;

                    bytesRead = Bass.BASS_ChannelGetData(stream, buffer, buffer.Length);

                    if (bytesRead > 0)
                    {
                        waveWriter.Write(buffer, bytesRead);

                        if (stopwatch.ElapsedMilliseconds >= minUpdateIntervalMs)
                        {
                            long currentPos = Bass.BASS_ChannelGetPosition(stream);
                            float currentProgress = (currentPos * 100f) / fileLength;

                            if (Math.Abs(currentProgress - lastProgress) >= progressThreshold)
                            {
                                lastProgress = currentProgress;
                                OnProgress?.Invoke((float)Math.Round(currentProgress, 2));
                                stopwatch.Restart();
                            }
                        }
                    }
                }
                while (bytesRead > 0 && !_isUserStopped);

                // Убеждаемся, что отправляем 100% только когда завершено
                if (!_isUserStopped)
                {
                    OnProgress?.Invoke(100f);
                    OnComplete?.Invoke(true);
                }
                else if (_isUserStopped)
                {
                    ExceptionManager.RaiseError(Error.OPERATION_ABORTED);
                }
            }
            catch (Exception ex)
            {
                isError = true;
                ExceptionManager.RaiseBassError(Error.DECODE_FAILED, ex.Message);
                OnComplete?.Invoke(false);
                return false;
            }
            finally
            {
                if (waveWriter != null)
                    waveWriter.Close();
                StopEncode(stream, 0);
            }
            return !isError;
        }
        private WaveWriter CreateWaveWriter(string outputFile, int channels, int sampleRate, int bitsPerSample)
        {
            switch (bitsPerSample)
            {
                case 8:
                    return new WaveWriter(outputFile, channels, sampleRate, 8, true);
                case 16:
                    return new WaveWriter(outputFile, channels, sampleRate, 16, true);
                case 24:
                    return new WaveWriter(outputFile, channels, sampleRate, 24, true);
                case 32:
                    return new WaveWriter(outputFile, channels, sampleRate, 32, true);
                default:
                    ExceptionManager.RaiseError(Error.UNSUPPORTED_BITSPERSAMPLE);
                    return new WaveWriter(outputFile, channels, sampleRate, 16, true);
            }
        }
        public void AbortEncode()
        {
            _isUserStopped = true;
        }
        public void StopEncode(int stream, int encoder)
        {
            if (encoder != 0)
            {
                if (!BassEnc.BASS_Encode_Stop(encoder))
                    ExceptionManager.RaiseBassError(Error.ENCODER_STOP_FAILED);
            }
            if (stream != 0)
            {
                if (!Bass.BASS_StreamFree(stream))
                    ExceptionManager.RaiseBassError(Error.STREAM_FREE_FAILED);
            }
        }
    }
}