using Microsoft.EntityFrameworkCore;

namespace AgentRecruiter.Models
{
    [Owned]
    public class CandidateName
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}