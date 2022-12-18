using LoggerService;

namespace LoggerService.Test;

public class LogManagerTests
{
    private readonly Logger _logger;

    public LogManagerTests()
    {
        this._logger = new Logger();
    }

    [Fact]
    public void LogInfoTest()
    {
        _logger.LogInfo("Info test");
    }

    [Fact]
    public void LogInfoDebug()
    {
        _logger.LogDebug("Info test");
    }

    [Fact]
    public void LogInfoError()
    {
        _logger.LogError("Info test");
    }

    [Fact]
    public void LogInfoWarn()
    {
        _logger.LogWarning("Info test");
    }
}