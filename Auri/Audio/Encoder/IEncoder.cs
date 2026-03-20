using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auri.Audio.Encoder
{
    public interface IEncoder
    {
        bool Encode(string outputAudio, EncoderPreset settings, int pass, int totalPass);
        void AbortEncode();
        event Action<int, float> OnProgress;
        event Action<int, bool> OnComplete;

        string Extension { get; }
    }
    [Serializable]
    public class EncoderPreset
    {
        public int SampleRate { get; set; } = 44100;
        public int Channels { get; set; } = 2;
        public int Bitrate { get; set; } = 128;
        public int BitsPerSample { get; set; } = 16;
        public Dictionary<string, object> CustomParams { get; set; } = new Dictionary<string, object>();
    }
}