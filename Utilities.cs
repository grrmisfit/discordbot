using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using discordbot.Modules;
using MinimalisticTelnet;
using TwitchLib.Api.Sections;

namespace discordbot
{
    public static class Extensions
    {
        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        public static string Right(this string value, int length)
        {
            return value.Substring(value.Length - length);
        }

        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                ? value
                : value.Substring(0, maxLength)
            );
        }
    }
    class Utilities
    {
        private static readonly string Token = Config.bot.webtoken;
        public static string AuthPass = Config.bot.webtokenpass;
        public static string LastLine;
        static Utilities()
    {
        

    }

        public static  string GetLogLine()
        {
            var client = new WebClient();
            var url = $"{Config.bot.serverIp}/api/getwebuiupdates?&adminuser={Token}&admintoken={AuthPass}";
            var logurl = $"{Config.bot.serverIp}/api/getlog?&adminuser={Token}&admintoken={AuthPass}&firstline=";
            var json = client.DownloadString(url);
            var data = WebApi.FromJson(json);
            var lastnum = data.Newlogs;
            var logdata = client.DownloadString(logurl + lastnum);
            var chat = WebApi.FromJson(logdata);
            string chatMsg = "";
            foreach (var t in chat.Entries)
            {
                if (t.Msg.ToLower().Contains("chat") && t.Msg.Contains("Global"))
                {
                    
                    if (t.Msg == LastLine)
                    {
                        chatMsg = "";
                        break;
                    }
                    
                    chatMsg = t.Msg;
                    var strlen = chatMsg.Length;
                    chatMsg = chatMsg.Split(')')[1];
                    LastLine = t.Msg;
                    break;
                }
            }

            return chatMsg;
        }
        public static async void GetTelentInfo()
        {
            TelnetConnection tc = new TelnetConnection("45.58.114.154", 26915);

            string s = tc.Login("root", "Gdr71195", 100);
            //string s = "";
            string prompt = "";
            Console.WriteLine(s);
            // prompt = s.Substring(prompt.Length - 1, 1);
            while (tc.IsConnected && prompt.Trim() != "Disconnecting")

            {
                // display server output
                Console.Write(tc.Read());

                // send client input to server
                //prompt = Console.ReadLine();
               // tc.WriteLine(prompt);
                tc.WriteLine("lp");
                // display server output
                Console.Write(tc.Read());
                await Task.Delay(30000);
            }
            Console.WriteLine("***DISCONNECTED");
            Console.ReadLine();
        }
        
    }
    

    
}
