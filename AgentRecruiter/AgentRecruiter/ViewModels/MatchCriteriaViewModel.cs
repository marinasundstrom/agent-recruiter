using AgentRecruiter.Services;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AgentRecruiter.ViewModels
{
    public class MatchCriteriaViewModel : BaseViewModel
    {
        private readonly IRecruitmentQueryService recruitmentQueryService;
        private readonly IDataService dataService;
        private readonly IAlertService alertService;
        private string technology;
        private IEnumerable<string> technologySuggestions;
        private int yearsOfExperience = 1;

        public MatchCriteriaViewModel(IRecruitmentQueryService recruitmentQueryService, IDataService dataService, IAlertService alertService)
        {
            this.recruitmentQueryService = recruitmentQueryService;
            this.dataService = dataService;
            this.alertService = alertService;

            PropertyChanged += MatchCriteriaViewModel_PropertyChanged;

            Title = "Match Criteria";

            #region Input Suggestion

            TechnologySuggestions = new ObservableCollection<string>();

            TechnologyTextChangedCommand = new Command<AutoSuggestTextChangeReason>(async reason =>
            {
                if (reason == AutoSuggestTextChangeReason.UserInput)
                {
                    if (!string.IsNullOrEmpty(Technology))
                    {
                        if (Technology.Length >= 2)
                        {
                            TechnologySuggestions = (await recruitmentQueryService.GetTechnologiesAsync())
                                .Where(t => t.Name.ToLower().Contains(Technology.ToLower()))
                                .Select(t => t.Name).ToList();
                        }
                    }
                    else
                    {
                        TechnologySuggestions = null;
                    }
                }
            });

            TechnologySuggestionChosenCommand = new Command<string>(chosenSuggestion =>
            {
                Technology = chosenSuggestion;
            });

            TechnologyQuerySubmittedCommand = new Command<QuerySubmitArgs>(args =>
            {
                if (args.ChosenSuggestion != null)
                {
                    // User selected an item from the suggestion list, take an action on it here.
                }
                else
                {
                    // User hit Enter from the search box. Use args.QueryText to determine what to do.
                }
            });

            #endregion
        }

        private async void MatchCriteriaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // INFO: Auto-save when any field changes.

            await SaveParametersAsync();
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            try
            {
                await LoadParametersAsync();
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

        private async Task LoadParametersAsync()
        {
            await dataService.LoadAsync();

            Query query = dataService.Query;

            Technology = query.Technologies.FirstOrDefault();
            YearsOfExperience = query.YearsOfExperience;
        }

        private async Task SaveParametersAsync()
        {
            Query query = dataService.Query;

            query.Technologies.Clear();
            query.Technologies.Add(Technology);
            query.YearsOfExperience = YearsOfExperience;

            await dataService.SaveAsync();
        }

        public string Technology
        {
            get => technology;
            set => SetProperty(ref technology, value);
        }

        public int YearsOfExperience
        {
            get => yearsOfExperience;
            set => SetProperty(ref yearsOfExperience, value);
        }

        #region Input Suggestion

        public IEnumerable<string> TechnologySuggestions
        {
            get => technologySuggestions;
            private set => SetProperty(ref technologySuggestions, value);
        }

        public Command<AutoSuggestTextChangeReason> TechnologyTextChangedCommand { get; }

        public Command<string> TechnologySuggestionChosenCommand { get; }

        public Command<QuerySubmitArgs> TechnologyQuerySubmittedCommand { get; }

        #endregion
    }
}
