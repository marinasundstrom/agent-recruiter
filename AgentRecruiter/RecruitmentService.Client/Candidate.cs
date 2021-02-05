using System;
using Newtonsoft.Json;

namespace RecruitmentService.Client
{
    public class Candidate
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("showSize")]
        public int ShowSize { get; set; }

        [JsonProperty("eyeColor")]
        public string EyeColor { get; set; }

        [JsonProperty("name")]
        public CandidateName Name { get; set; }

        [JsonProperty("currentCompany")]
        public string CurrentCompany { get; set; }

        [JsonProperty("picture")]
        public Uri Picture { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("fullResume")]
        public string FullResume { get; set; }

        [JsonProperty("registered")]
        public string Registered { get; set; }

        [JsonProperty("lastKnownLocation")]
        public Coordinates LastKnownLocation { get; set; }

        [JsonProperty("technologies")]
        public CandidatetTechnologyExperience[] Technologies { get; set; }

        [JsonProperty("languages")]
        public string[] Languages { get; set; }

        [JsonProperty("favoriteFruit")]
        public string FavoriteFruit { get; set; }
    }

}
