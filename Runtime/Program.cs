using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Runtime;

internal class Program
{
    private static ILogger<Program> _logger;
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureHostConfiguration(configurationBuilder =>
            {
                //Provide command line arguments and environment variables
                configurationBuilder.AddCommandLine(args);
                configurationBuilder.AddEnvironmentVariables();
            })
            .ConfigureAppConfiguration((context, builder) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                    Console.WriteLine("DEV MODE");
            })
            .ConfigureServices((context, services) =>
            {
                //Add NLog as the main logging service
                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddNLog();
                });
                
                services.AddTransient<IRunner, Runner>();
                
                Console.Title = context.Configuration.GetValue<string>("ConsoleName");
            })
            .Build();
        
        //create a logger for my Programmer class
        _logger = LoggerFactory.Create(lfBuilder => lfBuilder.AddNLog())
            .CreateLogger<Program>();
 
        _logger.LogInformation("Configuration loaded");
 
        //Get our instance we can actually call
        var service = ActivatorUtilities.CreateInstance<Runner>(host.Services);
        try
        {
            service.Run();
        }
        catch (Exception e)
        {
            _logger.LogCritical(e,"Uncaught exception in the Run method");
            throw;
        }
    }
}