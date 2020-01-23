using AgentRecruiter.Models;

using Microsoft.EntityFrameworkCore;

namespace AgentRecruiter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> AcceptedCandidates { get; set; }

        public DbSet<Candidate> RejectedCandidates { get; set; }

        public DbSet<CandidateTechnologyExperience> CandidateTechnologyExperiences { get; set; }

        public DbSet<Technology> Technologies { get; set; }

        public DbSet<CandidateLanguage> CandidateLanguages { get; set; }
    }
}
