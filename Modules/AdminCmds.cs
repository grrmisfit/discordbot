using System.Net;
using System.Threading.Tasks;
using discordbot.Core.UserAccounts;
using Discord;
using Discord.Commands;

namespace discordbot.Modules
{
    public class AdminCmds : ModuleBase<SocketCommandContext>
    {
        [Command("AddVet")]
        public async Task AddVet([Remainder] string user)
        {

        }

        [RequireUserPermission(GuildPermission.ManageGuild)]
        [Command("god")]
        public async Task GodAll()
        {
            string json = "";
            WebClient client = new WebClient();
            
                json = client.DownloadString("http://45.58.114.154:26916/api/getplayersonline?adminuser=" +
                                             Config.bot.webtoken + "&admintoken=" + Config.bot.webtokenpass);
            

            if (json == "") return;
            if (json == "[]") return;

            var a = UserAccount.FromJson(json);
            foreach (var t in a)
            {
                client.DownloadString(""http://45.58.114.154:26916/api/send?adminuser=" +
                Config.bot.webtoken + "&admintoken=" + Config.bot.webtokenpass)")
            }
        }
    }
}