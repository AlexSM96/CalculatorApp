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
        var infixTokens = Tokenize(expression);
        var postfixTokens = ConvertToPostfix(infixTokens);
        var results = new Stack<double>();
        foreach (string token in postfixTokens) 
        {
            if(double.TryParse(token, out double value))
            {
                results.Push(value);
                continue;
            }

            if (!_operations.ContainsKey(token))
            {
                continue;
            }

            if (!results.TryPop(out double rightOperand) || !results.TryPop(out double leftoperand))
            {
                throw new InvalidOperationException("Неверный формат выражения");
            }

            if(rightOperand == 0 && token.Equals("/"))
            {
                throw new DivideByZeroException("Деление на ноль");
            }

            double operationResult = _operations[token].Invoke(leftoperand, rightOperand);
            results.Push(operationResult);
        }

        if (results.Count != 1)
        {
            throw new InvalidOperationException("Неверный формат выражения");
        }

        return Math.Round(results.Pop(), 2);
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

    /// <summary>
    /// Метод для преобразования инфиксного представления в постфиксное (Польская обратная нотация) 
    /// </summary>
    /// <param name="infixTokens"></param>
    /// <returns></returns>
    private List<string> ConvertToPostfix(List<string> infixTokens)
    {
        var operators = new Stack<string>();
        var resultQueue = new List<string>();
        foreach (string token in infixTokens) 
        {
            switch (token)
            {
                case "+":
                case "-":
                    while (operators.TryPeek(out string? op) && !string.IsNullOrWhiteSpace(op) && !op.Equals("(") 
                        && GetOperationPriority(op) >= 1)
                    {
                        resultQueue.Add(operators.Pop());
                    }

                    operators.Push(token);
                    break;
                case "*":
                case "/":
                    while (operators.TryPeek(out string? op) && !string.IsNullOrWhiteSpace(op) && !op.Equals("(") 
                        && GetOperationPriority(op) >= 2)
                    {
                        resultQueue.Add(operators.Pop());
                    }

                    operators.Push(token);
                    break;
                case "(":
                    operators.Push(token);
                    break;
                case ")":
                    while (operators.TryPeek(out string? op) && !string.IsNullOrWhiteSpace(op) && !op.Equals("("))
                    {
                        resultQueue.Add(operators.Pop());
                    }

                    operators.Pop();
                    break;
                default:
                    resultQueue.Add(token);
                    break;
            }
        }

        while(operators.Count > 0)
        {
            resultQueue.Add(operators.Pop());
        }

        return resultQueue;
    }

    /// <summary>
    /// Метод для определения приоритета математичекой операции.
    /// Чем больше результат метода, тем приоритетнее входная операция.
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private int GetOperationPriority(string operation)
    {
        return operation switch
        {
            "+" or "-" => 1,
            "*" or "/" => 2,
            _ => throw new ArgumentException($"Неизвестная математическая операция: {operation}")
        };
    }
}
