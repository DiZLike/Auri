using System;
using System.Drawing;
using System.Windows.Forms;
using Auri.Audio.Encoder;

namespace Auri.Forms.Wizard
{
    public partial class QuickStartWizardForm : Form
    {
        private int _currentStep = 0;

        public QuickStartResult Result { get; private set; }

        public QuickStartWizardForm()
        {
            InitializeComponent();
            SetupEventHandlers();
            ShowStep(0);
        }

        private void SetupEventHandlers()
        {
            _btnBack.Click += BtnBack_Click;
            _btnNext.Click += BtnNext_Click;
            _btnFinish.Click += BtnFinish_Click;
        }

        private void ShowStep(int step)
        {
            _stepPanel.Controls.Clear();

            switch (step)
            {
                case 0:
                    ShowStep0_Usage();
                    break;
                case 1:
                    ShowStep1_QualityPreference();
                    break;
                case 2:
                    ShowStep2_Device();
                    break;
                case 3:
                    ShowStep3_Summary();
                    break;
            }

            _lblStepInfo.Text = $"Шаг {step + 1} из 3";
        }

        private void ShowStep0_Usage()
        {
            Label lblQuestion = CreateStepLabel("Как вы планируете использовать конвертированные файлы?", 0, 0);

            _rbStreaming = CreateRadioButton("🎵 Стриминг / Онлайн-плееры\n(Spotify, Apple Music, YouTube Music)", 20, 40, true);
            _rbMusicLibrary = CreateRadioButton("💿 Музыкальная библиотека на ПК\n(хранение, прослушивание в локальных плеерах)", 20, 90);
            _rbArchive = CreateRadioButton("📀 Архивация / Долгосрочное хранение\n(максимальное качество, возможно место не важно)", 20, 140);
            _rbUnknown = CreateRadioButton("🤔 Не уверен / Разное", 20, 190);

            _stepPanel.Controls.AddRange(new Control[] {
                lblQuestion, _rbStreaming, _rbMusicLibrary,
                _rbArchive, _rbUnknown
            });
        }

        private void ShowStep1_QualityPreference()
        {
            Label lblQuestion = CreateStepLabel("Что для вас важнее?", 0, 0);

            _rbBestQuality = CreateRadioButton("✨ Максимальное качество\n(размер файла не важен, главное - качество звука)", 20, 40, true);
            _rbGoodBalance = CreateRadioButton("⚖️ Сбалансированный подход\n(хорошее качество при приемлемом размере)", 20, 90);
            _rbSmallSize = CreateRadioButton("📦 Минимальный размер\n(экономия места, качество второстепенно)", 20, 140);
            _rbUnknownPreference = CreateRadioButton("🎯 Хороший баланс качество/размер", 20, 190);

            _stepPanel.Controls.AddRange(new Control[] {
                lblQuestion, _rbBestQuality, _rbGoodBalance,
                _rbSmallSize, _rbUnknownPreference
            });
        }

        private void ShowStep2_Device()
        {
            Label lblQuestion = CreateStepLabel("На каких устройствах будете слушать?", 0, 0);

            _rbMobileDevice = CreateRadioButton("📱 Мобильные устройства\n(Android/iOS, ограниченное место на диске)", 20, 40, true);
            _rbPCPlayer = CreateRadioButton("💻 Компьютер / Ноутбук\n(много места, поддержка любых форматов)", 20, 90);
            _rbHomeAudio = CreateRadioButton("🏠 Домашняя аудиосистема\n(качественная аппаратура, важна поддержка форматов)", 20, 140);
            _rbUnknownDevice = CreateRadioButton("🔊 Разные устройства", 20, 190);

            _stepPanel.Controls.AddRange(new Control[] {
                lblQuestion, _rbMobileDevice, _rbPCPlayer,
                _rbHomeAudio, _rbUnknownDevice
            });
        }

        private void ShowStep3_Summary()
        {
            var recommendation = GenerateRecommendation();

            Label lblTitle = CreateStepLabel("Рекомендация готова!", 0, 0, true);
            Label lblFormat = CreateInfoLabel($"📀 Рекомендуемый формат: {recommendation.FormatDisplayName}", 0, 40, true);
            Label lblDescription = CreateInfoLabel(recommendation.Description, 0, 75);
            Label lblNote = CreateInfoLabel(
                "Вы сможете изменить эти настройки позже в главном окне.\nКнопка \"Быстрая конвертация\" будет использовать этот формат.",
                0, 150, false, System.Drawing.Color.Gray, 8);

            _stepPanel.Controls.AddRange(new Control[] {
                lblTitle, lblFormat, lblDescription, lblNote
            });

            Result = recommendation;
        }

        private QuickStartResult GenerateRecommendation()
        {
            var userChoices = GetUserChoices();
            var result = new QuickStartResult();

            // Логика выбора формата
            if (userChoices.IsArchive || (userChoices.IsBestQuality && userChoices.IsHomeAudio))
            {
                result.Format = "flac";
                result.FormatDisplayName = "FLAC (без потерь)";
                result.Description = "Формат без потерь, сохраняющий оригинальное качество. Идеален для архивации и качественной домашней аппаратуры.";
                result.Preset = CreateFlacPreset();
            }
            else if (userChoices.IsMobile || (userChoices.IsSmallSize && !userChoices.IsArchive))
            {
                result.Format = "opus";
                result.FormatDisplayName = "Opus (оптимальный для мобильных)";
                result.Description = "Современный формат с отличным сжатием. При минимальном размере сохраняет хорошее качество. Идеален для мобильных устройств.";
                result.Preset = CreateMobileOpusPreset();
            }
            else if (userChoices.IsStreaming || (userChoices.IsGoodBalance && userChoices.IsPC))
            {
                result.Format = "mp3";
                result.FormatDisplayName = "MP3 (универсальный)";
                result.Description = "Самый совместимый формат. Работает везде, хороший баланс качества и размера.";
                result.Preset = CreateMp3Preset();
            }
            else
            {
                result.Format = "mp3";
                result.FormatDisplayName = "MP3 (среднее качество)";
                result.Description = "Универсальный формат, подходящий для большинства ситуаций.";
                result.Preset = CreateDefaultMp3Preset();
            }

            // Корректируем настройки в зависимости от приоритета качества
            if (userChoices.IsBestQuality && result.Format != "flac")
            {
                IncreasePresetQuality(result.Preset, result.Format);
            }
            else if (userChoices.IsSmallSize && result.Format != "flac")
            {
                DecreasePresetQuality(result.Preset, result.Format);
            }

            return result;
        }

