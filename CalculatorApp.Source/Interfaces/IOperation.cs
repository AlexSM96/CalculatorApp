namespace CalculatorApp.Source.Interfaces;

public interface IOperation
{
    public string Symbol { get; }

    public int Priority { get; }

    public double Execute(double leftOperand, double rightOperand);
}
