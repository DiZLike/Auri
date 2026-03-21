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
            this._stepPanel = new System.Windows.Forms.Panel();
            this._btnNext = new System.Windows.Forms.Button();
            this._btnBack = new System.Windows.Forms.Button();
            this._btnFinish = new System.Windows.Forms.Button();
            this._lblTitle = new System.Windows.Forms.Label();
            this._lblSubtitle = new System.Windows.Forms.Label();
            this._lblStepInfo = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // _stepPanel
            this._stepPanel.BackColor = System.Drawing.Color.White;
            this._stepPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._stepPanel.Location = new System.Drawing.Point(20, 125);
            this._stepPanel.Size = new System.Drawing.Size(500, 220);

            // _btnBack
            this._btnBack.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this._btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnBack.Location = new System.Drawing.Point(20, 365);
            this._btnBack.Size = new System.Drawing.Size(100, 35);
            this._btnBack.Text = "← Назад";
            this._btnBack.UseVisualStyleBackColor = false;

            // _btnNext
            this._btnNext.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this._btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnNext.ForeColor = System.Drawing.Color.White;
            this._btnNext.Location = new System.Drawing.Point(310, 365);
            this._btnNext.Size = new System.Drawing.Size(100, 35);
            this._btnNext.Text = "Далее →";
            this._btnNext.UseVisualStyleBackColor = false;

            // _btnFinish
            this._btnFinish.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this._btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnFinish.ForeColor = System.Drawing.Color.White;
            this._btnFinish.Location = new System.Drawing.Point(420, 365);
            this._btnFinish.Size = new System.Drawing.Size(100, 35);
            this._btnFinish.Text = "Завершить";
            this._btnFinish.UseVisualStyleBackColor = false;
            this._btnFinish.Visible = false;

            // _lblTitle
            this._lblTitle.BackColor = System.Drawing.Color.White;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._lblTitle.Location = new System.Drawing.Point(20, 20);
            this._lblTitle.Size = new System.Drawing.Size(500, 40);
            this._lblTitle.Text = "Добро пожаловать в Auri!";

            // _lblSubtitle
            this._lblSubtitle.BackColor = System.Drawing.Color.White;
            this._lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this._lblSubtitle.Location = new System.Drawing.Point(20, 65);
            this._lblSubtitle.Size = new System.Drawing.Size(500, 25);
            this._lblSubtitle.Text = "Давайте настроим конвертацию под ваши нужды";

            // _lblStepInfo
            this._lblStepInfo.BackColor = System.Drawing.Color.White;
            this._lblStepInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._lblStepInfo.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this._lblStepInfo.Location = new System.Drawing.Point(20, 95);
            this._lblStepInfo.Size = new System.Drawing.Size(500, 25);

            // QuickStartWizardForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(550, 450);
            this.Controls.Add(this._lblTitle);
            this.Controls.Add(this._lblSubtitle);
            this.Controls.Add(this._lblStepInfo);
            this.Controls.Add(this._stepPanel);
            this.Controls.Add(this._btnBack);
            this.Controls.Add(this._btnNext);
            this.Controls.Add(this._btnFinish);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мастер быстрого старта";

            this.ResumeLayout(false);
        }

        // UI элементы
        private System.Windows.Forms.Panel _stepPanel;
        private System.Windows.Forms.Button _btnNext;
        private System.Windows.Forms.Button _btnBack;
        private System.Windows.Forms.Button _btnFinish;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.Label _lblSubtitle;
        private System.Windows.Forms.Label _lblStepInfo;

        // Элементы управления для шагов (объявлены как protected для доступа из логики)
        protected System.Windows.Forms.RadioButton _rbStreaming;
        protected System.Windows.Forms.RadioButton _rbMusicLibrary;
        protected System.Windows.Forms.RadioButton _rbArchive;
        protected System.Windows.Forms.RadioButton _rbUnknown;

        protected System.Windows.Forms.RadioButton _rbBestQuality;
        protected System.Windows.Forms.RadioButton _rbGoodBalance;
        protected System.Windows.Forms.RadioButton _rbSmallSize;
        protected System.Windows.Forms.RadioButton _rbUnknownPreference;

        protected System.Windows.Forms.RadioButton _rbMobileDevice;
        protected System.Windows.Forms.RadioButton _rbPCPlayer;
        protected System.Windows.Forms.RadioButton _rbHomeAudio;
        protected System.Windows.Forms.RadioButton _rbUnknownDevice;
    }
}