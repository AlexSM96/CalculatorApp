using CalculatorApp.Source.Interfaces;

namespace CalculatorApp.Source.Operations;

public class Multiplication : IOperation
{
    public string Symbol => "*";

    public int Priority => 2;

    public double Execute(double leftOperand, double rightOperand)
    {
        return leftOperand * rightOperand;
    }
}
