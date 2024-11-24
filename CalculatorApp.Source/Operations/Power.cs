using CalculatorApp.Source.Interfaces;

namespace CalculatorApp.Source.Operations;

public class Power : IOperation
{
    public string Symbol => "^";

    public double Execute(double leftOperand, double rightOperand)
    {
        return Math.Pow(leftOperand, rightOperand);
    }
}
