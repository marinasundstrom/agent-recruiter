
using System.Collections.Generic;

namespace AgentRecruiter.Services
{
    public class Query
    {
        public IList<string> Technologies { get; set; } = new List<string>();

        public int YearsOfExperience { get; set; }
    }
}