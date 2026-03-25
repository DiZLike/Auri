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
        public AudioFile(string path, int index)
        {
            FilePath = path;
            Completed = false;
            Working = false;
            Index = index;
        }
    }
}