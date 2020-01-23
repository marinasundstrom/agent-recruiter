using AgentRecruiter.Models;
using AgentRecruiter.Services;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace AgentRecruiter.ViewModels
{
    public class MatchCriteriaViewModel : BaseViewModel
    {
        private readonly IRecruitmentQueryService recruitmentQueryService;
        private readonly IDataService dataService;
        private readonly IAlertService alertService;
        private ObservableCollection<object> selectedTechnologies;

        public MatchCriteriaViewModel(IRecruitmentQueryService recruitmentQueryService, IDataService dataService, IAlertService alertService)
        {
            this.recruitmentQueryService = recruitmentQueryService;
            this.dataService = dataService;
            this.alertService = alertService;

            AllTechnologies = new ObservableCollection<Technology>();
            SelectedTechnologies = new ObservableCollection<object>();

            SaveCommand = new Command(async () => await ExecuteSaveCommand());

            Title = "Match Criteria";
        }


        public async Task InitializeAsync()
        {
            IsBusy = true;

            try
            {
                var technologies = await recruitmentQueryService.GetTechnologiesAsync();

                AllTechnologies.Clear();

                foreach (var technology in technologies)
                {
                    AllTechnologies.Add(technology);
                }

                foreach (var technology in dataService.Query.Technologies)
                {
                    SelectedTechnologies.Add(AllTechnologies.OfType<Technology>().First(t => t.Name == technology.Name));
                }
            }
            catch (HttpRequestException exc)
            {
                await alertService.DisplayAlertAsync("Service Error", "Could not retrieve technologies.", "OK");
            }
            catch (Exception e)
            {
                await alertService.DisplayAlertAsync(string.Empty, "Something went wrong", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteSaveCommand()
        {
            dataService.Query.Technologies.Clear();
            foreach (var technology in SelectedTechnologies.OfType<Technology>())
            {
                dataService.Query.Technologies.Add(technology);
            }

            await dataService.SaveAsync();
        }

        public ObservableCollection<Technology> AllTechnologies { get; }

        public ObservableCollection<object> SelectedTechnologies
        {
            get => selectedTechnologies;
            set => SetProperty(ref selectedTechnologies, value);
        }

        public ICommand SaveCommand { get; }
    }
}
