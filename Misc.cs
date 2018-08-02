using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using discordbot.Core.UserAccounts;
using System.Net;
using Discord.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace discordbot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        public async Task SendMessage(string msg)
            {
           await Context.Channel.SendMessageAsync( msg);
            }

    [Command("Kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, string reason = "No reason given.")
        {
            await user.KickAsync(reason);
        }

        [Command("Ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, string reason = "No reason given.")
        {
            await user.Guild.AddBanAsync(user, 5, reason);
            
        }

        [Command("player")]
        public async Task GetPlayersList([Remainder]string message)
        {
            string json = "";
            using (WebClient client = new WebClient())
            {
                 json = client.DownloadString("http://45.58.114.154:26916/api/getplayersonline?adminuser=csmm2&admintoken=da3rd");
            }
            if (json == "") return;
            if (json == "[]") await SendMessage("No one is online atm, try again later");
           // var apiResponse = JsonConvert.DeserializeObject<apiResponse>(json);
            JArray a = JArray.Parse(json);
            var onlinePlayers = a.ToObject<List<GetPlayerOnlineResult>>();
            
            foreach(GetPlayerOnlineResult player in onlinePlayers)
            {
                if(player.Name == message)
                {
                    var embed = new EmbedBuilder();
                    embed.WithTitle("Player Information");
                    embed.WithDescription(message);
                    embed.AddField("Level", Math.Floor(player.Level),true);
                    embed.AddField("Kills", player.Zombiekills, true);
                    embed.AddField("Deaths", player.Playerdeaths, true);
                    //embed.AddField("Location", player.Position, true);
                    embed.WithColor(new Color(0, 255, 0));

                    await Context.Channel.SendMessageAsync("", false, embed.Build());

                }
            }
            //string steamid =  a.players[0].steamid.ToString();
            


        }

        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message by " + Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));
           
            await SendMessage("test");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("getporn")]
        public async Task PickOne([Remainder]string message)
        {

            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            
            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];



            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));
            embed.WithThumbnailUrl("https://i.kinja-img.com/gawker-media/image/upload/s--HApVmIBh--/c_scale,f_auto,fl_progressive,q_80,w_800/zigjxvw4cg3xbabifaet.png");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("secret")]

        public async Task RevealSecret([Remainder]string arg = "")
        {
            if (!IsUserOwner((SocketGuildUser)Context.User)) return;
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));

        }

        private bool IsUserOwner(SocketGuildUser user)
        {
            string targetRoleName = "BotOwner";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }

        [Command("data")]
        public async Task GetData()
        {
            await Context.Channel.SendMessageAsync("Data has " + DataStorage.GetPairsCount() + " pairs.");
            DataStorage.AddPairToStorage("Count" + DataStorage.GetPairsCount(), "TheCount" + DataStorage.GetPairsCount());
        }

       // [Command("stats")]
       // public async Task ThierStats(IGuildUser user)
      //  {
         //   var account = UserAccounts.GetAccount((SocketUser)user);
         //   await Context.Channel.SendMessageAsync($"You have {account.XP} XP and {account.Points} points");
       // }

       // [Command("mystats")]
       // public async Task MyStats([Remainder]string daplayer)
       // {
           //target = null;
           // var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
           // target = mentionedUser ?? Context.User;


           // var account = UserAccounts.GetAccount(target);
         //   await Context.Channel.SendMessageAsync($"{target.Username} has {account.XP} XP and {account.Points} points");
       // }

      /*  [Command("addxp")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddXp(uint xp)
        {
            var account = UserAccounts.GetAccount(Context.User);
            account.XP += xp;
            UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"You gained {xp} XP.");
        }
        */
       
       [Command("react")]
       public async Task HandleReactionMessage()
        {
           RestUserMessage msg = await Context.Channel.SendMessageAsync("React to me!");
            Global.MessageIdToTrack = msg.Id;
        }
      

    }
}
