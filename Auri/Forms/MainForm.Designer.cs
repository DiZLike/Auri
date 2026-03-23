namespace Auri
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnAddFiles = new Button();
            btnConvert = new Button();
            dataGridViewFiles = new DataGridView();
            colFileName = new DataGridViewTextBoxColumn();
            colFormat = new DataGridViewTextBoxColumn();
            colSize = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            lblOutputFormat = new Label();
            cmbOutputFormat = new ComboBox();
            lblOutputFolder = new Label();
            txtOutputPath = new TextBox();
            btnOpenFolder = new Button();
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            btnRemoveSelected = new Button();
            btnClearAll = new Button();
            lblQuality = new Label();
            cmbQuality = new ComboBox();
            panelControls = new Panel();
            btnQuickConvert = new Button();
            panelFormats = new Panel();
            cbRewriteFiles = new CheckBox();
            label4 = new Label();
            label3 = new Label();
            tbThreadCount = new TrackBar();
            btnPattern = new Button();
            label2 = new Label();
            txtPattern = new TextBox();
            cbSaveTracks = new CheckBox();
            label1 = new Label();
            btnUserPreset = new Button();
            progressBar = new ProgressBar();
            toolTip1 = new ToolTip(components);
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            удалитьВыбранныеToolStripMenuItem = new ToolStripMenuItem();
            очиститьВсёToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            выйтиToolStripMenuItem = new ToolStripMenuItem();
            конвертерToolStripMenuItem = new ToolStripMenuItem();
            конвертироватьToolStripMenuItem = new ToolStripMenuItem();
            быстраяКонвертацияToolStripMenuItem = new ToolStripMenuItem();
            помощьToolStripMenuItem = new ToolStripMenuItem();
            мастерНастройкиToolStripMenuItem = new ToolStripMenuItem();
            оПрограммеToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).BeginInit();
            statusStrip.SuspendLayout();
            panelControls.SuspendLayout();
            panelFormats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbThreadCount).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnAddFiles
            // 
            btnAddFiles.FlatStyle = FlatStyle.Flat;
            btnAddFiles.Location = new Point(13, 15);
            btnAddFiles.Name = "btnAddFiles";
            btnAddFiles.Size = new Size(146, 30);
            btnAddFiles.TabIndex = 0;
            btnAddFiles.Text = "➕ Добавить файлы";
            toolTip1.SetToolTip(btnAddFiles, "Добавить аудиофайлы для конвертации. Можно выбрать несколько файлов сразу");
            btnAddFiles.UseVisualStyleBackColor = true;
            btnAddFiles.Click += BtnAddFiles_Click;
            // 
            // btnConvert
            // 
            btnConvert.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConvert.BackColor = Color.FromArgb(76, 175, 80);
            btnConvert.FlatStyle = FlatStyle.Flat;
            btnConvert.ForeColor = Color.White;
            btnConvert.Location = new Point(650, 14);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(153, 32);
            btnConvert.TabIndex = 3;
            btnConvert.Tag = "convert";
            btnConvert.Text = "▶ Конвертировать";
            toolTip1.SetToolTip(btnConvert, "Запустить конвертацию всех файлов из списка с выбранными настройками");
            btnConvert.UseVisualStyleBackColor = false;
            btnConvert.Click += BtnConvert_Click;
            // 
            // dataGridViewFiles
            // 
            dataGridViewFiles.AllowDrop = true;
            dataGridViewFiles.AllowUserToAddRows = false;
            dataGridViewFiles.AllowUserToDeleteRows = false;
            dataGridViewFiles.AllowUserToResizeRows = false;
            dataGridViewFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewFiles.BackgroundColor = Color.White;
            dataGridViewFiles.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewFiles.Columns.AddRange(new DataGridViewColumn[] { colFileName, colFormat, colSize, colStatus });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewFiles.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewFiles.Location = new Point(13, 93);
            dataGridViewFiles.Name = "dataGridViewFiles";
            dataGridViewFiles.ReadOnly = true;
            dataGridViewFiles.RowHeadersVisible = false;
            dataGridViewFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewFiles.Size = new Size(944, 400);
            dataGridViewFiles.TabIndex = 1;
            dataGridViewFiles.DragDrop += dataGridViewFiles_DragDrop;
            dataGridViewFiles.DragEnter += dataGridViewFiles_DragEnter;
            // 
            // colFileName
            // 
            colFileName.FillWeight = 65F;
            colFileName.HeaderText = "Имя файла";
            colFileName.Name = "colFileName";
            colFileName.ReadOnly = true;
            // 
            // colFormat
            // 
            colFormat.FillWeight = 80F;
            colFormat.HeaderText = "Исходный формат";
            colFormat.Name = "colFormat";
            colFormat.ReadOnly = true;
            // 
            // colSize
            // 
            colSize.FillWeight = 20F;
            colSize.HeaderText = "Размер";
            colSize.Name = "colSize";
            colSize.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.FillWeight = 20F;
            colStatus.HeaderText = "Состояние";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // lblOutputFormat
            // 
            lblOutputFormat.AutoSize = true;
            lblOutputFormat.Location = new Point(18, 12);
            lblOutputFormat.Name = "lblOutputFormat";
            lblOutputFormat.Size = new Size(96, 15);
            lblOutputFormat.TabIndex = 0;
            lblOutputFormat.Text = "Формат вывода:";
            // 
            // cmbOutputFormat
            // 
            cmbOutputFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOutputFormat.FormattingEnabled = true;
            cmbOutputFormat.Items.AddRange(new object[] { "MP3 (популярный, хорошее сжатие, среднее качество)", "Opus (современный, лучшее качество, отличное сжатие)", "QAAC (формат Apple, отличное сжатие)", "FLAC (качество без потерь, много места)", "WAV (без сжатия, очень много места)" });
            cmbOutputFormat.Location = new Point(124, 9);
            cmbOutputFormat.Name = "cmbOutputFormat";
            cmbOutputFormat.Size = new Size(364, 23);
            cmbOutputFormat.TabIndex = 1;
            toolTip1.SetToolTip(cmbOutputFormat, "В какой формат преобразовывать файлы");
            cmbOutputFormat.SelectedIndexChanged += cmbOutputFormat_SelectedIndexChanged;
            // 
            // lblOutputFolder
            // 
            lblOutputFolder.AutoSize = true;
            lblOutputFolder.Location = new Point(18, 70);
            lblOutputFolder.Name = "lblOutputFolder";
            lblOutputFolder.Size = new Size(87, 15);
            lblOutputFolder.TabIndex = 4;
            lblOutputFolder.Text = "Папка вывода:";
            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new Point(124, 67);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.ReadOnly = true;
            txtOutputPath.Size = new Size(288, 23);
            txtOutputPath.TabIndex = 5;
            toolTip1.SetToolTip(txtOutputPath, "Куда сохранять сконвертированные файлы. Нажмите на поле, чтобы изменить папку");
            txtOutputPath.MouseClick += txtOutputPath_MouseClick;
            // 
            // btnOpenFolder
            // 
            btnOpenFolder.FlatStyle = FlatStyle.Flat;
            btnOpenFolder.Location = new Point(425, 65);
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.Size = new Size(63, 25);
            btnOpenFolder.TabIndex = 6;
            btnOpenFolder.Text = "Откр.";
            btnOpenFolder.UseVisualStyleBackColor = true;
            btnOpenFolder.Click += btnOpenFolder_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 649);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(970, 22);
            statusStrip.TabIndex = 3;
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(88, 17);
            lblStatus.Text = "Готов к работе";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnRemoveSelected
            // 
            btnRemoveSelected.FlatStyle = FlatStyle.Flat;
            btnRemoveSelected.Location = new Point(165, 15);
            btnRemoveSelected.Name = "btnRemoveSelected";
            btnRemoveSelected.Size = new Size(146, 30);
            btnRemoveSelected.TabIndex = 1;
            btnRemoveSelected.Text = "🗑 Удалить выбранные";
            toolTip1.SetToolTip(btnRemoveSelected, "Убрать из списка выделенные файлы");
            btnRemoveSelected.UseVisualStyleBackColor = true;
            btnRemoveSelected.Click += BtnRemoveSelected_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.FlatStyle = FlatStyle.Flat;
            btnClearAll.Location = new Point(317, 15);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(146, 30);
            btnClearAll.TabIndex = 2;
            btnClearAll.Text = "🗑 Очистить всё";
            toolTip1.SetToolTip(btnClearAll, "Полностью очистить список файлов");
            btnClearAll.UseVisualStyleBackColor = true;
            btnClearAll.Click += BtnClearAll_Click;
            // 
            // lblQuality
            // 
            lblQuality.AutoSize = true;
            lblQuality.Location = new Point(18, 41);
            lblQuality.Name = "lblQuality";
            lblQuality.Size = new Size(60, 15);
            lblQuality.TabIndex = 2;
            lblQuality.Text = "Качество:";
            // 
            // cmbQuality
            // 
            cmbQuality.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQuality.FormattingEnabled = true;
            cmbQuality.Location = new Point(124, 38);
            cmbQuality.Name = "cmbQuality";
            cmbQuality.Size = new Size(288, 23);
            cmbQuality.TabIndex = 3;
            toolTip1.SetToolTip(cmbQuality, "Предустановленные настройки качества для выбранного формата");
            cmbQuality.SelectedIndexChanged += cmbQuality_SelectedIndexChanged;
            // 
            // panelControls
            // 
            panelControls.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelControls.BackColor = Color.FromArgb(245, 245, 250);
            panelControls.Controls.Add(btnQuickConvert);
            panelControls.Controls.Add(btnClearAll);
            panelControls.Controls.Add(btnRemoveSelected);
            panelControls.Controls.Add(btnAddFiles);
            panelControls.Controls.Add(btnConvert);
            panelControls.Location = new Point(0, 27);
            panelControls.Name = "panelControls";
            panelControls.Padding = new Padding(10);
            panelControls.Size = new Size(970, 60);
            panelControls.TabIndex = 0;
            // 
            // btnQuickConvert
            // 
            btnQuickConvert.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuickConvert.BackColor = Color.FromArgb(76, 175, 80);
            btnQuickConvert.FlatStyle = FlatStyle.Flat;
            btnQuickConvert.ForeColor = Color.White;
            btnQuickConvert.Location = new Point(809, 14);
            btnQuickConvert.Name = "btnQuickConvert";
            btnQuickConvert.Size = new Size(148, 32);
            btnQuickConvert.TabIndex = 4;
            btnQuickConvert.Text = "⚡Быстрая конвертация";
            toolTip1.SetToolTip(btnQuickConvert, "Мгновенно конвертировать файлы в формат выбранный мастером быстрого старта.\r\nУдобно для быстрой обработки без лишних настроек");
            btnQuickConvert.UseVisualStyleBackColor = false;
            btnQuickConvert.Click += btnQuickConvert_Click;
            // 
            // panelFormats
            // 
            panelFormats.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelFormats.BackColor = Color.FromArgb(245, 245, 250);
            panelFormats.Controls.Add(cbRewriteFiles);
            panelFormats.Controls.Add(label4);
            panelFormats.Controls.Add(label3);
            panelFormats.Controls.Add(tbThreadCount);
            panelFormats.Controls.Add(btnPattern);
            panelFormats.Controls.Add(label2);
            panelFormats.Controls.Add(txtPattern);
            panelFormats.Controls.Add(cbSaveTracks);
            panelFormats.Controls.Add(label1);
            panelFormats.Controls.Add(btnUserPreset);
            panelFormats.Controls.Add(progressBar);
            panelFormats.Controls.Add(lblQuality);
            panelFormats.Controls.Add(cmbQuality);
            panelFormats.Controls.Add(lblOutputFormat);
            panelFormats.Controls.Add(cmbOutputFormat);
            panelFormats.Controls.Add(lblOutputFolder);
            panelFormats.Controls.Add(txtOutputPath);
            panelFormats.Controls.Add(btnOpenFolder);
            panelFormats.Location = new Point(0, 499);
            panelFormats.Name = "panelFormats";
            panelFormats.Padding = new Padding(15);
            panelFormats.Size = new Size(970, 147);
            panelFormats.TabIndex = 2;
            // 
            // cbRewriteFiles
            // 
            cbRewriteFiles.AutoSize = true;
            cbRewriteFiles.Location = new Point(497, 99);
            cbRewriteFiles.Name = "cbRewriteFiles";
            cbRewriteFiles.Size = new Size(131, 19);
            cbRewriteFiles.TabIndex = 28;
            cbRewriteFiles.Text = "Режим перезаписи";
            toolTip1.SetToolTip(cbRewriteFiles, "Существующие файлы в целевой папке будут перезаписаны.\r\nБудьте внимательны: восстановить старые файлы будет невозможно");
            cbRewriteFiles.UseVisualStyleBackColor = true;
            cbRewriteFiles.CheckedChanged += cbRewriteFiles_CheckedChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(609, 49);
            label4.Name = "label4";
            label4.Size = new Size(94, 15);
            label4.TabIndex = 27;
            label4.Text = "Максимальный";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(494, 49);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 26;
            label3.Text = "Экономичный";
            // 
            // tbThreadCount
            // 
            tbThreadCount.AutoSize = false;
            tbThreadCount.BackColor = Color.FromArgb(245, 245, 250);
            tbThreadCount.LargeChange = 1;
            tbThreadCount.Location = new Point(497, 30);
            tbThreadCount.Maximum = 9;
            tbThreadCount.Minimum = 1;
            tbThreadCount.Name = "tbThreadCount";
            tbThreadCount.Size = new Size(204, 21);
            tbThreadCount.TabIndex = 25;
            toolTip1.SetToolTip(tbThreadCount, "Сколько ядер процессора использовать для конвертации.\r\nЭкономичный – меньше нагрузки на компьютер\r\nМаксимальный – быстрее, но компьютер может тормозить");
            tbThreadCount.Value = 1;
            // 
            // btnPattern
            // 
            btnPattern.FlatStyle = FlatStyle.Flat;
            btnPattern.Location = new Point(425, 96);
            btnPattern.Name = "btnPattern";
            btnPattern.Size = new Size(63, 25);
            btnPattern.TabIndex = 13;
            btnPattern.Text = "Шаблон";
            toolTip1.SetToolTip(btnPattern, "Выбора переменных при создании структуры папок (исполнитель, альбом, трек и т.д.)");
            btnPattern.UseVisualStyleBackColor = true;
            btnPattern.Click += btnPattern_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 99);
            label2.Name = "label2";
            label2.Size = new Size(102, 15);
            label2.TabIndex = 12;
            label2.Text = "Структура папок:";
            // 
            // txtPattern
            // 
            txtPattern.Location = new Point(124, 96);
            txtPattern.Name = "txtPattern";
            txtPattern.Size = new Size(288, 23);
            txtPattern.TabIndex = 11;
            toolTip1.SetToolTip(txtPattern, "Формат создания структуры папок");
            // 
            // cbSaveTracks
            // 
            cbSaveTracks.AutoSize = true;
            cbSaveTracks.Location = new Point(497, 70);
            cbSaveTracks.Name = "cbSaveTracks";
            cbSaveTracks.Size = new Size(170, 19);
            cbSaveTracks.TabIndex = 10;
            cbSaveTracks.Text = "Сохранять список файлов";
            toolTip1.SetToolTip(cbSaveTracks, "После закрытия программы сохранить список файлов в текстовый файл (рядом с программой)");
            cbSaveTracks.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(494, 12);
            label1.Name = "label1";
            label1.Size = new Size(166, 15);
            label1.TabIndex = 9;
            label1.Text = "Режим производительности:";
            // 
            // btnUserPreset
            // 
            btnUserPreset.Enabled = false;
            btnUserPreset.FlatStyle = FlatStyle.Flat;
            btnUserPreset.Location = new Point(425, 36);
            btnUserPreset.Name = "btnUserPreset";
            btnUserPreset.Size = new Size(63, 25);
            btnUserPreset.TabIndex = 7;
            btnUserPreset.Text = "Настр.";
            toolTip1.SetToolTip(btnUserPreset, "Открывает окно с ручными настройками параметров кодека для тонкой подстройки");
            btnUserPreset.UseVisualStyleBackColor = true;
            btnUserPreset.Click += btnUserPreset_Click;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(12, 126);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(953, 13);
            progressBar.TabIndex = 4;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, конвертерToolStripMenuItem, помощьToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(970, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1, удалитьВыбранныеToolStripMenuItem, очиститьВсёToolStripMenuItem, toolStripSeparator1, выйтиToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(185, 22);
            toolStripMenuItem1.Text = "Добавить файлы";
            toolStripMenuItem1.Click += BtnAddFiles_Click;
            // 
            // удалитьВыбранныеToolStripMenuItem
            // 
            удалитьВыбранныеToolStripMenuItem.Name = "удалитьВыбранныеToolStripMenuItem";
            удалитьВыбранныеToolStripMenuItem.Size = new Size(185, 22);
            удалитьВыбранныеToolStripMenuItem.Text = "Удалить выбранные";
            удалитьВыбранныеToolStripMenuItem.Click += BtnRemoveSelected_Click;
            // 
            // очиститьВсёToolStripMenuItem
            // 
            очиститьВсёToolStripMenuItem.Name = "очиститьВсёToolStripMenuItem";
            очиститьВсёToolStripMenuItem.Size = new Size(185, 22);
            очиститьВсёToolStripMenuItem.Text = "Очистить всё";
            очиститьВсёToolStripMenuItem.Click += BtnClearAll_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(182, 6);
            // 
            // выйтиToolStripMenuItem
            // 
            выйтиToolStripMenuItem.Name = "выйтиToolStripMenuItem";
            выйтиToolStripMenuItem.Size = new Size(185, 22);
            выйтиToolStripMenuItem.Text = "Выйти";
            выйтиToolStripMenuItem.Click += выйтиToolStripMenuItem_Click;
            // 
            // конвертерToolStripMenuItem
            // 
            конвертерToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { конвертироватьToolStripMenuItem, быстраяКонвертацияToolStripMenuItem });
            конвертерToolStripMenuItem.Name = "конвертерToolStripMenuItem";
            конвертерToolStripMenuItem.Size = new Size(77, 20);
            конвертерToolStripMenuItem.Text = "Конвертер";
            // 
            // конвертироватьToolStripMenuItem
            // 
            конвертироватьToolStripMenuItem.Name = "конвертироватьToolStripMenuItem";
            конвертироватьToolStripMenuItem.Size = new Size(193, 22);
            конвертироватьToolStripMenuItem.Text = "Конвертировать";
            конвертироватьToolStripMenuItem.Click += BtnConvert_Click;
            // 
            // быстраяКонвертацияToolStripMenuItem
            // 
            быстраяКонвертацияToolStripMenuItem.Name = "быстраяКонвертацияToolStripMenuItem";
            быстраяКонвертацияToolStripMenuItem.Size = new Size(193, 22);
            быстраяКонвертацияToolStripMenuItem.Text = "Быстрая конвертация";
            быстраяКонвертацияToolStripMenuItem.Click += btnQuickConvert_Click;
            // 
            // помощьToolStripMenuItem
            // 
            помощьToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { мастерНастройкиToolStripMenuItem, оПрограммеToolStripMenuItem });
            помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            помощьToolStripMenuItem.Size = new Size(68, 20);
            помощьToolStripMenuItem.Text = "Помощь";
            // 
            // мастерНастройкиToolStripMenuItem
            // 
            мастерНастройкиToolStripMenuItem.Name = "мастерНастройкиToolStripMenuItem";
            мастерНастройкиToolStripMenuItem.Size = new Size(180, 22);
            мастерНастройкиToolStripMenuItem.Text = "Мастер настройки";
            мастерНастройкиToolStripMenuItem.Click += мастерНастройкиToolStripMenuItem_Click;
            // 
            // оПрограммеToolStripMenuItem
            // 
            оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            оПрограммеToolStripMenuItem.Size = new Size(180, 22);
            оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(970, 671);
            Controls.Add(dataGridViewFiles);
            Controls.Add(panelControls);
            Controls.Add(panelFormats);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(800, 500);
            Name = "MainForm";
            StartPosition = FormStartPosition.Manual;
            Text = "Auri";
            FormClosing += MainForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            panelControls.ResumeLayout(false);
            panelFormats.ResumeLayout(false);
            panelFormats.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbThreadCount).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        private System.Windows.Forms.Button btnAddFiles;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.DataGridView dataGridViewFiles;
        private System.Windows.Forms.Label lblOutputFormat;
        private System.Windows.Forms.ComboBox cmbOutputFormat;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.ComboBox cmbQuality;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Panel panelFormats;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnUserPreset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSaveTracks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Button btnPattern;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbThreadCount;
        private System.Windows.Forms.Button btnQuickConvert;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFormat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbRewriteFiles;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem удалитьВыбранныеToolStripMenuItem;
        private ToolStripMenuItem очиститьВсёToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem выйтиToolStripMenuItem;
        private ToolStripMenuItem конвертерToolStripMenuItem;
        private ToolStripMenuItem конвертироватьToolStripMenuItem;
        private ToolStripMenuItem быстраяКонвертацияToolStripMenuItem;
        private ToolStripMenuItem помощьToolStripMenuItem;
        private ToolStripMenuItem мастерНастройкиToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
    }
}