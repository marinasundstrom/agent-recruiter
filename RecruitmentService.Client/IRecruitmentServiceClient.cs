using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentService.Client
{
    public interface IRecruitmentServiceClient
    {
        Task<IEnumerable<Candidate>> GetCandidatesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Technology>> GetTechnologiesAsync(CancellationToken cancellationToken = default);
    }

}
