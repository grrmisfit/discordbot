using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using discordbot.Core.UserAccounts;
using discordbot.Core;

namespace discordbot.Core
{
    internal static class RepeatingTimer
    {
        private static Timer loopingTimer;
        private static SocketTextChannel channel;
        internal static Task StartTimer()
        {
            channel = Global.Client.GetGuild(293720753185226752).GetTextChannel(471312780079923210);

            loopingTimer = new Timer()
            {
                Interval = 5000,
                AutoReset = true,
                Enabled = true
            };
            loopingTimer.Elapsed += OnTimerTicked;


            return Task.CompletedTask;
        }
        
        public static void ThePlayerData()
        {
            
            
            string json2 = "";
            using (WebClient client = new WebClient())
            {
                json2 = client.DownloadString("http://45.58.114.154:26916/api/getplayersonline?adminuser=" + Config.bot.webtoken + "&admintoken=" + Config.bot.webtokenpass);
            }
            if (json2 == "") return;
            if (json2 == "[]") return;
           
            JArray b = JArray.Parse(json2);
            var onlinePlayers = b.ToObject<List<GetPlayerOnlineResult>>();

            /*foreach (GetPlayerOnlineResult player in onlinePlayers)
             {
                int idnum = 0;
                 var result = from a in onlinePlayers 
                              where a.Steamid == theaccounts[idnum].Steamid
                              select a;
                 var theid = result.FirstOrDefault();
                 var newid = new GetPlayerOnlineResult();
              */

            // int dacount = 0;
            var theaccounts = DataStorage.LoadUserAccounts(UserAccounts.UserAccounts.accountsFile).ToList();
            for (int i = 0; i < theaccounts.Count; i++)
            {

                if (theaccounts.Count == 0)
                {
                    UserAccounts.UserAccounts.CreateUserAccount(onlinePlayers[i].Steamid, onlinePlayers[i].Name, onlinePlayers[i].Experience, onlinePlayers[i].Playerkills, onlinePlayers[i].Playerdeaths, onlinePlayers[i].Online);
                    break;
                }
                if (onlinePlayers[i].Steamid == theaccounts[i].Steamid)
                {

                }
                else
                {
                  
                    UserAccounts.UserAccounts.CreateUserAccount(onlinePlayers[i].Steamid, onlinePlayers[i].Name, onlinePlayers[i].Experience, onlinePlayers[i].Playerkills, onlinePlayers[i].Playerdeaths, onlinePlayers[i].Online);

                }

            }
            
        }
        private static void OnTimerTicked(object sender, ElapsedEventArgs e)
        {

            ThePlayerData();


        }
    }
}
