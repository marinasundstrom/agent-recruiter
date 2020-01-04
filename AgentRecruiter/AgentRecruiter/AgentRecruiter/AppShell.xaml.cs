using AgentRecruiter.Views;

using Xamarin.Forms;

namespace AgentRecruiter
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("matchcriteria", typeof(MatchCriteriaPage));
        }
    }
}
