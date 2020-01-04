using AgentRecruiter.Services;

using MLToolkit.Forms.SwipeCardView.Core;

using RecruitmentService.Client;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace AgentRecruiter.ViewModels
{
    public class SwipeMatchesViewModel : BaseViewModel, IDisposable
    {
        private readonly IRecruitmentQueryService recruitmentQueryService;
        private readonly IAlertService alertService;
        private readonly IVibrationService vibrationService;
        private bool hasNoMatches;

        public SwipeMatchesViewModel(
            IRecruitmentQueryService recruitmentQueryService,
            IAlertService alertService,
            IVibrationService vibrationService)
        {
            this.recruitmentQueryService = recruitmentQueryService;
            this.alertService = alertService;
            this.vibrationService = vibrationService;

            Matches = new ObservableCollection<Candidate>();
            Matches.CollectionChanged += Matches_CollectionChanged;

            SwipeCommand = new Command<SwipedCardEventArgs>(OnSwipe);

            Title = "Matches";
        }

        private void Matches_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HasNoMatches = !Matches.Any();
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

            await vibrationService.VibrateAsync();
        }

        private async Task RejectCandidateAsync(Candidate candidate)
        {
            await recruitmentQueryService.RejectCandidateAsync(candidate);
        }

        public void Dispose()
        {
            Matches.CollectionChanged -= Matches_CollectionChanged;
        }

        public ObservableCollection<Candidate> Matches { get; }

        public ICommand SwipeCommand { get; }

        public bool HasNoMatches
        {
            get => hasNoMatches;
            private set => SetProperty(ref hasNoMatches, value);
        }
    }
}
