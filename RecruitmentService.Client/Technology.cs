using System;
using Newtonsoft.Json;

namespace RecruitmentService.Client
{
    public class Technology
    {
        [JsonProperty("votes")]
        public long Votes { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("fans")]
        public long Fans { get; set; }

        [JsonProperty("logo")]
        public Uri Logo { get; set; }

        [JsonProperty("stacks")]
        public long Stacks { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

}
