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
            this.colDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblOutputFormat = new System.Windows.Forms.Label();
            this.cmbOutputFormat = new System.Windows.Forms.ComboBox();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cmbQuality = new System.Windows.Forms.ComboBox();
            this.panelControls = new System.Windows.Forms.Panel();
            this.panelFormats = new System.Windows.Forms.Panel();
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
            this.btnConvert.Location = new System.Drawing.Point(672, 15);
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
            this.colDuration,
            this.colStatus});
            this.dataGridViewFiles.Location = new System.Drawing.Point(13, 73);
            this.dataGridViewFiles.MultiSelect = false;
            this.dataGridViewFiles.Name = "dataGridViewFiles";
            this.dataGridViewFiles.ReadOnly = true;
            this.dataGridViewFiles.RowHeadersVisible = false;
            this.dataGridViewFiles.RowTemplate.Height = 25;
            this.dataGridViewFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFiles.Size = new System.Drawing.Size(789, 305);
            this.dataGridViewFiles.TabIndex = 1;
            // 
            // colFileName
            // 
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
            this.colSize.FillWeight = 45F;
            this.colSize.HeaderText = "Размер";
            this.colSize.Name = "colSize";
            this.colSize.ReadOnly = true;
            // 
            // colDuration
            // 
            this.colDuration.FillWeight = 90F;
            this.colDuration.HeaderText = "Длительность";
            this.colDuration.Name = "colDuration";
            this.colDuration.ReadOnly = true;
            // 
            // colStatus
            // 
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
            this.cmbOutputFormat.Size = new System.Drawing.Size(292, 23);
            this.cmbOutputFormat.TabIndex = 1;
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
            this.txtOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputPath.Location = new System.Drawing.Point(124, 67);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.ReadOnly = true;
            this.txtOutputPath.Size = new System.Drawing.Size(223, 23);
            this.txtOutputPath.TabIndex = 5;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(353, 67);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(63, 25);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Обзор...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 509);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(815, 22);
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
            this.cmbQuality.Items.AddRange(new object[] {
            "Низкое (64 kbps)",
            "Среднее (128 kbps)",
            "Высокое (192 kbps)",
            "Очень высокое (320 kbps)",
            "Пользовательский"});
            this.cmbQuality.Location = new System.Drawing.Point(124, 38);
            this.cmbQuality.Name = "cmbQuality";
            this.cmbQuality.Size = new System.Drawing.Size(292, 23);
            this.cmbQuality.TabIndex = 3;
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
            this.panelControls.Size = new System.Drawing.Size(815, 60);
            this.panelControls.TabIndex = 0;
            // 
            // panelFormats
            // 
            this.panelFormats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFormats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.panelFormats.Controls.Add(this.lblQuality);
            this.panelFormats.Controls.Add(this.cmbQuality);
            this.panelFormats.Controls.Add(this.lblOutputFormat);
            this.panelFormats.Controls.Add(this.cmbOutputFormat);
            this.panelFormats.Controls.Add(this.lblOutputFolder);
            this.panelFormats.Controls.Add(this.txtOutputPath);
            this.panelFormats.Controls.Add(this.btnBrowse);
            this.panelFormats.Location = new System.Drawing.Point(0, 384);
            this.panelFormats.Name = "panelFormats";
            this.panelFormats.Padding = new System.Windows.Forms.Padding(15);
            this.panelFormats.Size = new System.Drawing.Size(815, 103);
            this.panelFormats.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(9, 493);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(798, 13);
            this.progressBar.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 531);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.dataGridViewFiles);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.panelFormats);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auri";
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
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.ComboBox cmbQuality;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Panel panelFormats;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFormat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}