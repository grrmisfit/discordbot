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
        public static List<GetPlayerOnlineResult> curaccounts;
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

        public static GetPlayerOnlineResult GetAccount(string thaplayer)
    {
            return GetOrCreateAccount(thaplayer);
    }

        private static GetPlayerOnlineResult GetOrCreateAccount(string id)
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

       /* public static GetPlayerOnlineResult CheckForNewPlayers(string id)
        {
            var result = from a in accounts
                         where a.Steamid == id
                         select a;
            var theid = result.FirstOrDefault();
            var newid = new GetPlayerOnlineResult();

            for (int i = 0; i <newid.Count; i++)
            {
                if (curaccounts[i].Steamid == id) CreateUserAccount(curaccounts[i].Steamid, curaccounts[i].Name, curaccounts[i].Experience, curaccounts[i].Playerkills, curaccounts[i].Playerdeaths, curaccounts[i].Online);


            }
           // if (theid == null) CreateUserAccount(id);//theid = CreateUserAccount(newid.Steamid, newid.Name, newid.Experience, theid.Playerkills, theid.Playerdeaths, theid.Online);
            
                 return theid;
               
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
        
    }
}
