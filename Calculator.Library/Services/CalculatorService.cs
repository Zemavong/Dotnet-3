using Calculator.Library.Configuration;
using Calculator.Library.Exceptions;
using Calculator.Library.Models;
using Calculator.Library.Services;
using Microsoft.Extensions.Logging;

namespace Calculator.Library;

public class CalculatorService(Microsoft.Extensions.Options.IOptions<CalculatorOptions> options, ILogger<CalculatorService> logger) : ICalculatorService
{

    private readonly CalculatorOptions _options = options.Value;

    private readonly ILogger<CalculatorService> _logger = logger;

    CalculationResult ICalculatorService.Add(double a, double b)
    {
        CalculationResult result = new CalculationResult();
        result.Value = Math.Round(a + b, _options.Precision);
        ValidateResult(result.Value);
        return result;
    }


    CalculationResult ICalculatorService.Subtract(double a, double b)
    {
        CalculationResult result = new CalculationResult();
        result.Value = Math.Round(a - b, _options.Precision);
        ValidateResult(a - b);
        return result;
    }

    CalculationResult ICalculatorService.Multiply(double a, double b)
    {
        CalculationResult result = new CalculationResult();
        result.Value = Math.Round(a * b, _options.Precision);
        ValidateResult(result.Value);
        return result;
    }

    CalculationResult ICalculatorService.Divide(double a, double b)
    {
        if (b == 0)
        {
            _logger.LogError(CalculationException.DivideByZero(), "Попытка поделить на ноль");
            throw CalculationException.DivideByZero();
        } else
        {
            CalculationResult result = new CalculationResult();
            result.Value = Math.Round(a / b, _options.Precision);
            ValidateResult(result.Value);
            return result;
        }
    }

    CalculationResult ICalculatorService.Power(double baseValue, double exponent)
    {
        CalculationResult result = new CalculationResult();
        result.Value = Math.Round(Math.Pow(baseValue, exponent), _options.Precision);
        ValidateResult(result.Value);
        return result;
    }

    CalculationResult ICalculatorService.SquareRoot(double value)
    {
        CalculationResult result = new CalculationResult();
        result.Value = Math.Round(Math.Sqrt(value), _options.Precision);
        ValidateResult(result.Value);
        return result;
    }

    private void ValidateResult(double result)
    {
        if (result < 0 && !_options.AllowNegativeResults)
        {
            _logger.LogError(CalculationException.IsNegative(), CalculationException.IsNegative().Message); 
            throw CalculationException.IsNegative();
        }
        if (result >= _options.MaxValue)
        {
            _logger.LogError(CalculationException.OverflowMaxValue(result), CalculationException.OverflowMaxValue(result).Message);
            throw CalculationException.OverflowMaxValue(result);

        }
    }
}