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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnAddFiles = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.dataGridViewFiles = new System.Windows.Forms.DataGridView();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFormat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblOutputFormat = new System.Windows.Forms.Label();
            this.cmbOutputFormat = new System.Windows.Forms.ComboBox();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cmbQuality = new System.Windows.Forms.ComboBox();
            this.panelControls = new System.Windows.Forms.Panel();
            this.panelFormats = new System.Windows.Forms.Panel();
            this.btnPattern = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.cbSaveTracks = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbThreadCount = new System.Windows.Forms.ComboBox();
            this.btnUserPreset = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiles)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.panelFormats.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddFiles
            // 
            this.btnAddFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFiles.Location = new System.Drawing.Point(13, 15);
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.Size = new System.Drawing.Size(130, 30);
            this.btnAddFiles.TabIndex = 0;
            this.btnAddFiles.Text = "➕ Добавить файлы";
            this.btnAddFiles.UseVisualStyleBackColor = true;
            this.btnAddFiles.Click += new System.EventHandler(this.BtnAddFiles_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvert.ForeColor = System.Drawing.Color.White;
            this.btnConvert.Location = new System.Drawing.Point(763, 17);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(130, 30);
            this.btnConvert.TabIndex = 3;
            this.btnConvert.Text = "▶ Конвертировать";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.BtnConvert_Click);
            // 
            // dataGridViewFiles
            // 
            this.dataGridViewFiles.AllowUserToAddRows = false;
            this.dataGridViewFiles.AllowUserToDeleteRows = false;
            this.dataGridViewFiles.AllowUserToResizeRows = false;
            this.dataGridViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFiles.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileName,
            this.colFormat,
            this.colSize,
            this.colStatus});
            this.dataGridViewFiles.Location = new System.Drawing.Point(13, 66);
            this.dataGridViewFiles.Name = "dataGridViewFiles";
            this.dataGridViewFiles.ReadOnly = true;
            this.dataGridViewFiles.RowHeadersVisible = false;
            this.dataGridViewFiles.RowTemplate.Height = 25;
            this.dataGridViewFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFiles.Size = new System.Drawing.Size(880, 354);
            this.dataGridViewFiles.TabIndex = 1;
            // 
            // colFileName
            // 
            this.colFileName.FillWeight = 65F;
            this.colFileName.HeaderText = "Имя файла";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            // 
            // colFormat
            // 
            this.colFormat.FillWeight = 80F;
            this.colFormat.HeaderText = "Формат";
            this.colFormat.Name = "colFormat";
            this.colFormat.ReadOnly = true;
            // 
            // colSize
            // 
            this.colSize.FillWeight = 20F;
            this.colSize.HeaderText = "Размер";
            this.colSize.Name = "colSize";
            this.colSize.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.FillWeight = 20F;
            this.colStatus.HeaderText = "Статус";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // lblOutputFormat
            // 
            this.lblOutputFormat.AutoSize = true;
            this.lblOutputFormat.Location = new System.Drawing.Point(18, 12);
            this.lblOutputFormat.Name = "lblOutputFormat";
            this.lblOutputFormat.Size = new System.Drawing.Size(96, 15);
            this.lblOutputFormat.TabIndex = 0;
            this.lblOutputFormat.Text = "Формат вывода:";
            // 
            // cmbOutputFormat
            // 
            this.cmbOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputFormat.FormattingEnabled = true;
            this.cmbOutputFormat.Items.AddRange(new object[] {
            "MP3 (популярный, хорошее сжатие)",
            "OPUS (лучшее качество, отличное сжатие)",
            "WAV (без сжатия, высокое качество)",
            "FLAC (без потерь, сжатие)",
            "AAC (лучше MP3 при низких битрейтах)",
            "OGG (хорошее качество)"});
            this.cmbOutputFormat.Location = new System.Drawing.Point(124, 9);
            this.cmbOutputFormat.Name = "cmbOutputFormat";
            this.cmbOutputFormat.Size = new System.Drawing.Size(364, 23);
            this.cmbOutputFormat.TabIndex = 1;
            this.cmbOutputFormat.SelectedIndexChanged += new System.EventHandler(this.cmbOutputFormat_SelectedIndexChanged);
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(18, 70);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(87, 15);
            this.lblOutputFolder.TabIndex = 4;
            this.lblOutputFolder.Text = "Папка вывода:";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(124, 67);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.ReadOnly = true;
            this.txtOutputPath.Size = new System.Drawing.Size(288, 23);
            this.txtOutputPath.TabIndex = 5;
            this.txtOutputPath.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtOutputPath_MouseClick);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFolder.Location = new System.Drawing.Point(425, 65);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(63, 25);
            this.btnOpenFolder.TabIndex = 6;
            this.btnOpenFolder.Text = "Откр.";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 576);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(906, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(88, 17);
            this.lblStatus.Text = "Готов к работе";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveSelected.Location = new System.Drawing.Point(149, 15);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(130, 30);
            this.btnRemoveSelected.TabIndex = 1;
            this.btnRemoveSelected.Text = "🗑 Удалить";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.BtnRemoveSelected_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAll.Location = new System.Drawing.Point(285, 15);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(100, 30);
            this.btnClearAll.TabIndex = 2;
            this.btnClearAll.Text = "Очистить всё";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(18, 41);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(60, 15);
            this.lblQuality.TabIndex = 2;
            this.lblQuality.Text = "Качество:";
            // 
            // cmbQuality
            // 
            this.cmbQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuality.FormattingEnabled = true;
            this.cmbQuality.Location = new System.Drawing.Point(124, 38);
            this.cmbQuality.Name = "cmbQuality";
            this.cmbQuality.Size = new System.Drawing.Size(288, 23);
            this.cmbQuality.TabIndex = 3;
            this.cmbQuality.SelectedIndexChanged += new System.EventHandler(this.cmbQuality_SelectedIndexChanged);
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelControls.Controls.Add(this.btnClearAll);
            this.panelControls.Controls.Add(this.btnRemoveSelected);
            this.panelControls.Controls.Add(this.btnAddFiles);
            this.panelControls.Controls.Add(this.btnConvert);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Padding = new System.Windows.Forms.Padding(10);
            this.panelControls.Size = new System.Drawing.Size(906, 60);
            this.panelControls.TabIndex = 0;
            // 
            // panelFormats
            // 
            this.panelFormats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFormats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelFormats.Controls.Add(this.btnPattern);
            this.panelFormats.Controls.Add(this.label2);
            this.panelFormats.Controls.Add(this.txtPattern);
            this.panelFormats.Controls.Add(this.cbSaveTracks);
            this.panelFormats.Controls.Add(this.label1);
            this.panelFormats.Controls.Add(this.cmbThreadCount);
            this.panelFormats.Controls.Add(this.btnUserPreset);
            this.panelFormats.Controls.Add(this.progressBar);
            this.panelFormats.Controls.Add(this.lblQuality);
            this.panelFormats.Controls.Add(this.cmbQuality);
            this.panelFormats.Controls.Add(this.lblOutputFormat);
            this.panelFormats.Controls.Add(this.cmbOutputFormat);
            this.panelFormats.Controls.Add(this.lblOutputFolder);
            this.panelFormats.Controls.Add(this.txtOutputPath);
            this.panelFormats.Controls.Add(this.btnOpenFolder);
            this.panelFormats.Location = new System.Drawing.Point(0, 426);
            this.panelFormats.Name = "panelFormats";
            this.panelFormats.Padding = new System.Windows.Forms.Padding(15);
            this.panelFormats.Size = new System.Drawing.Size(906, 147);
            this.panelFormats.TabIndex = 2;
            // 
            // btnPattern
            // 
            this.btnPattern.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPattern.Location = new System.Drawing.Point(425, 96);
            this.btnPattern.Name = "btnPattern";
            this.btnPattern.Size = new System.Drawing.Size(63, 25);
            this.btnPattern.TabIndex = 13;
            this.btnPattern.Text = "Шаблон";
            this.btnPattern.UseVisualStyleBackColor = true;
            this.btnPattern.Click += new System.EventHandler(this.btnPattern_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Структура папок:";
            // 
            // txtPattern
            // 
            this.txtPattern.Location = new System.Drawing.Point(124, 96);
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(288, 23);
            this.txtPattern.TabIndex = 11;
            // 
            // cbSaveTracks
            // 
            this.cbSaveTracks.AutoSize = true;
            this.cbSaveTracks.Location = new System.Drawing.Point(497, 37);
            this.cbSaveTracks.Name = "cbSaveTracks";
            this.cbSaveTracks.Size = new System.Drawing.Size(170, 19);
            this.cbSaveTracks.TabIndex = 10;
            this.cbSaveTracks.Text = "Сохранять список файлов";
            this.cbSaveTracks.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(494, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Использовать ядер:";
            // 
            // cmbThreadCount
            // 
            this.cmbThreadCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThreadCount.FormattingEnabled = true;
            this.cmbThreadCount.Location = new System.Drawing.Point(615, 9);
            this.cmbThreadCount.Name = "cmbThreadCount";
            this.cmbThreadCount.Size = new System.Drawing.Size(58, 23);
            this.cmbThreadCount.TabIndex = 8;
            // 
            // btnUserPreset
            // 
            this.btnUserPreset.Enabled = false;
            this.btnUserPreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserPreset.Location = new System.Drawing.Point(425, 36);
            this.btnUserPreset.Name = "btnUserPreset";
            this.btnUserPreset.Size = new System.Drawing.Size(63, 25);
            this.btnUserPreset.TabIndex = 7;
            this.btnUserPreset.Text = "Настр.";
            this.btnUserPreset.UseVisualStyleBackColor = true;
            this.btnUserPreset.Click += new System.EventHandler(this.btnUserPreset_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 126);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(889, 13);
            this.progressBar.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 598);
            this.Controls.Add(this.dataGridViewFiles);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.panelFormats);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Auri";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFiles)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelControls.ResumeLayout(false);
            this.panelFormats.ResumeLayout(false);
            this.panelFormats.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ComboBox cmbThreadCount;
        private System.Windows.Forms.CheckBox cbSaveTracks;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFormat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Button btnPattern;
    }
}