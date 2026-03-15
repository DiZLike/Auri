using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib.Gif;

namespace Auri.Audio
{
    public class AudioInfo
    {
        public string Name { get; set; }
        public string Codec {  get; set; }
        public string Duration { get; set; }
        public float Frequency { get; set; }
        public int Bitrate { get; set; }
        public int Channels { get; set; }
        public AudioInfo(string name, string codec, string duration, float frequency, int bitrate, int channels)
        {
            Name = name;
            Codec = codec;
            Duration = duration;
            Frequency = frequency;
            Bitrate = bitrate;
            Channels = channels;
        }
    }
}
