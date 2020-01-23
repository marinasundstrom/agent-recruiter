
using AgentRecruiter.Models;

using System.Collections.Generic;

namespace AgentRecruiter.Services
{
    public class Query
    {
        public IList<Technology> Technologies { get; set; } = new List<Technology>();
    }
}