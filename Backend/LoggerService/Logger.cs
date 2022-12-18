using NLog;
using NLog.Config;
using NLog.Targets;

namespace LoggerService;

public class Logger : ILogger
{
    private readonly NLog.Logger _logger;

    public Logger(string loggerFilePath = null)
    {
        var loggingConfiguration = new LoggingConfiguration();
        var logTargetToConsole = new ColoredConsoleTarget("logConsole");
        loggingConfiguration.AddRule(LogLevel.Info, LogLevel.Fatal, logTargetToConsole);
        if (loggerFilePath != null)
        {
            var logTargetToSpecificFilePath = new FileTarget("logFile") {FileName = loggerFilePath};
            loggingConfiguration.AddRule(LogLevel.Info, LogLevel.Fatal, logTargetToSpecificFilePath);
        }

        LogManager.Configuration = loggingConfiguration;
        LogManager.Setup();
        _logger = LogManager.GetCurrentClassLogger();
    }

    public void LogInfo(string message)
    {
        _logger.Info(message);
    }

    public void LogDebug(string message)
    {
        _logger.Debug(message);
    }

    public void LogWarning(string message)
    {
        _logger.Warn(message);
    }

    public void LogError(string message)
    {
        _logger.Error(message);
    }
}