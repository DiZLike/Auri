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
        public int Bitrate { get; set; } = 0;
        public int BitsPerSample { get; set; } = 16;
        public string Format { get; set; } = "Unk";
        public Dictionary<string, object> CustomParams { get; set; } = new Dictionary<string, object>();
        public override string ToString()
        {
            string mes = String.Empty;
            string bitrate = String.Empty;
            if (Bitrate > 0)
                bitrate = $"Битрейт: {Bitrate} кбит/с";
            else
            {
                if (CustomParams.ContainsKey("AvgBitrate"))
                    bitrate = $"Битрейт: ~{CustomParams["AvgBitrate"]} кбит/с";
            }
            return $"Формат: {Format}\n" +
                    $"Частота: {SampleRate}\n" +
                    $"{bitrate}\n";
        }
    }
}