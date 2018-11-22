using System.Collections.Generic;
using System.Linq;

namespace discordbot.Core.UserAccounts
{
    public static class PlayerAccounts
    {


        public static List<PlayerAccount> accounts;
        //public static List<UserAccount> curonline;

        public static string pFile = "Players/";

        

        public static void SaveAccount(string id)
        {
            DataStorage.SaveUserAccounts( accounts, $"{pFile}{id}.json");
        }

        public static PlayerAccount GetAccount(string user)
        {
            return GetOrCreateAccount(user);
        }

        private static PlayerAccount GetOrCreateAccount(string id)
        {
            if (DataStorage.SaveExists($"{pFile}{id}.json"))
                {

                   accounts = DataStorage.LoadUserAccounts($"{pFile}{id}.json").ToList();

                }
                 else
                // {
                //    accounts = new List<PlayerAccount>();
                //     DataStorage.SaveUserAccounts(accounts, accountsFile);
                //  }
                var result = from a in accounts
                where a.Steamid == id
                select a;

            var account = result.FirstOrDefault() ?? CreateUserAccount(id);
            return account;
        }

        private static PlayerAccount CreateUserAccount(string id)
        {

            var newAccount = new PlayerAccount()
            {
                Steamid = id,


            };

            accounts.Add(newAccount);
            SaveAccounts(id);
            return newAccount;
        }
    }

}
