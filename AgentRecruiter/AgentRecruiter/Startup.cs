using AgentRecruiter.Data;
using AgentRecruiter.Helpers;
using AgentRecruiter.Services;
using AgentRecruiter.ViewModels;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RecruitmentService.Client;

using System;
using System.IO;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AgentRecruiter
{
    public class Startup
    {
        private const string databaseName = "database.db";

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

            var dbContext = host.Services.GetService<ApplicationDbContext>();
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            App.ServiceProvider = host.Services;
            ViewModelLocator.ServiceProvider = host.Services;

            return App.ServiceProvider.GetService<App>();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                String databasePath = "";
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        SQLitePCL.Batteries_V2.Init();
                        databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName); ;
                        break;
                    case Device.Android:
                        databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                        break;
                    default:
                        throw new NotImplementedException("Platform not supported");
                }
                optionsBuilder.UseSqlite($"Filename={databasePath}");
            });

            services.AddAutoMapper(typeof(Startup).Assembly);

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
            }).ConfigurePrimaryHttpMessageHandler(() => GetInsecureHandler());
        }

        // This method must be in a class in a platform project, even if
        // the HttpClient object is constructed in a shared project.
        public static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}
