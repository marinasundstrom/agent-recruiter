using System.Text.Json.Serialization;

namespace RecruitmentService.Client
{
    public struct Coordinates
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude}, {Longitude}";
        }
    }

}
