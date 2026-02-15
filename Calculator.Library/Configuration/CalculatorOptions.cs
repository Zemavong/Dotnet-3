namespace Calculator.Library.Configuration;

public class CalculatorOptions
{
    public const string SectionName = "Calculator";
    public bool EnableLogging { get; set; }
    public int MaxHistorySize { get; set; }
    public double MaxValue { get; init; } = double.MaxValue;
    public int Precision { get; init; } = 15;
    public bool AllowNegativeResults { get; init; } = true;
}