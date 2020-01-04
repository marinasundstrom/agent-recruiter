using AgentRecruiter.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentRecruiter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwipeMatchesPage : ContentPage
    {
        public SwipeMatchesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as SwipeMatchesViewModel).InitializeAsync();

            base.OnAppearing();
        }

        private async void GoToMatchCriteriaButton_Clicked(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("matchcriteria");
        }
    }
}