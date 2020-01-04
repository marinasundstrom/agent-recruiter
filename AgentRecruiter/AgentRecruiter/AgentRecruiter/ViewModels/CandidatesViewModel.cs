using AgentRecruiter.Services;

using RecruitmentService.Client;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AgentRecruiter.ViewModels
{
    public class CandidatesViewModel : BaseViewModel
    {
        private IRecruitmentQueryService recruitmentQueryService;
        private readonly IAlertService alertService;

        public CandidatesViewModel(IRecruitmentQueryService recruitmentQueryService, IAlertService alertService)
        {
            this.recruitmentQueryService = recruitmentQueryService;
            this.alertService = alertService;

            Candidates = new ObservableCollection<Candidate>();

            Title = "Candidates";
        }


        public async Task InitializeAsync()
        {
            IsBusy = true;

            try
            {
                var candidates = await recruitmentQueryService.GetAcceptedCandidatesAsync();

                Candidates.Clear();

                foreach (var candidate in candidates)
                {
                    Candidates.Add(candidate);
                }
            }
            catch (Exception exc)
            {
                await alertService.DisplayAlertAsync(string.Empty, "Something went wrong", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ObservableCollection<Candidate> Candidates { get; }
    }
}
