using Auri.Audio.Encoder;
using Auri.Managers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Auri.Forms
{
    public partial class UserPresetForm : Form
    {
        public event Action<EncoderSettings> EncoderSettingsChanged;

        private readonly string _format;
        private readonly ConfigManager _config;

        // Константы для MP3
        private static readonly object[] Mp3CbrBitrates = { 320, 256, 192, 160, 128, 96, 64, 56, 48, 40, 32 };
        private static readonly object[] Mp3VbrBitrates =
        {
            "~245 кбит/с", "~225 кбит/с", "~190 кбит/с", "~175 кбит/с", "~165 кбит/с",
            "~130 кбит/с", "~115 кбит/с", "~100 кбит/с", "~85 кбит/с", "~65 кбит/с"
        };

        // Маппинги для параметров
        private static readonly Dictionary<string, string> Mp3ChannelModeMap = new Dictionary<string, string>
        {
            { "s", "Stereo" },
            { "j", "Joint Stereo" },
            { "f", "Forced Joint Stereo" },
            { "d", "Dual Channels" },
            { "m", "Mono" }
        };

        private static readonly Dictionary<string, string> Mp3ChannelModeReverseMap = new Dictionary<string, string>
        {
            { "Stereo", "s" },
            { "Joint Stereo", "j" },
            { "Forced Joint Stereo", "f" },
            { "Dual Channels", "d" },
            { "Mono", "m" }
        };

        public UserPresetForm(string format, ConfigManager config)
        {
            InitializeComponent();
            _format = format?.ToLower() ?? throw new ArgumentNullException(nameof(format));
            _config = config ?? throw new ArgumentNullException(nameof(config));

            LoadPreset();
        }

        private void LoadPreset()
        {
            LoadOpusPreset();
            LoadMp3Preset();
        }

        #region Opus Preset Methods

        private void LoadOpusPreset()
        {
            EncoderSettings settings = _config.GetUserEncoderPreset("opus");

            SetComboBoxValue(cmbOpusBitrate, settings.Bitrate.ToString());
            SetComboBoxValue(cmbOpusChannels, settings.Channels > 1 ? "Stereo" : "Mono");

            Dictionary<string, ComboBox> paramToComboBox = new Dictionary<string, ComboBox>
            {
                { "mode", cmbOpusMode },
                { "framesize", cmbOpusFramesize },
                { "complexity", cmbOpusComp },
                { "content", cmbOpusContent }
            };

            foreach (KeyValuePair<string, ComboBox> kvp in paramToComboBox)
            {
                string paramName = kvp.Key;
                ComboBox comboBox = kvp.Value;

                if (settings.CustomParams.TryGetValue(paramName, out object value))
                {
                    SetComboBoxValue(comboBox, value.ToString().ToLower());
                }
            }
        }

        private void SaveOpusPreset()
        {
            EncoderSettings settings = new EncoderSettings
            {
                Bitrate = TryParseInt(cmbOpusBitrate.SelectedItem?.ToString()) ?? 0,
                Frequency = TryParseInt(cmbOpusFrequency.SelectedItem?.ToString()) ?? 0,
                Channels = cmbOpusChannels.SelectedItem?.ToString() == "Mono" ? 1 : 2
            };

            settings.CustomParams.Clear();

            Dictionary<ComboBox, string> comboBoxToParam = new Dictionary<ComboBox, string>
            {
                { cmbOpusMode, "mode" },
                { cmbOpusFramesize, "framesize" },
                { cmbOpusComp, "complexity" },
                { cmbOpusContent, "content" }
            };

            foreach (KeyValuePair<ComboBox, string> kvp in comboBoxToParam)
            {
                ComboBox comboBox = kvp.Key;
                string paramName = kvp.Value;

                if (comboBox.SelectedItem != null)
                {
                    settings.CustomParams[paramName] = comboBox.SelectedItem.ToString().ToLower();
                }
            }

            EncoderSettingsChanged?.Invoke(settings);
        }

        #endregion

        #region MP3 Preset Methods

        private void LoadMp3Preset()
        {
            EncoderSettings settings = _config.GetUserEncoderPreset("mp3");

            SetComboBoxValue(cmbMp3Frequency, settings.Frequency.ToString());

            Dictionary<string, ComboBox> paramToComboBox = new Dictionary<string, ComboBox>
            {
                { "mode", cmbMp3Mode },
                { "quality", cmbMp3Comp },
                { "channelMode", cmbMp3Channels }
            };

            foreach (KeyValuePair<string, ComboBox> kvp in paramToComboBox)
            {
                string paramName = kvp.Key;
                ComboBox comboBox = kvp.Value;

                if (settings.CustomParams.TryGetValue(paramName, out object value))
                {
                    string displayValue = value.ToString();

                    // Преобразуем код канала в отображаемое имя
                    if (paramName == "channelMode" && Mp3ChannelModeMap.ContainsKey(displayValue))
                    {
                        displayValue = Mp3ChannelModeMap[displayValue];
                    }

                    SetComboBoxValue(comboBox, displayValue.ToLower());
                }
            }

            // Устанавливаем битрейт
            if (settings.CustomParams.TryGetValue("mode", out object modeObj) && modeObj?.ToString() == "vbr")
            {
                cmbMp3Bitrate.SelectedIndex = settings.Bitrate;
            }
            else
            {
                SetComboBoxValue(cmbMp3Bitrate, settings.Bitrate.ToString());
            }
        }

        private void SaveMp3Preset()
        {
            EncoderSettings settings = new EncoderSettings
            {
                Bitrate = GetMp3Bitrate(),
                Frequency = TryParseInt(cmbMp3Frequency.SelectedItem?.ToString()) ?? 0,
                Channels = cmbMp3Channels.SelectedItem?.ToString() == "Mono" ? 1 : 2
            };

            settings.CustomParams.Clear();

            Dictionary<ComboBox, string> comboBoxToParam = new Dictionary<ComboBox, string>
            {
                { cmbMp3Mode, "mode" },
                { cmbMp3Comp, "quality" },
                { cmbMp3Channels, "channelMode" }
            };

            foreach (KeyValuePair<ComboBox, string> kvp in comboBoxToParam)
            {
                ComboBox comboBox = kvp.Key;
                string paramName = kvp.Value;

                if (comboBox.SelectedItem != null)
                {
                    string value = comboBox.SelectedItem.ToString();

                    // Преобразуем отображаемое имя канала в код
                    if (paramName == "channelMode" && Mp3ChannelModeReverseMap.ContainsKey(value))
                    {
                        value = Mp3ChannelModeReverseMap[value];
                    }

                    settings.CustomParams[paramName] = value.ToLower();
                }
            }

            EncoderSettingsChanged?.Invoke(settings);
        }

        private int GetMp3Bitrate()
        {
            if (cmbMp3Bitrate.SelectedItem == null) return 0;

            // Для VBR режима битрейт - это индекс элемента
            if (cmbMp3Mode.SelectedIndex > 1)
            {
                return cmbMp3Bitrate.SelectedIndex;
            }

            // Для CBR режима парсим число
            int? bitrate = TryParseInt(cmbMp3Bitrate.SelectedItem.ToString());
            return bitrate ?? 0;
        }

        #endregion

        #region Helper Methods

        private static int? TryParseInt(string value)
        {
            if (int.TryParse(value, out int result))
                return result;
            return null;
        }

        private void SetComboBoxValue(ComboBox comboBox, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            int index = comboBox.FindStringExact(value);
            if (index >= 0)
            {
                comboBox.SelectedIndex = index;
            }
        }

        #endregion

        #region Event Handlers

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (_format)
            {
                case "opus":
                    SaveOpusPreset();
                    break;
                case "mp3":
                    SaveMp3Preset();
                    break;
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbMp3Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMp3Bitrate.Items.Clear();

            // Индексы 0 и 1 - CBR режимы, остальные - VBR
            if (cmbMp3Mode.SelectedIndex < 2)
            {
                cmbMp3Bitrate.Items.AddRange(Mp3CbrBitrates);
            }
            else
            {
                cmbMp3Bitrate.Items.AddRange(Mp3VbrBitrates);
            }

            if (cmbMp3Bitrate.Items.Count > 0)
            {
                cmbMp3Bitrate.SelectedIndex = 0;
            }
        }

        #endregion
    }
}