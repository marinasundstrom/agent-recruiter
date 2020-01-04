using RecruitmentService.Client;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgentRecruiter.Services
{
    public interface IRecruitmentQueryService
    {
        Task InitializeAsync();

        Task<IEnumerable<Candidate>> GetMatchingCandidatesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Technology>> GetTechnologiesAsync(CancellationToken cancellationToken = default);

        Task AcceptCandidateAsync(Candidate candidate);
        Task RejectCandidateAsync(Candidate candidate);

        Task<IEnumerable<Candidate>> GetAcceptedCandidatesAsync();
    }
}