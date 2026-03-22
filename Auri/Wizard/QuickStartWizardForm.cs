using Auri.Audio.Encoder;
using Auri.Wizard;
using Auri.Wizard.Recommendation;
using Auri.Wizard.Recommendation.Strategy;

namespace Auri.Forms.Wizard
{
    public partial class QuickStartWizardForm : Form
    {
        private int _currentStep = 0;
        private readonly List<IRecommendationStrategy> _strategies;

        // Выбор пользователя
        private string _usageType;      // "mobile", "pc", "home", "apple", "max_compat"
        private string _qualityPriority; // "best", "balanced", "compact"
        private string _specialNeed;     // "high_efficiency", "streaming", "none"

        public QuickStartResult Result { get; private set; }

        public QuickStartWizardForm()
        {
            InitializeComponent();

            // Инициализация стратегий
            _strategies = new List<IRecommendationStrategy>
            {
                new MaxCompatibilityStrategy(),
                new AppleStrategy(),
                new HomeAudioStrategy(),
                new DefaultOpusStrategy()
            }.OrderBy(s => s.Priority).ToList();

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
                    ShowStep1_QualityPriority();
                    break;
                case 2:
                    ShowStep2_SpecialNeeds();
                    break;
                case 3:
                    ShowStep3_Summary();
                    break;
            }

            if (step < 3)
                _lblStepInfo.Text = $"Шаг {step + 1} из 3";
            else
                _lblStepInfo.Text = "Готово";
        }

