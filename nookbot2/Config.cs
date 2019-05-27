using System.IO;
using Newtonsoft.Json;

namespace nookbot2
{
    class Config
    {
        private const string configFolder = "config";
        private const string configFile = "config.json";
        public const string configFullPath = configFolder + "/" + configFile;

        public static BotConfig Bot;

        // This method returns a bool to indicate if the config file and folder previously existed.
        public static bool CreateConfig()
        {
            if (!Directory.Exists(configFolder))
            {
                Bot = new BotConfig();
                string json_ = JsonConvert.SerializeObject(Bot, Formatting.Indented);
                File.WriteAllText(configFullPath, json_);
                return false;
            }

            string json = File.ReadAllText(configFullPath);
            Bot = JsonConvert.DeserializeObject<BotConfig>(json);
            return true;
        }
    }

    public class BotConfig
    {
        public string Token = "";
        public string CommandPrefix = ".";
        public ulong LogChannel;
    }
}
