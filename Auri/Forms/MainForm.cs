using Auri.Forms;
using Auri.Forms.Dialogs;
using Auri.Wizard;
using Engine.Audio;
using Engine.Audio.Encoder;
using Engine.Managers;
using Engine.Services;
using Music.Parser;
using System.Diagnostics;
using System.Reflection;

namespace Auri
{
    public partial class MainForm : Form
    {
        private AudioEngineService _audioEngine;
        private AudioManager _converter;
        private List<AudioFile> _audioFiles;
        private ConfigManager _config;
        private MetaService _metaService;

        private int _lastQualityIndex;
        private bool _aborted;
        private bool _isReset;
        public MainForm()
        {
            InitializeComponent();
            SetTitle();

            ExceptionManager.OnDetailedError += ExceptionManager_OnDetailedError;
            _audioEngine = new AudioEngineService();
            _config = new ConfigManager();
            _audioFiles = new List<AudioFile>();
            _metaService = new MetaService();

            Test();

            InitializeThreadCountComboBox();
            LoadSettings();

            // Запуск мастера быстрого старта при первом запуске
            RunQuickStartWizardIfNeeded();
        }
        private void SetTitle()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string versionString = version != null
                ? $"{version.Major}.{version.Minor}.{version.Build}"
                : "1.0.0";
            this.Text = $"Auri {versionString}";
        }

        private void ExceptionManager_OnDetailedError(Error error, string message)
        {
            if (error == Error.FILE_ALREADY_EXISTS || error == Error.OPERATION_ABORTED)
                return;
            UserDialogs.ShowError("Ошибка", message);
        }
        private void SaveSettings()
        {
            if (_isReset)
                return;
            // форма
            _config.Settings.FormSettings.FormY = this.Location.Y;
            _config.Settings.FormSettings.FormX = this.Location.X;
            _config.Settings.FormSettings.FormWidth = this.Width;
            _config.Settings.FormSettings.FormHeight = this.Height;
            _config.Settings.FormSettings.WindowState = (int)this.WindowState;

            // конвертер
            _config.Settings.ConverterSettings.OutputFormatIndex = cmbOutputFormat.SelectedIndex;
            _config.Settings.ConverterSettings.QualityIndex = cmbQuality.SelectedIndex;
            _config.Settings.ConverterSettings.ThreadsCountIndex = tbThreadCount.Value;
            _config.Settings.ConverterSettings.OutputPath = txtOutputPath.Text;
            _config.Settings.ConverterSettings.PathPattern = txtPattern.Text;
            _config.Settings.ConverterSettings.RewriteAudio = cbRewriteFiles.Checked;
            _config.Settings.ConverterSettings.SaveTrackList = cbSaveTracks.Checked;

            // Всегда очищаем список перед обновлением
            _config.Settings.ConverterSettings.TrackList.Clear();

            // Сохраняем треки только если включено и есть данные
            if (cbSaveTracks.Checked && dataGridViewFiles.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewFiles.Rows)
                {
                    if (row.Tag is string filePath && File.Exists(filePath))
                    {
                        _config.Settings.ConverterSettings.TrackList.Add(filePath);
                    }
                }
            }

