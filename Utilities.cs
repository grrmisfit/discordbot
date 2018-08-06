using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using discordbot.Modules.Warframe;
using Newtonsoft.Json.Linq;

namespace discordbot
{
    class Utilities
    {
        private static Dictionary<string, string> alerts;
        private static Dictionary<string, string> missions;
        private static Dictionary<string, string> sorties;
       //old code  private static Dictionary<string, string> sortieboss;
        // not sued yet private static Dictionary<string, string> voids;
        static Utilities()
        {
            string thealerts = File.ReadAllText("SystemLang/alerts.json");
            string themissions = File.ReadAllText("Systemlang/WfData.json");
            string sortieinfo = File.ReadAllText("Systemlang/WfData.json");
           // string sortieboss = File.ReadAllText("Modules/Warframe/sortieboss.json");
          //  string sortiedesc = File.ReadAllText("Modules/Warframe/sortiedesc.json");
           // string sortietype = File.ReadAllText("Modules/Warframe/modtype.json");
            var data = JsonConvert.DeserializeObject<dynamic>(thealerts);
            var data2 = JsonConvert.DeserializeObject<dynamic>(themissions);
            // alerts = data.ToObject<Dictionary<string, string>>();
            var sortiedata = JsonConvert.DeserializeObject<dynamic>(sortieinfo);
            missions = data2.ToObject<Dictionary<string, string>>();
           sorties = sortiedata.ToObject<Dictionary<string, string>>();

        }

        public static string GetAlert(string key)
        {
            if (alerts.ContainsKey(key)) return alerts[key];
            return "";
        }

        public static string GetMissions(string key)
        {
            if (missions.ContainsKey(key)) return missions[key];
            return "";
        }

        public static string GetSortieBoss(string key)
        {
             if (sorties.ContainsKey(key)) return sorties[key];
           
            return "";
        }
        public static string GetSortieType(string key)
        {
            if (sorties.ContainsKey(key)) return sorties[key];
            
            return "";
        }
        public static string GetSortieSeed(string key)
        {
            if (sorties.ContainsKey(key)) return sorties[key];
            
            return "";
        }
        private Task SendMessage(string v)
        {
            throw new NotImplementedException();
        }

        public static string GetFormattedAlert(string key, params object[] parameter)
        {

            if (alerts.ContainsKey(key))
              {  return string.Format(alerts[key], parameter);


            }
            return "";
        }
        public static string GetFormattedAlert(string key, object parameter)
        {

            return GetFormattedAlert(key, new object[] { parameter });
        }
    }
}
