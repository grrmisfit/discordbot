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

            foreach (GetPlayerOnlineResult player in onlinePlayers)
            {
                UserAccounts.UserAccounts.CheckForNewPlayers(player.Steamid);
            }

            // int dacount = 0;
           // var theaccounts = DataStorage.LoadUserAccounts(UserAccounts.UserAccounts.accountsFile).ToList();

           
            }


                private static void OnTimerTicked(object sender, ElapsedEventArgs e)
                {

                    ThePlayerData();


                }
            }
}
