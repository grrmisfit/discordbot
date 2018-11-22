using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System;
using discordbot.Core.UserAccounts;
using Discord;
using MinimalisticTelnet;

namespace discordbot.Core
{

    public static class RepeatingTimer
    {

        private static string accountsFile = "players.json";
        private static Timer loopingTimer;
        private static SocketTextChannel channel;
        public static Timer telTimer;

        internal static Task StartTimer()


        {
            channel = Global.Client.GetGuild(293720753185226752).GetTextChannel(471312780079923210);

            loopingTimer = new Timer()
            {
                Interval = 2000,
                AutoReset = true,
                Enabled = true
            };
            loopingTimer.Elapsed += OnTimerTicked;


            return Task.CompletedTask;
        }

        public static Task TelnetTimer()
        {
            telTimer = new Timer()
            {
                Interval = 2000,
                AutoReset = true,
                Enabled = true
            };
            telTimer.Elapsed += OnTelTimerTicked;
            return Task.CompletedTask;
        }

        public static void GetTelnetInfo()
        {

        }

        public async static void OnTelTimerTicked(object sender, ElapsedEventArgs e)
        {


          
        }

        public static void ThePlayerData()
            {


                string json = "";
                using (WebClient client = new WebClient())
                {
                    json = client.DownloadString("http://45.58.114.154:26916/api/getplayersonline?adminuser=" +
                                                 Config.bot.webtoken + "&admintoken=" + Config.bot.webtokenpass);
                }

                if (json == "") return;
                if (json == "[]") return;

                 var a = UserAccount.FromJson(json);
                var onlinePlayers =a;
                string savedaccounts = File.ReadAllText("players.json");
                JArray b = JArray.Parse(savedaccounts);
                var theaccounts = b.ToObject<List<UserAccount>>();
                List<UserAccount> tmp = new List<UserAccount>();
                foreach (UserAccount onlinePlayer in onlinePlayers)
                {
                    bool playerfound = false;

                   /* foreach (UserAccount theaccount in theaccounts)
                    {
                        if (onlinePlayer.Steamid.Equals(theaccount.Steamid))
                        {
                            //player found so break second loop for next comparisson
                            
                            playerfound = true;
                           // break;
                        }
                    }

                    if (!playerfound)
                    {
                        //player was in onlineList but not in savedlist
                        // do something
                        tmp.Add(onlinePlayer);
                    }
                    */
                    var account = UserAccounts.UserAccounts.GetAccount(onlinePlayer.Steamid);
                    UserAccounts.UserAccounts.SaveAccounts();
                }

                foreach (UserAccount res in tmp)
                {
                    //theaccounts.Add(res);
                    //DataStorage.SaveTmpAccounts(theaccounts, accountsFile);
                }



            }


            private static async void OnTimerTicked(object sender, ElapsedEventArgs e)
            {

               // ThePlayerData();

                var msg = Utilities.GetLogLine();
                var chnl = Global.Client.GetChannel(484348515338944512) as IMessageChannel;
                if (!string.IsNullOrEmpty(msg))
                {
                    if (chnl != null) await chnl.SendMessageAsync(msg);
                }
        }
        }
    }
