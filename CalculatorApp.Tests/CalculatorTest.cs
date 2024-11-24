using CalculatorApp.Source;
using Xunit;

namespace CalculatorApp.Tests;

public class CalculatorTest
{
    [Fact]
    public void ComputeSimpleExpressionReturnCorrectResult()
    {
        string expression = "1+2";
        double expectedResult = 3;
        Calculator calculator = new Calculator();

        var result = calculator.Compute(expression);

        Assert.Equal(expectedResult, result);
    }
}
