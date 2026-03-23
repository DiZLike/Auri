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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserPresetForm));
            tabControl1 = new TabControl();
            mp3Page = new TabPage();
            tbMp3Quality = new TrackBar();
            label8 = new Label();
            cmbMp3Channels = new ComboBox();
            label9 = new Label();
            label10 = new Label();
            cmbMp3Mode = new ComboBox();
            label11 = new Label();
            label12 = new Label();
            cmbMp3Bitrate = new ComboBox();
            cmbMp3Frequency = new ComboBox();
            opusPage = new TabPage();
            tbOpusQuality = new TrackBar();
            label7 = new Label();
            cmbOpusChannels = new ComboBox();
            label6 = new Label();
            cmbOpusContent = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            cmbOpusMode = new ComboBox();
            label3 = new Label();
            cmbOpusFramesize = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            cmbOpusBitrate = new ComboBox();
            cmbOpusFrequency = new ComboBox();
            qaacPage = new TabPage();
            cbQaacHe = new CheckBox();
            tbQaacVbr = new TrackBar();
            label25 = new Label();
            tbQaacQuality = new TrackBar();
            label24 = new Label();
            label23 = new Label();
            cmbQaacChannels = new ComboBox();
            label15 = new Label();
            cmbQaacMode = new ComboBox();
            label16 = new Label();
            label22 = new Label();
            cmbQaacBitrate = new ComboBox();
            cmbQaacFrequency = new ComboBox();
            flacPage = new TabPage();
            lblLossyWavInfo = new Label();
            label18 = new Label();
            cmbLossyWavQuality = new ComboBox();
            label17 = new Label();
            cmbFlacBitPerSample = new ComboBox();
            cbLossyWav = new CheckBox();
            label14 = new Label();
            tbFlacCompress = new TrackBar();
            label13 = new Label();
            cmbFlacFrequency = new ComboBox();
            wavPage = new TabPage();
            label21 = new Label();
            cmbWaveChannels = new ComboBox();
            label20 = new Label();
            cmbWaveBitPerSample = new ComboBox();
            label19 = new Label();
            cmbWaveFrequency = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            toolTip = new ToolTip(components);
            tabControl1.SuspendLayout();
            mp3Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbMp3Quality).BeginInit();
            opusPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbOpusQuality).BeginInit();
            qaacPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbQaacVbr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbQaacQuality).BeginInit();
            flacPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbFlacCompress).BeginInit();
            wavPage.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(mp3Page);
            tabControl1.Controls.Add(opusPage);
            tabControl1.Controls.Add(qaacPage);
            tabControl1.Controls.Add(flacPage);
            tabControl1.Controls.Add(wavPage);
            tabControl1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(424, 254);
            tabControl1.TabIndex = 0;
            // 
            // mp3Page
            // 
            mp3Page.Controls.Add(tbMp3Quality);
            mp3Page.Controls.Add(label8);
            mp3Page.Controls.Add(cmbMp3Channels);
            mp3Page.Controls.Add(label9);
            mp3Page.Controls.Add(label10);
            mp3Page.Controls.Add(cmbMp3Mode);
            mp3Page.Controls.Add(label11);
            mp3Page.Controls.Add(label12);
            mp3Page.Controls.Add(cmbMp3Bitrate);
            mp3Page.Controls.Add(cmbMp3Frequency);
            mp3Page.Location = new Point(4, 22);
            mp3Page.Margin = new Padding(4, 3, 4, 3);
            mp3Page.Name = "mp3Page";
            mp3Page.Padding = new Padding(4, 3, 4, 3);
            mp3Page.Size = new Size(416, 228);
            mp3Page.TabIndex = 1;
            mp3Page.Text = "MP3";
            mp3Page.UseVisualStyleBackColor = true;
            // 
            // tbMp3Quality
            // 
            tbMp3Quality.AutoSize = false;
            tbMp3Quality.BackColor = Color.Snow;
            tbMp3Quality.LargeChange = 1;
            tbMp3Quality.Location = new Point(227, 125);
            tbMp3Quality.Margin = new Padding(4, 3, 4, 3);
            tbMp3Quality.Maximum = 9;
            tbMp3Quality.Name = "tbMp3Quality";
            tbMp3Quality.Size = new Size(163, 24);
            tbMp3Quality.TabIndex = 24;
            toolTip.SetToolTip(tbMp3Quality, "0 - наилучшее качество (медленнее)\r\n9 - максимальная скорость (ниже качество)");
            tbMp3Quality.Value = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(224, 14);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(90, 13);
            label8.TabIndex = 23;
            label8.Text = "Режим каналов:";
            // 
            // cmbMp3Channels
            // 
            cmbMp3Channels.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMp3Channels.FormattingEnabled = true;
            cmbMp3Channels.Items.AddRange(new object[] { "Stereo", "Joint Stereo", "Forced Joint Stereo", "Mono" });
            cmbMp3Channels.Location = new Point(227, 32);
            cmbMp3Channels.Margin = new Padding(4, 3, 4, 3);
            cmbMp3Channels.Name = "cmbMp3Channels";
            cmbMp3Channels.Size = new Size(163, 21);
            cmbMp3Channels.TabIndex = 22;
            toolTip.SetToolTip(cmbMp3Channels, resources.GetString("cmbMp3Channels.ToolTip"));
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(224, 106);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(110, 13);
            label9.TabIndex = 21;
            label9.Text = "Качество/Скорость:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(14, 106);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(78, 13);
            label10.TabIndex = 19;
            label10.Text = "Тип битрейта:";
            // 
            // cmbMp3Mode
            // 
            cmbMp3Mode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMp3Mode.FormattingEnabled = true;
            cmbMp3Mode.Items.AddRange(new object[] { "CBR", "ABR", "VBR" });
            cmbMp3Mode.Location = new Point(18, 125);
            cmbMp3Mode.Margin = new Padding(4, 3, 4, 3);
            cmbMp3Mode.Name = "cmbMp3Mode";
            cmbMp3Mode.Size = new Size(163, 21);
            cmbMp3Mode.TabIndex = 18;
            toolTip.SetToolTip(cmbMp3Mode, "CBR - постоянный битрейт (предсказуемый размер)\r\nABR - средний битрейт (компромисс)\r\nVBR - переменный битрейт (лучшее качество при том же размере)");
            cmbMp3Mode.SelectedIndexChanged += cmbMp3Mode_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(14, 60);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(94, 13);
            label11.TabIndex = 17;
            label11.Text = "Битрейт (кбит/с):";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(14, 14);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(154, 13);
            label12.TabIndex = 16;
            label12.Text = "Частота дискретизации (Hz):";
            // 
            // cmbMp3Bitrate
            // 
            cmbMp3Bitrate.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMp3Bitrate.FormattingEnabled = true;
            cmbMp3Bitrate.Items.AddRange(new object[] { "320", "256", "192", "160", "128", "96", "64", "56", "48", "40", "32" });
            cmbMp3Bitrate.Location = new Point(18, 78);
            cmbMp3Bitrate.Margin = new Padding(4, 3, 4, 3);
            cmbMp3Bitrate.Name = "cmbMp3Bitrate";
            cmbMp3Bitrate.Size = new Size(163, 21);
            cmbMp3Bitrate.TabIndex = 15;
            toolTip.SetToolTip(cmbMp3Bitrate, resources.GetString("cmbMp3Bitrate.ToolTip"));
            // 
            // cmbMp3Frequency
            // 
            cmbMp3Frequency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMp3Frequency.FormattingEnabled = true;
            cmbMp3Frequency.Items.AddRange(new object[] { "48000", "44100", "32000", "22050", "16000", "11025", "8000" });
            cmbMp3Frequency.Location = new Point(18, 32);
            cmbMp3Frequency.Margin = new Padding(4, 3, 4, 3);
            cmbMp3Frequency.Name = "cmbMp3Frequency";
            cmbMp3Frequency.Size = new Size(163, 21);
            cmbMp3Frequency.TabIndex = 14;
            toolTip.SetToolTip(cmbMp3Frequency, "Чем выше частота, тем лучше передаются высокие частоты.\r\nСтандарт для музыки - 44100");
            // 
            // opusPage
            // 
            opusPage.Controls.Add(tbOpusQuality);
            opusPage.Controls.Add(label7);
            opusPage.Controls.Add(cmbOpusChannels);
            opusPage.Controls.Add(label6);
            opusPage.Controls.Add(cmbOpusContent);
            opusPage.Controls.Add(label5);
            opusPage.Controls.Add(label4);
            opusPage.Controls.Add(cmbOpusMode);
            opusPage.Controls.Add(label3);
            opusPage.Controls.Add(cmbOpusFramesize);
            opusPage.Controls.Add(label2);
            opusPage.Controls.Add(label1);
            opusPage.Controls.Add(cmbOpusBitrate);
            opusPage.Controls.Add(cmbOpusFrequency);
            opusPage.Location = new Point(4, 22);
            opusPage.Margin = new Padding(4, 3, 4, 3);
            opusPage.Name = "opusPage";
            opusPage.Padding = new Padding(4, 3, 4, 3);
            opusPage.Size = new Size(416, 228);
            opusPage.TabIndex = 0;
            opusPage.Text = "Opus";
            opusPage.UseVisualStyleBackColor = true;
            // 
            // tbOpusQuality
            // 
            tbOpusQuality.AutoSize = false;
            tbOpusQuality.BackColor = Color.Snow;
            tbOpusQuality.LargeChange = 1;
            tbOpusQuality.Location = new Point(227, 125);
            tbOpusQuality.Margin = new Padding(4, 3, 4, 3);
            tbOpusQuality.Name = "tbOpusQuality";
            tbOpusQuality.Size = new Size(163, 24);
            tbOpusQuality.TabIndex = 25;
            toolTip.SetToolTip(tbOpusQuality, "Баланс между скоростью кодирования и качеством звука");
            tbOpusQuality.Value = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(224, 14);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(90, 13);
            label7.TabIndex = 13;
            label7.Text = "Режим каналов:";
            // 
            // cmbOpusChannels
            // 
            cmbOpusChannels.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOpusChannels.FormattingEnabled = true;
            cmbOpusChannels.Items.AddRange(new object[] { "Stereo", "Mono" });
            cmbOpusChannels.Location = new Point(227, 32);
            cmbOpusChannels.Margin = new Padding(4, 3, 4, 3);
            cmbOpusChannels.Name = "cmbOpusChannels";
            cmbOpusChannels.Size = new Size(163, 21);
            cmbOpusChannels.TabIndex = 12;
            toolTip.SetToolTip(cmbOpusChannels, "Как обрабатывать стерео.\r\nStereo - полноценное стерео\r\nMono - смешивает в один канал (экономит место)");
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(224, 152);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(117, 13);
            label6.TabIndex = 11;
            label6.Text = "Оптимизировать для:";
            // 
            // cmbOpusContent
            // 
            cmbOpusContent.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOpusContent.FormattingEnabled = true;
            cmbOpusContent.Items.AddRange(new object[] { "Music", "Speech" });
            cmbOpusContent.Location = new Point(227, 171);
            cmbOpusContent.Margin = new Padding(4, 3, 4, 3);
            cmbOpusContent.Name = "cmbOpusContent";
            cmbOpusContent.Size = new Size(163, 21);
            cmbOpusContent.TabIndex = 10;
            toolTip.SetToolTip(cmbOpusContent, "Оптимизация кодека под тип контента\r\nMusic - для музыки\r\nSpeech - для речи/подкастов");
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(224, 106);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(110, 13);
            label5.TabIndex = 9;
            label5.Text = "Скорость/Качество:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 106);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(78, 13);
            label4.TabIndex = 7;
            label4.Text = "Тип битрейта:";
            // 
            // cmbOpusMode
            // 
            cmbOpusMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOpusMode.FormattingEnabled = true;
            cmbOpusMode.Items.AddRange(new object[] { "VBR", "CVBR", "HARD-CBR" });
            cmbOpusMode.Location = new Point(18, 125);
            cmbOpusMode.Margin = new Padding(4, 3, 4, 3);
            cmbOpusMode.Name = "cmbOpusMode";
            cmbOpusMode.Size = new Size(163, 21);
            cmbOpusMode.TabIndex = 6;
            toolTip.SetToolTip(cmbOpusMode, "VBR - качество меняется в зависимости от сложности музыки (лучшее соотношение размер/качество)\r\nCVBR - ограниченный переменный битрейт\r\nHARD-CBR - строго постоянный битрейт (для стримов)");
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 152);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(105, 13);
            label3.TabIndex = 5;
            label3.Text = "Размер кадра (мс):";
            // 
            // cmbOpusFramesize
            // 
            cmbOpusFramesize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOpusFramesize.FormattingEnabled = true;
            cmbOpusFramesize.Items.AddRange(new object[] { "60", "40", "20", "10", "5", "2.5" });
            cmbOpusFramesize.Location = new Point(18, 171);
            cmbOpusFramesize.Margin = new Padding(4, 3, 4, 3);
            cmbOpusFramesize.Name = "cmbOpusFramesize";
            cmbOpusFramesize.Size = new Size(163, 21);
            cmbOpusFramesize.TabIndex = 4;
            toolTip.SetToolTip(cmbOpusFramesize, "Влияет на задержку кодирования.\r\nМеньше значение — ниже задержка (важно для стримов и звонков)\r\nБольше значение — выше качество на низких битрейтах, но растёт задержка");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 60);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(94, 13);
            label2.TabIndex = 3;
            label2.Text = "Битрейт (кбит/с):";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 14);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(154, 13);
            label1.TabIndex = 2;
            label1.Text = "Частота дискретизации (Hz):";
            // 
            // cmbOpusBitrate
            // 
            cmbOpusBitrate.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOpusBitrate.FormattingEnabled = true;
            cmbOpusBitrate.Items.AddRange(new object[] { "320", "256", "192", "160", "128", "96", "64", "56", "48", "40", "32" });
            cmbOpusBitrate.Location = new Point(18, 78);
            cmbOpusBitrate.Margin = new Padding(4, 3, 4, 3);
            cmbOpusBitrate.Name = "cmbOpusBitrate";
            cmbOpusBitrate.Size = new Size(163, 21);
            cmbOpusBitrate.TabIndex = 1;
            toolTip.SetToolTip(cmbOpusBitrate, resources.GetString("cmbOpusBitrate.ToolTip"));
            // 
            // cmbOpusFrequency
            // 
            cmbOpusFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOpusFrequency.Enabled = false;
            cmbOpusFrequency.FormattingEnabled = true;
            cmbOpusFrequency.Items.AddRange(new object[] { "48000", "44100", "32000", "22050", "16000", "11025", "8000" });
            cmbOpusFrequency.Location = new Point(18, 32);
            cmbOpusFrequency.Margin = new Padding(4, 3, 4, 3);
            cmbOpusFrequency.Name = "cmbOpusFrequency";
            cmbOpusFrequency.Size = new Size(163, 21);
            cmbOpusFrequency.TabIndex = 0;
            // 
            // qaacPage
            // 
            qaacPage.Controls.Add(cbQaacHe);
            qaacPage.Controls.Add(tbQaacVbr);
            qaacPage.Controls.Add(label25);
            qaacPage.Controls.Add(tbQaacQuality);
            qaacPage.Controls.Add(label24);
            qaacPage.Controls.Add(label23);
            qaacPage.Controls.Add(cmbQaacChannels);
            qaacPage.Controls.Add(label15);
            qaacPage.Controls.Add(cmbQaacMode);
            qaacPage.Controls.Add(label16);
            qaacPage.Controls.Add(label22);
            qaacPage.Controls.Add(cmbQaacBitrate);
            qaacPage.Controls.Add(cmbQaacFrequency);
            qaacPage.Location = new Point(4, 22);
            qaacPage.Margin = new Padding(4, 3, 4, 3);
            qaacPage.Name = "qaacPage";
            qaacPage.Size = new Size(416, 228);
            qaacPage.TabIndex = 4;
            qaacPage.Text = "QAAC";
            qaacPage.UseVisualStyleBackColor = true;
            // 
            // cbQaacHe
            // 
            cbQaacHe.AutoSize = true;
            cbQaacHe.Location = new Point(18, 156);
            cbQaacHe.Margin = new Padding(4, 3, 4, 3);
            cbQaacHe.Name = "cbQaacHe";
            cbQaacHe.Size = new Size(97, 17);
            cbQaacHe.TabIndex = 32;
            cbQaacHe.Text = "High Efficiency";
            toolTip.SetToolTip(cbQaacHe, "Режим повышенной эффективности для низких битрейтов (хорошо для стриминга)");
            cbQaacHe.UseVisualStyleBackColor = true;
            cbQaacHe.CheckedChanged += cbQaacHe_CheckedChanged;
            // 
            // tbQaacVbr
            // 
            tbQaacVbr.AutoSize = false;
            tbQaacVbr.BackColor = Color.Snow;
            tbQaacVbr.LargeChange = 1;
            tbQaacVbr.Location = new Point(227, 78);
            tbQaacVbr.Margin = new Padding(4, 3, 4, 3);
            tbQaacVbr.Maximum = 127;
            tbQaacVbr.Name = "tbQaacVbr";
            tbQaacVbr.Size = new Size(163, 24);
            tbQaacVbr.TabIndex = 31;
            tbQaacVbr.TickStyle = TickStyle.None;
            toolTip.SetToolTip(tbQaacVbr, resources.GetString("tbQaacVbr.ToolTip"));
            tbQaacVbr.Value = 90;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(224, 60);
            label25.Margin = new Padding(4, 0, 4, 0);
            label25.Name = "label25";
            label25.Size = new Size(142, 13);
            label25.TabIndex = 30;
            label25.Text = "Качество VBR (мин/макс):";
            // 
            // tbQaacQuality
            // 
            tbQaacQuality.AutoSize = false;
            tbQaacQuality.BackColor = Color.Snow;
            tbQaacQuality.LargeChange = 1;
            tbQaacQuality.Location = new Point(227, 125);
            tbQaacQuality.Margin = new Padding(4, 3, 4, 3);
            tbQaacQuality.Maximum = 2;
            tbQaacQuality.Name = "tbQaacQuality";
            tbQaacQuality.Size = new Size(163, 24);
            tbQaacQuality.TabIndex = 29;
            toolTip.SetToolTip(tbQaacQuality, "Баланс скорости кодирования и качества (0-2)");
            tbQaacQuality.Value = 2;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(224, 106);
            label24.Margin = new Padding(4, 0, 4, 0);
            label24.Name = "label24";
            label24.Size = new Size(110, 13);
            label24.TabIndex = 28;
            label24.Text = "Скорость/Качество:";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(224, 14);
            label23.Margin = new Padding(4, 0, 4, 0);
            label23.Name = "label23";
            label23.Size = new Size(90, 13);
            label23.TabIndex = 27;
            label23.Text = "Режим каналов:";
            // 
            // cmbQaacChannels
            // 
            cmbQaacChannels.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQaacChannels.FormattingEnabled = true;
            cmbQaacChannels.Items.AddRange(new object[] { "Stereo", "Mono" });
            cmbQaacChannels.Location = new Point(227, 32);
            cmbQaacChannels.Margin = new Padding(4, 3, 4, 3);
            cmbQaacChannels.Name = "cmbQaacChannels";
            cmbQaacChannels.Size = new Size(163, 21);
            cmbQaacChannels.TabIndex = 26;
            toolTip.SetToolTip(cmbQaacChannels, "Как обрабатывать стерео.\r\nStereo - полноценное стерео\r\nMono - смешивает в один канал (экономит место)");
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(14, 106);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(78, 13);
            label15.TabIndex = 25;
            label15.Text = "Тип битрейта:";
            // 
            // cmbQaacMode
            // 
            cmbQaacMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQaacMode.FormattingEnabled = true;
            cmbQaacMode.Items.AddRange(new object[] { "CBR", "ABR", "CVBR", "VBR" });
            cmbQaacMode.Location = new Point(18, 125);
            cmbQaacMode.Margin = new Padding(4, 3, 4, 3);
            cmbQaacMode.Name = "cmbQaacMode";
            cmbQaacMode.Size = new Size(163, 21);
            cmbQaacMode.TabIndex = 24;
            toolTip.SetToolTip(cmbQaacMode, "CBR - постоянный битрейт\nABR - средний битрейт\nVBR - переменный битрейт");
            cmbQaacMode.SelectedIndexChanged += cmbQaacMode_SelectedIndexChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(14, 60);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(94, 13);
            label16.TabIndex = 23;
            label16.Text = "Битрейт (кбит/с):";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(14, 14);
            label22.Margin = new Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new Size(154, 13);
            label22.TabIndex = 22;
            label22.Text = "Частота дискретизации (Hz):";
            // 
            // cmbQaacBitrate
            // 
            cmbQaacBitrate.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQaacBitrate.FormattingEnabled = true;
            cmbQaacBitrate.Items.AddRange(new object[] { "320", "256", "192", "160", "128", "96", "64", "56", "48", "40", "32" });
            cmbQaacBitrate.Location = new Point(18, 78);
            cmbQaacBitrate.Margin = new Padding(4, 3, 4, 3);
            cmbQaacBitrate.Name = "cmbQaacBitrate";
            cmbQaacBitrate.Size = new Size(163, 21);
            cmbQaacBitrate.TabIndex = 21;
            toolTip.SetToolTip(cmbQaacBitrate, resources.GetString("cmbQaacBitrate.ToolTip"));
            // 
            // cmbQaacFrequency
            // 
            cmbQaacFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQaacFrequency.FormattingEnabled = true;
            cmbQaacFrequency.Items.AddRange(new object[] { "48000", "44100", "32000", "22050", "16000", "11025", "8000" });
            cmbQaacFrequency.Location = new Point(18, 32);
            cmbQaacFrequency.Margin = new Padding(4, 3, 4, 3);
            cmbQaacFrequency.Name = "cmbQaacFrequency";
            cmbQaacFrequency.Size = new Size(163, 21);
            cmbQaacFrequency.TabIndex = 20;
            toolTip.SetToolTip(cmbQaacFrequency, "Чем выше частота, тем лучше передаются высокие частоты.\r\nСтандарт для музыки - 44100");
            // 
            // flacPage
            // 
            flacPage.Controls.Add(lblLossyWavInfo);
            flacPage.Controls.Add(label18);
            flacPage.Controls.Add(cmbLossyWavQuality);
            flacPage.Controls.Add(label17);
            flacPage.Controls.Add(cmbFlacBitPerSample);
            flacPage.Controls.Add(cbLossyWav);
            flacPage.Controls.Add(label14);
            flacPage.Controls.Add(tbFlacCompress);
            flacPage.Controls.Add(label13);
            flacPage.Controls.Add(cmbFlacFrequency);
            flacPage.Location = new Point(4, 22);
            flacPage.Margin = new Padding(4, 3, 4, 3);
            flacPage.Name = "flacPage";
            flacPage.Size = new Size(416, 228);
            flacPage.TabIndex = 2;
            flacPage.Text = "FLAC";
            flacPage.UseVisualStyleBackColor = true;
            // 
            // lblLossyWavInfo
            // 
            lblLossyWavInfo.AutoSize = true;
            lblLossyWavInfo.Font = new Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblLossyWavInfo.ForeColor = Color.Gray;
            lblLossyWavInfo.Location = new Point(225, 118);
            lblLossyWavInfo.Margin = new Padding(4, 0, 4, 0);
            lblLossyWavInfo.Name = "lblLossyWavInfo";
            lblLossyWavInfo.Size = new Size(150, 26);
            lblLossyWavInfo.TabIndex = 31;
            lblLossyWavInfo.Text = "LossyWAV добавляет сжатие\r\nс потерями";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(224, 14);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new Size(112, 13);
            label18.TabIndex = 28;
            label18.Text = "Качество LossyWAV:";
            // 
            // cmbLossyWavQuality
            // 
            cmbLossyWavQuality.AutoCompleteCustomSource.AddRange(new string[] { "32", "24", "16", "8" });
            cmbLossyWavQuality.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLossyWavQuality.Enabled = false;
            cmbLossyWavQuality.FormattingEnabled = true;
            cmbLossyWavQuality.Items.AddRange(new object[] { "Наивысшее", "Высокое", "Повышенное", "Стандартное", "Среднее", "Портативное", "Низкое" });
            cmbLossyWavQuality.Location = new Point(227, 32);
            cmbLossyWavQuality.Margin = new Padding(4, 3, 4, 3);
            cmbLossyWavQuality.Name = "cmbLossyWavQuality";
            cmbLossyWavQuality.Size = new Size(163, 21);
            cmbLossyWavQuality.TabIndex = 27;
            toolTip.SetToolTip(cmbLossyWavQuality, "Степень сжатия с потерями\r\nНаивысшее - минимальные потери\r\nНизкое - сильное сжатие");
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(14, 60);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(102, 13);
            label17.TabIndex = 26;
            label17.Text = "Разрядность (бит):";
            // 
            // cmbFlacBitPerSample
            // 
            cmbFlacBitPerSample.AutoCompleteCustomSource.AddRange(new string[] { "32", "24", "16", "8" });
            cmbFlacBitPerSample.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFlacBitPerSample.FormattingEnabled = true;
            cmbFlacBitPerSample.Items.AddRange(new object[] { "32", "24", "16", "8" });
            cmbFlacBitPerSample.Location = new Point(18, 78);
            cmbFlacBitPerSample.Margin = new Padding(4, 3, 4, 3);
            cmbFlacBitPerSample.Name = "cmbFlacBitPerSample";
            cmbFlacBitPerSample.Size = new Size(163, 21);
            cmbFlacBitPerSample.TabIndex = 25;
            toolTip.SetToolTip(cmbFlacBitPerSample, "Точность передачи динамического диапазона.\r\n16 бит - стандарт для CD\r\n24 бит - для студийных записей");
            // 
            // cbLossyWav
            // 
            cbLossyWav.AutoSize = true;
            cbLossyWav.Location = new Point(227, 76);
            cbLossyWav.Margin = new Padding(4, 3, 4, 3);
            cbLossyWav.Name = "cbLossyWav";
            cbLossyWav.Size = new Size(117, 17);
            cbLossyWav.TabIndex = 24;
            cbLossyWav.Text = "Сжатие LossyWav";
            toolTip.SetToolTip(cbLossyWav, "Дополнительное сжатие с небольшими потерями,\r\nчтобы уменьшить размер файла");
            cbLossyWav.UseVisualStyleBackColor = true;
            cbLossyWav.CheckedChanged += cbLossyWav_CheckedChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(14, 106);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(154, 13);
            label14.TabIndex = 21;
            label14.Text = "Уровень сжатия (мин/макс):";
            // 
            // tbFlacCompress
            // 
            tbFlacCompress.AutoSize = false;
            tbFlacCompress.BackColor = Color.Snow;
            tbFlacCompress.LargeChange = 1;
            tbFlacCompress.Location = new Point(18, 125);
            tbFlacCompress.Margin = new Padding(4, 3, 4, 3);
            tbFlacCompress.Maximum = 8;
            tbFlacCompress.Name = "tbFlacCompress";
            tbFlacCompress.Size = new Size(163, 24);
            tbFlacCompress.TabIndex = 20;
            toolTip.SetToolTip(tbFlacCompress, "0 - минимальное сжатие (быстро)\r\n8 - максимальное сжатие (медленнее, но файл меньше)\r\nКачество звука НЕ меняется!");
            tbFlacCompress.Value = 5;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(14, 14);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(154, 13);
            label13.TabIndex = 18;
            label13.Text = "Частота дискретизации (Hz):";
            // 
            // cmbFlacFrequency
            // 
            cmbFlacFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFlacFrequency.Enabled = false;
            cmbFlacFrequency.FormattingEnabled = true;
            cmbFlacFrequency.Items.AddRange(new object[] { "48000", "44100", "32000", "22050", "16000", "11025", "8000" });
            cmbFlacFrequency.Location = new Point(18, 32);
            cmbFlacFrequency.Margin = new Padding(4, 3, 4, 3);
            cmbFlacFrequency.Name = "cmbFlacFrequency";
            cmbFlacFrequency.Size = new Size(163, 21);
            cmbFlacFrequency.TabIndex = 17;
            // 
            // wavPage
            // 
            wavPage.Controls.Add(label21);
            wavPage.Controls.Add(cmbWaveChannels);
            wavPage.Controls.Add(label20);
            wavPage.Controls.Add(cmbWaveBitPerSample);
            wavPage.Controls.Add(label19);
            wavPage.Controls.Add(cmbWaveFrequency);
            wavPage.Location = new Point(4, 22);
            wavPage.Margin = new Padding(4, 3, 4, 3);
            wavPage.Name = "wavPage";
            wavPage.Size = new Size(416, 228);
            wavPage.TabIndex = 3;
            wavPage.Text = "WAV";
            wavPage.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(14, 106);
            label21.Margin = new Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new Size(90, 13);
            label21.TabIndex = 30;
            label21.Text = "Режим каналов:";
            // 
            // cmbWaveChannels
            // 
            cmbWaveChannels.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWaveChannels.FormattingEnabled = true;
            cmbWaveChannels.Items.AddRange(new object[] { "Stereo", "Mono" });
            cmbWaveChannels.Location = new Point(18, 125);
            cmbWaveChannels.Margin = new Padding(4, 3, 4, 3);
            cmbWaveChannels.Name = "cmbWaveChannels";
            cmbWaveChannels.Size = new Size(163, 21);
            cmbWaveChannels.TabIndex = 29;
            toolTip.SetToolTip(cmbWaveChannels, "Как обрабатывать стерео.\r\nStereo - полноценное стерео\r\nMono - смешивает в один канал (экономит место)");
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(14, 60);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(102, 13);
            label20.TabIndex = 28;
            label20.Text = "Разрядность (бит):";
            // 
            // cmbWaveBitPerSample
            // 
            cmbWaveBitPerSample.AutoCompleteCustomSource.AddRange(new string[] { "32", "24", "16", "8" });
            cmbWaveBitPerSample.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWaveBitPerSample.FormattingEnabled = true;
            cmbWaveBitPerSample.Items.AddRange(new object[] { "32", "24", "16", "8" });
            cmbWaveBitPerSample.Location = new Point(18, 78);
            cmbWaveBitPerSample.Margin = new Padding(4, 3, 4, 3);
            cmbWaveBitPerSample.Name = "cmbWaveBitPerSample";
            cmbWaveBitPerSample.Size = new Size(163, 21);
            cmbWaveBitPerSample.TabIndex = 27;
            toolTip.SetToolTip(cmbWaveBitPerSample, "Точность звука.\r\n16 бит - стандарт\r\n24 бит - для профессиональной работы");
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(14, 14);
            label19.Margin = new Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new Size(154, 13);
            label19.TabIndex = 18;
            label19.Text = "Частота дискретизации (Hz):";
            // 
            // cmbWaveFrequency
            // 
            cmbWaveFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWaveFrequency.FormattingEnabled = true;
            cmbWaveFrequency.Items.AddRange(new object[] { "48000", "44100", "32000", "22050", "16000", "11025", "8000" });
            cmbWaveFrequency.Location = new Point(18, 32);
            cmbWaveFrequency.Margin = new Padding(4, 3, 4, 3);
            cmbWaveFrequency.Name = "cmbWaveFrequency";
            cmbWaveFrequency.Size = new Size(163, 21);
            cmbWaveFrequency.TabIndex = 17;
            toolTip.SetToolTip(cmbWaveFrequency, "Чем выше частота, тем лучше передаются высокие частоты.\r\nСтандарт для музыки - 44100");
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.DialogResult = DialogResult.OK;
            btnSave.Location = new Point(331, 261);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 1;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(237, 261);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 100;
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.ToolTipTitle = "Подсказка";
            // 
            // UserPresetForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(433, 297);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "UserPresetForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Пользовательские настройки энкодера";
            tabControl1.ResumeLayout(false);
            mp3Page.ResumeLayout(false);
            mp3Page.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbMp3Quality).EndInit();
            opusPage.ResumeLayout(false);
            opusPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbOpusQuality).EndInit();
            qaacPage.ResumeLayout(false);
            qaacPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbQaacVbr).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbQaacQuality).EndInit();
            flacPage.ResumeLayout(false);
            flacPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbFlacCompress).EndInit();
            wavPage.ResumeLayout(false);
            wavPage.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mp3Page;
        private System.Windows.Forms.TabPage opusPage;
        private System.Windows.Forms.TabPage flacPage;
        private System.Windows.Forms.TabPage wavPage;

        // Opus controls
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbOpusChannels;
        private System.Windows.Forms.TrackBar tbOpusQuality;

        // MP3 controls
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbMp3Channels;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbMp3Mode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbMp3Bitrate;
        private System.Windows.Forms.ComboBox cmbMp3Frequency;
        private System.Windows.Forms.TrackBar tbMp3Quality;

        // FLAC controls
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbFlacFrequency;
        private System.Windows.Forms.TrackBar tbFlacCompress;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox cbLossyWav;
        private System.Windows.Forms.ComboBox cmbFlacBitPerSample;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbLossyWavQuality;
        private System.Windows.Forms.Label lblLossyWavInfo;

        // WAV controls
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbWaveFrequency;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cmbWaveChannels;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmbWaveBitPerSample;

        // Buttons
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        // ToolTip
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage qaacPage;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbQaacMode;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbQaacBitrate;
        private System.Windows.Forms.ComboBox cmbQaacFrequency;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cmbQaacChannels;
        private System.Windows.Forms.TrackBar tbQaacQuality;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TrackBar tbQaacVbr;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.CheckBox cbQaacHe;
    }
}