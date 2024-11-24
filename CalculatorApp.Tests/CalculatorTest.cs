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

    [Theory]
    [InlineData("(1 + 2) * 2", 6)]
    [InlineData("100 * 25 + (32 - 5 + 7) / 2 - 3", 2514)]
    [InlineData("(29 + 231) * 3 - 256 / 8", 748)]
    public void ComputeExpressionWithParentesisReturnCorrectResult(string expression, double expectedResult)
    {
        Calculator calculator = new Calculator();

        var result = calculator.Compute(expression);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData("1,5 + 2,7", 4.2)]
    [InlineData("5,3 - 2", 3.3)]
    [InlineData("6,2 * 9,3", 57.66)]
    [InlineData("100,2 / 25,4", 3.94)]
    public void ComputeExpressionWithDoubleValuesReturnCorrectResult(string expression, double expectedValue) 
    {
        Calculator calculator = new Calculator();

        var result = calculator.Compute(expression);

        Assert.Equal(expectedValue, result);
    }

    [Fact]
    public void ThrowDivideByZeroExceptionWhileComputeExpression()
    {
        string expression = "20 / 0";
        Calculator calculator = new Calculator();

        Func<object?> result = () => calculator.Compute(expression);

        Assert.Throws<DivideByZeroException>(result);
    }

    [Fact]
    public void ThrowInvalidOperationExceptionIfExpressionNotCorrect()
    {
        string expression = "//*20 - 0";
        Calculator calculator = new Calculator();

        Func<object?> result = () => calculator.Compute(expression);

        Assert.Throws<InvalidOperationException>(result);
    }
}
