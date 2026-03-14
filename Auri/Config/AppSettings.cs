using System;
using System.Windows.Forms;

namespace Auri.Config
{
    [Serializable]
    public class AppSettings
    {
        public FormSettings FormSettings { get; set; } = new FormSettings();
        
    }
    [Serializable]
    public class FormSettings
    {
        public int FormX { get; set; }
        public int FormY { get; set; }
        public FormWindowState WindowState { get; set; }
    }
}