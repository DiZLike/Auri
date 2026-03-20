using Auri.Audio;
using Auri.Audio.Encoder;
using Auri.Forms;
using Auri.Forms.Dialogs;
using Auri.Managers;
using Auri.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auri
{
    public partial class MainForm : Form
    {
        private AudioEngineService _bass;
        private AudioManager _converter;
        private List<AudioFile> _audioFiles;
        private ConfigManager _config;
        private MetaService _metaService;

        private int _lastQualityIndex;
        private bool _aborted;

        public MainForm()
        {
            InitializeComponent();
            ExceptionManager.OnDetailedError += ExceptionManager_OnDetailedError;
            _bass = new AudioEngineService();
            _config = new ConfigManager();
            _audioFiles = new List<AudioFile>();
            _metaService = new MetaService();
            InitializeThreadCountComboBox();
            LoadSettings();
        }

        private void ExceptionManager_OnDetailedError(Error error, string message)
        {
            if (error == Error.FILE_ALREADY_EXISTS || error == Error.OPERATION_ABORTED)
                return;
            UserDialogs.ShowError("Ошибка", message);
        }

        private void SaveSettings()
        {
            // форма
            _config.Settings.FormSettings.FormY = this.Location.Y;
            _config.Settings.FormSettings.FormX = this.Location.X;
            _config.Settings.FormSettings.FormWidth = this.Width;
            _config.Settings.FormSettings.FormHeight = this.Height;
            _config.Settings.FormSettings.WindowState = this.WindowState;

            // конвертер
            _config.Settings.ConverterSettings.OutputFormatIndex = cmbOutputFormat.SelectedIndex;
            _config.Settings.ConverterSettings.QualityIndex = cmbQuality.SelectedIndex;
            _config.Settings.ConverterSettings.ThreadsCountIndex = tbThreadCount.Value;
            _config.Settings.ConverterSettings.OutputPath = txtOutputPath.Text;
            _config.Settings.ConverterSettings.PathPattern = txtPattern.Text;

            // таблица треков
            _config.Settings.ConverterSettings.SaveTrackList = cbSaveTracks.Checked;
            if (dataGridViewFiles.Rows.Count == 0 )
                _config.Settings.ConverterSettings.TrackList.Clear();

            if (cbSaveTracks.Checked)
            {
                _config.Settings.ConverterSettings.TrackList.Clear();
                foreach (DataGridViewRow row in dataGridViewFiles.Rows)
                {
                    if (row.Tag != null)
                    {
                        string filePath = row.Tag.ToString();
                        _config.Settings.ConverterSettings.TrackList.Add(filePath);
                    }
                }
            }

            // перезапись аудио
            _config.Settings.ConverterSettings.RewriteAudio = cbRewriteFiles.Checked;

            _config.SaveSettings();
        }
        private void LoadSettings()
        {
            // форма
            this.Location = new Point(_config.Settings.FormSettings.FormX, _config.Settings.FormSettings.FormY);
            this.Size = new Size(_config.Settings.FormSettings.FormWidth, _config.Settings.FormSettings.FormHeight);
            this.WindowState = _config.Settings.FormSettings.WindowState;

            // конвертер
            cmbOutputFormat.SelectedIndex = _config.Settings.ConverterSettings.OutputFormatIndex;
            cmbQuality.SelectedIndex = _config.Settings.ConverterSettings.QualityIndex;

            if (_config.Settings.ConverterSettings.ThreadsCountIndex < tbThreadCount.Maximum)
                tbThreadCount.Value = _config.Settings.ConverterSettings.ThreadsCountIndex;
            else
                tbThreadCount.Value = tbThreadCount.Maximum;

            txtOutputPath.Text = _config.Settings.ConverterSettings.OutputPath;
            if (txtOutputPath.Text == String.Empty)
                txtOutputPath.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),"Auri");
            txtPattern.Text = _config.Settings.ConverterSettings.PathPattern;

            // таблица треков
            cbSaveTracks.Checked = _config.Settings.ConverterSettings.SaveTrackList;
            if (cbSaveTracks.Checked)
                AddFilesToGrid(_config.Settings.ConverterSettings.TrackList.ToArray());

            // перезапись аудио
            cbRewriteFiles.Checked = _config.Settings.ConverterSettings.RewriteAudio;
        }
        private string[] LoadPresets(string format)
        {
            string presetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encoder_presets");
            string presetFile = Path.Combine(presetPath, $"{format.ToLower()}_presets.txt");
            if (!File.Exists(presetFile))
                return new string[] {String.Empty};
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
                ofd.Filter = "Аудио файлы|*.mp3;*.opus;*.wav;*.flac;*.aac;*.ogg;*.m4a;*.wma;*.aiff|Все файлы|*.*";
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
            if (btnConvert.Tag.ToString() == "convert")
            {
                _aborted = false;
                btnConvert.Text = "Остановить";
                btnConvert.Tag = "abort";
                btnQuickConvert.Enabled = false;
                btnConvert.BackColor = Color.Red;
                if (dataGridViewFiles.Rows.Count == 0)
                {
                    MessageBox.Show("Добавьте файлы для конвертации", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

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

            _converter = new AudioManager(_config, _bass, _audioFiles.ToArray(), outputPath, txtPattern.Text, format, preset);
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
            btnConvert.Tag = "abort";
            btnQuickConvert.Enabled = false;
            _aborted = false;
            btnConvert.Text = "Остановить";
            btnConvert.BackColor = Color.Red;
            if (dataGridViewFiles.Rows.Count == 0)
            {
                MessageBox.Show("Добавьте файлы для конвертации", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string format = "mp3";
            EncoderPreset encoderSettings = new EncoderPreset();
            encoderSettings.SampleRate = 44100;
            encoderSettings.Bitrate = 192;
            encoderSettings.Channels = 2;
            encoderSettings.CustomParams["Mode"] = "cbr";
            encoderSettings.CustomParams["ChannelMode"] = "j";
            encoderSettings.CustomParams["Quality"] = 0;


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

        private void cbRewriteFiles_CheckedChanged(object sender, EventArgs e)
        {
            _config.Settings.ConverterSettings.RewriteAudio = cbRewriteFiles.Checked;
        }
    }
}