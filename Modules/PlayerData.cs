using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discordbot.Modules
{
    class PlayerData
    {
        public static List<GetPlayerOnlineResult> players;

        public static void SavePlayers(IEnumerable<GetPlayerOnlineResult> accounts, string filePath)
        {
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        // Get all userAccounts
        public static IEnumerable<GetPlayerOnlineResult> LoadUserAccounts(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<GetPlayerOnlineResult>>(json);
        }

        public static bool SaveExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
