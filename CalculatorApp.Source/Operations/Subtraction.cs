using CalculatorApp.Source.Interfaces;

namespace CalculatorApp.Source.Operations;

public class Subtraction : IOperation
{
    public string Symbol => "-";

    public double Execute(double leftOperand, double rightOperand)
    {
        return leftOperand - rightOperand;
    }
}
