namespace Auri.Forms
{
    partial class UserPresetForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mp3Page = new System.Windows.Forms.TabPage();
            this.opusPage = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbOpusChannels = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbOpusContent = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbOpusComp = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbOpusMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbOpusFramesize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbOpusBitrate = new System.Windows.Forms.ComboBox();
            this.cmbOpusFrequency = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.opusPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.mp3Page);
            this.tabControl1.Controls.Add(this.opusPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(462, 155);
            this.tabControl1.TabIndex = 0;
            // 
            // mp3Page
            // 
            this.mp3Page.Location = new System.Drawing.Point(4, 22);
            this.mp3Page.Name = "mp3Page";
            this.mp3Page.Padding = new System.Windows.Forms.Padding(3);
            this.mp3Page.Size = new System.Drawing.Size(454, 129);
            this.mp3Page.TabIndex = 1;
            this.mp3Page.Text = "MP3";
            this.mp3Page.UseVisualStyleBackColor = true;
            // 
            // opusPage
            // 
            this.opusPage.Controls.Add(this.label7);
            this.opusPage.Controls.Add(this.cmbOpusChannels);
            this.opusPage.Controls.Add(this.label6);
            this.opusPage.Controls.Add(this.cmbOpusContent);
            this.opusPage.Controls.Add(this.label5);
            this.opusPage.Controls.Add(this.cmbOpusComp);
            this.opusPage.Controls.Add(this.label4);
            this.opusPage.Controls.Add(this.cmbOpusMode);
            this.opusPage.Controls.Add(this.label3);
            this.opusPage.Controls.Add(this.cmbOpusFramesize);
            this.opusPage.Controls.Add(this.label2);
            this.opusPage.Controls.Add(this.label1);
            this.opusPage.Controls.Add(this.cmbOpusBitrate);
            this.opusPage.Controls.Add(this.cmbOpusFrequency);
            this.opusPage.Location = new System.Drawing.Point(4, 22);
            this.opusPage.Name = "opusPage";
            this.opusPage.Padding = new System.Windows.Forms.Padding(3);
            this.opusPage.Size = new System.Drawing.Size(454, 129);
            this.opusPage.TabIndex = 0;
            this.opusPage.Text = "OPUS";
            this.opusPage.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Каналы:";
            // 
            // cmbOpusChannels
            // 
            this.cmbOpusChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpusChannels.FormattingEnabled = true;
            this.cmbOpusChannels.Items.AddRange(new object[] {
            "Stereo",
            "Mono"});
            this.cmbOpusChannels.Location = new System.Drawing.Point(66, 87);
            this.cmbOpusChannels.Name = "cmbOpusChannels";
            this.cmbOpusChannels.Size = new System.Drawing.Size(121, 21);
            this.cmbOpusChannels.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(198, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Контент:";
            // 
            // cmbOpusContent
            // 
            this.cmbOpusContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpusContent.FormattingEnabled = true;
            this.cmbOpusContent.Items.AddRange(new object[] {
            "Music",
            "Speech"});
            this.cmbOpusContent.Location = new System.Drawing.Point(286, 60);
            this.cmbOpusContent.Name = "cmbOpusContent";
            this.cmbOpusContent.Size = new System.Drawing.Size(121, 21);
            this.cmbOpusContent.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Алгоритм:";
            // 
            // cmbOpusComp
            // 
            this.cmbOpusComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpusComp.FormattingEnabled = true;
            this.cmbOpusComp.Items.AddRange(new object[] {
            "10",
            "9",
            "8",
            "7",
            "6",
            "5",
            "4",
            "3",
            "2",
            "1"});
            this.cmbOpusComp.Location = new System.Drawing.Point(286, 33);
            this.cmbOpusComp.Name = "cmbOpusComp";
            this.cmbOpusComp.Size = new System.Drawing.Size(121, 21);
            this.cmbOpusComp.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Режим:";
            // 
            // cmbOpusMode
            // 
            this.cmbOpusMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpusMode.FormattingEnabled = true;
            this.cmbOpusMode.Items.AddRange(new object[] {
            "VBR",
            "CVBR",
            "HARD-CBR"});
            this.cmbOpusMode.Location = new System.Drawing.Point(66, 60);
            this.cmbOpusMode.Name = "cmbOpusMode";
            this.cmbOpusMode.Size = new System.Drawing.Size(121, 21);
            this.cmbOpusMode.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Размер кадра:";
            // 
            // cmbOpusFramesize
            // 
            this.cmbOpusFramesize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpusFramesize.FormattingEnabled = true;
            this.cmbOpusFramesize.Items.AddRange(new object[] {
            "60",
            "40",
            "20",
            "10",
            "5",
            "2.5"});
            this.cmbOpusFramesize.Location = new System.Drawing.Point(286, 6);
            this.cmbOpusFramesize.Name = "cmbOpusFramesize";
            this.cmbOpusFramesize.Size = new System.Drawing.Size(121, 21);
            this.cmbOpusFramesize.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Качество:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Частота:";
            // 
            // cmbOpusBitrate
            // 
            this.cmbOpusBitrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpusBitrate.FormattingEnabled = true;
            this.cmbOpusBitrate.Items.AddRange(new object[] {
            "320",
            "256",
            "192",
            "160",
            "128",
            "96",
            "64",
            "56",
            "48",
            "40",
            "32"});
            this.cmbOpusBitrate.Location = new System.Drawing.Point(66, 33);
            this.cmbOpusBitrate.Name = "cmbOpusBitrate";
            this.cmbOpusBitrate.Size = new System.Drawing.Size(121, 21);
            this.cmbOpusBitrate.TabIndex = 1;
            // 
            // cmbOpusFrequency
            // 
            this.cmbOpusFrequency.Enabled = false;
            this.cmbOpusFrequency.FormattingEnabled = true;
            this.cmbOpusFrequency.Items.AddRange(new object[] {
            "48000",
            "44100",
            "32000",
            "22050",
            "11025",
            "8000"});
            this.cmbOpusFrequency.Location = new System.Drawing.Point(66, 6);
            this.cmbOpusFrequency.Name = "cmbOpusFrequency";
            this.cmbOpusFrequency.Size = new System.Drawing.Size(121, 21);
            this.cmbOpusFrequency.TabIndex = 0;
            this.cmbOpusFrequency.Text = "48000";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(383, 157);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(302, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UserPresetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 186);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserPresetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Пользовательские настройки энкодера";
            this.tabControl1.ResumeLayout(false);
            this.opusPage.ResumeLayout(false);
            this.opusPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mp3Page;
        private System.Windows.Forms.TabPage opusPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbOpusBitrate;
        private System.Windows.Forms.ComboBox cmbOpusFrequency;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbOpusFramesize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbOpusMode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbOpusContent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbOpusComp;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbOpusChannels;
    }
}