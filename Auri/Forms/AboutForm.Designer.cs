namespace Auri.Forms
{
    partial class AboutForm
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
            panelMain = new Panel();
            linkLabel1 = new LinkLabel();
            pictureBoxIcon = new PictureBox();
            lblTitle = new Label();
            lblVersion = new Label();
            lblCopyright = new Label();
            lblCompany = new Label();
            separator1 = new Label();
            lblDescription = new Label();
            lblFeaturesTitle = new Label();
            lblFeatures = new Label();
            lnkGitHub = new LinkLabel();
            btnOK = new Button();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).BeginInit();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(linkLabel1);
            panelMain.Controls.Add(pictureBoxIcon);
            panelMain.Controls.Add(lblTitle);
            panelMain.Controls.Add(lblVersion);
            panelMain.Controls.Add(lblCopyright);
            panelMain.Controls.Add(lblCompany);
            panelMain.Controls.Add(separator1);
            panelMain.Controls.Add(lblDescription);
            panelMain.Controls.Add(lblFeaturesTitle);
            panelMain.Controls.Add(lblFeatures);
            panelMain.Controls.Add(lnkGitHub);
            panelMain.Controls.Add(btnOK);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(450, 420);
            panelMain.TabIndex = 0;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9F);
            linkLabel1.Location = new Point(20, 385);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(69, 15);
            linkLabel1.TabIndex = 11;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Поддержка";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // pictureBoxIcon
            // 
            pictureBoxIcon.Image = Properties.Resources.auri;
            pictureBoxIcon.Location = new Point(20, 20);
            pictureBoxIcon.Name = "pictureBoxIcon";
            pictureBoxIcon.Size = new Size(64, 64);
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxIcon.TabIndex = 0;
            pictureBoxIcon.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(100, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(71, 37);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Auri";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 9F);
            lblVersion.ForeColor = Color.Gray;
            lblVersion.Location = new Point(104, 62);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(52, 15);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "Версия: ";
            // 
            // lblCopyright
            // 
            lblCopyright.AutoSize = true;
            lblCopyright.Font = new Font("Segoe UI", 8F);
            lblCopyright.ForeColor = Color.DimGray;
            lblCopyright.Location = new Point(20, 100);
            lblCopyright.Name = "lblCopyright";
            lblCopyright.Size = new Size(190, 13);
            lblCopyright.TabIndex = 3;
            lblCopyright.Text = "© 2026 Auri. Все права защищены.";
            // 
            // lblCompany
            // 
            lblCompany.AutoSize = true;
            lblCompany.Font = new Font("Segoe UI", 8F);
            lblCompany.ForeColor = Color.DimGray;
            lblCompany.Location = new Point(20, 118);
            lblCompany.Name = "lblCompany";
            lblCompany.Size = new Size(64, 13);
            lblCompany.TabIndex = 4;
            lblCompany.Text = "Компания:";
            // 
            // separator1
            // 
            separator1.BorderStyle = BorderStyle.Fixed3D;
            separator1.Location = new Point(20, 145);
            separator1.Name = "separator1";
            separator1.Size = new Size(410, 2);
            separator1.TabIndex = 5;
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 9F);
            lblDescription.Location = new Point(20, 160);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(410, 60);
            lblDescription.TabIndex = 6;
            lblDescription.Text = "Auri — мощный и удобный конвертер аудиофайлов. Программа поддерживает множество форматов и предоставляет гибкие настройки для получения качественного звука.";
            // 
            // lblFeaturesTitle
            // 
            lblFeaturesTitle.AutoSize = true;
            lblFeaturesTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFeaturesTitle.Location = new Point(20, 235);
            lblFeaturesTitle.Name = "lblFeaturesTitle";
            lblFeaturesTitle.Size = new Size(152, 19);
            lblFeaturesTitle.TabIndex = 7;
            lblFeaturesTitle.Text = "Основные функции:";
            // 
            // lblFeatures
            // 
            lblFeatures.Font = new Font("Segoe UI", 8.5F);
            lblFeatures.Location = new Point(20, 260);
            lblFeatures.Name = "lblFeatures";
            lblFeatures.Size = new Size(410, 85);
            lblFeatures.TabIndex = 8;
            lblFeatures.Text = "• Конвертация в MP3, Opus, QAAC, FLAC, WAV\r\n• Поддержка тегов и создание структуры папок\r\n• Готовые пресеты и ручная настройка параметров\r\n• Поддержка LossyWAV для FLAC\r\n• Мастер быстрого старта";
            // 
            // lnkGitHub
            // 
            lnkGitHub.AutoSize = true;
            lnkGitHub.Font = new Font("Segoe UI", 9F);
            lnkGitHub.Location = new Point(20, 355);
            lnkGitHub.Name = "lnkGitHub";
            lnkGitHub.Size = new Size(224, 15);
            lnkGitHub.TabIndex = 9;
            lnkGitHub.TabStop = true;
            lnkGitHub.Text = "GitHub: https://github.com/DiZLike/Auri";
            lnkGitHub.LinkClicked += lnkGitHub_LinkClicked;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.DialogResult = DialogResult.OK;
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Location = new Point(355, 385);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 25);
            btnOK.TabIndex = 10;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 420);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "О программе";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label separator1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblFeaturesTitle;
        private System.Windows.Forms.Label lblFeatures;
        private System.Windows.Forms.LinkLabel lnkGitHub;
        private System.Windows.Forms.Button btnOK;
        private LinkLabel linkLabel1;
    }
}