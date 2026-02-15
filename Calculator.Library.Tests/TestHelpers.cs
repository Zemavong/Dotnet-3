using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Calculator.Library.Configuration;
using Calculator.Library.Services;

namespace Calculator.Tests;

public static class TestHelpers
{
    /// <summary>
    /// Создаёт мок логгера для указанного типа
    /// </summary>
    public static Mock<ILogger<T>> CreateLoggerMock<T>() where T : class
    {
        return new Mock<ILogger<T>>();
    }

    /// <summary>
    /// Создаёт настройки калькулятора через IOptions<T>
    /// </summary>
    public static IOptions<CalculatorOptions> CreateOptions(
        int precision = 2,
        bool enableLogging = true,
        bool allowNegativeResults = true,
        double maxValue = double.MaxValue)
    {
        var options = new CalculatorOptions
        {
            Precision = precision,
            EnableLogging = enableLogging,
            AllowNegativeResults = allowNegativeResults,
            MaxValue = maxValue
        };

        return Options.Create(options);
    }

    /// <summary>
    /// Проверяет, что логгер вызывался с указанным уровнем и сообщением
    /// </summary>
    public static void VerifyLogCalled<T>(
        this Mock<ILogger<T>> loggerMock,
        LogLevel logLevel,
        string messageContains) where T : class
    {
        loggerMock.Verify(
            x => x.Log(
                logLevel,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains(messageContains, StringComparison.OrdinalIgnoreCase)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()
            ),
            Times.AtLeastOnce
        );
    }
}