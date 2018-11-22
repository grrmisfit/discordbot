using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace discordbot
{

    public class Misc : ModuleBase<SocketCommandContext>
    {
        
        public async Task SendMessage(string msg)
            {
           await Context.Channel.SendMessageAsync( msg);
            }
        public string themsg;
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
            if (!File.Exists("players.json"))
            {
              await  SendMessage("Player File not found!");
                return;
            }
             json = File.ReadAllText("players.json");
           
            if (json == "") return;
            if (json == "[]") await SendMessage("No one is online atm, try again later");
           
            JArray a = JArray.Parse(json);
            List<GetPlayerOnlineResult> tmpdata = new List<GetPlayerOnlineResult>();
            var tempdata = a.ToObject<List<GetPlayerOnlineResult>>();
            var namefound = false;
            foreach (GetPlayerOnlineResult player in tempdata)
            {
                if(player.Name.ToLower().Contains(message.ToLower()) )
                {
                    namefound = true;
                    var embed = new EmbedBuilder();
                    embed.WithTitle("Player Information");
                    embed.WithDescription(player.Name);
                    embed.AddField("Level", Math.Floor(player.Level),true);
                    embed.AddField("Kills", player.Zombiekills, true);
                    embed.AddField("Deaths", player.Playerdeaths, true);
                    embed.AddField("Last Online on", player.Lastonline, true);
                    embed.WithColor(new Color(0, 255, 0));

                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    break;
                }
                
                  }
           
            if (namefound == false) await  SendMessage("Player hasnt been seen yet, try again later!");
             
                    

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

       

        [Command("log")]

        public async Task LogCom()
        {
            
            Utilities.GetLogLine();
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

        [Command("nfo")]
        public async Task SayNfo()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Server Information");
            embed.AddField("Server Ip: ", "45.58.114.154");
            embed.AddField("Port: ", "26912");
            embed.AddField("Notes: ", "Run with EAC on!");
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("say")]
        public async Task SayToChat([Remainder] string msg)
        {
            
            await Context.Channel.SendMessageAsync(msg);
        }

       // [Command("mystats")]
       // public async Task MyStats([Remainder]string daplayer)
       // {
           //target = null;
           // var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
           // target = mentionedUser ?? Context.User;


           // var account = UserAccounts.GetAccount(target);
         //   await Context.Channel.SendMessageAsync($"{target.Username} has {account.XP} XP and {account.Points} points");
       // }

     
       
       [Command("react")]
       public async Task HandleReactionMessage()
        {
           RestUserMessage msg = await Context.Channel.SendMessageAsync("React to me!");
            Global.MessageIdToTrack = msg.Id;
        }
      

    }
}
