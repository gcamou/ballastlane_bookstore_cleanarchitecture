using Microsoft.Extensions.Logging;

namespace Domain.Core.Logger;
public class ApplicationLogger
{
    public static ILoggerFactory LoggerFactory { get; set; }
    public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
    public static ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);
}