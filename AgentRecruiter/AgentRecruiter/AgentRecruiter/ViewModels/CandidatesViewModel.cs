using AgentRecruiter.Services;

using RecruitmentService.Client;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AgentRecruiter.ViewModels
{
    public class CandidatesViewModel : BaseViewModel
    {
        private IRecruitmentQueryService recruitmentQueryService;

        public CandidatesViewModel(IRecruitmentQueryService recruitmentQueryService)
        {
            this.recruitmentQueryService = recruitmentQueryService;

            Candidates = new ObservableCollection<Candidate>();
        }


        public async Task InitializeAsync()
        {
            var candidates = await recruitmentQueryService.GetAcceptedCandidatesAsync();

            Candidates.Clear();

            foreach (var candidate in candidates)
            {
                Candidates.Add(candidate);
            }
        }

        public ObservableCollection<Candidate> Candidates { get; }
    }
}
