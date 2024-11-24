using System.Text.RegularExpressions;

namespace CalculatorApp.Source;

public class Calculator
{
    private readonly Dictionary<string, Func<double, double, double>> _operations;

    public Calculator()
    {
        _operations = new()
        {
            ["+"] = (x, y) => x + y,
            ["-"] = (x, y) => x - y,
            ["*"] = (x, y) => x * y,
            ["/"] = (x, y) => x / y,
        };
    }

    public double Compute(string expression)
    {
        var tokenizeExpression = Tokenize(expression);

        if(tokenizeExpression.Count != 3 || !double.TryParse(tokenizeExpression[0], out double leftOperand) 
            || !double.TryParse(tokenizeExpression[2], out double rightOperand))
        {
            throw new InvalidOperationException("Неверный формат выражения");
        }

        if (!_operations.ContainsKey(tokenizeExpression[1]))
        {
            throw new InvalidOperationException("Неверный формат выражения");
        }

        return _operations[tokenizeExpression[1]].Invoke(leftOperand, rightOperand);
    }


    /// <summary>
    /// Метод позволяет преобразовать строку "expression" в последовательный список из операндов и операторов 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns>Список строк</returns>
    private List<string> Tokenize(string expression)
    {
        var regexPattern = new Regex(@"(\d+\,\d+|\d+|[+*/()-])");

        return regexPattern
            .Matches(expression)
            .Select(match => match.Value.ToString())
            .ToList(); 
    }
}
