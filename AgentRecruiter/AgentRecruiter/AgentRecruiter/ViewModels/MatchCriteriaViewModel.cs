using AgentRecruiter.Services;

using RecruitmentService.Client;

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace AgentRecruiter.ViewModels
{
    public class MatchCriteriaViewModel : BaseViewModel
    {
        private readonly IRecruitmentQueryService recruitmentQueryService;
        private readonly IDataService dataService;
        private ObservableCollection<object> selectedTechnologies;

        public MatchCriteriaViewModel(IRecruitmentQueryService recruitmentQueryService, IDataService dataService)
        {
            this.recruitmentQueryService = recruitmentQueryService;
            this.dataService = dataService;

            AllTechnologies = new ObservableCollection<Technology>();
            SelectedTechnologies = new ObservableCollection<object>();

            SaveCommand = new Command(async () => await ExecuteSaveCommand());

            Title = "Match Criteria";
        }


        public async Task InitializeAsync()
        {
            IsBusy = true;

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

            IsBusy = false;
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
