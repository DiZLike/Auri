using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;

namespace Auri.Services
{
    public class EncodeService
    {
        public event Action<string> OnError;
        private BassAudioService _bass;
        private bool _isUserStopped;


        public EncodeService(BassAudioService bass)
        {
            _bass = bass;
        }

        public int CreateStream(string audioFile)
        {
            int stream = Bass.BASS_StreamCreateFile(audioFile, 0, 0,
                BASSFlag.BASS_DEFAULT | BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_DECODE);
            if (stream == 0)
                OnError?.Invoke($"Ошибка создания потока аудио: {Bass.BASS_ErrorGetCode()}");
            return stream;
        }
        public void FreeStream(int stream)
        {
            Bass.BASS_StreamFree(stream);
        }
        public int CreateEncoder(int stream, string encoderPath, string args)
        {
            int encoder = BassEnc.BASS_Encode_Start(
                stream, $"\"{encoderPath}\" {args}", 0, null, IntPtr.Zero);
            if (encoder == 0)
                OnError?.Invoke($"Ошибка инициализации энкодера: {Bass.BASS_ErrorGetCode()}");
            return encoder;
        }

        public void StartEncode(int stream, int encoder)
        {
            byte[] buffer = new byte[65536];
            int bytesRead;
            do
            {
                bytesRead = Bass.BASS_ChannelGetData(stream, buffer, buffer.Length);
            }
            while (bytesRead > 0 || !_isUserStopped);
            StopEncode(stream, encoder);
        }
        public void StopEncode()
        {
            _isUserStopped = true;
        }
        public void StopEncode(int stream, int encoder)
        {
            BassEnc.BASS_Encode_Stop(encoder);
            FreeStream(stream);
        }
    }
}
