namespace Auri.Forms.Wizard
{
    partial class QuickStartWizardForm
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
            _stepPanel = new Panel();
            _btnNext = new Button();
            _btnBack = new Button();
            _btnFinish = new Button();
            _lblTitle = new Label();
            _lblSubtitle = new Label();
            _lblStepInfo = new Label();
            SuspendLayout();
            // 
            // _stepPanel
            // 
            _stepPanel.BackColor = Color.White;
            _stepPanel.Location = new Point(20, 125);
            _stepPanel.Name = "_stepPanel";
            _stepPanel.Size = new Size(500, 340);
            _stepPanel.TabIndex = 3;
            // 
            // _btnNext
            // 
            _btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _btnNext.BackColor = Color.FromArgb(76, 175, 80);
            _btnNext.FlatStyle = FlatStyle.Flat;
            _btnNext.ForeColor = Color.White;
            _btnNext.Location = new Point(310, 471);
            _btnNext.Name = "_btnNext";
            _btnNext.Size = new Size(100, 35);
            _btnNext.TabIndex = 5;
            _btnNext.Text = "Далее →";
            _btnNext.UseVisualStyleBackColor = false;
            // 
            // _btnBack
            // 
            _btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _btnBack.BackColor = Color.FromArgb(240, 240, 240);
            _btnBack.FlatStyle = FlatStyle.Flat;
            _btnBack.Location = new Point(20, 471);
            _btnBack.Name = "_btnBack";
            _btnBack.Size = new Size(100, 35);
            _btnBack.TabIndex = 4;
            _btnBack.Text = "← Назад";
            _btnBack.UseVisualStyleBackColor = false;
            // 
            // _btnFinish
            // 
            _btnFinish.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _btnFinish.BackColor = Color.FromArgb(76, 175, 80);
            _btnFinish.FlatStyle = FlatStyle.Flat;
            _btnFinish.ForeColor = Color.White;
            _btnFinish.Location = new Point(420, 471);
            _btnFinish.Name = "_btnFinish";
            _btnFinish.Size = new Size(100, 35);
            _btnFinish.TabIndex = 6;
            _btnFinish.Text = "Завершить";
            _btnFinish.UseVisualStyleBackColor = false;
            _btnFinish.Visible = false;
            // 
            // _lblTitle
            // 
            _lblTitle.BackColor = Color.White;
            _lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            _lblTitle.Location = new Point(20, 20);
            _lblTitle.Name = "_lblTitle";
            _lblTitle.Size = new Size(500, 40);
            _lblTitle.TabIndex = 0;
            _lblTitle.Text = "Добро пожаловать в Auri!";
            // 
            // _lblSubtitle
            // 
            _lblSubtitle.BackColor = Color.White;
            _lblSubtitle.Font = new Font("Segoe UI", 10F);
            _lblSubtitle.ForeColor = Color.Gray;
            _lblSubtitle.Location = new Point(20, 65);
            _lblSubtitle.Name = "_lblSubtitle";
            _lblSubtitle.Size = new Size(500, 25);
            _lblSubtitle.TabIndex = 1;
            _lblSubtitle.Text = "Давайте настроим конвертацию под ваши нужды";
            // 
            // _lblStepInfo
            // 
            _lblStepInfo.BackColor = Color.White;
            _lblStepInfo.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            _lblStepInfo.ForeColor = Color.FromArgb(100, 100, 100);
            _lblStepInfo.Location = new Point(20, 95);
            _lblStepInfo.Name = "_lblStepInfo";
            _lblStepInfo.Size = new Size(500, 25);
            _lblStepInfo.TabIndex = 2;
            // 
            // QuickStartWizardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(544, 523);
            Controls.Add(_lblTitle);
            Controls.Add(_lblSubtitle);
            Controls.Add(_lblStepInfo);
            Controls.Add(_stepPanel);
            Controls.Add(_btnBack);
            Controls.Add(_btnNext);
            Controls.Add(_btnFinish);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "QuickStartWizardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Мастер быстрого старта";
            ResumeLayout(false);
        }

        // UI элементы
        private System.Windows.Forms.Panel _stepPanel;
        private System.Windows.Forms.Button _btnNext;
        private System.Windows.Forms.Button _btnBack;
        private System.Windows.Forms.Button _btnFinish;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.Label _lblSubtitle;
        private System.Windows.Forms.Label _lblStepInfo;

        // Элементы для шага 0 (использование)
        protected System.Windows.Forms.RadioButton _rbMobileDevice;
        protected System.Windows.Forms.RadioButton _rbPCPlayer;
        protected System.Windows.Forms.RadioButton _rbHomeAudio;
        protected System.Windows.Forms.RadioButton _rbAppleDevice;
        protected System.Windows.Forms.RadioButton _rbMaxCompatibility;

        // Элементы для шага 1 (качество) - без изменений
        protected System.Windows.Forms.RadioButton _rbBestQuality;
        protected System.Windows.Forms.RadioButton _rbGoodBalance;
        protected System.Windows.Forms.RadioButton _rbSmallSize;

        // Элементы для шага 2 (особые требования)
        protected System.Windows.Forms.RadioButton _rbStreaming;
        protected System.Windows.Forms.RadioButton _rbNoSpecial;
        protected System.Windows.Forms.RadioButton _rbHighEfficiency;
    }
}