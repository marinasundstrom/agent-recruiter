using AutoMapper;

using RecruitmentService.Client;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AgentRecruiter.Services
{
    public sealed class RecruitmentQueryService : IRecruitmentQueryService
    {
        private readonly IRecruitmentServiceClient recruitmentServiceClient;
        private readonly IDataService dataService;
        private readonly IMapper mapper;
        private bool initalized = false;

        public RecruitmentQueryService(IRecruitmentServiceClient recruitmentServiceClient, IDataService dataService, IMapper mapper)
        {
            this.recruitmentServiceClient = recruitmentServiceClient;
            this.dataService = dataService;
            this.mapper = mapper;
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

        public async Task<IEnumerable<Models.Technology>> GetTechnologiesAsync(CancellationToken cancellationToken = default)
        {
            if (await dataService.HasTechnologiesAsync())
            {
                // INFO: Load technologies from database.
                return await dataService.GetTechnologiesAsync();
            }

            // WARNING: This will take some time the first time the app opens.

            var technologies = await recruitmentServiceClient.GetTechnologiesAsync(cancellationToken);

            var mappedTechnologies =
                mapper.ProjectTo<Models.Technology>(technologies.AsQueryable())
                //.Take(TechnologiesResultLimit)
                .ToList();

            await dataService.UpdateTechnologiesAsync(mappedTechnologies);

            return mappedTechnologies;
        }

        public async Task<IEnumerable<Models.Candidate>> GetMatchingCandidatesAsync(CancellationToken cancellationToken = default)
        {
            var query = dataService.Query;

            var acceptedCandidates = await dataService.GetAcceptedCandidatesAsync();
            var rejectedCandidates = await dataService.GetRejectedCandidatesAsync();

            var candidates =
               await recruitmentServiceClient.GetCandidatesAsync(cancellationToken);

            var matchingCandidates = candidates
                .Where(candidate => candidate.IsActive)
                .Where(candidate => candidate.Technologies.Any(c => query.Technologies.Contains(c.Name) && c.ExperienceYears >= query.YearsOfExperience));

            var newCandidates = matchingCandidates
                .Where(c => !rejectedCandidates.Any(c2 => c2.Id == c.Id))
                .Where(c => !acceptedCandidates.Any(c2 => c2.Id == c.Id));

            return mapper.ProjectTo<Models.Candidate>(newCandidates.AsQueryable());
        }

        public async Task AcceptCandidateAsync(Models.Candidate candidate)
        {
            await dataService.AddAcceptedCandidateAsync(candidate);
        }

        public async Task RejectCandidateAsync(Models.Candidate candidate)
        {
            await dataService.AddRejectedCandidateAsync(candidate);
        }

        public async Task<IEnumerable<Models.Candidate>> GetAcceptedCandidatesAsync()
        {
            return await dataService.GetAcceptedCandidatesAsync();
        }
    }
}
