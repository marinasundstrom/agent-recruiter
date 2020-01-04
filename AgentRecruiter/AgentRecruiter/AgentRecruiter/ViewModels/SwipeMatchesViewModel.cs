using AgentRecruiter.Services;

using MLToolkit.Forms.SwipeCardView.Core;

using RecruitmentService.Client;

using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace AgentRecruiter.ViewModels
{
    public class SwipeMatchesViewModel : BaseViewModel
    {
        private readonly IRecruitmentQueryService recruitmentQueryService;
        private readonly IAlertService alertService;

        public SwipeMatchesViewModel(IRecruitmentQueryService recruitmentQueryService, IAlertService alertService)
        {
            this.recruitmentQueryService = recruitmentQueryService;
            this.alertService = alertService;

            Matches = new ObservableCollection<Candidate>();

            SwipeCommand = new Command<SwipedCardEventArgs>(OnSwipe);

            Title = "Swipe";
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            try
            {
                await recruitmentQueryService.InitializeAsync();

                var matches = await recruitmentQueryService.GetMatchingCandidatesAsync();

                Matches.Clear();

                foreach (var match in matches)
                {
                    Matches.Add(match);
                }
            }
            catch (HttpRequestException exc)
            {
                await alertService.DisplayAlertAsync("Service Error", "Could not get matches.", "OK");
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

        private async void OnSwipe(SwipedCardEventArgs obj)
        {
            switch (obj.Direction)
            {
                case SwipeCardDirection.Right:
                    await AcceptCandidateAsync((Candidate)obj.Item);
                    break;

                case SwipeCardDirection.Left:
                    await RejectCandidateAsync((Candidate)obj.Item);
                    break;
            }
        }

        private async Task AcceptCandidateAsync(Candidate candidate)
        {
            await recruitmentQueryService.AcceptCandidateAsync(candidate);

            // TODO: Put in its own service to enable unit testing.
            Vibration.Vibrate();
        }

        private async Task RejectCandidateAsync(Candidate candidate)
        {
            await recruitmentQueryService.RejectCandidateAsync(candidate);
        }

        public ObservableCollection<Candidate> Matches { get; }

        public ICommand SwipeCommand { get; }
    }
}
