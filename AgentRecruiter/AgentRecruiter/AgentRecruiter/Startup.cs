using AgentRecruiter.Helpers;
using AgentRecruiter.Services;
using AgentRecruiter.ViewModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RecruitmentService.Client;

using System;
using System.IO;

using Xamarin.Essentials;

namespace AgentRecruiter
{
    public class Startup
    {
        public static App Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices = null)
        {
            string systemDir = FileSystem.CacheDirectory;

            Utils.ExtractSaveResource("AgentRecruiter.appsettings.json", systemDir);
            Utils.ExtractSaveResource("AgentRecruiter.appsettings.Development.json", systemDir);

            string fullConfig = Path.Combine(systemDir, "AgentRecruiter.appsettings.json");
            string fullDevConfig = Path.Combine(systemDir, "AgentRecruiter.appsettings.Development.json");

            var host = new HostBuilder()
                            .ConfigureHostConfiguration(c =>
                            {
                                c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                                c.AddJsonFile(fullConfig);
                                c.AddJsonFile(fullDevConfig, optional: true);
                            })
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

            services.AddSingleton<IAlertService, AlertService>();
            services.AddSingleton<IVibrationService, VibrationService>();

            services.AddHttpClient<IRecruitmentServiceClient, RecruitmentServiceClient>((sp, httpClient) =>
            {
                var serviceEndpoint = sp
                    .GetService<IConfiguration>()
                    .GetValue<string>("ServiceEndpoint");

                httpClient.BaseAddress = new Uri($"{serviceEndpoint}/");
            });
        }
    }
}
