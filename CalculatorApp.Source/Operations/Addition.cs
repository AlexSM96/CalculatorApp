using CalculatorApp.Source.Interfaces;

namespace CalculatorApp.Source.Operations;

public class Addition : IOperation
{
    public string Symbol => "+";

    public int Priority => 1;

    public double Execute(double leftOperand, double rightOperand)
    {
        return leftOperand + rightOperand;
    }
}
