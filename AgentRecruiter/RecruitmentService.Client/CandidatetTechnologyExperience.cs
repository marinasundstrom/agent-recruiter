using System.Text.Json.Serialization;

namespace RecruitmentService.Client
{

    public class CandidatetTechnologyExperience
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("experianceYears")]
        public int ExperienceYears { get; set; }
    }

}
