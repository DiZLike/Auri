using Auri.Audio;
using Auri.Audio.Encoder;
using Auri.Forms;
using Auri.Managers;
using Auri.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auri
{
    public partial class MainForm : Form
    {
        private BassAudioService _bass;
        private ConverterManager _converter;
        private List<AudioFile> _audioFiles;
        private ConfigManager _config;

        public MainForm()
        {
            InitializeComponent();
            LoadDefaults();
            _bass = new BassAudioService();
            _config = new ConfigManager();
            _audioFiles = new List<AudioFile>();
            InitializeThreadCountComboBox();
        }
        private void SaveSettings()
        {
            _config.Settings.FormSettings.FormY = this.Location.Y;
            _config.Settings.FormSettings.FormX = this.Location.X;
            _config.Settings.FormSettings.WindowState = this.WindowState;

            _config.SaveSettings();
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
            for (int i = 0; i < threads; i++)
            {
                cmbThreadCount.Items.Add((i + 1).ToString());
            }
            cmbThreadCount.SelectedIndex = cmbThreadCount.Items.Count - 1;
        }

        private void LoadDefaults()
        {
            // Установка значений по умолчанию
            txtOutputPath.Text = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                "Auri");

            cmbOutputFormat.SelectedIndex = 1; // OPUS
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

                    int rowIndex = dataGridViewFiles.Rows.Add();
                    DataGridViewRow row = dataGridViewFiles.Rows[rowIndex];

                    row.Cells[0].Value = Path.GetFileNameWithoutExtension(filePath);
                    row.Cells[1].Value = Path.GetExtension(filePath).TrimStart('.').ToUpper();
                    row.Cells[2].Value = FormatFileSize(fileInfo.Length);
                    row.Cells[3].Value = _bass.GetDuration(filePath);
                    row.Cells[4].Value = "Ожидание";
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
            btnConvert.Text = "Остановить";
            btnConvert.BackColor = Color.Red;
            if (dataGridViewFiles.Rows.Count == 0)
            {
                MessageBox.Show("Добавьте файлы для конвертации", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string format = GetSelectedFormat();
            int preset = cmbQuality.SelectedIndex;

            EncoderSettings encoderSettings = new EncoderSettings();
            if (cmbQuality.Items[preset].ToString().ToLower() == "пользовательский")
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

        private void StartConversion(string format, EncoderSettings preset, string outputPath)
        {
            int threads = int.Parse(cmbThreadCount.Text);
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
                row.Cells[4].Value = "Ожидание";
                row.DefaultCellStyle.BackColor = Color.White;
            }

            progressBar.Value = 0;
            lblStatus.Text = $"Конвертация в {format}...";

            _converter = new ConverterManager(_bass, _audioFiles.ToArray(), outputPath, format, preset);
            _converter.OnProgress += (index, progress) =>
            {
                if (index < dataGridViewFiles.Rows.Count)
                {
                    DataGridViewRow row = dataGridViewFiles.Rows[index];
                    row.Cells[4].Value = $"{progress}%";
                }
            };
            _converter.OnOverallProgress += (overall) =>
            {
                progressBar.Invoke(new Action(() =>
                {
                    progressBar.Maximum = 100;
                    progressBar.Value = (int)Math.Round(overall);
                }));
            };
            _converter.OnComplete += (index, status) =>
            {
                if (index < dataGridViewFiles.Rows.Count)
                {
                    DataGridViewRow row = dataGridViewFiles.Rows[index];
                    row.Cells[4].Value = $"Готово";
                }
            };
            _converter.OnAllComplete += (status) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    btnConvert.Text = "Конвертировать";
                    btnConvert.BackColor = Color.FromArgb(76, 175, 80);
                    Task.Delay(500).ContinueWith(_ =>
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            progressBar.Value = 0;
                            lblStatus.Text = "Конвертация завершена";
                        }));
                    });
                }));
            };
            _converter.Convert(threads);
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
            if (cmbQuality.Items[cmbQuality.SelectedIndex].ToString().ToLower().Contains("пользовательский"))
                btnUserPreset.Enabled = true;
            else btnUserPreset.Enabled = false;
        }

        private void cmbOutputFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbQuality.Items.Clear();
            string format = GetSelectedFormat();
            string[] presets = LoadPresets(format);
            cmbQuality.Items.AddRange(presets);
            int index = cmbQuality.FindString("высокое");
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
            EncoderSettings settings = _config.GetUserEncoderPreset(format);
            var userPresetForm = new UserPresetForm(format, settings);
            userPresetForm.EncoderSettingsChanged += (preset) =>
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
    }
}