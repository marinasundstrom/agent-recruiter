using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgentRecruiter.Models
{
    public class CandidateTechnologyExperience
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Name))]
        public Technology Technology { get; set; }

        public int ExperienceYears { get; set; }
    }
}