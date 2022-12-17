using NLog;
using NLog.Layouts;

namespace LoggerService;

public class LogManager : ILogManager
{
    private readonly Logger _logger;

    public LogManager(string loggerFilePath = null)
    {
        var loggingConfiguration = new NLog.Config.LoggingConfiguration();
        var logTargetToConsole = new NLog.Targets.ColoredConsoleTarget("logConsole");
        loggingConfiguration.AddRule(LogLevel.Info, LogLevel.Fatal, logTargetToConsole);
        if (loggerFilePath != null)
        {
            var logTargetToSpecificFilePath = new NLog.Targets.FileTarget("logFile") {FileName = loggerFilePath};
            loggingConfiguration.AddRule(LogLevel.Info, LogLevel.Fatal, logTargetToSpecificFilePath);
        }

        NLog.LogManager.Configuration = loggingConfiguration;
        NLog.LogManager.Setup();
        this._logger = NLog.LogManager.GetCurrentClassLogger();
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