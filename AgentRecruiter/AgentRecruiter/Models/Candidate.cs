using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgentRecruiter.Models
{
    public class Candidate
    {
        [Key]
        public string Id { get; set; }

        public int Index { get; set; }

        public string Guid { get; set; }

        public bool IsActive { get; set; }

        public int Age { get; set; }

        public int ShowSize { get; set; }

        public string EyeColor { get; set; }

        public CandidateName Name { get; set; }

        public string CurrentCompany { get; set; }

        public Uri Picture { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string About { get; set; }

        public string FullResume { get; set; }

        public string Registered { get; set; }

        public Coordinates LastKnownLocation { get; set; }

        // TODO: Solve foreign key issue. This is not important at the moment.
        [NotMapped]
        public ICollection<CandidateTechnologyExperience> Technologies { get; set; }

        public ICollection<CandidateLanguage> Languages { get; set; }

        public string FavoriteFruit { get; set; }
        public bool IsAccepted { get; internal set; }
    }
}
