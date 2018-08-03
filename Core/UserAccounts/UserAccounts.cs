using Discord.WebSocket;
using discordbot.Modules;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace discordbot.Core.UserAccounts
{
  public static  class UserAccounts
    {
        

        public static List<GetPlayerOnlineResult> accounts;
        public static List<GetPlayerOnlineResult> curonline;
        public static string curplayers = "";
        public static string accountsFile = "players.json";
            static UserAccounts()
             {
                if(DataStorage.SaveExists(accountsFile))
            {
                accounts = DataStorage.LoadUserAccounts(accountsFile).ToList();
                
            }
                else
            {
                accounts = new List<GetPlayerOnlineResult>();
                SaveAccounts();
            }
             }
        
        public static void SaveAccounts()
        {
            DataStorage.SaveUserAccounts(accounts, accountsFile);
        }

     /*   public static GetPlayerOnlineResult GetAccount(string thaplayer)
    {
            return GetOrCreateAccount(thaplayer);
    }

      /*  private static GetPlayerOnlineResult GetOrCreateAccount(string id)
        {
            var result = from a in accounts
                         
                         where a.Steamid == id
                         select a;

            var account = result.FirstOrDefault();
            if (account == null)
            {
                account = CreateUserAccount(account.Steamid, account.Name, account.Experience, account.Playerkills, account.Playerdeaths, account.Online);
            }

            return account;
        }
        */
        public static GetPlayerOnlineResult CreateUserAccount(string id, string name, int xp, int kills, int deaths, bool on)
        {



            var newAccount = new GetPlayerOnlineResult()
            {
                Steamid = id,
                Name = name,
                Experience = xp,
                Playerkills = kills,
                Playerdeaths = deaths,
                Online = on,
            };

            accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;

        }

        public static void CheckForNewPlayers(string id)
        {
            //var result = from a in accounts
            //              where a.Steamid == id
            //              select a;
            // var account = result.FirstOrDefault();

            // if (account == null)
            JObject a = JObject.Parse(curplayers);
            for (int i = 0; i <curonline.Count; i++)
                if (a.ContainsKey(id) == true) 
                    {
                         CreateUserAccount(curonline[i].Steamid, curonline[i].Name, curonline[i].Experience, curonline[i].Playerkills, curonline[i].Playerdeaths, curonline[i].Online);
                    }

    
        }
            
          }
        
        }
