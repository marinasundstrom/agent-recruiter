using System;
using System.ComponentModel.DataAnnotations;

namespace AgentRecruiter.Models
{
    public class Technology
    {
        public long Votes { get; set; }

        [Key]
        public string Name { get; set; }

        public Uri Url { get; set; }

        public long Fans { get; set; }

        public Uri Logo { get; set; }

        public long Stacks { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
