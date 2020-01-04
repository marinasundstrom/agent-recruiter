using RecruitmentService.Client;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgentRecruiter.Services
{
    public interface IDataService
    {
        Query Query { get; }
        IList<Candidate> AcceptedCandidates { get; }
        IList<string> RejectedCandidates { get; }

        Task LoadAsync();
        Task SaveAsync();
    }
}