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
        bool Encode(string outputAudio, EncoderSettings settings, int pass, int totalPass);
        void Stop();
        event Action<string> OnError;
        event Action<int, float> OnProgress;
        event Action<int, bool> OnComplete;
    }
    [Serializable]
    public class EncoderSettings
    {
        [JsonProperty("frequency")]
        public int Frequency { get; set; } = 44100;
        [JsonProperty("channels")]
        public int Channels { get; set; } = 2;
        [JsonProperty("bitrate")]
        public int Bitrate { get; set; } = 128;
        [JsonProperty("bit")]
        public int BitsPerSample { get; set; } = 16;
        [JsonProperty("params")]
        public Dictionary<string, object> CustomParams { get; set; } = new Dictionary<string, object>();
    }
}