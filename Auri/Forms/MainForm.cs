using Auri.Audio;
using Auri.Audio.Encoder;
using Auri.Managers;
using Auri.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Auri
{
    public partial class MainForm : Form
    {
        private BassAudioService _bass;
        private ConverterManager _converter;
        private List<AudioFile> _audioFiles;

        public MainForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            LoadDefaults();
            _bass = new BassAudioService();
        }

        private void LoadDefaults()
        {
            // Установка значений по умолчанию
            txtOutputPath.Text = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                "Auri");

            cmbOutputFormat.SelectedIndex = 1; // OPUS
            cmbQuality.SelectedIndex = 2; // Высокое качество
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
                    _audioFiles = new List<AudioFile>();
                    _audioFiles.AddRange(ofd.FileNames.Select((item, index) => new AudioFile(item, index)));
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
                foreach (DataGridViewRow row in dataGridViewFiles.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dataGridViewFiles.Rows.Remove(row);
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
            if (dataGridViewFiles.Rows.Count == 0)
            {
                MessageBox.Show("Добавьте файлы для конвертации", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string format = GetSelectedFormat();
            int quality = cmbQuality.SelectedIndex;
            EncoderSettings encoderSettings = new EncoderSettings();
            switch (quality)
            {
                case 0:
                    // opus
                    encoderSettings.Bitrate = 64;
                    encoderSettings.CustomParams.Add("mode", "vbr");
                    encoderSettings.CustomParams.Add("complexity", "10");
                    encoderSettings.CustomParams.Add("framesize", "60");
                    break;
                case 1:
                    // opus
                    encoderSettings.Bitrate = 128;
                    encoderSettings.CustomParams.Add("mode", "vbr");
                    encoderSettings.CustomParams.Add("complexity", "10");
                    encoderSettings.CustomParams.Add("framesize", "40");
                    break;
                case 2:
                    // opus
                    encoderSettings.Bitrate = 192;
                    encoderSettings.CustomParams.Add("mode", "vbr");
                    encoderSettings.CustomParams.Add("complexity", "10");
                    encoderSettings.CustomParams.Add("framesize", "20");
                    break;
                case 3:
                    // opus
                    encoderSettings.Bitrate = 320;
                    encoderSettings.CustomParams.Add("mode", "vbr");
                    encoderSettings.CustomParams.Add("complexity", "10");
                    encoderSettings.CustomParams.Add("framesize", "20");
                    break;
                default:
                    // opus
                    encoderSettings.Bitrate = 128;
                    encoderSettings.CustomParams.Add("mode", "vbr");
                    encoderSettings.CustomParams.Add("complexity", "10");
                    encoderSettings.CustomParams.Add("framesize", "40");
                    break;
            }


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

        private void StartConversion(string format, EncoderSettings quality, string outputPath)
        {
            int totalFiles = dataGridViewFiles.Rows.Count;

            // Сбрасываем статусы
            foreach (DataGridViewRow row in dataGridViewFiles.Rows)
            {
                row.Cells[4].Value = "Ожидание";
                row.DefaultCellStyle.BackColor = Color.White;
            }

            progressBar.Maximum = totalFiles;
            progressBar.Value = 0;
            lblStatus.Text = $"Конвертация в {format}...";

            _converter = new ConverterManager(_bass, _audioFiles.ToArray(), outputPath, format, quality);
            _converter.OnProgress += (index, progress) =>
            {
                DataGridViewRow row = dataGridViewFiles.Rows[index];
                row.Cells[4].Value = $"{progress}%";
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
                DataGridViewRow row = dataGridViewFiles.Rows[index];
                row.Cells[4].Value = $"Готово";
            };
            _converter.Convert();
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
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

        private void UpdateStatus()
        {
            int count = dataGridViewFiles.Rows.Count;
            lblStatus.Text = count > 0
                ? $"Загружено файлов: {count}"
                : "Готов к работе";
        }
    }
}