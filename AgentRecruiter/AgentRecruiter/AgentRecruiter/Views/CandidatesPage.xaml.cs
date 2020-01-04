using AgentRecruiter.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentRecruiter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CandidatesPage : ContentPage
    {
        public CandidatesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as CandidatesViewModel).InitializeAsync();

            base.OnAppearing();
        }
    }
}