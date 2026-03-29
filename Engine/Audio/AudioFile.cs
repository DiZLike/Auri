using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Audio
{
    public class AudioFile
    {
        public string FilePath { get; set; }
        public int Index { get; set; }
        public bool Completed { get; set; }
        public bool Working { get; set; }
        public bool IsCue { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double StartTimeSec { get { return StartTime.TotalSeconds; }  }
        public double EndTimeSec { get { return EndTime.TotalSeconds; } }
        public AudioFile(string path, int index, bool isCue, TimeSpan startTime, TimeSpan endTime)
        {
            FilePath = path;
            Completed = false;
            Working = false;
            Index = index;
            IsCue = isCue;
            StartTime = startTime;
            EndTime = endTime;
        }
        public AudioFile(string path, int index)
        {
            FilePath = path;
            Completed = false;
            Working = false;
            Index = index;
            IsCue = false;
            StartTime = TimeSpan.Zero;
            EndTime = TimeSpan.Zero;
        }
    }
}