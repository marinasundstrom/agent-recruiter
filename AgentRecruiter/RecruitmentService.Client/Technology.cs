using System;
using System.Text.Json.Serialization;

namespace RecruitmentService.Client
{
    public class Technology
    {
        [JsonPropertyName("votes")]
        public long Votes { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        [JsonPropertyName("fans")]
        public long Fans { get; set; }

        [JsonPropertyName("logo")]
        public Uri Logo { get; set; }

        [JsonPropertyName("stacks")]
        public long Stacks { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

}
