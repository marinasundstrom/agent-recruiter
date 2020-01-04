using System.Text.Json.Serialization;

namespace RecruitmentService.Client
{
    public class CandidateName
    {
        [JsonPropertyName("first")]
        public string FirstName { get; set; }

        [JsonPropertyName("last")]
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

}
