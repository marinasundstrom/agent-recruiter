using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgentRecruiter.Models
{
    public class CandidateLanguage
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Candidate))]
        public string CandidateId { get; set; }

        public Candidate Candidate { get; set; }
    }
}