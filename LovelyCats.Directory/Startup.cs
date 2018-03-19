using System;
using System.Reflection;
using System.Threading.Tasks;
using LovelyCats.Directory.Features.Cats.RequestHandlers;
using LovelyCats.Directory.Infrastructures.HttpClientFactory;
using LovelyCats.Directory.Infrastructures.Services.Cat;
using LovelyCats.Directory.Infrastructures.Services.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LovelyCats.Directory
{
    public class Startup
    {
        public IConfigurationRoot _configuration { get; }
        public IServiceProvider _serviceProvider { get; }

        private IMediator _mediator => _serviceProvider.GetService<IMediator>();
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
            var serviceCollection = new ServiceCollection();
            // Configure services
            _serviceProvider = ConfigureServices(serviceCollection);

            // Config Logging
            ConfigureLogging();
        }

        #region helpers
        private IServiceProvider ConfigureServices(IServiceCollection serviceCollection)
        {

            serviceCollection.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
            serviceCollection.AddLogging();
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());

            serviceCollection.AddSingleton<IPetService, PetService>();
            serviceCollection.AddSingleton<IHttpClientFactory, HttpClientFactory>();

            return serviceCollection.BuildServiceProvider();
        }


        private void ConfigureLogging()
        {
            _serviceProvider.GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);
        }

        #endregion

        public async Task RunAsync()
        {
            var catResult = await _mediator.Send(new GetCatResultRequest());

            if (!catResult.Success)
            {
                Console.WriteLine("Error occurs when fetching the data");
                return;
            }

            foreach (var feature in catResult.FeatureGroups)
            {
                Console.WriteLine(feature.Key);
                foreach (var name in feature.Value)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine();
            }
        }
    }
}