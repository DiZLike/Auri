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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mp3Page = new System.Windows.Forms.TabPage();
            this.tbMp3Quality = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbMp3Channels = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbMp3Mode = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbMp3Bitrate = new System.Windows.Forms.ComboBox();
            this.cmbMp3Frequency = new System.Windows.Forms.ComboBox();
            this.opusPage = new System.Windows.Forms.TabPage();
            this.tbOpusQuality = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbOpusChannels = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbOpusContent = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbOpusMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbOpusFramesize = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbOpusBitrate = new System.Windows.Forms.ComboBox();
            this.cmbOpusFrequency = new System.Windows.Forms.ComboBox();
            this.flacPage = new System.Windows.Forms.TabPage();
            this.lblLossyWavInfo = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbLossyWavQuality = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbFlacBitPerSample = new System.Windows.Forms.ComboBox();
            this.cbLossyWav = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbFlacCompress = new System.Windows.Forms.TrackBar();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbFlacFrequency = new System.Windows.Forms.ComboBox();
            this.wavPage = new System.Windows.Forms.TabPage();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbWaveChannels = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbWaveBitPerSample = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbWaveFrequency = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.mp3Page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMp3Quality)).BeginInit();
            this.opusPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpusQuality)).BeginInit();
            this.flacPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbFlacCompress)).BeginInit();
            this.wavPage.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.wavPage);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(363, 220);
            this.tabControl1.TabIndex = 0;
            // 
            // mp3Page
            // 
            this.mp3Page.Controls.Add(this.tbMp3Quality);
            this.mp3Page.Controls.Add(this.label8);
            this.mp3Page.Controls.Add(this.cmbMp3Channels);
            this.mp3Page.Controls.Add(this.label9);
            this.mp3Page.Controls.Add(this.label10);
            this.mp3Page.Controls.Add(this.cmbMp3Mode);
            this.mp3Page.Controls.Add(this.label11);
            this.mp3Page.Controls.Add(this.label12);
            this.mp3Page.Controls.Add(this.cmbMp3Bitrate);
            this.mp3Page.Controls.Add(this.cmbMp3Frequency);
            this.mp3Page.Location = new System.Drawing.Point(4, 22);
            this.mp3Page.Name = "mp3Page";
            this.mp3Page.Padding = new System.Windows.Forms.Padding(3);
            this.mp3Page.Size = new System.Drawing.Size(384, 194);
            this.mp3Page.TabIndex = 1;
            this.mp3Page.Text = "MP3";
            this.mp3Page.UseVisualStyleBackColor = true;
            // 
            // tbMp3Quality
            // 
            this.tbMp3Quality.AutoSize = false;
            this.tbMp3Quality.BackColor = System.Drawing.Color.Snow;
            this.tbMp3Quality.LargeChange = 1;
            this.tbMp3Quality.Location = new System.Drawing.Point(195, 105);
            this.tbMp3Quality.Maximum = 9;
            this.tbMp3Quality.Name = "tbMp3Quality";
            this.tbMp3Quality.Size = new System.Drawing.Size(140, 21);
            this.tbMp3Quality.TabIndex = 24;
            this.toolTip.SetToolTip(this.tbMp3Quality, "0 - наилучшее качество (медленнее)\n9 - максимальная скорость (ниже качество)");
            this.tbMp3Quality.Value = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(192, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Режим каналов:";
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
            this.cmbMp3Channels.Location = new System.Drawing.Point(195, 28);
            this.cmbMp3Channels.Name = "cmbMp3Channels";
            this.cmbMp3Channels.Size = new System.Drawing.Size(140, 21);
            this.cmbMp3Channels.TabIndex = 22;
            this.toolTip.SetToolTip(this.cmbMp3Channels, "Режим объединения каналов");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(192, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Качество/Скорость:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Тип битрейта:";
            // 
            // cmbMp3Mode
            // 
            this.cmbMp3Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMp3Mode.FormattingEnabled = true;
            this.cmbMp3Mode.Items.AddRange(new object[] {
            "CBR",
            "ABR",
            "VBR"});
            this.cmbMp3Mode.Location = new System.Drawing.Point(15, 108);
            this.cmbMp3Mode.Name = "cmbMp3Mode";
            this.cmbMp3Mode.Size = new System.Drawing.Size(140, 21);
            this.cmbMp3Mode.TabIndex = 18;
            this.toolTip.SetToolTip(this.cmbMp3Mode, "CBR - постоянный битрейт\nABR - средний битрейт\nVBR - переменный битрейт");
            this.cmbMp3Mode.SelectedIndexChanged += new System.EventHandler(this.cmbMp3Mode_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Битрейт (кбит/с):";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(154, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Частота дискретизации (Hz):";
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
            this.cmbMp3Bitrate.Location = new System.Drawing.Point(15, 68);
            this.cmbMp3Bitrate.Name = "cmbMp3Bitrate";
            this.cmbMp3Bitrate.Size = new System.Drawing.Size(140, 21);
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
            this.cmbMp3Frequency.Location = new System.Drawing.Point(15, 28);
            this.cmbMp3Frequency.Name = "cmbMp3Frequency";
            this.cmbMp3Frequency.Size = new System.Drawing.Size(140, 21);
            this.cmbMp3Frequency.TabIndex = 14;
            // 
            // opusPage
            // 
            this.opusPage.Controls.Add(this.tbOpusQuality);
            this.opusPage.Controls.Add(this.label7);
            this.opusPage.Controls.Add(this.cmbOpusChannels);
            this.opusPage.Controls.Add(this.label6);
            this.opusPage.Controls.Add(this.cmbOpusContent);
            this.opusPage.Controls.Add(this.label5);
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
            this.opusPage.Size = new System.Drawing.Size(355, 194);
            this.opusPage.TabIndex = 0;
            this.opusPage.Text = "Opus";
            this.opusPage.UseVisualStyleBackColor = true;
            // 
            // tbOpusQuality
            // 
            this.tbOpusQuality.AutoSize = false;
            this.tbOpusQuality.BackColor = System.Drawing.Color.Snow;
            this.tbOpusQuality.LargeChange = 1;
            this.tbOpusQuality.Location = new System.Drawing.Point(195, 105);
            this.tbOpusQuality.Maximum = 9;
            this.tbOpusQuality.Name = "tbOpusQuality";
            this.tbOpusQuality.Size = new System.Drawing.Size(140, 21);
            this.tbOpusQuality.TabIndex = 25;
            this.tbOpusQuality.Value = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(192, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Режим каналов:";
            // 
            // cmbOpusChannels
            // 
            this.cmbOpusChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpusChannels.FormattingEnabled = true;
            this.cmbOpusChannels.Items.AddRange(new object[] {
            "Stereo",
            "Mono"});
            this.cmbOpusChannels.Location = new System.Drawing.Point(195, 28);
            this.cmbOpusChannels.Name = "cmbOpusChannels";
            this.cmbOpusChannels.Size = new System.Drawing.Size(140, 21);
            this.cmbOpusChannels.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Оптимизировать для:";
            // 
            // cmbOpusContent
            // 
            this.cmbOpusContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpusContent.FormattingEnabled = true;
            this.cmbOpusContent.Items.AddRange(new object[] {
            "Music",
            "Speech"});
            this.cmbOpusContent.Location = new System.Drawing.Point(195, 148);
            this.cmbOpusContent.Name = "cmbOpusContent";
            this.cmbOpusContent.Size = new System.Drawing.Size(140, 21);
            this.cmbOpusContent.TabIndex = 10;
            this.toolTip.SetToolTip(this.cmbOpusContent, "Оптимизация кодека под тип контента");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Скорость/Качество:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Тип битрейта:";
            // 
            // cmbOpusMode
            // 
            this.cmbOpusMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpusMode.FormattingEnabled = true;
            this.cmbOpusMode.Items.AddRange(new object[] {
            "VBR",
            "CVBR",
            "HARD-CBR"});
            this.cmbOpusMode.Location = new System.Drawing.Point(15, 108);
            this.cmbOpusMode.Name = "cmbOpusMode";
            this.cmbOpusMode.Size = new System.Drawing.Size(140, 21);
            this.cmbOpusMode.TabIndex = 6;
            this.toolTip.SetToolTip(this.cmbOpusMode, "Режим управления битрейтом");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Размер кадра (мс):";
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
            this.cmbOpusFramesize.Location = new System.Drawing.Point(15, 148);
            this.cmbOpusFramesize.Name = "cmbOpusFramesize";
            this.cmbOpusFramesize.Size = new System.Drawing.Size(140, 21);
            this.cmbOpusFramesize.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Битрейт (кбит/с):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Частота дискретизации (Hz):";
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
            this.cmbOpusBitrate.Location = new System.Drawing.Point(15, 68);
            this.cmbOpusBitrate.Name = "cmbOpusBitrate";
            this.cmbOpusBitrate.Size = new System.Drawing.Size(140, 21);
            this.cmbOpusBitrate.TabIndex = 1;
            // 
            // cmbOpusFrequency
            // 
            this.cmbOpusFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cmbOpusFrequency.Location = new System.Drawing.Point(15, 28);
            this.cmbOpusFrequency.Name = "cmbOpusFrequency";
            this.cmbOpusFrequency.Size = new System.Drawing.Size(140, 21);
            this.cmbOpusFrequency.TabIndex = 0;
            // 
            // flacPage
            // 
            this.flacPage.Controls.Add(this.lblLossyWavInfo);
            this.flacPage.Controls.Add(this.label18);
            this.flacPage.Controls.Add(this.cmbLossyWavQuality);
            this.flacPage.Controls.Add(this.label17);
            this.flacPage.Controls.Add(this.cmbFlacBitPerSample);
            this.flacPage.Controls.Add(this.cbLossyWav);
            this.flacPage.Controls.Add(this.label14);
            this.flacPage.Controls.Add(this.tbFlacCompress);
            this.flacPage.Controls.Add(this.label13);
            this.flacPage.Controls.Add(this.cmbFlacFrequency);
            this.flacPage.Location = new System.Drawing.Point(4, 22);
            this.flacPage.Name = "flacPage";
            this.flacPage.Size = new System.Drawing.Size(355, 194);
            this.flacPage.TabIndex = 2;
            this.flacPage.Text = "FLAC";
            this.flacPage.UseVisualStyleBackColor = true;
            // 
            // lblLossyWavInfo
            // 
            this.lblLossyWavInfo.AutoSize = true;
            this.lblLossyWavInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLossyWavInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblLossyWavInfo.Location = new System.Drawing.Point(193, 102);
            this.lblLossyWavInfo.Name = "lblLossyWavInfo";
            this.lblLossyWavInfo.Size = new System.Drawing.Size(150, 26);
            this.lblLossyWavInfo.TabIndex = 31;
            this.lblLossyWavInfo.Text = "LossyWAV добавляет сжатие\r\nс потерями";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(192, 12);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(112, 13);
            this.label18.TabIndex = 28;
            this.label18.Text = "Качество LossyWAV:";
            // 
            // cmbLossyWavQuality
            // 
            this.cmbLossyWavQuality.AutoCompleteCustomSource.AddRange(new string[] {
            "32",
            "24",
            "16",
            "8"});
            this.cmbLossyWavQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLossyWavQuality.Enabled = false;
            this.cmbLossyWavQuality.FormattingEnabled = true;
            this.cmbLossyWavQuality.Items.AddRange(new object[] {
            "Наивысшее",
            "Высокое",
            "Повышенное",
            "Стандартное",
            "Среднее",
            "Портативное",
            "Низкое"});
            this.cmbLossyWavQuality.Location = new System.Drawing.Point(195, 28);
            this.cmbLossyWavQuality.Name = "cmbLossyWavQuality";
            this.cmbLossyWavQuality.Size = new System.Drawing.Size(140, 21);
            this.cmbLossyWavQuality.TabIndex = 27;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(102, 13);
            this.label17.TabIndex = 26;
            this.label17.Text = "Разрядность (бит):";
            // 
            // cmbFlacBitPerSample
            // 
            this.cmbFlacBitPerSample.AutoCompleteCustomSource.AddRange(new string[] {
            "32",
            "24",
            "16",
            "8"});
            this.cmbFlacBitPerSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFlacBitPerSample.FormattingEnabled = true;
            this.cmbFlacBitPerSample.Items.AddRange(new object[] {
            "32",
            "24",
            "16",
            "8"});
            this.cmbFlacBitPerSample.Location = new System.Drawing.Point(15, 68);
            this.cmbFlacBitPerSample.Name = "cmbFlacBitPerSample";
            this.cmbFlacBitPerSample.Size = new System.Drawing.Size(140, 21);
            this.cmbFlacBitPerSample.TabIndex = 25;
            // 
            // cbLossyWav
            // 
            this.cbLossyWav.AutoSize = true;
            this.cbLossyWav.Location = new System.Drawing.Point(195, 66);
            this.cbLossyWav.Name = "cbLossyWav";
            this.cbLossyWav.Size = new System.Drawing.Size(117, 17);
            this.cbLossyWav.TabIndex = 24;
            this.cbLossyWav.Text = "Сжатие LossyWav";
            this.toolTip.SetToolTip(this.cbLossyWav, "Предварительное сжатие с потерями\nдля уменьшения размера");
            this.cbLossyWav.UseVisualStyleBackColor = true;
            this.cbLossyWav.CheckedChanged += new System.EventHandler(this.cbLossyWav_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(154, 13);
            this.label14.TabIndex = 21;
            this.label14.Text = "Уровень сжатия (мин/макс):";
            // 
            // tbFlacCompress
            // 
            this.tbFlacCompress.AutoSize = false;
            this.tbFlacCompress.BackColor = System.Drawing.Color.Snow;
            this.tbFlacCompress.LargeChange = 1;
            this.tbFlacCompress.Location = new System.Drawing.Point(15, 108);
            this.tbFlacCompress.Maximum = 8;
            this.tbFlacCompress.Name = "tbFlacCompress";
            this.tbFlacCompress.Size = new System.Drawing.Size(140, 21);
            this.tbFlacCompress.TabIndex = 20;
            this.tbFlacCompress.Value = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 12);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(154, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "Частота дискретизации (Hz):";
            // 
            // cmbFlacFrequency
            // 
            this.cmbFlacFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFlacFrequency.Enabled = false;
            this.cmbFlacFrequency.FormattingEnabled = true;
            this.cmbFlacFrequency.Items.AddRange(new object[] {
            "48000",
            "44100",
            "32000",
            "22050",
            "16000",
            "11025",
            "8000"});
            this.cmbFlacFrequency.Location = new System.Drawing.Point(15, 28);
            this.cmbFlacFrequency.Name = "cmbFlacFrequency";
            this.cmbFlacFrequency.Size = new System.Drawing.Size(140, 21);
            this.cmbFlacFrequency.TabIndex = 17;
            // 
            // wavPage
            // 
            this.wavPage.Controls.Add(this.label21);
            this.wavPage.Controls.Add(this.cmbWaveChannels);
            this.wavPage.Controls.Add(this.label20);
            this.wavPage.Controls.Add(this.cmbWaveBitPerSample);
            this.wavPage.Controls.Add(this.label19);
            this.wavPage.Controls.Add(this.cmbWaveFrequency);
            this.wavPage.Location = new System.Drawing.Point(4, 22);
            this.wavPage.Name = "wavPage";
            this.wavPage.Size = new System.Drawing.Size(384, 194);
            this.wavPage.TabIndex = 3;
            this.wavPage.Text = "WAV";
            this.wavPage.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 92);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(90, 13);
            this.label21.TabIndex = 30;
            this.label21.Text = "Режим каналов:";
            // 
            // cmbWaveChannels
            // 
            this.cmbWaveChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaveChannels.FormattingEnabled = true;
            this.cmbWaveChannels.Items.AddRange(new object[] {
            "Stereo",
            "Mono"});
            this.cmbWaveChannels.Location = new System.Drawing.Point(15, 108);
            this.cmbWaveChannels.Name = "cmbWaveChannels";
            this.cmbWaveChannels.Size = new System.Drawing.Size(140, 21);
            this.cmbWaveChannels.TabIndex = 29;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 52);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(102, 13);
            this.label20.TabIndex = 28;
            this.label20.Text = "Разрядность (бит):";
            // 
            // cmbWaveBitPerSample
            // 
            this.cmbWaveBitPerSample.AutoCompleteCustomSource.AddRange(new string[] {
            "32",
            "24",
            "16",
            "8"});
            this.cmbWaveBitPerSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaveBitPerSample.FormattingEnabled = true;
            this.cmbWaveBitPerSample.Items.AddRange(new object[] {
            "32",
            "24",
            "16",
            "8"});
            this.cmbWaveBitPerSample.Location = new System.Drawing.Point(15, 68);
            this.cmbWaveBitPerSample.Name = "cmbWaveBitPerSample";
            this.cmbWaveBitPerSample.Size = new System.Drawing.Size(140, 21);
            this.cmbWaveBitPerSample.TabIndex = 27;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(154, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "Частота дискретизации (Hz):";
            // 
            // cmbWaveFrequency
            // 
            this.cmbWaveFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaveFrequency.FormattingEnabled = true;
            this.cmbWaveFrequency.Items.AddRange(new object[] {
            "48000",
            "44100",
            "32000",
            "22050",
            "16000",
            "11025",
            "8000"});
            this.cmbWaveFrequency.Location = new System.Drawing.Point(15, 28);
            this.cmbWaveFrequency.Name = "cmbWaveFrequency";
            this.cmbWaveFrequency.Size = new System.Drawing.Size(140, 21);
            this.cmbWaveFrequency.TabIndex = 17;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(284, 226);
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
            this.btnCancel.Location = new System.Drawing.Point(203, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(12, 226);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Сбросить";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 300;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Подсказка";
            // 
            // UserPresetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 257);
            this.Controls.Add(this.btnReset);
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
            ((System.ComponentModel.ISupportInitialize)(this.tbMp3Quality)).EndInit();
            this.opusPage.ResumeLayout(false);
            this.opusPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpusQuality)).EndInit();
            this.flacPage.ResumeLayout(false);
            this.flacPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbFlacCompress)).EndInit();
            this.wavPage.ResumeLayout(false);
            this.wavPage.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button btnReset;

        // ToolTip
        private System.Windows.Forms.ToolTip toolTip;
    }
}