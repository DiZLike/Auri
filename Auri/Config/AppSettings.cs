using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Auri.Config
{
    [Serializable]
    public class AppSettings
    {
        public FormSettings FormSettings { get; set; } = new FormSettings();
        public ConverterSettings ConverterSettings { get; set; } = new ConverterSettings();
        
    }
    [Serializable]
    public class FormSettings
    {
        public int FormX { get; set; }
        public int FormY { get; set; }
        public int FormWidth { get; set; }
        public int FormHeight { get; set; }
        public FormWindowState WindowState { get; set; }
    }
    [Serializable]
    public class ConverterSettings
    {
        public bool AdvancedMode { get; set; }
        public int OutputFormatIndex { get; set; }
        public int QualityIndex { get; set; }
        public int ThreadsCountIndex { get; set; }
        public string OutputPath { get; set; }
        public string PathPattern { get; set; }
        public bool SaveTrackList { get; set; }
        public bool RewriteAudio { get; set; }
        public List<string> TrackList { get; set; } = new List<string>();
    }
}