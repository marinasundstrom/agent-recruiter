using RecruitmentService.Client;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgentRecruiter.Services
{
    public sealed class RecruitmentQueryService : IRecruitmentQueryService
    {
        private const int CandidateResultLimit = 100;
        private const int TechnologiesResultLimit = 100;

        private readonly IRecruitmentServiceClient recruitmentServiceClient;
        private readonly IDataService dataService;

        private bool initalized = false;

        public RecruitmentQueryService(IRecruitmentServiceClient recruitmentServiceClient, IDataService dataService)
        {
            this.recruitmentServiceClient = recruitmentServiceClient;
            this.dataService = dataService;
        }

        public async Task InitializeAsync()
        {
            if (initalized)
            {
                return;
            }

            await dataService.LoadAsync();

            initalized = true;
        }

        public async Task<IEnumerable<Technology>> GetTechnologiesAsync(CancellationToken cancellationToken = default)
        {
            var technologies = await recruitmentServiceClient.GetTechnologiesAsync(cancellationToken);
            return technologies
                .Take(TechnologiesResultLimit)
                .ToList();
        }

        public async Task<IEnumerable<Candidate>> GetMatchingCandidatesAsync(CancellationToken cancellationToken = default)
        {
            var query = dataService.Query.Technologies.Select(t => t.Name);

            var acceptedCandidates = dataService.AcceptedCandidates;
            var rejectedCandidates = dataService.RejectedCandidates;

            var candidates = await recruitmentServiceClient.GetCandidatesAsync(cancellationToken);

            var matchingCandidates = candidates
                .Where(candidate => candidate.IsActive)
                .Where(candidate => candidate.Technologies.Any(c => query.Contains(c.Name)));

            return matchingCandidates
                .Where(c => !rejectedCandidates.Any(c2 => c2 == c.Id))
                .Where(c => !acceptedCandidates.Any(c2 => c2.Id == c.Id))
                .Take(CandidateResultLimit)
                .ToList();
        }

        public async Task AcceptCandidateAsync(Candidate candidate)
        {
            dataService.AcceptedCandidates.Add(candidate);

            await dataService.SaveAsync();
        }

        public async Task RejectCandidateAsync(Candidate candidate)
        {
            dataService.RejectedCandidates.Add(candidate.Id);

            await dataService.SaveAsync();
        }

        public Task<IEnumerable<Candidate>> GetAcceptedCandidatesAsync()
        {
            return Task.FromResult<IEnumerable<Candidate>>(dataService.AcceptedCandidates.ToList());
        }
    }
}