            _config.SaveSettings();
        }
        private void LoadSettings()
        {
            try
            {
                if (_config.NoConfigured)
                {
                    SetDefaultSettings();
                    return;
                }

                // форма с проверкой видимости
                var screen = Screen.FromPoint(new Point(_config.Settings.FormSettings.FormX,
                                                         _config.Settings.FormSettings.FormY));
                if (screen.WorkingArea.Contains(_config.Settings.FormSettings.FormX,
                                                _config.Settings.FormSettings.FormY))
                {
                    this.Location = new Point(_config.Settings.FormSettings.FormX,
                                             _config.Settings.FormSettings.FormY);
                }

                this.Size = new Size(_config.Settings.FormSettings.FormWidth,
                                    _config.Settings.FormSettings.FormHeight);

                // Сначала WindowState, потом остальное
                this.WindowState = (FormWindowState)_config.Settings.FormSettings.WindowState;

                // конвертер с валидацией
                if (_config.Settings.ConverterSettings.OutputFormatIndex >= 0 &&
                    _config.Settings.ConverterSettings.OutputFormatIndex < cmbOutputFormat.Items.Count)
                {
                    cmbOutputFormat.SelectedIndex = _config.Settings.ConverterSettings.OutputFormatIndex;
                }

                if (_config.Settings.ConverterSettings.QualityIndex >= 0 &&
                    _config.Settings.ConverterSettings.QualityIndex < cmbQuality.Items.Count)
                {
                    cmbQuality.SelectedIndex = _config.Settings.ConverterSettings.QualityIndex;
                }

                // Валидация количества потоков
                int threadsCount = _config.Settings.ConverterSettings.ThreadsCountIndex;
                if (threadsCount < tbThreadCount.Minimum)
                    tbThreadCount.Value = tbThreadCount.Minimum;
                else if (threadsCount > tbThreadCount.Maximum)
                    tbThreadCount.Value = tbThreadCount.Maximum;
                else
                    tbThreadCount.Value = threadsCount;

                txtOutputPath.Text = string.IsNullOrEmpty(_config.Settings.ConverterSettings.OutputPath)
                    ? GetDefaultOutputPath()
                    : _config.Settings.ConverterSettings.OutputPath;

                txtPattern.Text = _config.Settings.ConverterSettings.PathPattern;
                cbRewriteFiles.Checked = _config.Settings.ConverterSettings.RewriteAudio;
                cbSaveTracks.Checked = _config.Settings.ConverterSettings.SaveTrackList;

                // таблица треков
                if (cbSaveTracks.Checked && _config.Settings.ConverterSettings.TrackList?.Any() == true)
                {
                    var existingFiles = _config.Settings.ConverterSettings.TrackList
                        .Where(File.Exists)
                        .ToArray();

                    if (existingFiles.Any())
                    {
                        AddFilesToGrid(existingFiles);
                    }
                }
            }
            catch (Exception)
            {
                // Применение значений по умолчанию
                SetDefaultSettings();
            }
        }

        private string GetDefaultOutputPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "Auri");
        }

        private void SetDefaultSettings()
        {
            cmbOutputFormat.SelectedIndex = 0;
            int index = cmbQuality.FindString("высокое");
            if (index == -1)
                index = cmbQuality.FindString("максимальное");
            cmbQuality.SelectedIndex = index;
            tbThreadCount.Value = tbThreadCount.Maximum / 2;
            txtOutputPath.Text = GetDefaultOutputPath();
            txtPattern.Text = "[filename]";
            cbRewriteFiles.Checked = false;
            cbSaveTracks.Checked = false;
        }
        private string[] LoadPresets(string format)
        {
            string presetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encoder_presets");
            string presetFile = Path.Combine(presetPath, $"{format.ToLower()}_presets.txt");
            if (!File.Exists(presetFile))
                return new string[] { String.Empty };
            return File.ReadAllLines(presetFile);
        }
        private void InitializeThreadCountComboBox()
        {
            int threads = Environment.ProcessorCount;
            tbThreadCount.Maximum = threads;
            tbThreadCount.Value = tbThreadCount.Maximum;
        }
        private void BtnAddFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter =
                    "Все поддерживаемые форматы|*.wav;*.wave;*.mp3;*.opus;*.aac;*.m4a;*.mp4;*.ac3;*.mpc;*.spx;*.tta;*.ape;*.flac;*.wma;*.wv;*.webm;*.alac|" +
                    "WAV (без сжатия)|*.wav;*.wave|" +
                    "MP3|*.mp3|" +
                    "Opus|*.opus|" +
                    "AAC / M4A|*.aac;*.m4a|" +
                    "MP4|*.mp4|" +
                    "AC3|*.ac3|" +
                    "Musepack|*.mpc|" +
                    "Speex|*.spx|" +
                    "True Audio|*.tta|" +
                    "Monkey's Audio|*.ape|" +
                    "FLAC|*.flac|" +
                    "WMA|*.wma|" +
                    "WavPack|*.wv|" +
                    "WebM|*.webm|" +
                    "ALAC|*.alac|" +
                    "Все файлы|*.*";
                ofd.Title = "Выберите аудиофайлы для конвертации";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    AddFilesToGrid(ofd.FileNames);
                    UpdateStatus();
                }
            }
        }
        private void AddFilesToGrid(string[] filePaths)
        {
            // Отключаем обновление DataGridView для ускорения
            dataGridViewFiles.SuspendLayout();

            try
            {
                int addedCount = 0;

                foreach (string filePath in filePaths)
                {
                    // Проверяем, не добавлен ли уже этот файл
                    bool fileExists = false;
                    foreach (DataGridViewRow row1 in dataGridViewFiles.Rows)
                    {
                        if (row1.Tag?.ToString() == filePath)
                        {
                            fileExists = true;
                            break;
                        }
                    }

                    if (fileExists)
                        continue;

                    FileInfo fileInfo = new FileInfo(filePath);
                    var info = _metaService.GetTrackInfo(filePath);
                    var chans = info.Channels > 1 ? "Stereo" : "Mono";

                    int rowIndex = dataGridViewFiles.Rows.Add();
                    DataGridViewRow row = dataGridViewFiles.Rows[rowIndex];

                    row.Cells[0].Value = info.Name;
                    row.Cells[1].Value = $"{info.Frequency} kHz, {info.Bitrate} kbps, {chans}, {info.Duration} ({info.Codec})";
                    row.Cells[2].Value = FormatFileSize(fileInfo.Length);
                    row.Cells[3].Value = "Ожидание";
                    row.Tag = filePath; // Сохраняем полный путь

                    // Подсветка новой строки
                    row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);

                    addedCount++;
                }
            }
            finally
            {
                // Возобновляем обновление DataGridView
                dataGridViewFiles.ResumeLayout();
            }
        }
        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
        private void BtnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (dataGridViewFiles.SelectedRows.Count > 0)
            {
                // Собираем пути файлов для удаления
                List<string> pathsToRemove = new List<string>();

                foreach (DataGridViewRow row in dataGridViewFiles.SelectedRows)
                {
                    if (!row.IsNewRow && row.Tag != null)
                    {
                        pathsToRemove.Add(row.Tag.ToString());
                    }
                }

                // Удаляем из DataGridView
                foreach (DataGridViewRow row in dataGridViewFiles.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dataGridViewFiles.Rows.Remove(row);
                    }
                }

                UpdateStatus();
            }
            else
            {
                MessageBox.Show("Выберите файлы для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnClearAll_Click(object sender, EventArgs e)
        {
            if (dataGridViewFiles.Rows.Count > 0)
            {
                dataGridViewFiles.Rows.Clear();
                _audioFiles.Clear();
                UpdateStatus();
            }
        }
        private string GetSelectedFormat()
        {
            string selected = cmbOutputFormat.SelectedItem?.ToString() ?? "";
            return selected.Split(' ')[0]; // Берем только первую часть (MP3, WAV и т.д.)
        }
        private void BtnConvert_Click(object sender, EventArgs e)
        {
            var ttt = dataGridViewFiles;


            if (btnConvert.Tag.ToString() == "convert")
            {
                if (dataGridViewFiles.Rows.Count == 0)
                {
                    MessageBox.Show("Добавьте файлы для конвертации", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _aborted = false;
                btnConvert.Text = "Остановить";
                btnConvert.Tag = "abort";
                btnQuickConvert.Enabled = false;
                btnConvert.BackColor = Color.Red;

                string format = GetSelectedFormat();
                int preset = cmbQuality.SelectedIndex;

                EncoderPreset encoderSettings = new EncoderPreset();
                if (cmbQuality.Items[preset].ToString().ToLower() == "пользовательское")
                    encoderSettings = _config.GetUserEncoderPreset(format);
                else
                    encoderSettings = _config.GetEncoderPreset(format, preset);

                string outputPath = txtOutputPath.Text;

                // Проверка папки вывода
                if (!Directory.Exists(outputPath))
                {
                    try
                    {
                        Directory.CreateDirectory(outputPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось создать папку вывода: {ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                StartConversion(format, encoderSettings, outputPath);
            }
            else
            {
                btnQuickConvert.Enabled = true;
                _aborted = true;
                _converter.AbortAll();
            }
        }
        private void StartConversion(string format, EncoderPreset preset, string outputPath)
        {
            int threads = tbThreadCount.Value;
            int totalFiles = dataGridViewFiles.Rows.Count;
            _audioFiles.Clear();
            foreach (DataGridViewRow row in dataGridViewFiles.Rows)
            {
                if (row.Tag != null)
                {
                    string filePath = row.Tag.ToString();
                    _audioFiles.Add(new AudioFile(filePath, _audioFiles.Count));
                }
            }

            // Сбрасываем статусы
            foreach (DataGridViewRow row in dataGridViewFiles.Rows)
            {
                row.Cells[3].Value = "Ожидание";
                row.DefaultCellStyle.BackColor = Color.White;
            }

            progressBar.Value = 0;
            lblStatus.Text = $"Конвертация в {format}...";

            _converter = new AudioManager(_config, _audioEngine, _audioFiles.ToArray(), outputPath, txtPattern.Text, format, preset);
            _converter.OnProgress += (index, progress) =>
            {
                if (_aborted)
                    return;
                this.BeginInvoke(new Action(() =>
                {
                    if (index < dataGridViewFiles.Rows.Count)
                    {
                        DataGridViewRow row = dataGridViewFiles.Rows[index];
                        row.Cells[3].Value = $"{progress}%";
                    }
                }));
            };
            _converter.OnOverallProgress += (overall) =>
            {
                if (_aborted)
                    return;
                progressBar.Invoke(new Action(() =>
                {
                    progressBar.Maximum = 100;
                    progressBar.Value = (int)Math.Round(overall);
                }));
            };
            _converter.OnComplete += (index, status) =>
            {
                if (_aborted)
                    return;
                this.BeginInvoke(new Action(() =>
                {
                    if (index < dataGridViewFiles.Rows.Count)
                    {
                        DataGridViewRow row = dataGridViewFiles.Rows[index];
                        row.Cells[3].Value = $"Готово";
                    }
                }));
            };
            _converter.OnAllComplete += (status) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    btnConvert.Text = "Конвертировать";
                    btnConvert.Tag = "convert";
                    btnQuickConvert.Enabled = true;
                    btnConvert.BackColor = Color.FromArgb(76, 175, 80);

                    // Используем Timer вместо Task.Delay + BeginInvoke
                    var timer = new System.Windows.Forms.Timer();
                    timer.Interval = 500;
                    timer.Tick += (s, e) =>
                    {
                        timer.Stop();
                        if (!_aborted) // Проверяем, не была ли операция прервана
                        {
                            progressBar.Value = 0;
                            lblStatus.Text = "Конвертация завершена";
                        }
                        timer.Dispose();
                    };
                    timer.Start();
                }));
            };
            _converter.OnAbort += () =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    btnConvert.Text = "Конвертировать";
                    btnConvert.Tag = "convert";
                    btnQuickConvert.Enabled = true;
                    btnConvert.BackColor = Color.FromArgb(76, 175, 80);
                    progressBar.Value = 0;
                    lblStatus.Text = "Конвертация отменена";
                    for (int i = 0; i < dataGridViewFiles.Rows.Count; i++)
                    {
                        DataGridViewRow row = dataGridViewFiles.Rows[i];
                        row.Cells[3].Value = $"Отменено";
                    }
                }));
            };
            _converter.OnFileExists += (fileIndex) =>
            {

                this.BeginInvoke(new Action(() =>
                {
                    if (fileIndex < dataGridViewFiles.Rows.Count)
                    {
                        DataGridViewRow row = dataGridViewFiles.Rows[fileIndex];
                        row.Cells[3].Value = $"Файл существует";
                    }

                    bool allCompleted = _audioFiles.All(audioFile => audioFile.Completed == true);
                    if (allCompleted)
                    {
                        btnConvert.Text = "Конвертировать";
                        btnConvert.Tag = "convert";
                        btnQuickConvert.Enabled = true;
                        btnConvert.BackColor = Color.FromArgb(76, 175, 80);
                        progressBar.Value = 0;
                        lblStatus.Text = "Конвертация завершена";
                    }

                }));
            };

            _converter.Convert(threads, _metaService);
        }
        private void UpdateStatus()
        {
            int count = dataGridViewFiles.Rows.Count;
            lblStatus.Text = count > 0
                ? $"Загружено файлов: {count}"
                : "Готов к работе";
        }
        private void cmbQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbQuality.SelectedIndex == -1)
                cmbQuality.SelectedIndex = 0;
            if (cmbQuality.Items[cmbQuality.SelectedIndex].ToString().ToLower().Contains("пользовательское"))
            {
                if (!_config.Settings.ConverterSettings.AdvancedMode)
                {
                    DialogResult dialogResult = UserDialogs.ShowDialog("Предупреждение", "Данный режим предназначен для опытных пользователей.\n" +
                        "Включить режим эксперта?");
                    if (dialogResult == DialogResult.Yes)
                        _config.Settings.ConverterSettings.AdvancedMode = true;
                    else
                    {
                        cmbQuality.SelectedIndex = _lastQualityIndex;
                        return;
                    }
                }
                btnUserPreset.Enabled = true;
            }
            else btnUserPreset.Enabled = false;
            _lastQualityIndex = cmbQuality.SelectedIndex;
        }
        private void cmbOutputFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbQuality.Items.Clear();
            string format = GetSelectedFormat();
            string[] presets = LoadPresets(format);
            cmbQuality.Items.AddRange(presets);
            int index = cmbQuality.FindString("высокое");
            if (index == -1)
                index = cmbQuality.FindString("максимальное");
            cmbQuality.SelectedIndex = index;
        }
        private void txtOutputPath_MouseClick(object sender, MouseEventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Выберите папку для сохранения конвертированных файлов";
                fbd.SelectedPath = txtOutputPath.Text;
                fbd.ShowNewFolderButton = true;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = fbd.SelectedPath;
                }
            }
        }
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", txtOutputPath.Text);
        }
        private void btnUserPreset_Click(object sender, EventArgs e)
        {
            string format = GetSelectedFormat();
            //EncoderSettings settings = _config.GetUserEncoderPreset(format);
            var userPresetForm = new UserPresetForm(format, _config);
            userPresetForm.EncoderPresetChanged += (preset) =>
            {
                _config.SaveUserEncoderPreset(format, preset);
                lblStatus.Text = "Настройки сохранены";
            };
            userPresetForm.ShowDialog();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            _audioEngine?.EngineFree();
        }
        private void btnPattern_Click(object sender, EventArgs e)
        {
            var patternForm = new PatternForm();
            patternForm.OnAddPattern += (tag) =>
            {
                txtPattern.Text += tag;
            };
            patternForm.Show();
        }
        private void btnQuickConvert_Click(object sender, EventArgs e)
        {
            if (dataGridViewFiles.Rows.Count == 0)
            {
                MessageBox.Show("Добавьте файлы для конвертации", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_config.Settings.ConverterSettings.QuickStartPreset == null)
            {
                MessageBox.Show("Для быстрой конвертации необходимо прости мастер настройки", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string format = _config.Settings.ConverterSettings.QuickStartFormat;
            EncoderPreset encoderPreset = _config.Settings.ConverterSettings.QuickStartPreset;

            btnConvert.Tag = "abort";
            btnQuickConvert.Enabled = false;
            _aborted = false;
            btnConvert.Text = "Остановить";
            btnConvert.BackColor = Color.Red;

            string outputPath = txtOutputPath.Text;

            // Проверка папки вывода
            if (!Directory.Exists(outputPath))
            {
                try
                {
                    Directory.CreateDirectory(outputPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось создать папку вывода: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            StartConversion(format, encoderPreset, outputPath);
        }
        private void cbRewriteFiles_CheckedChanged(object sender, EventArgs e)
        {
            _config.Settings.ConverterSettings.RewriteAudio = cbRewriteFiles.Checked;
        }
        private void dataGridViewFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.All(file => IsAllowedFile(file)))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
                e.Effect = DragDropEffects.None;
        }
        private void dataGridViewFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                AddFilesToGrid(files);
                UpdateStatus();
            }
        }
        private bool IsAllowedFile(string filePath)
        {
            string[] allowedExtensions = {
                ".wav", ".wave", ".mp3", ".opus", ".aac", ".m4a", ".mp4",
                ".ac3", ".mpc", ".spx", ".tta",
                ".ape", ".flac", ".wma", ".wv", ".webm", ".alac"
};
            string extension = Path.GetExtension(filePath).ToLower();

            return allowedExtensions.Contains(extension);
        }
        private void RunQuickStartWizardIfNeeded()
        {
            var wizard = new QuickStartWizard(_config);
            if (wizard.NeedsSetup())
            {
                var result = wizard.ShowWizard();
                if (result != null)
                {
                    // Сохраняем пресет для быстрой конвертации
                    _config.SaveQuickStartPreset(result.Format, result.Preset);

                    lblStatus.Text = $"Мастер завершен. Рекомендовано: {result.FormatDisplayName}";
                }
            }
            if (_config.Settings.ConverterSettings.QuickStartPreset != null)
            {
                string presetInfo = _config.Settings.ConverterSettings.QuickStartPreset.ToString();
                toolTip1.SetToolTip(btnQuickConvert,
                    $"Мгновенно конвертировать файлы в формат выбранный мастером быстрого старта.\n" +
                    $"Удобно для быстрой обработки без лишних настроек\n\n" +
                    $"Текущий пресет:\n" +
                    $"{presetInfo}");
            }
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void мастерНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _config.Settings.ConverterSettings.QuickStartCompleted = false;
            RunQuickStartWizardIfNeeded();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog(this);
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "help", "index.html"),
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть справку: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void проверитьОбновлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/DiZLike/Auri/releases",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть ссылку: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void сбросНастроекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = UserDialogs.ShowDialog("Предупреждение", "Сбросить настройки до стандартных значений?");
            if (result == DialogResult.No) return;

            _isReset = _config.ResetSettings();
            if (_isReset)
            {
                MessageBox.Show("Настройки сброшены! Необходимо перезапустить программу", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            else
                MessageBox.Show("Настройки отсутствуют или файлы заняты программой!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }







        private void Test()
        {
            //var cueFile = @"H:\2001 - Дальнобойщики-2\Ария - Дальнобойщики-2.cue";
            //var cue = CueService.Parse(cueFile);
            //PlayTrack(cue, 10);
            var search = "amaranthe";
            var parser = new HitmoParser();
            //var result = parser.SearchTracksWithLinkCheck(search);
            //var link = parser.GetDirectStreamUrl(result.Tracks[1].DownloadUrl);
            _audioEngine.Play("");
            //parser.DownloadTrack(link, Path.GetFileName(link));

        }

        //public void PlayTrack(CueFile cueFile, int trackNumber)
        //{
        //    // Останавливаем текущее воспроизведение
        //    StopPlayback();

        //    // Определяем путь к аудиофайлу
        //    string filePath = cueFile.FilePath;

        //    // Находим нужный трек
        //    var track = cueFile.Tracks.FirstOrDefault(t => t.Number == trackNumber);

        //    // Создаем поток для аудиофайла
        //    int streamHandle = Bass.BASS_StreamCreateFile(filePath, 0, 0, BASSFlag.BASS_DEFAULT);

        //    // Устанавливаем позицию на начало трека
        //    double startSeconds = track.StartTime.TotalSeconds;
        //    long startBytes = Bass.BASS_ChannelSeconds2Bytes(streamHandle, startSeconds);
        //    bool ok = Bass.BASS_ChannelSetPosition(streamHandle, startBytes);

        //    // Определяем конец трека
        //    double endSeconds = track.EndTime.TotalSeconds;
        //    long endBytes = Bass.BASS_ChannelSeconds2Bytes(streamHandle, endSeconds);

        //    // Устанавливаем синхронизацию для остановки в конце трека
        //    int isOk = Bass.BASS_ChannelSetSync(streamHandle, BASSSync.BASS_SYNC_POS, endBytes,
        //        (handle, channel, data, user) => StopPlayback(), IntPtr.Zero);

        //    // Запускаем воспроизведение
        //    bool play = Bass.BASS_ChannelPlay(streamHandle, false);
        //    _currentStream = streamHandle;
        //}


        //public void StopPlayback()
        //{
        //    if (_currentStream != 0)
        //    {
        //        Bass.BASS_ChannelStop(_currentStream);
        //        Bass.BASS_StreamFree(_currentStream);
        //        _currentStream = 0;
        //    }
        //}
    }
}