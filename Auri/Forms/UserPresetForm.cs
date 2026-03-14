using Auri.Audio.Encoder;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Auri.Forms
{
    public partial class UserPresetForm : Form
    {
        public event Action<EncoderSettings> EncoderSettingsChanged;
        public string _format;
        private EncoderSettings _settings;
        public UserPresetForm(string format, EncoderSettings settings = null)
        {
            InitializeComponent();
            _format = format.ToLower();
            _settings = settings ?? new EncoderSettings();
            GetOpusPreset();
        }
        private void GetOpusPreset()
        {
            SetComboBoxSelectedItem(cmbOpusBitrate, _settings.Bitrate.ToString());
            SetComboBoxSelectedItem(cmbOpusChannels, _settings.Channels > 1 ? "Stereo" : "Mono");

            // Словарь для маппинга параметров на соответствующие комбобоксы
            var paramToComboBox = new Dictionary<string, ComboBox>
            {
                ["mode"] = cmbOpusMode,
                ["framesize"] = cmbOpusFramesize,
                ["complexity"] = cmbOpusComp,
                ["content"] = cmbOpusContent
            };

            // Единообразная обработка всех параметров
            foreach (var kvp in paramToComboBox)
            {
                if (_settings.CustomParams.TryGetValue(kvp.Key, out object value))
                    SetComboBoxSelectedItem(kvp.Value, value.ToString().ToLower());
            }
        }
        private void SetOpusPreset()
        {
            // Сохраняем основные параметры
            if (int.TryParse(cmbOpusBitrate.SelectedItem?.ToString(), out int bitrate))
                _settings.Bitrate = bitrate;
            if (int.TryParse(cmbOpusFrequency.SelectedItem?.ToString(), out int frequency))
                _settings.Frequency = frequency;

            _settings.Channels = cmbOpusChannels.SelectedItem?.ToString() == "Mono" ? 1 : 2;

            // Очищаем старые кастомные параметры
            _settings.CustomParams.Clear();

            // Словарь для маппинга комбобоксов на названия параметров
            var comboBoxToParam = new Dictionary<ComboBox, string>
            {
                [cmbOpusMode] = "mode",
                [cmbOpusFramesize] = "framesize",
                [cmbOpusComp] = "complexity",
                [cmbOpusContent] = "content"
            };

            // Сохраняем все кастомные параметры
            foreach (var kvp in comboBoxToParam)
            {
                if (kvp.Key.SelectedItem != null)
                {
                    _settings.CustomParams[kvp.Value] = kvp.Key.SelectedItem.ToString().ToLower();
                }
            }

            // Вызываем событие об изменении настроек
            EncoderSettingsChanged?.Invoke(_settings);
        }

        private void SetComboBoxSelectedItem(ComboBox comboBox, string value)
        {
            int index = comboBox.FindStringExact(value);
            if (index >= 0) // Проверяем, что элемент найден
            {
                comboBox.SelectedIndex = index;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetOpusPreset();
            Close();
        }
    }
}