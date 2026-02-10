using System.Globalization;

namespace Calculator.Library.Models;

public class CalculationResult( ) 
{
    public double Value { get; set; } = 0.00;

    public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);
}