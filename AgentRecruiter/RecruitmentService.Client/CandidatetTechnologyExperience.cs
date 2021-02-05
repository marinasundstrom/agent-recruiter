using Newtonsoft.Json;

namespace RecruitmentService.Client
{

    public class CandidatetTechnologyExperience
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("experianceYears")]
        public int ExperienceYears { get; set; }
    }

}
