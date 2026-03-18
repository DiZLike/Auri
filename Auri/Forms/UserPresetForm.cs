using Auri.Audio.Encoder;
using Auri.Managers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Auri.Forms
{
    public partial class UserPresetForm : Form
    {
        public event Action<EncoderPreset> EncoderPresetChanged;

        private readonly string _format;
        private readonly ConfigManager _config;

        // Константы для MP3
        private static readonly object[] Mp3CbrBitrates = { 320, 256, 192, 160, 128, 96, 64, 56, 48, 40, 32 };
        private static readonly object[] Mp3VbrBitrates =
        {
            "~245", "~225", "~190", "~175", "~165",
            "~130", "~115", "~100", "~85", "~65"
        };

        // Маппинги для параметров LossyWav
        public UserPresetForm(string format, ConfigManager config)
        {
            InitializeComponent();
            _format = format?.ToLower() ?? throw new ArgumentNullException(nameof(format));
            _config = config ?? throw new ArgumentNullException(nameof(config));

            LoadPreset();
            SelectTabByFormat(format);
        }
        private void LoadPreset()
        {
            try
            {
                LoadOpusPreset();
                LoadMp3Preset();
                LoadFlacPreset();
                LoadWavePreset();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пресета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Opus Preset Methods
        private void LoadOpusPreset()
        {
            EncoderPreset preset = _config.GetUserEncoderPreset("opus");
            if (preset == null) return;

            SetComboBoxValue(cmbOpusBitrate, preset.Bitrate.ToString());
            SetComboBoxValue(cmbOpusChannels, preset.Channels > 1 ? "Stereo" : "Mono");

            if (preset.CustomParams.ContainsKey("Mode"))
                SetComboBoxValue(cmbOpusMode, preset.CustomParams["Mode"].ToString());
            else cmbOpusMode.SelectedIndex = 0;
            if (preset.CustomParams.ContainsKey("Framesize"))
                SetComboBoxValue(cmbOpusFramesize, preset.CustomParams["Framesize"].ToString());
            else cmbOpusFramesize.SelectedIndex = 0;
            if (preset.CustomParams.ContainsKey("Complexity"))
                tbOpusQuality.Value = ConvertToInt(preset.CustomParams["Complexity"]);
            else tbOpusQuality.Value = 0;
            if (preset.CustomParams.ContainsKey("Content"))
                SetComboBoxValue(cmbOpusContent, preset.CustomParams["Content"].ToString());
            else cmbOpusContent.SelectedIndex = 0;
        }
        private void SaveOpusPreset()
        {
            try
            {
                EncoderPreset preset = new EncoderPreset();
                preset.Bitrate = TryParseInt(cmbOpusBitrate.SelectedItem?.ToString()) ?? 0;
                preset.SampleRate = TryParseInt(cmbOpusFrequency.SelectedItem?.ToString()) ?? 0;
                preset.Channels = cmbOpusChannels.SelectedItem?.ToString() == "Mono" ? 1 : 2;

                preset.CustomParams?.Clear();
                preset.CustomParams["Mode"] = cmbOpusMode.SelectedItem.ToString().ToLower();
                preset.CustomParams["Framesize"] = cmbOpusFramesize.SelectedItem.ToString().ToLower();
                preset.CustomParams["Complexity"] = tbOpusQuality.Value;
                preset.CustomParams["Content"] = cmbOpusContent.SelectedItem.ToString().ToLower();

                EncoderPresetChanged?.Invoke(preset);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении пресета Opus: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region MP3 Preset Methods
        private void LoadMp3Preset()
        {
            Dictionary<string, string> mp3ChannelModeMap = new Dictionary<string, string>
            {
                { "s", "Stereo" },
                { "j", "Joint Stereo" },
                { "f", "Forced Joint Stereo" },
                { "d", "Dual Channels" },
                { "m", "Mono" }
            };

            EncoderPreset preset = _config.GetUserEncoderPreset("mp3");
            if (preset == null) return;

            SetComboBoxValue(cmbMp3Frequency, preset.SampleRate.ToString());
            if (preset.CustomParams.ContainsKey("Mode"))
            {
                SetComboBoxValue(cmbMp3Mode, preset.CustomParams["Mode"].ToString());
                if (preset.CustomParams["Mode"].ToString() != "vbr")
                    SetComboBoxValue(cmbMp3Bitrate, preset.Bitrate.ToString());
                else
                    cmbMp3Bitrate.SelectedIndex = preset.Bitrate;
            }
            else cmbMp3Mode.SelectedIndex = 0;

            

            if (preset.CustomParams.ContainsKey("ChannelMode"))
                SetComboBoxValue(cmbMp3Channels, mp3ChannelModeMap[preset.CustomParams["ChannelMode"].ToString()]);
            else cmbMp3Channels.SelectedIndex = 0;
            if (preset.CustomParams.ContainsKey("Quality"))
                tbMp3Quality.Value = ConvertToInt(preset.CustomParams["Quality"]);
            else tbMp3Quality.Value = 0;
        }
        private void SaveMp3Preset()
        {
            Dictionary<string, string> mp3ChannelModeMap = new Dictionary<string, string>
            {
                { "Stereo", "s" },
                { "Joint Stereo", "j" },
                { "Forced Joint Stereo", "f" },
                { "Dual Channels", "d" },
                { "Mono", "m" }
            };
            try
            {
                EncoderPreset preset = new EncoderPreset
                {
                    Bitrate = GetMp3Bitrate(),
                    SampleRate = TryParseInt(cmbMp3Frequency.SelectedItem?.ToString()) ?? 0,
                    Channels = cmbMp3Channels.SelectedItem?.ToString() == "Mono" ? 1 : 2
                };

                // режим битрейта
                preset.CustomParams?.Clear();
                preset.CustomParams["Mode"] = cmbMp3Mode.SelectedItem.ToString().ToLower();

                // стерео режим
                preset.CustomParams["ChannelMode"] = mp3ChannelModeMap[cmbMp3Channels.SelectedItem.ToString()];

                preset.CustomParams["Quality"] = tbMp3Quality.Value;

                EncoderPresetChanged?.Invoke(preset);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении пресета MP3: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        #region FLAC Preset Methods
        private void LoadFlacPreset()
        {
            Dictionary<string, string> lossyQualityMap = new Dictionary<string, string>
            {
                { "I", "Наивысшее" },
                { "E", "Высокое" },
                { "H", "Повышенное" },
                { "S", "Стандартное" },
                { "C", "Среднее" },
                { "P", "Портативное" },
                { "X", "Низкое" }
            };

            EncoderPreset preset = _config.GetUserEncoderPreset("flac");
            if (preset == null) return;

            SetComboBoxValue(cmbFlacBitPerSample, preset.BitsPerSample.ToString());
            if (preset.CustomParams.ContainsKey("Compress"))
                tbFlacCompress.Value = ConvertToInt(preset.CustomParams["Compress"]);
            else tbFlacCompress.Value = 0;
            if (preset.CustomParams.ContainsKey("UseLossyWav"))
                cbLossyWav.Checked = ConvertToBool(preset.CustomParams["UseLossyWav"]);
            else cbLossyWav.Checked = false;
            if (preset.CustomParams.ContainsKey("LossyWavQuality"))
                SetComboBoxValue(cmbLossyWavQuality, lossyQualityMap[preset.CustomParams["LossyWavQuality"].ToString()]);
            else cmbLossyWavQuality.SelectedIndex = 0;
        }
        private void SaveFlacPreset()
        {
            Dictionary<string, string> lossyQualityMap = new Dictionary<string, string>
            {
                { "Наивысшее", "I" },
                { "Высокое", "E" },
                { "Повышенное", "H" },
                { "Стандартное", "S" },
                { "Среднее", "C" },
                { "Портативное", "P" },
                { "Низкое", "X" }
            };
            try
            {

                EncoderPreset preset = new EncoderPreset();
                preset.BitsPerSample = TryParseInt(cmbFlacBitPerSample.SelectedItem?.ToString()) ?? 16;

                preset.CustomParams?.Clear();
                preset.CustomParams["Compress"] = tbFlacCompress.Value;
                preset.CustomParams["UseLossyWav"] = cbLossyWav.Checked;
                preset.CustomParams["LossyWavQuality"] = lossyQualityMap[cmbLossyWavQuality.SelectedItem.ToString()];

                EncoderPresetChanged?.Invoke(preset);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении пресета FLAC: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region WAV Preset Methods
        private void LoadWavePreset()
        {
            EncoderPreset preset = _config.GetUserEncoderPreset("wav");
            if (preset == null) return;

            SetComboBoxValue(cmbWaveFrequency, preset.SampleRate.ToString());
            SetComboBoxValue(cmbWaveBitPerSample, preset.BitsPerSample.ToString());
            SetComboBoxValue(cmbWaveChannels, preset.Channels > 1 ? "Stereo" : "Mono");
        }
        private void SaveWavePreset()
        {
            try
            {
                EncoderPreset preset = new EncoderPreset();
                preset.SampleRate = ConvertToInt(cmbWaveFrequency.SelectedItem.ToString());
                preset.BitsPerSample = ConvertToInt(cmbWaveBitPerSample.SelectedItem.ToString());
                preset.Channels = cmbWaveChannels.SelectedItem.ToString() == "Mono" ? 1 : 2;

                EncoderPresetChanged?.Invoke(preset);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении пресета FLAC: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Helper Methods
        private static int? TryParseInt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (int.TryParse(value, out int result))
                return result;
            return null;
        }
        private static int ConvertToInt(object value)
        {
            if (value == null) return 0;

            if (value is int intValue)
                return intValue;

            if (value is string stringValue && int.TryParse(stringValue, out int parsedValue))
                return parsedValue;

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }
        private static bool ConvertToBool(object value)
        {
            if (value == null) return false;

            if (value is bool boolValue)
                return boolValue;

            if (value is string stringValue && bool.TryParse(stringValue, out bool parsedValue))
                return parsedValue;

            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }
        private void SetComboBoxValue(ComboBox comboBox, string value)
        {
            if (comboBox == null || string.IsNullOrEmpty(value)) return;

            // Ищем точное совпадение
            int index = comboBox.FindStringExact(value);
            if (index == -1)
                index = comboBox.FindString(value);

            // Если не нашли, ищем частичное совпадение (без учета регистра)
            if (index < 0)
            {
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    string itemValue = comboBox.Items[i]?.ToString();
                    if (!string.IsNullOrEmpty(itemValue) &&
                        itemValue.Equals(value, StringComparison.OrdinalIgnoreCase))
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (index >= 0)
            {
                comboBox.SelectedIndex = index;
            }
        }
        private void SelectTabByFormat(string format)
        {
            var tabPage = tabControl1.TabPages
                .Cast<TabPage>()
                .FirstOrDefault(tp => tp.Text.Equals(format, StringComparison.OrdinalIgnoreCase));
            if (tabPage != null)
                tabControl1.SelectedTab = tabPage;
        }
        #endregion

        #region Event Handlers
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                switch (_format)
                {
                    case "opus":
                        SaveOpusPreset();
                        break;
                    case "mp3":
                        SaveMp3Preset();
                        break;
                    case "flac":
                        SaveFlacPreset();
                        break;
                    case "wav":
                        SaveWavePreset();
                        break;
                    default:
                        MessageBox.Show($"Неизвестный формат: {_format}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void cmbMp3Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMp3Bitrate == null) return;

            cmbMp3Bitrate.Items.Clear();

            // Индексы 0 и 1 - CBR режимы, остальные - VBR
            if (cmbMp3Mode.SelectedIndex < 2)
            {
                if (Mp3CbrBitrates != null && Mp3CbrBitrates.Length > 0)
                {
                    cmbMp3Bitrate.Items.AddRange(Mp3CbrBitrates);
                }
            }
            else
            {
                if (Mp3VbrBitrates != null && Mp3VbrBitrates.Length > 0)
                {
                    cmbMp3Bitrate.Items.AddRange(Mp3VbrBitrates);
                }
            }

            if (cmbMp3Bitrate.Items.Count > 0)
            {
                cmbMp3Bitrate.SelectedIndex = 0;
            }
        }
        private void cbLossyWav_CheckedChanged(object sender, EventArgs e)
        {
            if (cmbLossyWavQuality != null)
            {
                cmbLossyWavQuality.Enabled = cbLossyWav.Checked;
            }
        }

        #endregion

        private void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}