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
            this.label8 = new System.Windows.Forms.Label();
            this.cmbMp3Channels = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbMp3Comp = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbMp3Mode = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbMp3Bitrate = new System.Windows.Forms.ComboBox();
            this.cmbMp3Frequency = new System.Windows.Forms.ComboBox();
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
            this.flacPage = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tbCompress = new System.Windows.Forms.TrackBar();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cbLossyWav = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.mp3Page.SuspendLayout();
            this.opusPage.SuspendLayout();
            this.flacPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCompress)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.mp3Page);
            this.tabControl1.Controls.Add(this.opusPage);
            this.tabControl1.Controls.Add(this.flacPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(462, 155);
            this.tabControl1.TabIndex = 0;
            // 
            // mp3Page
            // 
            this.mp3Page.Controls.Add(this.label8);
            this.mp3Page.Controls.Add(this.cmbMp3Channels);
            this.mp3Page.Controls.Add(this.label9);
            this.mp3Page.Controls.Add(this.cmbMp3Comp);
            this.mp3Page.Controls.Add(this.label10);
            this.mp3Page.Controls.Add(this.cmbMp3Mode);
            this.mp3Page.Controls.Add(this.label11);
            this.mp3Page.Controls.Add(this.label12);
            this.mp3Page.Controls.Add(this.cmbMp3Bitrate);
            this.mp3Page.Controls.Add(this.cmbMp3Frequency);
            this.mp3Page.Location = new System.Drawing.Point(4, 22);
            this.mp3Page.Name = "mp3Page";
            this.mp3Page.Padding = new System.Windows.Forms.Padding(3);
            this.mp3Page.Size = new System.Drawing.Size(454, 129);
            this.mp3Page.TabIndex = 1;
            this.mp3Page.Text = "MP3";
            this.mp3Page.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Каналы:";
            // 
            // cmbMp3Channels
            // 
            this.cmbMp3Channels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMp3Channels.FormattingEnabled = true;
            this.cmbMp3Channels.Items.AddRange(new object[] {
            "Stereo",
            "Joint Stereo",
            "Forced Joint Stereo",
            "Mono"});
            this.cmbMp3Channels.Location = new System.Drawing.Point(66, 87);
            this.cmbMp3Channels.Name = "cmbMp3Channels";
            this.cmbMp3Channels.Size = new System.Drawing.Size(121, 21);
            this.cmbMp3Channels.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(193, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Алгоритм:";
            // 
            // cmbMp3Comp
            // 
            this.cmbMp3Comp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMp3Comp.FormattingEnabled = true;
            this.cmbMp3Comp.Items.AddRange(new object[] {
            "9",
            "8",
            "7",
            "6",
            "5",
            "4",
            "3",
            "2",
            "1",
            "0"});
            this.cmbMp3Comp.Location = new System.Drawing.Point(281, 6);
            this.cmbMp3Comp.Name = "cmbMp3Comp";
            this.cmbMp3Comp.Size = new System.Drawing.Size(121, 21);
            this.cmbMp3Comp.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Режим:";
            // 
            // cmbMp3Mode
            // 
            this.cmbMp3Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMp3Mode.FormattingEnabled = true;
            this.cmbMp3Mode.Items.AddRange(new object[] {
            "CBR",
            "ABR",
            "VBR"});
            this.cmbMp3Mode.Location = new System.Drawing.Point(66, 60);
            this.cmbMp3Mode.Name = "cmbMp3Mode";
            this.cmbMp3Mode.Size = new System.Drawing.Size(121, 21);
            this.cmbMp3Mode.TabIndex = 18;
            this.cmbMp3Mode.SelectedIndexChanged += new System.EventHandler(this.cmbMp3Mode_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Качество:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Частота:";
            // 
            // cmbMp3Bitrate
            // 
            this.cmbMp3Bitrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMp3Bitrate.FormattingEnabled = true;
            this.cmbMp3Bitrate.Items.AddRange(new object[] {
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
            this.cmbMp3Bitrate.Location = new System.Drawing.Point(66, 33);
            this.cmbMp3Bitrate.Name = "cmbMp3Bitrate";
            this.cmbMp3Bitrate.Size = new System.Drawing.Size(121, 21);
            this.cmbMp3Bitrate.TabIndex = 15;
            // 
            // cmbMp3Frequency
            // 
            this.cmbMp3Frequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMp3Frequency.FormattingEnabled = true;
            this.cmbMp3Frequency.Items.AddRange(new object[] {
            "48000",
            "44100",
            "32000",
            "22050",
            "16000",
            "11025",
            "8000"});
            this.cmbMp3Frequency.Location = new System.Drawing.Point(66, 6);
            this.cmbMp3Frequency.Name = "cmbMp3Frequency";
            this.cmbMp3Frequency.Size = new System.Drawing.Size(121, 21);
            this.cmbMp3Frequency.TabIndex = 14;
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
            this.opusPage.Text = "Opus";
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
            this.label6.Location = new System.Drawing.Point(193, 63);
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
            this.cmbOpusContent.Location = new System.Drawing.Point(281, 60);
            this.cmbOpusContent.Name = "cmbOpusContent";
            this.cmbOpusContent.Size = new System.Drawing.Size(121, 21);
            this.cmbOpusContent.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 36);
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
            this.cmbOpusComp.Location = new System.Drawing.Point(281, 33);
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
            this.label3.Location = new System.Drawing.Point(193, 9);
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
            this.cmbOpusFramesize.Location = new System.Drawing.Point(281, 6);
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
            "16000",
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
            // flacPage
            // 
            this.flacPage.Controls.Add(this.cbLossyWav);
            this.flacPage.Controls.Add(this.label16);
            this.flacPage.Controls.Add(this.label15);
            this.flacPage.Controls.Add(this.label14);
            this.flacPage.Controls.Add(this.tbCompress);
            this.flacPage.Controls.Add(this.label13);
            this.flacPage.Controls.Add(this.comboBox1);
            this.flacPage.Location = new System.Drawing.Point(4, 22);
            this.flacPage.Name = "flacPage";
            this.flacPage.Size = new System.Drawing.Size(454, 129);
            this.flacPage.TabIndex = 2;
            this.flacPage.Text = "FLAC";
            this.flacPage.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "Частота:";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "48000",
            "44100",
            "32000",
            "22050",
            "16000",
            "11025",
            "8000"});
            this.comboBox1.Location = new System.Drawing.Point(66, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.Text = "44100";
            // 
            // tbCompress
            // 
            this.tbCompress.AutoSize = false;
            this.tbCompress.LargeChange = 1;
            this.tbCompress.Location = new System.Drawing.Point(66, 33);
            this.tbCompress.Maximum = 8;
            this.tbCompress.Name = "tbCompress";
            this.tbCompress.Size = new System.Drawing.Size(121, 21);
            this.tbCompress.TabIndex = 20;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 21;
            this.label14.Text = "Частота:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(63, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(13, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(174, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 13);
            this.label16.TabIndex = 23;
            this.label16.Text = "8";
            // 
            // cbLossyWav
            // 
            this.cbLossyWav.AutoSize = true;
            this.cbLossyWav.Location = new System.Drawing.Point(207, 10);
            this.cbLossyWav.Name = "cbLossyWav";
            this.cbLossyWav.Size = new System.Drawing.Size(76, 17);
            this.cbLossyWav.TabIndex = 24;
            this.cbLossyWav.Text = "LossyWav";
            this.cbLossyWav.UseVisualStyleBackColor = true;
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
            this.mp3Page.ResumeLayout(false);
            this.mp3Page.PerformLayout();
            this.opusPage.ResumeLayout(false);
            this.opusPage.PerformLayout();
            this.flacPage.ResumeLayout(false);
            this.flacPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCompress)).EndInit();
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbMp3Channels;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbMp3Comp;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbMp3Mode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbMp3Bitrate;
        private System.Windows.Forms.ComboBox cmbMp3Frequency;
        private System.Windows.Forms.TabPage flacPage;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TrackBar tbCompress;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox cbLossyWav;
    }
}