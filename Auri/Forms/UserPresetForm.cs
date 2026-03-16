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
            EncoderSettings settings = _config.GetUserEncoderPreset("opus");
            if (settings == null) return;

            SetComboBoxValue(cmbOpusBitrate, settings.Bitrate.ToString());
            SetComboBoxValue(cmbOpusChannels, settings.Channels > 1 ? "Stereo" : "Mono");

            if (settings.CustomParams.ContainsKey("mode"))
                SetComboBoxValue(cmbOpusMode, settings.CustomParams["mode"].ToString());
            else cmbOpusMode.SelectedIndex = 0;
            if (settings.CustomParams.ContainsKey("framesize"))
                SetComboBoxValue(cmbOpusFramesize, settings.CustomParams["framesize"].ToString());
            else cmbOpusFramesize.SelectedIndex = 0;
            if (settings.CustomParams.ContainsKey("complexity"))
                SetComboBoxValue(cmbOpusComp, settings.CustomParams["complexity"].ToString());
            else cmbOpusComp.SelectedIndex = 0;
            if (settings.CustomParams.ContainsKey("content"))
                SetComboBoxValue(cmbOpusContent, settings.CustomParams["content"].ToString());
            else cmbOpusContent.SelectedIndex = 0;
        }
        private void SaveOpusPreset()
        {
            try
            {
                EncoderSettings settings = new EncoderSettings();
                settings.Bitrate = TryParseInt(cmbOpusBitrate.SelectedItem?.ToString()) ?? 0;
                settings.Frequency = TryParseInt(cmbOpusFrequency.SelectedItem?.ToString()) ?? 0;
                settings.Channels = cmbOpusChannels.SelectedItem?.ToString() == "Mono" ? 1 : 2;

                settings.CustomParams?.Clear();
                settings.CustomParams["mode"] = cmbOpusMode.SelectedItem.ToString().ToLower();
                settings.CustomParams["framesize"] = cmbOpusFramesize.SelectedItem.ToString().ToLower();
                settings.CustomParams["complexity"] = cmbOpusComp.SelectedItem.ToString().ToLower();
                settings.CustomParams["content"] = cmbOpusContent.SelectedItem.ToString().ToLower();

                EncoderSettingsChanged?.Invoke(settings);
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

            EncoderSettings settings = _config.GetUserEncoderPreset("mp3");
            if (settings == null) return;

            SetComboBoxValue(cmbMp3Frequency, settings.Frequency.ToString());
            if (settings.CustomParams.ContainsKey("mode"))
            {
                SetComboBoxValue(cmbMp3Mode, settings.CustomParams["mode"].ToString());
                if (settings.CustomParams["mode"].ToString() != "vbr")
                    SetComboBoxValue(cmbMp3Bitrate, settings.Bitrate.ToString());
                else
                    cmbMp3Bitrate.SelectedIndex = settings.Bitrate;
            }
            else cmbMp3Mode.SelectedIndex = 0;

            

            if (settings.CustomParams.ContainsKey("channelMode"))
                SetComboBoxValue(cmbMp3Channels, mp3ChannelModeMap[settings.CustomParams["channelMode"].ToString()]);
            else cmbMp3Channels.SelectedIndex = 0;
            if (settings.CustomParams.ContainsKey("quality"))
                SetComboBoxValue(cmbMp3Comp, settings.CustomParams["quality"].ToString());
            else cmbMp3Comp.SelectedIndex = 0;
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
                EncoderSettings settings = new EncoderSettings
                {
                    Bitrate = GetMp3Bitrate(),
                    Frequency = TryParseInt(cmbMp3Frequency.SelectedItem?.ToString()) ?? 0,
                    Channels = cmbMp3Channels.SelectedItem?.ToString() == "Mono" ? 1 : 2
                };

                // режим битрейта
                settings.CustomParams?.Clear();
                settings.CustomParams["mode"] = cmbMp3Mode.SelectedItem.ToString().ToLower();

                // стерео режим
                settings.CustomParams["channelMode"] = mp3ChannelModeMap[cmbMp3Channels.SelectedItem.ToString()];

                // Извлекаем число из качества алгоритма
                Match match = Regex.Match(cmbMp3Comp.SelectedItem.ToString(), @"\d+");
                if (match.Success)
                    settings.CustomParams["quality"] = int.Parse(match.Value);
                else
                    settings.CustomParams["quality"] = 0;

                EncoderSettingsChanged?.Invoke(settings);
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

            EncoderSettings settings = _config.GetUserEncoderPreset("flac");
            if (settings == null) return;

            SetComboBoxValue(cmbFlacBitPerSample, settings.BitsPerSample.ToString());
            if (settings.CustomParams.ContainsKey("compress"))
                tbFlacCompress.Value = ConvertToInt(settings.CustomParams["compress"]);
            else tbFlacCompress.Value = 0;
            if (settings.CustomParams.ContainsKey("useLossyWav"))
                cbLossyWav.Checked = ConvertToBool(settings.CustomParams["useLossyWav"]);
            else cbLossyWav.Checked = false;
            if (settings.CustomParams.ContainsKey("lossyWavQuality"))
                SetComboBoxValue(cmbLossyWavQuality, lossyQualityMap[settings.CustomParams["lossyWavQuality"].ToString()]);
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

                EncoderSettings settings = new EncoderSettings();
                settings.BitsPerSample = TryParseInt(cmbFlacBitPerSample.SelectedItem?.ToString()) ?? 16;

                settings.CustomParams?.Clear();
                settings.CustomParams["compress"] = tbFlacCompress.Value;
                settings.CustomParams["useLossyWav"] = cbLossyWav.Checked;
                settings.CustomParams["lossyWavQuality"] = lossyQualityMap[cmbLossyWavQuality.SelectedItem.ToString()];

                EncoderSettingsChanged?.Invoke(settings);
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
    }
}