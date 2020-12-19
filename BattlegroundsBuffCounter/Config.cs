using System.IO;
using Newtonsoft.Json;

namespace BattlegroundsBuffCounter
{
    public class Config
    {
        private static readonly string ConfigFolderPath =
            Hearthstone_Deck_Tracker.Config.AppDataPath + @"\Plugins\ExecutusCounter";
        private static readonly string ConfigFilePath = ConfigFolderPath + @"\ExecutusCounter.config";
        public double ExecutusCounterTop = 0;
        public double ExecutusCounterLeft = 0;

        public void Save()
        {
            Directory.CreateDirectory(ConfigFolderPath);
            File.WriteAllText(ConfigFilePath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        
        private Config() {}

        public static Config Load()
        {
            return File.Exists(ConfigFilePath) 
                ? JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigFilePath)) 
                : new Config();
        }
    }
}