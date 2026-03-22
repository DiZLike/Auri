// QuickStartWizard.cs
using Auri.Audio.Encoder;
using Auri.Forms.Wizard;
using Auri.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Auri.Wizard
{
    public class QuickStartWizard
    {
        private readonly ConfigManager _config;
        private QuickStartResult _result;

        public QuickStartWizard(ConfigManager config)
        {
            _config = config;
        }

        public bool NeedsSetup()
        {
            // Проверяем, был ли уже выполнен мастер быстрого старта
            return !_config.Settings.ConverterSettings.QuickStartCompleted;
        }

        public QuickStartResult ShowWizard()
        {
            using (var wizardForm = new QuickStartWizardForm())
            {
                var dialogResult = wizardForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    _result = wizardForm.Result;
                    _config.Settings.ConverterSettings.QuickStartCompleted = true;
                    _config.Settings.ConverterSettings.QuickStartFormat = _result.Format;
                    _config.Settings.ConverterSettings.QuickStartPreset = _result.Preset;
                    _config.SaveSettings();
                    return _result;
                }
            }
            return null;
        }

        public EncoderPreset GetQuickConvertPreset()
        {
            string format = _config.Settings.ConverterSettings.QuickStartFormat;
            if (string.IsNullOrEmpty(format))
                return null;

            return _config.GetQuickStartPreset(format);
        }
    }
}