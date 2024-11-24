using CalculatorApp.Source;
using Xunit;

namespace CalculatorApp.Tests;

public class CalculatorTest
{
    [Theory]
    [InlineData("1+2", 3)]
    [InlineData("5-2", 3)]
    [InlineData("6*9", 54)]
    [InlineData("100/25", 4)]
    public void ComputeSimpleExpressionReturnCorrectResult(string expression, double expectedResult)
    {
        Calculator calculator = new Calculator();

        var result = calculator.Compute(expression);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void ComputeExpressionWithParentesisReturnCorrectResult()
    {
        string expression = "(1 + 2) * 2";
        double expectedResult = 6;
        Calculator calculator = new Calculator();

        var result = calculator.Compute(expression);

        Assert.Equal(expectedResult, result);
    }
}
