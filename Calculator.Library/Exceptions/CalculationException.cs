namespace Calculator.Library.Exceptions;

public class CalculationException(string message, string? errorCode = null) : Exception(message)
{

    public static CalculationException DivideByZero() =>
        new("Деление на ноль запрещено.", "DIV_BY_ZERO");

    public static CalculationException IsNegative() =>
        new("Результат вычисления не может быть отрицательным", "INVALID_RESULT");

    public static CalculationException OverflowMaxValue(double maxValue) =>
        new($"Результат превышает допустимое значение: {maxValue}.", "RESULT_TOO_LARGE");
}