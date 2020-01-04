using AgentRecruiter.Services;
using AgentRecruiter.ViewModels;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RecruitmentService.Client;

using System;

namespace AgentRecruiter
{
    public class Startup
    {
        public static App Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices = null)
        {
            var host = new HostBuilder()
                            .ConfigureServices((c, x) =>
                            {
                                nativeConfigureServices?.Invoke(c, x);
                                ConfigureServices(c, x);
                            })
                            .ConfigureLogging(l => l.AddConsole(o =>
                            {
                                o.DisableColors = true;
                            }))
                            .Build();

            App.ServiceProvider = host.Services;
            ViewModelLocator.ServiceProvider = host.Services;

            return App.ServiceProvider.GetService<App>();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddTransient<AppShell>();
            services.AddSingleton<App>();

            services.AddTransient<CandidatesViewModel>();
            services.AddTransient<MatchCriteriaViewModel>();
            services.AddTransient<SwipeMatchesViewModel>();

            services.AddSingleton<IDataService, DataService>();
            services.AddTransient<IRecruitmentQueryService, RecruitmentQueryService>();

            services.AddHttpClient<IRecruitmentServiceClient, RecruitmentServiceClient>(httpClient => httpClient.BaseAddress = new Uri("https://v1.ifs.aero/"));
        }
    }
}
