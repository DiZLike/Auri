using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auri.Managers
{
    public class UserEncSetManager
    {
        private string confPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encoders");
        public UserEncSetManager() 
        {
            if (!Directory.Exists(confPath))
                Directory.CreateDirectory(confPath);
        }
        public void SaveConf(string format, object conf)
        {
            UserOpus opus = conf as UserOpus;
            JsonDataManager.SaveToJson<UserOpus>(opus, "123.json");
        }
        public void LoadConf(string format)
        {

        }
    }

    [Serializable]
    public class UserOpus
    {
        [JsonProperty("bitrate")]
        public int Bitrate { get; set; } = 128;
        [JsonProperty("framesize")]
        public float Framesize { get; set; } = 40;
        [JsonProperty("mode")]
        public string Mode { get; set; } = "vbr";
        [JsonProperty("content")]
        public string Content { get; set; } = "music";
        [JsonProperty("comp")]
        public int Comp { get; set; } = 10;
    }

}
