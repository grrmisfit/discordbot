using System;
using System.Collections.Generic;

namespace discordbot.Modules
{

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class WebApi
    {
        [JsonProperty("gametime")]
        public Gametime Gametime { get; set; }

        [JsonProperty("players")]
        public long Players { get; set; }

        [JsonProperty("hostiles")]
        public long Hostiles { get; set; }

        [JsonProperty("animals")]
        public long Animals { get; set; }

        [JsonProperty("newlogs")]
        public long Newlogs { get; set; }
        [JsonProperty("firstLine")]
        public long FirstLine { get; set; }

        [JsonProperty("lastLine")]
        public long LastLine { get; set; }

        [JsonProperty("entries")]
        public List<Entry> Entries { get; set; }
        public partial class Entry
        {
            [JsonProperty("date")]
            public DateTimeOffset Date { get; set; }

            [JsonProperty("time")]
            public DateTimeOffset Time { get; set; }

            [JsonProperty("uptime")]
            public string Uptime { get; set; }

            [JsonProperty("msg")]
            public string Msg { get; set; }

            [JsonProperty("trace")]
            public string Trace { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }
    }

    public partial class Gametime
    {
        [JsonProperty("days")]
        public long Days { get; set; }

        [JsonProperty("hours")]
        public long Hours { get; set; }

        [JsonProperty("minutes")]
        public long Minutes { get; set; }
    }

    public partial class WebApi
    {
        public static WebApi FromJson(string json) => JsonConvert.DeserializeObject<WebApi>(json, discordbot.Modules.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this WebApi self) => JsonConvert.SerializeObject(self, discordbot.Modules.Converter.Settings);
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
