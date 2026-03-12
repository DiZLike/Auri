namespace Auri.Audio.Encoder.Opus
{
    public class OpusParams
    {
        public int Bitrate { get; set; }
        public string Mode { get; set; }
        public string AudioType { get; set; }
        public int Complexity { get; set; }
        public int FrameSize { get; set; }
        public OpusParams()
        {
            Bitrate = 128;
            Mode = "vbr";
            AudioType = "music";
            Complexity = 10;
            FrameSize = 20;
        }
    }
}