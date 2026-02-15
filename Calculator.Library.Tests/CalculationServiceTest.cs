using Calculator.Library;
using Calculator.Library.Configuration;
using Calculator.Library.Exceptions;
using Calculator.Library.Models;
using Calculator.Library.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Calculator.Tests;

public class CalculatorServiceTests
{
    [Theory]
    [InlineData(10, 5, 15)]
    [InlineData(3.14159, 2.71828, 5.86)]
    [InlineData(-5, 10, 5)]
    [InlineData(0, 0, 0)]
    [InlineData(100.555, 0.445, 101.00)]
    public void Add_ShouldReturnCorrectResult(double a, double b, double expected)
    {
        // Arrange
        var loggerMock = TestHelpers.CreateLoggerMock<CalculatorService>();
        var options = TestHelpers.CreateOptions(precision: 2);

        ICalculatorService calculator = new CalculatorService(options, loggerMock.Object);

        // Act
        CalculationResult result = calculator.Add(a, b);

        // Assert
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(5, 10, -5)]
    [InlineData(100.777, 50.777, 50.00)]
    [InlineData(0, 0, 0)]
    public void Subtract_ShouldReturnCorrectResult(double a, double b, double expected)
    {
        // Arrange
        var loggerMock = TestHelpers.CreateLoggerMock<CalculatorService>();
        var options = TestHelpers.CreateOptions(precision: 2);

        ICalculatorService calculator = new CalculatorService(options, loggerMock.Object);

        // Act
        CalculationResult result = calculator.Subtract(a, b);

        // Assert
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData(10, 5, 50)]
    [InlineData(2.5, 4, 10.00)]
    [InlineData(-5, 4, -20.00)]
    [InlineData(0, 100, 0)]
    public void Multiply_ShouldReturnCorrectResult(double a, double b, double expected)
    {
        // Arrange
        var loggerMock = TestHelpers.CreateLoggerMock<CalculatorService>();
        var options = TestHelpers.CreateOptions(precision: 2);

        ICalculatorService calculator = new CalculatorService(options, loggerMock.Object);

        // Act
        CalculationResult result = calculator.Multiply(a, b);

        // Assert
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData(10, 5, 2.00)]
    [InlineData(100, 4, 25.00)]
    [InlineData(7.5, 2.5, 3.00)]
    [InlineData(-10, 2, -5.00)]
    public void Divide_ShouldReturnCorrectResult(double a, double b, double expected)
    {
        // Arrange
        var loggerMock = TestHelpers.CreateLoggerMock<CalculatorService>();
        var options = TestHelpers.CreateOptions(precision: 2);

        ICalculatorService calculator = new CalculatorService(options, loggerMock.Object);

        // Act
        CalculationResult result = calculator.Divide(a, b);

        // Assert
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData(2, 3, 8.00)]
    [InlineData(10, 2, 100.00)]
    [InlineData(2.5, 2, 6.25)]
    public void Power_ShouldReturnCorrectResult(double baseValue, double exponent, double expected)
    {
        // Arrange
        var loggerMock = TestHelpers.CreateLoggerMock<CalculatorService>();
        var options = TestHelpers.CreateOptions(precision: 2);

        ICalculatorService calculator = new CalculatorService(options, loggerMock.Object);

        // Act
        CalculationResult result = calculator.Power(baseValue, exponent);

        // Assert
        Assert.Equal(expected, result.Value);
    }

    [Theory]
    [InlineData(4, 2.00)]
    [InlineData(25, 5.00)]
    [InlineData(2, 1.41)]
    public void SquareRoot_ShouldReturnCorrectResult(double value, double expected)
    {
        // Arrange
        var loggerMock = TestHelpers.CreateLoggerMock<CalculatorService>();
        var options = TestHelpers.CreateOptions(precision: 2);

        ICalculatorService calculator = new CalculatorService(options, loggerMock.Object);

        // Act
        CalculationResult result = calculator.SquareRoot(value);

        // Assert
        Assert.Equal(expected, result.Value, 0.01); // с небольшой погрешностью для корня
    }

    [Fact]
    public void Add_WithHighPrecision_ShouldRoundCorrectly()
    {
        // Arrange
        var loggerMock = TestHelpers.CreateLoggerMock<CalculatorService>();
        var options = TestHelpers.CreateOptions(precision: 4);

        ICalculatorService calculator = new CalculatorService(options, loggerMock.Object);

        // Act
        CalculationResult result = calculator.Add(1.23456, 2.34567);

        // Assert
        Assert.Equal(3.5802, result.Value);
    }
}