
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AgentRecruiter.Services
{
    public interface IRecruitmentQueryService
    {
        Task InitializeAsync();

        Task<IEnumerable<Models.Candidate>> GetMatchingCandidatesAsync(CancellationToken cancellationToken = default);
       
        Task<IEnumerable<Models.Technology>> GetTechnologiesAsync(CancellationToken cancellationToken = default);

        Task AcceptCandidateAsync(Models.Candidate candidate);

        Task RejectCandidateAsync(Models.Candidate candidate);

        Task<IEnumerable<Models.Candidate>> GetAcceptedCandidatesAsync();
    }
}