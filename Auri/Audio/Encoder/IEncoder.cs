using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auri.Audio.Encoder
{
    public interface IEncoder
    {
        bool Encode(string outputAudio, EncoderSettings settings);
        void Stop();
        event Action<string> OnError;
        event Action<int, float> OnProgress;
        event Action<int, bool> OnComplete;
    }

    public class EncoderSettings
    {
        public int Bitrate { get; set; } = 128;
        public Dictionary<string, object> CustomParams { get; set; } = new Dictionary<string, object>();
    }
}
