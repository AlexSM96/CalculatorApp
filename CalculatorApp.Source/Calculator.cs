using CalculatorApp.Source.Interfaces;
using CalculatorApp.Source.Operations;
using System.Text.RegularExpressions;

namespace CalculatorApp.Source;

public class Calculator
{
    private readonly List<IOperation> _operations;

    public Calculator()
    {
        _operations = new()
        {
           new Addition(),
           new Subtraction(),
           new Multiplication(),
           new Devision()
        };
    }

    /// <summary>
    /// Метод для расчета математического выражения из строки
    /// </summary>
    public double Compute(string expression)
    {
        var infixTokens = Tokenize(expression);
        var postfixTokens = ConvertToPostfix(infixTokens);
        var results = new Stack<double>();
        foreach (string token in postfixTokens)
        {
            if (double.TryParse(token, out double value))
            {
                results.Push(value);
                continue;
            }

            var operation = _operations.FirstOrDefault(x => x.Symbol.Equals(token));
            if (operation is null)
            {
                continue;
            }

            if (!results.TryPop(out double rightOperand) || !results.TryPop(out double leftoperand))
            {
                throw new InvalidOperationException("Неверный формат выражения");
            }

            double operationResult = operation.Execute(leftoperand, rightOperand);
            results.Push(operationResult);
        }

        if (results.Count != 1)
        {
            throw new InvalidOperationException("Неверный формат выражения");
        }

        return Math.Round(results.Pop(), 2);
    }

    /// <summary>
    /// Метод позволяет рыширить список базовых оперций (+ - * /)
    /// </summary>
    /// <returns>True если опрция добавлена, иначе False</returns>
    public bool TryRegisterOperation(IOperation operation)
    {
        if (_operations.Any(x => x.Symbol.Equals(operation.Symbol)))
        {
            return false;
        }

        _operations.Add(operation);
        return true;
    }

    /// <summary>
    /// Метод позволяет преобразовать строку "expression" в последовательный список из операндов и операторов 
    /// </summary>
    private List<string> Tokenize(string expression)
    {
        return Regex.Split(expression, @$"(\d+\,\d+|\d+|[+/*()-])")
            .Where(token => !string.IsNullOrWhiteSpace(token))
            .Select(token => token.Trim())
            .ToList();
    }

    /// <summary>
    /// Метод для преобразования инфиксного представления в постфиксное (Польская обратная нотация) 
    /// </summary>
    private List<string> ConvertToPostfix(List<string> infixTokens)
    {
        var operators = new Stack<string>();
        var resultQueue = new List<string>();
        foreach (string token in infixTokens)
        {
            var operation = _operations.FirstOrDefault(x => x.Symbol.Equals(token));
            if (operation is not null)
            {
                while (operators.TryPeek(out string? op) && !string.IsNullOrWhiteSpace(op) && !op.Equals("(")
                         && GetOperationPriority(op) >= operation.Priority)
                {
                    resultQueue.Add(operators.Pop());
                }

                operators.Push(token);
            }
            else
            {
                if (double.TryParse(token, out var result))
                {
                    resultQueue.Add(token);
                }
                else
                {
                    ProcessParentheses(token, resultQueue, operators);
                }
            }
        }

        while (operators.Count > 0)
        {
            resultQueue.Add(operators.Pop());
        }

        return resultQueue;
    }

    /// <summary>
    /// Метод для обработки вырожений со скобками.
    /// Превращает выражение в постфиксную нотацию убирая из выражения скобки 
    /// </summary>
    private void ProcessParentheses(string token, List<string> resultQueue, Stack<string> operators)
    {
        switch (token)
        {
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
        }
    }

    /// <summary>
    /// Метод для определения приоритета математичекой операции.
    /// Чем больше результат метода, тем приоритетнее входная операция.
    /// </summary>
    private int GetOperationPriority(string token)
    {
        var operation = _operations.FirstOrDefault(x => x.Symbol.Equals(token));
        if (operation is null)
        {
            throw new ArgumentException($"Неизвестная математическая операция: {token}");
        }

        return operation.Priority;
    }
}
