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
        private StrategyType _usageType;            // "mobile", "pc", "home", "apple", "max_compat"
        private StrategyQuality _qualityPriority;   // "best", "balanced", "compact"
        private StrategySpecial _specialNeed;       // "high_compress", "medium_compress", "none"

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
                new MobileStrategy(),
                new DesktopStrategy()
            }.OrderBy(s => s.Priority).ToList();

            SetupEventHandlers();
            ShowStep(0);
        }

        private void SetupEventHandlers()
        {
            _btnBack.Click += BtnBack_Click;
            _btnNext.Click += BtnNext_Click;
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

            // Обновляем текст кнопки
            if (step < 3)
            {
                _lblStepInfo.Text = $"Шаг {step + 1} из 3";
                _btnNext.Text = "Далее →";
            }
            else
            {
                _lblStepInfo.Text = "Готово";
                _btnNext.Text = "Завершить";
            }
        }

        private void ShowStep0_Usage()
        {
            Label lblQuestion = CreateStepLabel("Где вы планируете слушать музыку?", 0, 0, true, 11);

            var group = new GroupBox
            {
                Location = new Point(20, 40),
                Size = new Size(460, 240), // Уменьшено с 290 до 210
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            _rbMobileDevice = CreateRadioButton("📱 На телефоне\n(экономия места, для прогулок и поездок)", 10, 10, true);
            _rbPCPlayer = CreateRadioButton("💻 На компьютере / ноутбуке\n(универсальность, любое программное обеспечение)", 10, 55);
            _rbHomeAudio = CreateRadioButton("🏠 На домашней аудиосистеме\n(колонки, ресивер, максимальное качество)", 10, 100);
            _rbAppleDevice = CreateRadioButton("🍎 В экосистеме Apple\n(iPhone, iPad, Mac, AirPods)", 10, 145);
            _rbMaxCompatibility = CreateRadioButton("🚗 Старые устройства / автомагнитола\n(максимальная совместимость, MP3)", 10, 190);

            group.Controls.AddRange(new[] { _rbMobileDevice, _rbPCPlayer, _rbHomeAudio, _rbAppleDevice, _rbMaxCompatibility });
            _stepPanel.Controls.Add(lblQuestion);
            _stepPanel.Controls.Add(group);
        }

        private void ShowStep1_QualityPriority()
        {
            Label lblQuestion = CreateStepLabel("Что для вас важнее?", 0, 0, true, 11);

            var group = new GroupBox
            {
                Location = new Point(20, 40),
                Size = new Size(460, 240), // Уменьшено с 180 до 130
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            _rbBestQuality = CreateRadioButton("🎵 Максимальное качество звука\n(размер файла не имеет значения)", 10, 10);
            _rbGoodBalance = CreateRadioButton("⚖️ Сбалансированный подход\n(хорошее качество и приемлемый размер)", 10, 55, true);
            _rbSmallSize = CreateRadioButton("💾 Минимальный размер файлов\n(экономия места на диске)", 10, 100);

            group.Controls.AddRange(new[] { _rbBestQuality, _rbGoodBalance, _rbSmallSize });
            _stepPanel.Controls.Add(lblQuestion);
            _stepPanel.Controls.Add(group);
        }

        private void ShowStep2_SpecialNeeds()
        {
            Label lblQuestion = CreateStepLabel("Есть ли особые требования?", 0, 0, true, 11);

            var group = new GroupBox
            {
                Location = new Point(20, 40),
                Size = new Size(460, 240), // Уменьшено с 180 до 130
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            _rbHighEfficiency = CreateRadioButton("⚡ Высокая эффективность сжатия\n(минимальный размер при нормальном качестве)", 10, 10);
            _rbMediumEfficiency = CreateRadioButton("⚡ Средняя эффективность сжатия\n(средний размер при хорошем качестве)", 10, 55);
            _rbNoSpecial = CreateRadioButton("✅ Нет особых требований\n(стандартное использование)", 10, 100, true);

            group.Controls.AddRange(new[] { _rbHighEfficiency, _rbMediumEfficiency, _rbNoSpecial });
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
                Size = new Size(460, 240),
                BackColor = Color.FromArgb(248, 249, 250)
            };

            Label lblFormat = CreateInfoLabel($"🎵 Формат: {recommendation.FormatDisplayName}", 15, 15, true, Color.FromArgb(76, 175, 80), 11);
            Label lblDescription = CreateInfoLabel(recommendation.Description, 15, 55, false, Color.Black, 9);

            Label lblNote = CreateInfoLabel(
                "─────────────────────────────\n" +
                "✓ Эти настройки будут использоваться для быстрой конвертации\n" +
                "✓ Вы сможете изменить их позже в главном окне программы",
                15, 180, false, Color.Gray, 8);

            infoPanel.Controls.AddRange([lblFormat, lblDescription, lblNote]);

            _stepPanel.Controls.Add(lblTitle);
            _stepPanel.Controls.Add(infoPanel);

            Result = recommendation;
        }

        private QuickStartResult GenerateRecommendation()
        {
            var context = new RecommendationContext(
                _usageType,
                _qualityPriority,
                _specialNeed
            );

            var strategy = _strategies.FirstOrDefault(s => s.CanApply(context));

            if (strategy == null)
                throw new InvalidOperationException("No applicable recommendation strategy found");

            return strategy.Apply(context);
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
                Size = new Size(440, text.Contains("\n") ? 40 : 25), // Уменьшено с 45/30 до 40/25
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

            _btnBack.Enabled = _currentStep != 0;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            // Если на последнем шаге - завершаем
            if (_currentStep == 3)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            // Проверяем и сохраняем выбор пользователя на текущем шаге
            if (!SaveCurrentStepSelection())
                return;

            if (_currentStep < 3)
            {
                _currentStep++;
                ShowStep(_currentStep);
            }

            _btnBack.Enabled = true;
        }

        /// <summary>
        /// Сохраняет выбор пользователя на текущем шаге
        /// </summary>
        /// <returns>true, если выбор сделан, иначе false</returns>
        private bool SaveCurrentStepSelection()
        {
            switch (_currentStep)
            {
                case 0:
                    if (_rbMobileDevice.Checked)
                        _usageType = StrategyType.MOBILE;
                    else if (_rbPCPlayer.Checked)
                        _usageType = StrategyType.DESKTOP;
                    else if (_rbHomeAudio.Checked)
                        _usageType = StrategyType.HOME;
                    else if (_rbAppleDevice.Checked)
                        _usageType = StrategyType.APPLE;
                    else if (_rbMaxCompatibility.Checked)
                        _usageType = StrategyType.MAX_COMPACT;
                    else
                    {
                        ShowWarning();
                        return false;
                    }
                    break;

                case 1:
                    if (_rbBestQuality.Checked)
                        _qualityPriority = StrategyQuality.BEST;
                    else if (_rbGoodBalance.Checked)
                        _qualityPriority = StrategyQuality.BALANCED;
                    else if (_rbSmallSize.Checked)
                        _qualityPriority = StrategyQuality.COMPACT;
                    else
                    {
                        ShowWarning();
                        return false;
                    }
                    break;

                case 2:
                    if (_rbHighEfficiency.Checked)
                        _specialNeed = StrategySpecial.HIGH_COMPRESS;
                    else if (_rbMediumEfficiency.Checked)
                        _specialNeed = StrategySpecial.MEDIUM_COMPRESS;
                    else if (_rbNoSpecial.Checked)
                        _specialNeed = StrategySpecial.NONE;
                    else
                    {
                        ShowWarning();
                        return false;
                    }
                    break;
            }

            return true;
        }

        private void ShowWarning()
        {
            MessageBox.Show(
                "Пожалуйста, выберите один из вариантов, чтобы продолжить.",
                "Выбор не сделан",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}