using Microsoft.Extensions.Logging;

namespace Runtime;

public class Runner
{
    private readonly ILogger<Runner> _logger;
    private int _startNumber;

    public Runner(ILogger<Runner> logger, int startNumber)
    {
        _logger = logger;
        this._startNumber = startNumber;
    }
    public async Task RunAsync()
    {
        
    }
}