        private UserChoices GetUserChoices()
        {
            return new UserChoices
            {
                IsStreaming = _rbStreaming?.Checked ?? false,
                IsMusicLibrary = _rbMusicLibrary?.Checked ?? false,
                IsArchive = _rbArchive?.Checked ?? false,
                IsBestQuality = _rbBestQuality?.Checked ?? false,
                IsGoodBalance = _rbGoodBalance?.Checked ?? false,
                IsSmallSize = _rbSmallSize?.Checked ?? false,
                IsMobile = _rbMobileDevice?.Checked ?? false,
                IsPC = _rbPCPlayer?.Checked ?? false,
                IsHomeAudio = _rbHomeAudio?.Checked ?? false
            };
        }

        private EncoderPreset CreateFlacPreset()
        {
            return new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                BitsPerSample = 16,
                Bitrate = 0,
                CustomParams = new System.Collections.Generic.Dictionary<string, object>
                {
                    ["Compress"] = 5,
                    ["UseLossyWav"] = false,
                    ["LossyWavQuality"] = "S"
                }
            };
        }

        private EncoderPreset CreateMobileOpusPreset()
        {
            return new EncoderPreset
            {
                SampleRate = 48000,
                Channels = 2,
                Bitrate = 96,
                CustomParams = new System.Collections.Generic.Dictionary<string, object>
                {
                    ["Mode"] = "vbr",
                    ["Content"] = "music",
                    ["Complexity"] = 10,
                    ["Framesize"] = 20
                }
            };
        }

        private EncoderPreset CreateMp3Preset()
        {
            return new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                Bitrate = 192,
                CustomParams = new System.Collections.Generic.Dictionary<string, object>
                {
                    ["Mode"] = "cbr",
                    ["ChannelMode"] = "j",
                    ["Quality"] = 0
                }
            };
        }

        private EncoderPreset CreateDefaultMp3Preset()
        {
            return new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                Bitrate = 128,
                CustomParams = new System.Collections.Generic.Dictionary<string, object>
                {
                    ["Mode"] = "cbr",
                    ["ChannelMode"] = "j",
                    ["Quality"] = 2
                }
            };
        }

        private void IncreasePresetQuality(EncoderPreset preset, string format)
        {
            if (format == "mp3")
            {
                preset.Bitrate = 320;
                preset.CustomParams["Quality"] = 0;
            }
            else if (format == "opus")
            {
                preset.Bitrate = 192;
                preset.CustomParams["Framesize"] = 10;
            }
        }

        private void DecreasePresetQuality(EncoderPreset preset, string format)
        {
            if (format == "mp3")
            {
                preset.Bitrate = 96;
                preset.CustomParams["Quality"] = 5;
            }
            else if (format == "opus")
            {
                preset.Bitrate = 48;
                preset.CustomParams["Framesize"] = 40;
            }
        }

        // Вспомогательные методы для создания UI элементов
        private Label CreateStepLabel(string text, int x, int y, bool isBold = false)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", isBold ? 11 : 9, isBold ? FontStyle.Bold : FontStyle.Regular),
                Location = new Point(x, y),
                Size = new Size(500, 30),
                BackColor = Color.White
            };
        }

        private RadioButton CreateRadioButton(string text, int x, int y, bool isChecked = false)
        {
            return new RadioButton
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(460, text.Contains("\n") ? 40 : 30),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White,
                Checked = isChecked
            };
        }

        private Label CreateInfoLabel(string text, int x, int y, bool isBold = false, Color? foreColor = null, int fontSize = 9)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", fontSize, isBold ? FontStyle.Bold : FontStyle.Regular),
                Location = new Point(x, y),
                Size = new Size(500, text.Contains("\n") ? 60 : 25),
                BackColor = Color.White,
                ForeColor = foreColor ?? Color.Black
            };
        }

        // Обработчики событий
        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (_currentStep > 0)
            {
                _currentStep--;
                ShowStep(_currentStep);
            }

            _btnNext.Visible = true;
            _btnFinish.Visible = false;

            _btnBack.Enabled = _currentStep != 0;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (_currentStep < 3)
            {
                _currentStep++;
                ShowStep(_currentStep);
            }

            if (_currentStep == 3)
            {
                _btnNext.Visible = false;
                _btnFinish.Visible = true;
            }

            _btnBack.Enabled = true;
        }

        private void BtnFinish_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        // Вспомогательный класс для хранения выбора пользователя
        private class UserChoices
        {
            public bool IsStreaming { get; set; }
            public bool IsMusicLibrary { get; set; }
            public bool IsArchive { get; set; }
            public bool IsBestQuality { get; set; }
            public bool IsGoodBalance { get; set; }
            public bool IsSmallSize { get; set; }
            public bool IsMobile { get; set; }
            public bool IsPC { get; set; }
            public bool IsHomeAudio { get; set; }
        }
    }
}