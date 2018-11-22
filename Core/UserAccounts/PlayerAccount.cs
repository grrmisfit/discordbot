using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace discordbot.Core.UserAccounts
{
    public class PlayerAccount
    {
        [JsonProperty("steamid")]
        public string Steamid { get; set; }

        [JsonProperty("entityid")]
        public long Entityid { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("experience")]
        public long Experience { get; set; }

        [JsonProperty("level")]
        public double Level { get; set; }

        [JsonProperty("health")]
        public long Health { get; set; }

        [JsonProperty("stamina")]
        public double Stamina { get; set; }

        [JsonProperty("zombiekills")]
        public long Zombiekills { get; set; }

        [JsonProperty("playerkills")]
        public long Playerkills { get; set; }

        [JsonProperty("playerdeaths")]
        public long Playerdeaths { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }

        [JsonProperty("totalplaytime")]
        public long Totalplaytime { get; set; }

        [JsonProperty("lastonline")]
        public DateTimeOffset Lastonline { get; set; }

        [JsonProperty("ping")]
        public long Ping { get; set; }
    }

    public partial class Position
    {
        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }

        [JsonProperty("z")]
        public long Z { get; set; }
    }

    public partial class UserAccount
    {
        public static List<UserAccount> FromJson(string json) => JsonConvert.DeserializeObject<List<UserAccount>>(json, discordbot.Modules.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<UserAccount> self) => JsonConvert.SerializeObject(self, discordbot.Modules.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
}