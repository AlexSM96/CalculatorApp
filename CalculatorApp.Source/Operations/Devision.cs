using CalculatorApp.Source.Interfaces;

namespace CalculatorApp.Source.Operations;

public class Devision : IOperation
{
    public string Symbol => "/";

    public int Priority => 2;

    public double Execute(double leftOperand, double rightOperand)
    {
        if(rightOperand == 0)
        {
            throw new DivideByZeroException("Деление на ноль");
        }

        return leftOperand / rightOperand;
    }
}
