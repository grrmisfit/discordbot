using System.Collections.Generic;
using System.Linq;

namespace discordbot.Core.UserAccounts
{
    public static class UserAccounts
    {


        public static List<UserAccount> accounts;
        //public static List<UserAccount> curonline;

        public static string accountsFile = "players.json";

        static UserAccounts()
        {
            if (DataStorage.SaveExists(accountsFile))
            {

                accounts = DataStorage.LoadUserAccounts(accountsFile).ToList();

            }
            else
            {
                accounts = new List<UserAccount>();
                DataStorage.SaveUserAccounts(accounts, accountsFile);
            }
        }

        public static void SaveAccounts()
        {
            DataStorage.SaveUserAccounts(accounts, accountsFile);
        }
       
        public static UserAccount GetAccount(string user)
        {
            return GetOrCreateAccount(user);
        }

        private static UserAccount GetOrCreateAccount(string id)
        {
            var result = from a in accounts
                where a.Steamid == id
                select a;

            var account = result.FirstOrDefault() ?? CreateUserAccount(id);
            return account;
        }

        private static UserAccount CreateUserAccount(string id)
        {

            var newAccount = new UserAccount()
            {
                Steamid = id,
                
                
            };

            accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;
        }
    }

}
