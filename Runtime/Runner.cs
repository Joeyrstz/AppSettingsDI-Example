using Microsoft.Extensions.Logging;

namespace Runtime;

public class Runner : IRunner
{
    private readonly ILogger<Runner> _logger;

    public Runner(ILogger<Runner> logger)
    {
        _logger = logger;
    }
    public void Run()
    {
        Console.WriteLine("Test");
    }
}