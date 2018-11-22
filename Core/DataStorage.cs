using discordbot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
namespace discordbot.Core
{
    public static class DataStorage
    {
        
      
        // Save all userAccounts
        public static void SaveUserAccounts(IEnumerable<PlayerAccount> accounts, string filePath)
        {
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        // Get all userAccounts
        public static IEnumerable<PlayerAccount> LoadUserAccounts(string filePath)
        {
            if(!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<PlayerAccount>>(json);
        }
        public static IEnumerable<PlayerAccount> LoadSavedAccounts(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<PlayerAccount>>(json);
        }
        public static void SaveTmpAccounts(IEnumerable<PlayerAccount> accounts, string filePath)
        {
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        public static bool SaveExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
