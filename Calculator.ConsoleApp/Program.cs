using CalculatorApp.Source;

var calculator = new Calculator();

while (true)
{
    Console.Write($"Введите выражение: ");
    string expression = Console.ReadLine() ?? string.Empty;
    if (string.IsNullOrWhiteSpace(expression))
    {
        continue;
    }

    var result = calculator.Compute(expression);
    Console.WriteLine($"Результат: {result}");
    Console.WriteLine("Нажмите 'Enter' чтобы продолжить");
    Console.ReadLine();
}
