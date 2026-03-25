using Engine.Audio.Encoder;

namespace Auri.Wizard
{
    public class QuickStartResult
    {
        public required string Format { get; set; }
        public required string FormatDisplayName { get; set; }
        public required string Description { get; set; }
        public required EncoderPreset Preset { get; set; }
    }
}