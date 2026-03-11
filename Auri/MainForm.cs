using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Auri
{
    public partial class MainForm : Form
    {
        //private DataGridViewTextBoxColumn colFileName;
        //private DataGridViewTextBoxColumn colFormat;
        //private DataGridViewTextBoxColumn colSize;
        //private DataGridViewTextBoxColumn colDuration;
        //private DataGridViewTextBoxColumn colStatus;

        public MainForm()
        {
            InitializeComponent();
            //SetupDataGridView();
            LoadDefaults();
            StyleButtons();
        }

        private void SetupDataGridView()
        {
            // Создаем колонки
            colFileName = new DataGridViewTextBoxColumn();
            colFileName.HeaderText = "Имя файла";
            colFileName.Name = "FileName";
            colFileName.ReadOnly = true;
            colFileName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colFileName.FillWeight = 50;

            colFormat = new DataGridViewTextBoxColumn();
            colFormat.HeaderText = "Формат";
            colFormat.Name = "Format";
            colFormat.ReadOnly = true;
            colFormat.Width = 80;

            colSize = new DataGridViewTextBoxColumn();
            colSize.HeaderText = "Размер";
            colSize.Name = "Size";
            colSize.ReadOnly = true;
            colSize.Width = 80;

            colDuration = new DataGridViewTextBoxColumn();
            colDuration.HeaderText = "Длительность";
            colDuration.Name = "Duration";
            colDuration.ReadOnly = true;
            colDuration.Width = 90;

            colStatus = new DataGridViewTextBoxColumn();
            colStatus.HeaderText = "Статус";
            colStatus.Name = "Status";
            colStatus.ReadOnly = true;
            colStatus.Width = 100;

            dataGridViewFiles.Columns.AddRange(new DataGridViewColumn[] {
                colFileName, colFormat, colSize, colDuration, colStatus
            });
        }

        private void LoadDefaults()
        {
            // Установка значений по умолчанию
            txtOutputPath.Text = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                "Auri Converter");

            cmbOutputFormat.SelectedIndex = 0; // MP3
            cmbQuality.SelectedIndex = 2; // Высокое качество

            // Создаем папку вывода, если её нет
            if (!Directory.Exists(txtOutputPath.Text))
            {
                Directory.CreateDirectory(txtOutputPath.Text);
            }
        }

        private void StyleButtons()
        {
            // Стили для кнопок
            btnConvert.FlatAppearance.BorderSize = 0;
            btnAddFiles.FlatAppearance.BorderSize = 1;
            btnRemoveSelected.FlatAppearance.BorderSize = 1;
            btnClearAll.FlatAppearance.BorderSize = 1;
            btnBrowse.FlatAppearance.BorderSize = 1;
        }

        private void BtnAddFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "Аудио файлы|*.mp3;*.wav;*.flac;*.aac;*.ogg;*.m4a;*.wma;*.aiff|Все файлы|*.*";
                ofd.Title = "Выберите аудиофайлы для конвертации";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in ofd.FileNames)
                    {
                        AddFileToGrid(file);
                    }
                    UpdateStatus();
                }
            }
        }

        private void AddFileToGrid(string filePath)
        {
            // Проверяем, не добавлен ли уже этот файл
            foreach (DataGridViewRow row1 in dataGridViewFiles.Rows)
            {
                if (row1.Tag?.ToString() == filePath)
                    return;
            }

            FileInfo fileInfo = new FileInfo(filePath);

            int rowIndex = dataGridViewFiles.Rows.Add();
            DataGridViewRow row = dataGridViewFiles.Rows[rowIndex];

            row.Cells["FileName"].Value = Path.GetFileName(filePath);
            row.Cells["Format"].Value = Path.GetExtension(filePath).TrimStart('.').ToUpper();
            row.Cells["Size"].Value = FormatFileSize(fileInfo.Length);
            row.Cells["Duration"].Value = "--:--";
            row.Cells["Status"].Value = "Ожидание";
            row.Tag = filePath; // Сохраняем полный путь

            // Подсветка новой строки
            row.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);
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
                var result = MessageBox.Show(
                    $"Удалить выбранные файлы ({dataGridViewFiles.SelectedRows.Count}) из списка?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridViewFiles.SelectedRows)
                    {
                        if (!row.IsNewRow)
                            dataGridViewFiles.Rows.Remove(row);
                    }
                    UpdateStatus();
                }
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
                var result = MessageBox.Show(
                    "Очистить весь список файлов?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dataGridViewFiles.Rows.Clear();
                    UpdateStatus();
                }
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
            string quality = cmbQuality.SelectedItem?.ToString() ?? "Среднее";
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

            StartConversion(format, quality);
        }

        private void StartConversion(string format, string quality)
        {
            int totalFiles = dataGridViewFiles.Rows.Count;
            int currentFile = 0;

            // Сбрасываем статусы
            foreach (DataGridViewRow row in dataGridViewFiles.Rows)
            {
                row.Cells["Status"].Value = "Ожидание";
                row.DefaultCellStyle.BackColor = Color.White;
            }

            progressBar.Maximum = totalFiles;
            progressBar.Value = 0;
            lblStatus.Text = $"Конвертация в {format}...";

            // Имитация конвертации (в реальном проекте здесь будет вызов библиотеки)
            Timer timer = new Timer { Interval = 300 };
            timer.Tick += (s, ev) => {
                if (currentFile < totalFiles)
                {
                    DataGridViewRow row = dataGridViewFiles.Rows[currentFile];
                    row.Cells["Status"].Value = $"Конвертация...";
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);

                    currentFile++;
                    progressBar.Value = currentFile;

                    if (currentFile == totalFiles)
                    {
                        timer.Stop();
                        timer.Dispose();

                        // Отмечаем все как готовые
                        foreach (DataGridViewRow r in dataGridViewFiles.Rows)
                        {
                            r.Cells["Status"].Value = "Готово ✓";
                            r.DefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);
                        }

                        lblStatus.Text = "Конвертация завершена!";
                        MessageBox.Show(
                            $"Конвертация завершена!\n\n" +
                            $"Формат: {format}\n" +
                            $"Качество: {quality}\n" +
                            $"Файлов: {totalFiles}\n" +
                            $"Сохранено в:\n{txtOutputPath.Text}",
                            "Успех",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            };
            timer.Start();
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