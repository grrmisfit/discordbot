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
using discordbot.Modules.Warframe;
using Newtonsoft.Json.Linq;
using discordbot.Core;
using System.IO;

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
                if(player.Name == message)
                {
                    namefound = true;
                    var embed = new EmbedBuilder();
                    embed.WithTitle("Player Information");
                    embed.WithDescription(message);
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
        [Command("missions")]
        public async Task GetMissions()
        {
            string url = "http://content.warframe.com/dynamic/worldState.php";
            string apiresponse = "";
            //apiClient.Encoding = Encoding.UTF8;

            using (WebClient client = new WebClient())
               
                apiresponse = client.DownloadString(url);

            // Warframe warframe = JsonConvert.DeserializeObject<Warframe>(apiresponse);
           // using Warframe;
            var warframe = Warframe.Warframe.FromJson(apiresponse);
            var seed = warframe.WorldSeed;
            var activeMissions = warframe.ActiveMissions; //this is a List<ActiveMission> 
            var theSorties = warframe.Sorties;
            int dacount = 0;
            foreach (ActiveMission am in activeMissions)
               
            {
                string type = activeMissions[dacount].MissionType;
                string bossname = theSorties[0].Boss;
                Date activationDate = am.Activation.Date;
                //Console.WriteLine(Utilities.GetMissions(type)); //+ " " + activeMissions[3].Node + " " + activeMissions[3].Region);
               

                await SendMessage(Utilities.GetMissions(type) + " " +  activeMissions[dacount].Node + " " + Utilities.GetSorties(bossname));
                dacount = dacount + 1;
               
            }
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
