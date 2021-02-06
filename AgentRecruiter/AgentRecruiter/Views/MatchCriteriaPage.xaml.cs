using AgentRecruiter.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentRecruiter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchCriteriaPage : ContentPage
    {
        public MatchCriteriaPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as MatchCriteriaViewModel).InitializeAsync();

            base.OnAppearing();
        }
    }
}