        private void ShowStep0_Usage()
        {
            Label lblQuestion = CreateStepLabel("Где вы планируете слушать музыку?", 0, 0, true, 11);

            var group = new GroupBox
            {
                Location = new Point(20, 40),
                Size = new Size(460, 290),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            _rbMobileDevice = CreateRadioButton("📱 На телефоне\n(экономия места, для прогулок и поездок)", 10, 20, true);
            _rbPCPlayer = CreateRadioButton("💻 На компьютере / ноутбуке\n(универсальность, любое программное обеспечение)", 10, 75);
            _rbHomeAudio = CreateRadioButton("🏠 На домашней аудиосистеме\n(колонки, ресивер, максимальное качество)", 10, 130);
            _rbAppleDevice = CreateRadioButton("🍎 В экосистеме Apple\n(iPhone, iPad, Mac, AirPods)", 10, 185);
            _rbMaxCompatibility = CreateRadioButton("🚗 Старые устройства / автомагнитола\n(максимальная совместимость, MP3)", 10, 240);

            var radios = new[] { _rbMobileDevice, _rbPCPlayer, _rbHomeAudio, _rbAppleDevice, _rbMaxCompatibility };
            foreach (var radio in radios)
            {
                radio.CheckedChanged += (s, e) =>
                {
                    if (_rbMobileDevice.Checked) _usageType = "mobile";
                    else if (_rbPCPlayer.Checked) _usageType = "pc";
                    else if (_rbHomeAudio.Checked) _usageType = "home";
                    else if (_rbAppleDevice.Checked) _usageType = "apple";
                    else if (_rbMaxCompatibility.Checked) _usageType = "max_compat";
                };
            }

            group.Controls.AddRange(radios);
            _stepPanel.Controls.Add(lblQuestion);
            _stepPanel.Controls.Add(group);
        }

        private void ShowStep1_QualityPriority()
        {
            Label lblQuestion = CreateStepLabel("Что для вас важнее?", 0, 0, true, 11);

            var group = new GroupBox
            {
                Location = new Point(20, 40),
                Size = new Size(460, 180),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            _rbBestQuality = CreateRadioButton("🎵 Максимальное качество звука\n(размер файла не имеет значения)", 10, 20);
            _rbGoodBalance = CreateRadioButton("⚖️ Сбалансированный подход\n(хорошее качество и приемлемый размер)", 10, 75, true);
            _rbSmallSize = CreateRadioButton("💾 Минимальный размер файлов\n(экономия места на диске)", 10, 130);

            var radios = new[] { _rbBestQuality, _rbGoodBalance, _rbSmallSize };
            foreach (var radio in radios)
            {
                radio.CheckedChanged += (s, e) =>
                {
                    if (_rbBestQuality.Checked) _qualityPriority = "best";
                    else if (_rbGoodBalance.Checked) _qualityPriority = "balanced";
                    else if (_rbSmallSize.Checked) _qualityPriority = "compact";
                };
            }

            group.Controls.AddRange(radios);
            _stepPanel.Controls.Add(lblQuestion);
            _stepPanel.Controls.Add(group);
        }

        private void ShowStep2_SpecialNeeds()
        {
            Label lblQuestion = CreateStepLabel("Есть ли особые требования?", 0, 0, true, 11);

            var group = new GroupBox
            {
                Location = new Point(20, 40),
                Size = new Size(460, 180),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            _rbHighEfficiency = CreateRadioButton("⚡ Высокая эффективность сжатия\n(минимальный размер при нормальном качестве)", 10, 20);
            _rbStreaming = CreateRadioButton("⚡ Средняя эффективность сжатия\n(средний размер при хорошем качестве)", 10, 75);
            _rbNoSpecial = CreateRadioButton("✅ Нет особых требований\n(стандартное использование)", 10, 130, true);

            var radios = new[] { _rbHighEfficiency, _rbStreaming, _rbNoSpecial };
            foreach (var radio in radios)
            {
                radio.CheckedChanged += (s, e) =>
                {
                    if (_rbHighEfficiency.Checked) _specialNeed = "high_efficiency";
                    else if (_rbStreaming.Checked) _specialNeed = "streaming";
                    else if (_rbNoSpecial.Checked) _specialNeed = "none";
                };
            }

            group.Controls.AddRange(radios);
            _stepPanel.Controls.Add(lblQuestion);
            _stepPanel.Controls.Add(group);
        }

        private void ShowStep3_Summary()
        {
            var recommendation = GenerateRecommendation();

            Label lblTitle = CreateStepLabel("Готово! Вот наша рекомендация:", 0, 0, true, 12);

            var infoPanel = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(460, 280),
                BackColor = Color.FromArgb(248, 249, 250)
            };

            Label lblFormat = CreateInfoLabel($"🎵 Формат: {recommendation.FormatDisplayName}", 15, 15, true, Color.FromArgb(76, 175, 80), 11);
            Label lblDescription = CreateInfoLabel(recommendation.Description, 15, 55, false, Color.Black, 9);

            string qualityInfo = GetQualityInfo(recommendation.Preset, recommendation.Format);
            Label lblQuality = CreateInfoLabel(qualityInfo, 15, 130, false, Color.DarkGreen, 9);

            Label lblNote = CreateInfoLabel(
                "─────────────────────────────\n" +
                "✓ Эти настройки будут использоваться для быстрой конвертации\n" +
                "✓ Вы сможете изменить их позже в главном окне программы",
                15, 180, false, Color.Gray, 8);

            infoPanel.Controls.AddRange(new Control[] { lblFormat, lblDescription, lblQuality, lblNote });

            _stepPanel.Controls.Add(lblTitle);
            _stepPanel.Controls.Add(infoPanel);

            Result = recommendation;
        }

        private QuickStartResult GenerateRecommendation()
        {
            var context = new RecommendationContext(
                _usageType ?? "mixed",
                _qualityPriority ?? "balanced",
                _specialNeed ?? "none"
            );

            var strategy = _strategies.FirstOrDefault(s => s.CanApply(context));

            if (strategy == null)
                throw new InvalidOperationException("No applicable recommendation strategy found");

            return strategy.Apply(context);
        }

        private string GetQualityInfo(EncoderPreset preset, string format)
        {
            if (format == "flac")
                return "📊 Качество: Без потерь (FLAC)";

            if (preset.Bitrate > 0)
            {
                if (preset.Bitrate >= 256)
                    return $"📊 Качество: Высокое ({preset.Bitrate} kbps)";
                if (preset.Bitrate >= 128)
                    return $"📊 Качество: Среднее ({preset.Bitrate} kbps)";
                return $"📊 Качество: Экономичное ({preset.Bitrate} kbps)";
            }

            return "📊 Качество: Стандартное";
        }

        // Вспомогательные методы для создания UI
        private Label CreateStepLabel(string text, int x, int y, bool isBold = false, int fontSize = 9)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", fontSize, isBold ? FontStyle.Bold : FontStyle.Regular),
                Location = new Point(x, y),
                Size = new Size(500, 35),
                BackColor = Color.White
            };
        }

        private RadioButton CreateRadioButton(string text, int x, int y, bool isChecked = false)
        {
            return new RadioButton
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(440, text.Contains("\n") ? 45 : 30),
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
                Size = new Size(430, text.Contains("\n") ? 60 : 30),
                BackColor = Color.Transparent,
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
            if (!IsCurrentStepValid())
            {
                ShowWarning();
                return;
            }

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

        private bool IsCurrentStepValid()
        {
            switch (_currentStep)
            {
                case 0:
                    return _rbMobileDevice?.Checked == true ||
                           _rbPCPlayer?.Checked == true ||
                           _rbHomeAudio?.Checked == true ||
                           _rbAppleDevice?.Checked == true ||
                           _rbMaxCompatibility?.Checked == true;
                case 1:
                    return _rbBestQuality?.Checked == true ||
                           _rbGoodBalance?.Checked == true ||
                           _rbSmallSize?.Checked == true;
                case 2:
                    return _rbHighEfficiency?.Checked == true ||
                           _rbStreaming?.Checked == true ||
                           _rbNoSpecial?.Checked == true;
                default:
                    return true;
            }
        }

        private void ShowWarning()
        {
            MessageBox.Show(
                "Пожалуйста, выберите один из вариантов, чтобы продолжить.",
                "Выбор не сделан",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnFinish_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}