using Newtonsoft.Json;

namespace RecruitmentService.Client
{
    public class CandidateName
    {
        [JsonProperty("first")]
        public string FirstName { get; set; }

        [JsonProperty("last")]
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

}
