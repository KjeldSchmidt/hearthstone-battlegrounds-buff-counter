using System.IO;
using Newtonsoft.Json;

namespace BattlegroundsBuffCounter
{
    public class Config
    {
        private static readonly string ConfigFolderPath =
            Hearthstone_Deck_Tracker.Config.AppDataPath + @"\Plugins\BattlegroundsBuffCounter";
        private static readonly string ConfigFilePath = ConfigFolderPath + @"\BattlegroundsBuffCounter.config";
        public double ExecutusCounterTop = 0;
        public double ExecutusCounterLeft = 0;
        public double CleefCounterTop = 0;
        public double CleefCounterLeft = 0;
        public double StrongarmCounterTop = 0;
        public double StrongarmCounterLeft = 0;

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