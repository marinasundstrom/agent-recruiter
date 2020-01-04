using System;
using System.Text.Json.Serialization;

namespace RecruitmentService.Client
{
    public class Candidate
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("guid")]
        public string Guid { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("showSize")]
        public int ShowSize { get; set; }

        [JsonPropertyName("eyeColor")]
        public string EyeColor { get; set; }

        [JsonPropertyName("name")]
        public CandidateName Name { get; set; }

        [JsonPropertyName("currentCompany")]
        public string CurrentCompany { get; set; }

        [JsonPropertyName("picture")]
        public Uri Picture { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("about")]
        public string About { get; set; }

        [JsonPropertyName("fullResume")]
        public string FullResume { get; set; }

        [JsonPropertyName("registered")]
        public string Registered { get; set; }

        [JsonPropertyName("lastKnownLocation")]
        public Coordinates LastKnownLocation { get; set; }

        [JsonPropertyName("technologies")]
        public CandidatetTechnologyExperience[] Technologies { get; set; }

        [JsonPropertyName("languages")]
        public string[] Languages { get; set; }

        [JsonPropertyName("favoriteFruit")]
        public string FavoriteFruit { get; set; }
    }

}
