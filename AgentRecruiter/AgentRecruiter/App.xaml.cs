using Microsoft.Extensions.DependencyInjection;

using System;

using Xamarin.Forms;

namespace AgentRecruiter
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; internal set; }

        public App()
        {
            InitializeComponent();

            MainPage = ServiceProvider.GetService<AppShell>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
