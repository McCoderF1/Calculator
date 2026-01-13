namespace CalculatorTests
{
    public class CalculatorTests
    {
        [TestCase("5", ExpectedResult = 5L)]
        [TestCase("5+5", ExpectedResult = 10L)]
        [TestCase("12+3*4-5", ExpectedResult = 19L)]
        [TestCase("8/2*3", ExpectedResult = 12L)]
        public long EvaluateExpression_ValidExpressions_ResultReturned(string expr)
        {
            Calculator.Engine.Calculator calculator = new();

            long? result = calculator.EvaluateExpression(expr);

            Assert.That(result.HasValue, Is.True);
            return result!.Value;
        }

        [Test]
        public void EvaluateExpression_DivideByZero_NullReturned()
        {
            Calculator.Engine.Calculator calculator = new();

            long? result = calculator.EvaluateExpression("1/0");

            Assert.That(result, Is.Null);
        }
    }
}
