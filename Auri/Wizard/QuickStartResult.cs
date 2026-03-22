using Auri.Audio.Encoder;

namespace Auri.Wizard
{
    public class QuickStartResult
    {
        public string Format { get; set; }
        public string FormatDisplayName { get; set; }
        public string Description { get; set; }
        public EncoderPreset Preset { get; set; }
    }
}